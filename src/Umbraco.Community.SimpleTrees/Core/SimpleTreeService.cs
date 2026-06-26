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

    public async Task<TargetPagedModel<ISimpleTreeItem>> GetTreeRootAsync(string treeAlias, string? unique, string entityType, int takeBefore, int takeAfter, bool foldersOnly)
    {
        var simpleTree = simpleTrees.FirstOrDefault(x => x.HasAlias(treeAlias));
        if (simpleTree == null)
        {
            throw new ArgumentException($"No tree found with alias '{treeAlias}'", nameof(treeAlias));
        }

        var model = await simpleTree.GetTreeRootAsync(0, int.MaxValue, foldersOnly);
        var index = model.Items.FindIndex(x => x.Unique == unique);
        var items = model.Items
            .Skip(Math.Max(0, index - takeBefore))
            .Take(takeAfter);

        return new TargetPagedModel<ISimpleTreeItem>
        {
            Items = items,
            Total = model.Total,
            TotalBefore = index,
            TotalAfter = (int)model.Total - index - 1
        };
    }

    public async Task<TargetPagedModel<ISimpleTreeItem>> GetPagedTargetTreeChildrenAsync(
        string treeAlias,
        string targetUnique,
        string targetEntityType,
        string entityType,
        string parentUnique,
        int takeBefore,
        int takeAfter,
        bool foldersOnly)
    {
        var simpleTree = simpleTrees.FirstOrDefault(x => x.HasAlias(treeAlias));
        if (simpleTree == null)
        {
            throw new ArgumentException($"No tree found with alias '{treeAlias}'", nameof(treeAlias));
        }

        var model = await simpleTree.GetTreeChildrenAsync(entityType, parentUnique, 0, int.MaxValue, foldersOnly);
        var index = model.Items.FindIndex(x => x.Unique == targetUnique && x.EntityType == targetEntityType);
        var items = model.Items
            .Skip(Math.Max(0, index - takeBefore))
            .Take(takeAfter);

        return new TargetPagedModel<ISimpleTreeItem>
        {
            Items = items,
            Total = model.Total,
            TotalBefore = index,
            TotalAfter = (int)model.Total - index - 1
        };
    }


    public async Task<PagedModel<ISimpleTreeItem>> GetPagedOffsetTreeChildrenAsync(string treeAlias, string entityType, string parentUnique, int skip, int take, bool foldersOnly)
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

public class TargetPagedModel<T> : PagedModel<T>
{
    public int TotalBefore { get; set; }
    public int TotalAfter { get; set; }
}