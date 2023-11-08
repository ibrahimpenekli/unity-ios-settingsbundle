using System.IO;
using System.Linq;

namespace Inscept.iOS.Settings
{
    internal static class PlistHelper
    {
        public static string ReplaceInvalidFileNameChars(string fileName, string separator = "")
        {
            var chars = Path.GetInvalidFileNameChars().Concat(new []{' ', '-', '_', '.'}).ToArray();
            
            if (!string.IsNullOrEmpty(fileName))
            {
                return string.Join(separator, fileName.Split(chars));
            }

            return string.Empty;
        }
    }
}