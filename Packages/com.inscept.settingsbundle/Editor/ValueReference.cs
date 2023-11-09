using System;

namespace Inscept.SettingsBundle
{
    [Serializable]
    public class ValueReference<T> : ValueReferenceBase<T, Variable<T>>
    {
        public T value => useConstant ? constantValue : (variable != null ? variable.value : default);

        public ValueReference()
        {
        }

        public ValueReference(T value) : base(value)
        {
        }
    }
}