using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

public class SimpleTreeCollection(Func<IEnumerable<ISimpleTree>> items) : BuilderCollectionBase<ISimpleTree>(items);

