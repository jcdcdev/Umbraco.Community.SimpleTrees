using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class MyTree(ISimpleTreeContext context) : SimpleTree(context)
{
    public override Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(int skip, int take, bool foldersOnly)
    {
        var data = new List<ISimpleTreeItem>
        {
            CreateRootItem("James", Guid.NewGuid().ToString(), "icon-user"),
            CreateRootItem("Tim", Guid.NewGuid().ToString(), "icon-user"),
        };

        return Task.FromResult(new PagedModel<ISimpleTreeItem>(data.Count, data));
    }

    public override Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string entityType, string parentUnique, int skip, int take, bool foldersOnly) => Task.FromResult(EmptyResult());

    public override string[] Menus => [nameof(MyMenu)];

    public override string Name => "My Tree";
}