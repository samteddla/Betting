
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
It works with nginx !!!
dockercompile
```docker
FROM node:latest as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY ./ .
RUN npm run build

FROM nginx as production-stage
RUN mkdir /app
COPY --from=build-stage /app/dist /app
COPY nginx.conf /etc/nginx/nginx.conf
CMD ["nginx", "-g", "daemon off;"]
```

# Build the Docker image
```bash
docker build . -t my-app
docker run -d -p 8080:80 my-app
```