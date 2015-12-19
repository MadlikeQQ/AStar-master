using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Map
    {
        private int[,] map;
        private int height;
        private int width;

        public Map()
        {
            SetMap(0);
        }

        public Map(int height, int width,string[] lines, int num)
        {
            this.height = height;
            this.width = width;
            SetMap(lines, num);
        }


        public void SetMap(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        map = new[,] {{0,0,0,0,1,0,0},
                                      {1,1,1,1,1,1,0},
                                      {0,1,0,0,0,1,0},
                                      {0,1,0,1,0,1,0},
                                      {0,0,0,1,0,0,0}};
                        height = 5;
                        width = 7;
                        break;
                    }
                case 1:
                    {
                        map = new[,] {{0,1,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                      {0,1,1,1,1,1,0,1,0,0,0,0,0,1,0},
                                      {0,1,0,0,0,1,0,1,0,0,0,0,0,1,0},
                                      {0,1,0,1,0,1,0,1,0,0,0,0,0,1,0},
                                      {0,1,0,1,1,1,0,1,0,0,0,0,0,1,0},
                                      {0,0,0,0,0,0,0,1,0,0,0,0,0,1,0},
                                      {1,1,1,1,1,1,0,1,0,0,0,0,0,1,0},
                                      {0,0,0,0,0,0,0,1,0,0,0,0,0,1,0},
                                      {0,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
                                      
                        height = 10;
                        width = 15;
                        break;
                    }
                case 2:
                    {
                        map = new[,] {{1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1},
                                      {0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1},
                                      {0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
                                      {0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1},
                                      {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1}
                        };
                        height = 13;
                        width = 20;
                        break;
                    }
            }
        }

        public void SetMap(string[] lines, int num)
        {
            int i, j;
           this.map = new int[height,width];

            for(i=num; i < num + height; i++)

                for (j = 0; j < width; j++)
                {
                   switch (lines[i][j])
                    {
                        case 'X':
                            {
                                map[i-num,j] = 1;
                               // Console.Write("i=" + (i-num) + ", j=" + j + ": " + map[i-num, j]);
                            //    Console.WriteLine();
                                break;
                            }
                        case 'O':
                            {
                                map[i-num, j] = 0;
                             //   Console.Write("i=" + (i - num) + ", j=" + j + ": " + map[i - num, j]);
                             //   Console.WriteLine();
                                break;
                            }


                    } 
                }
        }


        public void print1()
        {
            Console.WriteLine(map[0, 0]);
        }

        #region public functions

        public void Mark(int x, int y, int z)
        {
            map[y, x] = z;
        }

        public void Mark(Point input, int val)
        {
            map[input.Y  , input.X ] = val;
        }

  

    public void DrawMap()
    {
        for (int y = 0; y < height; y++)
        {
            String buffer = "";
            for (int x = 0; x < width; x++)
            {
                if (map[y, x] == 0)
                {
                    buffer += ' ';
                }
                if (map[y, x] == 1)
                {
                    buffer += 'X';
                }
                if (map[y, x] == 2)
                {
                    buffer += '.';
                }
                if (map[y, x] == 3)
                {
                    buffer += 'A';
                }
                
                    if (map[y, x] == 4)
                {
                    buffer += 'G';
                }
                if (map[y, x] == 5)
                  {
                        buffer += '*';
                    }
               if (map[y, x] == 6)
                 {
                        buffer += 'B';
                 }

                }
            Console.Write(buffer + "\n");
        }
        Console.WriteLine("--------------------------------");
            System.Threading.Thread.Sleep(600);
       // Console.ReadLine();
    }
        #endregion



        #region Properties
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
 
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public bool GetWalkable(int x, int y)
        {
            return map[y, x] == 0;
        }

    }
    #endregion

}
