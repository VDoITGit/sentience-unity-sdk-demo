using System.Threading.Tasks;
using UnityEngine;

namespace Sentience
{
    public static class AsyncExtensions
    {
        /// <summary>
        /// Similar to Task.Delay but is thread safe
        /// Task.Delay creates a separate thread. This makes Task.Delay incompatible with WebGL builds
        /// This method does not create a separate thread and therefore works with WebGL builds
        /// </summary>
        /// <param name="delayInSeconds"></param>
        public static async Task DelayTask(float delayInSeconds)
        {
            float currentTime = Time.time;
            float elapsedTime = 0;
            while (elapsedTime < delayInSeconds)
            {
                elapsedTime = Time.time - currentTime;
                await Task.Yield();
            }
        }
    }
}