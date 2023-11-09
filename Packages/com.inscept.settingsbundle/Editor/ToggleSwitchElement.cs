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
        private LocalizableStringReference _title = new LocalizableStringReference();

        /// <summary>
        /// The string displayed to the left of the switch. This is required.
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

            if (!title.TryGetValue("en", out var titleString))
                throw new ArgumentException($"Title is required for '{name} ({type})'");

            element.AddKeyValuePair("Title", titleString);
        }
    }
}