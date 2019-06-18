import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)
	
const url ="http://localhost:57356/"

export default new Vuex.Store({
	state: {
  		status: '',
  		baseURL: url,
  		token: JSON.parse(localStorage.getItem('user-token')) || "",
		user : {},
		mensagem: JSON.parse(localStorage.getItem('mensagem'))
	},
	mutations: {
		auth_request(state){
	    	state.status = 'loading'
	  	},
	  	auth_success(state, token, user){
		    state.status = 'success'
		    state.token = token
		    state.user = user
	  	},
	  	auth_error(state){
	    	state.status = 'error'
	  	},
	  	logout(state){
	    	state.status = ''
	    	state.token = ''
		  },
		set_mensagem(state, msgObj){
			state.mensagem = msgObj;
			localStorage.setItem('mensagem', JSON.stringify(msgObj));
			
		},
		remove_mensagem(state){
			state.mensagem = null;
			localStorage.removeItem('mensagem');
		}
	},
	actions: {
	  	login({commit}, user){
	        return new Promise((resolve, reject) => {
	            commit('auth_request')
	            axios({url: url+"login", data: user, method: 'POST' })
	            .then(resp => {
	                const token = resp.data
	                const user = resp.data.usuarioId
					
					localStorage.setItem('user-token', JSON.stringify(token))
	                // Add the following line:
	                axios.defaults.headers.common['Authorization'] = "bearer " + token.tokenDeAcesso
	                commit('auth_success', token, user);
	                resolve(resp);
	            })
	            .catch(err => {
	                commit('auth_error')
	                localStorage.removeItem('token')
	                reject(err);
	            })
	        })
	    },
	  	logout({commit}){
		    return new Promise((resolve, reject) => {
            commit('logout')
		      	localStorage.removeItem('user-token')
		      	delete axios.defaults.headers.common['Authorization']
		      	resolve()
		    })
		}
	},
	getters : {
	  	isLoggedIn: state => {
			if(!state.token || !state.token.dataDeExpiracao) return false
			var dataDeExpiracao = new Date(state.token.dataDeExpiracao)
			var dataAtual =new Date()
		
			if(dataDeExpiracao <= dataAtual) return false
			return true
        },
	  authStatus: state => state.status,
	  getAutenticacao: state => state.token,
	  getMensagem: state => state.mensagem,
	  emptyGuid: state => state.emptyGuid,
	  baseURL: state => state.baseURL,
	},
});