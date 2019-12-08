using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCodeDay3
{
    class Day3
    {
        public static void Day3Main(string inputFile)
        {
            int currX;
            int currY;
            char direction;
            int distance = 0;
            int currWire = 0;
            int minDistance = 100000000;
            List<int> wireX = new List<int>();
            List<int> wireY = new List<int>();
            List<int> wire0X = new List<int>();
            List<int> wire0Y = new List<int>();
            List<int> wire1X = new List<int>();
            List<int> wire1Y = new List<int>();
            HashSet<string> wire0Path = new HashSet<string>();
            HashSet<string> wire1Path = new HashSet<string>();
            HashSet<string> intersectionPoint = new HashSet<string>();
            string currCoord;

            foreach (string line in System.IO.File.ReadAllLines(inputFile))
            {
                currX = 0;
                currY = 0;
                foreach(string path in line.Split(","))
                {
                    direction = path[0];
                    distance = Int32.Parse(Regex.Match(path,@"\d+").Value);

                    switch (direction)
                    {
                        case 'L':
                            for(int i=0; i<distance; i++)
                            {
                                currX -= 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        case 'R':
                            for (int i = 0; i < distance; i++)
                            {
                                currX += 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        case 'U':
                            for (int i = 0; i < distance; i++)
                            {
                                currY += 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        case 'D':
                            for (int i = 0; i < distance; i++)
                            {
                                currY -= 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        default:
                            break;

                    }
                }
                if (currWire == 0)
                {
                    wire0X = new List<int>(wireX);
                    wire0Y = new List<int>(wireY);
                    wireX = new List<int>();
                    wireY = new List<int>();
                    currWire++;
                }
                else if (currWire == 1)
                {
                    wire1X = new List<int>(wireX);
                    wire1Y = new List<int>(wireY);
                }
                
            }

            for (int i = 0; i < wire0X.Count; i++)
            {
                currCoord = wire0X[i] + "," + wire0Y[i];
                wire0Path.Add(currCoord);
            }
            for (int i = 0; i < wire1X.Count; i++)
            {
                currCoord = wire1X[i] + "," + wire1Y[i];
                wire1Path.Add(currCoord);
            }

            foreach(string coord in wire0Path)
            {
                if (wire1Path.Contains(coord))
                {
                    intersectionPoint.Add(coord);
                }
            }

            foreach (string intersection in intersectionPoint)
            {
                currX = Math.Abs(Int32.Parse(intersection.Split(',')[0]));
                currY = Math.Abs(Int32.Parse(intersection.Split(',')[1]));
                distance = currX + currY;
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }


            Console.WriteLine("shortest distance is " + minDistance);


            Console.ReadLine();

        }

        public static void Day3Secondary(string inputFile)
        {
            int currX;
            int currY;
            char direction;
            int distance = 0;
            int currWire = 0;
            int minDistance = 100000000;
            List<int> wireX = new List<int>();
            List<int> wireY = new List<int>();
            List<int> wire0X = new List<int>();
            List<int> wire0Y = new List<int>();
            List<int> wire1X = new List<int>();
            List<int> wire1Y = new List<int>();
            List<string> wire0ListPath = new List<string>();
            List<string> wire1ListPath = new List<string>();
            HashSet<string> wire0Path = new HashSet<string>();
            HashSet<string> wire1Path = new HashSet<string>();
            HashSet<string> intersectionPoint = new HashSet<string>();
            string currCoord;
            int wire0Distance;
            int wire1Distance;

            foreach (string line in System.IO.File.ReadAllLines(inputFile))
            {
                currX = 0;
                currY = 0;
                foreach (string path in line.Split(","))
                {
                    direction = path[0];
                    distance = Int32.Parse(Regex.Match(path, @"\d+").Value);

                    switch (direction)
                    {
                        case 'L':
                            for (int i = 0; i < distance; i++)
                            {
                                currX -= 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        case 'R':
                            for (int i = 0; i < distance; i++)
                            {
                                currX += 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        case 'U':
                            for (int i = 0; i < distance; i++)
                            {
                                currY += 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        case 'D':
                            for (int i = 0; i < distance; i++)
                            {
                                currY -= 1;
                                wireX.Add(currX);
                                wireY.Add(currY);
                            }
                            break;
                        default:
                            break;

                    }
                }
                if (currWire == 0)
                {
                    wire0X = new List<int>(wireX);
                    wire0Y = new List<int>(wireY);
                    wireX = new List<int>();
                    wireY = new List<int>();
                    currWire++;
                }
                else if (currWire == 1)
                {
                    wire1X = new List<int>(wireX);
                    wire1Y = new List<int>(wireY);
                }

            }

            for (int i = 0; i < wire0X.Count; i++)
            {
                currCoord = wire0X[i] + "," + wire0Y[i];
                wire0Path.Add(currCoord);
                wire0ListPath.Add(currCoord);
            }
            for (int i = 0; i < wire1X.Count; i++)
            {
                currCoord = wire1X[i] + "," + wire1Y[i];
                wire1Path.Add(currCoord);
                wire1ListPath.Add(currCoord);
            }

            foreach (string coord in wire0Path)
            {
                if (wire1Path.Contains(coord))
                {
                    intersectionPoint.Add(coord);
                }
            }



            foreach (string intersection in intersectionPoint)
            {
                wire0Distance = wire0ListPath.FindIndex(x => x.StartsWith(intersection)) + 1;
                wire1Distance = wire1ListPath.FindIndex(x => x.StartsWith(intersection)) + 1;
                distance = wire0Distance + wire1Distance;
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }


            Console.WriteLine("shortest distance is " + minDistance);


            Console.ReadLine();

        }
    }
}
