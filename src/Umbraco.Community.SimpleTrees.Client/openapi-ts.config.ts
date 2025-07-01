import { defineConfig, defaultPlugins } from '@hey-api/openapi-ts';

export default defineConfig({
	input: 'http://localhost:54813/umbraco/swagger/SimpleTrees/swagger.json',
	plugins: [
		...defaultPlugins,
		{
			name: '@hey-api/client-fetch',
			exportFromIndex: true,
			throwOnError: true,
		},
		{
			name: '@hey-api/typescript',
			enums: 'typescript',
			readOnlyWriteOnlyBehavior: 'off',
		},
		{
			name: '@hey-api/sdk',
			asClass: true,
		}
	],
	output: {
		format: 'prettier',
		path: './src/api',
	}
});
