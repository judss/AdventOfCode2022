namespace Part2
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

            var highestScore = trees.Aggregate((agg, next) => 
                next.ScenicScore > agg.ScenicScore ? next : agg);

            Console.WriteLine(JsonConvert.SerializeObject(highestScore));
            Console.WriteLine($"Answer: {JsonConvert.SerializeObject(highestScore.ScenicScore)}");
        }

        private static void checkVisibilityOfTrees(List<Tree> trees)
        {
            foreach(var tree in trees)
            {
                var treeIWant = tree.Height == 8 && tree.Row == 7 && tree.Column == 48;

                if(treeIWant)
                {
                   Console.WriteLine(JsonConvert.SerializeObject(tree)); 
                }

                tree.Top = CheckIsVisibleFromTop(tree, trees);
                tree.Bottom = CheckIsVisibleFromBottom(tree, trees);
                tree.Left = CheckIsVisibleFromLeft(tree, trees);
                tree.Right = CheckIsVisibleFromRight(tree, trees);

                if(treeIWant)
                {
                   Console.WriteLine(JsonConvert.SerializeObject(tree)); 
                }
            }
        }

        private static int CheckIsVisibleFromTop(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = trees.Where(x=> x.Column == currentTree.Column && x.Row < currentTree.Row).ToList();
            
            treesToCheck.Reverse();

            return getScoreForTree(treesToCheck, currentTree);
        }

        private static int CheckIsVisibleFromBottom(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = trees.Where(x=> x.Column == currentTree.Column && x.Row > currentTree.Row).ToList();

            return getScoreForTree(treesToCheck, currentTree);
        }

        private static int CheckIsVisibleFromLeft(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = trees.Where(x=> x.Column < currentTree.Column && x.Row == currentTree.Row).ToList();

            treesToCheck.Reverse();

            return getScoreForTree(treesToCheck, currentTree);
        }

        private static int CheckIsVisibleFromRight(Tree currentTree, List<Tree> trees)
        {
            var treesToCheck = trees.Where(x=> x.Column > currentTree.Column && x.Row == currentTree.Row).ToList();

            return getScoreForTree(treesToCheck, currentTree);
        }       

        private static int getScoreForTree(List<Tree> treesToCheck, Tree currentTree)
        {
            var total = 0;
            
            foreach(var tree in treesToCheck)
            {
                if(currentTree.Height >= tree.Height)
                {
                    total++;

                    if(currentTree.Height == tree.Height || currentTree.Height == 0){
                        return total;
                    }
                }
                else
                {
                    total++;
                    return total;
                }
            }

            return total;
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