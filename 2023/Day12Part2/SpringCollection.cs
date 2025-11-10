namespace Day12Part1
{
    internal class SpringCollection
    {
        public string Definition { get; set; }
        public List<int> GroupSizes { get; set; }
        public long NumSolves { get; private set; }

        public SpringCollection(string line)
        {
            string[] parts = line.Split(' ');
            Definition = string.Join("?", Enumerable.Range(0, 5).Select(n => parts[0]));
            GroupSizes = string.Join(",", Enumerable.Range(0, 5).Select(n => parts[1])).Split(',').Select(int.Parse).ToList();
        }

        public void FindSolves()
        {
            NumSolves = GenerateValidSolutions(Definition).Distinct().Count();
        }

        public List<string> attempts = new List<string>();

        private IEnumerable<string> GenerateValidSolutions(string definition, int currentGroupIndex = 0, int currentSearchIndex = 0, int currentGroupSize = 0)
        {
            int firstOccurrence = definition.IndexOf('?');
            if (firstOccurrence == -1)
            {
                attempts.Add(definition);
                if (IsValidSolution(definition))
                {
                    yield return definition;
                }
            }
            else
            {
                for (int x = 0; x < definition.Length; x++)
                {
                    bool cancel = false;
                    switch (definition[x])
                    {
                        case '.':
                            if (currentGroupSize > 0)
                            {
                                if (GroupSizes.Count > currentGroupIndex && GroupSizes[currentGroupIndex] == currentGroupSize)
                                {
                                    // valid group, do continue, but look at the next group
                                    currentGroupIndex++;
                                    currentGroupSize = 0;
                                }
                                else
                                {
                                    // we cannot possibly get a valid result anymore, break the whole loop
                                    cancel = true;
                                    break;
                                }
                            }
                            break;
                        case '#':
                            currentGroupSize++;
                            break;
                        case '?':
                            // generate the options
                            foreach (var option in GenerateValidSolutions(GetStringForOption(definition, x, '.')))
                            {
                                yield return option;
                            }
                            if (currentGroupIndex < GroupSizes.Count && currentGroupSize < GroupSizes[currentGroupIndex])
                            {
                                foreach (var option in GenerateValidSolutions(MakeCompletedGroupDefinition(definition, x, currentGroupSize, GroupSizes[currentGroupIndex])))
                                {
                                    yield return option;
                                }
                            }
                            cancel = true; // this should have searched beyond already, so this should be done...
                            break;
                    }
                    if (cancel)
                    {
                        break;
                    }
                }
            }

        }

        private string MakeCompletedGroupDefinition(string definition, int x, int currentGroupSize, int maxSize)
        {
            StringBuilder sb = new StringBuilder(definition);
            int numToPlace = maxSize - currentGroupSize;
            int i;
            for(i = 0; i < numToPlace ; i++)
            {
                if (x + i >= definition.Length || sb[x+i] != '?')
                { 
                    break; 
                }
                sb[x + i] = '#';
            }
            if (x + i < definition.Length && sb[x+i] == '?')
            {
                sb[x + i] = '.';
            }
            return sb.ToString();
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
