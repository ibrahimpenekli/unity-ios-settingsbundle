using UnityEngine;

namespace Inscept.SettingsBundle.Examples
{
    [CreateAssetMenu]
    public class VersionVariable : Variable<string>
    {
        public override string value
        {
            get
            {
                var versionString = Application.version;
                
#if UNITY_IOS
                versionString += $" ({UnityEditor.PlayerSettings.iOS.buildNumber})";
#endif
                return versionString;
            }  
        } 
    }
}