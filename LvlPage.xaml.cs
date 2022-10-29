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

        string[] rotatorVariations = {"right", "left", "top", "down", "top-down", "right-left"};

        int timeOnTimer = 0;

        public string currentLevel;

        static bool addObjectMode = false;
        static bool levelFinished = false;
        int activeTag = 0;
        bool intersect = false;

        Label timer;
        Label activeTagLabel;

        DispatcherTimer time;

        Button addRotator_Button = new Button(); 
        Button confirmRotator_Button = new Button();

        public static List<RectangleGeometry> mirrorsUsers = new List<RectangleGeometry>();

        public static LvlPage Instance;

        public LvlPage(string selectedLvl)
        {
            Instance = this;

            currentLevel = selectedLvl;

            InitializeComponent();

            addRotator_Button.Width = 100;
            addRotator_Button.Height = 40;
            addRotator_Button.VerticalAlignment = VerticalAlignment.Bottom;
            addRotator_Button.HorizontalAlignment = HorizontalAlignment.Left;
            addRotator_Button.Margin = new Thickness(300, 0, 0, 40);
            addRotator_Button.Content = "addRotator";
            addRotator_Button.Click += addRotatorButton_Click;

            confirmRotator_Button.Width = 100;
            confirmRotator_Button.Height = 40;
            confirmRotator_Button.VerticalAlignment = VerticalAlignment.Bottom;
            confirmRotator_Button.HorizontalAlignment = HorizontalAlignment.Left;
            confirmRotator_Button.Margin = new Thickness(450, 0, 0, 40);
            confirmRotator_Button.Content = "confirm Button";
            confirmRotator_Button.Click += confirmButtom_Click;

            lvl = new Level(MainWindow.getWidth(), MainWindow.getHeight(), currentLevel);

            levelGrid.MouseDown += LevelGrid_MouseDown;
            levelGrid.MouseUp += LevelGrid_MouseUp;
            levelGrid.MouseMove += LevelGrid_MouseMove;

            time = new DispatcherTimer();
            time.Interval = TimeSpan.FromTicks(1);
            time.Tick += timer_Tick;
            time.Start();

            timer = new Label();
            timer.VerticalAlignment = VerticalAlignment.Center;
            timer.HorizontalAlignment = HorizontalAlignment.Center;
            timer.FontSize = 40;
            timer.FontFamily = new FontFamily("Consolas Bold");
            timer.Foreground = Brushes.Black;

            lvlPage.Focus();

        }

        bool isMoved = false;

        private void LevelGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoved && addObjectMode)
            {
                levelGrid.Children.Remove(lvl.prototype);
                lvl.prototype.Margin = GraphicUtilities.ConvertRectToRectangle(new Rect(e.GetPosition(this).X, e.GetPosition(this).Y, 30, 30), MainWindow.getWidth(), MainWindow.getHeight()).Margin;
                levelGrid.Children.Add(lvl.prototype);
            }
        }

        private void LevelGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && addObjectMode)
            { 
                isMoved = false;
            }
        }

        private void LevelGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && addObjectMode)
            { 
                isMoved = true;
            }
        }

        public void LevelInit()
        {
            levelGrid.Children.Clear();

            foreach (var item in lvl.boarders.GetBoarders())
            {
                levelGrid.Children.Add(item);
            }
            foreach(var item in lvl.rotators.GetListOfRotators())
            {
                levelGrid.Children.Add(item);
            }
            foreach(var item in lvl.finishObj.GetFinishList())
            {
                levelGrid.Children.Add(item);
            }
            foreach (var item in lvl.GetFullRay())
            {
                levelGrid.Children.Add(item);
            }

            if (addObjectMode)
                levelGrid.Children.Add(lvl.prototype);

            levelGrid.Children.Add(addRotator_Button);
            levelGrid.Children.Add(confirmRotator_Button);
            levelGrid.Children.Add(timer);

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
            LevelInit();
            timeOnTimer += 1;
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
                }
                if (e.Key == Key.S && lvl.rayAngle <= 1.56)
                {
                    lvl.rayAngle += 0.004;
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
            addRotator_Button.IsEnabled = false;
            levelGrid.Children.Add(lvl.prototype);
            confirmRotator_Button.IsEnabled = true;
            lvl.prototype.Tag = rotatorVariations[activeTag];
        }

        private void confirmButtom_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in lvl.boarders.GetBoarders())
            {
                if (GraphicUtilities.ConvertRectangleToRect(item, MainWindow.getWidth(), MainWindow.getHeight()).IntersectsWith(GraphicUtilities.ConvertRectangleToRect(lvl.prototype, MainWindow.getWidth(), MainWindow.getHeight())))
                {
                    intersect = true;
                }
            }

            foreach (var item in lvl.rotators.GetListOfRotators())
            {
                if (GraphicUtilities.ConvertRectangleToRect(item, MainWindow.getWidth(), MainWindow.getHeight()).IntersectsWith(GraphicUtilities.ConvertRectangleToRect(lvl.prototype, MainWindow.getWidth(), MainWindow.getHeight())))
                {
                    intersect = true;
                }
            }

            foreach (var item in lvl.finishObj.GetFinishList())
            {
                if (GraphicUtilities.ConvertRectangleToRect(item, MainWindow.getWidth(), MainWindow.getHeight()).IntersectsWith(GraphicUtilities.ConvertRectangleToRect(lvl.prototype, MainWindow.getWidth(), MainWindow.getHeight())))
                {
                    intersect = true;
                }
            }



            levelGrid.Children.Remove(lvl.prototype);

            Rectangle newRotator = new Rectangle();
            newRotator = lvl.prototype;

            if (!intersect)
            {
                lvl.rotators.AddRotator(newRotator);
            }

            lvl.prototype.Margin = new Thickness(0, 0, 0, 0);

            intersect = false;
            addObjectMode = false;

            addRotator_Button.IsEnabled = true;
            confirmRotator_Button.IsEnabled = false;
        }

    }
}