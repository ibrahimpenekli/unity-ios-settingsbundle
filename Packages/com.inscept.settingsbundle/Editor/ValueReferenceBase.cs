using System;
using UnityEngine;

namespace Inscept.SettingsBundle
{
    [Serializable]
    public class ValueReferenceBase<T, TVariable>
    {
        [SerializeField]
        private bool _useConstant = true;

        public bool useConstant
        {
            get => _useConstant;
            set => _useConstant = value;
        }

        [SerializeField]
        private T _constantValue;

        public T constantValue
        {
            get => _constantValue;
            set => _constantValue = value;
        }

        [SerializeField]
        private TVariable _variable;

        public TVariable variable
        {
            get => _variable;
            set => _variable = value;
        }
        
        public ValueReferenceBase()
        {
            useConstant = true;
        }

        public ValueReferenceBase(T value)
        {
            useConstant = true;
            constantValue = value;
        }
    }
}