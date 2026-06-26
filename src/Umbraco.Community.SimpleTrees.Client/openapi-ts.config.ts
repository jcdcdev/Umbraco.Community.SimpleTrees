import {defineConfig} from '@hey-api/openapi-ts';

export default defineConfig({
	input: 'http://localhost:54813/umbraco/openapi/SimpleTrees.json',
	plugins: [
		{
			name: '@hey-api/sdk',
			operations: {
				containerName: {
					name: 'SimpleTrees',
					casing: 'preserve',
				},
				strategy: 'byTags'
			}
		},
		{
			name: '@hey-api/client-fetch',
			runtimeConfigPath: './src/hey-api.ts',
			exportFromIndex: true,
			throwOnError: true,
		},
		'@hey-api/schemas',
		{
			enums: 'javascript',
			name: '@hey-api/typescript',
		},
	],
	output: {
		path: './src/api',
	}
});
