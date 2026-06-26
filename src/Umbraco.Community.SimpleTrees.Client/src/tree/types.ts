import {UmbTreeChildrenOfRequestArgs, UmbTreeItemModel, UmbTreeRootModel, UmbTreeAncestorsOfRequestArgs} from "@umbraco-cms/backoffice/tree"
import type {UmbTreeRootItemsRequestArgs} from "@umbraco-cms/backoffice/tree";
import type {UmbEntityModel} from "@umbraco-cms/backoffice/entity";
import type {UmbOffsetPaginationRequestModel, UmbTargetPaginationRequestModel} from "@umbraco-cms/backoffice/utils";

export interface SimpleTreesTreeItemModel extends UmbTreeItemModel {
	entityType: string;
}

export interface SimpleTreesTreeRootModel extends UmbTreeRootModel {
	entityType: string;
}

export interface SimpleTreesRootItemsRequestArgs extends UmbTreeRootItemsRequestArgs {
	treeAlias: string;
	foldersOnly?: boolean;
	paging?: UmbOffsetPaginationRequestModel | UmbTargetPaginationRequestModel;
}

export interface SimpleTreesChildrenOfRequestArgs extends UmbTreeChildrenOfRequestArgs {
	parent: UmbEntityModel;
	foldersOnly?: boolean;
	treeAlias: string;
	paging?: UmbOffsetPaginationRequestModel | UmbTargetPaginationRequestModel;
}

export interface SimpleTreesAncestorsOfRequestArgs extends UmbTreeAncestorsOfRequestArgs {
	treeItem: {
		unique: string;
		entityType: string;
		treeAlias: string;
	};
}
