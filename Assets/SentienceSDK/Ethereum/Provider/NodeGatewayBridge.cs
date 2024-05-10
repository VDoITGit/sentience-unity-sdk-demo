using System;
using System.Collections.Generic;
using Sentience;
using Sentience.Config;

namespace Sentience.Provider
{
    public static class NodeGatewayBridge
    {
        private static SentienceConfig _config = SentienceConfig.GetConfig();
        
        private static Dictionary<Chain, string> _pathAt = new Dictionary<Chain, string>()
        {
            { Chain.Ethereum, "mainnet" },
            { Chain.Polygon, "polygon" },
            { Chain.BNBSmartChain, "bsc" },
            { Chain.PolygonZkEvm, "polygon-zkevm" },
            { Chain.ArbitrumOne, "arbitrum" },
            { Chain.ArbitrumNova, "arbitrum-nova" },
            { Chain.Optimism, "optimism" },
            { Chain.Avalanche, "avalanche" },
            { Chain.Gnosis, "gnosis" },
            { Chain.Base, "base" },
            { Chain.OasysHomeverse, "homeverse" },
            { Chain.AstarZKEvm, "astar-zkevm" },
            { Chain.Xai, "xai" },

            { Chain.TestnetSepolia, "sepolia" },
            { Chain.TestnetArbitrumSepolia, "arbitrum-sepolia" },
            { Chain.TestnetBNBSmartChain, "bsc-testnet" },
            { Chain.TestnetBaseSepolia, "base-sepolia" },
            { Chain.TestnetOasysHomeverse, "homeverse-testnet" },
            { Chain.TestnetAvalanche, "avalanche-testnet" },
            { Chain.TestnetOptimisticSepolia, "optimism-sepolia" },
            { Chain.TestnetPolygonAmoy, "amoy" }, 
            { Chain.TestnetAstarZKyoto, "astar-zkyoto" }, 
            { Chain.TestnetXrSepolia, "xr-sepolia" },
            { Chain.TestnetXaiSepolia, "xai-sepolia" }, 
        };
        
        private const string _baseUrl = "https://nodes.Sentience.app/";
        
        public static string GetNodeGatewayUrl(Chain chain)
        {
            if (!_pathAt.ContainsKey(chain))
            {
                throw new Exception(
                    "Network is not supported. Please contact Sentience support and use your own RPC url in the meantime");
            }

            string builderApiKey = _config.BuilderAPIKey;
            if (string.IsNullOrWhiteSpace(builderApiKey))
            {
                throw SentienceConfig.MissingConfigError("Builder API Key");
            }
            
            string url = _baseUrl + _pathAt[chain] + "/" + builderApiKey; 
            return url;
        }
    }
}