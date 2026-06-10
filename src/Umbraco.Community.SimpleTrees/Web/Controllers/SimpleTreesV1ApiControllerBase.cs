using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.Filters;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Core.Models;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[SimpleTreesVersionedRoute("")]
[JsonOptionsName(Cms.Core.Constants.JsonOptionsNames.BackOffice)]
[MapToApi(Constants.Api.ApiName)]
[ApiController]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SimpleTreesV1ApiControllerBase(ISimpleTreeService service) : Controller
{
    protected readonly ISimpleTreeService Service = service;

    protected static PagedViewModel<TItem> PagedViewModel<TItem>(IEnumerable<TItem> items, long totalItems) => new() { Items = items, Total = totalItems };
    protected static PagedViewModel<SimpleTreeItemResponse> MapToResponse(PagedModel<ISimpleTreeItem> result)
    {
        var items = result.Items.Select(x =>
        {
            var parent = x.Parent == null
                ? null
                : new SimpleTreeItemParentResponse
                {
                    Unique = x.Parent.Unique,
                    EntityType = x.Parent.EntityType
                };

            return new SimpleTreeItemResponse
            {
                HasChildren = x.HasChildren,
                Name = x.Name,
                IsFolder = x.IsFolder,
                Icon = x.Icon,
                EntityType = x.EntityType,
                Unique = x.Unique,
                Parent = parent
            };
        });
        return PagedViewModel(items, result.Total);;
    }
    
}