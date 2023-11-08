using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
#endif

namespace Inscept.SettingsBundle
{
    public class SettingsBundlePostprocessBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder => 9999;
        
        public void OnPostprocessBuild(BuildReport report)
        {
#if UNITY_IOS
            if (report.summary.platform == BuildTarget.iOS)
            {
                if (!SettingsBundleEditor.TryGetSettingsBundle(out var settingsBundle))
                    return;
                
                var outputPath = report.summary.outputPath;
                var settingsBundlePath = settingsBundle.Export(outputPath);
                
                var projectPath = PBXProject.GetPBXProjectPath(outputPath);
                var project = new PBXProject();
                project.ReadFromFile(projectPath);
                
                var targetGuid = project.GetUnityMainTargetGuid();
                var settingsBundleGuid = 
                    project.AddFolderReference(settingsBundlePath, Path.GetFileName(settingsBundlePath));
                
                project.AddFileToBuild(targetGuid, settingsBundleGuid);
                File.WriteAllText(projectPath, project.WriteToString());
            }
#endif
        }
    }
}