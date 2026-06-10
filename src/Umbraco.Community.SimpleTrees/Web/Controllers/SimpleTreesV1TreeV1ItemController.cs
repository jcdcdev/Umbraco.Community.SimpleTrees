using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[SimpleTreesVersionedRoute("tree")]
public class SimpleTreesV1TreeV1ItemController(ISimpleTreeService service) : SimpleTreesV1ApiControllerBase(service)
{
    [HttpGet("items/paged-offset")]
    [ProducesResponseType(typeof(PagedViewModel<SimpleTreeItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedViewModel<SimpleTreeItemResponse>>> Get(
        string treeAlias,
        string entityType,
        string parentUnique,
        int skip = 0,
        int take = 100,
        bool foldersOnly = false)
    {
        var result = await Service.GetPagedOffsetTreeChildrenAsync(treeAlias, entityType, parentUnique, skip, take, foldersOnly);
        var model = MapToResponse(result);
        return Ok(model);
    }

    [HttpGet("items/paged-target")]
    [ProducesResponseType(typeof(TargetPagedModel<SimpleTreeItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<TargetPagedModel<SimpleTreeItemResponse>>> GetPaged(
        string treeAlias,
        string targetUnique,
        string targetEntityType,
        string parentUnique,
        string entityType,
        int takeBefore,
        int takeAfter,
        bool foldersOnly = false
    )
    {
        var result = await Service.GetPagedTargetTreeChildrenAsync(treeAlias, targetUnique, targetEntityType, entityType, parentUnique, takeBefore, takeAfter, foldersOnly);
        var mapped = MapToResponse(result);
        var model = new TargetPagedModel<SimpleTreeItemResponse>
        {
            Items = mapped.Items,
            Total = mapped.Total,
            TotalBefore = result.TotalBefore,
            TotalAfter = result.TotalAfter
        };

        return Ok(model);
    }
}