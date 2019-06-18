<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col sm="6" lg="3">
        <b-card no-body class="bg-info">
          <b-card-body class="pb-0">
            <div class="chart-wrapper center" v-if="loading" >
              <rotate-square v-if="loading"/>
            </div>
            <div v-else>
              <h4 class="mb-0">{{totalTask}}</h4>
              <p>Total de Tasks</p>
            </div>
          </b-card-body>
          <div style="height:70px" height="70"> </div>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>
<script>
import {RotateSquare} from 'vue-loading-spinner'
import { Callout } from '@coreui/vue'

export default {
  name: 'dashboard',
  components: {
    Callout,
    RotateSquare,
  },
  data() {
    return {
      loading: false,
      totalTask: 0,
    }
  },
  mounted() {
    this.GetData()
  },
  methods: {
    GetData(){
      var vm = this
      this.loading=true
      this.$http({url: '/dashboard/' + this.$store.getters.getAutenticacao.usuarioId, method: 'GET' })
        .then(response => {

          vm.totalTask = response.data
          vm.loading = false
      })
      .catch(err => {
          this.$store.commit("set_mensagem",{tipo: "danger", mensagem: err.response.data})
          this.$router.push('/metas/1')

      })
    }
  }
}
</script>