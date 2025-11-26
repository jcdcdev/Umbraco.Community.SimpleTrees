using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[ApiVersion("1.0")]
[SimpleTreesVersionedRoute("tree")]
public class SimpleTreesTreeController(ISimpleTreeService service) : SimpleTreesApiControllerBase(service)
{
    [HttpGet("root")]
    [ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PagedViewModel<SimpleTreeItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedViewModel<SimpleTreeItemResponse>>> Get(string treeAlias, int skip = 0, int take = 100, bool foldersOnly = false)
    {
        var result = await Service.GetTreeRootAsync(treeAlias, skip, take, foldersOnly);
        var model = MapToResponse(result);
        return Ok(model);
    }
}