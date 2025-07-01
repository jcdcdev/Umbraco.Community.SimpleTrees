import {UmbRoutableWorkspaceContext, UmbWorkspaceContext, UmbWorkspaceRouteManager} from "@umbraco-cms/backoffice/workspace";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbPathPattern} from "@umbraco-cms/backoffice/router";
import {UmbContextBase} from "@umbraco-cms/backoffice/class-api";
import {SimpleTreesWorkspaceElement} from "./simple-trees-workspace.element.ts";
import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";

const SIMPLE_TREES_MANAGER_CONTEXT_TOKEN = 'SimpleTrees.Workspace';
const EDIT_SIMPLE_TREES_WORKSPACE_PATH_PATTERN = new UmbPathPattern('/edit/:id');

export class SimpleTreesWorkspaceContext
	extends UmbContextBase
	implements UmbWorkspaceContext, UmbRoutableWorkspaceContext {
	readonly routes: UmbWorkspaceRouteManager;

	constructor(host: UmbControllerHost) {
		super(host, SIMPLE_TREES_MANAGER_CONTEXT_TOKEN);
		this.routes = new UmbWorkspaceRouteManager(host);
		this.routes.setRoutes([
			{
				path: EDIT_SIMPLE_TREES_WORKSPACE_PATH_PATTERN.toString(),
				component: SimpleTreesWorkspaceElement,
				setup: (component, info) => {
					const element = component as SimpleTreesWorkspaceElement;
					// @ts-ignore
					element.entityType = host.manifest.meta.entityType;
					// @ts-ignore
					element.uniqueId = info.match.params.id;
				}
			}]);
	}

	destroy(): void {
		this.routes.destroy();
		super.destroy();
	}
	
	getEntityType = () => {
		// @ts-ignore
		return this._host.manifest.meta.entityType;
	};
	
	readonly workspaceAlias = 'simple-trees-workspace';
}

export const api = SimpleTreesWorkspaceContext;
export const UMB_APP_CONTEXT = new UmbContextToken<SimpleTreesWorkspaceContext>(
	"UmbWorkspaceContext",
	"simple-trees-workspace"
);
