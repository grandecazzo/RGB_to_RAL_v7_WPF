using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для sipControl.xaml
    /// </summary>

    public partial class sipControl : UserControl
    {

        protected bool isDragging;
        protected bool isOverBody = false;
        private Point clickPosition;

        private Sip_window window;

        [DllImport("user32.dll")]
        static extern void ClipCursor(ref System.Drawing.Rectangle rect);

        [DllImport("user32.dll")]
        static extern void ClipCursor(IntPtr rect);

        public sipControl(Sip_window papawindow, Color color)
        {
            window = papawindow;

            XmlNode langNode = Settings.loadSettings();

            InitializeComponent();


            this.sipInfoTexBlock.Text = langNode.SelectSingleNode("field_sip_textblock").InnerText;
            this.EnterButton.Content = langNode.SelectSingleNode("field_enter_button").InnerText;
            this.CancelButton.Content = langNode.SelectSingleNode("field_cancel_button").InnerText;

            this.colorPanel.Background = Util.setColor(color);
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.drag_move_2_line_white);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!isDragging)
                (sender as Image).Source = Util.ImageFromBuffer(Properties.Resources.drag_move_2_line);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            window.saveColor();
        }

        private void EnterButton_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Util.setColor(255, 255, 255);
        }

        private void EnterButton_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Util.setColor(80, 180, 245);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isOverBody)
            {
                isDragging = true;
                var draggableControl = this;
                clickPosition = e.GetPosition(this);
                draggableControl.CaptureMouse();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = this;
            draggable.ReleaseMouseCapture();
        }

        private void Image_MouseMove(object sender, MouseEventArgs e) 
        {
            var draggableControl = this;

            if (isDragging && draggableControl != null && isOverBody==false)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }
                if (!isOverLap(currentPosition))
                {
                    transform.X = currentPosition.X - clickPosition.X;
                    transform.Y = currentPosition.Y - clickPosition.Y;
                }
            }
        }

        private void DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            isOverBody = true;
        }

        private void DockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            isOverBody = false;
        }

        private bool isOverLap(Point currentPosition)
        {
            if (currentPosition.X + this.ActualWidth >= window.Width +180 ||
            currentPosition.Y + this.ActualHeight >= window.Height +280 ||
            currentPosition.X <= 5 ||
            currentPosition.Y <= 5)
            {
                return true;
            }
            return false;
        }
    }
}
