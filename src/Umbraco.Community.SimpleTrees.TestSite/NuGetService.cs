using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Umbraco.Community.SimpleTrees.TestSite;

public class NuGetService
{
    public async Task<IEnumerable<IPackageSearchMetadata>> GetPackageMetadata(string packageId)
    {
        var logger = NullLogger.Instance;
        var cancellationToken = CancellationToken.None;
        var cache = new SourceCacheContext();

        var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        var resource = await repository.GetResourceAsync<PackageMetadataResource>(cancellationToken);

        var results = await resource.GetMetadataAsync(
            packageId,
            includePrerelease: true,
            includeUnlisted: false,
            cache,
            logger,
            cancellationToken);

        return results;
    }

    public async Task<IEnumerable<IPackageSearchMetadata>> GetPackages(string owner, int skip, int take)
    {
        var logger = NullLogger.Instance;
        var cancellationToken = CancellationToken.None;

        var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        var resource = await repository.GetResourceAsync<PackageSearchResource>(cancellationToken);
        var searchFilter = new SearchFilter(includePrerelease: true);

        var results = await resource.SearchAsync(
            $"owner:{owner}",
            searchFilter,
            skip: skip,
            take: take,
            logger,
            cancellationToken);
        
        return results;
    }
}