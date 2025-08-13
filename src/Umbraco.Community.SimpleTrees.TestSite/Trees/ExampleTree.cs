using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class ExampleTree(ISimpleTreeContext context) : SimpleTree(context)
{
    public override Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(int skip, int take, bool foldersOnly)
    {
        return Task.FromResult(new PagedModel<ISimpleTreeItem>(0, []));
    }

    public override Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string entityType, string parentUnique, int skip, int take, bool foldersOnly)
    {
        return Task.FromResult(new PagedModel<ISimpleTreeItem>(0, []));
    }

    public override string Name => "Example Tree";
}