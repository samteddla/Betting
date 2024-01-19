import { defineStore } from "pinia";
import { ref } from "vue";

interface Alert {
  message: string;
  type: string;
}

export const useAlertStore = defineStore("alert", () => {
  const alert = ref<Alert | null>(null);
  function success(message: string) {
    alert.value = { message, type: "success" };
  }
  function error(message: string) {
    alert.value = { message, type: "error" };
  }
  function info(message: string) {
    alert.value = { message, type: "info" };
  }
  function warning(message: string) {
    alert.value = { message, type: "warning" };
  }
  function clear() {
    alert.value = null;
  }
  function getAlert() {
    return alert.value;
  }
  return { alert,getAlert, success, error, clear, info, warning };
});