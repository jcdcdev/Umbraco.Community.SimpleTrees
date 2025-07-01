using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.Core;

public class SimpleMenuCollection(Func<IEnumerable<ISimpleMenu>> items) : BuilderCollectionBase<ISimpleMenu>(items);