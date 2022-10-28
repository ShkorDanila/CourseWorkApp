using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Animation;
using System.CodeDom;

namespace CourseWork
{
    public class PowerLight
    {
        GeometryGroup powerUp = new GeometryGroup();
        Path powerPath = new Path();
        List<RectangleGeometry> collisionPower = new List<RectangleGeometry>();
        SolidColorBrush activeBrush;

        private const double panelHeight = 150;
        private const double panelWidth = 15;

        public PowerLight()
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 204, 204, 255);
            powerPath.Fill = mySolidColorBrush;
            powerPath.StrokeThickness = 10;
        }
        public Path getPath()
        {
            powerPath.Data = powerUp;
            return powerPath;
        }

        public void addPowerUp(double x, double y, bool vertical = true)
        {
            if (vertical)
            {
                RectangleGeometry gl = new RectangleGeometry(new Rect(x, y, panelWidth, panelHeight));
                collisionPower.Add(gl);
                powerUp.Children.Add(gl);
            }
            else
            {
                RectangleGeometry mir = new RectangleGeometry(new Rect(x, y, panelHeight, panelWidth));
                collisionPower.Add(mir);
                powerUp.Children.Add(mir);
            }
        }
        public List<RectangleGeometry> getCollisions()
        {
            return collisionPower;
        }
    }
}
