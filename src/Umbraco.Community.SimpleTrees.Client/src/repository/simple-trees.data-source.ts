import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";
import { tryExecuteAndNotify } from "@umbraco-cms/backoffice/resources";
import { SimpleTrees } from "../api";
import { SimpleTreesChildrenOfRequestArgs, SimpleTreesRootItemsRequestArgs } from "../tree/types.ts";

export class SimpleTreesDataSource {

	#host: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		this.#host = host;

	}

	async getRoot(args: SimpleTreesRootItemsRequestArgs) {
		const options = {
			query: {
				skip: args.skip,
				take: args.take,
				foldersOnly: args.foldersOnly,
				treeAlias: args.treeAlias
			}
		}
		return await tryExecuteAndNotify(this.#host, SimpleTrees.getUmbracoSimpleTreesApiV1TreeRoot(options));
	}

	async getChildren(args: SimpleTreesChildrenOfRequestArgs) {
		if (!args.parent.unique) {
			return await this.getRoot(args);
		}

		const options = {
			query: {
				treeAlias: args.treeAlias,
				entityType: args.parent.entityType,
				parentUnique: args.parent.unique,
				skip: args.skip,
				take: args.take,
				foldersOnly: args.foldersOnly,
			}
		}

		return await tryExecuteAndNotify(this.#host, SimpleTrees.getUmbracoSimpleTreesApiV1TreeItems(options));
	}
}