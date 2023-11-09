using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.SettingsBundle
{
    [CreateAssetMenu(fileName = "Title", menuName = AssetMenuRoot + "Title")]
    public class TitleElement : KeyValuePreferenceElement<string>
    {
        public override string type => "PSTitleValueSpecifier";

        [Tooltip("The string displayed to the left of the value.")]
        [SerializeField]
        private LocalizableStringReference _title = new LocalizableStringReference();

        /// <summary>
        /// The string displayed to the left of the value..
        /// </summary>
        public LocalizableStringReference title
        {
            get => _title;
            set => _title = value;
        }

        public override void GetLocalizableStrings(IList<LocalizableStringReference> localizedStrings)
        {
            base.GetLocalizableStrings(localizedStrings);

            localizedStrings.Add(title);
        }

        protected override void WriteXml(XElement element)
        {
            base.WriteXml(element);

            if (!title.TryGetDefaultValue(out var titleString))
                throw new ArgumentException($"Title is required for '{name} ({type})'");

            element.AddKeyValuePair("Title", titleString);
        }
    }
}