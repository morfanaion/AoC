MD5 md5 = MD5.Create();
int i = 1;
string input = "iwrupvqb";
while(!Convert.ToHexString(md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input + i.ToString()))).StartsWith("00000"))
{
    i++;
}
Console.WriteLine(i);