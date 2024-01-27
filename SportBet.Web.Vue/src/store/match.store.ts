import { defineStore } from "pinia";
import { ref } from "vue";
import { AxiosClient } from "@/services";
import { useAlertStore } from "@/store";
import { Client } from '@/api/api2'

import {type GetMatchResult, type MyBetExtende, type GetActiveMatchs, type GetActiveMatch, type BetOnGame, type GetActivBetsResponse, type BetResultResponse,
type MyBet, type UpdateBetResult } from "@/api/api2";

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
    const updatedBetResult = ref<UpdateBetResult[]>();
    const matchResult = ref<GetMatchResult>();

    const api = new Client(undefined, axiosClient);
    
    const getMatchResults = async (matchtypeId: number, selectionId : number) => {
        await getMatch(selectionId);
        matchResult.value = await api.getMatchResults(matchtypeId,selectionId).then((response) => {

            var resultOutcome = (response.matches === null || response.matches === undefined)? null 
            : response.matches.map((c : any) => {
                return { matchId: c.matchId, outcomeId: c.outcomeId};
            });

            reversePlayOutcome(resultOutcome);
            return response;
        });
    }
    const updateMatchResult = async (betResult: any) => {
        betResult.UpdateBetResultRequest = createPlayOutCome(betResult.UpdateBetResultRequest);
        updatedBetResult.value = await api.updateMatchResults(betResult.matchtypeId,betResult.matchSelectionId,betResult.UpdateBetResultRequest).then((response) => {
            return response;
        });
    }

    const getMatches = async () => {
        
        matchs.value = await api.getMatchSelectionsAll().then((response) => {
            return response;
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
        match.value = await api.getMatchSelections(id).then((response) => {
            return response;
        });
    }
   
    const betOn = async (betOnRequest : BetOnGame) => {  
        
        betOnRequest.matches = createPlayOutCome(betOnRequest.matches);

        return await api.betOn(betOnRequest).then((response) => {            
            return response;
        });
    }

    const getMyCards = async () => {
        cards.value = await api.getActiveBets().then((response) => {
            return response;
        });
    }

    const getMyCardDetail = async (id: number) => {
        card.value = await api.getBetResult(id).then((response) => {
            return response;
        });
    }

    const getMyCardExtende = async (id: number) => {
        cardExtended.value = await api.getCardExtended(id).then((response) => {
        
            playOutcome.value = (response.matches === null || response.matches === undefined)? null 
            : response.matches.map((c : any) => {
                return { matchId: c.matchId, outcomeId: c.outcomeId};
            });

            console.log(playOutcome.value);
            reversePlayOutcome(playOutcome.value);

            return response;
        });
    }

    // remove this ????
    const getPlayedCard = async (id: number) => {
        await getMyCardExtende(id);
        playedCard.value = await api.getCard(id).then((response) => {
            return response;
        });

        if(playedCard.value !== null){
            playOutcome.value = playedCard.value.map((c : any) => {
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

    return {matchResult, updatedBetResult, cardExtended, match, matchs,cards,card,playedCard,playOutcome,getMatchResults,updateMatchResult, getMatches, getMatch, betOn, getMyCards, getMyCardDetail, getPlayedCard, getMyCardExtende };
});
