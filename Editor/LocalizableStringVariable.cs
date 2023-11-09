using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.SettingsBundle
{
    public abstract class LocalizableStringVariable : ScriptableObject
    {
        public abstract bool IsLocalizable();
        public abstract bool TryGetValue(LocaleIdentifier localeIdentifier, out string value);
    }
}