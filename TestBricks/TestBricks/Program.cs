using System;

namespace TestBricks
{
    class Program
    {
        static int Main(string[] args)
        {

            Console.WriteLine("Enter dimmentions: ");
            
            var dimenntions = Console.ReadLine();
            var dim = dimenntions.Split(" ");
            int y = int.Parse(dim[0]);
            int x = int.Parse(dim[1]);
            int[,] wall = new int[y,x];//Define initial wall
            int?[,] result = new int?[y, x];//Define result wall

            if(x%2 != 0 || y%2 != 0 || x > 100 || y > 100)// dimension validation
            {
                Console.WriteLine("No solution exists!");
                return -1;
            }

            for(int i = 0; i<y; i++)
            {
                var line = Console.ReadLine();
                var lineValues = line.Split(" ");
                if(lineValues.Length != x)
                {
                    Console.WriteLine("Too many or too low bricks per row");
                    return -1;
                }

                for(int j = 0; j<lineValues.Length; j++)
                {
                    wall[i,j] = int.Parse(lineValues[j]);
                }
            }

            //----------------------------------------------------

            for(int i = 0; i<y; i++)
            {
                for(int j = 0; j < x; j++)
                {
                    //Check if the row is odd
                    if ((i + 1) % 2 != 0)
                    {
                        
                        if (i < (y - 1) && j < (x - 1))
                        {
                            //Check if the bricks are horizontal
                            if (wall[i, j] == wall[i, j + 1])
                            {
                                // Rotate bricks verticaly
                                if (result[i, j] == null && result[i + 1, j] == null)
                                {
                                    result[i, j] = wall[i, j];
                                    result[i + 1, j] = wall[i, j + 1];
                                }
                                j++;

                                if ((j + 2) <= (x - 1))
                                {
                                    //Check neighbour brick position on the same row
                                    if (i < (y - 1) && j < (x - 1) && wall[i, j + 1] == wall[i, j + 2])
                                    {
                                        if (result[i, j] == null && result[i, j + 1] == null)
                                        {
                                            result[i, j] = wall[i, j + 1];
                                            result[i, j + 1] = wall[i, j + 2];
                                        }
                                    }

                                    //Check neighbour brick position on the next row
                                    if (i < (y - 1) && j < (x - 1) && wall[i, j + 1] == wall[i +1, j + 1])
                                    {
                                        if (result[i, j] == null && result[i, j + 1] == null)
                                        {
                                            result[i, j] = wall[i, j + 1];
                                            result[i, j + 1] = wall[i+1, j + 1];
                                        }
                                    }
                                    //Checks if the bottom row is horizontal, and then rotates it verticaly
                                    if (i < (y - 1) && j < (x - 1) && wall[i + 1, j - 1] == wall[i + 1, j])
                                    {
                                        if (result[i + 1, j] == null && result[i + 1, j + 1] == null)
                                        {
                                            result[i + 1, j] = wall[i + 1, j - 1];
                                            result[i + 1, j + 1] = wall[i + 1, j];
                                        }
                                    }

                                    j += 2;
                                    continue;
                                }
                            }

                            //Check if the bricks are vertical
                            if (wall[i,j] == wall[i+1, j])
                            {
                                //Rotates vetical to horizontal bricks
                                if (result[i, j] == null && result[i, j+1] == null)
                                {
                                    result[i, j] = wall[i, j];
                                    result[i, j+1] = wall[i+1, j];
                                }
                                j++;

                                //Rotates next vertical brick to horizontal
                                if (i < (y - 1) && j < (x - 1) && wall[i, j] == wall[i, j + 1])
                                {
                                    if (result[i, j+1] == null && result[i, j + 2] == null)
                                    {
                                        result[i, j+1] = wall[i, j];
                                        result[i, j + 2] = wall[i, j + 1];
                                    }
                                }
                                j+=2;

                                //Check for out of boundries. Edge case when the last two bricks are horizontal
                                if (j > x)
                                {
                                    j -= 2;
                                    if (i < (y - 1) && wall[i, j] == wall[i + 1, j])
                                    {
                                        //Checks if the bricks are horizontal, rotate verticaly.
                                        if (result[i + 1, j - 1] == null && result[i + 1, j] == null)
                                        {
                                            result[i + 1, j - 1] = wall[i, j];
                                            result[i + 1, j] = wall[i + 1, j];
                                            continue;
                                        }
                                    }
                                    continue;
                                }

                                //Rotate horizontal to vertical neightbour brick
                                if (i < (y - 1) && wall[i, j] == wall[i + 1, j])
                                {
                                    if (result[i + 1, j-1] == null && result[i + 1, j] == null)
                                    {
                                        result[i + 1, j - 1] = wall[i, j];
                                        result[i + 1, j] = wall[i + 1, j];
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    //Check if the row is even
                    if((i+1)%2 == 0 && j < (x-1) && wall[i,j] == wall[i, j+1])
                    {
                        //Checks if the last brick is vertical and rotate horizontally
                       if(result[i-1, j+1] == null && result[i, j+1] == null)
                        {
                            result[i - 1, j + 1] = wall[i, j];
                            result[i, j + 1] = wall[i, j + 1];
                            continue;
                        }
                       //Checks if the last brick is vertical and rotate horizontally
                       if(result[i, j] == null && result[i, j-1] == null)
                        {
                            result[i, j] = wall[i, j];
                            result[i, j - 1] = wall[i, j + 1];
                            continue;
                        }
                    }
                }
            }


            //----------------------------------------------------
            Console.WriteLine("----------------------------------");

            for(int i = 0; i<y; i++)
            {
                for(int j = 0; j< x; j++)
                {
                    if(result[i, j] == null)
                    {
                        Console.Write("* ");
                    }
                    else
                        Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.Read();
            return 0;
        }
    }
}
