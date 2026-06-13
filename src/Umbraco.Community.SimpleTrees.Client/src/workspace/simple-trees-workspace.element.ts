import {LitElement, html, css, nothing} from 'lit';
import {customElement, property, state} from 'lit/decorators.js';
import {UmbElementMixin} from "@umbraco-cms/backoffice/element-api";
import {unsafeHTML} from '@umbraco-cms/backoffice/external/lit';
import {UUITextStyles} from '@umbraco-cms/backoffice/external/uui';
import {SIMPLE_TREES_CONTEXT_TOKEN} from "../context/simple-trees.context.ts";
import {HtmlScriptContentRuntime} from "../utils/html-script-content-runtime.ts";

@customElement('simple-tree-item-workspace')
export class SimpleTreesWorkspaceElement extends UmbElementMixin(LitElement) {

	@property({type: String})
	uniqueId: string = '';

	@state()
	loading: boolean = true;

	@state()
	content: string | undefined;

	@property({type: String})
	entityType: string = '';

	private readonly runtime = new HtmlScriptContentRuntime();

	constructor() {
		super();
		this.consumeContext(SIMPLE_TREES_CONTEXT_TOKEN, async (context) => {
			if (!context) {
				return;
			}

			const response = await context.render(this.uniqueId, this.entityType);
			this.loading = false;

			if (response.error) {
				console.error(response.error);
			} else {

				this.content = response.data?.body;
			}
		});
	}

	protected updated(): void {
		void this.runtime.executeScripts(this.content, this.renderRoot);
	}
	
	render() {
		if (this.loading) {
			return nothing;
		}

		const body = this.runtime.extractHtml(this.content);
		return html`
			<div class="uui-text">
				${body ? unsafeHTML(body) : nothing}
			</div>
		`;
	}

	static styles = [
		UUITextStyles,
		css`
			:host {
				display: flex;
				flex-direction: column;
				gap: var(--uui-size-4);
				padding: var(--uui-size-layout-1);
				height: calc(100% - 50px);
				overflow: auto;
			}

			pre {
				font-family: monospace;
				background-color: var(--uui-color-background);
				padding: var(--uui-size-layout-1)
			}
		`]
}

declare global {
	interface HTMLElementTagNameMap {
		'simple-tree-item-workspace': SimpleTreesWorkspaceElement;
	}
}