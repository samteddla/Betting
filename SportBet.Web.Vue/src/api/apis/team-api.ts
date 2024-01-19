
import globalAxios, { AxiosResponse, AxiosInstance, AxiosRequestConfig } from 'axios';
import { Configuration } from '../configuration';
// Some imports not used depending on template conditions
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from '../base';
import { AddTeamCommand } from '../models';
import { TeamResponse } from '../models';
/**
 * TeamApi - axios parameter creator
 * @export
 */
export const TeamApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * addTeam
         */
        addTeam: async (body?: AddTeamCommand, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Team`;
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
         * getTeams
         */
        getTeams: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Team`;
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
    }
};

/**
 * TeamApi - functional programming interface
 * @export
 */
export const TeamApiFp = function(configuration?: Configuration) {
    return {
        
        async addTeam(body?: AddTeamCommand, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<TeamResponse>>> {
            const localVarAxiosArgs = await TeamApiAxiosParamCreator(configuration).addTeam(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async getTeams(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<Array<TeamResponse>>>> {
            const localVarAxiosArgs = await TeamApiAxiosParamCreator(configuration).getTeams(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
    }
};

/**
 * TeamApi - factory interface
 * @export
 */
export const TeamApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    return {
        
        async addTeam(body?: AddTeamCommand, options?: AxiosRequestConfig): Promise<AxiosResponse<TeamResponse>> {
            return TeamApiFp(configuration).addTeam(body, options).then((request) => request(axios, basePath));
        },
        
        async getTeams(options?: AxiosRequestConfig): Promise<AxiosResponse<Array<TeamResponse>>> {
            return TeamApiFp(configuration).getTeams(options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * @class TeamApi
 */
export class TeamApi extends BaseAPI {
    
    public async addTeam(body?: AddTeamCommand, options?: AxiosRequestConfig) : Promise<AxiosResponse<TeamResponse>> {
        return TeamApiFp(this.configuration).addTeam(body, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async getTeams(options?: AxiosRequestConfig) : Promise<AxiosResponse<Array<TeamResponse>>> {
        return TeamApiFp(this.configuration).getTeams(options).then((request) => request(this.axios, this.basePath));
    }
}
