using SomeUtilities.Core;
using SomeUtilities.Helpers;
using Xunit.Abstractions;

namespace SomeUtilities.Tests.Core.EnumerationTests.TestCases;

public sealed class GetEnumerationById_SomeOtherEnumeration_Option1 : GetEnumerationById, IXunitSerializable
{
    public GetEnumerationById_SomeOtherEnumeration_Option1() : base(nameof(GetEnumerationById_SomeOtherEnumeration_Option1)) => FunctionsHelper.DoNothing();

    public override int GivenId => 1;

    public override Enumeration ExpectedEnumeration => Tests.SomeOtherEnumeration.Option1;
}
