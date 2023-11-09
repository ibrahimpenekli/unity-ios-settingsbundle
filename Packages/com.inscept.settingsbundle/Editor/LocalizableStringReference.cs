using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Inscept.SettingsBundle
{
    [Serializable]
    public class LocalizableStringReference : ValueReferenceBase<LocalizedString, LocalizableStringVariable>
    {
        public LocalizableStringReference()
        {
        }

        public LocalizableStringReference(LocalizedString value) : base(value)
        {
        }

        public bool IsLocalizable()
        {
            if (useConstant)
                return true;
            
            return variable != null && variable.IsLocalizable();
        }

        public bool TryGetValue(LocaleIdentifier localeIdentifier, out string value)
        {
            value = string.Empty;

            if (useConstant)
            {
                if (constantValue.IsEmpty)
                    return false;

                var locale = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
                var stringDatabase = LocalizationSettings.StringDatabase;

                value = stringDatabase.GetLocalizedString(
                    constantValue.TableEntryReference, locale, FallbackBehavior.DontUseFallback);

                return !string.IsNullOrEmpty(value);
            }

            return variable != null && variable.TryGetValue(localeIdentifier, out value);
        }
    }
}