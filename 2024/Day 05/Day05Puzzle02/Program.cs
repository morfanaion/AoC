using Day05Puzzle01;

int sum = 0;
List<OrderRule> orderRules = new List<OrderRule>();
Action<string> theAction = str =>
{
    if (!string.IsNullOrEmpty(str))
    {
        orderRules.Add(OrderRule.FromString(str));
    }
    else
    {
        theAction = str2 =>
        {
            int[] update = str2.Split(',').Select(s => int.Parse(s)).ToArray();
            if (orderRules.Any(rule => !rule.RuleAppliesCorrectly(update)))
            {
                while(orderRules.Any(rule => !rule.RuleAppliesCorrectly(update)))
                {
                    foreach (OrderRule rule in orderRules.Where(rule => !rule.RuleAppliesCorrectly(update)))
                    {
                        rule.ApplyRule(ref update);
                    }
                }
                sum += update[update.Length / 2];
            }
        };
    }
};
foreach(string str3 in File.ReadAllLines("input.txt"))
{
    theAction(str3);
}
Console.WriteLine(sum);