using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using System.Windows.Shapes;
using CourseWorkApp;
using System.Windows.Navigation;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Net.Mail;

namespace CourseWorkApp
{
    public class Rotators
    {
        double rotatorSide = 30;
        List<Rectangle> rotatorsList = new List<Rectangle>();
        Rectangle rotator;

        public Rotators()
        {
        }

        public void AddRotator(Rectangle prototype)
        {
            rotator = new Rectangle();
            rotator.Margin = prototype.Margin;
            rotator.Width = rotatorSide;
            rotator.Height = rotatorSide; 
            rotator.Tag = prototype.Tag;
            rotator.Fill = Brushes.Red;
            rotatorsList.Add(rotator);
        }
        
        public List<Rectangle> GetListOfRotators()
        {
            return rotatorsList;
        }
    }
}
