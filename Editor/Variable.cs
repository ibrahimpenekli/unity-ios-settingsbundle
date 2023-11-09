using UnityEngine;

namespace Inscept.SettingsBundle
{
    public abstract class Variable<T> : ScriptableObject
    {
        public abstract T value { get; }
    }
}