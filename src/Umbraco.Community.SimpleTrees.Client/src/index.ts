import {UmbEntryPointOnInit} from "@umbraco-cms/backoffice/extension-api";
import SimpleTreesRepository from "./repository/simple-trees.repository.ts";
import {ManifestRepository} from "@umbraco-cms/backoffice/extension-registry";
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


export const manifests = [treeRepository, ...ManifestLocalizations];
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
};