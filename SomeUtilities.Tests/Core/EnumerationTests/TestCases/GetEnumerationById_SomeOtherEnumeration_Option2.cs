using SomeUtilities.Core;
using SomeUtilities.Helpers;
using Xunit.Abstractions;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeOtherEnumeration_Option2 : GetEnumerationById, IXunitSerializable
{
    public GetEnumerationById_SomeOtherEnumeration_Option2() : base(nameof(GetEnumerationById_SomeOtherEnumeration_Option2)) => FunctionsHelper.DoNothing();

    public override int GivenId => 2;

    public override Enumeration ExpectedEnumeration => Tests.SomeOtherEnumeration.Option2;
}
