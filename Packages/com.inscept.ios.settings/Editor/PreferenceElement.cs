using UnityEngine;

namespace Inscept.iOS.Settings
{
    public class PreferenceElement : ScriptableObject
    {
        public const string AssetMenuRoot = "iOS/Settings Bundle/";
        
        [Tooltip("Include 'Phone' to display the element on iPhone and iPod touch." +
                 "Include 'Pad' to display it on iPad")]
        [SerializeField]
        private UserInterfaceIdioms _supportedUserInterfaceIdioms = UserInterfaceIdioms.Default;

        /// <summary>
        /// Indicates that the element is displayed only on specific types of devices. The value of this key is an
        /// array of strings with the supported idioms. Include 'Phone' to display the element on iPhone and iPod touch.
        /// Include 'Pad' to display it on iPad.
        /// </summary>
        /// <remarks>
        /// This is available in iOS 4.2 and later.
        /// </remarks>
        public UserInterfaceIdioms supportedUserInterfaceIdioms
        {
            get => _supportedUserInterfaceIdioms;
            set => _supportedUserInterfaceIdioms = value;
        }
    }
}