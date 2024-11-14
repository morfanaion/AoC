namespace Day08Puzzle03
{
    internal class Tree
    {
        public Tree? North { get; set; }
        public Tree? East { get; set; }
        public Tree? South { get; set; }
        public Tree? West { get; set; }

        public int  Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public bool Visible
        {
            get
            {
                return IsVisibleFrom(t => t.North) ||
                    IsVisibleFrom(t => t.East) ||
                    IsVisibleFrom(t => t.South) ||
                    IsVisibleFrom(t => t.West);
            }
        }

        public int Score
        {
            get
            {
                return NumTreesVisibleTo(t => t.North) *
                    NumTreesVisibleTo(t => t.East) *
                    NumTreesVisibleTo(t => t.South) *
                    NumTreesVisibleTo(t => t.West);
            }
        }

        private bool IsVisibleFrom(Func<Tree, Tree?> nextTree)
        {
            Tree? tree = this;
            while((tree = nextTree(tree)) != null)
            {
                if(tree.Height >= Height)
                {
                    return false;
                }
            }
            return true;
        }
    
        private int NumTreesVisibleTo(Func<Tree, Tree?> nextTree)
        {
            Tree? tree = this;
            int result = 0;
            while ((tree = nextTree(tree)) != null)
            {
                result++;
                if (tree.Height >= Height)
                {
                    break;
                }
            }
            return result;
        }
    }
}
