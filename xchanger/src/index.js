import { createRouter, createWebHistory } from 'vue-router'
import ItemTiles from './components/ItemTiles.vue'
import LogInForm from './components/LogInForm.vue'
import SignInForm from './components/SignInForm.vue'
import ItemDetails from './components/ItemDetails.vue'
import NotFound from './components/NotFound.vue'
import UserItemTiles from './components/UserItemTiles.vue'
import ItemForm from './components/ItemForm.vue'
import EditItemForm from './components/EditItemForm.vue'


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
    path: '/items',
    component: ItemTiles,
    props: route => ({  user: route.query.user,  category: route.query.category}),
    children: [

      { path: 'login', component: LogInForm },
    ],
  },
//{
  // path: '/items/user/:login',
  //   component: ItemTiles,
  //   props: true,
  //   // children: [

  //   //   { path: 'login', component: LogInForm },
  //   // ],
  // },
  {
    path: '/items/:id',
    component: ItemDetails,
    props: true
    //props: {imageUrl: require("../assets/images/kask.png")}
  },
  {
    path: '/my-profile/edit-item/:id',
    component: EditItemForm,
    props: true
    //props: {imageUrl: require("../assets/images/kask.png")}
  },
  {
    path: '/my-profile',
    component: UserItemTiles,
    //props: {imageUrl: require("../assets/images/kask.png")}
  },
  {
    path: '/my-profile/add-item',
    component: ItemForm,
    //props: {imageUrl: require("../assets/images/kask.png")}
  },

  {
    path: '/:pathMatch(.*)*',
    component: NotFound,
    //props: {imageUrl: require("../assets/images/kask.png")}
  },


]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})
export default router
