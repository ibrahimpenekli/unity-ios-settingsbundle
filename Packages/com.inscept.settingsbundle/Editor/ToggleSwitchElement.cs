using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.SettingsBundle
{
    [CreateAssetMenu(fileName = "Toggle Switch", menuName = AssetMenuRoot + "Toggle Switch")]
    public class ToggleSwitchElement : KeyValuePreferenceElement<bool>
    {
        public override string type => "PSToggleSwitchSpecifier";
        
        [Tooltip("The string displayed to the left of the switch. This is required.")]
        [SerializeField]
        private LocalizedString _title;

        /// <summary>
        /// The string displayed to the left of the switch. This is required.
        /// </summary>
        public LocalizedString title
        {
            get => _title;
            set => _title = value;
        }

        public override void GetLocalizedStrings(IList<LocalizedString> localizedStrings)
        {
            base.GetLocalizedStrings(localizedStrings);
            
            if (!title.IsEmpty)
            {
                localizedStrings.Add(title);
            }
        }

        protected override void WriteXml(XElement element)
        {
            base.WriteXml(element);
            
            if (title.IsEmpty)
                throw new ArgumentException($"Title is required for '{name} ({type})'");
            
            element.AddKeyValuePair("Title", title, "en");
        }
    }
}