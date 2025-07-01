using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.SimpleTrees.Core;

namespace Umbraco.Community.SimpleTrees;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddSimpleTrees();
    }
}