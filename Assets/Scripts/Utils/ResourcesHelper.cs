using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class ResourcesHelper
    {
        public static void SetFeaturesFromTextFile(string resourceFile, Action<string, string> setAction)
        {
            Resources.Load<TextAsset>(resourceFile)
                .text.Split('\n')
                .Select(line => line.Split(' ')).ToList()
                .ForEach(token => setAction(token[0], token[1]));
        }

        public static void SetFeaturesFromTextFile(string resourceFile, Action<KeyValuePair<string, string>> setAction)
        {
            Resources.Load<TextAsset>(resourceFile)
                .text.Split('\n')
                .Select(line =>
                    {
                        var firstSpace = line.IndexOf(' ');
                        var key = line.Substring(0, firstSpace);
                        var value = line.Substring(firstSpace + 1);

                        return new KeyValuePair<string, string>(key, value);
                    }
                )
                .ToList()
                .ForEach(setAction);
        }
    }
}
