using System.Threading.Tasks;
using Sentience.WaaS.Authentication;
using SentienceSDK.WaaS;

namespace Sentience.WaaS
{
    public interface IIntentSender
    {
        public Task<T> SendIntent<T, T2>(T2 args, IntentType type, uint timeBeforeExpiryInSeconds = 30);
        public Task<bool> DropSession(string dropSessionId);
        public Task<T> PostIntent<T>(string payload, string path);
        public Task<WaaSSession[]> ListSessions();
        public Task<SuccessfulTransactionReturn> GetTransactionReceipt(SuccessfulTransactionReturn response);
    }
}