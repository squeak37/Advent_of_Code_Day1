using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay5
{
    class Day5
    {
        public static void Day5Main(string inputFile)
        {

        }

        static public List<int> OpCodeForward(List<int> currList, int startPoint)
        {
            int loc1 = currList[startPoint + 1];
            int loc2 = currList[startPoint + 2];
            int var1 = currList[loc1];
            int var2 = currList[loc2];
            int setPoint = currList[startPoint + 3];


            switch (currList[startPoint])
            {
                case 1:
                    currList[setPoint] = var1 + var2;
                    break;
                case 2:
                    currList[setPoint] = var1 * var2;
                    break;
                case 3:
                    currList[setPoint] = var1;
                    break;
                case 4:
                    currList[var1] = setPoint;
                    break;
                default:
                    throw new System.InvalidOperationException("Only Valid initial number for Op Code's are 1, 2, 3, 4, and 99");
            }

            return currList;
        }
    }
}
