import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse, UmbTargetPagedModel} from "@umbraco-cms/backoffice/repository";
import {tryExecute} from "@umbraco-cms/backoffice/resources";
import {SimpleTreeItemResponse, SimpleTrees} from "../api";
import {SimpleTreesChildrenOfRequestArgs, SimpleTreesRootItemsRequestArgs} from "../tree/types.ts";
import {isOffsetPaginationRequest} from "@umbraco-cms/backoffice/utils";

export class SimpleTreesDataSource {

	#host: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		this.#host = host;
	}

	async getRoot(args: SimpleTreesRootItemsRequestArgs): Promise<UmbDataSourceResponse<UmbTargetPagedModel<SimpleTreeItemResponse>>> {
		if (!args.paging) {
			args.paging = {
				skip: 0,
				take: 10
			}
		}

		if (isOffsetPaginationRequest(args.paging)) {
			const options = {
				query: {
					skip: args.paging.skip,
					take: args.paging.take,
					foldersOnly: args.foldersOnly,
					treeAlias: args.treeAlias
				}
			}
			const result = await tryExecute(this.#host, SimpleTrees.getTreeRootPagedOffset(options));

			const total = parseInt(result.data.total.toString());
			return {
				data: {
					items: result.data.items,
					total: total,
					totalAfter: args.paging.skip + args.paging.take < total ? total - (args.paging.skip + args.paging.take) : 0,
					totalBefore: args.paging.skip
				}
			}
		}

		const options = {
			query: {
				takeBefore: args.paging.takeBefore,
				takeAfter: args.paging.takeAfter,
				entityType: args.paging.target.entityType,
				unique: args.paging.target.unique || undefined,
				foldersOnly: args.foldersOnly,
				treeAlias: args.treeAlias
			}
		};

		const result = await tryExecute(this.#host, SimpleTrees.getTreeRootPagedTarget(options));
		const total = parseInt(result.data.total.toString());
		return {
			data: {
				items: result.data.items,
				total: total,
				totalAfter: parseInt(result.data.totalAfter.toString()),
				totalBefore: parseInt(result.data.totalBefore.toString())
			}
		}
	}

	async getChildren(args: SimpleTreesChildrenOfRequestArgs): Promise<UmbDataSourceResponse<UmbTargetPagedModel<SimpleTreeItemResponse>>> {
		if (!args.paging) {
			args.paging = {
				skip: 0,
				take: 10
			}
		}
		if (isOffsetPaginationRequest(args.paging)) {
			const options = {
				query: {
					entityType: args.parent.entityType,
					parentUnique: args.parent.unique || undefined,
					skip: args.paging.skip,
					take: args.paging.take,
					foldersOnly: args.foldersOnly,
					treeAlias: args.treeAlias,
				}
			}
			const result = await tryExecute(this.#host, SimpleTrees.getTreeItemsPagedOffset(options));
			const total = parseInt(result.data.total.toString());
			return {
				data: {
					items: result.data.items,
					total: total,
					totalAfter: args.paging.skip + args.paging.take < total ? total - (args.paging.skip + args.paging.take) : 0,
					totalBefore: args.paging.skip
				}
			}
		}

		const options = {
			query: {
				takeBefore: args.paging.takeBefore,
				takeAfter: args.paging.takeAfter,
				entityType: args.parent.entityType,
				parentUnique: args.parent.unique || undefined,

				targetUnique: args.paging.target.unique!,
				targetEntityType: args.paging.target.entityType!,
				foldersOnly: args.foldersOnly,
				treeAlias: args.treeAlias
			}
		};

		const result = await tryExecute(this.#host, SimpleTrees.getTreeItemsPagedTarget(options));
		const total = parseInt(result.data.total.toString());
		return {
			data: {
				items: result.data.items,
				total: total,
				totalAfter: parseInt(result.data.totalAfter.toString()),
				totalBefore: parseInt(result.data.totalBefore.toString())
			}
		}
	}
}