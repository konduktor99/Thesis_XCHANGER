<template >
<div>

  <!-- <router-view v-slot="{ Component }">
  <transition name="fade">
    <component :is="Component" />
  </transition>
</router-view> -->

<h2 v-if="this.$route.query.category">{{this.$route.query.category}}</h2>
<h2 v-if="this.$route.query.search">Wyniki dla '{{this.$route.query.search}}'</h2>
<div v-if="(items && !error) || user">
<div v-if="replyingExchange" class="alert alert-primary" role="alert">
  Wybierz przedmiot który chcesz wymienić za <b>{{this.exchangeItem}}</b>.
</div>
<div class="main-main">
    <div v-if="usr" class="details-item-user" >
        <h3>{{usr.login}}</h3> <br>
        <h4>{{usr.phoneNumber}}</h4> <br>
        <h5><b>{{usr.email}}</b></h5> <br>
        <h5>Od {{new Date(usr.joinDate).toLocaleDateString('pl-PL')}} na platformie.</h5> 
        
        </div>  
    <div v-if="error && !userAccess" id="error"> 
    <h1>{{error}}</h1>
    </div>
    <div v-if="!error && !items" class="loader-wrapper"><div class="lds-facebook"><div></div><div></div><div></div></div></div>
    <router-link v-if="userAccess" id="add-article-tile" to="/my-profile/add-item">
  
    <i  class="fa fa-plus-square"></i>
 
  </router-link> 
  <router-link v-for="item in items" :key="item" :to="{ path: `/items/${item.id}`, query: {replyingExchange:replyingExchange, exchangeItem:exchangeItem}}" style="text-decoration: none; color: inherit; margin: 2px 0 17px 0; ">
    <ItemTile  v-bind="item" v-bind:modify="userAccess" @deletedItem="deleteItem($event)"/>
  </router-link>

</div>
</div>
<div v-else-if="!items && !error" class="loader-wrapper"><div class="lds-facebook"><div></div><div></div><div></div></div></div>
<div v-else id="error"> 
 <h1>{{error}}</h1>
</div>
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
    userAccess: Boolean,
    replyingExchange: Number,
    exchangeItem: String,
  },
  components: {
    ItemTile
  },
  data: () => {
    return{
      items: undefined,
      error: undefined,
      usr: undefined,
      search: undefined
    }
  },
    methods:{

      deleteItem(itemId){
        var index = this.items.findIndex((e => e.id == itemId));
        this.items.splice(index,1)
      },


      getItems(){
        let pathPart = ""
        if(this.user && this.category)
        pathPart = `category/${this.category}/user/${this.user}`
        else if(this.user)
        pathPart = `user/${this.user}`
        else if(this.category)
        pathPart = `category/${this.category}`

        if(this.search){
        axios.get(`Items/${pathPart}`,{ params:{Query:this.search}})
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
            this.error = `${mess} :(`
          });
        }else{

          axios.get(`Items/${pathPart}`, )
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
            this.error = `${mess} :(`
          });
        }
         
      


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
          this.error = `${mess} :(`
        });
      }
    },
    mounted:function(){
      this.search = this.$route.query.search,
      this.getItems();
      if(this.user)
        this.getUser();
        
  }
}

</script>



