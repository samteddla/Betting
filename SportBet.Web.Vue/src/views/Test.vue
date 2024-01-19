<template>
        <v-table style="max-width: 600px; width: 100%;" class="left mx-auto">
            <thead>
                <tr>
                    <th class="text-left">
                        Name
                    </th>
                    <th class="text-left">
                        Description
                    </th>
                    <th class="text-left">
                        H
                    </th>
                    <th class="text-left">
                        A
                    </th>
                    <th class="text-left">
                        D
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in desserts" :key="item.name">
                    <td>{{ item.name }}</td>
                    <td>{{ item.fat }}</td>
                    <td>
                        <v-checkbox multiple label="" color="red" value="red" hide-details></v-checkbox>
                    </td>
                    <td>
                        <v-checkbox label="" color="blue" value="blue" hide-details></v-checkbox>
                    </td>
                    <td>
                        <v-checkbox label="" color="green" value="green" hide-details></v-checkbox>
                    </td>
                </tr>
            </tbody>
        </v-table>

    <v-sheet elevation="12" max-width="600" rounded="lg" width="100%" class="pa-4 text-center mx-auto">
        <v-table>
            <thead>
                <tr>
                    <th class="text-left">
                        Name
                    </th>
                    <th class="text-left">
                        Description
                    </th>
                    <th class="text-left">
                        carbs (% DV)
                    </th>
                    <th class="text-left">
                        sodium (% DV)
                    </th>
                    <th class="text-left">
                        calcium
                    </th>
                    <th class="text-left">
                        Calories
                    </th>
                    <th class="text-left">
                        Calories
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in desserts" :key="item.name">
                    <td>{{ item.name }}</td>
                    <td>{{ item.fat }}</td>
                    <td>{{ item.carbs }}</td>
                    <td>{{ item.protein }}</td>
                    <td>{{ item.sodium }}</td>
                    <td>{{ item.calcium }}</td>
                    <td>{{ item.calories }}</td>
                </tr>
            </tbody>
        </v-table>
    </v-sheet>
    <div height="10px;"></div>
    <v-card class="mx-auto" elevation="1" max-width="500">
        <v-card-title class="py-5 font-weight-black">Securely access your tax form</v-card-title>

        <v-card-text>
            To download your tax form from GitHub Sponsors on Stripe Express, you must also verify the Tax ID number used on
            your tax forms, as they contain sensitive personal information.
        </v-card-text>

        <v-card-text>
            <div class="text-subtitle-2 font-weight-black mb-1">Last 4 digits of your SSN</div>

            <v-text-field label="Enter value here" single-line variant="outlined"></v-text-field>

            <v-btn :disabled="loading" :loading="loading" block class="text-none mb-4" color="indigo-darken-3"
                size="x-large" variant="flat" @click="login">
                Verify and continue
            </v-btn>

            <v-btn block class="text-none" color="grey-lighten-3" size="x-large" variant="flat">
                Cancel
            </v-btn>
        </v-card-text>
    </v-card>

    <v-divider>
        <span class="text-subtitle-2 font-weight-black">OR</span>
    </v-divider>

    <v-sheet class="position-relative" min-height="450">
        <div class="position-absolute d-flex align-center justify-center w-100 h-100">
            <v-btn size="x-large" color="deep-purple-darken-2" @click="dialog = !dialog">
                Open Dialog
            </v-btn>
        </div>

        <v-fade-transition hide-on-leave>
            <v-card v-if="dialog" append-icon="$close" class="mx-auto" elevation="16" max-width="500"
                title="Send a receipt">
                <template v-slot:append>
                    <v-btn icon="$close" variant="text" @click="dialog = false"></v-btn>
                </template>

                <v-divider></v-divider>

                <div class="py-12 text-center">
                    <v-icon class="mb-6" color="success" icon="mdi-check-circle-outline" size="128"></v-icon>

                    <div class="text-h4 font-weight-bold">This receipt was sent</div>
                </div>

                <v-divider></v-divider>

                <div class="pa-4 text-end">
                    <v-btn class="text-none" color="medium-emphasis" min-width="92" rounded variant="outlined"
                        @click="dialog = false">
                        Close
                    </v-btn>
                </div>
            </v-card>
        </v-fade-transition>
    </v-sheet>

    <v-sheet elevation="12" max-width="600" rounded="lg" width="100%" class="pa-4 text-center mx-auto">
        <v-icon class="mb-5" color="success" icon="mdi-check-circle" size="112"></v-icon>

        <h2 class="text-h5 mb-6">You reconciled this account</h2>

        <p class="mb-4 text-medium-emphasis text-body-2">
            To see a report on this reconciliation, click <a href="#" class="text-decoration-none text-info">View
                reconciliation report.</a>

            <br>

            Otherwise, you're done!
        </p>

        <v-divider class="mb-4"></v-divider>

        <div class="text-end">
            <v-btn class="text-none" color="success" rounded variant="flat" width="90">
                Done
            </v-btn>
        </div>
    </v-sheet>
</template>


<script lang="ts" setup>
import { ref, watch } from 'vue';

const loading = ref(false);
const dialog = ref(false);

watch(loading, (val: boolean) => {
    if (!val) return;

    setTimeout(() => (loading.value = false), 2000);
});

const login = () => {
    loading.value = !loading.value;
    console.log('login');
};

const desserts = ref([
    {
        name: 'Frozen Yogurt',
        calories: 159,
        fat: "Frozen Yogurt",
        carbs: 24,
        protein: 4.0,
        sodium: 87,
        calcium: '14%',
        iron: '1%',
    },
    {
        name: 'Ice cream sandwich',
        calories: 237,
        fat: "Ice cream sandwich",
        carbs: 37,
        protein: 4.3,
        sodium: 129,
        calcium: '8%',
        iron: '1%',
    },
    {
        name: 'Eclair',
        calories: 262,
        fat: "Eclair",
        carbs: 23,
        protein: 6.0,
        sodium: 337,
        calcium: '6%',
        iron: '7%',
    },
    {
        name: 'Cupcake',
        calories: 305,
        fat: "Cupcake",
        carbs: 67,
        protein: 4.3,
        sodium: 413,
        calcium: '3%',
        iron: '8%',
    },
    {
        name: 'Gingerbread',
        calories: 356,
        fat: "Gingerbread",
        carbs: 49,
        protein: 3.9,
        sodium: 327,
        calcium: '7%',
        iron: '16%',
    },
    {
        name: 'Jelly bean',
        calories: 375,
        fat: "Jelly bean",
        carbs: 94,
        protein: 0.0,
        sodium: 50,
        calcium: '0%',
        iron: '0%',
    },
    {
        name: 'Lollipop',
        calories: 392,
        fat: "Lollipop",
        carbs: 98,
        protein: 0,
        sodium: 38,
        calcium: '0%',
        iron: '2%',
    },
    {
        name: 'Honeycomb',
        calories: 408,
        fat: "Honeycomb",
        carbs: 87,
        protein: 6.5,
        sodium: 562,
        calcium: '0%',
        iron: '45%',
    },
    {
        name: 'Donut',
        calories: 452,
        fat: "Donut",
        carbs: 51,
        protein: 4.9,
        sodium: 326,
        calcium: '2%',
        iron: '22%',
    },
    {
        name: 'Frozen Yogurt',
        calories: 159,
        fat: "Frozen Yogurt",
        carbs: 24,
        protein: 4.0,
        sodium: 87,
        calcium: '14%',
        iron: '1%',
    },
    {
        name: 'Ice cream sandwich',
        calories: 237,
        fat: "Ice cream sandwich",
        carbs: 37,
        protein: 4.3,
        sodium: 129,
        calcium: '8%',
        iron: '1%',
    },
    {
        name: 'Eclair',
        calories: 262,
        fat: "Eclair",
        carbs: 23,
        protein: 6.0,
        sodium: 337,
        calcium: '6%',
        iron: '7%',
    }
]);
</script>
<style lang="">
    
</style>