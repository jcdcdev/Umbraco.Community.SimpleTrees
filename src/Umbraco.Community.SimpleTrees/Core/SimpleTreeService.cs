using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Core.Composing.Collections;
using Umbraco.Community.SimpleTrees.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleTrees.Core;

public class SimpleTreeService(SimpleTreeCollection simpleTrees) : ISimpleTreeService
{
    private IEnumerable<ISimpleTreeItem> EnsureRootTreeItems(ISimpleTree simpleTree, IEnumerable<ISimpleTreeItem> items)
    {
        foreach (var item in items)
        {
            if (item.EntityType.IsNullOrWhiteSpace())
            {
                item.EntityType = simpleTree.DefaultRootEntityType;
            }

            yield return item;
        }
    }

    private IEnumerable<ISimpleTreeItem> EnsureTreeItems(ISimpleTree simpleTree, IEnumerable<ISimpleTreeItem> items)
    {
        foreach (var item in items)
        {
            if (item.EntityType.IsNullOrWhiteSpace())
            {
                item.EntityType = simpleTree.DefaultEntityType;
            }

            yield return item;
        }
    }

    public async Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(string treeAlias, int skip, int take, bool foldersOnly)
    {
        var simpleTree = simpleTrees.FirstOrDefault(x => x.HasAlias(treeAlias));
        if (simpleTree == null)
        {
            throw new ArgumentException($"No tree found with alias '{treeAlias}'", nameof(treeAlias));
        }

        var model = await simpleTree.GetTreeRootAsync(skip, take, foldersOnly);
        return new PagedModel<ISimpleTreeItem>(model.Total, EnsureRootTreeItems(simpleTree, model.Items));
    }

    public async Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string treeAlias, string entityType, string parentUnique, int skip, int take, bool foldersOnly)
    {
        var simpleTree = simpleTrees.FirstOrDefault(x => x.HasAlias(treeAlias));
        if (simpleTree == null)
        {
            throw new ArgumentException($"No tree found with alias '{treeAlias}'", nameof(treeAlias));
        }

        var model = await simpleTree.GetTreeChildrenAsync(entityType, parentUnique, skip, take, foldersOnly);
        return new PagedModel<ISimpleTreeItem>(model.Total, EnsureTreeItems(simpleTree, model.Items));
    }

    public IEnumerable<ISimpleTree> GetAll() => simpleTrees;
    public ISimpleTree? GetByAlias(string alias) => simpleTrees.FirstOrDefault(x => x.HasAlias(alias));
}