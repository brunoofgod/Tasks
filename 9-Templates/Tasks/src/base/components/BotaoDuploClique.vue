<template>
    <b-button :disabled="loading" :variant="variant" class="btn-square" @click="click"><i class="fa fa-spinner fa-spin" v-show="loading"></i> {{text}}</b-button>
</template>

<script>

export default {
    name: 'botao-duplo-clique',
    props: {
        variant:{
            type: String,
            default: "default"
        },
        text:{
          type: String,
          default: "Salvar"
        }
    },
    data() {
        return {
            loading: false,
        }
    },
    methods: {
        click(){
            this.loading=true;
            this.$emit("click");
        },
        unloading(){
            this.loading=false;
        }
    },
}
</script>

<style>

</style>
