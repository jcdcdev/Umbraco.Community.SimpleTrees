using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

public class SimpleEntityExecuteActionCollection(Func<IEnumerable<ISimpleEntityExecuteAction>> items) : BuilderCollectionBase<ISimpleEntityExecuteAction>(items);