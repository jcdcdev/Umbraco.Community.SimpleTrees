using System.Text.Json.Serialization;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class MarketplaceRequest
{
    [JsonPropertyName("PackageId")] public required string PackageId { get; set; }
}