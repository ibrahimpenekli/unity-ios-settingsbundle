using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Inscept.iOS.Settings
{
    public class SettingsBundlePostprocessBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder => 9999;
        
        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform == BuildTarget.iOS)
            {
                if (!SettingsBundleEditor.TryGetSettingsBundle(out var settingsBundle))
                    return;
                
                
            }
        }
    }
}