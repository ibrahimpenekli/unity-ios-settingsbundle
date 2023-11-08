using UnityEngine;

namespace Inscept.iOS.Settings
{
    public abstract class Variable<T> : ScriptableObject
    {
        public abstract T value { get; }
    }
}