using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sentience.Config
{
    [CreateAssetMenu(fileName = "SentienceConfig", menuName = "Sentience/SentienceConfig", order = 1)]
    public class SentienceConfig : ScriptableObject
    {
        [Header("Social Sign In Configuration - Standalone Platforms")]
        public string UrlScheme;
        public string GoogleClientId;
        public string DiscordClientId;
        public string FacebookClientId;
        public string AppleClientId;
        
        [Header("Social Sign In Configuration - iOS")]
        public string GoogleClientIdIOS;
        public string DiscordClientIdIOS;
        public string FacebookClientIdIOS;
        public string AppleClientIdIOS;
        
        [Header("Social Sign In Configuration - Android")]
        public string GoogleClientIdAndroid;
        public string DiscordClientIdAndroid;
        public string FacebookClientIAndroid;
        public string AppleClientIdAndroid;

        [Header("WaaS Configuration")]
        public string WaaSVersion = "1.0.0";
        public string WaaSConfigKey;

        [Header("Sentience SDK Configuration")] 
        public string BuilderAPIKey;
        
        private static SentienceConfig _config;

        public static SentienceConfig GetConfig()
        {
            if (_config == null)
            {
                _config = Resources.Load<SentienceConfig>("SentienceConfig");
            }

            if (_config == null)
            {
                throw new Exception("SentienceConfig not found. Make sure to create and configure it and place it at the root of your Resources folder. Create it from the top bar with Assets > Create > Sentience > SentienceConfig");
            }

            return _config;
        }

        public static Exception MissingConfigError(string valueName)
        {
            return new Exception($"{valueName} is not set. Please set it in SentienceConfig asset in your Resources folder.");
        }

        public static ConfigJwt GetConfigJwt()
        {
            string configKey = _config.WaaSConfigKey;
            if (string.IsNullOrWhiteSpace(configKey))
            {
                throw SentienceConfig.MissingConfigError("WaaS Config Key");
            }

            return JwtHelper.GetConfigJwt(configKey);
        }
    }
}