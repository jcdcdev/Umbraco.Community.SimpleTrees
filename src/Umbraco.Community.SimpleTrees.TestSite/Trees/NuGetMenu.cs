using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class NuGetMenu : SimpleMenu
{
    public override string Name => "NuGet Packages";
    public override string[] Sections => [jcdcdev.Umbraco.Core.Constants.Sections.Content];
}