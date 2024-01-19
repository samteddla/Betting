<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useAlertStore } from '@/store';
import { onMounted, ref } from 'vue';

const alertStore = useAlertStore();
const { alert } = storeToRefs(alertStore);

const error = ref(alert.value !== null);

function clearAlert() {
    console.log('alert cleared');
    alertStore.clear();
    error.value = false;
}

onMounted(() => {
    error.value = alert.value !== null;
    console.log('mounted');
    console.log(alert.value);
    // alert.value = alertStore.getAlert();
});

</script>

<template>
    <div>
        <v-alert 
        v-model="error"
        :color="alert?.type.toLowerCase()"
        icon="$success"
        closable
        close-label="Close Alert"
        :title="alert?.type"
        @input="clearAlert"
        >
            {{ alert?.message }}
        </v-alert>
    </div>
</template>

<style scoped>
.alert-container {
    padding-top: 15px;
}

@media screen and (min-width: 750px) {
    .alert-container {
        padding-top: 50px;
    }
}

.closebtn {
    margin-left: 15px;
    font-weight: bold;
    float: right;
    font-size: 22px;
    line-height: 20px;
    cursor: pointer;
    transition: 0.3s;
}

.closebtn:hover {
    color: black;
}

.alert {
    display: block;
    padding: 6px;
    margin: 6px;
    border-radius: 3px;
    color: #000000;
}

.error {
    background-color: #ffdddd;
    border-left: 6px solid #f44336;
}

.success {
    background-color: #ddffdd;
    border-left: 6px solid #04AA6D;
}

.info {
    background-color: #e7f3fe;
    border-left: 6px solid #2196F3;
}

.warning {
    background-color: #ffffcc;
    border-left: 6px solid #ffeb3b;
}
</style>