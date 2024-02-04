<script setup lang="ts">
import { TeamComponent } from '@/components/Teams';
import { MatchStore } from '@/store';

const store = MatchStore();
store.getMyCards();
</script>

<template>
    <TeamComponent />
</template>