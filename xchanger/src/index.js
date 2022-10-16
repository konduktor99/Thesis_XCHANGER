import { createRouter, createWebHistory } from 'vue-router'
import ItemTiles from './components/ItemTiles.vue'
import LogInForm from './components/LogInForm.vue'
import SignInForm from './components/SignInForm.vue'
import ItemDetails from './components/ItemDetails.vue'

const routes = [
  // {
  //   path: '/login',
  //   name: 'LogIn',
  //   component:
  //    LogInForm
     
    
  // },
  {
    path: '/signin',
    component: SignInForm
  },
  {
    path: '/',
    component: ItemTiles,
    children: [

      { path: 'login', component: LogInForm },
    ],
  },
  {
    path: '/item',
    component: ItemDetails,
  }

]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})
export default router
