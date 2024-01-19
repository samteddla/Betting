
code
```js
<v-app>
    <v-app-bar app>
    
    </v-app-bar>
    <!-- navigation drawer -->
    <v-navigation-drawer app>
    </v-navigation-drawer>  

    <!--  main content -->
    <v-main>
        <v-container>
            <!--  route-view -->
            <RouterView />
        </v-container>
    </v-main>

    <v-footer app>
    </v-footer>
</v-app>
```
class="text-center"

code  baseline
```js
<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer">
      <!--  -->
    </v-navigation-drawer>

    <v-app-bar>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-app-bar-title>Application</v-app-bar-title>
    </v-app-bar>

    <v-main>
      <!--  -->
    </v-main>
  </v-app>
</template>

<script setup>
  import { ref } from 'vue'

  const drawer = ref(null)
</script>

<script>
  export default {
    data: () => ({ drawer: null }),
  }
</script>
```
