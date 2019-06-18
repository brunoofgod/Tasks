<template>
  <div class="animated fadeIn">
    <mensagem></mensagem>
    <b-row v-if="!loadingItens && items.length <= 0">
      <b-col lg="12">
        <b-card
          header-tag="header"
          footer-tag="footer">
          <div slot="header">
            <i class="fa fa-align-justify"></i> <strong> Tasks </strong>
            <div class="card-header-actions">
            </div>
          </div>
          <div>
            <b-jumbotron header="Nenhuma Task cadastrada" >
              <b-btn variant="primary"  href="/#/task/novo" >Adicionar</b-btn>
            </b-jumbotron>
          </div>
        </b-card>
      </b-col>
    </b-row>
    <b-row v-if="!loadingItens && items.length > 0">
      <b-col lg="12">
          <div class=" fright margin-5-bottom">
            <b-button variant="primary" class="btn-square" href="/#/task/novo">Adicionar</b-button>
        </div>
      </b-col>
    </b-row>
        <b-row>
      <b-col lg="12">
          <grid-tasks
            :table-data="items" 
            :total-de-paginas="totalDePaginas"
            :action-buttom="true"
            :loading="loadingItens"
            :fields="fields" 
            caption="Tasks"
            @table-edit="editarRegistro" 
            @paginacao="paginacao"
            @table-remove="removerRegistro" >
          </grid-tasks>
          
      </b-col>
    </b-row>
  </div>
</template>

<script>
import GridTasks from '@/views/Tasks/GridTasks'
import Mensagem from '@/base/components/Mensagem'

export default {
  components: {
    GridTasks,
    Mensagem
  },
  data(){
    return {
      items: [],
      loadingItens: true,
      totalDePaginas: 1,
      fields: [
        {key: 'titulo', label: 'Título', sortable: true},
        {key: 'acoes', label: "Ações", sortable: false, thClass: "center, wd-120-px"}
      ],
    }
  },
  beforeMount(){
    this.getGridData(this.$route.params.id)
  },
  methods: {
    editarRegistro(registro){
      this.$router.push('/task/editar/' + registro)
    },
    removerRegistro(registro){
      var vm = this
      vm.loadingItens = true
      this.$http({url: 'task/excluir/' +  registro.id +  "/" + vm.$store.getters.getAutenticacao.usuarioId, method: 'DELETE' })
        .then(response => {
          vm.$store.commit("set_mensagem",{tipo: "success", mensagem: "A Task foi excluída com sucesso."})

          vm.$router.go()
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.message})
      })    
    },
    getGridData(pagina){
      var vm = this
      vm.loadingItens = true
      this.$http({url: '/task/' +  this.$route.params.id +"/" + vm.$store.getters.getAutenticacao.usuarioId, method: 'GET' })
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
