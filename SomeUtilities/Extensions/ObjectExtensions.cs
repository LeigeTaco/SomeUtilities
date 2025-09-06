using SomeUtilities.Helpers;

namespace SomeUtilities.Extensions;

public static class ObjectExtensions
{
    public static void DoNothing(this object? obj) => FunctionsHelper.DoNothing(obj);

    public static void DoNothing(this object? obj, params object?[] args) => FunctionsHelper.DoNothing(obj, args);
}
