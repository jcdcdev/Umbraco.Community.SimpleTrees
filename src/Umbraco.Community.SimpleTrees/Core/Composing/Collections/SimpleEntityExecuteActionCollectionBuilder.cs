using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

internal class SimpleEntityExecuteActionCollectionBuilder : OrderedCollectionBuilderBase<SimpleEntityExecuteActionCollectionBuilder, SimpleEntityExecuteActionCollection, ISimpleEntityExecuteAction>
{
    protected override SimpleEntityExecuteActionCollectionBuilder This => this;
}