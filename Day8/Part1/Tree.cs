using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part1
{
    public class Tree
    {
        public Tree(int height, int row, int column)
        {
            Height = height;
            Row = row;
            Column = column;


        }
        public int Height { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsVisible { get; set; }
    }
}