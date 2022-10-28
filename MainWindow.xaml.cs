using System;
using System.Collections.Generic;
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

namespace CourseWorkApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static double width = 1600;
        static double height = 720;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Content = new MainPage();
        }
        public static double getWidth()
        {
            return width;
        }
        public static double getHeight()
        {
            return height;
        }


    }
}
