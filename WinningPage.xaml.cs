using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для WinningPage.xaml
    /// </summary>
    public partial class WinningPage : Page
    {
        public WinningPage(string winOrLose)
        {
            InitializeComponent();
            finishLabel.Text = winOrLose;
        }

        private void toLevelSelectButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LevelSelectionPage());
        }

        private void toMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
