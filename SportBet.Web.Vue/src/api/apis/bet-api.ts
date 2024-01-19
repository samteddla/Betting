
import globalAxios, { AxiosResponse, AxiosInstance, AxiosRequestConfig } from 'axios';
import { Configuration } from '../configuration';
// Some imports not used depending on template conditions
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from '../base';
import { BetOnGame } from '../models';
import { BetOnGameResponse } from '../models';
import { BetResultResponse } from '../models';
import { CreateMatchSelectionsRequest } from '../models';
import { CreateMatchSelectionsResponse } from '../models';
import { GetActivBetsResponse } from '../models';
import { GetActiveMatch } from '../models';
import { GetActiveMatchs } from '../models';
import { GetMatchTypes } from '../models';
import { GetOutcomes } from '../models';
import { MyBet } from '../models';
import { MyBetExtende } from '../models';
import { MyBets } from '../models';
import { UpdateBetResult } from '../models';
import { UpdateBetResultRequest } from '../models';
/**
 * BetApi - axios parameter creator
 * @export
 */
export const BetApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * betBetOnPost
         */
        betBetOnPost: async (body?: BetOnGame, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/bet-on`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            localVarHeaderParameter['Content-Type'] = 'application/json';

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            const needsSerialization = (typeof body !== "string") || localVarRequestOptions.headers['Content-Type'] === 'application/json';
            localVarRequestOptions.data =  needsSerialization ? JSON.stringify(body !== undefined ? body : {}) : (body || "");

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betCreateMatchSelectionPost
         */
        betCreateMatchSelectionPost: async (body?: CreateMatchSelectionsRequest, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/create-match-selection`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            localVarHeaderParameter['Content-Type'] = 'application/json';

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            const needsSerialization = (typeof body !== "string") || localVarRequestOptions.headers['Content-Type'] === 'application/json';
            localVarRequestOptions.data =  needsSerialization ? JSON.stringify(body !== undefined ? body : {}) : (body || "");

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetActiveBetsGet
         */
        betGetActiveBetsGet: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/get-active-bets`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetBetResultIdGet
         */
        betGetBetResultIdGet: async (id: number, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            if (id === null || id === undefined) {
                throw new RequiredError('id','Required parameter id was null or undefined when calling betGetBetResultIdGet.');
            }
            const localVarPath = `/Bet/get-bet-result/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetCardExtendedIdGet
         */
        betGetCardExtendedIdGet: async (id: number, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            if (id === null || id === undefined) {
                throw new RequiredError('id','Required parameter id was null or undefined when calling betGetCardExtendedIdGet.');
            }
            const localVarPath = `/Bet/get-card-extended/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetCardIdGet
         */
        betGetCardIdGet: async (id: number, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            if (id === null || id === undefined) {
                throw new RequiredError('id','Required parameter id was null or undefined when calling betGetCardIdGet.');
            }
            const localVarPath = `/Bet/get-card/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetCardsGet
         */
        betGetCardsGet: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/get-cards`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetMatchSelectionsGet
         */
        betGetMatchSelectionsGet: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/get-match-selections`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetMatchSelectionsIdGet
         */
        betGetMatchSelectionsIdGet: async (id: number, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            if (id === null || id === undefined) {
                throw new RequiredError('id','Required parameter id was null or undefined when calling betGetMatchSelectionsIdGet.');
            }
            const localVarPath = `/Bet/get-match-selections/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetMatchTypesGet
         */
        betGetMatchTypesGet: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/get-match-types`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetMatchTypesIdGet
         */
        betGetMatchTypesIdGet: async (id: number, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            if (id === null || id === undefined) {
                throw new RequiredError('id','Required parameter id was null or undefined when calling betGetMatchTypesIdGet.');
            }
            const localVarPath = `/Bet/get-match-types/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betGetOutcomesGet
         */
        betGetOutcomesGet: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Bet/get-outcomes`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * betUpdateBetResultMatchtypeidPut
         */
        betUpdateBetResultMatchtypeidPut: async (matchtypeId: number, body?: UpdateBetResultRequest, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'matchtypeId' is not null or undefined
            if (matchtypeId === null || matchtypeId === undefined) {
                throw new RequiredError('matchtypeId','Required parameter matchtypeId was null or undefined when calling betUpdateBetResultMatchtypeidPut.');
            }
            const localVarPath = `/Bet/update-bet-result/{matchtypeid}`
                .replace(`{${"matchtypeId"}}`, encodeURIComponent(String(matchtypeId)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'PUT', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            localVarHeaderParameter['Content-Type'] = 'application/json';

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            const needsSerialization = (typeof body !== "string") || localVarRequestOptions.headers['Content-Type'] === 'application/json';
            localVarRequestOptions.data =  needsSerialization ? JSON.stringify(body !== undefined ? body : {}) : (body || "");

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * BetApi - functional programming interface
 * @export
 */
export const BetApiFp = function(configuration?: Configuration) {
    return {
        
        async betBetOnPost(body?: BetOnGame, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<BetOnGameResponse>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betBetOnPost(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betCreateMatchSelectionPost(body?: CreateMatchSelectionsRequest, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<CreateMatchSelectionsResponse>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betCreateMatchSelectionPost(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetActiveBetsGet(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<GetActivBetsResponse>>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetActiveBetsGet(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetBetResultIdGet(id: number, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<BetResultResponse>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetBetResultIdGet(id, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetCardExtendedIdGet(id: number, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<MyBetExtende>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetCardExtendedIdGet(id, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetCardIdGet(id: number, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<MyBet>>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetCardIdGet(id, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetCardsGet(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<MyBets>>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetCardsGet(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetMatchSelectionsGet(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<GetActiveMatchs>>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetMatchSelectionsGet(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetMatchSelectionsIdGet(id: number, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<GetActiveMatch>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetMatchSelectionsIdGet(id, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetMatchTypesGet(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<GetMatchTypes>>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetMatchTypesGet(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetMatchTypesIdGet(id: number, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<GetMatchTypes>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetMatchTypesIdGet(id, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betGetOutcomesGet(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<GetOutcomes>>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betGetOutcomesGet(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async betUpdateBetResultMatchtypeidPut(matchtypeId: number, body?: UpdateBetResultRequest, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<UpdateBetResult>>> {
            const localVarAxiosArgs = await BetApiAxiosParamCreator(configuration).betUpdateBetResultMatchtypeidPut(matchtypeId, body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
    }
};

/**
 * BetApi - factory interface
 * @export
 */
export const BetApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    return {
        
        async betBetOnPost(body?: BetOnGame, options?: AxiosRequestConfig): Promise<AxiosResponse<BetOnGameResponse>> {
            return BetApiFp(configuration).betBetOnPost(body, options).then((request) => request(axios, basePath));
        },
        
        async betCreateMatchSelectionPost(body?: CreateMatchSelectionsRequest, options?: AxiosRequestConfig): Promise<AxiosResponse<CreateMatchSelectionsResponse>> {
            return BetApiFp(configuration).betCreateMatchSelectionPost(body, options).then((request) => request(axios, basePath));
        },
        
        async betGetActiveBetsGet(options?: AxiosRequestConfig): Promise<AxiosResponse<Array<GetActivBetsResponse>>> {
            return BetApiFp(configuration).betGetActiveBetsGet(options).then((request) => request(axios, basePath));
        },
        
        async betGetBetResultIdGet(id: number, options?: AxiosRequestConfig): Promise<AxiosResponse<BetResultResponse>> {
            return BetApiFp(configuration).betGetBetResultIdGet(id, options).then((request) => request(axios, basePath));
        },
        
        async betGetCardExtendedIdGet(id: number, options?: AxiosRequestConfig): Promise<AxiosResponse<MyBetExtende>> {
            return BetApiFp(configuration).betGetCardExtendedIdGet(id, options).then((request) => request(axios, basePath));
        },
        
        async betGetCardIdGet(id: number, options?: AxiosRequestConfig): Promise<AxiosResponse<Array<MyBet>>> {
            return BetApiFp(configuration).betGetCardIdGet(id, options).then((request) => request(axios, basePath));
        },
        
        async betGetCardsGet(options?: AxiosRequestConfig): Promise<AxiosResponse<Array<MyBets>>> {
            return BetApiFp(configuration).betGetCardsGet(options).then((request) => request(axios, basePath));
        },
        
        async betGetMatchSelectionsGet(options?: AxiosRequestConfig): Promise<AxiosResponse<Array<GetActiveMatchs>>> {
            return BetApiFp(configuration).betGetMatchSelectionsGet(options).then((request) => request(axios, basePath));
        },
        
        async betGetMatchSelectionsIdGet(id: number, options?: AxiosRequestConfig): Promise<AxiosResponse<GetActiveMatch>> {
            return BetApiFp(configuration).betGetMatchSelectionsIdGet(id, options).then((request) => request(axios, basePath));
        },
        
        async betGetMatchTypesGet(options?: AxiosRequestConfig): Promise<AxiosResponse<Array<GetMatchTypes>>> {
            return BetApiFp(configuration).betGetMatchTypesGet(options).then((request) => request(axios, basePath));
        },
        
        async betGetMatchTypesIdGet(id: number, options?: AxiosRequestConfig): Promise<AxiosResponse<GetMatchTypes>> {
            return BetApiFp(configuration).betGetMatchTypesIdGet(id, options).then((request) => request(axios, basePath));
        },
        
        async betGetOutcomesGet(options?: AxiosRequestConfig): Promise<AxiosResponse<Array<GetOutcomes>>> {
            return BetApiFp(configuration).betGetOutcomesGet(options).then((request) => request(axios, basePath));
        },
        
        async betUpdateBetResultMatchtypeidPut(matchtypeId: number, body?: UpdateBetResultRequest, options?: AxiosRequestConfig): Promise<AxiosResponse<UpdateBetResult>> {
            return BetApiFp(configuration).betUpdateBetResultMatchtypeidPut(matchtypeId, body, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * @class BetApi
 */
export class BetApi extends BaseAPI {
    
    public async betBetOnPost(body?: BetOnGame, options?: AxiosRequestConfig) : Promise<AxiosResponse<BetOnGameResponse>> {
        return BetApiFp(this.configuration).betBetOnPost(body, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betCreateMatchSelectionPost(body?: CreateMatchSelectionsRequest, options?: AxiosRequestConfig) : Promise<AxiosResponse<CreateMatchSelectionsResponse>> {
        return BetApiFp(this.configuration).betCreateMatchSelectionPost(body, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetActiveBetsGet(options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<GetActivBetsResponse>>> {
        return BetApiFp(this.configuration).betGetActiveBetsGet(options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetBetResultIdGet(id: number, options?: AxiosRequestConfig) : Promise<AxiosResponse<BetResultResponse>> {
        return BetApiFp(this.configuration).betGetBetResultIdGet(id, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetCardExtendedIdGet(id: number, options?: AxiosRequestConfig) : Promise<AxiosResponse<MyBetExtende>> {
        return BetApiFp(this.configuration).betGetCardExtendedIdGet(id, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetCardIdGet(id: number, options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<MyBet>>> {
        return BetApiFp(this.configuration).betGetCardIdGet(id, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetCardsGet(options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<MyBets>>> {
        return BetApiFp(this.configuration).betGetCardsGet(options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetMatchSelectionsGet(options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<GetActiveMatchs>>> {
        return BetApiFp(this.configuration).betGetMatchSelectionsGet(options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetMatchSelectionsIdGet(id: number, options?: AxiosRequestConfig) : Promise<AxiosResponse<GetActiveMatch>> {
        return BetApiFp(this.configuration).betGetMatchSelectionsIdGet(id, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetMatchTypesGet(options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<GetMatchTypes>>> {
        return BetApiFp(this.configuration).betGetMatchTypesGet(options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetMatchTypesIdGet(id: number, options?: AxiosRequestConfig) : Promise<AxiosResponse<GetMatchTypes>> {
        return BetApiFp(this.configuration).betGetMatchTypesIdGet(id, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betGetOutcomesGet(options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<GetOutcomes>>> {
        return BetApiFp(this.configuration).betGetOutcomesGet(options).then((request) => request(this.axios, this.basePath));
    }
    
    public async betUpdateBetResultMatchtypeidPut(matchtypeId: number, body?: UpdateBetResultRequest, options?: AxiosRequestConfig) : Promise<AxiosResponse<UpdateBetResult>> {
        return BetApiFp(this.configuration).betUpdateBetResultMatchtypeidPut(matchtypeId, body, options).then((request) => request(this.axios, this.basePath));
    }
}
