<template>
    <v-card :loading="loading" text="Pessoa Fisica" variant="tonal">
        <v-container>
            <form>
                <v-text-field v-model="fields.name" label="Nome"></v-text-field>
                <v-text-field v-model="fields.document.number" label="CPF" type="number"></v-text-field>
                <v-text-field v-model="fields.phonenumber" label="Telefone" type="number"></v-text-field>
                <v-text-field v-model="fields.emailAddress" label="E-Mail" type="email"></v-text-field>
                <v-text-field v-model="fields.birthday" label="Data de nascimento" type="date"></v-text-field>
                <v-text-field prepend-inner-icon="mdi-map-marker" v-model="fields.address.cep" type="number" label="CEP"
                    @focusout="getCep()"></v-text-field>
                <v-text-field v-model="fields.address.number" label="Numero"></v-text-field>
                <v-text-field v-model="fields.address.complement" label="Complemento"></v-text-field>
                <v-text-field v-model="cep.city" disabled label="Cidade"></v-text-field>
                <v-text-field v-model="cep.state" disabled label="Estado"></v-text-field>
                <v-text-field v-model="cep.neighborhood" disabled label="Bairro"></v-text-field>


            </form>
        </v-container>
        <v-card-actions>
            <v-container>
                <v-row justify="end">
                    <v-btn color="primary" elevation="3" :disabled="loading">Cadastrar</v-btn>
                </v-row>
            </v-container>
        </v-card-actions>
    </v-card>
    <pre>{{ fields }}</pre>
</template>

<script lang='ts'>
import { defineComponent } from 'vue'
import axios from 'axios'

export default defineComponent({
    name: 'Individual',
    methods: {
        getCep: function () {
            this.$data.loading = true;
            const cep = this.$data.fields.address.cep;
            const url = `http://customerprofile-api.cristianoprogramador.com/Address/${cep}`;
            axios.get(url).then(response => {
                console.log(response);
                this.$data.cep.city = ""
                this.$data.cep.neighborhood = ""
                this.$data.cep.state = ""
            }).finally(() => { this.$data.loading = false; })
        }
    },
    components: {
    },
    data: () => ({
        loading: false,
        fields: {
            name: "",
            document: {
                number: "",
                documentType: 0
            },
            address: {
                cep: "",
                number: "",
                complement: ""
            },
            birthday: "",
            phonenumber: "",
            emailAddress: ""
        },
        cep: {
            city: "",
            neighborhood: "",
            state: ""
        }
    }),
})
</script>