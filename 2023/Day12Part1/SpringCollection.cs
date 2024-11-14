using System.Text;

namespace Day12Part1
{
    internal class SpringCollection
    {
        public string Definition { get; set; }
        public List<int> GroupSizes { get; set; }
        public int NumSolves { get; private set; }

        public SpringCollection(string line)
        {
            string[] parts = line.Split(' ');
            Definition = parts[0];
            GroupSizes = parts[1].Split(',').Select(int.Parse).ToList();
        }

        public void FindSolves()
        {
            NumSolves = GetAllOptions(Definition).Count(IsValidSolution);
        }

        private bool IsValidSolution(string arg)
        {
            List<int> groups = new List<int>();
            int groupSize = 0;
            foreach(char c in arg)
            {
                switch(c)
                {
                    case '.':
                        if(groupSize > 0)
                        {
                            groups.Add(groupSize);
                            groupSize = 0;
                        }
                        break;
                    case '#':
                        groupSize++;
                        break;
                }
            }
            if (groupSize > 0)
            {
                groups.Add(groupSize);
                groupSize = 0;
            }
            if(groups.Count != GroupSizes.Count) 
            {
                return false; 
            }
            for(int i = 0; i <  GroupSizes.Count; i++)
            {
                if (groups[i] != GroupSizes[i])
                {
                    return false;
                }
            }
            return true;
        }

        private IEnumerable<string> GetAllOptions(string definition)
        {
            int firstOccurrence = definition.IndexOf('?');
            if (firstOccurrence == -1)
            {
                yield return definition;
            }
            else
            {
                foreach (string option in GetAllOptions(GetStringForOption(definition, firstOccurrence, '.')))
                {
                    yield return option;
                }
                foreach (string option in GetAllOptions(GetStringForOption(definition, firstOccurrence, '#')))
                {
                    yield return option;
                }
            }

        }

        private string GetStringForOption(string definition, int firstOccurrence, char v)
        {
            StringBuilder result = new StringBuilder(definition);
            result[firstOccurrence] = v;
            return result.ToString();
        }
    }
}
