using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.Core;

public interface ISimpleTreeService
{
    Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(string treeAlias, int skip, int take, bool foldersOnly);
    Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string treeAlias, string entityType, string parentUnique, int skip, int take, bool foldersOnly);
    IEnumerable<ISimpleTree> GetAll();
    ISimpleTree? GetByAlias(string alias);
}