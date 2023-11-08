using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Inscept.iOS.Settings
{
    public abstract class PreferenceElement : ScriptableObject
    {
        public const string AssetMenuRoot = "iOS/Settings Bundle/";

        /// <summary>
        /// 
        /// </summary>
        public abstract string type { get; }

        [Tooltip("Include 'Phone' to display the element on iPhone and iPod touch." +
                 "Include 'Pad' to display it on iPad")]
        [SerializeField]
        private UserInterfaceIdioms _supportedUserInterfaceIdioms = UserInterfaceIdioms.Default;

        /// <summary>
        /// Indicates that the element is displayed only on specific types of devices. The value of this key is an
        /// array of strings with the supported idioms. Include 'Phone' to display the element on iPhone and iPod touch.
        /// Include 'Pad' to display it on iPad.
        /// </summary>
        /// <remarks>
        /// This is available in iOS 4.2 and later.
        /// </remarks>
        public UserInterfaceIdioms supportedUserInterfaceIdioms
        {
            get => _supportedUserInterfaceIdioms;
            set => _supportedUserInterfaceIdioms = value;
        }

        public virtual IEnumerable<LocalizedString> GetLocalizedStrings()
        {
            return Enumerable.Empty<LocalizedString>();
        }

        public XElement CreateXml()
        {
            var xml = new XElement("dict");

            WriteXmlElement(xml, "Type", type);
            WriteXml(xml);

            var idioms = supportedUserInterfaceIdioms.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (idioms.Any() && supportedUserInterfaceIdioms != UserInterfaceIdioms.Default)
            {
                xml.Add(new XElement("key", "SupportedUserInterfaceIdioms"));
                
                var array = new XElement("array");

                foreach (var idiom in idioms)
                {
                    array.Add(new XElement("string", idiom));
                }

                xml.Add(array);
            }

            return xml;
        }

        protected abstract void WriteXml(XElement element);

        protected static void WriteXmlElement<T>(XElement parent, string key, T value)
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

        protected static void WriteXmlElement(XElement parent, string key, LocalizedString value,
            LocaleIdentifier localeIdentifier)
        {
            if (value.IsEmpty)
                return;

            var locale = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
            var strings = LocalizationSettings.StringDatabase;
            WriteXmlElement(parent, key, strings.GetLocalizedString(value.TableEntryReference, locale));
        }
    }
}