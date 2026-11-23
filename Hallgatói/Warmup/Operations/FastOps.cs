namespace ProfilingWarmup.Operations;

public static class FastOps
{
    public static string ConcatenateStrings(int count)
    {
        var sb = new System.Text.StringBuilder(count * 8);
        for (int i = 0; i < count; i++)
        {
            sb.Append($"Item_{i}, ");
        }
        return sb.ToString();
    }

    public static List<int> UniqueSorted(List<int> list)
    {
        var set = new HashSet<int>(list);
        var sorted = set.OrderBy(x => x).ToList();
        return sorted;
    }

    public static List<int> GeneratePrimes(int max)
    {
        var sieve = new bool[max + 1];
        var primes = new List<int>();

        for (int i = 2; i <= max; i++)
        {
            if (!sieve[i])
            {
                primes.Add(i);
                for (int j = i * 2; j <= max; j += i)
                {
                    sieve[j] = true;
                }
            }
        }

        return primes;
    }
}
