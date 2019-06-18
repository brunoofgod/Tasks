// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import 'core-js/es6/promise'
import 'core-js/es6/string'
import 'core-js/es7/array'
// import cssVars from 'css-vars-ponyfill'
import Vue from 'vue'
import Vuex from 'vuex'
import BootstrapVue from 'bootstrap-vue'
import App from './App'
import router from './router'
import store from './store'
import Axios from 'axios'
import UUID from 'vue-uuid';
import BotaoDuploClique from './base/components/BotaoDuploClique';
import Validator from './base/components/Validator';
import VueProgressBar from 'vue-progressbar/'



var instance = Axios.create({
  baseURL: store.getters.baseURL
});

Vue.prototype.$http = instance;
const token = JSON.parse(localStorage.getItem('user-token'));

if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = "bearer " + token.tokenDeAcesso;
}

// todo
router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuth)) {
    if(!store.getters.getAutenticacao.administrador &&  to.meta.admin)
    {
      next('/403')
    }
    if (store.getters.isLoggedIn) {
      next();
      return;
    }
    next('/login');
  } else {
    next();
  }
})
// cssVars()

var opt = {
  color: '#20a8d8',
  failedColor: '#874b4b',
  thickness: '5px',
  transition: {
    speed: '0.2s',
    opacity: '0.6s',
  }
};

Vue.use(VueProgressBar,  opt)
Vue.use(BootstrapVue)
Vue.use(Vuex)
Vue.use(UUID)
Vue.component('botao-duplo-clique', BotaoDuploClique)
Vue.component('validator', Validator)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: {
    App
  }
})
import('./base/Filters/Filters.js')
