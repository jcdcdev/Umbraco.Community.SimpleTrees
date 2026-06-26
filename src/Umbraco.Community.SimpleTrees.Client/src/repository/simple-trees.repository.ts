import {UmbTreeItemModel, UmbTreeRepository} from "@umbraco-cms/backoffice/tree";
import {
	SimpleTreesChildrenOfRequestArgs,
	SimpleTreesRootItemsRequestArgs,
	SimpleTreesAncestorsOfRequestArgs,
	SimpleTreesTreeItemModel,
	SimpleTreesTreeRootModel
} from "../tree/types.ts";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {SimpleTreesTreeServerDataSource} from "../tree/simple-trees.server-data-source.ts";
import {UmbApi} from "@umbraco-cms/backoffice/extension-api";
import {tryExecute} from "@umbraco-cms/backoffice/resources";
import {SimpleEntityActionRequest, SimpleTrees} from "../api";
import {UmbRepositoryBase, UmbRepositoryResponseWithAsObservable, UmbTargetPagedModel} from "@umbraco-cms/backoffice/repository";

export class SimpleTreesRepository
	extends UmbRepositoryBase
	implements UmbTreeRepository<SimpleTreesTreeItemModel, SimpleTreesTreeRootModel, SimpleTreesRootItemsRequestArgs, SimpleTreesChildrenOfRequestArgs, SimpleTreesAncestorsOfRequestArgs>, UmbApi {
	_treeAlias?: string;
	_treeName: string = '';
	_rootEntityType: string = '';
	_dataSource: SimpleTreesTreeServerDataSource

	constructor(host: UmbControllerHost) {
		super(host);
		// @ts-ignore
		const _manifest = host.manifest;
		if (_manifest) {
			this._treeAlias = _manifest.alias;
			this._treeName = _manifest.name;
			this._rootEntityType = _manifest.meta.rootEntityType;
		}
		this._dataSource = new SimpleTreesTreeServerDataSource(host);
	}

	async requestTreeRoot() {
		if (!this._treeAlias || this._treeAlias === '') {
			throw new Error('Tree alias is not defined. Please ensure the tree alias is set in the manifest.');
		}

		const options: SimpleTreesRootItemsRequestArgs = {
			treeAlias: this._treeAlias,
		};
		const {data: treeRootData} = await this._dataSource.getRootItems(options);
		const hasChildren = treeRootData ? treeRootData.total > 0 : false;

		const data: SimpleTreesTreeRootModel = {
			unique: null,
			entityType: this._rootEntityType,
			name: this._treeName,
			hasChildren,
			isFolder: true,
		};

		return {data};
	}

	async requestTreeRootItems(args: SimpleTreesRootItemsRequestArgs): Promise<UmbRepositoryResponseWithAsObservable<UmbTargetPagedModel<UmbTreeItemModel>, UmbTreeItemModel[]>> {
		debugger;
		const options: SimpleTreesRootItemsRequestArgs = {
			...args,
			treeAlias: this._treeAlias!,
		};

		const items = await this._dataSource.getRootItems(options)
		return items;
	}

	async requestTreeItemsOf(args: SimpleTreesChildrenOfRequestArgs) {
		debugger;
		const options: SimpleTreesChildrenOfRequestArgs = {
			...args,
			treeAlias: this._treeAlias!,
		};

		const items = await this._dataSource.getChildrenOf(options)
		return items;
	}

	async requestTreeItemAncestors(args: SimpleTreesAncestorsOfRequestArgs) {
		debugger;
		const options: SimpleTreesAncestorsOfRequestArgs = {
			...args,
		};

		options.treeItem.treeAlias = this._treeAlias!;
		const items = await this._dataSource.getAncestorsOf(options)
		return items;
	}


	async render(unique: string, entityType: string) {
		const options = {
			query: {
				unique: unique,
				entityType: entityType,
			}
		};
		return await tryExecute(this._host, SimpleTrees.getTreeRender(options));
	}

	async runEntityExecuteAction(entityType: string, unique: string, actionAlias: string) {
		const body: SimpleEntityActionRequest = {
			entityType: entityType,
			unique: unique,
			actionAlias: actionAlias,
		}
		const options = {
			body: body
		};

		return await tryExecute(this._host, SimpleTrees.postEntityActionExecute(options));
	}

	async runEntityUrlAction(entityType: string, unique: string, actionAlias: string) {
		const body: SimpleEntityActionRequest = {
			entityType: entityType,
			unique: unique,
			actionAlias: actionAlias,
		}
		const options = {
			body: body
		};

		return await tryExecute(this._host, SimpleTrees.postEntityActionUrl(options));
	}
}

export default SimpleTreesRepository;
