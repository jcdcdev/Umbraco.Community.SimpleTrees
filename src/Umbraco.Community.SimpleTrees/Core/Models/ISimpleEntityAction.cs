namespace Umbraco.Community.SimpleTrees.Core.Models;

public interface ISimpleEntityAction
{
    string Alias { get; }
    string Icon { get; }
    string Name { get; }
    int Weight { get; }
    string[] GetEntityTypes(IReadOnlyCollection<ISimpleTree> simpleTreesLookup, IReadOnlyCollection<ISimpleEntityType> simpleEntityTypesLookup);
}