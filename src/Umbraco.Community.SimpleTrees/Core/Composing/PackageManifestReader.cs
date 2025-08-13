using System.Reflection;
using jcdcdev.Umbraco.Core.Extensions;
using jcdcdev.Umbraco.Core.Web.Models.Manifests;
using Umbraco.Cms.Core.Manifest;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.SimpleTrees.Core.Composing.Collections;
using Umbraco.Community.SimpleTrees.Core.Manifest;

namespace Umbraco.Community.SimpleTrees.Core.Composing;

public class PackageManifestReader(
    ISimpleTreeService simpleTreeService,
    SimpleEntityTypeCollection simpleEntityTypeCollection,
    SimpleMenuCollection simpleMenuCollection,
    SimpleEntityExecuteActionCollection entityExecuteActionCollection,
    SimpleEntityUrlActionCollection entityUrlActionCollection
)
    : IPackageManifestReader
{
    public Task<IEnumerable<PackageManifest>> ReadPackageManifestsAsync()
    {
        var extensions = new List<IManifest>();
        var packageManifest = new PackageManifest
        {
            Name = Constants.PackageName,
            Version = Assembly.GetAssembly(typeof(PackageManifestReader))?.GetName().Version?.ToSemVer()?.ToString() ?? "0.1.0",
            AllowPublicAccess = false,
            AllowTelemetry = true,
            Extensions = []
        };

        foreach (var menu in simpleMenuCollection)
        {
            var menuManifest = new MenuManifest
            {
                Alias = menu.Alias,
                Name = menu.Name,
            };

            var sidebarApp = new SectionSidebarAppManifest
            {
                Kind = "menu",
                Alias = menu.Alias + "SectionSidebarApp",
                Name = menu.Name,
                Meta = new SectionSidebarAppManifest.SectionSidebarAppManifestMeta
                {
                    Menu = menuManifest.Alias,
                    Label = menu.Name,
                    Sections = []
                },
                Conditions = menu.Conditions
            };

            extensions.Add(menuManifest);
            extensions.Add(sidebarApp);
        }

        var customTrees = simpleTreeService.GetAll().ToList();
        foreach (var customTree in customTrees)
        {
            var tree = new TreeManifest
            {
                Kind = "default",
                Alias = customTree.Alias,
                Name = customTree.Name,
                Meta = new TreeManifest.TreeManifestMeta
                {
                    RepositoryAlias = "SimpleTrees.Repository",
                    RootEntityType = customTree.DefaultRootEntityType,
                    EntityType = customTree.DefaultEntityType,
                }
            };

            var menuItem = new MenuItemTreeKindManifest
            {
                Kind = "tree",
                Alias = $"{customTree.Alias}MenuItem",
                Name = $"{customTree.Name} Menu Item",
                Weight = 0,
                Meta = new MenuItemTreeKindManifest.MenuItemTreeKindManifestMeta
                {
                    Label = customTree.Label,
                    TreeAlias = tree.Alias,
                    Menus = customTree.Menus,
                },
            };

            extensions.Add(menuItem);
            extensions.Add(tree);
        }

        var treeEntityTypes = customTrees.SelectMany(t => new List<string> { t.DefaultEntityType, t.DefaultRootEntityType }).Distinct().ToList();
        var customEntityTypes = simpleEntityTypeCollection
            .Select(x => x.Alias)
            .ToList();

        var forEntityTypes = treeEntityTypes
            .Union(customEntityTypes)
            .Distinct()
            .ToList();

        foreach (var entityType in forEntityTypes)
        {
            var workspace = new WorkspaceManifest
            {
                Kind = "routable",
                Alias = "SimpleTrees.Workspace" + entityType,
                Name = "Simple Trees Workspace",
                Js = "/App_Plugins/SimpleTrees/dist/simple-trees-workspace.context.js",
                Meta = new WorkspaceManifest.WorkspaceManifestMeta
                {
                    EntityType = entityType
                }
            };

            extensions.Add(workspace);
        }

        var treeItem = new TreeItemManifest
        {
            Kind = "default",
            Alias = "SimpleTrees.Tree.Item",
            Name = "Simple Tree Item",
            ForEntityTypes = forEntityTypes.ToArray()
        };

        extensions.Add(treeItem);

        extensions.Add(new BackofficeEntryPointManifest
        {
            Name = "simple-trees.entrypoint",
            Alias = "simple-trees.entrypoint",
            Js = "/App_Plugins/SimpleTrees/dist/index.js"
        });

        foreach (var entityAction in entityExecuteActionCollection)
        {
            var entityActionManifest = new EntityExecuteActionManifest
            {
                Alias = entityAction.Alias,
                Name = entityAction.Name,
                Weight = entityAction.Weight + 2,
                ForEntityTypes = entityAction.GetEntityTypes(customTrees, simpleEntityTypeCollection.ToList()).ToArray(),
                Meta = new EntityActionManifest.EntityActionManifestMeta
                {
                    Icon = entityAction.Icon,
                    Label = entityAction.Name
                }
            };

            extensions.Add(entityActionManifest);
        }

        foreach (var entityAction in entityUrlActionCollection)
        {
            var entityActionManifest = new EntityUrlActionManifest
            {
                Alias = entityAction.Alias,
                Name = entityAction.Name,
                Weight = entityAction.Weight + 1,
                ForEntityTypes = entityAction.GetEntityTypes(customTrees, simpleEntityTypeCollection.ToList()).ToArray(),
                Meta = new EntityActionManifest.EntityActionManifestMeta
                {
                    Icon = entityAction.Icon,
                    Label = entityAction.Name
                }
            };

            extensions.Add(entityActionManifest);
        }

        packageManifest.Extensions = extensions.OfType<object>().ToArray();
        return Task.FromResult<IEnumerable<PackageManifest>>([packageManifest]);
    }
}