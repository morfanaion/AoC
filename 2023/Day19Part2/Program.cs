using Day19Part2;
DateTime start = DateTime.Now;
foreach (string line in File.ReadAllLines("input.txt"))
{
    if (!string.IsNullOrEmpty(line))
    {
        Workflow workflow = new Workflow(line);
        Workflow.Workflows.Add(workflow.Id, workflow);
    }
    else
    {
        break;
    }
}

Console.WriteLine(Workflow.Workflows["in"].GetNumAccepted(WorkingRange.Complete));
Console.WriteLine((DateTime.Now - start).TotalMilliseconds);