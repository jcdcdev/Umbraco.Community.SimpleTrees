using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[ApiVersion("1.0")]
[SimpleTreesVersionedRoute("tree")]
public class SimpleTreesTreeItemController(ISimpleTreeService service) : SimpleTreesApiControllerBase(service)
{
    [HttpGet("items")]
    [ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PagedViewModel<SimpleTreeItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedViewModel<SimpleTreeItemResponse>>> Get(
        string treeAlias,
        string entityType,
        string parentUnique,
        int skip = 0,
        int take = 100,
        bool foldersOnly = false)
    {
        var result = await Service.GetTreeChildrenAsync(treeAlias, entityType, parentUnique, skip, take, foldersOnly);
        var model = MapToResponse(result);
        return Ok(model);
    }
}