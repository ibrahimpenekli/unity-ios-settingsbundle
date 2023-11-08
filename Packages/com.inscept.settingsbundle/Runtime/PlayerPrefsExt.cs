using UnityEngine;

namespace Inscept.SettingsBundle
{
    /// <summary>
    /// You can access/modify iOS settings by using the <see cref="PlayerPrefs"/> class. This class contains some
    /// extension methods that <see cref="PlayerPrefs"/> class doesn't have.
    /// </summary>
    public static class PlayerPrefsExt
    {
        public static bool GetBool(string key, bool defaultValue = default)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }
}