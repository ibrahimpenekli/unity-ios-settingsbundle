using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.SettingsBundle.Examples
{
    [CreateAssetMenu]
    public class TextVariable : LocalizableStringVariable
    {
        public string text;

        public override bool IsLocalizable()
        {
            return false;
        }

        public override bool TryGetValue(LocaleIdentifier localeIdentifier, out string value)
        {
            value = text;
            return true;
        }
    }
}