<script setup lang="ts">
    import { MatchStore } from '@/store';
    const store = MatchStore();
    store.getMyCards();
</script>

<template>
  <div>
    <h2>My Cards</h2>
    <div v-if="store.cards" class="d-flex flex-wrap ga-3 mx-6 my-3">
      <v-card
        class="mx-auto"
        v-for="n in store.cards"
        :key="n.id"
        width="640px"
        link
        :to="`/card/${n.id}`"
        :title="`${n.id} ${n.typeName}`"
      >
        <v-card-text>
          {{ n.name }}  CARD ID :  {{ n.id  }}
        </v-card-text>
      </v-card>
    </div>  
  </div>

  <div v-if="store.cards">
    <div>
      <pre>{{ store.cards }}</pre>
    </div>
  </div>
</template>