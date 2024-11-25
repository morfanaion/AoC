using System.Numerics;

namespace AOC.Maths
{
	public class IntegerMath<T> where T : struct, IBinaryInteger<T>, IMinMaxValue<T>
	{
		private static List<T> _primeNumbers = new List<T>();
		public static IEnumerable<T> PrimeNumbers
		{
			get
			{
				T lastValue = T.One;
				foreach (T primeNumber in _primeNumbers)
				{
					yield return lastValue = primeNumber;
				}
				while(lastValue != T.MaxValue)
				{
					bool isPrime = true;
					lastValue++;
					foreach (T primeNumber in _primeNumbers)
					{
						T squared = primeNumber * primeNumber;
						if(primeNumber > squared)
						{
							break;
						}
						if(squared > lastValue)
						{
							break;
						}
						if(lastValue % primeNumber == T.Zero)
						{
							isPrime = false;
						}
					}
					if(isPrime)
					{
						_primeNumbers.Add(lastValue);
						yield return lastValue;
					}
				}
			}
		}
	}
}
