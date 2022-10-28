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
    public class Mirror
    {
        GeometryGroup mirrors = new GeometryGroup();
        Path mirrorPath = new Path();
        List<RectangleGeometry> collisionsMirrors = new List<RectangleGeometry>();

        private const double mirrorHeight = 150;
        private const double mirrorWidth = 15;

        public Mirror()
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 204, 204, 255);
            mirrorPath.Fill = mySolidColorBrush;
        }

        public Path getPath()
        {
            mirrorPath.Data = mirrors;
            return mirrorPath;
        }

        public void addMirror(double x, double y, bool vertical = true)
        {
            if (vertical)
            {
                RectangleGeometry mir = new RectangleGeometry(new Rect(x, y, mirrorWidth, mirrorHeight));
                collisionsMirrors.Add(mir);
                mirrors.Children.Add(mir);
            }
            else
            {
                RectangleGeometry mir = new RectangleGeometry(new Rect(x, y, mirrorHeight, mirrorWidth));
                collisionsMirrors.Add(mir);
                mirrors.Children.Add(mir);
            }
            
        }
        public void addMirror(RectangleGeometry newMirror)
        {
            collisionsMirrors.Add(newMirror);
            mirrors.Children.Add(newMirror);
        }

        public List<RectangleGeometry> getCollisions()
        {
            return collisionsMirrors;
        }
    }
}
