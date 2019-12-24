using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay10
{
    class Day10
    {
        public static void Day10Main(string inputFile)
        {
            string[] asteroidField = System.IO.File.ReadAllLines(inputFile);
            List<Tuple<int, int>> asteroidPos = new List<Tuple<int, int>>();
            int xLoc = 0;

            for (int yLoc = 0; yLoc<asteroidField.Length; yLoc++)
            {
                xLoc = 0;
                foreach (char loc in asteroidField[yLoc])
                {
                    switch (loc)
                    {
                        case '#':
                            asteroidPos.Add(Tuple.Create(xLoc, yLoc));
                            break;
                        case '.':
                            break;
                        default:
                            throw new System.InvalidOperationException("asteroid field should only contain empty space or fudging asteroids");
                    }
                    xLoc++;
                }
            }

            Console.WriteLine("Final Output is ");
            Console.ReadLine();
        }
    }
}
