<template>

  <router-view v-slot="{ Component }">
  <transition name="fade">
    <component :is="Component" />
  </transition>
</router-view>

<div v-if="items && !error">
<div class="main-main">

    <div v-if="usr" class="details-item-user" >
        <h3>{{usr.login}}</h3> <br>
        <h4>{{usr.phoneNumber}}</h4> <br>
        <h5>Od {{new Date(usr.joinDate).toLocaleDateString('pl-PL')}} na platformie.</h5>    
    </div>
    <router-link v-if="userAccess" id="add-article-tile" to="/my-profile/add-item">
  
    <i  class="fa fa-plus-square"></i>
 
  </router-link>
  <router-link v-for="item in items" :key="item" :to=" `/items/${item.id}`" style="text-decoration: none; color: inherit; margin: 2px 0 17px 0; ">
    <ItemTile  v-bind="item" v-bind:modify="userAccess"/>
  </router-link>

</div>
</div>
<div v-else-if="!items && !error" class="loader-wrapper"><div class="lds-facebook"><div></div><div></div><div></div></div></div>
<div v-else id="error"> 
 <h1>{{error}}</h1>
</div>
</template>

<script>

import ItemTile from './ItemTile.vue'
import axios from 'axios'
export default {
  name: 'ItemTiles',
  props: {
    user: String,
    category: String,
    userAccess: Boolean
  },
  components: {
    ItemTile
  },
  data: () => {
    return{
      items: undefined,
      error: undefined,
      usr: undefined,
    }
  },
    methods:{
      getItems(){
        let pathPart = ""
        if(this.user)
        pathPart = `user/${this.user}`
      

        axios.get(`Items/${pathPart}`)
        .then((response)=>{
          this.items = response.data;
        }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe rządanie"
                break;
              case 404:
                mess = error.response.data
                break;
              case 500:
                mess = "Wystąpił błąd wewnętrzny serwera"
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${error.response.status} ${mess} :(`
        });
      },
       getUser(){
      
        axios.get(`Users/${this.user}`)
        .then((response)=>{
          this.usr = response.data;
        }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe rządanie"
                break;
              case 404:
                mess = error.response.data
                break;
              case 500:
                mess = error.response.data
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${error.response.status} ${mess} :(`
        });
      }
    },
    mounted:function(){
      this.getItems();
      if(this.user)
        this.getUser();
  }
}

</script>
<style scoped>

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}

</style>


