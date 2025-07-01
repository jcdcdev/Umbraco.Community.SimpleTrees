import {UmbTreeChildrenOfRequestArgs, UmbTreeItemModel, UmbTreeRootModel, UmbTreeAncestorsOfRequestArgs} from "@umbraco-cms/backoffice/tree"
import type {UmbTreeRootItemsRequestArgs} from "@umbraco-cms/backoffice/tree";
import type {UmbEntityModel} from "@umbraco-cms/backoffice/entity";

export interface SimpleTreesTreeItemModel extends UmbTreeItemModel {
	entityType: string;
}

export interface SimpleTreesTreeRootModel extends UmbTreeRootModel {
	entityType: string;
}

export interface SimpleTreesRootItemsRequestArgs extends UmbTreeRootItemsRequestArgs {
	treeAlias: string;
	foldersOnly?: boolean;
	skip?: number;
	take?: number;
}

export interface SimpleTreesChildrenOfRequestArgs extends UmbTreeChildrenOfRequestArgs {
	parent: UmbEntityModel;
	foldersOnly?: boolean;
	skip?: number;
	take?: number;
	treeAlias: string;
}

export interface SimpleTreesAncestorsOfRequestArgs extends UmbTreeAncestorsOfRequestArgs {
	treeItem: {
		unique: string;
		entityType: string;
		treeAlias: string;
	};
}
