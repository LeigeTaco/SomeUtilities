using SomeUtilities.Core;
using SomeUtilities.Helpers;

namespace SomeUtilities.Tests.NET8.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeEnumeration_Option1 : GetEnumerationById
{
    public GetEnumerationById_SomeEnumeration_Option1() : base(nameof(GetEnumerationById_SomeEnumeration_Option1)) => FunctionsHelper.DoNothing();

    public override int GivenId => 1;

    public override Enumeration ExpectedEnumeration => Tests.SomeEnumeration.Option1;
}
