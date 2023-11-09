﻿using UnityEngine;

namespace Inscept.SettingsBundle.Examples
{
    public class FloatPreferenceSetter : PreferenceSetter<float>
    {
        protected override float GetValue(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        protected override void SetValue(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }
}