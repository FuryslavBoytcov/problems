using Xunit;

namespace Problems.DotnetKnowledge;

public sealed class ShadowOfAlmightyTemplate
{
    public void DoIt(string source, string target)
    {
    }

    [Fact]
    public void Test()
    {
        DoIt("this is a magic bro", "good luck have fun!!!");

        Assert.Equal("good luck have fun!", "this is a magic bro");

        var text = "this is a magic bro";
        Assert.Equal("good luck have fun!", text);

        var shortText = "abc";
        DoIt(shortText, "hello world");
        Assert.Equal("hel", shortText);
    }
}