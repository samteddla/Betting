
import globalAxios, { AxiosResponse, AxiosInstance, AxiosRequestConfig } from 'axios';
import { Configuration } from '../configuration';
// Some imports not used depending on template conditions
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from '../base';
import { AddUserRequest } from '../models';
import { AddUserResponse } from '../models';
import { AuthenticationResult } from '../models';
import { ChangePasswordRequest } from '../models';
import { ChangePasswordResponse } from '../models';
import { LoginRequest } from '../models';
import { UpdateUserRequest } from '../models';
import { UpdateUserResponse } from '../models';
/**
 * AuthApi - axios parameter creator
 * @export
 */
export const AuthApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * authAddUserPost
         */
        authAddUserPost: async (body?: AddUserRequest, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Auth/add-user`;
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
         * authChangePasswordPut
         */
        authChangePasswordPut: async (body?: ChangePasswordRequest, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Auth/change-password`;
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
        /**
         * authUpdateUserPut
         */
        authUpdateUserPut: async (body?: UpdateUserRequest, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Auth/update-user`;
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
        /**
         * login
         */
        login: async (body?: LoginRequest, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/Auth`;
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
    }
};

/**
 * AuthApi - functional programming interface
 * @export
 */
export const AuthApiFp = function(configuration?: Configuration) {
    return {
        
        async authAddUserPost(body?: AddUserRequest, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<AddUserResponse>>> {
            const localVarAxiosArgs = await AuthApiAxiosParamCreator(configuration).authAddUserPost(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async authChangePasswordPut(body?: ChangePasswordRequest, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<ChangePasswordResponse>>> {
            const localVarAxiosArgs = await AuthApiAxiosParamCreator(configuration).authChangePasswordPut(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async authUpdateUserPut(body?: UpdateUserRequest, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<UpdateUserResponse>>> {
            const localVarAxiosArgs = await AuthApiAxiosParamCreator(configuration).authUpdateUserPut(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        
        async login(body?: LoginRequest, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<AuthenticationResult>>> {
            const localVarAxiosArgs = await AuthApiAxiosParamCreator(configuration).login(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
    }
};

/**
 * AuthApi - factory interface
 * @export
 */
export const AuthApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    return {
        
        async authAddUserPost(body?: AddUserRequest, options?: AxiosRequestConfig): Promise<AxiosResponse<AddUserResponse>> {
            return AuthApiFp(configuration).authAddUserPost(body, options).then((request) => request(axios, basePath));
        },
        
        async authChangePasswordPut(body?: ChangePasswordRequest, options?: AxiosRequestConfig): Promise<AxiosResponse<ChangePasswordResponse>> {
            return AuthApiFp(configuration).authChangePasswordPut(body, options).then((request) => request(axios, basePath));
        },
        
        async authUpdateUserPut(body?: UpdateUserRequest, options?: AxiosRequestConfig): Promise<AxiosResponse<UpdateUserResponse>> {
            return AuthApiFp(configuration).authUpdateUserPut(body, options).then((request) => request(axios, basePath));
        },
        
        async login(body?: LoginRequest, options?: AxiosRequestConfig): Promise<AxiosResponse<AuthenticationResult>> {
            return AuthApiFp(configuration).login(body, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * @class AuthApi
 */
export class AuthApi extends BaseAPI {
    
    public async authAddUserPost(body?: AddUserRequest, options?: AxiosRequestConfig) : Promise<AxiosResponse<AddUserResponse>> {
        return AuthApiFp(this.configuration).authAddUserPost(body, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async authChangePasswordPut(body?: ChangePasswordRequest, options?: AxiosRequestConfig) : Promise<AxiosResponse<ChangePasswordResponse>> {
        return AuthApiFp(this.configuration).authChangePasswordPut(body, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async authUpdateUserPut(body?: UpdateUserRequest, options?: AxiosRequestConfig) : Promise<AxiosResponse<UpdateUserResponse>> {
        return AuthApiFp(this.configuration).authUpdateUserPut(body, options).then((request) => request(this.axios, this.basePath));
    }
    
    public async login(body?: LoginRequest, options?: AxiosRequestConfig) : Promise<AxiosResponse<AuthenticationResult>> {
        return AuthApiFp(this.configuration).login(body, options).then((request) => request(this.axios, this.basePath));
    }
}
