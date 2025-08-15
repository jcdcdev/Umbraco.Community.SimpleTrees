using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public interface ISimpleEntityExecuteAction : ISimpleEntityAction
{
    Task<SimpleEntityActionExecuteResponse> ExecuteAsync(string unique, string entityType);
}