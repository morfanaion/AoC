
namespace Day09Puzzle02
{
    internal class FileRecord
    {
        public static int IdCount = 0;

        public FileRecord(long start, long size)
        {
            Id = IdCount++;
            FileSections = [(start, size)];
        }

        public List<(long start, long size)> FileSections { get; private set; }

        public int Id { get; set; }

        internal bool MoveFile(List<(long start, long size)> emptySpaces)
        {
            (long start, long size) = FileSections.First();
            int voidIdToFill = -1;
            for (int i = 0; i < emptySpaces.Count; i++)
            {
                if (emptySpaces[i].start > start)
                {
                    break;
                }
                if (emptySpaces[i].size >= size)
                {
                    voidIdToFill = i;
                    break;
                }
            }
            if(voidIdToFill != -1)
            {
                (long emptySpaceStart, long emptySpaceSize) = emptySpaces[voidIdToFill];
                FileSections[0] = (emptySpaceStart, size);
                if(emptySpaceSize == size)
                {
                    emptySpaces.RemoveAt(voidIdToFill);
                }
                else
                {
                    emptySpaces[voidIdToFill] = (emptySpaceStart + size, emptySpaceSize - size);
                }
                return true;
            }
            return false;
        }

        public long FileChecksum => PositionIds.Sum() * Id;

        private IEnumerable<long> PositionIds
        {
            get
            {
                foreach (var section in FileSections)
                {
                    for (long startId = section.start; startId < section.start + section.size; startId++)
                    {
                        yield return startId;
                    }
                }
            }
        }
    }
}
