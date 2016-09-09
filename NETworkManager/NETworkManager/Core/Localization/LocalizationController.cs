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
        // List of all available localizations
        public static List<LocalizationInfo> GetLocalizationList
        {
            get
            {
                return new List<LocalizationInfo> {
                    new LocalizationInfo("English", "/Resources/Localization/Resources.en-US.xaml", new Uri("/Resources/Localization/Flags/en-US.png", UriKind.Relative), "BornToBeRoot", "en-US"),
                    new LocalizationInfo("Deutsch", "/Resources/Localization/Resources.de-DE.xaml", new Uri("/Resources/Localization/Flags/de-DE.png", UriKind.Relative), "BornToBeRoot", "de-DE")
                };
            }
        }
        
        // Current localization
        private static LocalizationInfo _currentLocalization;
        public static LocalizationInfo CurrentLocalization
        {
            get { return _currentLocalization; }
            set { _currentLocalization = value; }
        }

        // Load localization from user settings, alternative use system language and if it's not available choose english
        private static ResourceDictionary lastLocalization;

        public static void LoadLocalization()
        {
            string cultureCode = Properties.Settings.Default.Localization_CultureCode;
                        
            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = CultureInfo.CurrentCulture.Name;

            LocalizationInfo localizationInfo = GetLocalizationList.Where(x => x.Code == cultureCode).FirstOrDefault();

            if (localizationInfo == null)
                localizationInfo = GetLocalizationList.First();

            if (lastLocalization != null)
                Application.Current.Resources.MergedDictionaries.Remove(lastLocalization);

            lastLocalization = new ResourceDictionary { Source = new Uri(localizationInfo.Path, UriKind.Relative) };

            CurrentLocalization = localizationInfo;

            Application.Current.Resources.MergedDictionaries.Add(lastLocalization);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentLocalization.Code);
        }
    }
}
