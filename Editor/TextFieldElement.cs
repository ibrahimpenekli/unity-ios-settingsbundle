
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.SettingsBundle
{
    [CreateAssetMenu(fileName = "Text Field", menuName = AssetMenuRoot + "Text Field")]
    public class TextFieldElement : KeyValuePreferenceElement<string>
    {
        public override string type => "PSTextFieldSpecifier";

        [Tooltip("The string displayed to the left of the text field’s value.")]
        [SerializeField]
        private LocalizableStringReference _title = new LocalizableStringReference();

        /// <summary>
        /// The string displayed to the left of the text field’s value. This string is drawn left aligned and in
        /// bold face. If you omit the title, the editable text field spans the width of the row.
        /// </summary>
        public LocalizableStringReference title
        {
            get => _title;
            set => _title = value;
        }

        [Tooltip("Is the text field password-entry text field or not?")]
        [SerializeField]
        private bool _isSecure;
        
        /// <summary>
        /// If <c>true</c>, the text field is a password-entry text field, which replaces the typed text with bullet
        /// characters. If <c>false</c>, the text field is a standard text field that displays the typed text.
        /// </summary>
        public bool isSecure
        {
            get => _isSecure;
            set => _isSecure = value;
        }

        [Tooltip("The type of keyboard to display to the user. The default value is Alphabet.")]
        [SerializeField]
        private KeyboardType _keyboardType = KeyboardType.Alphabet;
        
        /// <summary>
        /// The type of keyboard to display to the user. The default value is <see cref="KeyboardType.Alphabet"/>.
        /// </summary>
        public KeyboardType keyboardType
        {
            get => _keyboardType;
            set => _keyboardType = value;
        }
        
        [Tooltip("The auto-capitalization style to apply to typed text. The default value is None.")]
        [SerializeField]
        private AutoCapitalizationType _autoCapitalizationType = AutoCapitalizationType.None;
        
        /// <summary>
        /// The auto-capitalization style to apply to typed text. The default value is
        /// <see cref="AutoCapitalizationType.None"/>.
        /// </summary>
        public AutoCapitalizationType autoCapitalizationType
        {
            get => _autoCapitalizationType;
            set => _autoCapitalizationType = value;
        }
        
        [Tooltip("The auto-correction style to apply when typing. The default value is Default.")]
        [SerializeField]
        private AutoCorrectionType _autoCorrectionType = AutoCorrectionType.Default;
        
        /// <summary>
        /// The auto-correction style to apply when typing. The default value is
        /// <see cref="AutoCorrectionType.Default"/>.
        /// </summary>
        public AutoCorrectionType autoCorrectionType
        {
            get => _autoCorrectionType;
            set => _autoCorrectionType = value;
        }

        public override void GetLocalizableStrings(IList<LocalizableStringReference> localizedStrings)
        {
            base.GetLocalizableStrings(localizedStrings);
            
            localizedStrings.Add(title);
        }

        protected override void WriteXml(XElement element)
        {
            base.WriteXml(element);
            
            if (title.TryGetDefaultValue(out var titleString))
            {
                element.AddKeyValuePair("Title", titleString);    
            }
            
            element.AddKeyValuePair("IsSecure", isSecure);
            element.AddKeyValuePair("AutocorrectionType", autoCorrectionType);
            element.AddKeyValuePair("AutocapitalizationType", autoCapitalizationType);
            element.AddKeyValuePair("KeyboardType", keyboardType);
        }
    }
}