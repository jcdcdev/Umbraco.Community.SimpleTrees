using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[SimpleTreesVersionedRoute("tree")]
public class SimpleTreesV1TreeRootV1Controller(ISimpleTreeService service) : SimpleTreesV1ApiControllerBase(service)
{
    [HttpGet("root/paged-offset")]
    [ProducesResponseType(typeof(PagedViewModel<SimpleTreeItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedViewModel<SimpleTreeItemResponse>>> Get(string treeAlias, int skip = 0, int take = 100, bool foldersOnly = false)
    {
        var result = await Service.GetTreeRootAsync(treeAlias, skip, take, foldersOnly);
        var model = MapToResponse(result);
        return Ok(model);
    }

    [HttpGet("root/paged-target")]
    [ProducesResponseType(typeof(TargetPagedModel<SimpleTreeItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<TargetPagedModel<SimpleTreeItemResponse>>> GetPaged(
        string treeAlias,
        string? unique,
        string entityType,
        int takeBefore,
        int takeAfter,
        bool foldersOnly = false
    )
    {
        var result = await Service.GetTreeRootAsync(treeAlias, unique, entityType, takeBefore, takeAfter, foldersOnly);
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