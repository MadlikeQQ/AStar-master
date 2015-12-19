using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.SetWindowSize(80, 80);

            //foreach (string line in lines)
            //{
            //    console.writeline(line);
            //}

            //return;
            string[] lines = System.IO.File.ReadAllLines(@"inputaa.txt");


           

            List<Point> points = new List<Point>();

            //read agent A start 
            string[] bits = lines[1].Split(' ');

            Point agent_A_start = new Point(int.Parse(bits[0]) - 1, int.Parse(bits[1]) -1);
            points.Add(agent_A_start);
            //read agent B start
            bits = lines[2].Split(' ');

            Point agent_B_start = new Point(int.Parse(bits[0]) -1 , int.Parse(bits[1])-1 );
            points.Add(agent_B_start);
            //read goal

            bits = lines[3].Split(' ');
            Point goal = new Point(int.Parse(bits[0]) -1 , int.Parse(bits[1])-1 );
            points.Add(goal);

             
            foreach (Point p in points)
            {
                Console.WriteLine(p.Y + " " + p.X);

            }

            //read secondary goals
            int num = int.Parse(lines[4]);
            Point[] secondaryGoals = new Point[num + 1];
            int i;
            for( i = 5; i < 5 + num; i++)
            {
                bits = lines[i].Split(' ');
                secondaryGoals[i - 5] = new Point(int.Parse(bits[0])-1 , int.Parse(bits[1])-1 );
            }
            secondaryGoals[num] = goal;
            foreach (Point p in secondaryGoals) Console.WriteLine(p.Y + " " + p.X);
            ///  Console.WriteLine(myMap.Height +  " " + myMap.Width );
            

           

             bits = lines[0].Split(' ');
            //  myMap.setHeight(int.Parse(bits[0]));
            // myMap.setWidth(int.Parse(bits[1]));

            Map myMap = new Map(int.Parse(bits[1]), int.Parse(bits[0]), lines,i);
           // myMap.Height = );
            //myMap.Width = ;

            Console.WriteLine(myMap.Height + " " + myMap.Width);


           // myMap.SetMap(lines,i);

            //myMap.print1();
           // myMap.DrawMap();




           // return;




            // myMap.setHeight(lines[1][1]);



           // myMap.SetMap(2);
            Pathfinder pathfinder = new Pathfinder(myMap);
            Point startP = agent_A_start;
            //new Point(2,9);
            Point endP = secondaryGoals[0];
                //new Point(17, 8);
            myMap.Mark(startP, 3);
            myMap.Mark(endP, 4);
            myMap.DrawMap();
            List<Point> prevA = new List<Point>();
            //prev = null;
            //List<Point> path = pathfinder.FindPath(startP, endP, null);

            foreach (Point g in secondaryGoals)
            {

                myMap.Mark(g, 4);
                List<Point> path = pathfinder.FindPath(startP, g);
                prevA.AddRange(path);
                //prevA.Remove(prevA.Last());   A..../(G1)/
                startP = g;

            }
            prevA.Add(goal);

            startP = agent_B_start;
            myMap.Mark(startP, 6);
            List <Point> prevB = new List<Point>();

            foreach (Point g in secondaryGoals)
            {

                myMap.Mark(g, 4);
                List<Point> path = pathfinder.FindPath(startP, g);
                prevB.AddRange(path);
               // prevB.Remove(prevB.Last());
                startP = g;

            }
            prevB.Add(goal);

           

          

            pathfinder.conflicts(prevA, prevB,myMap);
            int max = Math.Max(prevA.Count, prevB.Count);
            //if (max == prevA.Count) prevA.Remove(prevA.Last());
            //else prevB.Remove(prevB.Last());
            //Console.WriteLine("Step count: " + prevA.Count);

            for (int mitso = 0; mitso < max - 2; mitso++)    
            {
               //Console.Write("t=" + mitso + ": ");
                if (mitso < prevA.Count)
                  //  Console.Write("A.("+ (prevA[mitso].X + 1 ) +"," + (prevA[mitso].Y +1)+  ")");
                  myMap.Mark(prevA[mitso].X, prevA[mitso].Y, 2);
                if (mitso < prevB.Count)
                 // Console.WriteLine("   B.(" + (prevB[mitso].X +1)+ "," + (prevB[mitso].Y +1) + ")");
                   myMap.Mark(prevB[mitso].X, prevB[mitso].Y, 5);


                
                //if (prevA[mitso].X == prevB[mitso].X && prevA[mitso].Y == prevB[mitso].Y)
                //{ Console.WriteLine("Crash!"); break; }

                myMap.DrawMap();
            }

            Console.WriteLine("A count:" + prevA.Count);
            Console.WriteLine("B count:" + prevB.Count);
            // myMap.Mark(startP, 3);
            // myMap.Mark(endP, 4);

        }
    }
}
