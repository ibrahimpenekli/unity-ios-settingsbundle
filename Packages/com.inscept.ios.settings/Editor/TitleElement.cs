using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.iOS.Settings
{
    [CreateAssetMenu(fileName = "Title", menuName = AssetMenuRoot + "Title")]
    public class TitleElement : KeyValuePreferenceElement<string>
    {
        public override string type => "PSTitleValueSpecifier";
        
        [Tooltip("The string displayed to the left of the value.")]
        [SerializeField]
        private LocalizedString _title;

        /// <summary>
        /// The string displayed to the left of the value..
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