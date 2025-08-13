import {MetaEntityAction, UmbEntityActionArgs, UmbEntityActionBase} from '@umbraco-cms/backoffice/entity-action';
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {SIMPLE_TREES_CONTEXT_TOKEN, SimpleTreesContext} from "../context/simple-trees.context.ts";
import {UMB_NOTIFICATION_CONTEXT, UmbNotificationContext, UmbNotificationDefaultData} from '@umbraco-cms/backoffice/notification';

class SimpleTreesExecuteEntityAction extends UmbEntityActionBase<MetaEntityAction> {
	_context?: SimpleTreesContext;
	_notificationContext?: UmbNotificationContext;

	constructor(host: UmbControllerHost, args: UmbEntityActionArgs<MetaEntityAction>) {
		super(host, args);
		this.consumeContext(SIMPLE_TREES_CONTEXT_TOKEN, (context) => {
			this._context = context;
		})

		this.consumeContext(UMB_NOTIFICATION_CONTEXT, (context) => {
			this._notificationContext = context;
		})
	}

	async execute() {
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
			const result = await this._context?.runEntityExecuteAction(unique, this.args.entityType, alias);
			const responseData = result?.data;
			if (responseData) {
				const data: UmbNotificationDefaultData = {
					// @ts-ignore
					headline: responseData.title,
					// @ts-ignore
					message: responseData.message
				};
				// @ts-ignore
				const style = responseData.isSuccess ? 'positive' : 'danger';
				this._notificationContext?.peek(style, {data});
			} else {
				throw new Error('No data returned from the action execution.');
			}

		} catch (error) {
			console.error('Error executing entity action:', error);
			this._notificationContext?.peek("danger", {
				data: {
					headline: 'Error executing entity action',
					message: error instanceof Error ? error.message : 'An unknown error occurred.'
				}
			});
		}
	}
}

export {SimpleTreesExecuteEntityAction as api};
