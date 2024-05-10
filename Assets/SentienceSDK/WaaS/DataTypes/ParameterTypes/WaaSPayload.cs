using System;

namespace Sentience.WaaS.Authentication
{
    [Serializable]
    public class WaaSPayload
    {
        public string encryptedPayloadKey;
        public string payloadCiphertext;
        public string payloadSig;

        public WaaSPayload(string encryptedPayloadKey, string payloadCiphertext, string payloadSig)
        {
            this.encryptedPayloadKey = encryptedPayloadKey;
            this.payloadCiphertext = payloadCiphertext;
            this.payloadSig = payloadSig;
        }
    }
}