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
using System.Windows.Media.Media3D;
using System.Collections;
using System.Reflection;

namespace CourseWork
{
    public class SunRay
    {
        private GeometryGroup rayGroup = new GeometryGroup();
        private Path rayPath = new Path();
        SolidColorBrush activeBrush = new SolidColorBrush(Color.FromRgb(242, 255, 0));
        private static SolidColorBrush rayBrush = new SolidColorBrush(Color.FromRgb(242, 255, 0));

        public SunRay(List<LineGeometry>list)
        {
 
            foreach(var item in list)
            {
                rayGroup.Children.Add(item);
            }
            rayPath.Stroke = rayBrush;
            rayPath.StrokeThickness = 5;
            rayPath.Data = rayGroup;
        }
        
        public Path getPath()
        {
            return rayPath;
        }

        public void changeGroup(List<LineGeometry> list)
        {
            rayGroup.Children.Clear();
            foreach (var item in list)
            {
                rayGroup.Children.Add(item);
            }
        }

        public void changeStroke(int stroke)
        {
            rayPath.StrokeThickness = stroke;
        }
        public static SolidColorBrush getRayBrush()
        {
            return rayBrush;
        }

    }
}
