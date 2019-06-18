<template>
  <b-card :header="caption" v-if="items.length > 0 || loading">
    <div class="center" v-show="loading"><rotate-square /></div>
    <b-table 
      v-if="!loading && items.length > 0"
      :hover="true"
      responsive="sm" 
      :items="items" 
      :fields="captions" 
      striped
      :per-page="25">
      <template slot="acoes" slot-scope="data" >
        <div class="btn-group-sm" style="text-align: right" > 
          <b-button  variant="warning"  @click="edit(data.item)" style="margin-right:10px " title="Editar" >
              <i class="fa fa-edit fa-lg" style="color: black"></i>
          </b-button>
          <b-button  variant="danger"  @click="clicklixeira(data.item)" title="Excluir">
              <i class="fa fa-trash-o fa-lg" style="color: black"></i>
          </b-button>
        </div>
      </template>
    </b-table>
    <nav>
      <b-pagination-nav
        size="md" 
        :number-of-pages="totalDePaginas" 
        v-model="currentPage" 
        :link-gen="linkGen" >
      </b-pagination-nav>
    </nav>
    <b-modal 
      title="Confirmar exclusão"
      class="modal-danger" 
      v-model="openModalConfirm" 
      @ok="remove" 
      ok-variant="danger">Você confirma a exclusão desse registro? 
    </b-modal>

  </b-card>
</template>
<script>
import {RotateSquare} from 'vue-loading-spinner'

export default {
  components:{
    RotateSquare,
  },
  inheritAttrs: false,
  props: {
    caption: {
      type: String,
      default: 'Table'
    },
    tableData: {
      type: [Array, Function],
      default: () => []
    },
    totalDePaginas: {
      type: Number,
      default: 1
    },
    fields: {
        type: [Array, Object],
        default: () => []
    },
    loading:{
      type: Boolean,
      default: true
    }
  },
  watch: {
    currentPage(){
      this.$emit("paginacao", this.currentPage )
    }
  },
  created() {
    this.currentPage = parseInt(this.$route.params.id)
  },
  data(){
    return {
      currentPage: 1,
      openModalConfirm: false,
      itemExcluir: null
    }
  },
  computed: {
    items() {
      const items =  this.tableData
      return Array.isArray(items) ? items : items()
    },
    captions() { return this.fields }
  },
  methods: {
    edit(obj){
      this.$router.push('/task/editar/' + obj.id)
    },
    remove(obj){
      this.openModalConfirm=false
      this.$emit('table-remove', this.itemExcluir)
      this.itemExcluir=null
    },
    linkGen (pageNum) {
      return '/#/tasks/' + pageNum
    },
    clicklixeira(item){
      this.openModalConfirm=true
      this.itemExcluir=item
    }
  }
}
</script>
