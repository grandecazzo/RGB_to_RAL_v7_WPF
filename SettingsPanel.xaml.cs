using Microsoft.VisualBasic.CompilerServices;
using RGBtoRALv7_WPF.Resources;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace RGBtoRALv7_WPF
{
    /// <summary>
    /// Логика взаимодействия для SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        public SettingsPanel()
        {
            InitializeComponent();

            this.setLang();

            switch (Properties.Settings.Default.Language)
            {
                case "en":
                    ComboBoxLanguage.SelectedIndex = 0;
                    break;
                case "ru":
                    ComboBoxLanguage.SelectedIndex = 1;
                    break;

                default:
                    ComboBoxLanguage.SelectedIndex = 0;
                    break;
            }

            switch (Properties.Settings.Default.SipType)
            {
                case 0:
                    ComboBoxSipType.SelectedIndex = 0;
                    break;

                case 1:
                    ComboBoxSipType.SelectedIndex = 1;
                    break;

                case 2:
                    ComboBoxSipType.SelectedIndex = 2;
                    break;

                default:
                    ComboBoxSipType.SelectedIndex = 0;
                    break;
            }

            if (Properties.Settings.Default.HideMainWindow)
            {
                CheckBoxHideWindow.IsChecked = true;
            }
            else
            {
                CheckBoxHideWindow.IsChecked = false;
            }
        }

        private void Reset_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Util.setColor(80, 180, 245);
        }

        private void Reset_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Util.setColor(255, 255, 255);
        }


        private void setLang()
        {
            XmlNode langNode = Settings.loadSettings();

            this.settings_langLabel.Content = langNode.SelectSingleNode("field_language").InnerText;

            this.settings_sipLabel.Content = langNode.SelectSingleNode("field_siptype").InnerText;

            this.ComboBoxSipType.Items.Clear();
            this.ComboBoxSipType.Items.Add(langNode.SelectSingleNode("field_siptype_0").InnerText);
            this.ComboBoxSipType.Items.Add(langNode.SelectSingleNode("field_siptype_2").InnerText);

            this.CheckBoxHideWindow.Content = langNode.SelectSingleNode("field_hide_window").InnerText;

            this.settings_develLabel.Text = langNode.SelectSingleNode("field_developed").InnerText;

            this.settings_setToDef.Content = langNode.SelectSingleNode("field_setdefault").InnerText;
            this.resetButton.Content = langNode.SelectSingleNode("field_reset_button").InnerText;

            ContentPanelMaster.setLanguage();
        }
    }
}
