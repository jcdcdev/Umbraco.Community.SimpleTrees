import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";
import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import SimpleTreesRepository from "../repository/simple-trees.repository.ts";

export const SIMPLE_TREES_CONTEXT_TOKEN = new UmbContextToken<SimpleTreesContext>("SimpleTreesContext");

export class SimpleTreesContext extends UmbControllerBase {
	#repository: SimpleTreesRepository;

	constructor(host: UmbControllerHost) {
		super(host);
		this.#repository = new SimpleTreesRepository(this);
		this.provideContext(SIMPLE_TREES_CONTEXT_TOKEN, this);
	}

	async runEntityExecuteAction(unique: string, entityType: string, actionAlias: string) {
		return await this.#repository.runEntityExecuteAction(entityType, unique, actionAlias);
	}

	async runEntityUrlAction(unique: string, entityType: string, actionAlias: string) {
		return await this.#repository.runEntityUrlAction(entityType, unique, actionAlias);
	}

	async render(unique: string, entityType: string) {
		return await this.#repository.render(unique, entityType);
	}
}
