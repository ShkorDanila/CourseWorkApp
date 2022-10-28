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
using System.Windows.Media.Imaging;

namespace CourseWorkApp
{
    public class FinishObject
    {
        List<Rectangle> finishList = new List<Rectangle>();

        private const double finishSide = 50;

        double gridWidth;
        double gridHeight;

        public FinishObject(double gridWidth, double gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
        }


        public void addFinish(double x, double y)
        {
            finishList.Add(GraphicUtilities.ConvertRectToRectangle(new Rect(x, y, finishSide, finishSide), gridWidth, gridHeight));
        }

        public List<Rectangle> GetFinishList()
        {
            return finishList;
        }
    }
}
