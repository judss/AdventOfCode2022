namespace Part1
{
    using System;
    using Newtonsoft.Json; 

    public class Program
    {
        private static int maxRow = 0;
        private static int maxColumn = 0;

        public static void Main(string[] args)
        {
            // read the puzzle input into a string
            var fileContents = File.ReadAllText(@"./puzzle-input.txt");

            // split out the rows
            var treeRows = fileContents.Split("\r\n");

            // map out all of the trees
            var trees = MapTrees(treeRows);

            // set constants
            var lastTree = trees.Last();
            maxRow = lastTree.Row;
            maxColumn = lastTree.Column;

            checkVisibilityOfTrees(trees);

            var visibleTrees = trees.Count(x => x.IsVisible);

            Console.WriteLine(visibleTrees);
        }

        private static void checkVisibilityOfTrees(List<Tree> trees)
        {
            foreach(var tree in trees)
            {
                if(tree.Column == 0 || tree.Row == 0)
                {
                    tree.IsVisible = true;
                }
                else
                {
                    var top = CheckIsVisibleFromTop(tree, trees);
                    var bottom = CheckIsVisibleFromBottom(tree, trees);
                    var left = CheckIsVisibleFromLeft(tree, trees);
                    var right = CheckIsVisibleFromRight(tree, trees);

                    if(top || bottom || left || right){
                        tree.IsVisible = true;
                    }
                }
            }
        }

        private static bool CheckIsVisibleFromTop(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = new List<Tree>();

            treesToCheck.AddRange(trees.Where(x=> x.Column == currentTree.Column && x.Row < currentTree.Row));

            return CheckIsVisible(treesToCheck, currentTree);
        }

        private static bool CheckIsVisibleFromBottom(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = new List<Tree>();

            treesToCheck.AddRange(trees.Where(x=> x.Column == currentTree.Column && x.Row > currentTree.Row));

            return CheckIsVisible(treesToCheck, currentTree);
        }

        private static bool CheckIsVisibleFromLeft(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = new List<Tree>();

            treesToCheck.AddRange(trees.Where(x=> x.Column < currentTree.Column && x.Row == currentTree.Row));

            return CheckIsVisible(treesToCheck, currentTree);
        }

        private static bool CheckIsVisibleFromRight(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = new List<Tree>();
            
            treesToCheck.AddRange(trees.Where(x=> x.Column > currentTree.Column && x.Row == currentTree.Row));

            return CheckIsVisible(treesToCheck, currentTree);
        }       

        private static bool CheckIsVisible(List<Tree> treesToCheck, Tree currentTree)
        {
            foreach(var tree in treesToCheck)
            {
                if(tree.Height >= currentTree.Height)
                {
                    return false;
                }                    
            }

            return true;
        }

        private static List<Tree> MapTrees(string[] treeRows){
            var trees = new List<Tree>();

            for(var r = 0; r < treeRows.Count(); r++)
            {
                var row = treeRows[r];
                char[] columns = row.ToCharArray();

                for(var c = 0; c < columns.Count(); c++){
                    var treeHeight = int.Parse(columns[c].ToString());
                    var tree = new Tree(treeHeight, r, c);
                    trees.Add(tree);
                }
            }

            return trees;
        }
    }
}