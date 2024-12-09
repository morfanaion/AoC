
namespace Day09Puzzle01
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
            if(emptySpaces.Count == 1)
            {
                // given the premises, we can only have empty spaces when we have started moving files. Since we start at the back, if we have just 1 big empty space left, it's at the end, we can't move anymore
                return false;
            }
            (long currentStart, long currentSize) = FileSections.First();
            FileSections.Clear();
            (long lastEmptySpaceStart, long lastEmptySpaceSize) = (currentStart, currentSize);
            if (emptySpaces[^1].start > currentStart)
            {
                // there is empty space behind me, I should become a part of that space
                (_, lastEmptySpaceSize) = emptySpaces[^1];
                lastEmptySpaceSize += currentSize;
                emptySpaces[^1] = (lastEmptySpaceStart, lastEmptySpaceSize);
            }
            else
            {
                // no empty space after me, I'm leaving an empty space there
                emptySpaces.Add((lastEmptySpaceStart, lastEmptySpaceSize));
            }
            (lastEmptySpaceStart, lastEmptySpaceSize) = emptySpaces[^2];
            if (lastEmptySpaceStart + lastEmptySpaceSize == currentStart)
            {
                // there is empty space before me. Join the last two empty spaces together
                emptySpaces[^2] = (emptySpaces[^2].start, emptySpaces[^1].size + emptySpaces[^2].size);
                emptySpaces.RemoveAt(emptySpaces.Count - 1);
            }
            
            while (currentSize > 0)
            {
                (long emptySpaceStart, long emptySpaceSize) = emptySpaces[0];
                if (emptySpaceSize > currentSize)
                {
                    FileSections.Add((emptySpaceStart, currentSize));
                    emptySpaces[0] = (emptySpaceStart + currentSize, emptySpaceSize - currentSize);
                    currentSize = 0;
                }
                else
                {
                    FileSections.Add((emptySpaceStart, emptySpaceSize));
                    emptySpaces.RemoveAt(0);
                    currentSize -= emptySpaceSize;
                }
            }
            return true;
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
