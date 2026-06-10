using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Core.Composing.Collections;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[SimpleTreesVersionedRoute("entity-action")]
public class SimpleTreesV1EntityActionController(
    ISimpleTreeService service,
    SimpleEntityExecuteActionCollection executeActions,
    SimpleEntityUrlActionCollection urlActions)
    : SimpleTreesV1ApiControllerBase(service)
{
    [HttpPost("execute")]
    [ProducesResponseType(typeof(SimpleEntityActionExecuteResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<SimpleEntityActionExecuteResponse>> PostExecuteAction(SimpleEntityActionRequest request)
    {
        var action = executeActions.FirstOrDefault(x => x.Alias == request.ActionAlias);
        if (action == null)
        {
            return NotFound($"Action with alias '{request.ActionAlias}' not found.");
        }

        try
        {
            var model = await action.ExecuteAsync(request.Unique, request.EntityType);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return BadRequest(SimpleEntityActionExecuteResponse.Error($"Error executing action '{request.ActionAlias}'", ex.Message));
        }
    }

    [HttpPost("url")]
    [ProducesResponseType(typeof(SimpleEntityActionUrlResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<SimpleEntityActionUrlResponse>> PostUrlAction(SimpleEntityActionRequest request)
    {
        var action = urlActions.FirstOrDefault(x => x.Alias == request.ActionAlias);
        if (action == null)
        {
            return NotFound($"Action with alias '{request.ActionAlias}' not found.");
        }

        try
        {
            var model = await action.GetUrlAsync(request.Unique, request.EntityType);
            return Ok(new SimpleEntityActionUrlResponse(model.ToString()));
        }
        catch (Exception ex)
        {
            return BadRequest($"Error executing action '{request.ActionAlias}': {ex.Message}");
        }
    }
}