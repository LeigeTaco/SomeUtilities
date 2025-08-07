using SomeUtilities.Core;
using SomeUtilities.Helpers;
using Xunit.Abstractions;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeEnumeration_Option2 : GetEnumerationById, IXunitSerializable
{
    public GetEnumerationById_SomeEnumeration_Option2() : base(nameof(GetEnumerationById_SomeEnumeration_Option2)) => FunctionsHelper.DoNothing();

    public override int GivenId => 2;

    public override Enumeration ExpectedEnumeration => Tests.SomeEnumeration.Option2;
}
