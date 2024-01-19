<template>
  <v-app>
    <AppBar />
    <v-main>
      <v-container fluid>
       <alert-component>
        
       </alert-component>
       <router-view>
       </router-view>
      </v-container>
    </v-main>
  </v-app>
  
</template>

<script lang="ts" setup>
import AppBar from './components/AppBar.vue'
import {AlertComponent} from '@/components'
</script>

<style scoped>

</style>

