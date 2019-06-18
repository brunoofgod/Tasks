<template>
  <AppHeaderDropdown right no-caret>
    <template slot="header">
      <label for="">{{$store.getters.getAutenticacao.usuario}}</label>
      <img
        src="img/avatars/6.png"
        class="img-avatar"
        alt="admin@bootstrapmaster.com" />
    </template>
    <template slot="dropdown">
      <b-dropdown-item @click="Logout"><i class="fa fa-lock" /> Sair</b-dropdown-item>
    </template>
  </AppHeaderDropdown>
</template>

<script>
import { HeaderDropdown as AppHeaderDropdown } from '@coreui/vue'
export default {
  name: 'DefaultHeaderDropdownAccnt',
  components: {
    AppHeaderDropdown
  },
  data(){
    return { itemsCount: 42 }
  },
  methods:{
    Logout(){
    this.$store.dispatch('logout')
      .then(() => {
        this.$router.push('/login')
      })
    }
  }
}
</script>
