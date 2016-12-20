using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace NETworkManager.Core.Localization
{
    public static class LocalizationController
    {
        /// <summary>
        /// List of all available localizations
        /// Localizations are stored as .xaml-file in the resources
        /// </summary>
        public static List<LocalizationInfo> LocalizationList
        {
            get
            {
                return new List<LocalizationInfo> {
                    new LocalizationInfo("English", "/Resources/Localization/Resources.en-US.xaml", new Uri("/Resources/Localization/Flags/en-US.png", UriKind.Relative), "BornToBeRoot", "en-US"),
                    new LocalizationInfo("Deutsch", "/Resources/Localization/Resources.de-DE.xaml", new Uri("/Resources/Localization/Flags/de-DE.png", UriKind.Relative), "BornToBeRoot", "de-DE")
                };
            }
        }

        /// <summary>
        /// Get or set the current localization
        /// </summary>
        private static LocalizationInfo _currentLocalization;
        public static LocalizationInfo CurrentLocalization
        {
            get { return _currentLocalization; }
            set { _currentLocalization = value; }
        }

        /// <summary>
        /// Load the localization from the user settings
        /// </summary>
        public static void LoadLocalization()
        {
            string cultureCode = Properties.Settings.Default.Localization_CultureCode;

            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = CultureInfo.CurrentCulture.Name;

            LocalizationInfo info = LocalizationList.Where(x => x.Code == cultureCode).FirstOrDefault();

            if (info == null)
                info = LocalizationList.First();

            if (info.Code != Properties.Resources.Localization_DefaultCultureCode)
                ChangeLocalization(info);
            else
                CurrentLocalization = info;
        }
        

        private static ResourceDictionary _localizationResourceDictionary;

        /// <summary>
        /// Change the localization
        /// </summary>
        /// <param name="info">LocalizationInfo</param>
        public static void ChangeLocalization(LocalizationInfo info)
        {
            // Set the current localization
            CurrentLocalization = info;

            // Remove dictionaries, which are no longer required
            if (_localizationResourceDictionary != null)
                Application.Current.Resources.MergedDictionaries.Remove(_localizationResourceDictionary);

            // Create/Initialize a new dictionary from the .xaml-file in the resource
            _localizationResourceDictionary = new ResourceDictionary { Source = new Uri(info.Path, UriKind.Relative) };

            // Add the new dictionary
            Application.Current.Resources.MergedDictionaries.Add(_localizationResourceDictionary);

            // Set the culture code
            Thread.CurrentThread.CurrentCulture = new CultureInfo(info.Code);
        }
    }
}
