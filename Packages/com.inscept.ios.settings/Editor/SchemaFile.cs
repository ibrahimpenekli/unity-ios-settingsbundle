using System;
using UnityEngine;

namespace Inscept.iOS.Settings
{
    [CreateAssetMenu(fileName = "Schema File", menuName = PreferenceElement.AssetMenuRoot + "Schema File")]
    public class SchemaFile : ScriptableObject
    {
        [SerializeField]
        private PreferenceElement[] _preferenceElements  = Array.Empty<PreferenceElement>();

        public PreferenceElement[] preferenceElements
        {
            get => _preferenceElements;
            set => _preferenceElements = value;
        }
    }
}