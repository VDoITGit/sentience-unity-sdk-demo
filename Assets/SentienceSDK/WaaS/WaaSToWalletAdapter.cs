using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Sentience;
using Sentience.Provider;
using Sentience.WaaS;
using System;
using Sentience.ABI;
using Sentience.Transactions;
using Sentience.Utils;
using SentienceSDK.WaaS;

namespace Sentience.WaaS
{
    public class WaaSToWalletAdapter : Sentience.Wallet.IWallet
    {
        private IWallet _wallet;

        public WaaSToWalletAdapter(IWallet wallet)
        {
            _wallet = wallet;
        }
        
        public Address GetAddress()
        {
            return _wallet.GetWalletAddress();
        }

        public async Task<TransactionReceipt> DeployContract(IEthClient client, string bytecode, ulong value = 0)
        {
            string chainId = await client.ChainID();
            Chain network = chainId.ChainFromHexString();
            ContractDeploymentReturn result = await _wallet.DeployContract(network, bytecode, value.ToString());
            if (result is FailedContractDeploymentReturn failedResult)
            {
                throw new Exception("Failed to deploy contract: " + failedResult.Error);
            }
            else if (result is SuccessfulContractDeploymentReturn successfulResult)
            {
                string transactionHash = successfulResult.TransactionReturn.txHash;
                TransactionReceipt receipt = await client.WaitForTransactionReceipt(transactionHash);
                if (receipt.contractAddress == null)
                {
                    receipt.contractAddress = successfulResult.DeployedContractAddress.Value;
                }

                return receipt;
            }
            else
            {
                throw new Exception($"Unknown contract deployment result type. Given {result.GetType().Name}");
            }
        }

        public async Task<string> SendTransaction(IEthClient client, EthTransaction transaction)
        {
            RawTransaction waasTransaction = new RawTransaction(transaction.To, transaction.Value.ToString(), transaction.Data);
            IntentDataSendTransaction args = await BuildTransactionArgs(client, new Transaction[] { waasTransaction });
            TransactionReturn result = await _wallet.SendTransaction(args.network.ChainFromHexString(), args.transactions);
            if (result is FailedTransactionReturn failedResult)
            {
                throw new Exception(failedResult.error);
            }
            else if (result is SuccessfulTransactionReturn successfulResult)
            {
                return successfulResult.txHash;
            }
            else
            {
                throw new Exception($"Unknown transaction result type. Given {result.GetType().Name}");
            }
        }

        private async Task<IntentDataSendTransaction> BuildTransactionArgs(IEthClient client, Transaction[] transactions)
        {
            string networkId = await client.ChainID();
            IntentDataSendTransaction args = new IntentDataSendTransaction(GetAddress(), networkId, transactions);
            return args;
        }

        public async Task<TransactionReceipt> SendTransactionAndWaitForReceipt(IEthClient client, EthTransaction transaction)
        {
            string transactionHash = await SendTransaction(client, transaction);
            TransactionReceipt receipt = await client.WaitForTransactionReceipt(transactionHash);
            return receipt;
        }

        public async Task<string[]> SendTransactionBatch(IEthClient client, EthTransaction[] transactions)
        {
            int transactionCount = transactions.Length;
            if (transactionCount <= 0)
            {
                throw new Exception("Cannot send empty transaction batch");
            }
            RawTransaction[] waasTransactions = new RawTransaction[transactionCount];
            for (int i = 0; i < transactionCount; i++)
            {
                waasTransactions[i] = new RawTransaction(transactions[i].To, transactions[i].Value.ToString(), transactions[i].Data);
            }

            IntentDataSendTransaction args = await BuildTransactionArgs(client, waasTransactions);
            TransactionReturn result = await _wallet.SendTransaction(args.network.ChainFromHexString(), args.transactions);
            if (result is FailedTransactionReturn failedResult)
            {
                throw new Exception(failedResult.error);
            }
            else if (result is SuccessfulTransactionReturn successfulResult)
            {
                return new[] { successfulResult.txHash };
            }
            else
            {
                throw new Exception($"Unknown transaction result type. Given {result.GetType().Name}");
            }
        }

        public async Task<TransactionReceipt[]> SendTransactionBatchAndWaitForReceipts(IEthClient client, EthTransaction[] transactions)
        {
            string[] transactionHashes = await SendTransactionBatch(client, transactions);
            int transactionCount = transactionHashes.Length;
            TransactionReceipt[] receipts = new TransactionReceipt[transactionCount];
            for (int i = 0; i < transactionCount; i++)
            {
                receipts[i] = await client.WaitForTransactionReceipt(transactionHashes[i]);
            }

            return receipts;
        }

        public async Task<string> SignMessage(byte[] message, byte[] chainId)
        {
            string messageString = SentienceCoder.HexStringToHumanReadable(SentienceCoder.ByteArrayToHexString(message));
            string chainIdString =
                SentienceCoder.HexStringToHumanReadable(SentienceCoder.ByteArrayToHexString(chainId));

            return await SignMessage(messageString, chainIdString);
        }

        public async Task<string> SignMessage(string message, string chainId)
        {
            string signature = await _wallet.SignMessage(chainId.ChainFromHexString(), message);
            return signature;
        }

        public async Task<bool> IsValidSignature(string signature, string message, Chain chain)
        {
            var result = await _wallet.IsValidMessageSignature(chain, message, signature);
            return result.isValid;
        }
    }
}