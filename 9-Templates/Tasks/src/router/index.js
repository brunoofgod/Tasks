import Vue from 'vue'
import Router from 'vue-router'

// Containers
const DefaultContainer = () => import('@/containers/DefaultContainer')

// Views
const Dashboard = () => import('@/views/Dashboard')

// Views - Pages
const Page404 = () => import('@/views/pages/Page404')
const Page500 = () => import('@/views/pages/Page500')
const Page403 = () => import('@/views/pages/Page403')
const Login = () => import('@/views/pages/Login')
const Register = () => import('@/views/pages/Register')

//Usuários
const Usuarios = () => import('@/views/Usuarios/Usuarios')
const NovoUsuario = () => import('@/views/Usuarios/NovoUsuario')

//Usuários
const Tasks = () => import('@/views/Tasks/Tasks')
const NovaTask = () => import('@/views/Tasks/NovaTask')

Vue.use(Router)

export default new Router({
  mode: 'hash',
  linkActiveClass: 'open active',
  scrollBehavior: () => ({ y: 0 }),
  routes: [
    {
      path: '/',
      redirect: '/dashboard',
      name: 'Home',
      component: DefaultContainer,
      children: [
        {
          path: 'dashboard',
          name: 'Dashboard',
          component: Dashboard,
          meta: { 
            requiresAuth: true
          }
        },
        {
          path: 'tasks',
          meta: {
            label: 'Tasks',
            requiresAuth: true
          },
          component: {
            render (c) { return c('router-view') }
          },
          children: [
            {
              path: '',
              redirect: '/tasks/1',
              component: Tasks,
              meta: { 
                requiresAuth: true
              }
            },
            {
              path: ':id',
              component: Tasks,
              meta: { 
                requiresAuth: true
              }
            },
          ]
        },
        {
          path: 'task',
          meta: { 
            label: 'Task',
            requiresAuth: true,
          },
          component: {
            render (c) { return c('router-view') }
          },
          children: [
            {
              path: '',
              redirect: '/tasks/1',
              component: Tasks,
            },
            {
              path: 'editar/:id',
              name: 'Editar task',
              component: NovaTask,
              meta: { 
                requiresAuth: true
              }
            },
            {
              path: 'novo',
              name: 'Nova task',
              component: NovaTask,
              meta: { 
                requiresAuth: true
              }
            },
          ]
        },        
        {
          path: 'usuarios',
          meta: {
            label: 'Usuarios',
            requiresAuth: true
          },
          component: {
            render (c) { return c('router-view') }
          },
          children: [
            {
              path: '',
              redirect: '/usuarios/1',
              component: Usuarios,
              meta: { 
                requiresAuth: true
              }
            },
            {
              path: ':id',
              component: Usuarios,
              meta: { 
                requiresAuth: true
              }
            },
          ]
        },
        {
          path: 'usuario',
          meta: { 
            label: 'Usuário',
            requiresAuth: true,
          },
          component: {
            render (c) { return c('router-view') }
          },
          children: [
            {
              path: '',
              redirect: '/usuarios/1',
              component: Usuarios,
            },
            {
              path: 'editar/:id',
              name: 'Editar usuario',
              component: NovoUsuario,
              meta: { 
                requiresAuth: true
              }
            },
            {
              path: 'novo',
              name: 'Novo usuario',
              component: NovoUsuario,
              meta: { 
                requiresAuth: true
              }
            },
          ]
        },
      ]
    },
    {
      path: '/',
      redirect: '/pages/404',
      name: 'Pages',
      component: {
        render (c) { return c('router-view') }
      },
      children: [
        {
          path: '404',
          name: 'Page404',
          component: Page404
        },
        {
          path: '500',
          name: 'Page500',
          component: Page500
        },
        {
          path: '403',
          name: 'Page403',
          component: Page403
        },        
        {
          path: 'login',
          name: 'Login',
          component: Login
        },
        {
          path: 'register',
          name: 'Register',
          component: Register
        }
      ]
    }
  ]
})
