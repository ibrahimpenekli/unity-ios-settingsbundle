using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Inscept.SettingsBundle
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

        public virtual void GetLocalizableStrings(IList<LocalizableStringReference> localizedStrings)
        {
            // Intentionally empty.
        }

        public XElement CreateXml()
        {
            var xml = new XElement("dict");

            xml.AddKeyValuePair("Type", type);
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
    }
}