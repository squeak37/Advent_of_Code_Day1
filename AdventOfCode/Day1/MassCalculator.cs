using System;

namespace AdventOfCodeDay1
{
    class MassCalculator
    {
        static public double calculateMass(double mass)
        {
            double finalFuel = 0;

            finalFuel = Math.Floor(mass / 3) - 2;
            double fuelRequired = finalFuel;

            while (true)
            {
                fuelRequired = Math.Floor(fuelRequired / 3) - 2;
                if (fuelRequired <= 0)
                {
                    break;
                }
                else
                {
                    finalFuel += fuelRequired;
                }

            }


            return finalFuel;
        }
    }
}
