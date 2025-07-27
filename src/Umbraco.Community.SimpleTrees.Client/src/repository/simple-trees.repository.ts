import { UmbTreeRepositoryBase } from "@umbraco-cms/backoffice/tree";
import {
	SimpleTreesChildrenOfRequestArgs,
	SimpleTreesRootItemsRequestArgs,
	SimpleTreesAncestorsOfRequestArgs,
	SimpleTreesTreeItemModel,
	SimpleTreesTreeRootModel
} from "../tree/types.ts";
import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";
import { SimpleTreesTreeServerDataSource } from "../tree/simple-trees.server-data-source.ts";
import { UmbApi } from "@umbraco-cms/backoffice/extension-api";
import { SIMPLE_TREES_TREE_STORE_CONTEXT } from "../tree/simple-trees.tree-store.ts";
import { tryExecute } from "@umbraco-cms/backoffice/resources";
import { SimpleTrees } from "../api";

export class SimpleTreesRepository extends UmbTreeRepositoryBase<SimpleTreesTreeItemModel, SimpleTreesTreeRootModel, SimpleTreesRootItemsRequestArgs, SimpleTreesChildrenOfRequestArgs, SimpleTreesAncestorsOfRequestArgs> implements UmbApi {
	_treeAlias?: string;
	_treeName: string = '';
	_rootEntityType: string = '';

	constructor(host: UmbControllerHost) {
		super(host, SimpleTreesTreeServerDataSource, SIMPLE_TREES_TREE_STORE_CONTEXT);
		// @ts-ignore
		const _manifest = host.manifest;
		if (_manifest) {
			this._treeAlias = _manifest.alias;
			this._treeName = _manifest.name;
			this._rootEntityType = _manifest.meta.rootEntityType;
		}
	}

	async requestTreeRoot() {
		if (!this._treeAlias || this._treeAlias === '') {
			throw new Error('Tree alias is not defined. Please ensure the tree alias is set in the manifest.');
		}

		const options: SimpleTreesRootItemsRequestArgs = {
			treeAlias: this._treeAlias,
		};
		const { data: treeRootData } = await this._treeSource.getRootItems(options);
		const hasChildren = treeRootData ? treeRootData.total > 0 : false;

		const data: SimpleTreesTreeRootModel = {
			unique: null,
			entityType: this._rootEntityType,
			name: this._treeName,
			hasChildren,
			isFolder: true,
		};

		return { data };
	}

	async render(unique: string, entityType: string) {
		const options = {
			query: {
				unique: unique,
				entityType: entityType,
			}
		};
		return await tryExecute(this._host, SimpleTrees.getUmbracoSimpleTreesApiV1TreeRender(options));
	}
}

export default SimpleTreesRepository;
