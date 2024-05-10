using System.Numerics;

namespace Sentience
{
    [System.Serializable]
    public class RuntimeChecks
    {
        public bool running;
        public string syncMode;
        public BigInteger lastBlockNum;
    }
}