using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class MyMenu : SimpleMenu
{
    public override string Name => "My Tree Menu";
    public override string[] Sections => [jcdcdev.Umbraco.Core.Constants.Sections.Members, jcdcdev.Umbraco.Core.Constants.Sections.Media];
}