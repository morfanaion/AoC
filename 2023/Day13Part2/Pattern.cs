namespace Day13Part2
{
    internal class Pattern
    {
        private static uint[] AcceptableInclusiveOrs = new uint[]
        {
            0x00000001, 0x00000002, 0x00000004, 0x00000008, 0x00000010, 0x00000020, 0x00000040, 0x00000080,
            0x00000100, 0x00000200, 0x00000400, 0x00000800, 0x00001000, 0x00002000, 0x00004000, 0x00008000,
            0x00010000, 0x00020000, 0x00040000, 0x00080000, 0x00100000, 0x00200000, 0x00400000, 0x00800000,
            0x01000000, 0x02000000, 0x04000000, 0x08000000, 0x10000000, 0x20000000, 0x40000000, 0x80000000
        };


        uint[] Rows;
        uint[] Columns;
        public List<string> temp;
        public Pattern(List<string> patternDefinition)
        {
            Rows = patternDefinition.Select(GenerateNumberForRow).ToArray();
            Columns = Enumerable.Range(0, patternDefinition[0].Length).Select(i => GenerateNumberForRow(string.Join("", patternDefinition.Select(line => line[i])))).ToArray();
            temp = patternDefinition.ToList();
        }

        private uint GenerateNumberForRow(string row)
        {
            uint result = 0;
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
            uint exclusiveOrResult;
            for (int i = 0; i < Columns.Length - 1; i++)
            {
                exclusiveOrResult = Columns[i] ^ Columns[i + 1];
                if (exclusiveOrResult == 0 || AcceptableInclusiveOrs.Contains(exclusiveOrResult))
                {
                    int numAcceptableInclusiveOrs = exclusiveOrResult == 0 ? 0 : 1;
                    for (int j = 1; i - j >= 0 && i + j + 1 < Columns.Length; j++)
                    {
                        exclusiveOrResult = Columns[i - j] ^ Columns[i + j + 1];
                        if (exclusiveOrResult != 0)
                        {
                            if (AcceptableInclusiveOrs.Contains(exclusiveOrResult))
                            {
                                numAcceptableInclusiveOrs++;

                                if (numAcceptableInclusiveOrs > 1)
                                    break;
                            }
                            else
                            {
                                numAcceptableInclusiveOrs = 0;
                                break;
                            }
                        }
                    }
                    if (numAcceptableInclusiveOrs == 1)
                    {
                        return i + 1;
                    }
                }
            }
            for (int i = 0; i < Rows.Length - 1; i++)
            {
                exclusiveOrResult = Rows[i] ^ Rows[i + 1];
                if (exclusiveOrResult == 0 || AcceptableInclusiveOrs.Contains(exclusiveOrResult))
                {
                    int numAcceptableInclusiveOrs = exclusiveOrResult == 0 ? 0 : 1;
                    for (int j = 1; i - j >= 0 && i + j + 1 < Rows.Length; j++)
                    {
                        exclusiveOrResult = Rows[i - j] ^ Rows[i + j + 1];
                        if (exclusiveOrResult != 0)
                        {
                            if (AcceptableInclusiveOrs.Contains(exclusiveOrResult))
                            {
                                numAcceptableInclusiveOrs++;

                                if (numAcceptableInclusiveOrs > 1)
                                    break;
                            }
                            else
                            {
                                numAcceptableInclusiveOrs = 0;
                                break;
                            }
                        }
                    }
                    if (numAcceptableInclusiveOrs == 1)
                    {
                        return 100 * (i + 1);
                    }
                }
            }
            return result;
        }
    }
}

