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
        static double width = 1400;
        static double height = 630;

        static MainWindow Instance;
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            mainWindow.Width = width;
            mainWindow.Height = height;
            mainFrame.Content = new MainPage();
        }
        public static double getWidth()
        {
            return MainWindow.Instance.Width;
        }
        
        public static double getHeight()
        {
            return MainWindow.Instance.Height;
        }


    }
}
