using LuaEngine.Scripts.Tests.Helpers;

namespace Prism.Core.Tests.Helpers;

public static class RandomGenerator
{
    private const string Chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzАаБбВвГгДдЕеЖжЗзИиКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя0123456789-.\":()";
    private const int MaxStringLength = 10;

    public static string GenerateString(Random sporadic)
    {
        return new string(Enumerable.Repeat(Chars, sporadic.Next(1, MaxStringLength)).Select(s => s[sporadic.Next(s.Length)]).ToArray());
    }

    public static DateTime GenerateDate(Random sporadic, int minYear, int maxYear)
    {
        if (minYear >= maxYear)
            (minYear, maxYear) = (maxYear, minYear);

        return new DateTime(
            sporadic.Next(minYear, maxYear),
            sporadic.Next(1, 13),
            sporadic.Next(1, 29),
            23,
            59,
            59);
    }

    public static IEnumerable<string> GenerateCollectionOfUniqueStrings(Random sporadic, int? collectionCount = null)
    {
        int count = collectionCount.HasValue ? collectionCount.Value : sporadic.Next() + 1;
        HashSet<string> strs = new HashSet<string>();
        for (int i = 0; i < count; i++)
        {
            string str = GenerateString(sporadic);
            if (!strs.Add(str))
                i--;
        }

        return strs.ToList();
    }

    public static IEnumerable<DateTime> GenerateCollectionOfUniqueDates(Random sporadic, int minYear, int maxYear, int? collectionCount = null)
    {
        int count = collectionCount.HasValue ? collectionCount.Value : sporadic.Next() + 1;
        HashSet<DateTime> dateTimes = new HashSet<DateTime>();
        for (int i = 0; i < count; i++)
        {
            DateTime date = GenerateDate(sporadic, minYear, maxYear);
            if (!dateTimes.Add(date))
                i--;
        }
        return dateTimes.ToList();
    }

    public static Result<T> GenerateEnumValue<T>(Random sporadic) where T : struct, Enum
    {
        var values = Enum.GetValues(typeof(T));
        if (values.Length <= 0)
            return Result<T>.Empty;

        T randomValue = (T)values.GetValue(sporadic.Next(values.Length))!;
        return randomValue;
    }

    public static Guid GenerateGuid(Random sporadic)
    {
        var guidBytes = new byte[16];

        sporadic.NextBytes(guidBytes);

        return new Guid(guidBytes);
    }

    public static bool GenerateBool(Random sporadic)
    {
        return sporadic.Next(2) == 1;
    }

    public static Result<T> PickOne<T>(Random sporadic, IEnumerable<T> values) =>
        PickOne(sporadic, values.ToArray());

    public static Result<T> PickOne<T>(Random sporadic, params T[] values)
    {
        var count = values.Length;
        if (count <= 0)
            return Result<T>.Empty;

        return values.ElementAt(sporadic.Next(0, count));
    }
}
