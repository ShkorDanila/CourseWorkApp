using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace CourseWorkApp
{
    public class GraphicUtilities
    {
        public static Rect ConvertRectangleToRect(Rectangle r, double gridWidth, double gridHeight) 
        { 
            return new Rect(gridWidth / 2 - r.Width / 2 + r.Margin.Left / 2, gridHeight / 2 - r.Height / 2 - r.Margin.Bottom / 2, r.Width, r.Height);
        }
        public static Rectangle ConvertRectToRectangle(Rect r, double gridWidth, double gridHeight)
        {
            Rectangle convertedR = new Rectangle();
            convertedR.Margin = new Thickness(2 * r.X + r.Width - gridWidth, 0, 0, gridHeight - r.Height - 2 * r.Y);
            convertedR.Width = r.Width;
            convertedR.Height = r.Height;
            return convertedR;
        }
    }
}
