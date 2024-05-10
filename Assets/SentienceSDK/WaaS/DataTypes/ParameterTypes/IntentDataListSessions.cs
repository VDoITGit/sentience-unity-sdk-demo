using System;

namespace SentienceSDK.WaaS
{
    [Serializable]
    public class IntentDataListSessions
    {
        public string wallet { get; private set; }
        
        public IntentDataListSessions(string walletAddress)
        {
            this.wallet = walletAddress;
        }
    }
}