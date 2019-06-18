<template>
  <div class="app flex-row align-items-center">
    <div class="container">
      <b-row class="justify-content-center">
        <b-col md="6" sm="8">
          <b-card no-body class="mx-4">
            <b-card-body class="p-4">
              <b-form>
                <h1>Registrar</h1>
                <p class="text-muted">Criar uma conta</p>
                <validator prop-name="Apelido" :erros="erros" campo-descritivo="Apelido">
                  <b-form-input v-model="Apelido"
                    type="text"
                    id="Apelido"
                    slot="component"
                    placeholder="Digite um apelido/nome" />
                </validator>
                <validator prop-name="Email" :erros="erros" campo-descritivo="E-mail">
                  <b-form-input v-model="Email"
                    type="text"
                    id="Email"
                    slot="component"
                    placeholder="Digite um e-mail" />
                </validator>

                <validator prop-name="Senha" :erros="erros" campo-descritivo="Senha">
                  <b-form-input v-model="Senha"
                    type="password"
                    id="Senha"
                    slot="component"
                    placeholder="Digite uma senha" />
                </validator>
                <b-button variant="secondary" class="btn-square margin-5-right" @click="$router.push('/login')" >Cancelar</b-button>
                <botao-duplo-clique variant="success" @click="Inserir" ref="botaoSalvar" text="Criar conta"/>
              </b-form>
            </b-card-body>
          </b-card>
        </b-col>
      </b-row>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Register',
  computed:{
    UsuarioViewModel(){
      return {
        Id: this.$uuid.v4(),
        Apelido: this.Apelido,
        DataDeCadastro: this.DataDeCadastro,
        Senha: this.Senha,
        Email: this.Email,
      }
    }
  },
  data(){
    return {
      Apelido: "",
      Email: "",
      Senha: "",
      erros: [],
      DataDeCadastro: "",
    }
  },
  methods:{
    Inserir(){
      var vm = this
      this.$http({url: '/usuario/novo', data: this.UsuarioViewModel, method: 'POST'  })
        .then(response => {
          if(!response.data.sucesso){
            vm.erros = response.data.erros
            vm.$refs.botaoSalvar.unloading()
            return
          }
          this.$store.commit("set_mensagem",{tipo: "success", mensagem: "O usuário foi salvo com sucesso!"})
          this.$router.push('/login')
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.response.data})
          this.$router.push('/500')
      })
    },
  },
}
</script>
