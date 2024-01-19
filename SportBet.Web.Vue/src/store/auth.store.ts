
import { defineStore } from "pinia";
import { AxiosClient } from "@/services";
import router from "@/router";
import { useAlertStore } from "@/store";
import { Client} from '@/api/api2'
import { type LoginRequest, type ChangePasswordRequest, type ChangePasswordResponse } from '@/api/index'
import { ref } from "vue";
import { type AuthenticationResult } from '@/api/api2'
import { jwtDecode } from "jwt-decode";

export const AuthStore = defineStore("auth", () => {

  const user = ref<AuthenticationResult>(JSON.parse(localStorage.getItem("user")?.toString() || "null"));
  // console.log('The user is : ',user.value);
  const returnUrl = ref<null | string>(null);
  const axiosClient = AxiosClient.getApi();
  // const api = new AuthApi(undefined, undefined, axiosClient);
  const api = new Client(undefined, axiosClient);

  const alertStore = useAlertStore();
  const passwordResponse = ref<ChangePasswordResponse>();

  const login = async (username?: string, password?: string) => {
    const loginRequest: LoginRequest = {
      username: username,
      password: password
    };

    const response  = await api.login(loginRequest).then((res) => {
      return res;
  });

    //const response = await api.login(loginRequest);
    if (response !== null) {
      user.value = response;
      alertStore.info("ok");
      // console.log(response.data);
      localStorage.setItem("user", JSON.stringify(user.value));
      console.log('TreturnUrl.value is : ', returnUrl.value);
      router.push(returnUrl.value || "/");
    } else {
      const alertStore = useAlertStore();
      alertStore.error("Invalid username or password");
    }
  }

  const logout = async () => {
    user.value = null!;
    console.log('The user is : ', user.value);
    localStorage.removeItem("user");
    router.push('/login');
  }

  const changePassword = async (passwordReq: ChangePasswordRequest) => {
    passwordResponse.value = await api.changePassword(passwordReq).then((response) => {
      return response;
    });
  }

  const checkToken = () => {
    if (user.value.token) {
        const decodedToken = jwtDecode(user.value.token)
        if (!decodedToken) {
            return false
        }
        //console.log(decodedToken);
        //console.log(decodedToken.exp! * 1000);
        //console.log(new Date().getTime());
        return decodedToken && (decodedToken.exp! * 1000) > new Date().getTime()
    }
    return false;
}

  return {passwordResponse, user, returnUrl, login, logout, changePassword, checkToken };
});

