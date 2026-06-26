using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class NuGetPackageTree(ISimpleTreeContext context) : SimpleTree(context)
{
    private static readonly string[] Authors = ["jcdcdev", "umbraco"];
    public override string Name => "NuGet Packages";
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
                var item = CreateItem(result.Title, result.Identity.Id, parentUnique, "icon-document", hasChildren: true, isFolder: true);

                items.Add(item);
            }

            return new PagedModel<ISimpleTreeItem>(items.Count, items);
        }

        var versions = (await service
                .GetPackageMetadata(parentUnique))
            .Reverse()
            .ToList();

        var versionItems = new List<ISimpleTreeItem>();
        foreach (var version in versions.Skip(skip).Take(take))
        {
            var name = version.Identity.Version.ToString();
            var unique = $"{version.Identity.Id}_{version.Identity.Version}";
            var item = CreateItem<NuGetPackageVersionEntityType>(name, unique, parentUnique);

            versionItems.Add(item);
        }

        return new PagedModel<ISimpleTreeItem>(versions.Count, versionItems);
    }
}