using jcdcdev.Umbraco.Core.Web.Models.Manifests;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public abstract class SimpleMenu : ISimpleMenu
{
    public string Alias => GetType().Name;
    public abstract string Name { get; }
    public virtual IConditionManifest[] Conditions => BuildConditions().ToArray();
    public virtual string[] Sections => [jcdcdev.Umbraco.Core.Constants.Sections.Content];

    private List<IConditionManifest> BuildConditions()
    {
        var conditions = new List<IConditionManifest>();
        if (Sections.Any())
        {
            conditions.Add(ConditionManifest.SectionAlias(Sections));
        }

        return conditions;
    }
}