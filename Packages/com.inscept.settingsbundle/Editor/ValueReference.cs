using System;
using UnityEngine;

namespace Inscept.SettingsBundle
{
    [Serializable]
    public class ValueReference<T>
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
        private Variable<T> _variable;

        public Variable<T> variable
        {
            get => _variable;
            set => _variable = value;
        }
        
        public T value => useConstant ? constantValue : (variable != null ? variable.value : default);

        public ValueReference()
        {
            useConstant = true;
        }

        public ValueReference(T value)
        {
            useConstant = true;
            constantValue = value;
        }
    }
}