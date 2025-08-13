using System.Net.Http.Headers;
using Umbraco.Community.SimpleTrees.Core.Models;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class NuGetPackageItemEntityExecuteAction : SimpleEntityExecuteAction
{
    private readonly HttpClient _client;

    public NuGetPackageItemEntityExecuteAction(HttpClient client)
    {
        var url = "https://functions.marketplace.umbraco.com/api/";
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Umbraco.Community.SimpleTrees.TestSite", "1.0"));
        _client = client;
    }

    public override string Icon => "icon-refresh";
    public override string Name => "Sync Package";
    public override Type[] ForTreeItems => [typeof(NuGetPackageTree)];

    public override async Task<SimpleEntityActionExecuteResponse> ExecuteAsync(string unique, string entityType)
    {
        try
        {
            var split = unique.Split('_');
            var packageId = split[0];
            var model = new MarketplaceRequest
            {
                PackageId = packageId,
            };

            var result = await _client.PostAsJsonAsync("InitiateSinglePackageSyncFunction", model);
            if (!result.IsSuccessStatusCode)
            {
                return SimpleEntityActionExecuteResponse.Error("Failed to initiate package update", $"Status Code: {result.StatusCode}, Reason: {result.ReasonPhrase}");
            }

            var message = $"Package {packageId} update has been initiated. You can check the progress in the Umbraco Marketplace.";
            return SimpleEntityActionExecuteResponse.Success("Package update initiated successfully", message);
        }
        catch (Exception ex)
        {
            return SimpleEntityActionExecuteResponse.Error("An error occurred while initiating the package update", ex.Message);
        }
    }
}