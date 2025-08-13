using Umbraco.Community.SimpleTrees.Core.Composing.Collections;

namespace Umbraco.Community.SimpleTrees.Core.Models;

public class SimpleTreeContext(SimpleEntityTypeCollection collection) : ISimpleTreeContext
{
    public IEnumerable<ISimpleEntityType> CustomEntityTypes => collection;
}