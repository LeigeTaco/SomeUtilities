using SomeUtilities.Core;
using SomeUtilities.Helpers;
using Xunit.Abstractions;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeEnumeration_Option3 : GetEnumerationById, IXunitSerializable
{
    public GetEnumerationById_SomeEnumeration_Option3() : base(nameof(GetEnumerationById_SomeEnumeration_Option3)) => FunctionsHelper.DoNothing();

    public override int GivenId => 3;

    public override Enumeration ExpectedEnumeration => Tests.SomeEnumeration.Option3;
}
