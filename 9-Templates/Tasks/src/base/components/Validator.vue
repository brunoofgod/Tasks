<template>
    <div>
        <b-form-group slot="component" :invalid-feedback="invalidFeedback" :state="erroDoCampo && erroDoCampo.campo == null">
            <label for="vat">{{campoDescritivo}}</label><small v-if="small"> {{small}}</small>
            <slot name="component" />
        </b-form-group>
    </div>
</template>

<script>

export default {
    name: 'validator',
    props: {
        erros:{
            type: Array
        },
        propName:{
            type: String
        },
        campoDescritivo:{
            type: String
        },
        small:{
            type: String
        }
    },
    computed: {
        invalidFeedback(){
            if(this.erroDoCampo && this.erroDoCampo.mensagem.length > 0) return this.erroDoCampo.mensagem;
        }
    },
    created() {
        this.getErroDoCampo();
    },
    watch: {
        erros(){
            this.getErroDoCampo();
        },
        erroDoCampo(){
            if(this.erroDoCampo && this.erroDoCampo.mensagem.length > 0){
                 document.getElementById(this.propName).classList.add("is-invalid")
                 document.getElementById(this.propName).classList.add("border-red")

                  //tratamento do select2
                  var el = document.getElementById(this.propName).children[0];
                  if(el.className == 'dropdown-toggle'){
                    el.classList.add("border-red");
                  }
            }
        }
    },
    data() {
        return {
            erroDoCampo: {mensagem: '', campo: null}
        }
    },
    methods: {
        getErroDoCampo(){
            var vm = this;
            this.erroDoCampo = this.erros.find(x => x.campo == vm.propName);
        }
    },
}
</script>

<style>
.border-red{
  border-color: #f86c6b !important
}
</style>
