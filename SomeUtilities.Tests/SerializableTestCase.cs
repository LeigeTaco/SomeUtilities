using SomeUtilities.Helpers;
using SomeUtilities.Testing.Abstraction;
using Xunit.Abstractions;

namespace SomeUtilities.Tests;

public abstract class SerializableTestCase<TClass> : TestCase<TClass>
    where TClass : UnitTest
{
    protected SerializableTestCase(string testMethodName, string testCaseName) : base(testMethodName, testCaseName) => FunctionsHelper.DoNothing();

    // Xunit Serializable Methods. Abstract class cannot inherit because it does not fit 'new()' constraint
    public void Deserialize(IXunitSerializationInfo info) => FunctionsHelper.DoNothing(info);

    public void Serialize(IXunitSerializationInfo info) => FunctionsHelper.DoNothing(info);
}
