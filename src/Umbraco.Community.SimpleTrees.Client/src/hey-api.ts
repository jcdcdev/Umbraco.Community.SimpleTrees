import type { CreateClientConfig } from './api';
import { umbHttpClient } from '@umbraco-cms/backoffice/http-client';

export const createClientConfig: CreateClientConfig = (config) => ({
    ...config,
    ...umbHttpClient.getConfig(),
});
