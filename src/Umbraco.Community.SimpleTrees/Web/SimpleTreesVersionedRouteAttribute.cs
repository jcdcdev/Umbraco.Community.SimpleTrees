using Umbraco.Cms.Web.Common.Routing;

namespace Umbraco.Community.SimpleTrees.Web;

public class SimpleTreesVersionedRouteAttribute(string template) : BackOfficeRouteAttribute($"SimpleTrees/api/v{{version:apiVersion}}/{template.TrimStart('/')}");