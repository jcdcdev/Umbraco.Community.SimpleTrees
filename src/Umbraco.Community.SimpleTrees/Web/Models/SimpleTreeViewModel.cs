using Humanizer;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleTreeViewModel(string unique, string entityType)
{
    public bool IsRoot => Unique.InvariantEquals("null");
    public string Unique { get; } = unique;
    public string EntityType { get; } = entityType;
    public string ViewPath => $"~/Views/Trees/{X}.cshtml";
    public string ViewComponent => $"{X}";

    private string X => string.Join("", EntityType.Split("-").Select(x => x.ToFirstUpperInvariant()));
}