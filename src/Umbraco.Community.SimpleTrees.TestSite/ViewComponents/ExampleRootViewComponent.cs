using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleTrees.Web;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.ViewComponents;

public class ExampleRootViewComponent : SimpleTreeViewComponent
{
    public override IViewComponentResult Invoke(SimpleTreeViewModel model)
    {
        return Content($"Hello {model.Unique} {model.EntityType} {DateTime.UtcNow:HH:mm:ss}");
    }
}