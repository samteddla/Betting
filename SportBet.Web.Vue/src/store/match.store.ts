import { defineStore } from "pinia";
import { AxiosClient } from "@/services";
import { useAlertStore } from "@/store";
import { BetApi } from '@/api/index'
import { ref } from "vue";

import {type MyBetExtende, type GetActiveMatchs, type GetActiveMatch, type BetOnGame, type GetActivBetsResponse, type BetResultResponse,
type MyBet } from "@/api/index";
import { get } from "node:http";

export const MatchStore = defineStore("match", () => {

    const axiosClient = AxiosClient.getApi();
    const alertStore = useAlertStore();
    const matchs = ref<GetActiveMatchs[]>([])
    const match = ref<GetActiveMatch>()
    const cards = ref<GetActivBetsResponse[]>()
    const card = ref<BetResultResponse>()
    const cardExtended = ref<MyBetExtende>()
    const playedCard = ref<MyBet[]>()
    const playOutcome = ref<{ matchId: number; outcomeId: number }[]>([]);  

    const api = new BetApi(undefined, undefined, axiosClient);

    const getMatches = async () => {
        matchs.value = await api.betGetMatchSelectionsGet().then((response) => {
            return response.data;
        });

        if(matchs.value.length == 0){
            alertStore.error("No matches found");
        }
        else
        {
            console.log(matchs.value);
            alertStore.success("Matches found");
        }
    }

    const getMatch = async (id: number) => {
        match.value = await api.betGetMatchSelectionsIdGet(id).then((response) => {
            return response.data;
        });
    }
   
    const betOn = async (betOnRequest : BetOnGame) => {  
        
        betOnRequest.matches = createPlayOutCome(betOnRequest.matches);

        return await api.betBetOnPost(betOnRequest).then((response) => {
            const res = response;
            if(res.status === 200){
                alertStore.info("ok");
                console.log(res.data);
            }
            else
            {
                alertStore.error("data provided is not valid");
            }
            return response.data;
        });
    }

    const getMyCards = async () => {
        cards.value = await api.betGetActiveBetsGet().then((response) => {
            return response.data;
        });
    }

    const getMyCardDetail = async (id: number) => {
        card.value = await api.betGetBetResultIdGet(id).then((response) => {
            console.log(response.data);
            return response.data;
        });
    }

    const getMyCardExtende = async (id: number) => {
        cardExtended.value = await api.betGetCardExtendedIdGet(id).then((response) => {
        
            playOutcome.value = response.data.matches.map((c) => {
                return { matchId: c.matchId, outcomeId: c.outcomeId};
            });
            console.log("playedCard.value");
            console.log(playOutcome.value);
            console.log("playedCard.value");
            reversePlayOutcome(playOutcome.value);

            console.log("cardExtended.value");
            console.log(response.data);
            console.log("cardExtended.value");
            return response.data;
        });
    }

    // remove this ????
    const getPlayedCard = async (id: number) => {
        await getMyCardExtende(id);
        playedCard.value = await api.betGetCardIdGet(id).then((response) => {
            console.log("playedCard.value");
            console.log(response.data);
            console.log("playedCard.value");
            return response.data;
        });

        if(playedCard.value !== null){
            playOutcome.value = playedCard.value.map((c) => {
                return { matchId: c.matchId, outcomeId: c.outcomeId };
            });

            card.value?.matches.forEach((e: { matchId: number; matchResult: any; }) => {
                const index = playOutcome.value.findIndex((p) => p.matchId === e.matchId);
                if (index !== -1) {
                    playOutcome.value[index].Win = e.matchResult;
                }                
            });
            console.log("playedCard.value");
            console.log(playOutcome.value);
        }
        reversePlayOutcome(playOutcome.value);
    }

    const reversePlayOutcome = (result : any) =>
    {
        const originalOutcomes = [1, 2, 4, 5, 3, 6, 7]
        const combinedOutcomes = [[1], [2], [4], [1, 4], [1, 2], [2, 4], [1, 2, 4]];
        
        const reversedArray = [];
        for (const item of result) {
            const matchId = item.matchId;
            const originalOutcomeId = item.outcomeId;

            const index = originalOutcomes.indexOf(originalOutcomeId);
            if (index !== -1) {
                const lists = combinedOutcomes[index];
            
                for (let i = 0; i < lists.length; i++) {
                    const outcomeId = lists[i];
                    reversedArray.push({ matchId, outcomeId });
                }
            }
        }
        playOutcome.value = reversedArray;
        console.log("reversedArray");
        console.log(playOutcome.value);
        console.log("reversedArray");
    }

    const createPlayOutCome = (input :any) =>
    {
        const groupedMatches = {};
        const result = [];
        input.forEach((item : any) => {
            const matchId = item.matchId;
            const outcomeId = item.outcomeId;
    
            if (!groupedMatches[matchId]) {
                groupedMatches[matchId] = { matchId, outcomeId: outcomeId };
            } else {
                groupedMatches[matchId].outcomeId += outcomeId;
            }
        });
    
        for (const matchId in groupedMatches) {
            result.push(groupedMatches[matchId]);
        }
        return result;
    }

    return { cardExtended, match, matchs,cards,card,playedCard,playOutcome, getMatches, getMatch, betOn, getMyCards, getMyCardDetail, getPlayedCard, getMyCardExtende };
});
