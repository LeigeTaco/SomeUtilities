using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SomeUtilities.Core;

public abstract class Enumeration
{
    private static readonly Lazy<FrozenDictionary<Type, FrozenDictionary<int, Enumeration>>> _allEnumerationsMap = new(CreateMap);

    private static readonly Dictionary<Type, Dictionary<int, Enumeration>> _registeredEnumerations = [];

    public int Id { get; }

    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;

        Register();
    }

    public static TEnumeration GetEnumerationById<TEnumeration>(int id)
        where TEnumeration : Enumeration
    {
        var type = typeof(TEnumeration);
        var map = _allEnumerationsMap.Value;

        return (TEnumeration)map[type][id];

    }

    public static bool TryFindById<TEnumeration>(int id, [NotNullWhen(true)] out TEnumeration? enumeration)
        where TEnumeration : Enumeration
    {
        var type = typeof(TEnumeration);
        var map = _allEnumerationsMap.Value;

        if (map.TryGetValue(type, out var dictionary)
            && dictionary.TryGetValue(id, out var @enum))
        {
            enumeration = @enum as TEnumeration;
        }
        else
        {
            enumeration = null;
        }

        return enumeration is not null;
    }

    public static IEnumerable<TEnumeration> GetAllEnumerations<TEnumeration>()
        where TEnumeration : Enumeration
    {
        var type = typeof(TEnumeration);
        var map = _allEnumerationsMap.Value;

        return map[type].Select(kvp => kvp.Value as TEnumeration)!;
    }

    private void Register()
    {
        var type = GetType();

        if (!_registeredEnumerations.TryGetValue(type, out var enumsForType))
        {
            enumsForType = [];

            _registeredEnumerations[type] = enumsForType;
        }

        enumsForType[Id] = this;
    }

    private static FrozenDictionary<Type, FrozenDictionary<int, Enumeration>> CreateMap()
    {
        try
        {
            return _registeredEnumerations.ToFrozenDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ToFrozenDictionary());
        }
        finally
        {
            _registeredEnumerations.Clear();
        }
    }
}
