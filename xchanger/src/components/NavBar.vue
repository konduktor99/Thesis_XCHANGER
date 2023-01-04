<template>
 <nav>

<ul>
    <li><router-link   to="/">Strona Główna </router-link> </li>
    <li><div class="dropdown">
       <a >Kategorie </a>
        <div class="dropdown-content">
          <router-link  v-for="cat in categories" :key="cat" :to="{ path: `/items`, query: {category:cat.name, user:this.$route.query.user}}" :style="this.$route.query.category==cat.name?'background-color:#629AF4;':''">{{cat.name}}</router-link>
          <transition name="fade">
          <router-link v-if="this.$route.query.category"  :to="{ path: `/items`, query: {user:this.$route.query.user}}"><b>Resetuj </b><i class="fa fa-undo"></i></router-link>
          </transition>
        </div>
      </div></li>
      <form class="search-form" style="" @submit.prevent="">
      <input style=" display:inline; width:60%" class="form-control mr-sm-2" v-model="this.search"  placeholder="Co Cię interesuje?" aria-label="Search">
      <router-link :to="{ path: `/items`, query: { search:this.search}}" @click="resetSearch" style=" display:inline; margin-left:5px;" class="btn btn-outline-success my-2 my-sm-0" >Szukaj <i class="fa fa-search"></i></router-link>
    </form>
</ul>

</nav>

</template>

<script>

import axios from 'axios'
export default {
  name: 'NavBar',

  data(){
    return{
      categories:[],
      search: undefined
    }
  },

methods:{
  resetSearch(){
    this.search = null;
  },

   getCategories(){
         axios.get('Categories')
          .then(response=>{
              this.categories = response.data
          }).catch(() => {
          
         console.warn("Nie udało się wczytać")
        });
      },
},

mounted:function(){
    this.getCategories()
  }
}


</script>

<style scoped>

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter-from /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}

</style>
