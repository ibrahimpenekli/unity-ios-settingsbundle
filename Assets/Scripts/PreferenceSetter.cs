using System;
using UnityEngine;
using UnityEngine.Events;

namespace Inscept.iOS.Settings.Examples
{
    public abstract class PreferenceSetter<T> : MonoBehaviour
    {
        public string identifier;
        public UnityEvent<T> setter;

        private void OnEnable()
        {
            setter.Invoke(GetValue(identifier));
        }

        public void SetValue(T value)
        {
            SetValue(identifier, value);
            PlayerPrefs.Save();
        }

        protected abstract T GetValue(string key);
        protected abstract void SetValue(string key, T value);
    }
}