using Umbraco.Community.SimpleTrees.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleTreeViewModel(string unique, string entityType)
{
    public bool IsRoot => Unique.InvariantEquals("null");
    public string Unique { get; } = unique;
    public string EntityType { get; } = entityType;
    public string ViewPath => $"~/Views/Trees/{ViewName}.cshtml";
    public string ViewComponent => $"{ViewName}";
    private string ViewName => SimpleEntityType.GetViewNameFromAlias(EntityType);
}