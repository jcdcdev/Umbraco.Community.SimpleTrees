namespace Umbraco.Community.SimpleTrees.Core.Models;

public interface ISimpleTreeContext
{
    IEnumerable<ISimpleEntityType> CustomEntityTypes { get; }
}