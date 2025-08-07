using SomeUtilities.Core;
using SomeUtilities.Helpers;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public abstract class GetEnumerationById : SerializableTestCase<Tests>
{
    protected GetEnumerationById(string testCaseName) : base(nameof(Tests.GetEnumerationById_SomeEnumeration), testCaseName) => FunctionsHelper.DoNothing();

    public abstract int GivenId { get; }

    public abstract Enumeration ExpectedEnumeration { get; }
}
