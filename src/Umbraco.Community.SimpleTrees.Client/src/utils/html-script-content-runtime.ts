type ScriptInfo = {
    src?: string;
    text: string;
    type?: string;
};

export class HtmlScriptContentRuntime {
    private lastContent?: string;

    private parse(content: string) {
        const el = document.createElement('div');
        el.innerHTML = content;
        const scriptEls = Array.from(el.querySelectorAll('script'));
        const scripts = scriptEls.map((s) => ({
            src: s.getAttribute('src') ?? undefined,
            text: s.textContent ?? '',
            type: s.getAttribute('type') ?? undefined,
        }));
        scriptEls.forEach((s) => s.remove());
        return { html: el.innerHTML, scripts };
    }

    extractHtml(content?: string): string {
        return content ? this.parse(content).html : '';
    }

    async executeScripts(content: string | undefined, root: ParentNode = document): Promise<void> {
        if (!content || content === this.lastContent) {
            return;
        }

        this.lastContent = content;

        for (const script of this.parse(content).scripts) {
            try {
                await this.run(script, root);
            } catch (error) {
                console.error(error);
            }
        }
    }

    private run(script: ScriptInfo, root: ParentNode): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            if (!script.src) {
                try {
                    new Function('document', 'window', script.text)(this.scopedDocument(root), window);
                    resolve();
                } catch (error) {
                    reject(error);
                }
                return;
            }

            const el = Object.assign(document.createElement('script'), {
                src: script.src,
                type: script.type ?? 'text/javascript',
                onload: resolve,
                onerror: () => reject(new Error(`Failed to load script: ${script.src}`)),
            });

            document.head.appendChild(el);
        });
    }

    private scopedDocument(root: ParentNode): Document {
        return new Proxy(document, {
            get: (target, prop, receiver) => {
                if (prop === 'getElementById') {
                    return (id: string) => (root as any).getElementById?.(id) ?? target.getElementById(id);
                }

                if (prop === 'querySelector') {
                    return (sel: string) => root.querySelector(sel) ?? target.querySelector(sel);
                }

                if (prop === 'querySelectorAll') {
                    return (sel: string) => {
                        const local = root.querySelectorAll(sel);
                        return local.length ? local : target.querySelectorAll(sel);
                    };
                }

                return Reflect.get(target, prop, receiver);
            },
        }) as Document;
    }
}
