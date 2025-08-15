using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

public class SimpleMenuCollection(Func<IEnumerable<ISimpleMenu>> items) : BuilderCollectionBase<ISimpleMenu>(items);