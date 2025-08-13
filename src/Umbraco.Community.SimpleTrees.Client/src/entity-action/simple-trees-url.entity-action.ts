import {MetaEntityAction, UmbEntityActionBase, UmbEntityActionArgs} from '@umbraco-cms/backoffice/entity-action';
import {SimpleTreesContext} from "../context/simple-trees.context.ts";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";

class SimpleTreesUrlEntityAction extends UmbEntityActionBase<MetaEntityAction> {
	_context?: SimpleTreesContext;

	constructor(host: UmbControllerHost, args: UmbEntityActionArgs<MetaEntityAction>) {
		super(host, args);
		this._context = new SimpleTreesContext(host);
	}

	async getHref() {
		try {
			// @ts-ignore
			const alias = this.manifest.alias;
			if (!alias) {
				throw new Error('Alias is required to generate the URL.');
			}
			const unique = this.args.unique?.toString();
			if (!unique) {
				throw new Error('Unique identifier is required to generate the URL.');
			}
			const result = await this._context?.runEntityUrlAction(unique, this.args.entityType, alias);
			// @ts-ignore
			const href = result?.data?.url || '';
			return href;
		} catch (error) {
			console.error('Error getting href:', error);
		}
	}
}

export {SimpleTreesUrlEntityAction as api};