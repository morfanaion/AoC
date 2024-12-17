const byte ADV = 0;
const byte BXL = 1;
const byte BST = 2;
const byte JNZ = 3;
const byte BXC = 4;
const byte OUT = 5;
const byte BDV = 6;
const byte CDV = 7;

string[] input = File.ReadAllLines("input.txt");
long registerA = 0;
long registerB = 0;
long registerC = 0;
byte[] program = input[4].Substring("Program: ".Length).Split(',').Select(byte.Parse).ToArray();
byte[] expectedOutput = [4, 1, 3, 0];

int currentInstructionIdx = program.Length - 2;
int idxcounter = 0;
int loopbackIdx = program.Select(instr => (idxcounter++, instr)).Zip(GetAlternatingBooleans(true, program.Length)).Where(pair => pair.Second).Select(pair => pair.First).First(pair => pair.instr == JNZ).Item1;
int i = expectedOutput.Length - 1;
int iteration = 31;
while(i >= 0 || currentInstructionIdx >= 0)
{
    switch(program[currentInstructionIdx])
    {
        case ADV:
            registerA *= (long)Math.Pow(2, GetComboOperandValue(program[currentInstructionIdx + 1]));
            break;
        case BXL:
            // we need the smallest number we could get here, always assume that the 8 bit is on
            registerB ^= program[currentInstructionIdx + 1];
            break;
        case BST:
            switch (program[currentInstructionIdx + 1])
            {
                case 4:
                    registerA = registerA / 8 * 8 + registerB;
                    break;
                case 5:
                    registerB += registerB / 8 * 8 + registerB;
                    break;
                case 6:
                    registerC += registerC / 8 * 8 + registerB; ;
                    break;
                default:
                    //ignore anything other than these 3. They are not important, because they do not impact the output at all
                    break;
            }
            break;
        case JNZ:
            // we don't do anything here. We're working backwards, meaning that the first time we get here, we didn't do anything, in the loop, we will skip this one
            break;
        case BXC:
            registerB ^= registerC;
            if(iteration == 51)
            {
                registerB = 4;
            }
            break;
        case OUT:
            switch(program[currentInstructionIdx + 1])
            {
                case 4:
                    registerA = registerA / 8 * 8 + expectedOutput[i];
                    break;
                case 5:
                    registerB = registerB / 8 * 8 + expectedOutput[i];
                    break;
                case 6:
                    registerC = registerC / 8 * 8 + expectedOutput[i];
                    break;
                default:
                    //ignore anything other than these 3. They are not important, because they do not impact the output at all
                    break;
            }
            i--;
            break;
        case BDV:
            registerA = Math.Max(registerB * (long)Math.Pow(2, GetComboOperandValue(program[currentInstructionIdx + 1])), registerA);
            break;
        case CDV:
            registerA = Math.Max(registerC * (long)Math.Pow(2, GetComboOperandValue(program[currentInstructionIdx + 1])), registerA);
            registerC = registerA / (long)Math.Pow(2, GetComboOperandValue(program[currentInstructionIdx + 1]));
            break;
    }
    if(currentInstructionIdx == 0 && i >= 0)
    {
        currentInstructionIdx = loopbackIdx;
        iteration--;
    }
    currentInstructionIdx -= 2;
    iteration--;
}

Console.WriteLine(registerA);

long GetComboOperandValue(byte b)
{
    switch (b)
    {
        case 4:
            return registerA;
        case 5:
            return registerB;
        case 6:
            return registerC;
        case 7:
            throw new ArgumentException("Value 7 is reserved");
        default:
            return b;
    }
}

IEnumerable<bool> GetAlternatingBooleans(bool first, int count)
{
    bool previous = !first;
    for(int i = 0; i< count; i++)
    {
        yield return previous = !previous;
    }
}