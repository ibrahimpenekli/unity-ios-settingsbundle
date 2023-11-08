using UnityEngine;

namespace Inscept.iOS.Settings.Examples
{
    public class StringPreferenceSetter : PreferenceSetter<string>
    {
        protected override string GetValue(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        protected override void SetValue(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}