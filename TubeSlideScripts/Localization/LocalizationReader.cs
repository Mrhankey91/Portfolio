using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Localization
{
    public static class LocalizationReader
    {
        private static string pathCSV = "Localization/Localization";
        private static Dictionary<string, Dictionary<string, string>> localizationData = new Dictionary<string, Dictionary<string, string>>();
        public static string language = "en";

        public static void LoadLocalizationData()
        {
            /* using (StreamReader reader = new StreamReader(pathCSV))
             {
                 while (!reader.EndOfStream)
                 {
                     string line = reader.ReadLine();

                     if (line == null)
                         continue; 

                     string[] values = line.Split(';');
                     Debug.Log(line);
                 }
             }*/

            TextAsset localizationFile = Resources.Load<TextAsset>(pathCSV) as TextAsset;
            string[] lines = Regex.Split(localizationFile.text, "\n|\r|\r\n");
            for(int i = 0; i < lines.Length; ++i)
            {
                if (lines[i] == null || lines[i] == "")
                    continue;

                string[] split = lines[i].Split(',');
                if(i == 0)
                {
                    CreateLanguageDictonaries(split);
                }
                else
                {
                    AddKeyValuesToDictionaries(split);
                }
            }
        }

        private static void CreateLanguageDictonaries(string[] input)
        {
            for(int i = 1; i < input.Length; ++i)
            {
                localizationData.Add(input[i], new Dictionary<string, string>());
            }
        }

        private static void AddKeyValuesToDictionaries(string[] input)
        {
            for (int i = 1; i < input.Length; ++i)
            {
                localizationData[localizationData.ElementAt(i-1).Key].Add(input[0], input[i]);
            }
        }

        public static string GetLocalizationByKey(string key)
        {
            if (localizationData.ContainsKey(language)) {
                Dictionary<string, string> data = localizationData[language];

                string output = "";
                if(data.TryGetValue(key, out output))
                    return output;
                else
                    return key;
            }
            else {
                return key;
            }
        }

        public static string GetLocalizationByKey(string key, string language)
        {
            if (localizationData.ContainsKey(language)) {
                Dictionary<string, string> data = localizationData[language];

                string output = "";
                if(data.TryGetValue(key, out output))
                    return output;
                else
                    return key;
            }
            else {
                return key;
            }
        }

    }
}