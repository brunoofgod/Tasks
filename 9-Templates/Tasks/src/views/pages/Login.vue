<template>
  <div class="app flex-row align-items-center">
    <div class="container">
      <b-row class="justify-content-center">
        <b-col md="8">
          <b-card-group>
            <b-card no-body class="p-4">
              <b-card-body>
                <b-form>
                   <b-alert v-show="Mensagem" show variant="danger"> {{Mensagem}} </b-alert>
                  <h1>Entrar</h1>
                  <p class="text-muted">Entre na sua conta</p>
                  <b-input-group class="mb-3">
                    <b-input-group-prepend><b-input-group-text><i class="icon-user"></i></b-input-group-text></b-input-group-prepend>
                    <input type="text" 
                      class="form-control"
                      placeholder="E-mail"
                      autocomplete="username email"
                      v-model="Email"
                      id="email"
                      v-on:keyup.enter="login" />
                  </b-input-group>
                  <b-input-group class="mb-4">
                    <b-input-group-prepend><b-input-group-text><i class="icon-lock"></i></b-input-group-text></b-input-group-prepend>
                    <input type="password" 
                      class="form-control" 
                      placeholder="Senha" 
                      autocomplete="current-password" 
                      v-model="Senha"
                      v-on:keyup.enter="login" />
                  </b-input-group>
                  <b-row>
                    <b-col cols="6">
                      <botao-duplo-clique variant="primary" text="Entrar" class="px-4" @click="login" ref="botaoLogar" />
                    </b-col>
                  </b-row>
                </b-form>
              </b-card-body>
            </b-card>
            <b-card no-body class="text-white bg-primary py-5 d-md-down-none" style="width:44%">
              <b-card-body class="text-center">
                <div>
                  <h2>Ainda não tem uma conta?</h2>
                  <p>Clique abaixo e registre-se.</p>
                  <button @click="$router.push('/register')" class="btn btn-primary active mt-3">Criar uma conta!</button>
                </div>
              </b-card-body>
            </b-card>
          </b-card-group>
        </b-col>
      </b-row>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Login',
  data() {
    return {
      Email: "",
      Senha : "" ,
      Mensagem: "",
    }
  },
  mounted() {
    document.getElementById('email').focus()
  },
  methods: {
    login() {
      let login = this.Email
      let senha = this.Senha
      this.$refs.botaoLogar.loading = true
      var vm = this
      this.$store.dispatch('login', { login, senha })
        .then(() => this.$router.push('/'))
        .catch(err => vm.CatchLogin(err)
        )
    },
    CatchLogin(err){
      this.Mensagem="Usuário ou senha inválidos"
      this.$refs.botaoLogar.loading=false
    }
  },  
}
</script>
