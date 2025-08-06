using System;
using System.Collections.Generic;
using System.Linq;
using SomeUtilities.Helpers;

namespace SomeUtilities.Testing.Abstraction;

public abstract class TestCase<TTestClass>
    where TTestClass : UnitTest
{
    public string TestMethodName { get; }

    public string TestCaseName { get; }

    protected TestCase(string testMethodName, string testCaseName)
    {
        TestMethodName = testMethodName;
        TestCaseName = testCaseName;

        FunctionsHelper.DoNothing();
    }

    public static IEnumerable<TTestCase> GetTestCases<TTestCase>(string testMethodName)
        where TTestCase : TestCase<TTestClass>
    {
        var testClassType = typeof(TTestClass);
        var assembly = testClassType.Assembly;

        foreach (var testCase in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(TTestCase))).Select(Activator.CreateInstance))
        {
            if (testCase is TTestCase tc && string.Equals(tc.TestMethodName, testMethodName))
            {
                yield return tc;
            }
        }
    }
}
