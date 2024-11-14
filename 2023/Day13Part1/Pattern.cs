namespace Day13Part1
{
    internal class Pattern
    {
        int[] Rows;
        int[] Columns;
        public Pattern(List<string> patternDefinition)
        {
            Rows = patternDefinition.Select(GenerateNumberForRow).ToArray();
            Columns = Enumerable.Range(0, patternDefinition[0].Length).Select(i => GenerateNumberForRow(string.Join("", patternDefinition.Select(line => line[i])))).ToArray();
        }

        private int GenerateNumberForRow(string row)
        {
            int result = 0;
            foreach(char c in row)
            {
                result <<= 1;
                if(c == '#')
                {
                    result++;
                }
            }
            return result;
        }

        public int GetReflectionValue()
        {
            int result = 0;
            for(int i = 0; i < Rows.Length - 1; i++) 
            {
                if (Rows[i] == Rows[i + 1])
                {
                    bool isReflection = true;
                    for(int j = 1; i - j >= 0 && i + j + 1 < Rows.Length; j++)
                    {
                        if (Rows[i - j] != Rows[i + j + 1])
                        {
                            isReflection = false;
                            break;
                        }
                    }
                    if(isReflection)
                    {
                        result += 100 * (i + 1);
                    }
                }
            }
            for (int i = 0; i < Columns.Length - 1; i++)
            {
                if (Columns[i] == Columns[i + 1])
                {
                    bool isReflection = true;
                    for (int j = 1; i - j >= 0 && i + j + 1 < Columns.Length; j++)
                    {
                        if (Columns[i - j] != Columns[i + j + 1])
                        {
                            isReflection = false;
                            break;
                        }
                    }
                    if (isReflection)
                    {
                        result += i + 1;
                    }
                }
            }
            return result;
        }
    }
}

