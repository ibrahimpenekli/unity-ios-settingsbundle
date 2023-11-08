using System.Xml.Linq;
using UnityEngine;

namespace Inscept.iOS.Settings
{
    /// <summary>
    /// This element displays a slider that you can use to specify a continuous range of values for the user.
    /// </summary>
    /// <remarks>
    /// Sliders are not supported on tvOS.
    /// </remarks>
    [CreateAssetMenu(fileName = "Slider", menuName = AssetMenuRoot + "Slider")]
    public class SliderElement : KeyValuePreferenceElement<float>
    {
        public override string type => "PSSliderSpecifier";
        
        [Tooltip("The minimum value for the slider. Required.")]
        [SerializeField]
        private ValueReference<float> _minimumValue = new ValueReference<float>();

        /// <summary>
        /// The minimum value for the slider. Required.
        /// </summary>
        public ValueReference<float> minimumValue
        {
            get => _minimumValue;
            set => _minimumValue = value;
        }

        [Tooltip("The maximum value for the slider. Required.")]
        [SerializeField]
        private ValueReference<float> _maximumValue = new ValueReference<float>();

        /// <summary>
        /// The maximum value for the slider. Required.
        /// </summary>
        public ValueReference<float> maximumValue
        {
            get => _maximumValue;
            set => _maximumValue = value;
        }

        protected override void WriteXml(XElement element)
        {
            base.WriteXml(element);
            
            element.AddKeyValuePair("MinimumValue", minimumValue.value);
            element.AddKeyValuePair("MaximumValue", maximumValue.value);
        }
    }
}