using RGBtoRALv7_WPF.Resources;
using System;
using System.Collections.Generic;
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

namespace RGBtoRALv7_WPF
{
    /// <summary>
    /// Логика взаимодействия для RGBPanel.xaml
    /// </summary>
    public partial class RGBPanel : UserControl
    {
        public RGBPanel()
        {
            InitializeComponent();

            RedSlider.Minimum = 0;
            GreenSlider.Minimum = 0;
            BlueSlider.Minimum = 0;

            RedSlider.Maximum = 255;
            GreenSlider.Maximum = 255;
            BlueSlider.Maximum = 255;

        }


        /*private static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 0 && i <= 255;
        }*/

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            int i=-1;
            string text = ((TextBox)sender).Text + e.Text;
            bool isValid = int.TryParse(text, out i) && i >= 0 && i <= 255;

            if (!isValid)
            {
                if(i>125)
                    ((TextBox)sender).Text = "255";
                if (i < 125 && i >= 0)
                    ((TextBox)sender).Text = "0";
            }

            if(i==255 && e.Text=="0")
                ((TextBox)sender).Text = "0";

            if (((TextBox)sender).SelectedText.Length > 0)
                if (i >= 0 && !isValid)
                    ((TextBox)sender).Text = e.Text;
                else
                ((TextBox)sender).Text = "";

            e.Handled = !isValid;
        }

        private void RedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(((TextBox)sender).Text!="" && ((TextBox)sender).Text != " ")
            {
                if(RedSlider!=null)
                    RedSlider.Value = Int32.Parse(((TextBox)sender).Text);
            }

            if(((TextBox)sender).Text.Contains(" "))
            {
                ((TextBox)sender).Text = "0";
            }
        }

        private void GreenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "" && ((TextBox)sender).Text != " ")
            {
                if (GreenSlider != null)
                    GreenSlider.Value = Int32.Parse(((TextBox)sender).Text);
            }

            if (((TextBox)sender).Text.Contains(" "))
            {
                ((TextBox)sender).Text = "0";
            }
        }

        private void BlueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "" && ((TextBox)sender).Text != " ")
            {
                if (BlueSlider != null)
                    BlueSlider.Value = Int32.Parse(((TextBox)sender).Text);
            }

            if (((TextBox)sender).Text.Contains(" "))
            {
                ((TextBox)sender).Text = "0";
            }
        }


        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedTextBox.Text = ((int)RedSlider.Value).ToString();
            ContentPanelMaster.UpdateColor();
            MainWindow.InputPanel.Background = Util.setColor(ContentPanelMaster.inputColor.ToColor());
        }

        private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GreenTextBox.Text = ((int)GreenSlider.Value).ToString();
            ContentPanelMaster.UpdateColor();
            MainWindow.InputPanel.Background = Util.setColor(ContentPanelMaster.inputColor.ToColor());
        }

        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlueTextBox.Text = ((int)BlueSlider.Value).ToString();
            ContentPanelMaster.UpdateColor();
            MainWindow.InputPanel.Background = Util.setColor(ContentPanelMaster.inputColor.ToColor());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentPanelMaster.FindColor();
            MainWindow.UpdateColorPanels();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            FindLabel.Foreground = Util.setColor(255,255,255);
            FindImage.Source = Util.ImageFromBuffer(Properties.Resources.search_line);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            FindLabel.Foreground = Util.setColor(80, 180, 245);
            FindImage.Source = Util.ImageFromBuffer(Properties.Resources.search_line_blue);
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.sip_line);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.sip_line_blue);
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindLabel.Foreground = Util.setColor(80, 180, 245);
            FindImage.Source = Util.ImageFromBuffer(Properties.Resources.search_line_blue);
        }

        private void Sip_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.HideMainWindow)
            {
                ContentPanelMaster.mainw.WindowState = WindowState.Minimized;
            }

            Sip_window sip = new Sip_window(ContentPanelMaster.mainw);

            sip.Show();
        }
    }
}
