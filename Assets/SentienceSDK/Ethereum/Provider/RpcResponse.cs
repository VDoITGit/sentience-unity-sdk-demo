
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Sentience.Provider
{
    [System.Serializable]
    public class RpcResponse 
    {
        public string jsonrpc;
        public int id;
        public JToken result;
        public RpcError error;
    }
}
