using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

internal class SimpleEntityUrlActionCollectionBuilder : OrderedCollectionBuilderBase<SimpleEntityUrlActionCollectionBuilder, SimpleEntityUrlActionCollection, ISimpleEntityUrlAction>
{
    protected override SimpleEntityUrlActionCollectionBuilder This => this;
}