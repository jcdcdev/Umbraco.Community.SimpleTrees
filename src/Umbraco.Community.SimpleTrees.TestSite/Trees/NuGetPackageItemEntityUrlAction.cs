using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class NuGetPackageItemEntityUrlAction : SimpleEntityUrlAction
{
    public override string Icon => "icon-link";
    public override string Name => "Go to Package";
    public override Type[] ForTreeItems => [typeof(NuGetPackageTree)];
    public override Type[] ForSimpleEntityTypes => [typeof(NuGetPackageVersionEntityType)];

    public override Task<Uri> GetUrlAsync(string unique, string entityType)
    {
        var split = unique.Split('_');
        var packageId = split[0];
        var path = packageId;

        if (split.Length > 1)
        {
            var version = split[1];
            path += "/" + version + "#readme-body-tab";
        }

        var uri = new Uri("https://www.nuget.org/packages/" + path);
        return Task.FromResult(uri);
    }
}