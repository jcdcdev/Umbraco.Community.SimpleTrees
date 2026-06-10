import {UmbTreeServerDataSourceBase} from "@umbraco-cms/backoffice/tree";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {SimpleTreesDataSource} from "../repository/simple-trees.data-source.ts";
import {
	SimpleTreesAncestorsOfRequestArgs,
	SimpleTreesChildrenOfRequestArgs,
	SimpleTreesRootItemsRequestArgs,
	SimpleTreesTreeItemModel
} from "./types.ts";
import {UmbDataSourceResponse, UmbTargetPagedModel} from "@umbraco-cms/backoffice/repository";
import {SimpleTreeItemResponse} from "../api";

export class SimpleTreesTreeServerDataSource extends UmbTreeServerDataSourceBase<SimpleTreeItemResponse, SimpleTreesTreeItemModel, SimpleTreesRootItemsRequestArgs, SimpleTreesChildrenOfRequestArgs, SimpleTreesAncestorsOfRequestArgs> {
	constructor(host: UmbControllerHost) {
		const resource = new SimpleTreesDataSource(host);

		const getRootItems = async (args: SimpleTreesRootItemsRequestArgs) => {
			return await resource.getRoot(args);
		};

		const getChildrenOf = async (args: SimpleTreesChildrenOfRequestArgs): Promise<UmbDataSourceResponse<UmbTargetPagedModel<SimpleTreeItemResponse>>> => {
			return await resource.getChildren(args);
		};

		const mapper = (item: SimpleTreeItemResponse): SimpleTreesTreeItemModel => {
			return {
				unique: item.unique,
				entityType: item.entityType,
				parent: {
					unique: item.parent?.unique || null,
					entityType: item.parent?.entityType || ''
				},
				name: item.name,
				hasChildren: item.hasChildren,
				isFolder: item.isFolder,
				icon: item.icon
			};
		};

		function getAncestorsOf(args: SimpleTreesAncestorsOfRequestArgs): Promise<UmbDataSourceResponse<Array<SimpleTreeItemResponse>>> {
			console.log('getAncestorsOf', args);
			throw new Error('Method not implemented.');
		}

		super(host, {
			getRootItems,
			getChildrenOf,
			getAncestorsOf,
			mapper,
		});
	}
}