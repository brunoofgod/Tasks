<template>
  <div class="animated fadeIn"
      v-on:keyup.enter="Editar">
    <b-row>
      <b-col sm="12" >
        <div class="fright margin-5-bottom">
            <b-button variant="secondary" class="btn-square margin-5-right" @click="Voltar" >Cancelar</b-button>
            <botao-duplo-clique variant="success" @click="Editar" ref="botaoSalvar"/>
        </div>
      </b-col>
    </b-row>
    <b-row>
      <b-col sm="8">
        <b-card>
          <div slot="header">
            <strong>Usuários </strong><small>Edição</small>
          </div>
          <b-row>
            <b-col sm="6">
              <validator prop-name="Apelido" :erros="erros" campo-descritivo="Apelido">
                <b-form-input v-model="Apelido"
                  type="text"
                  id="Apelido"
                  slot="component"
                  placeholder="Digite um apelido/nome "></b-form-input>
              </validator>
            </b-col>
          </b-row>
          <validator prop-name="Email" :erros="erros" campo-descritivo="E-mail">
            <b-form-input v-model="Email"
              type="text"
              id="Email"
              slot="component"
              placeholder="Digite o email"></b-form-input>
          </validator>
          <validator prop-name="Senha" :erros="erros" campo-descritivo="Senha">
            <b-form-input v-model="Senha"
              type="password"
              id="Senha"
              slot="component"
              placeholder="Digite a senha" />
          </validator>
        </b-card>
      </b-col>
      <b-col sm="4">
        <b-card >
          <div slot="header">
            <strong>Outras informações </strong><small>Edição</small>
          </div>
          <b-form-group>
            <label for="vat">Data de cadastro: {{dataDeCadastro | pt-br}} </label>
          </b-form-group>
            <b-form-group v-if="usuarioDeCadastro"> 
            <label for="vat">Usuário de cadastro:  {{usuarioDeCadastro}}</label>
          </b-form-group>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { Switch as cSwitch } from '@coreui/vue'

export default {
  name: 'novo-usuario',
    components:{
      cSwitch
  },
  computed:{
    now(){
      var date = new Date()
      return date.toISOString().split('T')[0]
    },
    UsuarioViewModel(){
      var vm =this
      return {
        Id: this.usuarioId || this.$uuid.v4(),
        Apelido: this.Apelido,
        UsuarioDeCadastroId: this.usuarioDeCadastroId,
        DataDeCadastro: this.dataDeCadastro,
        Senha: this.Senha,
        Email: this.Email,
      }
    }
  },
  created() {
    this.GetUsaurio()
    if(!this.usuarioId) this.dataDeCadastro= this.now
  },
  data() {
    return {
      options: [],
        usuarioId: this.$route.params.id || null,
        usuarioDeCadastro: this.$store.getters.getAutenticacao.usuario,
        usuarioDeCadastroId: this.$store.getters.getAutenticacao.usuarioId,
        dataDeCadastro: this.now,
        Apelido: "",
        Email: "",
        Senha: "",
        erros: [],
    }
  },
  methods: {
    Voltar(){
      this.$router.push('/usuarios/1')
    },
    GetUsaurio(){
      if(this.usuarioId == null ) return
      var vm = this
      this.$http({url: '/usuario/editar/' +  this.$route.params.id  , method: 'GET' })
        .then(response => {
          vm.dataDeCadastro = response.data.dataDeCadastro
          vm.Apelido = response.data.apelido
          vm.Email = response.data.email
          vm.Senha = response.data.senha
      })
      .catch(err => {
          this.$router.push('/500')
      })
    },
    Editar(){
      var vm = this
      this.$http({url: '/usuario/editar', data: this.UsuarioViewModel, method: 'PUT'  })
        .then(response => {
          if(!response.data.sucesso){
            vm.erros = response.data.erros
            vm.$refs.botaoSalvar.unloading()
            return
          }
          this.$store.commit("set_mensagem",{tipo: "success", mensagem: "O usuário foi editado com sucesso!"})
          this.$router.push('/usuarios/1')
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.response.data})
          this.$router.push('/usuarios/1')
      })
    },
  },
}
</script>