using UnityEngine;

namespace Inscept.iOS.Settings
{
    /// <summary>
    /// This element displays a slider that you can use to specify a continuous range of values for the user.
    /// </summary>
    [CreateAssetMenu(fileName = "Slider", menuName = AssetMenuRoot + "Slider")]
    public class SliderElement : KeyValuePreferenceElement<float>
    {
        [Tooltip("The minimum value for the slider. Required.")]
        [SerializeField]
        private float _minimumValue;

        /// <summary>
        /// The minimum value for the slider. Required.
        /// </summary>
        public float minimumValue
        {
            get => _minimumValue;
            set => _minimumValue = value;
        }

        [Tooltip("The maximum value for the slider. Required.")]
        [SerializeField]
        private float _maximumValue;

        /// <summary>
        /// The maximum value for the slider. Required.
        /// </summary>
        public float maximumValue
        {
            get => _maximumValue;
            set => _maximumValue = value;
        }

        [Tooltip("The image to display on the side of the slider representing the minimum value." +
                 "This image should be 21 by 21 pixels.")]
        [SerializeField]
        private Texture2D _minimumValueImage;

        /// <summary>
        /// The image to display on the side of the slider representing the minimum value.
        /// This image should be 21 by 21 pixels.
        /// </summary>
        public Texture2D minimumValueImage
        {
            get => _minimumValueImage;
            set => _minimumValueImage = value;
        }
        
        [Tooltip("The image to display on the side of the slider representing the maximum value." +
                 "This image should be 21 by 21 pixels.")]
        [SerializeField]
        private Texture2D _maximumValueImage;

        /// <summary>
        /// The image to display on the side of the slider representing the maximum value.
        /// This image should be 21 by 21 pixels.
        /// </summary>
        public Texture2D maximumValueImage
        {
            get => _maximumValueImage;
            set => _maximumValueImage = value;
        }
    }
}