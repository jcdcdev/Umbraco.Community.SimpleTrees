using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.Core;

internal class SimpleMenuCollectionBuilder : OrderedCollectionBuilderBase<SimpleMenuCollectionBuilder, SimpleMenuCollection, ISimpleMenu>
{
    protected override SimpleMenuCollectionBuilder This => this;
}