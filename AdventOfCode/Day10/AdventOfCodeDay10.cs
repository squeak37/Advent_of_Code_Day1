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
            List<Tuple<double, double>> asteroidPos = new List<Tuple<double, double>>();
            double xLoc = 0;
            int canSee = 0;

            for (double yLoc = 0; yLoc<asteroidField.Length; yLoc++)
            {
                xLoc = 0;
                foreach (char loc in asteroidField[(int)yLoc])
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

            List<Tuple<double, double>> asteroidCheck1 = new List<Tuple<double, double>>(asteroidPos);
            List<Tuple<double, double>> asteroidCheck2 = new List<Tuple<double, double>>(asteroidPos);
            List<Tuple<double, double>> validAsteroidList = new List<Tuple<double, double>>();
            Tuple<double, double> finalLoc = Tuple.Create(double.NaN, double.NaN);
            List<Tuple<Tuple<double, double>, List<Tuple<double, double>>>> asteroidCompList = new List<Tuple<Tuple<double, double>, List<Tuple<double, double>>>>();

            //Tuple<double, double> currAsteroid = Tuple.Create(3.0, 4.0);
            bool isValid = false;
            bool checkIsBetween = false;
            int maxVision = 0;


            foreach (Tuple<double, double> currAsteroid in asteroidPos)
            {
                isValid = false;
                checkIsBetween = false;
                canSee = 0;
                validAsteroidList = new List<Tuple<double, double>>();

                asteroidCheck1 = new List<Tuple<double, double>>(asteroidPos);
                asteroidCheck1.Remove(currAsteroid);

                foreach (Tuple<double,double> checkAsteroid1 in asteroidCheck1)
                {
                    asteroidCheck2 = new List<Tuple<double, double>>(asteroidPos);
                    asteroidCheck2.Remove(currAsteroid);
                    asteroidCheck2.Remove(checkAsteroid1);
                    isValid = true;
                    foreach (Tuple<double, double> checkAsteroid2 in asteroidCheck2)
                    {
                        checkIsBetween = IsBetween(currAsteroid, checkAsteroid1, checkAsteroid2);
                        if (checkIsBetween)
                        {
                            isValid = false;
                        }
                    }
                    if (isValid)
                    {
                        validAsteroidList.Add(checkAsteroid1);
                        canSee++;
                    }
                }
                //Console.WriteLine("Current CanSee is " + canSee);
                asteroidCompList.Add(Tuple.Create(currAsteroid, new List<Tuple<double, double>>(validAsteroidList)));
                if (canSee > maxVision)
                {
                    maxVision = canSee;
                    finalLoc = currAsteroid;
                }
            }


            Console.WriteLine("Final Output is " + maxVision + " at location (" + finalLoc.Item1 + " , " + finalLoc.Item2 + ")");

            asteroidCheck1 = new List<Tuple<double, double>>(asteroidPos);
            asteroidCheck1.Remove(finalLoc);
            double angle = 0;

            foreach (Tuple<double, double> currAsteroid in asteroidCheck1)
            {
                angle = AngleCalculator(finalLoc, currAsteroid);
            }



            Console.ReadLine();
        }

        public static bool IsBetween(Tuple<double,double> a, Tuple<double, double> b, Tuple<double, double> c)
        {
            double bax = (b.Item1 - a.Item1);
            double bay = (b.Item2 - a.Item2);
            double cax = (c.Item1 - a.Item1);
            double cay = (c.Item2 - a.Item2);

            double crossProduct = (cay * bax) - (cax * bay);
            double dotProduct = (cax * bax) + (cay * bay);
            double squaredLengthBA = Math.Pow(bax,2) + Math.Pow(bay, 2);

            // If the cross product is non-0 then they can't intersect
            if (Math.Abs(crossProduct) > 0)
            {
                return false;
            }

            // If the dotproduct is < 0 then they are not on a single line.
            if (dotProduct < 0)
            {
                return false;
            }

            // If they are on the same line, check that point C is between A and B, not beyond it.
            if (dotProduct > squaredLengthBA)
            {
                return false;
            }

            return true;
        }

        public static double AngleCalculator(Tuple<double, double> a, Tuple<double, double> b)
        {
            double angle = 0;
            b = Tuple.Create(b.Item1 - a.Item1, b.Item2 - a.Item2);
            

            double ab = (a.Item1 * b.Item1) + (a.Item2 * b.Item2);
            double a2 = Math.Pow(Math.Pow(a.Item1, 2) + Math.Pow(a.Item2, 2), 0.5);
            double b2 = Math.Pow(Math.Pow(b.Item1, 2) + Math.Pow(b.Item2, 2), 0.5);

            double cosAngle = ab / (a2 * b2);

            angle = (Math.Acos(cosAngle)) * 180 / Math.PI;

            Console.WriteLine("Current angle is: " + angle);

            return angle;
        }
    }
}
