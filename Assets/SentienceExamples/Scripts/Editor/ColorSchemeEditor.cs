#if UNITY_EDITOR
using Sentience.Demo;
using UnityEditor;
using UnityEngine;

namespace Sentience.Demo.EditorExtensions
{
    [CustomEditor(typeof(ColorSchemeManager))]
    public class ColorSchemeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ColorSchemeManager colorSchemeManager = (ColorSchemeManager)target;

            if (GUILayout.Button("Apply"))
            {
                colorSchemeManager.ApplyColorScheme();
            }
        }
    }
}
#endif