using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.iOS.Settings
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

        public override IEnumerable<LocalizedString> GetLocalizedStrings()
        {
            foreach (var str in base.GetLocalizedStrings())
            {
                yield return str;
            }
            
            yield return title;
        }

        protected override void WriteXml(XElement element)
        {
            base.WriteXml(element);
            
            if (title.IsEmpty)
                throw new ArgumentException($"Title is required for '{name} ({type})'");
            
            WriteXmlElement(element, "Title", title, "en");
        }
    }
}