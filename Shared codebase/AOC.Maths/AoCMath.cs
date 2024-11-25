using System.Numerics;

namespace AOC.Maths
{
  public static class AoCMath
  {
    public static T Max<T>(params T[] numbers) where T : struct, INumber<T> => numbers.Max();
    public static T Min<T>(params T[] numbers) where T : struct, INumber<T> => numbers.Min();
    public static T Sum<T>(params T[] numbers) where T : struct, INumber<T> => numbers.Sum();
    public static T Sum<T>(this IEnumerable<T> numbers) where T : struct, INumber<T>
    {
      T result = T.Zero;
      foreach (T number in numbers)
      {
        checked { result += T.CreateChecked(number); }
      }
      return result;
    }

    public static T Average<T>(params T[] numbers) where T : struct, INumber<T> => numbers.Average();
    public static T Average<T>(this IEnumerable<T> numbers) where T : struct, INumber<T>
    {
      switch (Type.GetTypeCode(typeof(T)))
      {
        case TypeCode.Char:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.SByte:
          return AverageInteger(numbers);
        case TypeCode.Byte:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          return AverageIntegerUnsigned(numbers);
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return AverageFloating(numbers);
        default:
          throw new ArgumentException("Supplied numbertype is not supported");
      }
    }

    private static T AverageFloating<T>(IEnumerable<T> numbers) where T : struct, INumber<T>
    {
      if (!numbers.Any())
      {
        return T.Zero;
      }
      T result = numbers.First();
      T numItems = T.One;
      foreach (T number in numbers.Skip(1))
      {
        numItems++;
        result += (number - result) / numItems;
      }
      return result;
    }

    private static T AverageInteger<T>(IEnumerable<T> numbers) where T : struct, INumber<T>
    {
      if (!numbers.Any())
      {
        return T.Zero;
      }
      T result = numbers.First();
      T numItems = T.One;
      T remainder = T.Zero;
      foreach (T number in numbers.Skip(1))
      {
        T currentResult = result;
        numItems++;
        result += (number - currentResult + remainder) / numItems;
        remainder = (number - currentResult + remainder) % numItems;
        if (remainder < T.Zero)
        {
          result--;
          remainder += numItems;
        }
      }
      return result;
    }

    private static T AverageIntegerUnsigned<T>(IEnumerable<T> numbers) where T : struct, INumber<T>
    {
      if (!numbers.Any())
      {
        return T.Zero;
      }
      T result = numbers.First();
      T numItems = T.One;
      T remainder = T.Zero;
      foreach (T number in numbers.Skip(1))
      {
        if (number > result)
        {
          T currentResult = result;
          numItems++;
          result += (number - currentResult + remainder) / numItems;
          remainder = (number - currentResult + remainder) % numItems;
        }
        else
        {
          T currentResult = result;
          numItems++;
          result -= (currentResult - number - remainder) / numItems;
          remainder = numItems - ((currentResult - number - remainder) % numItems);
          if (numItems == remainder)
          {
            remainder = T.Zero;
          }
          else
          {
            result--;
          }
        }
      }
      return result;
    }

    public static T Median<T>(params T[] numbers) where T : struct, INumber<T>
    {
      (int middle, int remainder) = Math.DivRem(numbers.Length, 2);
      if (remainder == 0)
      {
        return numbers.Order().Skip(middle - 1).Take(2).Average();
      }
      return numbers.Order().Skip(middle).First();
    }

    public static T Median<T>(this IEnumerable<T> numbers) where T : struct, INumber<T>
    {
      bool onEven = true;
      Queue<T> queue = new Queue<T>();
      foreach (T number in numbers.OrderBy(n => n))
      {
        onEven = !onEven;
        if (!onEven && queue.Count != 0)
        {
          _ = queue.Dequeue();
        }
        queue.Enqueue(number);
      }
      if (onEven)
      {
        T number1 = queue.Dequeue();
        T number2 = queue.Dequeue();
        return Average(number1, number2);
      }
      return queue.Dequeue();
    }
  }
}
