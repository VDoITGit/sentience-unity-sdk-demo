using System;

namespace SentienceSDK.WaaS
{
    [Serializable]
    public class IntentDataCloseSession
    {
        public string sessionId { get; private set; }

        public IntentDataCloseSession(string sessionId)
        {
            this.sessionId = sessionId;
        }
    }
}