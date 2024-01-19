<template>
    <v-container>
        <v-col>
         <table class="table table-striped">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>description</th>
                    <th>isEnabled</th>
                    <th>View</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="m in store.matchs" :key="m.matchSelectionId">
                    <td>{{ m.matchSelectionId }}</td>
                    <td>{{ m.name }}</td>
                    <td>{{ m.description }}</td>
                    <td>{{ m.isEnabled }}</td>
                    <td><RouterLink :to="{ name: 'Game', params: { id: m.matchSelectionId }}">View</RouterLink></td>              
                </tr>
            </tbody>
        </table>
        </v-col>
    </v-container>
</template>

<script setup lang="ts">
import { MatchStore } from '@/store';
import { RouterLink } from 'vue-router';

const store = MatchStore();
store.getMatches();

</script>