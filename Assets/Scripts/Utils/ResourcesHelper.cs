using System;
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
    }
}