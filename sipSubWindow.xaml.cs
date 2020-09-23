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
using System.Windows.Shapes;
using WpfScreenHelper;

namespace RGBtoRALv7_WPF
{
    /// <summary>
    /// Логика взаимодействия для sipSubWindow.xaml
    /// </summary>
    public partial class sipSubWindow : Window
    {
        Sip_window papawindow;
        public sipSubWindow(Screen screen, Sip_window papa)
        {
            
            InitializeComponent();

            this.sipScreenImage.Source = Util.getWindowScreen(screen);
            this.Topmost = true;
            papawindow = papa;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.sipScreenImage.Opacity = 1;

            this.Focus();
            this.Activate();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.sipScreenImage.Opacity = 0.5;
        }

        private void sipScreenImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);

            papawindow.setColor(Util.getColorOfPixel(this.sipScreenImage.Source as BitmapImage, (int)p.X, (int)p.Y));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                papawindow.saveColor();
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    papawindow.Close();
                }
            }
        }
    }
}
