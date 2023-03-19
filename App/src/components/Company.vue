<template>
    <v-card :loading="loading" text="Pessoa Juridica" variant="tonal">
        <v-card-actions>
            <v-container>
                <v-row justify="end">
                    <v-btn color="primary" elevation="3">Cadastrar</v-btn>
                </v-row>
            </v-container>
        </v-card-actions>
    </v-card>
</template>

<script lang='ts'>
import { defineComponent } from 'vue'
export default defineComponent({
    name: 'Company',
    components: {
    },
    data: () => ({
        loading: false,
    }),
})
</script>