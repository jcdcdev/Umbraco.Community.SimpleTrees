using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public abstract class SimpleEntityExecuteAction : SimpleEntityAction, ISimpleEntityExecuteAction
{
    public abstract Task<SimpleEntityActionExecuteResponse> ExecuteAsync(string unique, string entityType);
}