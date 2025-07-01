using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.Filters;
using Umbraco.Cms.Api.Management.Filters;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Community.SimpleTrees.Web.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleTrees.Web.Controllers;

[ApiExplorerSettings(GroupName = Constants.Api.GroupName)]
[SimpleTreesVersionedRoute("tree")]
[MapToApi(Constants.Api.ApiName)]
[JsonOptionsName(Cms.Core.Constants.JsonOptionsNames.BackOffice)]
[ApiController]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[AppendEventMessages]
[Produces("application/json")]
public class SimpleTreesController(
    ICompositeViewEngine viewEngine,
    ILogger<SimpleTreesController> logger,
    IViewComponentDescriptorProvider viewComponentDescriptorProvider,
    AppCaches appCaches)
    : Controller
{
    private readonly ILogger _logger = logger;
    private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

    [HttpGet("render")]
    [Produces<SimpleTreeRenderModel>]
    public async Task<IActionResult> Render(string unique, string entityType)
    {
        var model = new SimpleTreeViewModel(unique, entityType);
        var path = model.ViewPath;
        var result = viewEngine.GetView(null, path, false);
        if (result.Success)
        {
            var body = await RenderAsync(result, model);
            return Ok(body);
        }

        var viewComponentName = model.ViewComponent;
        if (ViewComponentExists(viewComponentName))
        {
            var body = await RenderAsync(viewComponentName, model);
            return Ok(body);
        }

        return await ReturnError(model);
    }

    private async Task<IActionResult> ReturnError(SimpleTreeViewModel viewModel)
    {
        var result = viewEngine.GetView(null, Constants.ErrorViewPath, false);
        var body = await RenderAsync(result, viewModel);
        return Ok(body);
    }

    private async Task<SimpleTreeRenderModel> RenderAsync(string viewComponentName, SimpleTreeViewModel viewModel)
    {
        var sp = HttpContext.RequestServices;

        var helper = new DefaultViewComponentHelper(
            sp.GetRequiredService<IViewComponentDescriptorCollectionProvider>(),
            HtmlEncoder.Default,
            sp.GetRequiredService<IViewComponentSelector>(),
            sp.GetRequiredService<IViewComponentInvokerFactory>(),
            sp.GetRequiredService<IViewBufferScope>());
        await using var writer = new StringWriter();
        var context = new ViewContext(ControllerContext, NullView.Instance, ViewData, TempData, writer, new HtmlHelperOptions());
        helper.Contextualize(context);
        var vcResult = await helper.InvokeAsync(viewComponentName, new { Model = viewModel });
        vcResult.WriteTo(writer, HtmlEncoder.Default);
        await writer.FlushAsync();
        var body = writer.ToString();
        return new SimpleTreeRenderModel
        {
            Body = body
        };
    }

    private async Task<SimpleTreeRenderModel> RenderAsync(ViewEngineResult result, object? model)
    {
        if (result.View == null)
        {
            return SimpleTreeRenderModel.Error;
        }

        var writer = new StringWriter();
        var viewContext = new ViewContext(new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState), result.View, ViewData, TempData, writer, new HtmlHelperOptions())
        {
            ViewData =
            {
                Model = model
            }
        };

        await result.View.RenderAsync(viewContext);
        var body = writer.ToString();
        return new SimpleTreeRenderModel
        {
            Body = body
        };
    }

    private bool ViewComponentExists(string viewComponentName)
    {
        return _runtimeCache.GetCacheItem(viewComponentName, () =>
        {
            var viewComponentDescriptors = viewComponentDescriptorProvider.GetViewComponents();
            return viewComponentDescriptors.Any(vc => vc.ShortName == viewComponentName);
        });
    }
}