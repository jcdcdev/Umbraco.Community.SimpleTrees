using System.Reflection;

namespace Umbraco.Community.SimpleTrees;

public static class Constants
{
    public static class Api
    {
        public const string ApiName = "SimpleTrees";
        public const string Title = "Simple Trees Api";
        public const string Description = "API for Simple Trees";
        public const string GroupName = "Simple Trees";
    }

    public const string PackageName = "Umbraco.Community.SimpleTrees";

    public const string ErrorView =
        """
        <div>
            <h1>Tree Not Found</h1>
        </div>
        """;

    public const string ErrorViewPath = "~/Views/Trees/ViewNotFound.cshtml";

    public const string ExampleView =
        """
        @inherits Umbraco.Community.SimpleTrees.Web.SimpleTreeViewPage

        <uui-box headline="This is my custom tree item">
        	<div>
        		@Model.Unique - @Model.EntityType
        	</div>
        </uui-box>
        """;

    private static readonly string NameSpace = Assembly.GetEntryAssembly()?.GetName()?.Name ?? "YourNamespace";

    public static object ExampleViewComponent(string name)
    {
        return $$"""
                 using Microsoft.AspNetCore.Mvc;
                 using Umbraco.Community.SimpleSimpleTrees.Web;

                 namespace {{NameSpace}}.Views.Components;

                 public class {{name}}ViewComponent : SimpleTreeViewComponent
                 {
                     public override IViewComponentResult Invoke(SimpleTreeViewModel model)
                     {
                         return Content($"Hello {model.SimpleTree.Alias}");
                     }
                 }
                 """;
    }
}