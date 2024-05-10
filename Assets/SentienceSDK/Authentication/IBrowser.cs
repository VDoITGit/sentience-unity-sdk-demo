using System.Threading.Tasks;

namespace Sentience.Authentication
{
    public interface IBrowser
    {
        public void Authenticate(string url, string redirectUrl = "");
    }
}