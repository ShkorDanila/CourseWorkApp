using System;
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
    public class FinishObject
    {
        GeometryGroup finishObjects = new GeometryGroup();
        Path finishPath = new Path();
        List<RectangleGeometry> collisionFinish = new List<RectangleGeometry>();

        private const double finishHeight = 25;
        private const double finishWidth = 25;

        public FinishObject()
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 204, 204, 255);
            finishPath.Fill = mySolidColorBrush;
        }

        public Path getPath()
        {
            finishPath.Data = finishObjects;
            return finishPath;
        }

        public void addFinish(double x, double y)
        {
                RectangleGeometry mir = new RectangleGeometry(new Rect(x, y, finishWidth, finishHeight));
                collisionFinish.Add(mir);
                finishObjects.Children.Add(mir);
        }

        public List<RectangleGeometry> getCollisions()
        {
            return collisionFinish;
        }
    }
}
