using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Inscept.SettingsBundle
{
    [CreateAssetMenu(
        fileName = "SettingsBundle", menuName = PreferenceElement.AssetMenuRoot + "Settings Bundle", order = -1)]
    public class SettingsBundle : ScriptableObject
    {
        public const string FileName = "Settings.bundle";
        
        [SerializeField]
        private PreferenceElement[] _preferenceElements  = Array.Empty<PreferenceElement>();

        public PreferenceElement[] preferenceElements
        {
            get => _preferenceElements;
            set => _preferenceElements = value;
        }

        public string Export(string outputDirectory)
        {
           var settingsBundlePath =  Path.Combine(outputDirectory, FileName);

           // Remove old Settings.bundle if exists.
           if (Directory.Exists(settingsBundlePath))
           {
               Directory.Delete(settingsBundlePath, true);
           }
           
           Directory.CreateDirectory(settingsBundlePath);

           WritePlistFiles(settingsBundlePath, "Root", preferenceElements);

           return settingsBundlePath;
        }

        private static void WritePlistFiles(string outputDirectory, string name,
            IEnumerable<PreferenceElement> preferenceElements)
        {
            var doc = new XDocument();
            doc.AddFirst(
                new XDocumentType(
                    "plist", "-//Apple/DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null));
           
            var xml = new XElement("plist", new XAttribute("version", "1.0"));
            doc.Add(xml);
           
            var dict = new XElement("dict");
            xml.Add(dict);

            var fileName = PlistHelper.ReplaceInvalidFileNameChars(name);
            dict.AddKeyValuePair("StringsTable", fileName);
            dict.Add(new XElement("key", "PreferenceSpecifiers"));

            var array = new XElement("array");
            dict.Add(array);

            var localizedStrings = new List<LocalizableStringReference>();

            foreach (var element in preferenceElements)
            {
                array.Add(element.CreateXml());

                element.GetLocalizableStrings(localizedStrings);

                if (element is ChildPaneElement childPaneElement)
                {
                    WritePlistFiles(outputDirectory, childPaneElement.name, childPaneElement.preferenceElements);
                }
            }
           
            var rootPlistPath = Path.Combine(outputDirectory, $"{fileName}.plist");
            using var plistFile = File.Create(rootPlistPath);
            doc.Save(plistFile);

            WriteStringsFiles(outputDirectory, fileName, localizedStrings);
        }

        private static void WriteStringsFiles(string outputDirectory, string name, 
            ICollection<LocalizableStringReference> localizableStrings)
        {
            var stringDatabase = LocalizationSettings.StringDatabase;
            if (stringDatabase == null)
            {
                Debug.LogWarning("String database could not be accessed.");
                return;
            }

            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                var localeDirectory = Path.Combine(outputDirectory, $"{locale.Identifier.Code}.lproj");
                
                if (!Directory.Exists(localeDirectory))
                {
                    Directory.CreateDirectory(localeDirectory);
                }
                
                var localePath = Path.Combine(localeDirectory, $"{name}.strings");
                using var writer = File.CreateText(localePath);
                    
                foreach (var localizableString in localizableStrings)
                {
                    if (!localizableString.IsLocalizable() ||
                        !localizableString.TryGetDefaultValue(out var defaultValue) ||
                        !localizableString.TryGetValue(locale.Identifier, out var value))
                        continue;

                    writer.WriteLine($"\"{defaultValue}\" = \"{value}\";");
                }
            }
        }
    }
}