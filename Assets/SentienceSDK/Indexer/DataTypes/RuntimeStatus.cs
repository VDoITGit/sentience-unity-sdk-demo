using System.Numerics;

namespace Sentience
{
    [System.Serializable]
    public class RuntimeStatus
    {
        public bool healthOK;
        public bool indexerEnabled;
        public string startTime;
        public double uptime;
        public string ver;
        public string branch;
        public string commitHash;
        public BigInteger chainID;
        public RuntimeChecks checks;
    }
}