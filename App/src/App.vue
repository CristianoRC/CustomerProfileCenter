<template>
  <v-app>
    <v-main>
      <Register />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import Register from './components/Register.vue';

export default defineComponent({
  name: 'App',
  components: {
    Register
  },
  data() {
    return {
      //
    }
  },
})
</script>
