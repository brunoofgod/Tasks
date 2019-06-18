<template>
<div class="animated fadeIn">
    <b-alert v-if="mensagem" dismissible show :variant="mensagem.tipo"> {{mensagem.mensagem}} </b-alert>
</div>
</template>

<script>

export default {
    name: 'mensagem',
    mounted() {
        this.mensagem =  this.$store.getters.getMensagem || null;
        this.$store.commit("remove_mensagem");
    },
    data() {
        return {
            mensagem: null,
            getMensagem: this.$store.getters.getMensagem
        }
    },
}
</script>

<style>

</style>
