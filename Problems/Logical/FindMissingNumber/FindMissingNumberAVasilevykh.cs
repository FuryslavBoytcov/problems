using Xunit;

namespace Problems.Logical.FindMissingNumber;

public sealed class FindMissingNumberAVasilevykh
{
   public static int FindMissingNumber(IReadOnlyCollection<int> source)
   {
      int currentArraySum = source.Sum();
      int sum = (source.First()+source.Last())*(source.Count+1)/2;
      return sum-currentArraySum;
   }
   
   [Fact]
   public void Test()
   {
      Assert.Equal(4, FindMissingNumber(new[] {0, 1, 2, 3, 5, 6}));
      Assert.Equal(3, FindMissingNumber(new[] {0, 1, 2, 4, 5}));
      Assert.Equal(6, FindMissingNumber(new[] {0, 1, 2, 3, 4, 5, 7}));
   }
}