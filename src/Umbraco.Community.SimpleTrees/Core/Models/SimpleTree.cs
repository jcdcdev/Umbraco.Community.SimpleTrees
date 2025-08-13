using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public abstract class SimpleTree(ISimpleTreeContext context) : ISimpleTree
{
    [Obsolete("This method is obsolete and will be removed in future versions. Please use the constructor that accepts ISimpleTreeContext instead.")]
    protected SimpleTree() : this(StaticServiceProvider.Instance.GetRequiredService<ISimpleTreeContext>())
    {
    }

    public virtual string Label => Name;

    public virtual string[] Menus => [jcdcdev.Umbraco.Core.Constants.Menus.Content];
    public string Alias => GetType().Name.TrimEnd("Tree");
    public string DefaultEntityType => $"{Alias.Kebaberize()}-item".ToLowerInvariant();
    public string DefaultRootEntityType => $"{Alias.Kebaberize()}-root".ToLowerInvariant();

    public bool HasAlias(string treeAlias) => treeAlias.InvariantEquals(Alias);
    public abstract Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(int skip, int take, bool foldersOnly);
    public abstract Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string entityType, string parentUnique, int skip, int take, bool foldersOnly);

    public virtual int Weight => 0;
    public abstract string Name { get; }

    protected ISimpleTreeItem CreateItem(
        string name,
        string unique,
        string parentUnique,
        string icon = "icon-item",
        bool isFolder = false,
        bool hasChildren = false,
        string? parentEntityType = null)
    {
        var parent = SimpleTreeItemParent.Create(unique: parentUnique, entityType: parentEntityType ?? DefaultRootEntityType);
        return SimpleTreeItem.Create(name, unique, DefaultEntityType, icon, isFolder, hasChildren, parent);
    }

    protected ISimpleTreeItem CreateItem<T>(
        string name,
        string unique,
        string parentUnique,
        bool isFolder = false,
        bool hasChildren = false,
        string? parentEntityType = null) where T : ISimpleEntityType
    {
        var entityType = context.CustomEntityTypes.FirstOrDefault(x => x.GetType() == typeof(T));
        if (entityType == null)
        {
            throw new ArgumentException($"Entity type '{typeof(T).Name}' not found in the collection.", nameof(T));
        }

        var parent = SimpleTreeItemParent.Create(unique: parentUnique, entityType: parentEntityType ?? DefaultRootEntityType);
        return SimpleTreeItem.Create(name, unique, entityType.Alias, entityType.Icon, isFolder, hasChildren, parent);
    }


    protected ISimpleTreeItem CreateRootItem(string name, string unique, string icon = "icon-folder", bool isFolder = false, bool hasChildren = false) =>
        SimpleTreeItem.Create(name, unique, DefaultRootEntityType, icon, isFolder, hasChildren);

    protected PagedModel<ISimpleTreeItem> EmptyResult() => new(0, []);
}