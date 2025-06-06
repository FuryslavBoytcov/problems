namespace Problems.Logical.FindMissingNumber;

public sealed class FindMissingNumberBzhemba
{
    public static int FindMissingNumber(IReadOnlyCollection<int> source)
    {
        var missing = 0;
        var i = 0;

        foreach (var num in source)
        {
            missing ^= num ^ i;
            i++;
        }

        missing ^= i;

        return missing;
    }
}