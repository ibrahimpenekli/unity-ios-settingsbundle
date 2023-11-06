using System;

namespace Inscept.iOS.Settings
{
    [Serializable]
    [Flags]
    public enum UserInterfaceIdioms
    {
        Default = 0,
        Phone = 1,
        Pad = 2
    }
}