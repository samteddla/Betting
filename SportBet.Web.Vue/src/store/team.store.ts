
import { defineStore } from "pinia";
import { AxiosClient } from "@/services";
import { ref } from "vue";
import { Client, type TeamResponse } from '@/api/api2'

export const TeamStore = defineStore("team", () => {
    const axiosClient = AxiosClient.getApi();
    const team = ref(<TeamResponse>{});
    const teams = ref<TeamResponse[]>([])

    const api = new Client(undefined, axiosClient);
   
    const getTeams = async () => {
        teams.value = await api.getTeams().then((response) => {
            return response;
        });
    }
    
    const addTeam = async (team: any) => {
        team.value = await api.addTeam(team).then((response) => {
            return response;
        });
    }
    return {teams,team, getTeams,  addTeam };
});

