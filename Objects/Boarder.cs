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
using System.Windows.Controls.Primitives;
using System.Windows.Media.Media3D;

namespace CourseWorkApp
{
    public class Boarder
    {
        private Rectangle boarder;

        double gridWidth;
        double gridHeight;

        List<Rectangle> boardersList = new List<Rectangle>();
        
        public Boarder(double gridWidth, double gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;

            boarder = GraphicUtilities.ConvertRectToRectangle(new Rect(0, 0, 30, gridHeight), gridWidth, gridHeight);
            boarder.Fill = Brushes.Gray;
            boardersList.Add(boarder);

            boarder = GraphicUtilities.ConvertRectToRectangle(new Rect(gridWidth - 30, 0, 30, gridHeight), gridWidth, gridHeight);
            boarder.Fill = Brushes.Gray;
            boardersList.Add(boarder);

            boarder = GraphicUtilities.ConvertRectToRectangle(new Rect(30, 0, gridWidth - 60, 30), gridWidth, gridHeight);
            boarder.Fill = Brushes.Gray;
            boardersList.Add(boarder);

            boarder = GraphicUtilities.ConvertRectToRectangle(new Rect(30, gridHeight - 100, gridWidth - 60, 100), gridWidth, gridHeight);
            boarder.Fill = Brushes.Gray;
            boardersList.Add(boarder);

        }

        public void addBoarder(Rect r)
        {
            boardersList.Add(GraphicUtilities.ConvertRectToRectangle(r, gridWidth, gridHeight));
        }

        public List<Rectangle> GetBoarders()
        {
            foreach (var boarder in boardersList)
                boarder.Fill = Brushes.Gray;
            return boardersList;
        }

    }
}
