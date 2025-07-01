using jcdcdev.Umbraco.Core.Web.Models.Manifests;

namespace Umbraco.Community.SimpleTrees.Models;

public interface ISimpleMenu
{
    string Alias { get; }
    string Name { get; }
    public IConditionManifest[] Conditions { get; }
    string[] Sections { get; }
}