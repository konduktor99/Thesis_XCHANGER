import { createRouter, createWebHistory } from 'vue-router'
import ItemTiles from './components/ItemTiles.vue'
import LogInForm from './components/LogInForm.vue'
import SignInForm from './components/SignInForm.vue'
import ItemDetails from './components/ItemDetails.vue'
import NotFound from './components/NotFound.vue'
// import UserItemTiles from './components/UserItemTiles.vue'
// import ItemForm from './components/ItemForm.vue'
import EditItemForm from './components/EditItemForm.vue'
import ExchangesList from './components/ExchangesList.vue'

const routes = [
  // {
  //   path: '/login',
  //   name: 'LogIn',
  //   component:
  //    LogInForm
     
    
  // },
  {

    path: '/',
    redirect: '/items'
  },
  {
    path: '/signin',
    component: SignInForm
  },
  {
    path: '/login',
    component: LogInForm
  },
  {
    path: '/exchanges/requested',
    props: { exchangesType: 0},
    component: ExchangesList
  },
  {
  path: '/exchanges/received',
  props: { exchangesType: 1},
  component: ExchangesList
  },
  {
  path: '/exchanges/history',
  props: { exchangesType: 2},
  component: ExchangesList
  },
  {
    path: '/items',
    component: ItemTiles,
    props: route => ({  user: route.query.user,
      category: route.query.category,
      replyingExchange: route.query.replyingExchange,
      exchangeItem:route.query.exchangeItem,
    }),
  },

  {
    path: '/items/:id',
    component: ItemDetails,
    props: route => ({replyingExchange: route.query.replyingExchange,
      exchangeItem:route.query.exchangeItem,
    }),
  },
  {
    path: '/my-profile/edit-item/:id',
    component: EditItemForm,
    props: true

  },
  {

    path: '/my-profile',
    component: ItemTiles,

    props: route => ({  user: route.query.user,  category: route.query.category,  userAccess: true}),
  },
  {
    path: '/my-profile/add-item',
    component: EditItemForm,
    props: { create: true},
  },

  {
    path: '/:pathMatch(.*)*',
    component: NotFound,
  },


]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})
export default router
