using SomeUtilities.Core;
using SomeUtilities.Helpers;
using Xunit.Abstractions;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeOtherEnumeration_Option4 : GetEnumerationById, IXunitSerializable
{
    public GetEnumerationById_SomeOtherEnumeration_Option4() : base(nameof(GetEnumerationById_SomeOtherEnumeration_Option4)) => FunctionsHelper.DoNothing();

    public override int GivenId => 4;

    public override Enumeration ExpectedEnumeration => Tests.SomeEnumeration.Option4;
}
