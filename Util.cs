using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfScreenHelper;
using Image = System.Drawing.Image;

namespace RGBtoRALv7_WPF
{
    static class Util
    {
        public static BitmapImage ImageFromBuffer(Byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        public static BitmapImage ImageFromBuffer(Bitmap pic)
        {
            byte[] bytes = ImageToByte(pic);
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        public static SolidColorBrush setColor(byte R, byte G, byte B)
        {
            return new SolidColorBrush(System.Windows.Media.Color.FromRgb(R, G, B));
        }

        public static SolidColorBrush setColor(byte A,byte R, byte G, byte B)
        {
            return new SolidColorBrush(System.Windows.Media.Color.FromArgb(A,R, G, B));
        }

        public static SolidColorBrush setColor(System.Windows.Media.Color newColor)
        {
            return new SolidColorBrush(newColor);
        }

        /*prinscreener*/

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr onj);

        static Bitmap CaptureArea(Screen screen)
        {
            Bitmap screen_image = new Bitmap((int)screen.Bounds.Width,(int)screen.Bounds.Height);

            using (Graphics g = Graphics.FromImage(screen_image))
            {
                g.CopyFromScreen((int)screen.Bounds.Left, (int)screen.Bounds.Top, 0, 0, screen_image.Size);
            }


            return screen_image;
        }

        public static BitmapImage getWindowScreen(Screen screen)
        {
            Bitmap bitmap = CaptureArea(screen);

            BitmapImage retval;

            byte[] data = ImageToByte(bitmap);

            retval = ImageFromBuffer(data);

            return retval;
        }

        static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static System.Windows.Media.Color getColorOfPixel(BitmapImage img, int X, int Y)
        {
            int stride = img.PixelWidth * 4;
            int size = img.PixelHeight * stride;
            byte[] pixels = new byte[size];
            img.CopyPixels(pixels, stride, 0);

            int index = Y * stride + 4 * X;

            byte blue = pixels[index];
            byte green = pixels[index + 1];
            byte red = pixels[index + 2];

            System.Windows.Media.Color resultcolor = System.Windows.Media.Color.FromArgb(255,red,green,blue);

            return resultcolor;
        }
    }
}
