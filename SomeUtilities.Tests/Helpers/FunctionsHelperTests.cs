using System.Linq;
using SomeUtilities.Helpers;
using SomeUtilities.Testing.Abstraction;
using Xunit;

namespace SomeUtilities.Tests.Helpers;

public sealed class FunctionsHelperTests : UnitTest
{
    [Fact]
    public void DoNothing_WithoutParams()
    {
        Assert.Null(Record.Exception(FunctionsHelper.DoNothing));
    }

    [Fact]
    public void DoNothing_WithParams()
    {
        Assert.Null(Record.Exception(() => FunctionsHelper.DoNothing(Enumerable.Range(1, 1000).ToArray())));
    }
}
