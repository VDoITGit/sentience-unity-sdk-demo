using System;
using System.Collections.Generic;
using UnityEngine;
using Sentience;

namespace Sentience.Demo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Network Icons Mapping", menuName = "Sentience/Network Icons Mapping")]
    public class NetworkIcons : ScriptableObject
    {
        public List<SerializableKeyValuePair<Chain, Sprite>> NetworkIconMapping = new List<SerializableKeyValuePair<Chain, Sprite>>();
        private Dictionary<Chain, Sprite> _networkIconDictionary;

        private void OnEnable()
        {
            _networkIconDictionary = new Dictionary<Chain, Sprite>();
            foreach (var mapping in NetworkIconMapping)
            {
                _networkIconDictionary[mapping.Key] = mapping.Value;
            }
        }

        public Sprite GetIcon(Chain chain)
        {
            if (_networkIconDictionary.ContainsKey(chain))
            {
                return _networkIconDictionary[chain];
            }

            Debug.LogError($"No icon found for chain {chain}. Returning 'default'");
            return default;
        }
    }
}