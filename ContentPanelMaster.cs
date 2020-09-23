using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace RGBtoRALv7_WPF.Resources
{
    static class ContentPanelMaster
    {
        public static RGBtoRAL ResultColor;

        public static RGBtoRAL inputColor;

        public static string currentCollection;

        public static RGBPanel panel;

        public static MainWindow mainw;

        /*private*/

        static string field_name = "Name";
        static string field_type = "Type";
        static string field_desc = "Description:";

        public static void setLanguage()
        {
            XmlNode langNode = Settings.loadSettings();

            panel.FindLabel.Content = langNode.SelectSingleNode("field_button_find").InnerText;

            field_name = langNode.SelectSingleNode("field_name").InnerText;
            field_type = langNode.SelectSingleNode("field_type").InnerText;
            field_desc = langNode.SelectSingleNode("field_desc").InnerText;

            panel.FindLabel.UpdateLayout();

            FindColor();
        }

        public static RGBPanel AddRGBPanel()
        {
            panel = new RGBPanel();

            

            panel.SetValue(Grid.RowProperty, 3);

            if (inputColor == null)
            {
                inputColor = new RGBtoRAL();

                Random rnd = new Random();

                panel.RedTextBox.Text = rnd.Next(0, 255).ToString();
                panel.GreenTextBox.Text = rnd.Next(0, 255).ToString();
                panel.BlueTextBox.Text = rnd.Next(0, 255).ToString();

                FindColor();
            }
            else
            {
                panel.RedTextBox.Text = inputColor.R.ToString();
                panel.GreenTextBox.Text = inputColor.G.ToString();
                panel.BlueTextBox.Text = inputColor.B.ToString();
                //Find()
            }

            setLanguage();

            return panel;
        }

        public static void UpdateColor()
        {
            inputColor.R = Int32.Parse(panel.RedTextBox.Text);
            inputColor.G = Int32.Parse(panel.GreenTextBox.Text);
            inputColor.B = Int32.Parse(panel.BlueTextBox.Text);
        }

        public static void FindColor()
        {
            UpdateColor();
            ResultColor = RGBFinder.Finder(currentCollection,inputColor.R,inputColor.G,inputColor.B);
            

            StringBuilder _sb = new StringBuilder();
            panel.NameTypeDescLabel.Text = _sb.AppendFormat("{0} {1} {2} {3}", field_name, ResultColor.Number, field_type, ResultColor.Type).ToString();

            StringBuilder sb = new StringBuilder();

            panel.DescriptionLabel.Content = sb.AppendFormat("{0} {1}",field_desc, ResultColor.Description).ToString();
            sb.Clear();
            panel.ColorDataLabel.Content = sb.AppendFormat("HEX: {0} RGB: {1};{2};{3}", ResultColor.HTML, ResultColor.R, ResultColor.G, ResultColor.B).ToString();
        }

    }
}
