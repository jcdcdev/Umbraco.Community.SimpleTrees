namespace Umbraco.Community.SimpleTrees.Core.Models;

public abstract class SimpleEntityUrlAction : SimpleEntityAction, ISimpleEntityUrlAction
{
    public abstract Task<Uri> GetUrlAsync(string unique, string entityType);
}