namespace Umbraco.Community.SimpleTrees.Core.Models;

public interface ISimpleEntityUrlAction : ISimpleEntityAction
{
    Task<Uri> GetUrlAsync(string unique, string entityType);
}