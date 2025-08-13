using Humanizer;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public abstract class SimpleEntityType : ISimpleEntityType
{
    public string Alias => GetAlias(GetType());
    public string Name => GetName(GetType());
    public abstract string Icon { get; }

    private static string GetName(Type type) => type.Name.TrimEnd("EntityType");

    private static string GetAlias(Type type) => $"{GetName(type).Kebaberize()}-item".ToLowerInvariant();

    public static string GetName<T>() where T : ISimpleEntityType => GetName(typeof(T));

    public static string GetAlias<T>() where T : ISimpleEntityType => GetAlias(typeof(T));

    public static string GetViewNameFromAlias(string entityType) => string.Join("", entityType.Split("-").Select(x => x.ToFirstUpperInvariant()));
}