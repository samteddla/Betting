
import { defineStore } from "pinia";
import { AxiosClient } from "@/services";
import { useAlertStore } from "@/store";
import { TeamApi } from '@/api/index'
import { ref } from "vue";
import { type TeamResponse } from '@/api/index'

export const TeamStore = defineStore("team", () => {
    const axiosClient = AxiosClient.getApi();
    const alertStore = useAlertStore();
    const team = ref(<TeamResponse>[]);
    const teams = ref<TeamResponse[]>([])

    const api = new TeamApi(undefined, undefined, axiosClient);
   
    const getTeams = async () => {
        teams.value = await api.getTeams().then((response) => {
            return response.data;
        });
    }
    
    const addTeam = async (team: any) => {
        team.value = await api.addTeam(team).then((response) => {
            if (response.status === 201) {
                alertStore.success("Team added successfully");
            }
            return response.data;
        });
    }
    return {teams,team, getTeams,  addTeam };
});

