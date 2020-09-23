using RGBtoRALv7_WPF.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfScreenHelper;

namespace RGBtoRALv7_WPF
{
    /// <summary>
    /// Логика взаимодействия для Sip_window.xaml
    /// </summary>
    public partial class Sip_window : Window
    {
        sipControl control;
        List<sipSubWindow> subwindows;
        Color findcolor;

        public Sip_window(MainWindow mainwindow) /* main sip */
        {
            findcolor = ContentPanelMaster.inputColor.ToColor();

            subwindows = new List<sipSubWindow>();
            var screens = Screen.AllScreens;
            
            System.Drawing.Rectangle mainwindowRect = new System.Drawing.Rectangle((int)Application.Current.MainWindow.Left, (int)Application.Current.MainWindow.Top, (int)mainwindow.Width, (int)mainwindow.Height);
            
            Screen activeScreen = null;
            
            foreach (var screen in screens)
            {
                System.Drawing.Rectangle screenrect = new System.Drawing.Rectangle((int)screen.WorkingArea.Left, (int)screen.WorkingArea.Top, (int)screen.WorkingArea.Width, (int)screen.WorkingArea.Height);
                
                if(mainwindowRect.IntersectsWith(screenrect))
                {
                    Console.WriteLine("OVERLAP!\n {0} {1} {2} {3} \n{4} {5} {6} {7}", Application.Current.MainWindow.Left, Application.Current.MainWindow.Top,
                    Application.Current.MainWindow.Width, Application.Current.MainWindow.Height, screenrect.Left, screenrect.Top,
                    screenrect.Width+screenrect.Left, screenrect.Height+screenrect.Top);

                    activeScreen = screen;

                    this.Left = activeScreen.Bounds.Left;
                    this.Top = activeScreen.Bounds.Top;
                    this.Width = activeScreen.Bounds.Width;
                    this.Height = activeScreen.Bounds.Height;

                    Console.WriteLine("{0} {1} {2} {3}",this.Left,this.Top,this.Width,this.Height);

                    break;
                }
            }

            InitializeComponent();

            this.sipScreenImage.Source = Util.getWindowScreen(activeScreen);

            if (Properties.Settings.Default.SipType == 0)
            {
                this.createSubwindows(screens, activeScreen);
            }

            sipControl _control = new sipControl(this, ContentPanelMaster.inputColor.ToColor())
            {
                RenderTransform = new TranslateTransform(Application.Current.MainWindow.Left - activeScreen.Bounds.Left, Application.Current.MainWindow.Top - activeScreen.Bounds.Top)
            };

            control = _control;
            this.Canvas.Children.Add(control);
            this.Topmost = true;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point mouse = e.GetPosition(this);

            findcolor = Util.getColorOfPixel(this.sipScreenImage.Source as BitmapImage, (int)mouse.X, (int)mouse.Y);

            control.colorPanel.Background = Util.setColor(findcolor);
        }

        private void sipScreenImage_MouseEnter(object sender, MouseEventArgs e)
        {
            this.sipScreenImage.Opacity = 1;

            this.Focus();
            this.Activate();
        }

        private void sipScreenImage_MouseLeave(object sender, MouseEventArgs e)
        {
            this.sipScreenImage.Opacity = 0.5;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            foreach(var subwindow in subwindows)
            {
                subwindow.Close();
            }

            if(ContentPanelMaster.mainw.WindowState == WindowState.Minimized)
            {
                ContentPanelMaster.mainw.WindowState = WindowState.Normal;
            }

            base.OnClosing(e);
        }

        public void setColor(Color _color)
        {
            findcolor = _color;

            this.control.colorPanel.Background = Util.setColor(findcolor);
        }

        public void saveColor()
        {
            ContentPanelMaster.panel.RedTextBox.Text = Convert.ToInt32(findcolor.R).ToString();
            ContentPanelMaster.panel.GreenTextBox.Text = Convert.ToInt32(findcolor.G).ToString();
            ContentPanelMaster.panel.BlueTextBox.Text = Convert.ToInt32(findcolor.B).ToString();

            ContentPanelMaster.FindColor();
            MainWindow.UpdateColorPanels();

            if(ContentPanelMaster.mainw.WindowState == WindowState.Minimized)
            {
                ContentPanelMaster.mainw.WindowState = WindowState.Normal;
            }    

            this.Close();
        }

        /* options */

        private void createSubwindows(IEnumerable<Screen> screens, Screen activeScreen)
        {
            foreach (var screen in screens)
            {
                //Console.WriteLine(screen.Bounds.Left + "x" + screen.Bounds.Top);
                if (screen.DeviceName != activeScreen.DeviceName)
                {
                    var newWindow = new sipSubWindow(screen, this)
                    {
                        Left = screen.Bounds.Left,
                        Top = screen.Bounds.Top,
                        Width = screen.Bounds.Width,
                        Height = screen.Bounds.Height,
                        WindowStartupLocation = WindowStartupLocation.Manual
                    };

                    subwindows.Add(newWindow);
                    newWindow.Show();

                    newWindow.WindowState = WindowState.Maximized;

                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        public System.Drawing.Rectangle getControlRectangle()
        {
            return new System.Drawing.Rectangle((int)Canvas.GetLeft(control),(int)Canvas.GetTop(control),(int)control.Width,(int)control.Height);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                this.saveColor();
            }
            else
            {
                if(e.Key == Key.Escape)
                {
                    this.Close();
                }
            }
        }
    }
}
