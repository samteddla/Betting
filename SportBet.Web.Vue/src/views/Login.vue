<template>
    <v-container fluid fill-height>
     <v-row align="center" justify="center">
       <v-col cols="12" sm="12" md="6">
        <v-card
            border
            class="mb-2"
            density="compact"
            prepend-icon="mdi-lock"
            subtitle="Login to your account!"
            title="LOGIN"
            variant="text"
          >
           <v-card-text>
             <v-form @submit.prevent="submitLogin" alidate-on="submit lazy">
               <v-text-field
                    v-model="login.email"
                    :rules="emailRules"
                    label="E-mail"                    
                    clearable
                    required
                    class="mb-4 pa-2"
                    >
               </v-text-field>
               <v-text-field 
                    v-model="login.password" 
                    label="Password" 
                    required
                    :rules="passwordRules"
                    class="mb-4 pa-2"
                    type="password"></v-text-field>
               <v-btn 
                    type="submit" 
                    color="primary" 
                    class="ml-2 pr-12 pl-12"
                    @click="submitLogin"
                    >Login</v-btn>
             </v-form>
           </v-card-text>
         </v-card>
       </v-col>
     </v-row>
   </v-container>
</template>


<script lang="ts" setup>
    import { ref } from 'vue'
    import { AuthStore  } from "@/store";

    const authStore = AuthStore();
    const login = ref<{ email?: string; password?: string }>({});

    const submitLogin = async () => {
      console.log(login.value.email, login.value.password);
        await authStore.login(login.value.email ?? '', login.value.password ?? '');
    }
    
    const emailRules = [
        (v: string) => !!v || 'E-mail is required',
        (v: string) => /^[a-z.-]+@[a-z.-]+\.[a-z]+$/.test(v) || 'E-mail must be valid',

    ]
    const passwordRules = [
        (v: string) => !!v || 'Password is required',
        (v: string) => v.length >= 8 || 'Password must be at least 8 characters',
    ]   
</script>
