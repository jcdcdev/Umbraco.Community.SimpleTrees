using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.Filters;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Community.SimpleTrees.Core;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[SimpleTreesVersionedRoute("")]
[MapToApi(Constants.Api.ApiName)]
[JsonOptionsName(Cms.Core.Constants.JsonOptionsNames.BackOffice)]
[ApiController]
[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[Produces("application/json")]
public class SimpleTreesApiControllerBase(ISimpleTreeService service) : ControllerBase
{
    protected readonly ISimpleTreeService Service = service;

    protected static PagedViewModel<TItem> PagedViewModel<TItem>(IEnumerable<TItem> items, long totalItems) => new() { Items = items, Total = totalItems };
}