<template>
  <v-sheet :elevation="10" rounded>
    <v-card>
      <v-card-title>
        <h1>Home</h1>
      </v-card-title>
      <v-card-text>
        <p>Home page</p>
      </v-card-text>
    </v-card>
  </v-sheet>

  <v-sheet :elevation="10" rounded>
    <v-card>
      <v-card-title>
        <h1>Test logout</h1>
      </v-card-title>
      <v-card-text>
        <p>Logout</p>
        <v-btn @click="logout">Logout</v-btn>
      </v-card-text>
    </v-card>
  </v-sheet>
  <div>
    <router-link to="/test">Test Page</router-link>
  </div>
</template>

<script lang="ts" setup>
import { AuthStore } from '@/store';

const store = AuthStore();

const logout = () => {
  console.log('logout');
  store.logout();
};


</script>
