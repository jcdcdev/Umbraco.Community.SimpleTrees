using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core.Composing.Collections;

public class SimpleEntityUrlActionCollection(Func<IEnumerable<ISimpleEntityUrlAction>> items) : BuilderCollectionBase<ISimpleEntityUrlAction>(items);