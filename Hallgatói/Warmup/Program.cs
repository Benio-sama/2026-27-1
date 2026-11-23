using ProfilingWarmup.Operations;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {

        var sw = new Stopwatch();
        // 1️. Sztring összefűzés lassú módon
        sw.Start();
        var slowConcat = SlowOps.ConcatenateStrings(5000);
        sw.Stop();
        Console.WriteLine($"Lassú sztring összefűzés: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var fastConcat = FastOps.ConcatenateStrings(5000);
        sw.Stop();
        Console.WriteLine($"Gyors sztring összefűzés: {sw.ElapsedMilliseconds} ms\n");

        // 2️. Lista rendezés és duplikátum eltávolítás
        Random rnd = new Random();
        var numbers = Enumerable.Range(0, 100_000).Select(i => rnd.Next(0,5000)).ToList();

        sw.Restart();
        var slowUnique = SlowOps.UniqueSorted(numbers);
        sw.Stop();
        Console.WriteLine($"Lassú UniqueSorted: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var fastUnique = FastOps.UniqueSorted(numbers);
        sw.Stop();
        Console.WriteLine($"Gyors UniqueSorted: {sw.ElapsedMilliseconds} ms\n");

        // 3️. Prímszám generálás
        sw.Restart();
        var slowPrimes = SlowOps.GeneratePrimes(20000);
        sw.Stop();
        Console.WriteLine($"Lassú prímszám generálás: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var fastPrimes = FastOps.GeneratePrimes(20000);
        sw.Stop();
        Console.WriteLine($"Gyors prímszám generálás: {sw.ElapsedMilliseconds} ms\n");
    }
}