using System;
using System.IO;
using Sentience.Config;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEngine;
#if UNITY_IOS || UNITY_STANDALONE_OSX
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
#endif

namespace Sentience.Editor
{
    public class CheckUrlScheme
    {
        private static string _plistPath;
        private static string _urlScheme;
        
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            SentienceConfig config = SentienceConfig.GetConfig();
            _urlScheme = config.UrlScheme;

            if (string.IsNullOrWhiteSpace(_urlScheme))
            {
                Debug.LogWarning(SentienceConfig.MissingConfigError("Url Scheme").Message);
            }
            
            if (target == BuildTarget.iOS)
            {
                _plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
                CheckPlistUrlScheme();
            }
            else if (target == BuildTarget.StandaloneOSX)
            {
                _plistPath = Path.Combine(pathToBuiltProject, "Contents/Info.plist");
                CheckPlistUrlScheme();
            }
        }

        private static Exception _missingUrlSchemeException = new BuildFailedException(
            "URL Scheme not set in Unity Editor. Please follow the instructions here or social sign in will not work! https://docs.Sentience.xyz/unity-waas-sdk/authentication");
        
        private static void CheckPlistUrlScheme()
        {
#if UNITY_STANDALONE_OSX
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(_plistPath);

            PlistElementDict rootDict = plist.root;
            if (!rootDict.values.ContainsKey("CFBundleURLTypes"))
            {
                throw _missingUrlSchemeException;
            }
            if (rootDict.values["CFBundleURLTypes"] is PlistElementArray existingArray)
            {
                if (existingArray.values.Count == 0)
                {
                    throw _missingUrlSchemeException;
                }
                if (existingArray.values[0] is PlistElementDict existingDict)
                {
                    if (!existingDict.values.ContainsKey("CFBundleURLSchemes"))
                    {
                        throw _missingUrlSchemeException;
                    }
                    if (existingDict.values["CFBundleURLSchemes"] is PlistElementArray newArray)
                    {
                        List<PlistElement> values = newArray.values;
                        int count = values.Count;
                        if (count == 0)
                        {
                            throw _missingUrlSchemeException;
                        }
                        for (int i =0; i < count; i++)
                        {
                            PlistElement plistElement = values[i];
                            if (plistElement is PlistElementString plistElementString)
                            {
                                if (plistElementString.value == _urlScheme)
                                {
                                    return;
                                }
                            }
                        }
                        throw _missingUrlSchemeException;
                    }
                }
            }
#endif
        }
    }
}