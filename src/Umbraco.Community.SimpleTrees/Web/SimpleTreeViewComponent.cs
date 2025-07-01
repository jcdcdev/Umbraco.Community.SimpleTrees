using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleTrees.Web.Models;

namespace Umbraco.Community.SimpleTrees.Web;

public abstract class SimpleTreeViewComponent : ViewComponent
{
    public abstract IViewComponentResult Invoke(SimpleTreeViewModel model);
}
