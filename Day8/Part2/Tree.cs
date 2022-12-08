using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part2
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
        public int ScenicScore { 
            get {
                return Top * Bottom * Left * Right;
            } 
        }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }


    }
}