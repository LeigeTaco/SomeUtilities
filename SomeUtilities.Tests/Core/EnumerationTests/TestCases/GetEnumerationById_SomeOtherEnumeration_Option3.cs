using SomeUtilities.Core;
using SomeUtilities.Helpers;
using Xunit.Abstractions;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeOtherEnumeration_Option3 : GetEnumerationById, IXunitSerializable
{
    public GetEnumerationById_SomeOtherEnumeration_Option3() : base(nameof(GetEnumerationById_SomeOtherEnumeration_Option3)) => FunctionsHelper.DoNothing();

    public override int GivenId => 3;

    public override Enumeration ExpectedEnumeration => Tests.SomeEnumeration.Option3;
}
