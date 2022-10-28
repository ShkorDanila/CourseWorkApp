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

namespace CourseWork
{
    public class Level
    {
        List<LineGeometry> rays = new List<LineGeometry>();

        SunRay ray;
        public Boarder boarders;
        public Mirror mirror;
        public FinishObject finishObj;
        public PowerLight powerUp;
        Point firstP;
        bool strokeUp = false;
        public double rayAngle = 0;
        Grid lvlGrid = new Grid();

        double gridWidth;
        double gridHeight;

        public Level(double gridWidthSetting, double gridHeightSetting, string lvlPath)
        {

            gridWidth = gridWidthSetting - 15;
            gridHeight = gridHeightSetting - 40;

            firstP = new Point(30,gridHeight/2);

            GameInverface gameControls = new GameInverface(gridWidth,gridHeight);

            boarders = new Boarder(gridWidth, gridHeight);
            mirror = new Mirror();
            finishObj = new FinishObject();
            powerUp = new PowerLight();
            
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
            }
            lvlReader.Close();

            lvlGrid.Children.Add(boarders.getPath());
            lvlGrid.Children.Add(mirror.getPath());
            lvlGrid.Children.Add(finishObj.getPath());
            lvlGrid.Children.Add(powerUp.getPath());

            rays.Add(applyLogics(rayAngle, firstP));

            ray = new SunRay(rays);
            lvlGrid.Children.Add(ray.getPath());
        }

        public LineGeometry applyLogics(double angle, Point firstPoint)
        {
            bool intersect = false;
            double len = 0;
            Rect dot = new Rect(firstPoint.X, firstPoint.Y, 1, 1);
           
            while (!intersect)
            {
                len += 0.1;
                dot.X = Math.Cos(angle) * len + firstPoint.X;
                dot.Y = Math.Sin(angle) * len + firstPoint.Y;
                foreach (var wall in boarders.getCollisions())
                {
                    if (dot.IntersectsWith(wall.Rect))
                    {
                        intersect = true;
                        break;
                    }
                }
                foreach (var mirror in mirror.getCollisions())
                {
                    if (dot.IntersectsWith(mirror.Rect))
                    {
                        if (dot.Y <= mirror.Rect.Y - 0.9)
                            rays.Add(applyLogics(-angle, new Point(dot.X, dot.Y)));
                        else if (dot.Y >= mirror.Rect.Y + mirror.Rect.Height - 0.9)
                            rays.Add(applyLogics(-angle, new Point(dot.X, dot.Y)));
                        else
                            rays.Add(applyLogics(Math.PI - angle, new Point(dot.X, dot.Y)));

                        intersect = true;
                        break;
                    }
                }
                foreach (var panel in powerUp.getCollisions())
                    {
                        if (dot.IntersectsWith(panel.Rect))
                        {
                            strokeUp = true;
                        }
                    }
                
                foreach (var finish in finishObj.getCollisions())
                {
                    if (dot.IntersectsWith(finish.Rect))
                    {
                        intersect = true;
                        break;
                    }
                }
            }
            LineGeometry line = new LineGeometry(firstPoint, new Point(Math.Cos(angle) * len + firstPoint.X, Math.Sin(angle) * len + firstPoint.Y));
            return line;
        }
        public Grid renderLvl()
        {
            strokeUp = false;
            rays.Clear();
            rays.Add(applyLogics(rayAngle, firstP));
            ray.changeGroup(rays);
            if (strokeUp)
                ray.changeStroke(10);
            else
                ray.changeStroke(5);
            return lvlGrid;
        }

        

    }
}
