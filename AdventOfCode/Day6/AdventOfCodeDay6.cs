using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AdventOfCode;


namespace AdventOfCodeDay6
{
    class Day6
    {
        public static void Day6Main(string inputFile)
        {
            List<string> input = new List<string>(File.ReadAllLines(inputFile));
            
            string overlord = "COM";
            int depth = 0;
            Orbit system = new Orbit(null,overlord,depth);
            
            int count = 0;
            int distanceBetweenPlanets = 0;

            system  = AddChildOrbitRecursive(system, input);
            count = CountOrbits(system,count);
            distanceBetweenPlanets = DistanceBetweenPlanets(system,"YOU","SAN");

            Console.WriteLine("Final Count is " + count);
            Console.WriteLine("Distance between me and Santa is " + distanceBetweenPlanets);
            Console.ReadLine();
        }
        
        static public Orbit AddChildOrbitRecursive(Orbit newParent,List<string> family)
        {
            string parent = newParent.Name;
            //bool livingChildren = true;
            //while (livingChildren)
            //{
            //foreach (string iAmPossiblyAParentToo in family.FindAll(x => x.StartsWith(parent)))
            foreach (string iAmPossiblyAParentToo in family.FindAll(x => x.Split(")")[0] == parent))
            {
                string name = iAmPossiblyAParentToo.Split(")")[1];
                Orbit tempOrbit = new Orbit(newParent, name, newParent.Depth);
                newParent.AddChildOrbit(tempOrbit);
                tempOrbit = AddChildOrbitRecursive(tempOrbit, family);
            }
            //}

            return newParent;
        }

        static public int CountOrbits(Orbit system, int count)
        {
            foreach (Orbit childOrbit in system.SubOrbit)
            {
                //Console.WriteLine("Planet " + childOrbit.Name + " had depth " + childOrbit.Depth);
                count += childOrbit.Depth;
                count = CountOrbits(childOrbit, count);
            }

            return count;
        }

        static public int DistanceBetweenPlanets(Orbit system, string currentlyOrbiting, string santaOrbiting)
        {
            int distance = 0;

            HashSet<string> myPlanet = new HashSet<string>();
            HashSet<string> santaPlanet = new HashSet<string>();
            bool foundRoute = false;
            (myPlanet, foundRoute) = GetFullParentList(system, currentlyOrbiting, myPlanet,false);
            (santaPlanet, foundRoute) = GetFullParentList(system, santaOrbiting, santaPlanet,false);

            //foreach (string planet in myPlanet)
            //{
            //    Console.Write(planet + ",");
            //}
            //Console.WriteLine("santa's go");
            //foreach (string planet in santaPlanet)
            //{
            //    Console.Write(planet + ",");
            //}
            

            myPlanet.SymmetricExceptWith(santaPlanet);
            
            distance = myPlanet.Count;

            return distance;
        }

        static public Tuple<HashSet<string>, bool> GetFullParentList(Orbit system, string planet, HashSet<string> parentList, bool FoundMyDaddy)
        {
            if (system.Name == planet)
            {
                FoundMyDaddy = true;
                return Tuple.Create(parentList, FoundMyDaddy);
            }
            foreach (Orbit childOrbit in system.SubOrbit)
            {
                (parentList,FoundMyDaddy) = GetFullParentList(childOrbit, planet, parentList, FoundMyDaddy);
                if (FoundMyDaddy)
                {
                    parentList.Add(system.Name);
                    break;
                }
            }

            return Tuple.Create(parentList, FoundMyDaddy);
        }
    }
}
