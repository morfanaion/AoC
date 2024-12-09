// See https://aka.ms/new-console-template for more information
using Day09Puzzle01;

string input = File.ReadAllText("input.txt");
List<FileRecord> records = new List<FileRecord>();
List<(long start, long size)> emptySpaces = new List<(long start, long size)>();
long currentIdx = 0;
for(int i =0; i < input.Length; i+=2)
{
    long size = input[i] - '0';
    records.Add(new FileRecord(currentIdx, size));
    currentIdx += size;
    if(i + 1 >= input.Length)
    {
        continue;
    }
    size = input[i + 1] - '0';
    if (size != 0)
    {
        emptySpaces.Add((currentIdx, size));
        currentIdx += size;
    }
}
records.Reverse();

foreach(FileRecord record in records)
{
    if (!record.MoveFile(emptySpaces))
    {
        break;
    }
}

Console.WriteLine(records.Sum(r => r.FileChecksum));