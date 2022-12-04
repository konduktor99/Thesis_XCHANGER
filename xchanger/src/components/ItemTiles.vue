<template>
<div v-if="items && !error">
  <transition name="fade">
    <router-view/>
  </transition>

<div class="main-main">

  <router-link v-for="item in items" :key="item" :to=" `/items/${item.id}`" style="text-decoration: none; color: inherit; ">
    <ItemTile  v-bind="item" v-bind:modify="false"/>
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
    category: String   
  },
  components: {
    ItemTile
  },
  data: () => {
    return{
      items: undefined,
      error: undefined
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
          this.error = `${error.response.status} ${error.response.data} :(`
        });
      }
    },
    mounted:function(){
      this.getItems();
  }
}

</script>
<style>
.fade-enter-from,
.fade-leave-to{
  opacity: 0;
}

.fade-enter-to,
.fade-leave-from{
  opacity: 1;
}


.fade-enter-active{
  transition: all 1.5s ease;
}
.fade-leave-active{
  transition: all 0.5s ease;
 
}

</style>


