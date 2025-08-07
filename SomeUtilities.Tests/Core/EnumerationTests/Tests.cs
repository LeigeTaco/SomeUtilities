using SomeUtilities.Core;
using SomeUtilities.Helpers;
using SomeUtilities.Testing.Abstraction;
using SomeUtilities.Tests.Core.EnumerationTests.TestCases;
using Xunit;

namespace SomeUtilities.Tests.Core.EnumerationTests;

public sealed class Tests : UnitTest
{
    public sealed class SomeEnumeration : Enumeration
    {
        private SomeEnumeration(int id, string name) : base(id, name) => FunctionsHelper.DoNothing();

        public static SomeEnumeration Option1 { get; } = new(1, nameof(Option1));

        public static SomeEnumeration Option2 { get; } = new(2, nameof(Option2));

        public static SomeEnumeration Option3 { get; } = new(3, nameof(Option3));

        public static SomeEnumeration Option4 { get; } = new(4, nameof(Option4));
    }

    public sealed class SomeOtherEnumeration : Enumeration
    {
        private SomeOtherEnumeration(int id, string name) : base(id, name) => FunctionsHelper.DoNothing();

        public static SomeOtherEnumeration Option1 { get; } = new(1, nameof(Option1));

        public static SomeOtherEnumeration Option2 { get; } = new(2, nameof(Option2));

        public static SomeOtherEnumeration Option3 { get; } = new(3, nameof(Option3));

        public static SomeOtherEnumeration Option4 { get; } = new(4, nameof(Option4));
    }

    [Theory, MemberData(nameof(GetEnumerationByIdTests))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1038:There are more theory data type arguments than allowed by the parameters of the test method", Justification = "Type implements IXunitSerializable")]
    public void GetEnumerationById_SomeEnumeration(GetEnumerationById testCase)
    {
        // ARRANGE
        SomeEnumeration actualEnumeration;

        // ACT
        actualEnumeration = Enumeration.GetEnumerationById<SomeEnumeration>(testCase.GivenId);

        // ASSERT
        Assert.Equal(testCase.ExpectedEnumeration, actualEnumeration);
    }

    [Fact]
    public void Fact()
    {
        Assert.NotEqual<Enumeration>(SomeEnumeration.Option1, SomeOtherEnumeration.Option1);
    }

    public static TheoryData<GetEnumerationById> GetEnumerationByIdTests() => new(TestCase.GetTestCases<Tests, GetEnumerationById>(nameof(GetEnumerationById_SomeEnumeration)));
}