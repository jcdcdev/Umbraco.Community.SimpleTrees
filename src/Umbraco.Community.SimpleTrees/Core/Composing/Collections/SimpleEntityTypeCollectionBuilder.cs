using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

internal class SimpleEntityTypeCollectionBuilder : OrderedCollectionBuilderBase<SimpleEntityTypeCollectionBuilder, SimpleEntityTypeCollection, ISimpleEntityType>
{
    protected override SimpleEntityTypeCollectionBuilder This => this;
}