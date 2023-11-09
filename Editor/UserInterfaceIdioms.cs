using System;

namespace Inscept.SettingsBundle
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