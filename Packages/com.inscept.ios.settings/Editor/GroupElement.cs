using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.iOS.Settings
{
    /// <summary>
    /// This type defines a group element, which is a way to visually group preferences on a page. This element
    /// should be placed in front of the preferences associated with the group. You can assign a title to the group
    /// or omit to display a gap between preferences.
    /// </summary>
    [CreateAssetMenu(fileName = "Group", menuName = AssetMenuRoot + "Group")]
    public class GroupElement : PreferenceElement
    {
        [Tooltip("The title of the group. If you do not specify title, a gap is inserted between preferences.")]
        [SerializeField]
        private LocalizedString _title;

        /// <summary>
        /// The title of the group. If you do not specify title, a gap is inserted between preferences.
        /// </summary>
        public LocalizedString title
        {
            get => _title;
            set => _title = value;
        }
        
        [Tooltip("Additional text to display below the group box. Providing a footer is optional.")]
        [SerializeField]
        private LocalizedString _footerText;

        /// <summary>
        /// Additional text to display below the group box. Providing a footer is optional.
        /// On tvOS, the additional text is limited to 5 lines.
        /// </summary>
        /// <remarks>
        /// This key is available in iOS 4.0 and later.
        /// </remarks>
        public LocalizedString footerText
        {
            get => _footerText;
            set => _footerText = value;
        }
    }
}