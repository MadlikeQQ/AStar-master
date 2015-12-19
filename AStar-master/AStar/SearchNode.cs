﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class SearchNode
    {
        public SearchNode Parent;
        public bool InOpenList;
        public bool InClosedList;
        public float DistanceToGoal;//h
        public float DistanceTraveled;//g



        public Point Position;
        public bool Walkable;
        public SearchNode[] Neighbors;
    }
}
