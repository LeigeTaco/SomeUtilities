using SomeUtilities.Core;
using SomeUtilities.Helpers;
using SomeUtilities.Testing.Abstraction;

namespace SomeUtilities.Tests.NET8.Core.EnumerationTests.TestCases;

public abstract class GetEnumerationById : TestCase<Tests>
{
    protected GetEnumerationById(string testCaseName) : base(nameof(Tests.GetEnumerationById), testCaseName) => FunctionsHelper.DoNothing();

    public abstract int GivenId { get; }

    public abstract Enumeration ExpectedEnumeration { get; }
}
