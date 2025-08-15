using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.SimpleTrees.Core.Composing.Collections;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing;

public static class UmbracoBuilderExtensions
{
    public static void AddSimpleTrees(this IUmbracoBuilder builder)
    {
        builder.SimpleTrees();
        var types = builder.TypeLoader.GetTypes<ISimpleTree>();
        foreach (var type in types)
        {
            builder.SimpleTrees().Append(type);
        }

        builder.SimpleMenus();
        var menus = builder.TypeLoader.GetTypes<ISimpleMenu>();
        foreach (var type in menus)
        {
            builder.SimpleMenus().Append(type);
        }

        builder.SimpleEntityExecuteActions();
        var entityExecuteActions = builder.TypeLoader.GetTypes<ISimpleEntityExecuteAction>();
        foreach (var type in entityExecuteActions)
        {
            builder.SimpleEntityExecuteActions().Append(type);
        }

        builder.SimpleEntityUrlActions();
        var entityUrlActions = builder.TypeLoader.GetTypes<ISimpleEntityUrlAction>();
        foreach (var type in entityUrlActions)
        {
            builder.SimpleEntityUrlActions().Append(type);
        }   
        
        builder.SimpleEntityTypes();
        var entityTypes = builder.TypeLoader.GetTypes<ISimpleEntityType>();
        foreach (var type in entityTypes)
        {
            builder.SimpleEntityTypes().Append(type);
        }

        builder.Services.ConfigureOptions<ConfigApiSwaggerGenOptions>();
        builder.Services.AddSingleton<IPackageManifestReader, PackageManifestReader>();
        builder.Services.AddSingleton<ISimpleTreeService, SimpleTreeService>();
        builder.Services.AddSingleton<ISimpleTreeContext, SimpleTreeContext>();
    }

    private static SimpleTreeCollectionBuilder SimpleTrees(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleTreeCollectionBuilder>();
    private static SimpleMenuCollectionBuilder SimpleMenus(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleMenuCollectionBuilder>();
    private static SimpleEntityExecuteActionCollectionBuilder SimpleEntityExecuteActions(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleEntityExecuteActionCollectionBuilder>();
    private static SimpleEntityUrlActionCollectionBuilder SimpleEntityUrlActions(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleEntityUrlActionCollectionBuilder>();
    private static SimpleEntityTypeCollectionBuilder SimpleEntityTypes(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleEntityTypeCollectionBuilder>();

}