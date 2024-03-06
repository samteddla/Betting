<template>
    <v-alert v-if="store.match" v-model="alertVisible" variant="outlined" type="success" :title="store.match.description"
        :text="alertVisibleMessage"></v-alert>
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
                                    <div>{{ moment(store.match.activeUntil).format('DD.MM.YYYY')  }}</div>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>

            <v-tabs v-model="matchtypeId" show-arrows slider-color="yellow" slider-size="2" centered grow
                bg-color="gray-lighten-2" class="v-btn--block">
                <v-tab value="0" style="font-size: 10px;">Full time</v-tab>
                <v-tab value="1" style="font-size: 10px;">Half time</v-tab>
                <v-tab value="2" style="font-size: 10px;">Both</v-tab>
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
                                    <v-checkbox v-model="store.playOutcome" label=""
                                        :value="{ matchId: match.matchId, outcomeId: c.outcomeId }">
                                    </v-checkbox>
                                </v-col>
                            </v-row>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="pa-2"></div>
            <v-btn block class="text-none mb-4" color="indigo-darken-3" size="x-large" variant="flat"
                @click="submitMatch">Update Result</v-btn>

            <v-btn block class="text-none" color="grey-lighten-3" size="x-large" variant="flat" @click="clearSelection">
                clear selections
            </v-btn>
        </v-container>
    </v-sheet>
    <v-container v-if="store.match">
        <v-row>
            <v-col>
                <div>matchResult:
                    <pre>{{ store.matchResult }}</pre>
                </div>
            </v-col>
            <v-col>
                <div>{{ matchtypeId }}</div>
                <div>selectedChoices (!) :
                    <pre>{{ selectedChoices }}</pre>
                </div>
            </v-col>
            <v-col>
                <div>playOutcome:
                    <pre>{{ store.playOutcome }}</pre>
                </div>
            </v-col>
        </v-row>
    </v-container>
</template>

<script lang="ts" setup>
import moment from 'moment';
import { useRoute } from 'vue-router'
import { ref, watch, onMounted, onUnmounted } from 'vue'
import { MatchStore } from '@/store';
const store = MatchStore();
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
const matchtypeId = ref(0);
const selectedChoices = ref([]);
const route = useRoute()
const routeId = ref('0');
const result = ref([]);
const alertVisible = ref(false);
const alertVisibleMessage = ref('');

// fetch the user information when params change
watch(() => route.params.id,
    async id => {
        getMatchResult(1, route.params.id.toString());
        routeId.value = id.toString();
    }
)

// watch matchtypeId
watch(() => matchtypeId.value,
    async matchtypesId => {
        matchtypesId = matchtypesId + 1;
        getMatchResult(matchtypesId, route.params.id.toString());
    }
)

onMounted(() => {
    console.log('mounted: ', route.params.id.toString());
    getMatchResult(1, route.params.id.toString());
});

const clearSelection = () => {
    selectedChoices.value = [];
    result.value = [];
}

const getMatchResult = async (matchtypeId: number, matchSelectionId: string) => {
    await store.getMatchResults(matchtypeId, parseInt(matchSelectionId.toString()));
    selectedChoices.value = [];
}

onUnmounted(() => {
    console.log('unmounted');
});

const submitMatch = async () => {
    var responses = {
        "matchSelectionId": store.match?.matchSelectionId,
        "matchtypeId": matchtypeId.value,
        "UpdateBetResultRequest": store.playOutcome.map((c: any) => {
            return {
                "matchId": c.matchId,
                "outcomeId": c.outcomeId
            }
        })
    };

    console.log(responses);
    await store.updateMatchResult(responses);
    alertVisible.value = true;
    alertVisibleMessage.value = (store.updatedBetResult?.map((c: any) => c.message) ?? []).join('\n\r');
    console.log(store.updatedBetResult);
    setTimeout(() => (alertVisible.value = false), 5000);
}
</script>