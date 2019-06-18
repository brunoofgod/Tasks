import Vue from 'vue'

export default Vue.filter('pt-br', function (value) {
    if(value == null || value == undefined || value == '') return '';
    if(value.includes("T")){
        value = value.split("T")[0];
        value = value.replace(new RegExp("-", "g"), "/");
    }
    return new Date(value).toLocaleDateString('pt-BR', { year: 'numeric', month: '2-digit', day: '2-digit' })
});