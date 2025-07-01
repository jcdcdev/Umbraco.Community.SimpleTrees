using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.Core;

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

        builder.Services.ConfigureOptions<ConfigApiSwaggerGenOptions>();
        builder.Services.AddSingleton<IPackageManifestReader, PackageManifestReader>();
        builder.Services.AddSingleton<ISimpleTreeService, SimpleTreeService>();
    }

    private static SimpleTreeCollectionBuilder SimpleTrees(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleTreeCollectionBuilder>();
    private static SimpleMenuCollectionBuilder SimpleMenus(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleMenuCollectionBuilder>();

    public class SimpleTreeCollectionBuilder : OrderedCollectionBuilderBase<SimpleTreeCollectionBuilder, SimpleTreeCollection, ISimpleTree>
    {
        protected override SimpleTreeCollectionBuilder This => this;
    }
}