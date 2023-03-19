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
                <v-text-field v-model="cep.state" disabled label="Estado"></v-text-field>
                <v-text-field v-model="cep.city" disabled label="Cidade"></v-text-field>
                <v-text-field v-model="cep.neighborhood" disabled label="Bairro"></v-text-field>
            </form>
        </v-container>
        <v-card-actions>
            <v-container>
                <v-row justify="end">
                    <v-btn @click="createCustomer" color="primary" elevation="3" :disabled="loading">Cadastrar</v-btn>
                </v-row>
            </v-container>
        </v-card-actions>
    </v-card>
    <v-snackbar color="orange-darken-2" v-model="showError" timeout="2000">
        {{ error }}
        <template v-slot:actions>
            <v-btn variant="text" @click="showError = false">
                Fechar
            </v-btn>
        </template>
    </v-snackbar>

    <v-snackbar color="blue-darken-2" v-model="showSuccess" timeout="2000">
        Cliente Cadastrado com Sucesso
        <template v-slot:actions>
            <v-btn variant="text" @click="showSuccess = false">
                Fechar
            </v-btn>
        </template>
    </v-snackbar>
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
            const url = `${process.env.VUE_APP_API}/Address/${cep}`;

            axios.get(url).then(response => {
                this.$data.cep.city = response.data.content.city
                this.$data.cep.neighborhood = response.data.content.neighborhood
                this.$data.cep.state = response.data.content.uf
            }).catch(error => {
                this.$data.showError = true;
                if (error.response.status === 400)
                    this.$data.error = error.response.data.error.errorMessage;
                else
                    this.$data.error = "Ocorreu um erro inesperado ao obter inforamções do CEP.";
            }).finally(() => {
                this.$data.loading = false;
            })
        },
        createCustomer: function () {
            this.$data.loading = true;
            const url = `${process.env.VUE_APP_API}/Customer`;

            axios.post(url, this.$.data.fields).then(response => {
                this.$data.showSuccess = true;

            }).catch(error => {
                this.$data.showError = true;
                if (error.response.status === 409)
                    this.$data.error = "Cliente já cadastrado.";
                else
                    this.$data.error = error.response.data.errorMessage;

            }).finally(() => {
                this.$data.loading = false;
            })
        }
    },
    components: {
    },
    data: () => ({
        loading: false,
        showError: false,
        showSuccess: false,
        error: "error",
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
            birthday: null,
            phonenumber: "",
            emailAddress: "",
            corporateName: "",
        },
        cep: {
            city: "",
            neighborhood: "",
            state: ""
        }
    }),
})
</script>