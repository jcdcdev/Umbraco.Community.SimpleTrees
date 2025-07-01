import {defineConfig} from "vite";

export default defineConfig({
	build: {
		lib: {
			entry: ["src/index.ts","src/workspace/simple-trees-workspace.context.ts"],
			formats: ["es"],
		},
		outDir: "../Umbraco.Community.SimpleTrees/wwwroot/App_Plugins/SimpleTrees/dist/",
		sourcemap: true,
		rollupOptions: {
			external: [/^@umbraco/],
		},
	},
});