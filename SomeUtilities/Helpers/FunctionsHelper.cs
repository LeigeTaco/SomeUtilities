using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SomeUtilities.Helpers;

public static class FunctionsHelper
{
    /// <summary> This method is honestly sooo clutch. Calling this can suppress so many messages </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DoNothing()
    {
        // Comment suppresses code smell
    }

    /// <inheritdoc cref="DoNothing()"/>
    /// <param name="args"> Add all your unused parameters here, they will not be called in any capacity </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Does Nothing")]
    public static void DoNothing(params object?[] args) => DoNothing();
}
