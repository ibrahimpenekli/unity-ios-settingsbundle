using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Inscept.SettingsBundle
{
    /// <summary>
    /// This type defines a group element, which is a way to visually group preferences on a page. This element
    /// should be placed in front of the preferences associated with the group. You can assign a title to the group
    /// or omit to display a gap between preferences.
    /// </summary>
    [CreateAssetMenu(fileName = "Group", menuName = AssetMenuRoot + "Group")]
    public class GroupElement : PreferenceElement
    {
        public override string type => "PSGroupSpecifier";

        [Tooltip("The title of the group. If you do not specify title, a gap is inserted between preferences.")]
        [SerializeField]
        private LocalizableStringReference _title = new LocalizableStringReference();

        /// <summary>
        /// The title of the group. If you do not specify title, a gap is inserted between preferences.
        /// </summary>
        public LocalizableStringReference title
        {
            get => _title;
            set => _title = value;
        }

        [Tooltip("Additional text to display below the group box. Providing a footer is optional.")]
        [SerializeField]
        private LocalizableStringReference _footerText = new LocalizableStringReference();

        /// <summary>
        /// Additional text to display below the group box. Providing a footer is optional.
        /// On tvOS, the additional text is limited to 5 lines.
        /// </summary>
        /// <remarks>
        /// This key is available in iOS 4.0 and later.
        /// </remarks>
        public LocalizableStringReference footerText
        {
            get => _footerText;
            set => _footerText = value;
        }

        public override void GetLocalizableStrings(IList<LocalizableStringReference> localizedStrings)
        {
            base.GetLocalizableStrings(localizedStrings);

            localizedStrings.Add(title);
            localizedStrings.Add(footerText);
        }

        protected override void WriteXml(XElement element)
        {
            if (title.TryGetDefaultValue(out var titleString))
            {
                element.AddKeyValuePair("Title", titleString);
            }

            if (footerText.TryGetDefaultValue(out var footerString))
            {
                element.AddKeyValuePair("FooterText", footerString);
            }
        }
    }
}