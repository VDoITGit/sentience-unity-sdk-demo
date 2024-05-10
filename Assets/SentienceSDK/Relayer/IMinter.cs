using System;
using System.Threading.Tasks;

namespace Sentience.Relayer
{
    public interface IMinter
    {
        public event Action<string> OnMintTokenSuccess;
        public event Action<string> OnMintTokenFailed;
        public Task<string> MintToken(string tokenId, uint amount = 1);
    }
}