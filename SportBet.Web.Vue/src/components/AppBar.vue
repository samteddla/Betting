<template>
  <v-navigation-drawer v-model="drawer" :width="344">
    <!-- <v-list>
          <v-list-item
            prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg"
            title="Sandra Adams"
            subtitle="sandra_a88@gmailcom"
          ></v-list-item>
      </v-list>-->

    <!-- search bar-->
    <v-responsive width="90%" class="my-6 mx-2">
      <v-text-field density="compact" flat hide-details label="Search" rounded="lg" single-line
        variant="solo-filled"></v-text-field>
    </v-responsive>
    <v-divider></v-divider>

    <div class="mx-2 my-3" width="140px" title="Sports">Sports</div>

    <div v-if="store.matchs" class="d-flex flex-wrap ga-3 mx-6 my-3">
      <v-card class="mx-auto" v-for="n in store.matchs" :key="n.matchSelectionId" :color="getColor(n.matchSelectionId)" width="140px"
        link :to="`/game/${n.matchSelectionId}`" @click="closeDrawer">
        <v-card-text>
          {{ n.name }}
        </v-card-text>
      </v-card>
    </div>

    <v-divider></v-divider>

    <v-list density="compact" nav>
      <v-list-item prepend-icon="mdi-folder" title="Home" value="myfiles" to="/" @click="closeDrawer"></v-list-item>
      <v-list-item prepend-icon="mdi-account-multiple" title="About" value="shared" to="/about"
        @click="closeDrawer"></v-list-item>
      <v-list-item prepend-icon="mdi-star" title="Login" value="starred" to="/login" @click="closeDrawer"></v-list-item>
      <v-list-item prepend-icon="mdi-card" title="Cards" value="cards" to="/cards" @click="closeDrawer"></v-list-item>
      <v-list-item prepend-icon="mdi-card" title="ACards" value="Acards" to="/acards" @click="closeDrawer"></v-list-item>
      <v-list-item prepend-icon="mdi-delete" title="Register" value="trash" to="/register"
        @click="closeDrawer"></v-list-item>
      <v-list-item prepend-icon="mdi-cloud" title="Admin Teams" value="backups" to="/admin"
        @click="closeDrawer"></v-list-item>
    </v-list>


  </v-navigation-drawer>

  <v-app-bar scroll-behavior="elevate">
    <!--         image="https://picsum.photos/1920/1080?random">-->
    <v-app-bar-nav-icon @click="drawer = !drawer">
    </v-app-bar-nav-icon>
    <v-img height="30" src="@/assets/logo.svg" />
    <!-- <v-app-bar-title>Application</v-app-bar-title>-->
    <v-spacer></v-spacer>


    <v-btn v-for="link in links" :key="link" :text="link" :to="`/${link.toLowerCase()}`" variant="text"></v-btn>
    <v-btn icon="mdi-dots-vertical" @click="toggleTheme"></v-btn>
  </v-app-bar>
</template>

<script lang="ts" setup>
import { ref, onMounted,watch } from 'vue'
import { useDisplay, useTheme } from 'vuetify'
import { MatchStore } from '@/store';

const store = MatchStore();
const theme = useTheme();
const drawer = ref(false)
const links = ref(['Register', 'Login']);
const color = ref(['green', 'blue', 'yellow', 'pink', 'red', 'purple', 'orange', 'grey', 'brown', 'teal']);
const { mobile } = useDisplay()

watch(mobile, (val) => {
  console.log('current value is :', val);
})

onMounted(() => {
  console.log(mobile.value) // false

  store.getMatches();
});

const closeDrawer = () => {
  drawer.value = false
}

const getColor = (i: number) => {
  var id = i % color.value.length;
  return color.value[id]
}
const toggleTheme = () => {
  theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark';
  console.log(theme.global.name.value);
}


</script>
