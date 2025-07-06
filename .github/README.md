# Umbraco.Community.SimpleTrees

[![Umbraco Marketplace](https://img.shields.io/badge/Umbraco-Marketplace-%233544B1?style=flat&logo=umbraco)](https://marketplace.umbraco.com/package/Umbraco.Community.SimpleTrees)
[![License](https://img.shields.io/github/license/jcdcdev/Umbraco.Community.SimpleTrees?color=8AB803&label=License&logo=github)](https://github.com/jcdcdev/Umbraco.Community.SimpleTrees?tab=MIT-1-ov-file)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.SimpleTrees?color=cc9900&label=Downloads&logo=nuget)](https://www.nuget.org/packages/Umbraco.Community.SimpleTrees)
[![Project Website](https://img.shields.io/badge/Project%20Website-jcdc.dev-jcdcdev?style=flat&color=3c4834&logo=data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNiIgaGVpZ2h0PSIxNiIgZmlsbD0id2hpdGUiIGNsYXNzPSJiaSBiaS1wYy1kaXNwbGF5IiB2aWV3Qm94PSIwIDAgMTYgMTYiPgogIDxwYXRoIGQ9Ik04IDFhMSAxIDAgMCAxIDEtMWg2YTEgMSAwIDAgMSAxIDF2MTRhMSAxIDAgMCAxLTEgMUg5YTEgMSAwIDAgMS0xLTF6bTEgMTMuNWEuNS41IDAgMSAwIDEgMCAuNS41IDAgMCAwLTEgMG0yIDBhLjUuNSAwIDEgMCAxIDAgLjUuNSAwIDAgMC0xIDBNOS41IDFhLjUuNSAwIDAgMCAwIDFoNWEuNS41IDAgMCAwIDAtMXpNOSAzLjVhLjUuNSAwIDAgMCAuNS41aDVhLjUuNSAwIDAgMCAwLTFoLTVhLjUuNSAwIDAgMC0uNS41TTEuNSAyQTEuNSAxLjUgMCAwIDAgMCAzLjV2N0ExLjUgMS41IDAgMCAwIDEuNSAxMkg2djJoLS41YS41LjUgMCAwIDAgMCAxSDd2LTRIMS41YS41LjUgMCAwIDEtLjUtLjV2LTdhLjUuNSAwIDAgMSAuNS0uNUg3VjJ6Ii8+Cjwvc3ZnPg==)](https://jcdc.dev/umbraco-packages/simple-trees)


This packages aims to help developers quickly put together Umbraco Trees using C#.

## Features

- C# custom tree creation
- No javascript or umbraco-package.json files required
- Supports both Views & View Components
- Easy to define section permissions

## Quick Start

### Install Package

```csharp
dotnet add package Umbraco.Community.SimpleTrees 
```

### Register Tree

By default, this will display in the content section.

```csharp title="ExampleTree.cs"
using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Models;

namespace Umbraco.Community.SimpleTrees.TestSite.Trees;

public class MyTree : SimpleTree
{
    public override Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(int skip, int take, bool foldersOnly)
    {
        var data = new List<ISimpleTreeItem>
        {
            CreateRootItem("James", Guid.NewGuid().ToString(), "icon-user"),
            CreateRootItem("Tim", Guid.NewGuid().ToString(), "icon-user"),
        };

        return Task.FromResult(new PagedModel<ISimpleTreeItem>(data.Count, data));
    }

    public override Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string entityType, string parentUnique, int skip, int take, bool foldersOnly) => Task.FromResult(EmptyResult());

    public override string Name => "My Tree";
}
```

### Create Views

- Your views **must** go in `/Views/Trees`
- You views **must** be the name of your tree entities 
    - For example: `MyTree.cs` => `/Views/Trees/MyItem.cshtml` &  `/Views/Trees/MyRoot.cshtml`

```csharp title="Views/Trees/MyItem.cshtml"
@inherits Umbraco.Community.SimpleTrees.Web.SimpleTreeViewPage

<uui-box headline="This is a custom tree item">
	<div>
		<table>
			<thead>
			<tr>
				<th>Entity Type</th>
				<th>Unique</th>
			</tr>
			</thead>
			<tbody>
			<tr>
				<td>@Model.EntityType</td>
				<td>@Model.Unique</td>
			</tr>
		</table>
	</div>
</uui-box>
```



## Contributing

Contributions to this package are most welcome! Please visit the [Contributing](https://github.com/jcdcdev/Umbraco.Community.SimpleTrees/contribute) page.

## Acknowledgements (Thanks)

- LottePitcher - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)



