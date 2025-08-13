import {UMB_AUTH_CONTEXT} from "@umbraco-cms/backoffice/auth";
import {client} from './api';
import {UmbEntryPointOnInit} from "@umbraco-cms/backoffice/extension-api";
import SimpleTreesRepository from "./repository/simple-trees.repository.ts";
import {SimpleTreesTreeStore} from "./tree/simple-trees.tree-store.ts";
import {ManifestRepository, ManifestTreeStore} from "@umbraco-cms/backoffice/extension-registry";
import {SimpleTreesContext} from "./context/simple-trees.context.ts";
import {ManifestLocalizations} from "./lang/manifests.ts";
import {ManifestEntityAction} from "@umbraco-cms/backoffice/entity-action";
import {api as SimpleTreesExecuteEntityAction} from "./entity-action/simple-trees-execute.entity-action.ts";
import {api as SimpleTreesUrlEntityAction} from "./entity-action/simple-trees-url.entity-action.ts";

const SIMPLE_TREES_REPOSITORY_ALIAS = 'SimpleTrees.Repository';
const treeRepository: ManifestRepository = {
	type: 'repository',
	alias: SIMPLE_TREES_REPOSITORY_ALIAS,
	name: 'Relation Type Repository',
	api: SimpleTreesRepository,
};

const SIMPLE_TREES_TREE_STORE_ALIAS = 'SimpleTrees.TreeStore';
const treeStore: ManifestTreeStore = {
	type: 'treeStore',
	alias: SIMPLE_TREES_TREE_STORE_ALIAS,
	name: 'Relation Type tree Store',
	api: SimpleTreesTreeStore
};

export const manifests = [treeRepository, treeStore, ...ManifestLocalizations];
export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
	new SimpleTreesContext(_host)

	extensionRegistry.registerMany([
		...manifests
	]);

	const entityUrlActions: ManifestEntityAction[] = extensionRegistry.getByType("entityUrlAction");
	const entityExecuteActions: ManifestEntityAction[] = extensionRegistry.getByType("entityExecuteAction");

	for (const entityActionManifest of entityUrlActions) {
		entityActionManifest.api = SimpleTreesUrlEntityAction;
		entityActionManifest.type = 'entityAction';
		extensionRegistry.unregister(entityActionManifest.alias);
		extensionRegistry.register(entityActionManifest);
	}

	for (const entityActionManifest of entityExecuteActions) {
		entityActionManifest.api = SimpleTreesExecuteEntityAction;
		entityActionManifest.type = 'entityAction';
		extensionRegistry.unregister(entityActionManifest.alias);
		extensionRegistry.register(entityActionManifest);
	}

	_host.consumeContext(UMB_AUTH_CONTEXT, (_auth) => {
		if (!_auth) {
			console.error('No auth context found');
			return;
		}

		const config = _auth.getOpenApiConfiguration();
		client.setConfig({
			auth: config.token,
			baseUrl: config.base,
			credentials: config.credentials,
		});

		client.interceptors.request.use(async (request, _options) => {
			const token = await _auth.getLatestToken();
			request.headers.set('Authorization', `Bearer ${token}`);
			return request;
		});

	});
};