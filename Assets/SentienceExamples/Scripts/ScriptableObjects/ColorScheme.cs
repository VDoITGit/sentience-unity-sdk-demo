using UnityEngine;

namespace Sentience.Demo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Color Scheme", menuName = "Sentience/Color Scheme")]
    public class ColorScheme : ScriptableObject
    {
        public Color backgroundColor;
        public Color textColor;
        public Color buttonColor;
    }
}