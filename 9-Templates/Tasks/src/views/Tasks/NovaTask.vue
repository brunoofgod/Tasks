<template>
  <div class="animated fadeIn"
      v-on:keyup.enter="InserirOuEditar">
    <b-row>
      <b-col sm="12" >
        <div class="fright margin-5-bottom">
            <b-button variant="secondary" class="btn-square margin-5-right" @click="Voltar" >Cancelar</b-button>
            <botao-duplo-clique variant="success" @click="InserirOuEditar" ref="botaoSalvar"/>
        </div>
      </b-col>
    </b-row>
    <b-row>
      <b-col sm="8">
        <b-card>
          <div slot="header">
            <strong>Task </strong><small>{{taskId != null ? "Edição" : "Inserção"}}</small>
          </div>
          <validator prop-name="Titulo" :erros="erros" campo-descritivo="Título">
            <b-form-input v-model="Titulo"
              type="text"
              id="Titulo"
              slot="component"
              placeholder="Digite um titulo" />
        </validator>
          <validator prop-name="Descricao" :erros="erros" campo-descritivo="Descrição">
            <b-form-input v-model="Descricao"
              type="text"
              id="Descricao"
              slot="component"
              placeholder="Digite uma descricao" />
        </validator>   
        <validator prop-name="Conclusao" :erros="erros" campo-descritivo="Conclusão">
          <b-form-input v-model="Conclusao"
            type="date"
            Id="Conclusao"
            slot="component" />
        </validator>             
        </b-card>
      </b-col>
      <b-col sm="4">
        <b-card >
          <div slot="header">
            <strong>Outras informações </strong><small>{{taskId != null ? "Edição" : "Inserção"}}</small>
          </div>
          <b-form-group>
            <label v-show="taskId != null" for="vat">Data de cadastro: {{dataDeCadastro | pt-br}} </label>
          </b-form-group>
          <b-form-group>
            <label>Status: </label><b-badge >{{Conclusao != null ? "Concluído" : "Aberto"}}</b-badge>
          </b-form-group>

        </b-card>
      </b-col>
    </b-row>
  </div>
</template>
<script>

export default {
  computed:{
    TaskViewModel(){
      return {
        Id: this.taskId || this.$uuid.v4(),
        Titulo: this.Titulo,
        Descricao: this.Descricao,
        Conclusao: this.Conclusao,
        UsuarioId: this.$store.getters.getAutenticacao.usuarioId
      }
    }
  },
  created() {
    this.GetTask()
  },
  data() {
    return {
      options: [],
        taskId: this.$route.params.id || null,
        Nome: "",
        erros: [],
        Conclusao: null,
        dataDeCadastro: null,
        Descricao: null,
        Titulo: null,
    }
  },
  methods: {
    Voltar(){
      this.$router.push('/tasks')
    },
    GetTask(){
      if(this.taskId == null ) return

      var vm = this
      this.$http({url: '/task/editar/' +  this.$route.params.id + "/" + this.$store.getters.getAutenticacao.usuarioId, method: 'GET' })
        .then(response => {
          vm.Conclusao = response.data.conclusao != null ? response.data.conclusao.split("T")[0] : null
          vm.dataDeCadastro = response.data.dataDeCadastro != null ? response.data.dataDeCadastro.split("T")[0] : null

          vm.Titulo = response.data.titulo
          vm.Descricao = response.data.descricao
      })
      .catch(err => {
          this.$router.push('/500')
      })
    },
    InserirOuEditar(){
      if(this.taskId != null ){
        this.Editar()
        return
      }

      this.Inserir()
    },
    Editar(){
      var vm = this
      this.$http({url: '/task/editar', data: this.TaskViewModel, method: 'PUT'  })
        .then(response => {
          if(!response.data.sucesso){
            vm.erros = response.data.erros
            vm.$refs.botaoSalvar.unloading()
            return
          }
          this.$store.commit("set_mensagem",{tipo: "success", mensagem: "A categoria foi editado com sucesso!"})
          this.$router.push('/tasks/1')
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.response.data})
          this.$router.push('/tasks/1')
      })
    },
    Inserir(){
      var vm = this
      this.$http({url: '/task/novo', data: this.TaskViewModel, method: 'POST'  })
        .then(response => {
          if(!response.data.sucesso){
            vm.erros = response.data.erros
            vm.$refs.botaoSalvar.unloading()
            return
          }
          this.$store.commit("set_mensagem",{tipo: "success", mensagem: "A categoria foi salva com sucesso!"})
          this.$router.push('/tasks/1')
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.response.data})
          this.$router.push('/tasks/1')
      })
    },
  },
}
</script>