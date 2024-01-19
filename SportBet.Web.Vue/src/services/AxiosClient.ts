
import { AuthStore } from "@/store";
import axios from "axios";
import type { AxiosInstance, AxiosResponse, InternalAxiosRequestConfig } from "axios";

class AxiosClient {
    private api: AxiosInstance;

    constructor() {
        console.log("AxiosClient constructor");
        console.log(import.meta.env.VITE_API_URL);
        this.api = axios.create({
            baseURL:  import.meta.env.VITE_API_URL,
        });

        this.api.interceptors.request.use(
            (config : InternalAxiosRequestConfig) => {
                const token = this.getAccessToken();
                config.headers.Authorization = `Bearer ${token}`;
               //  console.log("Request:", config);
                return config;
            },
            (error) => {
                console.log("Request error:", error);
                
                return Promise.reject(error);
            }
        );

        this.api.interceptors.response.use(
            (response: AxiosResponse) => {
                // console.log("Response:", response);
                return response;
            },
            (error) => {                
                console.log("Response error:", error);
                if (error.response.status === 401 && error.config) {
                    console.log("401 error:", error);
                    // refresh the JWT token and retry the request
                   /*return this.refreshToken().then(() => {
                        console.log("refresh requested!");
                        error.config.__isRetryRequest = true;
                        return this.api(error.config);
                    });*/
                }
                return Promise.reject(error);
            }
        );
    }

    private getAccessToken(): string | null {
        const store = AuthStore();
        // console.log(store.user?.token);
        return store.user?.token?.toString() || null;
    }

    public getApi(): AxiosInstance {
        return this.api;
    }
}

export default new AxiosClient();