using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleTrees.Web.Controllers;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web;

public abstract class SimpleTreeAsyncViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(SimpleTreeViewModel model);
}
