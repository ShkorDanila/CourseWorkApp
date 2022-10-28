using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для LevelSelectionPage.xaml
    /// </summary>
    public partial class LevelSelectionPage : Page
    {
        public LevelSelectionPage()
        {
            InitializeComponent();
            setLevelList();
        }
        void setLevelList()
        {
            string[] allfiles = Directory.GetFiles(@"D:\\COURSEWORK\\CourseWorkApp\\levels\\", "*.txt");

            foreach (string file in allfiles)
            {
                listOfLevels.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
            }
        }

        private void startLevelButton_Click(object sender, RoutedEventArgs e)
        {
            if (listOfLevels.SelectedItem != null)
            {
                string selectedLvl = "D:\\COURSEWORK\\CourseWorkApp\\levels\\" + Convert.ToString(listOfLevels.SelectedItem) + ".txt";
                NavigationService.Navigate(new LvlPage(selectedLvl));
            }
        }

        private void backToMMButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
