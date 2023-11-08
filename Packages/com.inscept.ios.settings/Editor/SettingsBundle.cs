using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Inscept.iOS.Settings
{
    [CreateAssetMenu(
        fileName = "SettingsBundle", menuName = PreferenceElement.AssetMenuRoot + "Settings Bundle", order = -1)]
    public class SettingsBundle : ScriptableObject
    {
        [Tooltip("Optional settings page title.")]
        [SerializeField]
        private LocalizedString _title;

        /// <summary>
        /// The string displayed as settings page title. If you omit the title, default title is displayed.
        /// </summary>
        public LocalizedString title
        {
            get => _title;
            set => _title = value;
        }
        
        [SerializeField]
        private PreferenceElement[] _preferenceElements  = Array.Empty<PreferenceElement>();

        public PreferenceElement[] preferenceElements
        {
            get => _preferenceElements;
            set => _preferenceElements = value;
        }

        public string Export(string outputDirectory)
        {
           var settingsBundlePath =  Path.Combine(outputDirectory, "Settings.bundle");

           // Remove old Settings.bundle if exists.
           if (Directory.Exists(settingsBundlePath))
           {
               Directory.Delete(settingsBundlePath, true);
           }
           
           Directory.CreateDirectory(settingsBundlePath);
           
           WritePlistFiles(settingsBundlePath, "Root", title, preferenceElements);

           return settingsBundlePath;
        }

        private static void WritePlistFiles(string outputDirectory, string name, LocalizedString title,
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

            if (!title.IsEmpty)
            {
                dict.AddKeyValuePair("Title", title, "en");
            }

            var fileName = PlistHelper.ReplaceInvalidFileNameChars(name);
            dict.AddKeyValuePair("StringsTable", fileName);
            dict.Add(new XElement("key", "PreferenceSpecifiers"));

            var array = new XElement("array");
            dict.Add(array);

            var localizedStrings = new List<LocalizedString>();

            if (!title.IsEmpty)
            {
                localizedStrings.Add(title);
            }
            
            foreach (var element in preferenceElements)
            {
                array.Add(element.CreateXml());

                localizedStrings.AddRange(element.GetLocalizedStrings());
                
                if (element is ChildPaneElement childPaneElement)
                {
                    WritePlistFiles(outputDirectory, childPaneElement.name, new LocalizedString(), 
                        childPaneElement.preferenceElements);
                }
            }
           
            var rootPlistPath = Path.Combine(outputDirectory, $"{fileName}.plist");
            using var plistFile = File.Create(rootPlistPath);
            doc.Save(plistFile);

            WriteStringsFiles(outputDirectory, fileName, localizedStrings);
        }

        private static void WriteStringsFiles(string outputDirectory, string name, 
            ICollection<LocalizedString> localizedStrings)
        {
            var stringDatabase = LocalizationSettings.StringDatabase;
            if (stringDatabase == null)
            {
                Debug.LogWarning("String database could not be accessed.");
                return;
            }

            var defaultLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
            
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                var localeDirectory = Path.Combine(outputDirectory, $"{locale.Identifier.Code}.lproj");
                
                if (!Directory.Exists(localeDirectory))
                {
                    Directory.CreateDirectory(localeDirectory);
                }
                
                var localePath = Path.Combine(localeDirectory, $"{name}.strings");
                using var writer = File.CreateText(localePath);
                    
                foreach (var localizedString in localizedStrings)
                {
                    Debug.Assert(!localizedString.IsEmpty);
                    
                    var defaultValue = stringDatabase.GetLocalizedString(
                        localizedString.TableEntryReference, defaultLocale, FallbackBehavior.DontUseFallback);
                    
                    var value = stringDatabase.GetLocalizedString(
                        localizedString.TableEntryReference, locale, FallbackBehavior.UseFallback);
                    
                    writer.WriteLine($"\"{defaultValue}\" = \"{value}\";");
                }
            }
        }
    }
}