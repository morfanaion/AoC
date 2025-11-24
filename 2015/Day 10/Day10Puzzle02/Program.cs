using Day10Puzzle02;

string lookAndSay = "1113222113";

Dictionary<string, Element> elementDictionary = File.ReadAllLines("elements.txt").Select(Element.FromString).ToDictionary(e => e.ElementName);
Element[] currentElements = elementDictionary.Values.Where(e => e.Representation == lookAndSay).ToArray();

for(int i = 0; i < 50; i++)
{
    currentElements = currentElements.SelectMany(e => e.DecayResult.Select(dr => elementDictionary[dr])).ToArray();
}
Console.WriteLine(currentElements.Sum(e => e.RepresentationLength));
