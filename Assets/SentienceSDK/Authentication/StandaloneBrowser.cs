using UnityEngine.Device;

namespace Sentience.Authentication
{
    public class StandaloneBrowser : IBrowser
    {
        public void Authenticate(string url, string redirectUrl = "")
        {
            Application.OpenURL(url);
        }
    }
}