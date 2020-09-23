using RGBtoRALv7_WPF.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RGBtoRALv7_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StackPanel InputPanel;
        public static StackPanel ResultPanel;

        static SettingsPanel settingpanel;

        public static bool isInSettings = false;

        public MainWindow()
        {
            InitializeComponent();
            InputColorPanel.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ResultColorPanel.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            InputPanel = InputColorPanel;
            ResultPanel = ResultColorPanel;

            ContentGrid.Children.Add(ContentPanelMaster.AddRGBPanel());
            ContentPanelMaster.currentCollection = "Classic";

            ContentPanelMaster.mainw = this;

            UpdateColorPanels();

        }

        public static void UpdateColorPanels()
        {
            InputPanel.Background = Util.setColor(ContentPanelMaster.inputColor.ToColor());
            ResultPanel.Background = Util.setColor(ContentPanelMaster.ResultColor.ToColor());
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseButtonImage_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.closeWhite);
        }

        private void CloseButtonImage_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.closeBlue);
        }

        private void MinimizeButtonImage_MouseEnter(object sender, MouseEventArgs e)
        {
            MinimizeButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.downWhite);
        }

        private void MinimizeButtonImage_MouseLeave(object sender, MouseEventArgs e)
        {
            MinimizeButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.downBlue);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if(this.Topmost)
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_fill);
            }
            else
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_2_fill);
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if(this.Topmost)
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_2_fill_blue);
            }
            else
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_fill_blue);
            }
        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.Topmost)
            {
                this.Topmost = false;
            }
            else
            {
                this.Topmost = true;
            }

            if (this.Topmost)
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_2_fill);
            }
            else
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_fill);
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void LockButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Topmost)
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_2_fill);
            }
            else
            {
                LockButtonImage.Source = Util.ImageFromBuffer(Properties.Resources.pushpin_fill);
            }
        }

        private void ComboBoxCollections_DropDownClosed(object sender, EventArgs e)
        {
            ContentPanelMaster.currentCollection = ((ComboBox)sender).Text;
        }

        private void Image_MouseEnter_1(object sender, MouseEventArgs e) // for palette button
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.palette_line);
        }

        private void Image_MouseLeave_1(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.palette_line_blue);
        }

        private void Image_MouseEnter_2(object sender, MouseEventArgs e) // for settings button
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.settings_3_line);
        }

        private void Image_MouseLeave_2(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.settings_3_line_blue);
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!isInSettings)
            {
                isInSettings = true;
                ContentGrid.Children.Remove(ContentPanelMaster.panel);
                settingpanel = new SettingsPanel();
                settingpanel.SetValue(Grid.RowProperty, 3);
                ContentGrid.Children.Add(settingpanel);
            }
            else
            {
                isInSettings = false;
                Settings.saveSettings(settingpanel.ComboBoxLanguage.Text,settingpanel.ComboBoxSipType.SelectedIndex,(bool)settingpanel.CheckBoxHideWindow.IsChecked);
                ContentGrid.Children.Remove(settingpanel);

                ContentPanelMaster.setLanguage();

                ContentGrid.Children.Add(ContentPanelMaster.panel);
            }
        }
    }
}
