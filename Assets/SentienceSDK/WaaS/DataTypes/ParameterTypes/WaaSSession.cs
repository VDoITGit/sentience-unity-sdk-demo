using System;
using Newtonsoft.Json.Linq;
using SentienceSDK.WaaS;

namespace Sentience.WaaS
{
    [Serializable]
    public class WaaSSession
    {
        public string id;
        public string address;
        public string userId;
        public int projectId;
        public Identity identity;
        public string friendlyName;
        public DateTime createdAt;
        public DateTime refreshedAt;
        public DateTime expiresAt;
            
    }
}