import { UmbTreeServerDataSourceBase } from "@umbraco-cms/backoffice/tree";
import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";
import { SimpleTreesDataSource } from "../repository/simple-trees.data-source.ts";
import {
	SimpleTreesAncestorsOfRequestArgs,
	SimpleTreesChildrenOfRequestArgs,
	SimpleTreesRootItemsRequestArgs,
	SimpleTreesTreeItemModel
} from "./types.ts";
import { SimpleTreeItemResponse } from "../api";

export class SimpleTreesTreeServerDataSource extends UmbTreeServerDataSourceBase<SimpleTreeItemResponse, SimpleTreesTreeItemModel, SimpleTreesRootItemsRequestArgs, SimpleTreesChildrenOfRequestArgs, SimpleTreesAncestorsOfRequestArgs> {
	resource?: SimpleTreesDataSource;
	_host?: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		const getRootItems = async (args: SimpleTreesRootItemsRequestArgs) => {
			const results = await this.resource?.getRoot(args)!;
			return {
				total: results.data?.data.total || 0,
				items: results.data?.data.items || []
			}

		};

		const getChildrenOf = async (args: SimpleTreesChildrenOfRequestArgs) => {
			// @ts-ignore
			args.treeAlias = this._host?._treeAlias;
			const results = await this.resource?.getChildren(args)!;
			return {
				total: results.data?.data.total || 0,
				items: results.data?.data.items || []
			}
		};

		const mapper = (item: SimpleTreeItemResponse): SimpleTreesTreeItemModel => {
			return {
				unique: item.unique,
				// @ts-ignore
				entityType: item.entityType,
				// @ts-ignore
				parent: item.parent ? {
					unique: item.parent.unique,
				} : undefined,
				name: item.name,
				hasChildren: item.hasChildren,
				isFolder: item.isFolder,
				icon: item.icon
			};
		};

		function getAncestorsOf(args: SimpleTreesAncestorsOfRequestArgs): Promise<SimpleTreeItemResponse[]> {
			console.log('getAncestorsOf', args);
			throw new Error('Method not implemented.');
		}

		super(host, {
			getRootItems,
			getChildrenOf,
			getAncestorsOf,
			mapper,
		});

		this._host = host;
		this.resource = new SimpleTreesDataSource(host);
	}
}