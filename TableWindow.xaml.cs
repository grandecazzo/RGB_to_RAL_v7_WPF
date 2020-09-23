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
using System.Windows.Shapes;

namespace RGBtoRALv7_WPF
{
    /// <summary>
    /// Логика взаимодействия для TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        private MainWindow papaWindow;
        public TableWindow(MainWindow window)
        {
            papaWindow = window;
            InitializeComponent();

            List<RGBtoRAL> colors = RGBFinder.fromXML(ContentPanelMaster.currentCollection);

            foreach(var color in colors)
            {

            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            papaWindow.Show();
            this.Close();
        }
    }
}
