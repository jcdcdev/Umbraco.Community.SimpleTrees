using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

internal class SimpleMenuCollectionBuilder : OrderedCollectionBuilderBase<SimpleMenuCollectionBuilder, SimpleMenuCollection, ISimpleMenu>
{
    protected override SimpleMenuCollectionBuilder This => this;
}