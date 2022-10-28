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

namespace CourseWorkApp
{
    public class SunRay
    {
        private List<Line> rayList = new List<Line>();
        private Path rayPath = new Path();
        SolidColorBrush activeBrush = new SolidColorBrush(Color.FromRgb(242, 255, 0));
        private static SolidColorBrush rayBrush = new SolidColorBrush(Color.FromRgb(242, 255, 0));
   
        public List<Line> GetRayList()
        {
            return rayList;
        }

        public void UpdateRayList(double rayAngle, Point firstP)
        {

            rayList.Clear();
            rayList.Add(Level.Instance.applyLogics(rayAngle, firstP));
            foreach (var item in rayList)
            {
                item.StrokeThickness = 5;
                item.Stroke = Brushes.Yellow;
            }
        }

    }
}
