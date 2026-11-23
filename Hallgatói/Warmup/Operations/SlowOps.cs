namespace ProfilingWarmup.Operations;

public static class SlowOps
{
    public static string ConcatenateStrings(int count)
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            result += $"Item_{i}, ";
        }
        return result;
    }

    public static List<int> UniqueSorted(List<int> list)
    {
        var copy = new List<int>();
        foreach (var item in list)
        {
            if (!copy.Contains(item))
                copy.Add(item);
        }
        copy.Sort();
        return copy;
    }

    public static List<int> GeneratePrimes(int max)
    {
        var primes = new List<int>();
        for (int i = 2; i <= max; i++)
        {
            bool isPrime = true;
            for (int j = 2; j < i; j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
                primes.Add(i);
        }
        return primes;
    }
}
