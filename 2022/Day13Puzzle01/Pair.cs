namespace Day13Puzzle01
{
    internal class Pair
    {
        public List<object>? Left { get; set; } = null;
        public List<object>? Right { get; set; } = null;

        public bool IsRightOrder => CompareLeftToRight(Left ?? new List<object>(), Right ?? new List<object>()) != 1;

        private int CompareLeftToRight(List<object> left, List<object> right)
        {
            int listSize = Math.Max(left.Count, right.Count);
            for(int i = 0; i < listSize; i++)
            {
                if(i == left.Count)
                {
                    return -1;
                }
                if(i == right.Count)
                {
                    return 1;
                }
                if(left[i] is int iLeft && right[i] is int iRight)
                {
                    if(iLeft < iRight)
                    {
                        return -1;
                    }
                    if(iLeft > iRight)
                    {
                        return 1;
                    }
                }
                if(left[i] is List<object> subLeft && right[i] is List<object> subRight)
                {
                    switch (CompareLeftToRight(subLeft, subRight))
                    {
                        case -1:
                           return -1;
                        case 1:
                            return 1;
                    }
                }
                if(left[i] is List<object> subLeft2 && right[i] is int iRight2)
                {
                    switch (CompareLeftToRight(subLeft2, new List<object>() { iRight2 }))
                    {
                        case -1:
                            return -1;
                        case 1:
                            return 1;
                    }
                }
                if(left[i] is int iLeft2 && right[i] is List<object> subRight2)
                {
                    switch (CompareLeftToRight(new List<object>() { iLeft2 }, subRight2))
                    {
                        case -1:
                           return -1;
                        case 1:
                            return 1;
                    }
                }
            }
            return 0;
        }
    }
}
