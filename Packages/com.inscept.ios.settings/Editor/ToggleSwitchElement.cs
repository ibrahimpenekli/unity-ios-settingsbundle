using UnityEngine;
using UnityEngine.Localization;

namespace Inscept.iOS.Settings
{
    [CreateAssetMenu(fileName = "Toggle Switch", menuName = AssetMenuRoot + "Toggle Switch")]
    public class ToggleSwitchElement : KeyValuePreferenceElement<bool>
    {
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
    }
}