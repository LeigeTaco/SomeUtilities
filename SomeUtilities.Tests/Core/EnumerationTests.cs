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

    public enum TryFindEnumerationByIdTestCase
    {
        SomeEnumeration1,
        SomeEnumeration2,
        SomeEnumeration3,
        SomeEnumeration4,
        SomeEnumeration_UnknownId,
        SomeOtherEnumeration1,
        SomeOtherEnumeration2,
        SomeOtherEnumeration3,
        SomeOtherEnumeration4,
        SomeOtherEnumeration_UnknownId,
    }

    public enum GetAllEnumerationsTestCase
    {
        SomeEnumeration,
        SomeOtherEnumeration,
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
        Assert.Equivalent(expectedEnumeration, actualEnumeration, true);
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
        Assert.Equivalent(expectedEnumeration, actualEnumeration, true);
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

    [Theory, MemberData(nameof(TryGetEnumerationById_WithGeneric_SomeEnumeration_TestData))]
    public void TryGetEnumerationById_WithGeneric_SomeEnumeration(TryFindEnumerationByIdTestCase testCase)
    {
        // ARRANGE
        bool actualResult;
        SomeEnumeration? actualEnumeration;

        GetTestData(
            testCase,
            out var givenId,
            out _,
            out var expectedResult,
            out var expectedEnumeration);

        // ACT
        actualResult = Enumeration.TryFindById(givenId, out actualEnumeration);

        // ASSERT
        Assert.Equal(expectedResult, actualResult);
        Assert.Equivalent(expectedEnumeration, actualEnumeration, true);
    }

    [Theory, MemberData(nameof(TryGetEnumerationById_WithGeneric_SomeOtherEnumeration_TestData))]
    public void TryGetEnumerationById_WithGeneric_SomeOtherEnumeration(TryFindEnumerationByIdTestCase testCase)
    {
        // ARRANGE
        bool actualResult;
        SomeOtherEnumeration? actualEnumeration;

        GetTestData(
            testCase,
            out var givenId,
            out _,
            out var expectedResult,
            out var expectedEnumeration);

        // ACT
        actualResult = Enumeration.TryFindById(givenId, out actualEnumeration);

        // ASSERT
        Assert.Equal(expectedResult, actualResult);
        Assert.Equivalent(expectedEnumeration, actualEnumeration, true);
    }

    [Theory, MemberData(nameof(TryGetEnumerationById_WithoutGeneric_TestData))]
    public void TryFindById_WithoutGeneric(TryFindEnumerationByIdTestCase testCase)
    {
        // ARRANGE
        bool actualResult;
        Enumeration? actualEnumeration;

        GetTestData(
            testCase,
            out var givenId,
            out var givenType,
            out var expectedResult,
            out var expectedEnumeration);

        // ACT
        actualResult = Enumeration.TryFindById(givenId, givenType, out actualEnumeration);

        // ASSERT
        Assert.Equal(expectedResult, actualResult);
        Assert.Equivalent(expectedEnumeration, actualEnumeration, true);
        Assert.Equal(expectedEnumeration?.GetType(), actualEnumeration?.GetType());
    }

    [Fact]
    public void GetAllEnumerations_WithGeneric_SomeEnumeration()
    {
        // ARRANGE
        IEnumerable<SomeEnumeration> actualEnumerations, expectedEnumerations = [
            SomeEnumeration.Option1,
            SomeEnumeration.Option2,
            SomeEnumeration.Option3,
            SomeEnumeration.Option4,
        ];

        // ACT
        actualEnumerations = Enumeration.GetAllEnumerations<SomeEnumeration>();

        // ASSERT
        Assert.Equivalent(expectedEnumerations, actualEnumerations);
    }

    [Fact]
    public void GetAllEnumerations_WithGeneric_SomeOtherEnumeration()
    {
        // ARRANGE
        IEnumerable<SomeOtherEnumeration> actualEnumerations, expectedEnumerations = [
            SomeOtherEnumeration.Option1,
            SomeOtherEnumeration.Option2,
            SomeOtherEnumeration.Option3,
            SomeOtherEnumeration.Option4,
        ];

        // ACT
        actualEnumerations = Enumeration.GetAllEnumerations<SomeOtherEnumeration>();

        // ASSERT
        Assert.Equivalent(expectedEnumerations, actualEnumerations);
    }

    [Theory, MemberData(nameof(GetAllEnumerationsTestData))]
    public void GetAllEnumerations_WithoutGeneric(GetAllEnumerationsTestCase testCase)
    {
        // ARRANGE
        IEnumerable<Enumeration> actualEnumerations;

        GetTestData(
            testCase,
            out var givenType,
            out var expectedEnumerations);

        // ACT
        actualEnumerations = Enumeration.GetAllEnumerations(givenType);

        // ASSERT
        Assert.Equivalent(expectedEnumerations, actualEnumerations, true);
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

    public static TheoryData<TryFindEnumerationByIdTestCase> TryGetEnumerationById_WithoutGeneric_TestData() => [.. Enum.GetValues<TryFindEnumerationByIdTestCase>()];

    public static TheoryData<TryFindEnumerationByIdTestCase> TryGetEnumerationById_WithGeneric_SomeEnumeration_TestData()
    {
        TheoryData<TryFindEnumerationByIdTestCase> testCases = [];

        foreach (var testCase in Enum.GetValues<TryFindEnumerationByIdTestCase>())
        {
            GetTestData(
                testCase,
                out _,
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

    public static TheoryData<TryFindEnumerationByIdTestCase> TryGetEnumerationById_WithGeneric_SomeOtherEnumeration_TestData()
    {
        TheoryData<TryFindEnumerationByIdTestCase> testCases = [];

        foreach (var testCase in Enum.GetValues<TryFindEnumerationByIdTestCase>())
        {
            GetTestData(
                testCase,
                out _,
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

    public static TheoryData<GetAllEnumerationsTestCase> GetAllEnumerationsTestData() => [.. Enum.GetValues<GetAllEnumerationsTestCase>()];

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

    private static void GetTestData(
        TryFindEnumerationByIdTestCase testCase,
        out int givenId,
        out Type givenType,
        out bool expectedResult,
        out Enumeration? expectedEnumeration)
    {
        expectedEnumeration = testCase switch
        {
            TryFindEnumerationByIdTestCase.SomeEnumeration1 => SomeEnumeration.Option1,
            TryFindEnumerationByIdTestCase.SomeEnumeration2 => SomeEnumeration.Option2,
            TryFindEnumerationByIdTestCase.SomeEnumeration3 => SomeEnumeration.Option3,
            TryFindEnumerationByIdTestCase.SomeEnumeration4 => SomeEnumeration.Option4,
            TryFindEnumerationByIdTestCase.SomeOtherEnumeration1 => SomeOtherEnumeration.Option1,
            TryFindEnumerationByIdTestCase.SomeOtherEnumeration2 => SomeOtherEnumeration.Option2,
            TryFindEnumerationByIdTestCase.SomeOtherEnumeration3 => SomeOtherEnumeration.Option3,
            TryFindEnumerationByIdTestCase.SomeOtherEnumeration4 => SomeOtherEnumeration.Option4,
            TryFindEnumerationByIdTestCase.SomeEnumeration_UnknownId or TryFindEnumerationByIdTestCase.SomeOtherEnumeration_UnknownId => null,
            _ => throw new NotImplementedException(),
        };

        switch (testCase)
        {
            case TryFindEnumerationByIdTestCase.SomeEnumeration1:
                expectedEnumeration = SomeEnumeration.Option1;
                givenType = typeof(SomeEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeEnumeration2:
                expectedEnumeration = SomeEnumeration.Option2;
                givenType = typeof(SomeEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeEnumeration3:
                expectedEnumeration = SomeEnumeration.Option3;
                givenType = typeof(SomeEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeEnumeration4:
                expectedEnumeration = SomeEnumeration.Option4;
                givenType = typeof(SomeEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeEnumeration_UnknownId:
                expectedEnumeration = null;
                givenType = typeof(SomeEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeOtherEnumeration1:
                expectedEnumeration = SomeOtherEnumeration.Option1;
                givenType = typeof(SomeOtherEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeOtherEnumeration2:
                expectedEnumeration = SomeOtherEnumeration.Option2;
                givenType = typeof(SomeOtherEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeOtherEnumeration3:
                expectedEnumeration = SomeOtherEnumeration.Option3;
                givenType = typeof(SomeOtherEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeOtherEnumeration4:
                expectedEnumeration = SomeOtherEnumeration.Option4;
                givenType = typeof(SomeOtherEnumeration);
                break;
            case TryFindEnumerationByIdTestCase.SomeOtherEnumeration_UnknownId:
                expectedEnumeration = null;
                givenType = typeof(SomeOtherEnumeration);
                break;
            default: throw new NotImplementedException();
        }

        if (expectedEnumeration is not null)
        {
            givenId = expectedEnumeration.Id;
            expectedResult = true;
        }
        else
        {
            givenId = -1;
            expectedResult = false;
        }
    }

    private static void GetTestData(
        GetAllEnumerationsTestCase testCase,
        out Type givenType,
        out IEnumerable<Enumeration> expectedEnumerations)
    {
        switch (testCase)
        {
            case GetAllEnumerationsTestCase.SomeEnumeration:
                givenType = typeof(SomeEnumeration);
                expectedEnumerations = [
                    SomeEnumeration.Option1,
                    SomeEnumeration.Option2,
                    SomeEnumeration.Option3,
                    SomeEnumeration.Option4,
                ];
                break;
            case GetAllEnumerationsTestCase.SomeOtherEnumeration:
                givenType = typeof(SomeOtherEnumeration);
                expectedEnumerations = [
                    SomeOtherEnumeration.Option1,
                    SomeOtherEnumeration.Option2,
                    SomeOtherEnumeration.Option3,
                    SomeOtherEnumeration.Option4,
                ];
                break;
            default: throw new NotImplementedException();
        }
    }
}