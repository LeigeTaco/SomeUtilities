using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SomeUtilities.Core;

public abstract record Enumeration(int Id, string Name)
{
    public static TEnumeration GetEnumerationById<TEnumeration>(int id)
        where TEnumeration : Enumeration
    {
        var type = typeof(TEnumeration);

        return (TEnumeration)GetEnumerationById(id, type);
    }

    public static Enumeration GetEnumerationById(int id, Type type)
    {
        var map = EnumerationMap.Map;

        return map[type][id];
    }

    public static bool TryFindById<TEnumeration>(int id, [NotNullWhen(true)] out TEnumeration? enumeration)
        where TEnumeration : Enumeration
    {
        var type = typeof(TEnumeration);

        enumeration = SafeGetEnumeration(id, type) as TEnumeration;

        return enumeration is not null;
    }

    public static bool TryFindById(int id, Type type, [NotNullWhen(true)] out object? enumeration)
    {
        enumeration = SafeGetEnumeration(id, type);

        return enumeration is not null;
    }

    private static Enumeration? SafeGetEnumeration(int id, Type type)
    {
        var map = EnumerationMap.Map;

        return map.TryGetValue(type, out var dictionary) && dictionary.TryGetValue(id, out var @enum)
            ? @enum
            : null;
    }

    public static IEnumerable<TEnumeration> GetAllEnumerations<TEnumeration>()
        where TEnumeration : Enumeration
    {
        var type = typeof(TEnumeration);
        var map = EnumerationMap.Map;

        return map[type].Select(kvp => kvp.Value as TEnumeration)!;
    }

    protected static FrozenDictionary<int, Enumeration> GetMapForType<T>() where T : Enumeration => EnumerationMap.Map[typeof(T)];
}
