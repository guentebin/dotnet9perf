//read file from root excute path
using System.Diagnostics;
using System.Text.RegularExpressions;

string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pride and Prejudice.txt");
string text = File.ReadAllText(path);

Dictionary<string, int> frequency = [];
var lookup = frequency.GetAlternateLookup<ReadOnlySpan<char>>();

Stopwatch sw = new();
while (true)
{
    long mem = GC.GetTotalAllocatedBytes();
    sw.Restart();

    Regex regexWord = new(@"\b\w+\b");

    Regex regexSpace = new(@"\s+");

    for (var trial = 0; trial < 10; trial++)
    {
        //foreach (Match match in regex.Matches(text))
        //{
        //    string word = match.Value;
        //    frequency[word] = frequency.TryGetValue(word, out int count) ? count + 1 : 1;
        //}

        //foreach (var m in regex.EnumerateMatches(text))
        //{
        //    string word = text.Substring(m.Index,m.Length);
        //    frequency[word] = frequency.TryGetValue(word, out int count) ? count + 1 : 1;
        //}

        //foreach (var m in regexWord.EnumerateMatches(text))
        //{
        //    var word = text.AsSpan(m.Index, m.Length);
        //    lookup[word] = lookup.TryGetValue(word, out int count) ? count + 1 : 1;
        //}

        //foreach (var m in regexSpace.Split(text))
        //{
        //    var word = m;
        //    lookup[word] = lookup.TryGetValue(word, out int count) ? count + 1 : 1;
        //}

        //foreach (var m in regexSpace.EnumerateSplits(text))
        //{
        //    var word = text[m];
        //    lookup[word] = lookup.TryGetValue(word, out int count) ? count + 1 : 1;
        //}

        //foreach (var m in regexSpace.EnumerateSplits(text))
        //{
        //    var word = text.AsSpan(m);
        //    lookup[word] = lookup.TryGetValue(word, out int count) ? count + 1 : 1;
        //}

        foreach (var m in text.AsSpan().Split(' '))
        {
            var word = text.AsSpan(m);
            lookup[word] = lookup.TryGetValue(word, out int count) ? count + 1 : 1;
        }
    }
    sw.Stop();
    mem = GC.GetTotalAllocatedBytes() - mem;

    Console.WriteLine($"Time: {sw.Elapsed.TotalMilliseconds}ms, Memory: {mem / 1024.0 / 1024:N2}mb");
}

static partial class Helpers
{
    [GeneratedRegex(@"\b\w+\b")]
    public static partial Regex Words();
    public static void Use<T>(T value) { }
}
