<template>
    <div class="main-content">
        <h2>Team</h2>
    </div>
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>ShortName</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="team in store.teams" :key="team.id">
                <td>{{ team.id }}</td>
                <td>{{ team.name }}</td>
                <td>{{ team.shortName }}</td>
            </tr>
        </tbody>
    </table>
</template>

<script setup lang="ts">
import { TeamStore } from '@/store';

const store = TeamStore();
store.getTeams();

</script>