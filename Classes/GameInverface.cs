using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CourseWork
{
    public class GameInverface
    {
        Button mirrorButton;
        Button glassButton;
        Slider verticalSlider;
        Slider horizontalSlider;
        Button confirmButton;
        Button rotationButton;

        Rectangle prototypeRectangle = new Rectangle();

        List<Control> controls = new List<Control>();
        List<RectangleGeometry> confirmedMirrors = new List<RectangleGeometry>();
        List<RectangleGeometry> confirmedGlass = new List<RectangleGeometry>();

        bool isRotated = false;

        double gridWidth, gridHeight;

        public GameInverface(double gridWidth, double gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;

            mirrorButton = new Button();
            glassButton = new Button();
            horizontalSlider = new Slider();
            verticalSlider = new Slider();
            confirmButton = new Button();
            rotationButton = new Button();

            verticalSlider.Margin = new Thickness(gridWidth/2 + 70, gridHeight - 100, 0, 0);
            verticalSlider.Maximum = gridHeight - 240;
            verticalSlider.Minimum = -gridHeight + 400;
            verticalSlider.Height = 90;
            verticalSlider.Orientation = Orientation.Vertical;
            verticalSlider.IsEnabled = false;
            verticalSlider.ValueChanged += verticalSlider_Slided;

            horizontalSlider.Margin = new Thickness(350, gridHeight - 60, 0, 0);
            horizontalSlider.Maximum = gridWidth - 250;
            horizontalSlider.Minimum = -gridWidth + 220;
            horizontalSlider.Width = 100;
            horizontalSlider.IsEnabled = false;
            horizontalSlider.ValueChanged += horizontalSlider_Slided;

            mirrorButton.Margin = new System.Windows.Thickness(0, gridHeight - 100, gridWidth/1.5,0);
            mirrorButton.Width = 100;
            mirrorButton.Height = 60;
            mirrorButton.Content = "addMirror";
            mirrorButton.Click += mirrorButton_Click;

            glassButton.Margin = new System.Windows.Thickness(0, gridHeight - 100, gridWidth / 1.5 - 300, 0);
            glassButton.Width = 100;
            glassButton.Height = 60;
            glassButton.Content = "addGlass";
            glassButton.Click += glassButton_Click;

            confirmButton.Margin = new System.Windows.Thickness(0, gridHeight - 100, gridWidth / 1.5 - 600, 0);
            confirmButton.Width = 100;
            confirmButton.Height = 60;
            confirmButton.Content = "Confirm";
            confirmButton.Click += confirmButton_Click;
            confirmButton.IsEnabled = false;

            rotationButton.Margin = new System.Windows.Thickness(0, gridHeight - 100, gridWidth / 1.5 - 900, 0);
            rotationButton.Width = 100;
            rotationButton.Height = 60;
            rotationButton.Content = "Rotate";
            rotationButton.Click += rotationButton_Click;
            rotationButton.IsEnabled = false;



            controls.Add(mirrorButton);
            controls.Add(glassButton);
            controls.Add(confirmButton);
            controls.Add(verticalSlider);
            controls.Add(horizontalSlider);
            controls.Add(rotationButton);

        }

        private void rotationButton_Click(object sender, RoutedEventArgs e)
        {
            double a = prototypeRectangle.Width;
            prototypeRectangle.Width = prototypeRectangle.Height;
            prototypeRectangle.Height = a;
            if (isRotated)
            {
                isRotated = false;
            }
            else
            {
                isRotated = true;
            }
        }

        private void horizontalSlider_Slided(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            prototypeRectangle.Margin = new Thickness(horizontalSlider.Value, 0, 0, verticalSlider.Value);
        }

        private void verticalSlider_Slided(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            prototypeRectangle.Margin = new Thickness(horizontalSlider.Value, 0, 0, verticalSlider.Value);
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            
            mirrorButton.IsEnabled = true;
            glassButton.IsEnabled = true;
            confirmButton.IsEnabled = false;
            rotationButton.IsEnabled = false;

            Rect newCollis = new Rect(gridWidth / 2 - prototypeRectangle.Width / 2 + prototypeRectangle.Margin.Left / 2, gridHeight / 2 - prototypeRectangle.Height / 2 - prototypeRectangle.Margin.Bottom / 2, prototypeRectangle.Width, prototypeRectangle.Height);

            bool intersection = false;

            if(prototypeRectangle.Tag == "mirror")
            {
                foreach(var item in LevelWindow.lvl.mirror.getCollisions())
                {
                    if (item.Rect.IntersectsWith(newCollis))
                        intersection = true;
                }
                foreach (var item in LevelWindow.lvl.powerUp.getCollisions())
                {
                    if (item.Rect.IntersectsWith(newCollis))
                        intersection = true;
                }
                foreach (var item in LevelWindow.lvl.boarders.getCollisions())
                {
                    if (item.Rect.IntersectsWith(newCollis))
                    {
                        intersection = true;
                    }
                }

                if (!intersection) {
                    if(!isRotated)
                        LevelWindow.lvl.mirror.addMirror(newCollis.X, newCollis.Y, true);
                    else
                        LevelWindow.lvl.mirror.addMirror(newCollis.X, newCollis.Y, false);

                    LevelWindow.lvl.renderLvl();
                }
            }
            if(prototypeRectangle.Tag == "power")
            {
                foreach (var item in LevelWindow.lvl.mirror.getCollisions())
                {
                    if (item.Rect.IntersectsWith(newCollis))
                        intersection = true;
                }
                foreach (var item in LevelWindow.lvl.powerUp.getCollisions())
                {
                    if (item.Rect.IntersectsWith(newCollis))
                        intersection = true;
                }
                foreach (var item in LevelWindow.lvl.boarders.getCollisions())
                {
                    if (item.Rect.IntersectsWith(newCollis))
                        intersection = true;
                }

                if (!intersection)
                {
                    LevelWindow.lvl.powerUp.addPowerUp(newCollis.X, newCollis.Y, true);
                    LevelWindow.lvl.renderLvl();
                }
            }

            prototypeRectangle.Width = 0;
            prototypeRectangle.Height = 0;

            horizontalSlider.Value = 0;
            verticalSlider.Value = 0;
            horizontalSlider.IsEnabled = false;
            verticalSlider.IsEnabled = false;
            LevelWindow.setMode(false);
        }

        private void glassButton_Click(object sender, RoutedEventArgs e)
        {
            mirrorButton.IsEnabled = false;
            glassButton.IsEnabled = false;
            confirmButton.IsEnabled = true;
            horizontalSlider.IsEnabled = true;
            verticalSlider.IsEnabled = true;
            rotationButton.IsEnabled = true;

            prototypeRectangle.Width = 15;
            prototypeRectangle.Height = 150;
            prototypeRectangle.Fill = Brushes.Black;
            prototypeRectangle.Tag = "power";
            LevelWindow.setMode(true);
        }

        private void mirrorButton_Click(object sender, RoutedEventArgs e)
        {
            mirrorButton.IsEnabled = false;
            glassButton.IsEnabled = false;
            confirmButton.IsEnabled = true;
            horizontalSlider.IsEnabled = true;
            verticalSlider.IsEnabled = true;
            rotationButton.IsEnabled = true;

            prototypeRectangle.Width = 15;
            prototypeRectangle.Height = 150;
            prototypeRectangle.Fill = Brushes.Black;
            prototypeRectangle.Tag = "mirror";
            LevelWindow.setMode(true);
        }

        public List<Control> getControls()
        {
            return controls;
        }
        public Rectangle getPrototype()
        {
            return prototypeRectangle;
        }

        public List<RectangleGeometry> getMirrorList()
        {
            return confirmedMirrors;
        }
        public List<RectangleGeometry> getGlassList()
        {
            return confirmedGlass;
        }

    }
}
