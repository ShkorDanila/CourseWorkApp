using CourseWorkApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace CourseWorkApp
{
    /// <summary>
    /// Логика взаимодействия для LvlPage.xaml
    /// </summary>
    public partial class LvlPage : Page
    {
        public static Level lvl;


        int timeOnTimer = 100;

        public string currentLevel;

        static bool addObjectMode = false;
        static bool levelFinished = false;

        Label timer;
        DispatcherTimer time;


        public static List<RectangleGeometry> mirrorsUsers = new List<RectangleGeometry>();

        public static LvlPage Instance;

        public LvlPage(string selectedLvl)
        {
            Instance = this;

            currentLevel = selectedLvl;

            InitializeComponent();

            lvl = new Level(1600, 720, currentLevel);

            lvl.prototype.MouseDown += Rectangle_MouseDown;
            lvl.prototype.MouseMove += Rectangle_MouseMove;
            lvl.prototype.MouseUp += Rectangle_MouseUp;

            time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += timer_Tick;
            time.Start();

            LevelInit();

            lvlPage.Focus();


            foreach (var item in lvl.boarders.GetBoarders())
                if (!levelGrid.Children.Contains(item))
                    levelGrid.Children.Add(item);

            timer = new Label();
            timer.VerticalAlignment = VerticalAlignment.Center;
            timer.HorizontalAlignment = HorizontalAlignment.Center;
            timer.FontSize = 40;
            timer.FontFamily = new FontFamily("Consolas Bold");
            timer.Foreground = Brushes.Black;
            levelGrid.Children.Add(timer);


        }

        public void LevelInit()
        {
            for (int i = 0; i < levelGrid.Children.Count; i++)
            {
                if (levelGrid.Children[i].GetType() == typeof(Line))
                {
                    levelGrid.Children.RemoveAt(i);
                }
            }

            foreach (var item in lvl.rotators.GetListOfRotators())
                if (!levelGrid.Children.Contains(item))
                    levelGrid.Children.Add(item);

            foreach (var item in lvl.GetFullRay())
                levelGrid.Children.Add(item);

            foreach (var item in lvl.rotators.GetListOfRotators())
                if (!levelGrid.Children.Contains(item))
                    levelGrid.Children.Add(item);

            if (levelFinished)
            {
                time.Stop();
                if (!currentLevel.Contains("{Finished}"))
                {
                    File.Copy(currentLevel, "D:\\COURSEWORK\\CourseWorkApp\\levels\\" + System.IO.Path.GetFileNameWithoutExtension(currentLevel) + " {Finished}.txt");
                    File.Delete(currentLevel);
                }
                NavigationService.Navigate(new WinningPage("You Won"));
                levelFinished = false;
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Content = timeOnTimer.ToString();
            timeOnTimer -= 1;
            if (timeOnTimer < 0)
            {
                time.Stop();
                NavigationService.Navigate(new WinningPage("Time is Up"));
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (!addObjectMode && !levelFinished)
            {
                if (e.Key == Key.W && lvl.rayAngle >= -1.56)
                {
                    lvl.rayAngle -= 0.004;
                    LevelInit();
                }
                if (e.Key == Key.S && lvl.rayAngle <= 1.56)
                {
                    lvl.rayAngle += 0.004;
                    LevelInit();
                }
            }
        }

        public static void FinishLevel()
        {
            levelFinished = true;
        }

      
        private void addRotatorButton_Click(object sender, RoutedEventArgs e)
        {
            addObjectMode = true;
            levelGrid.Children.Add(lvl.prototype);
            addRotator_Button.IsEnabled = false;
            confirmRotator_Button.IsEnabled = true;
        }
        private void confirmButtom_Click(object sender, RoutedEventArgs e)
        {
            addRotator_Button.IsEnabled = true;
            confirmRotator_Button.IsEnabled = false;
            lvl.rotators.AddRotator(lvl.prototype);
            levelGrid.Children.Remove(lvl.prototype);
            addObjectMode = false;
            LevelInit();
        }

        bool isMoved = false;
        Point startMovePosition;
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isMoved = true;
                startMovePosition = e.GetPosition(this);
            }
        }
        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                isMoved = false;
            }
        }
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoved)
            {
                lvl.prototype.Margin = GraphicUtilities.ConvertRectToRectangle(new Rect(e.GetPosition(this).X, e.GetPosition(this).Y, 30,30), 1600, 720).Margin;
            }
        }
    }
}