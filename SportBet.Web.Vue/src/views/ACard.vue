<template>
    <v-sheet elevation="12" rounded="lg" width="100%" class="pa-4 mx-auto">
        <v-container fluid fill-width v-if="store.cardExtended" style="max-width: 600px; width: 100%;">
            <v-row>
                <v-col>
                    <v-card>
                        <v-card-title>
                            <v-icon icon="mdi-calendar" start></v-icon>{{ store.cardExtended.matchSelectionName }} ID {{
                                store.cardExtended.matchSelectionId }}
                        </v-card-title>
                        <v-card-text>
                            <v-row>
                                <v-col>
                                    <div>{{ store.cardExtended.matchSelectionDescription }} {{ store.cardExtended.matchType
                                    }}</div>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>

            <table class="left mx-auto">
                <thead>
                    <tr>
                        <th colspan="2"></th>
                        <th>
                            <v-row>
                                <v-col v-for="c in outcomes" :key="c.outcomeId" style="width: 33%;">
                                    {{ c.name }}
                                </v-col>
                            </v-row>
                        </th>
                    </tr>
                </thead>
                <tbody style="font-size: smaller;">
                    <tr v-for="match in store.cardExtended.matches" :key="match.matchId">
                        <td>{{ match.homeTeam }}</td>
                        <td>{{ match.awayTeam }} {{ match.matchResultId }}</td>
                        <td>
                            <v-row class="small">
                                <v-col v-for="c in outcomes" :key="c.outcomeId">
                                    <v-checkbox v-model="store.playOutcome"
                                        :class="aWin(match.matchResultId??0, c.outcomeId) ? match.matchResult : ''" label=""
                                        disabled hide-details :value="{ matchId: match.matchId, outcomeId: c.outcomeId }">
                                    </v-checkbox>
                                </v-col>
                            </v-row>
                        </td>
                    </tr>
                </tbody>
            </table>
            <v-card>
                <v-card-title>
                    <v-icon icon="mdi-star" start></v-icon> CardId: {{ store.cardExtended.betCardId }}
                </v-card-title>
                <v-card-text>
                    <v-row>
                        <v-col>
                            <div> Bet :{{ store.cardExtended.betAmount }} Win: {{ store.cardExtended.wonAmount }} total
                                WinCount : {{
                                    store.cardExtended.totalWinCount }}</div>
                        </v-col>
                    </v-row>
                </v-card-text>
            </v-card>
        </v-container>
    </v-sheet>

    <v-container v-if="show">
        <v-row>
            <v-col>
                cardExtended:
                <pre>{{ store.cardExtended?.matches }}</pre>
            </v-col>
            <v-col>
                playedCard:
                <pre>{{ store.playedCard }}</pre>
            </v-col>
            <v-col>
                playOutcome :
                <pre>{{ store.playOutcome }}</pre>
            </v-col>
        </v-row>

    </v-container>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { watch, onMounted, ref } from 'vue'
import { MatchStore } from '@/store';

const store = MatchStore();
const route = useRoute()
const show = ref(true);
const outcomes = ref([
    {
        "outcomeId": 1,
        "name": "H"
    },
    {
        "outcomeId": 2,
        "name": "A"
    },
    {
        "outcomeId": 4,
        "name": "D"
    }]);


// fetch the user information when params change
watch(
    () => route.params.id,
    async id => {
        console.log('watch Id : ', id);
        store.getMyCardExtende(parseInt(route.params.id.toString()));
    }
)

onMounted(() => {
    console.log('mounted');
    console.log('mounted Id : ', route.params.id);
    store.getMyCardExtende(parseInt(route.params.id.toString()));
    if (store.cardExtended) {
        console.log('mountd.cardId : ', store.cardExtended.betCardId);
    }
})

const aWin = (matchResultId: number, outcomeId: number) => {
    let originalOutcomes = [1, 2, 4, 5, 3, 6, 7];
    let combinedOutcomes = [[1],[2],[4],[1, 4],[1, 2],[2, 4],[1, 2, 4]];

    let dictionary = originalOutcomes.reduce((acc: any, k: number , i: number) => {
        acc[k] = combinedOutcomes[i];
        return acc;
    }, {});

    let bothOutcomes = [5, 3, 6, 7];

    if (outcomeId === matchResultId) {
        return true;
    } else {
        let r = (dictionary as { [key: number]: number[] })[matchResultId] || [];
        let o = (dictionary as { [key: number]: number[] })[outcomeId] || [];

        let matchResultIds = bothOutcomes.includes(matchResultId) ? r : [matchResultId];
        let outcomeIds = bothOutcomes.includes(outcomeId) ? o : [outcomeId];

        let win = matchResultIds.filter(id => outcomeIds.includes(id)).length === 1;
        return win;
    }
}

</script>

<style scoped>
.Win {
    color: #4CAF50;
}

.Loss {
    color: red;
}

.NotPlayed {
    color: rgb(50, 60, 105);
}

.small {
    font-size: smaller;
}
</style>