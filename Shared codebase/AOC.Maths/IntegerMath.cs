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

        public static T FindGCD(T a, T b)
        {
            if (b == T.Zero)
            {
                return a;
            }
            return FindGCD(b, a % b);
        }

        public static T FindLCM(IEnumerable<T> numbers)
        {
            T result = numbers.First();
            foreach(T number in numbers.Skip(1))
            {
                result = number * result / FindGCD(number, result);
            }
            return result;
        }

		public static T FindLCM(params T[] numbers) => FindLCM((IEnumerable<T>)numbers);
    }
}
