using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class NuGetPackageTree : SimpleTree
{
    private static readonly string[] Authors = ["jcdcdev", "umbraco"];
    public override string[] Menus => [nameof(NuGetMenu)];

    public override Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(int skip, int take, bool foldersOnly)
    {
        var items = Authors.Select(x => CreateRootItem(x, x, "icon-user", true, true));
        return Task.FromResult<PagedModel<ISimpleTreeItem>>(new(Authors.Length, items));
    }

    public override async Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string entityType, string parentUnique, int skip, int take, bool foldersOnly)
    {
        var service = new NuGetService();
        if (entityType == DefaultRootEntityType)
        {
            var results = await service.GetPackages(parentUnique, skip, take);

            var items = new List<ISimpleTreeItem>();
            foreach (var result in results)
            {
                var item = CreateItem(result.Title, result.Identity.Id, parentUnique, "icon-document", hasChildren: true);
                item.IsFolder = true;
                items.Add(item);
            }

            return new PagedModel<ISimpleTreeItem>(items.Count, items);
        }

        var versions = await service.GetPackageMetadata(parentUnique);

        var versionItems = new List<ISimpleTreeItem>();
        foreach (var version in versions.Reverse())
        {
            var item = CreateItem(
                version.Identity.Version.ToString(),
                $"{version.Identity.Id}_{version.Identity.Version}",
                parentUnique,
                "icon-box");

            versionItems.Add(item);
        }

        return new PagedModel<ISimpleTreeItem>(versionItems.Count, versionItems);
    }

    public override string Name => "NuGet Packages";
}