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

namespace CourseWork
{
    public class Boarder
    {
        private RectangleGeometry leftBoarder;
        private RectangleGeometry topBoarder;
        private RectangleGeometry bottomBoarder;
        private RectangleGeometry rightBoarder;

        private Rect leftCollision;
        private Rect rightCollision;
        private Rect topCollision;
        private Rect bottomCollision;

        GeometryGroup boarders = new GeometryGroup();
        Path boardersPath = new Path();
        List<RectangleGeometry> collisionBoards = new List<RectangleGeometry>();

        public Boarder(double width, double height)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 204, 204, 255);
            boardersPath.Fill = mySolidColorBrush;

            leftCollision = new Rect(0, 0, 30, height);
            rightCollision = new Rect(width-30,0,30,height);
            topCollision = new Rect(30, 0, width - 60, 30);
            bottomCollision = new Rect(30, height - 100, width - 60, 100);

            leftBoarder = new RectangleGeometry(leftCollision);
            collisionBoards.Add(leftBoarder);

            rightBoarder = new RectangleGeometry(rightCollision);
            collisionBoards.Add(rightBoarder);

            topBoarder = new RectangleGeometry(topCollision);
            collisionBoards.Add(topBoarder);

            bottomBoarder = new RectangleGeometry(bottomCollision);
            collisionBoards.Add(bottomBoarder);
                
            boarders.Children.Add(leftBoarder);
            boarders.Children.Add(rightBoarder);
            boarders.Children.Add(topBoarder);
            boarders.Children.Add(bottomBoarder);
        }

        public Path getPath()
        {
            boardersPath.Data = boarders;
            return boardersPath;
        }

        public void addBoarder(Rect r)
        {
            RectangleGeometry rG = new RectangleGeometry(r);
            collisionBoards.Add(rG);
            boarders.Children.Add(rG);
        }

        public List<RectangleGeometry> getCollisions()
        {
            return collisionBoards;
        }

    }
}
