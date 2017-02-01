
using UnityEngine;

namespace GayProject.DataManagement
{
    public static class FData
    {

        public static void LoadFeature(string key, ref int feature)
        {
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetInt(key, feature);
            feature = PlayerPrefs.GetInt(key);
        }

        public static void LoadFeature(string key, ref string feature)
        {
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetString(key, feature);
            feature = PlayerPrefs.GetString(key);
        }

        public static void LoadFeature(string key, ref float feature)
        {
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetFloat(key, feature);
            feature = PlayerPrefs.GetFloat(key);
        }

        public static void SaveFeature(string key, ref string feature)
        {
            PlayerPrefs.SetString(key, feature);
        }

        public static void SaveFeature(string key, ref int feature)
        {
            PlayerPrefs.SetInt(key, feature);
        }

        public static void SaveFeature(string key, ref float feature)
        {
            PlayerPrefs.SetFloat(key, feature);
        }
    }
}
