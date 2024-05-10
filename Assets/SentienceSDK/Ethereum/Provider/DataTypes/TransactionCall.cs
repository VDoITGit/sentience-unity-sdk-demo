using System.Numerics;

namespace Sentience
{
    [System.Serializable]
    public class TransactionCall
    {
        public string from;
        public string to;
        public BigInteger gas;
        public BigInteger gasPrice;
        public BigInteger value;
        public string data;

    }
}