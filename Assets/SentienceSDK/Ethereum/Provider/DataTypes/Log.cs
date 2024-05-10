using System.Collections.Generic;
using System.Numerics;

namespace Sentience
{
    [System.Serializable]
    public class Log
    {
        public bool removed;
        public string logIndex;
        public string transactionIndex;
        public string transactionHash;
        public string blockHash;
        public string blockNumber;
        public string address;
        public string data;
        public List<string> topics;
    }
}
