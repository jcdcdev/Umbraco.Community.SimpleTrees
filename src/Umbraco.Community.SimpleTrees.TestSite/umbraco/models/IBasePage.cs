using Umbraco.Cms.Core.Models.PublishedContent;

namespace Umbraco.Community.SimpleTrees.TestSite.umbraco.models;

/// <summary>Base Page</summary>
public partial interface IBasePage : IPublishedElement
{
    /// <summary>Grid Content</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "")]
    [global::System.Diagnostics.CodeAnalysis.MaybeNull]
    global::Umbraco.Cms.Core.Models.Blocks.BlockGridModel GridContent { get; }
}