using Humanizer;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public abstract class SimpleEntityAction : ISimpleEntityAction
{
    public virtual string Alias => GetType().Name.Kebaberize().ToLowerInvariant();
    public virtual Type[] ForTreeRoots => [];
    public virtual Type[] ForSimpleEntityTypes => [];
    public virtual string[] ForEntityTypes => [];
    public abstract string Icon { get; }
    public abstract string Name { get; }
    public virtual int Weight => 0;
    public abstract Type[] ForTreeItems { get; }

    public string[] GetEntityTypes(IReadOnlyCollection<ISimpleTree> simpleTreesLookup, IReadOnlyCollection<ISimpleEntityType> simpleEntityTypesLookup)
    {
        var aliases = ForEntityTypes.ToList();
        if (ForTreeRoots.Any())
        {
            foreach (var type in ForTreeRoots)
            {
                var tree = simpleTreesLookup.FirstOrDefault(x => x.GetType() == type);
                if (tree == null)
                {
                    continue;
                }

                aliases.Add(tree.DefaultRootEntityType);
            }
        }
        
        if (ForTreeItems.Any())
        {
            foreach (var type in ForTreeItems)
            {
                var tree = simpleTreesLookup.FirstOrDefault(x => x.GetType() == type);
                if (tree == null)
                {
                    continue;
                }

                aliases.Add(tree.DefaultEntityType);
            }
        }
        
        if (ForSimpleEntityTypes.Any())
		{
			foreach (var type in ForSimpleEntityTypes)
			{
				var entityType = simpleEntityTypesLookup.FirstOrDefault(x => x.GetType() == type);
				if (entityType == null)
				{
					continue;
				}

				aliases.Add(entityType.Alias);
			}
		}

        return aliases.Distinct().ToArray();
    }
}