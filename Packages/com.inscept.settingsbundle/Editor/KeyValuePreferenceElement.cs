using System;
using System.Xml.Linq;
using UnityEngine;

namespace Inscept.SettingsBundle
{
    public abstract class KeyValuePreferenceElement<T> : PreferenceElement
    {
        [Tooltip("The preference key with which to associate the value. This key is required.")]
        [SerializeField]
        private string _identifier;

        /// <summary>
        /// The preference key with which to associate the value. This is the string you use this to retrieve the
        /// preference value in your code. This key is required.
        /// </summary>
        public string identifier
        {
            get => _identifier;
            set => _identifier = value;
        }
        
        [Tooltip("The default value for the preference key.")]
        [SerializeField]
        private ValueReference<T> _defaultValue = new ValueReference<T>();

        /// <summary>
        /// The default value for the preference key. This value is returned when the specified preferences
        /// <see cref="identifier"/> is not present in the preferences database.
        /// </summary>
        public ValueReference<T> defaultValue
        {
            get => _defaultValue;
            set => _defaultValue = value;
        }

        protected override void WriteXml(XElement element)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException($"Identifier is required for '{name} ({type})'");
                    
            element.AddKeyValuePair("Key", identifier);
            element.AddKeyValuePair("DefaultValue", defaultValue.value);
        }
    }
}