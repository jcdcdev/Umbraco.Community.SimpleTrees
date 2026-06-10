using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Community.SimpleTrees.Core;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[SimpleTreesVersionedRoute("tree")]
public class SimpleTreesRenderV1ApiController(
    ICompositeViewEngine viewEngine,
    ILogger<SimpleTreesRenderV1ApiController> logger,
    ISimpleTreeService service
) : SimpleTreesV1ApiControllerBase(service)
{
    [HttpGet("render")]
    [Produces<SimpleTreeRenderModel>]
    public async Task<IActionResult> Render(string unique, string entityType)
    {
        var model = new SimpleTreeViewModel(unique, entityType);
        var path = model.ViewPath;
        var result = viewEngine.GetView(null, path, false);
        if (result.Success)
        {
            logger.LogDebug("Rendering view {ViewPath} for unique {Unique} and entity type {EntityType}", path, unique, entityType);
            var partialViewResult = await this.RenderViewResultToStringAsync(result, model);
            return Ok(SimpleTreeRenderModel.Create(partialViewResult));
        }

        var viewComponentName = model.ViewComponent;
        if (!this.ViewComponentExists(viewComponentName))
        {
            logger.LogDebug("ViewComponent {ViewComponent} not found", viewComponentName);
            return await ReturnError(model);
        }

        var viewComponentResult = await this.RenderViewComponentToStringAsync(viewComponentName, model);
        logger.LogDebug("ViewComponent {ViewComponent} result for unique {Unique} and entity type {EntityType} {Body}", viewComponentName, unique, entityType, viewComponentResult);
        return Ok(SimpleTreeRenderModel.Create(viewComponentResult));
    }

    private async Task<IActionResult> ReturnError(SimpleTreeViewModel viewModel)
    {
        var errorViewResult = viewEngine.GetView(null, Constants.ErrorViewPath, false);
        if (errorViewResult.Success)
        {
            var body = await this.RenderViewResultToStringAsync(errorViewResult, viewModel);
            return Ok(SimpleTreeRenderModel.Create(body));
        }

        return Ok(SimpleTreeRenderModel.Error);
    }
}