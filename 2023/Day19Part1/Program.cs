using Day19Part1;
using System.Text.RegularExpressions;
DateTime start = DateTime.Now;
Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();
List<Part> parts = new List<Part>();
Queue<(Part part, string workflow)> queue = new Queue<(Part part, string workflow)>();

Action<string> processLine = line =>
{
    if (!string.IsNullOrEmpty(line))
    {
        Workflow workflow = new Workflow(line);
        workflows.Add(workflow.Id, workflow);
    }
    else
    {
        processLine = l =>
        {
            string[] partValues = l.Substring(1, l.Length - 2).Split(',');
            int x = 0, m = 0, a = 0, s = 0;
            foreach (string partValue in partValues)
            {
                Match match = RegexHelper.Instance.PartValueRegex().Match(partValue);
                switch (match.Groups["var"].Value)
                {
                    case "x":
                        x = int.Parse(match.Groups["value"].Value);
                        break;
                    case "m":
                        m = int.Parse(match.Groups["value"].Value);
                        break;
                    case "a":
                        a = int.Parse(match.Groups["value"].Value);
                        break;
                    case "s":
                        s = int.Parse(match.Groups["value"].Value);
                        break;
                }
            }
            Part newPart = new Part() { X = x, M = m, A = a, S = s };
            parts.Add(newPart);
            queue.Enqueue(new(newPart, "in"));
        };
    }
};

foreach(string inputLine in File.ReadAllLines("input.txt"))
{
    processLine(inputLine);
}

while(queue.Count > 0)
{
    (Part part, string workflow) itemToProcess = queue.Dequeue();
    workflows[itemToProcess.workflow].Process(itemToProcess.part, (p, w) => queue.Enqueue(new(p, w)));
}
Console.WriteLine((DateTime.Now - start).TotalMilliseconds);
Console.WriteLine(parts.Where(p => p.Accepted ?? false).Sum(p => p.XMAS));