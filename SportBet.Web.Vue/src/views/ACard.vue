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
                    <tr v-for="match in store.cardExtended.matches" :key="match.matchId" >
                        <td>{{ match.homeTeam }}</td>
                        <td>{{ match.awayTeam }} {{ match.matchResultId}}</td>
                        <td>
                            <v-row class="small">
                                <v-col v-for="c in outcomes" :key="c.outcomeId">
                                    <v-checkbox v-model="store.playOutcome" 
                                        :class="match.matchResultId == c.outcomeId ? match.matchResult : ''"
                                        label="" 
                                        disabled
                                        hide-details                                     
                                        :value="{ matchId: match.matchId, outcomeId: c.outcomeId }">
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
                            <div> Bet :{{ store.cardExtended.betAmount }} Win: {{ store.cardExtended.wonAmount }} total WinCount : {{
                        store.cardExtended.totalWinCount }}</div>
                        </v-col>
                    </v-row>
                </v-card-text>
            </v-card>
        </v-container>
    </v-sheet>

    <v-container v-if="show">
        <div>
            .cardExtended:
            <pre>{{ store.cardExtended }}</pre>
        </div>
        <div>
            playedCard:
            <pre>{{ store.playedCard }}</pre>
        </div>
        <div>
            playOutcome :
            {{ store.playOutcome }}
        </div>
    </v-container>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { watch, onMounted, ref } from 'vue'
import { MatchStore } from '@/store';

const store = MatchStore();
const route = useRoute()
const show = ref(false);
const outcomes = ref([
    {
        "outcomeId": 1,
        "name": "H"
    },
    {
        "outcomeId": 4,
        "name": "D"
    },
    {
        "outcomeId": 2,
        "name": "A"
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