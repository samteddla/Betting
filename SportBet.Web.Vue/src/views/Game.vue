<template>
    <v-alert v-model="alertVisible" v-if="store.match" variant="outlined" type="success" :title="store.match.description"
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
                                    <div>{{ store.match.activeUntil }}</div>
                                </v-col>
                            </v-row>
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>

            <v-tabs v-model="selectionId" show-arrows slider-color="yellow" slider-size="2" centered grow
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
            <v-sheet elevation="12" rounded="lg" width="100%" class="pa-2 mx-auto">
                <v-row>
                    <v-col>
                        <div>Costs: {{ costs }}</div>
                    </v-col>
                </v-row>
            </v-sheet>
            <div class="pa-2"></div>
            <v-btn v-if="canBuy" block class="text-none mb-4" color="indigo-darken-3" size="x-large" variant="flat"
                @click="submitMatch">Buy now</v-btn>

            <v-btn block class="text-none" color="grey-lighten-3" size="x-large" variant="flat" @click="clearSelection">
                clear selections
            </v-btn>
            <v-btn v-if="!isAuth" block class="text-none mb-4" color="indigo-darken-3" size="x-large" variant="flat"
                to="/login">
                Login and Buy
            </v-btn>
        </v-container>
    </v-sheet>
    <v-container v-if="show">
        <v-col>
            <div>{{ selectionId }}</div>
            <div>selectedChoices :
                <pre>{{ selectedChoices }}</pre>
            </div>
            <div>{{ result }}</div>
            <div> {{ reversedArray }}</div>
        </v-col>
    </v-container>

    <v-footer v-if="canBuy" app name="footer">
        <v-row>
            <v-col>
                <div>Costs: {{ costs }}</div>
            </v-col>
            <v-col>
                <v-btn v-if="canBuy" block class="text-none mb-4" color="indigo-darken-3" variant="flat"
                    @click="submitMatch">Buy</v-btn>

            </v-col>
        </v-row>

    </v-footer>
</template>

<script lang="ts" setup>
import { useRoute } from 'vue-router'
import { ref, watch, onMounted, onUnmounted } from 'vue'
import { MatchStore, AuthStore } from '@/store';
import router from '@/router';

const show = ref(true);
const costs = ref(0);
const canBuy = ref(false);
const halfandFullTime = ref(1);
const store = MatchStore();
const auth = AuthStore();
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
const isAuth = ref(false)
const selectionId = ref(1);
const selectedChoices = ref([]);
const route = useRoute()
const routeId = ref('0');
const result = ref([]);
const reversedArray = ref([]);
const alertVisible = ref(false);
const alertVisibleMessage = ref('');
// fetch the user information when params change
watch(() => route.params.id,
    async id => {
        console.log('watch Id : ', id);
        getMatch();
        routeId.value = id.toString();
    }
)

// watch the selectedChoices AND selectionId at the same time
watch([selectedChoices, selectionId], (val: any) => {
    var machIds = store.match.matches.map((m: any) => m.matchId);
    var length = machIds.length;
    var selectedMatchIds = val[0].map((c: any) => c.matchId);
    var wat = parseInt(val[1].toString());

    halfandFullTime.value = wat === 3 ? 2 : 1;

    var found = machIds.filter(function (item: number) {
        return selectedMatchIds.indexOf(item) == -1;
    });

    if (found.length === 0) {
        if (selectedMatchIds.length < length) {
            canBuy.value = false;
        }
        else {
            canBuy.value = true;
        }
    } else {
        canBuy.value = false;
    }

    if (canBuy.value) {
        costs.value = Math.pow(2, selectedMatchIds.length - length) * halfandFullTime.value;
        testMe();
    }
    isAuth.value = auth.user !== null;
    if (!isAuth.value) {
        canBuy.value = false;
    }

}, { immediate: false });

onMounted(() => {
    console.log('mounted: ', route.params.id.toString());
    getMatch();
});

const clearSelection = () => {
    selectedChoices.value = [];
    costs.value = 0;
    reversedArray.value = [];
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
    const responses = {
        "selectionId": store.match?.matchSelectionId,
        "matchTypeId": selectionId.value, /* 1 full time 2 half time  3 full time and half time*/
        "amount": costs.value,
        "matches": selectedChoices.value.map((c: any) => {
            return {
                "matchId": c.matchId,
                "outcomeId": c.outcomeId
            }
        })
    };

    console.log(responses);

    var resp = await store.betOn(responses);
    console.log(resp);
    if (resp) {
        if (resp.isSaved) {
            alertVisible.value = true;
            alertVisibleMessage.value = 'Your bet is saved!';
            setTimeout(() => (alertVisible.value = false), 5000);
            clearSelection();
        } else {
            alertVisible.value = true;
            alertVisibleMessage.value = 'Your bet is not saved!';
            setTimeout(() => (alertVisible.value = false), 5000);
        }
    }
}

const showCards = () => {
    router.push({ name: 'mycards' });
}

const testMe = () => {

    // Web match list to db!
    const inputArray = selectedChoices.value.map((c: any) => {
        return {
            "matchId": c.matchId,
            "outcomeId": c.outcomeId
        }
    });

    const groupedMatches = {};
    result.value = [];
    inputArray.forEach((item) => {
        const matchId = item.matchId;
        const outcomeId = item.outcomeId;

        if (!groupedMatches[matchId]) {
            groupedMatches[matchId] = { matchId, outcomeId: outcomeId };
        } else {
            groupedMatches[matchId].outcomeId += outcomeId;
        }
    });

    for (const matchId in groupedMatches) {
        result.value.push(groupedMatches[matchId]);
    }

    // db match list to web
    const originalOutcomes = [1, 2, 4, 5, 3, 6, 7]
    const combinedOutcomes = [[1], [2], [4], [1, 4], [1, 2], [2, 4], [1, 2, 4]];

    reversedArray.value = [];
    for (const item of result.value) {
        const matchId = item.matchId;
        const originalOutcomeId = item.outcomeId;

        // Find the index of originalOutcomeId in originalOutcomes
        const index = originalOutcomes.indexOf(originalOutcomeId);
        if (index !== -1) {
            const lists = combinedOutcomes[index];

            for (let i = 0; i < lists.length; i++) {
                const outcomeId = lists[i];
                reversedArray.value.push({ matchId, outcomeId });
            }
        }
    }
}

</script>