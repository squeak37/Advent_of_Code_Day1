using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay8
{
    class Day8
    {
        public static void Day8Main(string inputFile)
        {
            string input = System.IO.File.ReadAllText(inputFile);

            int width = 25;
            int height = 6;
            string[] singleLayer = new string[height];
            string[,] finalLayer = new string[width,height];
            List < string[] > layers = new List<string[]>();
            int startPoint = 0;

            int no_layers = input.Length / (width * height);


            string currColour = "";
            
            for (int i = 0; i< no_layers; i ++)
            {
                startPoint = i*height*width;
                singleLayer = new string[height];
                for (int j = 0; j < height; j++)
                {
                    currColour = input.Substring(startPoint,width);
                    singleLayer[j] = currColour;
                    startPoint += width;
                }
                layers.Add(singleLayer);
            }

            Day8.OneTwoKnockout(layers, height, width);
            finalLayer = Day8.FinalPic(finalLayer, layers);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (finalLayer[j,i] == "0")
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                Console.WriteLine("");
            }

            //Console.WriteLine("Final answer is: ");
            Console.ReadLine();
        }

        public static void OneTwoKnockout(List<string[]> layers, int height, int width)
        {
            int no_layers = layers.Count;
            int[] zeroCount = new int[no_layers];
            int currZeroes = 0;
            int countFewestZeroes = 150;
            int posFewestZeroes = 150;
            string currColour = "";
            int currOnes = 0;
            int currTwos = 0;

            for (int i = 0; i < no_layers; i++)
            {
                currZeroes = 0;
                for (int j = 0; j < height; j++)
                {
                    currColour = layers[i][j];
                    currZeroes += currColour.Length - currColour.Replace("0", "").Length;
                }
                zeroCount[i] = currZeroes;
                if (zeroCount[i] < countFewestZeroes)
                {
                    countFewestZeroes = zeroCount[i];
                    posFewestZeroes = i;
                }
            }
            
            for (int j = 0; j < height; j++)
            {
                currColour = layers[posFewestZeroes][j];
                currOnes += currColour.Length - currColour.Replace("1", "").Length;
                currTwos += currColour.Length - currColour.Replace("2", "").Length;
            }


            Console.WriteLine("Fewest zero layer is: " + posFewestZeroes);
            Console.WriteLine("No 1's in that layer is: " + currOnes);
            Console.WriteLine("No 1's in that layer is: " + currTwos);
            Console.WriteLine("Final answer is: " + (currOnes * currTwos));
        }

        public static string[,] FinalPic(string[,] map, List<string[]> layers)
        {
            int width = layers[0][0].Length;
            int height = layers[0].Length;
            int currLayer = 0;

            for (int i=0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[j,i] = "2";
                    currLayer = 0;
                    while (map[j,i] == "2" && currLayer < layers.Count)
                    {
                        switch (layers[currLayer][i].Substring(j,1))
                        {
                            case "0":
                                currLayer++;
                                // White.
                                map[j, i] = "0";
                                break;
                            case "1":
                                currLayer++;
                                // Black.
                                map[j, i] = "1";
                                break;
                            case "2":
                                currLayer++;
                                // Transparant.
                                map[j, i] = "2";
                                break;
                            default:
                                currLayer++;
                                break;
                        }
                    }
                }
            }
            
            return map;
        }
    }
}
