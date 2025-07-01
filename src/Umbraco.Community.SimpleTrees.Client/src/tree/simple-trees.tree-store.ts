import {UmbUniqueTreeStore} from "@umbraco-cms/backoffice/tree";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";

export class SimpleTreesTreeStore extends UmbUniqueTreeStore {
	constructor(host: UmbControllerHost) {
		super(host, SIMPLE_TREES_TREE_STORE_CONTEXT.toString());
	}
}

export const SIMPLE_TREES_TREE_STORE_CONTEXT = new UmbContextToken<SimpleTreesTreeStore>(
	'SimpleTreesTreeStore'
);