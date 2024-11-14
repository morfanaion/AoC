using Day25Part1;

Dictionary<string, Component> _components = new Dictionary<string, Component>();
foreach (string str in File.ReadAllLines("input.txt"))
{
    string[] parts = str.Split(':');
    string componentName = parts[0].Trim();
    if (!_components.TryGetValue(componentName, out Component? component))
    {
        component = new Component(componentName);
        _components.Add(componentName, component);
    }
    string[] connectedComponents = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    foreach (string sub in connectedComponents)
    {
        if (!_components.TryGetValue(sub.Trim(), out Component? subComponent))
        {
            subComponent = new Component(sub.Trim());
            _components.Add(sub.Trim(), subComponent);
        }
        subComponent.ConnectedComponents.Add(component);
        component.ConnectedComponents.Add(subComponent);
    }
}

Dictionary<string, List<Component>> Paths = new Dictionary<string, List<Component>>();
List<Component> components = _components.Values.ToList();

for (int i = 0; i < components.Count; i++)
{
    Component navigationStart = components[i];
    foreach (Component component in components.Skip(i + 1))
    {
        DijkstraTo(navigationStart, component);
    }
}
var AllPaths = Paths.Values.SelectMany(GetPathSegments).ToList().GroupBy(k => k).OrderByDescending(g => g.Count());
string[] removedPaths = AllPaths.Take(3).Select(g => g.Key).ToArray();

List<string> AllComponents = _components.Values.Select(c => c.Id).ToList();
List<string> group1 = new List<string>();
Component start = _components.First().Value;
Queue<Component> queue = new Queue<Component>();
queue.Enqueue(start);
AllComponents.Remove(start.Id);
while (queue.Count > 0)
{
    Component current = queue.Dequeue();
    MoveComponentToGroup1(current, queue.Enqueue, removedPaths, group1, AllComponents);
}
Console.WriteLine(AllComponents.Count * group1.Count);

void MoveComponentToGroup1(Component component, Action<Component> addToQueue, string[] pathKeys, List<string> group1, List<string> allComponents)
{
    group1.Add(component.Id);
    foreach (Component sub in component.ConnectedComponents)
    {
        if (ConnectionNotInPermutation(pathKeys, sub.Id, component.Id))
        {
            if (allComponents.Contains(sub.Id))
            {
                allComponents.Remove(sub.Id);
                addToQueue(sub);
            }
        }
    }
}

bool ConnectionNotInPermutation(string[] pathkeys, string id1, string id2)
{
    return !pathkeys.Contains(GetKey(id1, id2));
}

IEnumerable<string> GetPathSegments(List<Component> list)
{
    foreach ((Component First, Component Second) in list.Skip(1).Zip(list))
    {
        yield return GetKey(First.Id, Second.Id);
    }
}

void DijkstraTo(Component startComponent, Component endComponent)
{
    if (Paths.ContainsKey(GetKey(startComponent.Id, endComponent.Id)))
    {
        return;
    }
    List<string> componentsToVisit = components.Select(c => c.Id).ToList();
    componentsToVisit.Remove(startComponent.Id);
    PriorityQueue<Step, int> queue = new PriorityQueue<Step, int>();
    queue.Enqueue(new Step(null, startComponent, 0), 0);
    while (queue.Count > 0)
    {
        Step step = queue.Dequeue();
        foreach (Step nextStep in step.CurrentComponent.GetSteps(step.StepNumber, step))
        {
            if (componentsToVisit.Contains(nextStep.CurrentComponent.Id))
            {
                componentsToVisit.Remove(nextStep.CurrentComponent.Id);
                Step? currentStep = nextStep;
                string nextComponentId = currentStep.CurrentComponent.Id;
                while (currentStep.Previous != null)
                {
                    currentStep = currentStep.Previous;
                    string key = GetKey(currentStep.CurrentComponent.Id, nextComponentId);
                    if (!Paths.ContainsKey(key))
                    {
                        List<Component> path = nextStep.GetPath(currentStep.CurrentComponent.Id).ToList();
                        Paths.Add(key, path);
                    }
                }
                queue.Enqueue(nextStep, nextStep.StepNumber);
            }
            if (nextStep.CurrentComponent.Id == endComponent.Id)
            {
                return;
            }
        }
    }
}

string GetKey(params string[] strings)
{
    return string.Join("", strings.Order());
}