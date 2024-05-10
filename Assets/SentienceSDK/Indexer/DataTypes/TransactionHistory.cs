using System.Numerics;

namespace Sentience
{
    [System.Serializable]
    public class TransactionHistory
    {
        public string txnHash;
        public BigInteger blockNumber;
        public string blockHash;
        public BigInteger chainId;
        public string metaTxnID;
        public TxnTransfer[] transfers;
        public string timestamp;
    }
}