using System;
using System.Xml.Linq;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Inscept.SettingsBundle
{
    public static class XmlExtensions
    {
        internal static void AddKeyValuePair<T>(this XElement parent, string key, T value)
        {
            parent.Add(new XElement("key", key));
            
            var valueType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            
            if (valueType != typeof(bool))
            {
                var valueString = "string";
                var valueText = value.ToString();

                if (valueType == typeof(int))
                {
                    valueString = "integer";
                }
                else if (valueType == typeof(float))
                {
                    valueString = "real";
                }

                parent.Add(new XElement(valueString, valueText));
            }
            else
            {
                parent.Add(new XElement(Convert.ToBoolean(value) ? "true" : "false"));
            }
        }

        internal static void AddKeyValuePair(this XElement parent, string key, LocalizedString value,
            LocaleIdentifier localeIdentifier)
        {
            if (value.IsEmpty)
                return;

            var locale = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
            var strings = LocalizationSettings.StringDatabase;
            AddKeyValuePair(parent, key, strings.GetLocalizedString(value.TableEntryReference, locale));
        }
    }
}