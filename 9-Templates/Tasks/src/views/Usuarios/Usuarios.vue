<template>
  <div class="animated fadeIn">
    <mensagem></mensagem>
    <b-row v-if="!loadingItens && items.length <= 0">
      <b-col lg="12">
        <b-card
          header-tag="header"
          footer-tag="footer">
          <div slot="header">
            <i class="fa fa-align-justify"></i> <strong> Usuaaaários </strong>
            <div class="card-header-actions">
            </div>
          </div>
        </b-card>
      </b-col>
    </b-row>
    <b-row>
      <b-col lg="12">
          <grid-usuarios
            :table-data="items" 
            :total-de-paginas="totalDePaginas"
            :action-buttom="true"
            :loading="loadingItens"
            :fields="fields" 
            caption="Usuários"
            @paginacao="paginacao"
            @table-remove="removerRegistro" >
          </grid-usuarios>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import GridUsuarios from '@/views/Usuarios/GridUsuarios'
import Mensagem from '@/base/components/Mensagem'

export default {
  name: 'usuarios',
  components: {
    GridUsuarios,
    Mensagem
  },
  data(){
    return {
      items: [],
      loadingItens: true,
      totalDePaginas: 1,
      fields: [
        {key: 'nome', label: 'Nome', sortable: true},
        {key: 'email', label: 'E-mail', sortable: true},
        {key: 'acoes', label: "Ações", sortable: false, thClass: "center, wd-120-px"}
      ],
    }
  },
  beforeMount(){
    this.getGridData(this.$route.params.id)
  },
  methods: {
    removerRegistro(registro){
      var vm = this
      vm.loadingItens = true
      this.$http({url: 'usuario/excluir/' +  registro.id  + "/" + vm.$store.getters.getAutenticacao.usuarioId, method: 'DELETE' })
        .then(response => {
          this.$store.dispatch('logout')
            .then(() => {
              this.$router.push('/login')
            })
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.message})
      })    
    },
    getGridData(pagina){
      var vm = this
      vm.loadingItens = true
      this.$http({url: '/usuario/' +  this.$route.params.id  + "/" + vm.$store.getters.getAutenticacao.usuarioId, method: 'GET' })
        .then(response => {
          vm.loadingItens=false
          vm.items =  response.data.listagem
          vm.totalDePaginas =response.data.paginas
      })
      .catch(err => {
          alert(err.message)
      })
    },
    paginacao(ev){
      this.getGridData(ev)
    },
  },
}
</script>
