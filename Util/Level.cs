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
using System.Net.Security;

namespace CourseWorkApp
{
    public class Level
    {
        List<Line> rayList = new List<Line>();

        SunRay ray;

        public Boarder boarders;

        public FinishObject finishObj;

        public Rotators rotators;

        public Rectangle prototype;

        List<Shape> gameObjectList = new List<Shape>();

        Point firstP;

        public double rayAngle = 0;

        double gridWidth;
        double gridHeight;

        public static Level Instance;

        public Level(double gridWidthSetting, double gridHeightSetting, string lvlPath)
        {
            Instance = this;

            prototype = new Rectangle();
            prototype.Width = 30;
            prototype.Height = 30;
            prototype.Fill = Brushes.Black;

            gridWidth = gridWidthSetting - 15;
            gridHeight = gridHeightSetting - 40;

            firstP = new Point(30,gridHeight/2);

            rotators = new Rotators();
            boarders = new Boarder(gridWidth, gridHeight);
            finishObj = new FinishObject(gridWidth, gridHeight);
            
            StreamReader lvlReader = new StreamReader(lvlPath);

            string line;
            while ((line = lvlReader.ReadLine()) != null)
            {
                string[] lineArray = line.Split();
                if (lineArray[0] == "boarder")
                    boarders.addBoarder(new Rect(Convert.ToDouble(lineArray[1]),
                                                 Convert.ToDouble(lineArray[2]),
                                                 Convert.ToDouble(lineArray[3]),
                                                 Convert.ToDouble(lineArray[4])));
                if (lineArray[0] == "finish")
                    finishObj.addFinish(Convert.ToDouble(lineArray[1]),
                                        Convert.ToDouble(lineArray[2]));
            }

            lvlReader.Close();

            ray = new SunRay();

            foreach (var item in ray.GetRayList())
                gameObjectList.Add(item);
            foreach (var item in boarders.GetBoarders())
                gameObjectList.Add(item);
        }

        public Line applyLogics(double angle, Point firstPoint)
        {
            bool intersect = false;
            
            double len = 0;
            Rect dot = new Rect(firstPoint.X, firstPoint.Y, 1, 1);
           
            while (!intersect)
            {
                len += 0.1;
                dot.X = Math.Cos(angle) * len + firstPoint.X;
                dot.Y = Math.Sin(angle) * len + firstPoint.Y;
                
                foreach(var item in boarders.GetBoarders())
                {
                    if (dot.IntersectsWith(GraphicUtilities.ConvertRectangleToRect(item,gridWidth,gridHeight)))
                    {
                        intersect = true;
                        break;
                    }
                    
                }
                foreach(var item in rotators.GetListOfRotators())
                {
                    if (dot.IntersectsWith(GraphicUtilities.ConvertRectangleToRect(item, gridWidth, gridHeight)))
                    {
                        intersect = true;
                        break;
                    }
                }
            }

            Line line = new Line();

            line.X1 = firstPoint.X;
            line.Y1 = firstPoint.Y;
            line.X2 = Math.Cos(angle) * len + firstPoint.X;
            line.Y2 = Math.Sin(angle) * len + firstPoint.Y;
            line.Stroke = Brushes.Yellow;
            return line;
        }
        public List<Line> GetFullRay()
        {
            ray.UpdateRayList(rayAngle, firstP);
            return ray.GetRayList();
        }


    }
}
