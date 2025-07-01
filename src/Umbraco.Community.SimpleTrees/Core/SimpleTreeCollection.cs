using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.Core;

public class SimpleTreeCollection(Func<IEnumerable<ISimpleTree>> items) : BuilderCollectionBase<ISimpleTree>(items);