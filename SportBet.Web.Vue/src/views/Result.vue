<template>
    <v-sheet elevation="12" rounded="lg" width="100%" class="pa-4 mx-auto">
        <v-container v-if="store.match" style="max-width: 600px; width: 100%;">
            <v-row>
                <v-col>
                    <v-card>

                        <v-card-title>
                            <v-icon icon="mdi-calendar" start></v-icon>{{ store.match.name }} ID {{
                                store.match.matchSelectionId }}
                        </v-card-title>
                        <v-card-text>
                            <v-row>
                                <v-col>
                                    <div>{{ store.match.description }}</div>
                                </v-col>
                                <v-col>
                                    <div>{{ store.match.activeUntil }}</div>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>

            <v-tabs v-model="matchtypeId" show-arrows slider-color="yellow" slider-size="2" centered grow
                bg-color="gray-lighten-2" class="v-btn--block">
                <v-tab value="1" style="font-size: 10px;">Full time</v-tab>
                <v-tab value="2" style="font-size: 10px;">Half time</v-tab>
                <v-tab value="3" style="font-size: 10px;">Both</v-tab>
            </v-tabs>
        </v-container>


        <v-container fluid fill-width v-if="store.match" style="max-width: 600px; width: 100%;">
            <table class="left mx-auto">
                <thead>
                    <tr>
                        <th colspan="2"></th>
                        <th>
                            <v-row>
                                <v-col v-for="c in outcomes" :key="c.outcomeId" style="width: 15%;">
                                    {{ c.name }}
                                </v-col>
                            </v-row>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="match in store.match.matches" :key="match.matchSelectionId">
                        <td>{{ match.home }}</td>
                        <td>{{ match.away }}</td>
                        <td>
                            <v-row>
                                <v-col v-for="c in outcomes" :key="c.outcomeId">
                                    <v-checkbox v-model="selectedChoices" label=""
                                        :value="{ matchId: match.matchId, outcomeId: c.outcomeId }">
                                    </v-checkbox>
                                </v-col>
                            </v-row>
                        </td>
                    </tr>
                </tbody>
            </table>                    
            <div class="pa-2"></div>
            <v-btn block class="text-none mb-4" color="indigo-darken-3"
                size="x-large" variant="flat" @click="submitMatch">Update Result</v-btn>

            <v-btn block class="text-none" color="grey-lighten-3" size="x-large" variant="flat" @click="clearSelection">
                clear selections
            </v-btn>            
        </v-container>
    </v-sheet>
    <v-container v-if="store.match">
        <v-col>
            <div>{{ matchtypeId }}</div>
            <div>selectedChoices (!) :
                <pre>{{ selectedChoices }}</pre>
            </div>        
        </v-col>
    </v-container>
</template>

<script lang="ts" setup>

import { useRoute } from 'vue-router'
import { ref, watch, onMounted, onUnmounted } from 'vue'
import { MatchStore } from '@/store';
import { type UpdateBetResultRequest } from '@/api/api2';
const store = MatchStore();
const outcomes = ref([{ "outcomeId": 1, "name": "H" }, { "outcomeId": 4, "name": "A" }, { "outcomeId": 2, "name": "D" }]);
const matchtypeId = ref(1);
const selectedChoices = ref([]);
const route = useRoute()
const routeId = ref('0');
const result = ref([]);

// fetch the user information when params change
watch(() => route.params.id,
    async id  => {
        getMatch();
        routeId.value = id.toString();
    }
)

onMounted(() => {
    console.log('mounted: ', route.params.id.toString());
    getMatch();
});

const clearSelection = () => {
    selectedChoices.value = [];
    result.value = [];
}

const getMatch = async () => {
    await store.getMatch(parseInt(route.params.id.toString()));
    selectedChoices.value = [];
}

onUnmounted(() => {
    console.log('unmounted');
});

const submitMatch = async () => {
    
    var responses = {
        "matchSelectionId": store.match?.matchSelectionId,
        "matchtypeId": matchtypeId.value,
        "UpdateBetResultRequest": selectedChoices.value.map((c: any) => {
        return {                      
            "matchId": c.matchId,
            "outcomeId": c.outcomeId
        }
       })
    };

    console.log(responses);
    
    var resp = await store.updateBetResult(responses);

    console.log(store.updatedBetResult);
}
</script>