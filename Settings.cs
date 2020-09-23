using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Xml;

namespace RGBtoRALv7_WPF
{
    public class Settings
    {

        public static void setDefault()
        {
            Properties.Settings.Default.Language = "en";
            Properties.Settings.Default.SipType = 0;
            Properties.Settings.Default.DefaultCollection = "Classic";
            Properties.Settings.Default.HideMainWindow = true;

            Properties.Settings.Default.Save();
        }

        public static void saveSettings(string lang, int siptype, bool hidemain) /* Метод сохранения выбранных настроек из элементов в Properties.Settings */
        {
            Properties.Settings.Default.Language = getLang(lang);
            Properties.Settings.Default.SipType = siptype;
            //Properties.Settings.Default.DefaultCollection = "Classic";
            Properties.Settings.Default.HideMainWindow = hidemain;

            Properties.Settings.Default.Save();
        }

        static string getLang(string lang)
        {

            switch(lang)
            {
                case "English":
                    return "en";
                case "Russian":
                    return "ru";
                default:
                    return "en";
            }
        }

        public static XmlNode loadSettings()
        {
            XmlDocument settingsDoc = new XmlDocument();
            settingsDoc.LoadXml(Properties.Resources.Langs);

            XmlNode langNode;

            switch (Properties.Settings.Default.Language)
            {
                case "en":
                    langNode = settingsDoc.SelectSingleNode("//*[@id='en']");
                    break;
                case "ru":
                    langNode = settingsDoc.SelectSingleNode("//*[@id='ru']");
                    break;

                /* --more langs-- */

                default:
                    langNode = settingsDoc.SelectSingleNode("//*[@id='en']");
                    break;
            }

            return langNode;
        }
    }
}
