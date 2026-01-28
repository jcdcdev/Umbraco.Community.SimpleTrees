import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";
import { UmbDataSourceResponse } from "@umbraco-cms/backoffice/repository";
import { tryExecute } from "@umbraco-cms/backoffice/resources";
import { GetUmbracoSimpleTreesApiV1TreeRootResponse, SimpleTrees } from "../api";
import { SimpleTreesChildrenOfRequestArgs, SimpleTreesRootItemsRequestArgs } from "../tree/types.ts";

export class SimpleTreesDataSource {

	#host: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		this.#host = host;
	}

	async getRoot(args: SimpleTreesRootItemsRequestArgs): Promise<UmbDataSourceResponse<GetUmbracoSimpleTreesApiV1TreeRootResponse>> {
		const options = {
			query: {
				skip: args.skip,
				take: args.take,
				foldersOnly: args.foldersOnly,
				treeAlias: args.treeAlias
			}
		}
		return await tryExecute(this.#host, SimpleTrees.getUmbracoSimpleTreesApiV1TreeRoot(options));
	}

	async getChildren(args: SimpleTreesChildrenOfRequestArgs): Promise<UmbDataSourceResponse<GetUmbracoSimpleTreesApiV1TreeRootResponse>> {
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

		return await tryExecute(this.#host, SimpleTrees.getUmbracoSimpleTreesApiV1TreeItems(options));
	}
}