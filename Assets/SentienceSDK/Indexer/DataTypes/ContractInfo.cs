using System.Numerics;

namespace Sentience
{
    [System.Serializable]
    public class ContractInfo
    {
        public BigInteger chainId;
        public string address;
        public string name;
        public string type;
        public string symbol;
        public int decimals;
        public string logoURI;
        public ContractInfoExtensions extensions;
    }
}