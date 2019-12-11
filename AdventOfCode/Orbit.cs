using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Orbit
    {
        public Orbit Parent { get; set; }
        public List<Orbit> SubOrbit { get; set; }
        public string Name { get; set; }
        public int Depth { get; set; }

        public Orbit(Orbit parent, string name, int depth)
        {
            Parent = parent;
            SubOrbit = new List<Orbit>();
            Name = name;
            Depth = depth;
        }

        public Orbit AddChildOrbit(Orbit planet)
        {
            SubOrbit.Add(planet);
            planet.Depth = planet.Depth+1;
            if (planet.Name == "YOU")
            {
                Console.WriteLine("You are at depth " + planet.Depth);
            }
            if (planet.Name == "SAN")
            {
                Console.WriteLine("Santa is at depth " + planet.Depth);
            }

            return planet;
        }

    }

}
