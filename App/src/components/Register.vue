<template>
  <v-card>
    <v-layout>
      <v-app-bar color="indigo-darken-1">
        <template v-slot:prepend>
          <v-app-bar-nav-icon></v-app-bar-nav-icon>
        </template>
        <v-app-bar-title>Customer Control Center - Register</v-app-bar-title>
        <template v-slot:append>
          <v-btn icon="mdi-dots-vertical"></v-btn>
        </template>
      </v-app-bar>

      <v-main>
        <v-container>
          <v-tabs v-model="tab" color="deep-purple-accent-4" align-tabs="center">
            <v-tab :value="1">Pessoa Fisica</v-tab>
            <v-tab :value="2">Pessoa Juridica</v-tab>
          </v-tabs>
          <v-window v-model="tab">
            <v-container>
              <v-window-item :key="1" :value="1">
                <v-container>
                  <v-row justify="center">
                    <v-col cols="7">
                      <Individual></Individual>
                    </v-col>
                  </v-row>
                </v-container>
              </v-window-item>

              <v-window-item :key="2" :value="2">
                <v-container justify="center">
                  <v-row justify="center">
                    <v-col cols="7">
                      <Company />
                    </v-col>
                  </v-row>
                </v-container>
              </v-window-item>

            </v-container>
          </v-window>
        </v-container>
      </v-main>
    </v-layout>
  </v-card>
</template>
<script lang='ts'>
import { defineComponent } from 'vue'
import Individual from './Individual.vue';
import Company from './Company.vue';

export default defineComponent({
  name: 'Register',
  components: {
    Individual,
    Company
  },
  data: () => ({
    tab: null,
  }),
})
</script>
