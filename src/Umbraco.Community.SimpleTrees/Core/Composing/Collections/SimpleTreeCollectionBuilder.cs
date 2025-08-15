using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

public class SimpleTreeCollectionBuilder : OrderedCollectionBuilderBase<SimpleTreeCollectionBuilder, SimpleTreeCollection, ISimpleTree>
{
    protected override SimpleTreeCollectionBuilder This => this;
}