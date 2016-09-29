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

        // Current localization
        private static LocalizationInfo _currentLocalization;
        public static LocalizationInfo CurrentLocalization
        {
            get { return _currentLocalization; }
            set { _currentLocalization = value; }
        }

        // Load localization from the settings
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

        // Change localization 
        public static void ChangeLocalization(LocalizationInfo info)
        {
            CurrentLocalization = info;

            if (_localizationResourceDictionary != null)
                Application.Current.Resources.MergedDictionaries.Remove(_localizationResourceDictionary);

            _localizationResourceDictionary = new ResourceDictionary { Source = new Uri(info.Path, UriKind.Relative) };

            Application.Current.Resources.MergedDictionaries.Add(_localizationResourceDictionary);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(info.Code);
        }


    }
}
