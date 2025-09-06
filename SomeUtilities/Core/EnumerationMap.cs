using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SomeUtilities.Core;

internal static class EnumerationMap
{
    internal static FrozenDictionary<Type, FrozenDictionary<int, Enumeration>> Map => _mapProvider.Value;

    private static readonly Lazy<FrozenDictionary<Type, FrozenDictionary<int, Enumeration>>> _mapProvider = new(CreateMap);

    private static FrozenDictionary<Type, FrozenDictionary<int, Enumeration>> CreateMap()
    {
        Dictionary<Type, List<Enumeration>> precompiledMap = [];

        foreach (var enumerationType in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Enumeration)))))
        {
            List<Enumeration> enumerationList = [];

            foreach (var enumeration in enumerationType.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(pInfo => pInfo.PropertyType == enumerationType))
            {
                if (enumeration.GetValue(null) is Enumeration e)
                {
                    enumerationList.Add(e);
                }
            }

            precompiledMap.Add(enumerationType, enumerationList);
        }

        return precompiledMap.ToFrozenDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToFrozenDictionary(e => e.Id, e => e));
    }
}
