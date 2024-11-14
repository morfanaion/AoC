// See https://aka.ms/new-console-template for more information
using Day07Puzzle01;

PuzzleDir rootDir = new PuzzleDir();
PuzzleDir currentDir = rootDir;
bool inListCommand = false;
foreach(string line in File.ReadAllLines("Input.txt"))
{
    if(IsCommand(line))
    {
        inListCommand = false;
        string[] command = line.Split(' ');
        switch(command[1])
        {
            case "cd":
                if(command[2] == "/")
                {
                    currentDir = rootDir;
                }
                else if(command[2] == "..")
                {
                    currentDir = currentDir.AncestorDir ?? rootDir;
                }
                else
                {
                    currentDir = currentDir.OfType<PuzzleDir>().SingleOrDefault(dir => dir.Name == command[2]) ?? currentDir;
                }
                break;
            case "ls":
                inListCommand = true;
                break;
        }
    }
    else
    {
        if(inListCommand)
        {
            string[] listLine = line.Split(' ');
            if(listLine[0] == "dir")
            {
                currentDir.Add(new PuzzleDir() { Name = listLine[1], AncestorDir = currentDir });
            }
            else
            {
                currentDir.Add(new PuzzleFile() { Name = listLine[1], Size = int.Parse(listLine[0]) });
            }
        }
    }
}
List<PuzzleDir> dirs = GetAllDirsFromPuzzleDir(rootDir).ToList();
Console.WriteLine(dirs.Where(dir => dir.Size <= 100000).Sum(dir => dir.Size));

bool IsCommand(string line) => line.StartsWith('$');

IEnumerable<PuzzleDir> GetAllDirsFromPuzzleDir(PuzzleDir puzzleDir)
{
    foreach(PuzzleDir subDir in puzzleDir.OfType<PuzzleDir>())
    {
        foreach(PuzzleDir dirList in GetAllDirsFromPuzzleDir(subDir))
        {
            yield return dirList;
        }
    }
    yield return puzzleDir;
}