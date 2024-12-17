const byte ADV = 0;
const byte BXL = 1;
const byte BST = 2;
const byte JNZ = 3;
const byte BXC = 4;
const byte OUT = 5;
const byte BDV = 6;
const byte CDV = 7;

string[] input = File.ReadAllLines("input.txt");
long registerA = long.Parse(input[0].Substring("Register A: ".Length));
long registerB = long.Parse(input[1].Substring("Register B: ".Length));
long registerC = long.Parse(input[2].Substring("Register C: ".Length));
byte[] program = input[4].Substring("Program: ".Length).Split(',').Select(byte.Parse).ToArray();
List<long> output = new List<long>();
int iteration = 0;
for(int i = 0; i < program.Length; i+=2)
{
    byte comboOperand = program[i + 1];
    switch (program[i])
    {
        case ADV:
            registerA /= (long)Math.Pow(2, GetComboOperandValue(comboOperand));
            break;
        case BXL:
            registerB ^= comboOperand;
            break;
        case BST:
            registerB = GetComboOperandValue(comboOperand) % 8;
            break;
        case JNZ:
            if (registerA!= 0)
            {
                i = ((int)comboOperand) - 2;
            }
            break;
        case BXC:
            registerB ^= registerC;
            break;
        case OUT:
            output.Add(GetComboOperandValue(comboOperand) % 8);
            break;
        case BDV:
            registerB = registerA / (long)Math.Pow(2, GetComboOperandValue(comboOperand));
            break;
        case CDV:
            registerC = registerA / (long)Math.Pow(2, GetComboOperandValue(comboOperand));
            break;
    }
    iteration++;
}

Console.WriteLine(string.Join(',', output));

long GetComboOperandValue(byte b)
{
    switch(b)
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