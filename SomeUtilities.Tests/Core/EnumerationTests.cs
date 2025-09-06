using System;
using System.Collections.Generic;
using SomeUtilities.Core;
using SomeUtilities.Helpers;
using SomeUtilities.Testing.Abstraction;
using Xunit;

namespace SomeUtilities.Tests.Core;

public sealed class EnumerationTests : UnitTest
{
    public sealed record SomeEnumeration : Enumeration
    {
        private SomeEnumeration(int id, string name) : base(id, name) => FunctionsHelper.DoNothing();

        public static SomeEnumeration Option1 { get; } = new(1, nameof(Option1));

        public static SomeEnumeration Option2 { get; } = new(2, nameof(Option2));

        public static SomeEnumeration Option3 { get; } = new(3, nameof(Option3));

        public static SomeEnumeration Option4 { get; } = new(4, nameof(Option4));
    }

    public sealed record SomeOtherEnumeration : Enumeration
    {
        private SomeOtherEnumeration(int id, string name) : base(id, name) => FunctionsHelper.DoNothing();

        public static SomeOtherEnumeration Option1 { get; } = new(1, nameof(Option1));

        public static SomeOtherEnumeration Option2 { get; } = new(2, nameof(Option2));

        public static SomeOtherEnumeration Option3 { get; } = new(3, nameof(Option3));

        public static SomeOtherEnumeration Option4 { get; } = new(4, nameof(Option4));
    }

    public enum GetEnumerationByIdTestCase
    {
        SomeEnumeration1,
        SomeEnumeration2,
        SomeEnumeration3,
        SomeEnumeration4,
        SomeOtherEnumeration1,
        SomeOtherEnumeration2,
        SomeOtherEnumeration3,
        SomeOtherEnumeration4,
    }

    [Theory, MemberData(nameof(GetEnumerationById_WithGeneric_SomeEnumeration_TestData))]
    public void GetEnumerationById_WithGeneric_SomeEnumeration(GetEnumerationByIdTestCase testCase)
    {
        // ARRANGE
        SomeEnumeration actualEnumeration;

        GetTestData(
            testCase,
            out var givenId,
            out _,
            out var expectedEnumeration);

        // ACT
        actualEnumeration = Enumeration.GetEnumerationById<SomeEnumeration>(givenId);

        // ASSERT
        Assert.Equivalent(expectedEnumeration, actualEnumeration);
        Assert.Equal(expectedEnumeration.GetType(), actualEnumeration.GetType());
    }

    [Theory, MemberData(nameof(GetEnumerationById_WithGeneric_SomeOtherEnumeration_TestData))]
    public void GetEnumerationById_WithGeneric_SomeOtherEnumeration(GetEnumerationByIdTestCase testCase)
    {
        // ARRANGE
        SomeOtherEnumeration actualEnumeration;

        GetTestData(
            testCase,
            out var givenId,
            out _,
            out var expectedEnumeration);

        // ACT
        actualEnumeration = Enumeration.GetEnumerationById<SomeOtherEnumeration>(givenId);

        // ASSERT
        Assert.Equivalent(expectedEnumeration, actualEnumeration);
        Assert.Equal(expectedEnumeration.GetType(), actualEnumeration.GetType());
    }

    [Theory, MemberData(nameof(GetEnumerationById_WithoutGeneric_TestData))]
    public void GetEnumerationById_WithoutGeneric(GetEnumerationByIdTestCase testCase)
    {
        // ARRANGE
        Enumeration actualEnumeration;

        GetTestData(
            testCase,
            out var givenId,
            out var givenType,
            out var expectedEnumeration);

        // ACT
        actualEnumeration = Enumeration.GetEnumerationById(givenId, givenType);

        // ASSERT
        Assert.IsType(givenType, actualEnumeration);
        Assert.Equal(expectedEnumeration, actualEnumeration);
    }

    [Fact]
    public void GetEnumerationById_ThrowsWhenInvalidType()
    {
        // ARRANGE
        Exception? actual;

        // ACT
        actual = Record.Exception(() => Enumeration.GetEnumerationById(1, typeof(TestCase)));

        // ASSERT
        Assert.IsType<KeyNotFoundException>(actual);
    }

    [Fact]
    public void GetEnumerationById_ThrowsWhenInvalidId()
    {
        // ARRANGE
        Exception? actual;

        // ACT
        actual = Record.Exception(() => Enumeration.GetEnumerationById(0, typeof(SomeEnumeration)));

        // ASSERT
        Assert.IsType<KeyNotFoundException>(actual);
    }

    [Fact]
    public void TryFindById_WithoutGeneric()
    {
        // ARRANGE
        SomeEnumeration? actualSomeEnumeration, expectedSomeEnumeration = SomeEnumeration.Option1;
        SomeOtherEnumeration? actualSomeOtherEnumeration, expectedSomeOtherEnumeration = SomeOtherEnumeration.Option1;

        // ACT & ASSERT
        Assert.False(Enumeration.TryFindById(0, out actualSomeEnumeration));
        Assert.Null(actualSomeEnumeration);
        Assert.True(Enumeration.TryFindById(1, out actualSomeEnumeration));
        Assert.Equal(expectedSomeEnumeration, actualSomeEnumeration);

        Assert.False(Enumeration.TryFindById(0, out actualSomeOtherEnumeration));
        Assert.Null(actualSomeOtherEnumeration);
        Assert.True(Enumeration.TryFindById(1, out actualSomeOtherEnumeration));
        Assert.Equal(expectedSomeOtherEnumeration, actualSomeOtherEnumeration);

        Assert.NotEqual<Enumeration>(actualSomeOtherEnumeration, actualSomeEnumeration);
    }

    public static TheoryData<GetEnumerationByIdTestCase> GetEnumerationById_WithoutGeneric_TestData() => [.. Enum.GetValues<GetEnumerationByIdTestCase>()];

    public static TheoryData<GetEnumerationByIdTestCase> GetEnumerationById_WithGeneric_SomeEnumeration_TestData()
    {
        TheoryData<GetEnumerationByIdTestCase> testCases = [];

        foreach (var testCase in Enum.GetValues<GetEnumerationByIdTestCase>())
        {
            GetTestData(
                testCase,
                out _,
                out _,
                out var enumeration);

            if (enumeration is SomeEnumeration)
            {
                testCases.Add(testCase);
            }
        }

        return testCases;
    }

    public static TheoryData<GetEnumerationByIdTestCase> GetEnumerationById_WithGeneric_SomeOtherEnumeration_TestData()
    {
        TheoryData<GetEnumerationByIdTestCase> testCases = [];

        foreach (var testCase in Enum.GetValues<GetEnumerationByIdTestCase>())
        {
            GetTestData(
                testCase,
                out _,
                out _,
                out var enumeration);

            if (enumeration is SomeOtherEnumeration)
            {
                testCases.Add(testCase);
            }
        }

        return testCases;
    }

    private static void GetTestData(
        GetEnumerationByIdTestCase testCase,
        out int givenId,
        out Type givenType,
        out Enumeration expectedEnumeration)
    {
        expectedEnumeration = testCase switch
        {
            GetEnumerationByIdTestCase.SomeEnumeration1 => SomeEnumeration.Option1,
            GetEnumerationByIdTestCase.SomeEnumeration2 => SomeEnumeration.Option2,
            GetEnumerationByIdTestCase.SomeEnumeration3 => SomeEnumeration.Option3,
            GetEnumerationByIdTestCase.SomeEnumeration4 => SomeEnumeration.Option4,
            GetEnumerationByIdTestCase.SomeOtherEnumeration1 => SomeOtherEnumeration.Option1,
            GetEnumerationByIdTestCase.SomeOtherEnumeration2 => SomeOtherEnumeration.Option2,
            GetEnumerationByIdTestCase.SomeOtherEnumeration3 => SomeOtherEnumeration.Option3,
            GetEnumerationByIdTestCase.SomeOtherEnumeration4 => SomeOtherEnumeration.Option4,
            _ => throw new NotImplementedException(),
        };

        givenId = expectedEnumeration.Id;
        givenType = expectedEnumeration.GetType();
    }
}