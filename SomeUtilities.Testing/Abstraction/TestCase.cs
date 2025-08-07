using System;
using System.Collections.Generic;
using System.Linq;
using SomeUtilities.Helpers;

namespace SomeUtilities.Testing.Abstraction;

public abstract class TestCase
{
    protected internal static Dictionary<Type, Dictionary<string, List<TestCase>>> TestCases { get; } = new();

    public string TestMethodName { get; }

    public string TestCaseName { get; }

    protected TestCase(string testMethodName, string testCaseName)
    {
        TestMethodName = testMethodName;
        TestCaseName = testCaseName;

        FunctionsHelper.DoNothing();
    }

    public static IEnumerable<TCase> GetTestCases<TClass, TCase>(string testMethodName)
        where TCase : TestCase<TClass>
        where TClass : UnitTest
    {
        var testClassType = typeof(TClass);
        var assembly = testClassType.Assembly;

        foreach (var testCase in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TCase))).Select(Activator.CreateInstance))
        {
            if (testCase is TCase tc && string.Equals(tc.TestMethodName, testMethodName))
            {
                yield return tc;
            }
        }
    }
}

public abstract class TestCase<TClass> : TestCase
    where TClass : UnitTest
{
    protected TestCase(string testMethodName, string testCaseName) : base(testMethodName, testCaseName) => Register();

    private void Register()
    {
        var testClassType = typeof(TClass);

        if (!TestCases.TryGetValue(testClassType, out var testCasesForClass))
        {
            testCasesForClass = new(StringComparer.OrdinalIgnoreCase);

            TestCases[testClassType] = testCasesForClass;
        }

        if (!testCasesForClass.TryGetValue(TestMethodName, out var testCasesForMethod))
        {
            testCasesForMethod = new();

            testCasesForClass[TestMethodName] = testCasesForMethod;
        }

        testCasesForMethod.Add(this);
    }
}
