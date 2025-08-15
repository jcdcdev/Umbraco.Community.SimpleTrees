using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

public class SimpleEntityTypeCollection(Func<IEnumerable<ISimpleEntityType>> items) : BuilderCollectionBase<ISimpleEntityType>(items);