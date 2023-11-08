using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.iOS.Settings
{
    [CreateAssetMenu(fileName = "Child Pane", menuName = AssetMenuRoot + "Child Pane")]
    public class ChildPaneElement : PreferenceElement
    {
        public override string type => "PSChildPaneSpecifier";
        
        [Tooltip("The title string displayed in the preference row.")]
        [SerializeField]
        private LocalizedString _title;

        /// <summary>
        /// The title string displayed in the preference row. This is the string the user taps to display the next page.
        /// This string is also used as the title of the screen that is subsequently displayed. This is required. 
        /// </summary>
        public LocalizedString title
        {
            get => _title;
            set => _title = value;
        }

        [SerializeField]
        private PreferenceElement[] _preferenceElements  = Array.Empty<PreferenceElement>();

        public PreferenceElement[] preferenceElements
        {
            get => _preferenceElements;
            set => _preferenceElements = value;
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
            if (title.IsEmpty)
                throw new ArgumentException($"Title is required for '{name} ({type})'");
            
            var fileName = PlistHelper.ReplaceInvalidFileNameChars(name);
            element.AddKeyValuePair("Title", title, "en");
            element.AddKeyValuePair("File", fileName);
        }
    }
}