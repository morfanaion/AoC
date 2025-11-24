string lookAndSay = "1113222113";

for(int i = 0; i < 40; i++)
{
    string iterationResult = string.Empty;
    char lastChar = lookAndSay[0];
    int numRepeats = 1;
    foreach(char c in lookAndSay.Skip(1))
    {
        if(c != lastChar)
        {
            iterationResult += numRepeats.ToString() + lastChar;
            lastChar = c;
            numRepeats = 1;
            continue;
        }
        numRepeats++;
    }
    lookAndSay = iterationResult + numRepeats.ToString() + lastChar;
}
Console.WriteLine($"{lookAndSay.Length}");
