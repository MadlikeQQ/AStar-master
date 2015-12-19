using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Pathfinder
    {
        private SearchNode[,] searchNodes;
        private List<SearchNode> openList = new List<SearchNode>();
        private List<SearchNode> closedList = new List<SearchNode>();
        private List<Point> path = new List<Point>();

        private int width;
        private int height;


        public Pathfinder(Map map)
        {
            width = map.Width;
            height = map.Height;
         
            InitializeSearchNodes(map);
        }

        public List<Point> FindPath(Point startPoint, Point endPoint)
        {
            
            if (startPoint == null || endPoint == null) { Console.WriteLine("Koprofragos"); return null; }
            if (startPoint == endPoint)
            {
                return new List<Point>();
            }
            ///////////////////////////////////////////////////////////////////
            // Step 1: Clear the open and closed lists and reset all of the  //
            // nodes F and G values incase they're still set from last time. //
            ///////////////////////////////////////////////////////////////////
            ResetSearchNodes();

            // Store references to start and end nodes for convience.
            SearchNode startNode = searchNodes[startPoint.X,startPoint.Y];
            SearchNode endNode = searchNodes[endPoint.X, endPoint.Y];

            ///////////////////////////////////////////////////////////////////
            // Step 2: Set the start node's G value to 0 and its F value to  //
            //         the estimated distance between the start node and goal//
            //         node.  (This is where the heuristic comes in) and add //
            //         it to the open list                                   //
            ///////////////////////////////////////////////////////////////////
            startNode.InOpenList = true;
            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;
            openList.Add(startNode);
           
            ///////////////////////////////////////////////////////////////////
            // Step 3: While there are still nodes on the open list...       //
            ///////////////////////////////////////////////////////////////////
            while (openList.Count > 0)//each time openList contains the neighbors of the **previous** best node selected, updated iwth their 
                                      //heuristic distance to G.
            {

                // Find the node with the lowest F value
                
                SearchNode currentNode = FindBestNode();
             

                // If the open list is empty or no node can be found
                if (currentNode == null)
                {
                    
                    break;
                }

                // If we've reached our goal
                if (currentNode == endNode)
                {
                    return FindFinalPath(startNode, endNode);
                }


          

                // If not, keep going through the open list
                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    SearchNode neighbor = currentNode.Neighbors[i];
                    if (neighbor == null || neighbor.Walkable == false)
                    {
                        
                        continue;
                    }

                    if (neighbor.Walkable == false) {
                       
                        continue;
                    }



                    float distanceTraveled = currentNode.DistanceTraveled + 1;
                    float heuristic = Heuristic(neighbor.Position, endPoint);

                    // If the neighbor isn't in the closed or open list
                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {
                        neighbor.DistanceTraveled = distanceTraveled;
                        neighbor.DistanceToGoal = distanceTraveled + heuristic;
                        neighbor.Parent = currentNode;
                        
                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }
                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {
                        
                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;
                            neighbor.Parent = currentNode;
                            
                        }
                        
                    }
                }


                openList.Remove(currentNode);
                currentNode.InClosedList = true;
                
            }

            return new List<Point>();
        }

        private float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X)
                 + Math.Abs(point1.Y - point2.Y);
        }

        private void ResetSearchNodes()
        {
            openList.Clear();
            closedList.Clear();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SearchNode node = searchNodes[x, y];
                    if (node == null)
                    {
                        continue;
                    }
                    node.InOpenList = false;
                    node.InClosedList = false;
                    node.DistanceTraveled = float.MaxValue;
                    node.DistanceToGoal = float.MaxValue;
                }
            }
        }

        private List<Point> FindFinalPath(SearchNode startNode, SearchNode endNode)
        {
            closedList.Add(endNode);
            SearchNode parentTile = endNode.Parent;
            while (parentTile != startNode)
            {
                closedList.Add(parentTile);
                parentTile = parentTile.Parent;
            }

            List<Point> finalPath = new List<Point>();

            for (int i = closedList.Count - 1; i >= 0; i--)
            {
                finalPath.Add(new Point(closedList[i].Position.X,
                                        closedList[i].Position.Y));
            }
            return finalPath;
        }

        private SearchNode FindBestNode()
        {
           
            SearchNode currentTile = new SearchNode();//openList[0];
            
            float smallestDistanceToGoal = float.MaxValue;
            for (int i = 0; i < openList.Count; i++)
            {
              
                if (openList[i].DistanceToGoal < smallestDistanceToGoal)
                {
                   
                    currentTile = openList[i];
                    smallestDistanceToGoal = currentTile.DistanceToGoal;
                }
            }
       
            return currentTile;
        }

        private void InitializeSearchNodes(Map map)
        {
            searchNodes = new SearchNode[width,height];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SearchNode node = new SearchNode();
                    node.Position = new Point(x,y);
                    node.Walkable = map.GetWalkable(x, y);
                    if (node.Walkable)
                    {
                        node.Neighbors = new SearchNode[4];
                        searchNodes[x, y] = node;
                    }
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SearchNode node = searchNodes[x, y];
                    if (node == null || node.Walkable == false)
                    {
                        continue;
                    }

                    Point[] neighbors = new Point[]
                    {
                        new Point(x ,y - 1),
                        new Point(x ,y + 1),
                        new Point(x - 1, y),
                        new Point(x + 1, y),
                    };

                    for(int i = 0; i < neighbors.Length; i++)
                    {
                        Point position = neighbors[i];
                        if (position.X < 0 || position.X > width - 1 ||
                           position.Y < 0 || position.Y > height - 1)
                        {
                            continue;
                        }
                        SearchNode neighbor = searchNodes[position.X, position.Y];
                        if (neighbor == null || neighbor.Walkable == false)
                        {
                            continue;
                        }
                        node.Neighbors[i] = neighbor;
                    }
                }
            }
        }

        private bool isEqual(Point a, Point b)
        {
            return ((a.X == b.X) && (a.Y == b.Y));
        }

        public void conflicts(List<Point> pathA, List<Point> pathB, Map map)
        {
            int time = 0, min = Math.Min(pathA.Count, pathB.Count);
            

            while (time < min -1) 
            {
               
                if (isEqual(pathA[time], pathB[time]))
                {
                    if (!isEqual(pathA[time + 1], pathB[time - 1]))
                    {
                        // List<Point> temp = new List<Point>();
                        //temp.Add(pathB[time - 1]);
                        pathB.Insert(time - 1, pathB[time - 2]);
                        
                    }
                    else
                    {   //list - X - time-1 []
                        // time -1 , pisw stin time -2 , enas neigbor you time -2 , time -2 
                        int x = pathB[time - 2].X, y = pathB[time - 2].Y,stamou;

                        List< Point > neighbors = new List<Point>();
                        neighbors.Add(new Point(x, y - 1));
                        neighbors.Add(new Point(x, y + 1));
                        neighbors.Add(new Point(x - 1, y));
                        neighbors.Add(new Point(x + 1, y));

                        neighbors.Remove(pathB[time - 1]);
                        for( stamou = 0; stamou < neighbors.Count; stamou++)
                        {
                            if(map.GetWalkable(neighbors[stamou].X, neighbors[stamou].Y) == false) continue;
                            break;
                        }

                        pathB.Insert(time - 1, neighbors[stamou]);
                        pathB.Insert(time, pathB[time - 2]);
                        pathB.Insert(time + 1, pathB[time - 1]);time=1;
                       // min++;//=2;

                        

                        //{
                        //     new Point(x ,y - 1),
                        //     new Point(x ,y + 1),
                        //     new Point(x - 1, y),
                        //     new Point(x + 1, y),
                        //};

                    }

                }

                //    else
                //    { List<Point> temp = new List<Point>();

                //        while (isEqual(pathB[time -1 - count], pathA[time +1 + count]))
                //        {
                //            count++;
                //            //Point node = new Point(pathB[time + count].X, pathB[time].Y - 1);
                //            temp.Add(pathB[time -1 - count]);
                //            //pathB.Insert(time + count, pathB[time - count]);
                //            //pathB.Insert(time + 2, pathB[time]);
                //        }
                //        pathB.InsertRange(time + 1, temp);
                //        temp.Reverse();
                //        pathB.InsertRange(time + count, temp );
                //    }

                //    if (min == countA) { time++; continue; }
                //    else
                //    {
                //        countB += 2*count;
                //        if (countB > countA) min = countA;
                //        else min = countB;
                //    }


                //}
                //time+=2*count;
                time++;
            }
           
        }
    }
  
}
