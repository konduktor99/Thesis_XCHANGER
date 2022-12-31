<template>
  <div id="app" >
    <HeaderBar :loggedIn="this.$cookies.get('signed')" :loggedUser="loggedUser" @currUser="getCurrUsr($event)"/>
    <NavBar register="false"/>
    <main>
      <div v-if="loggedUser">
      <button type="button" class="button-requests" @click="openSideBar"><i class="fa fa-exchange" aria-hidden="true"></i></button>
      <div id="side-bar" class="sidebar">
        <i class="closebtn" style="cursor:pointer" @click="closeSideBar">×</i>
        <h3 ><i class="fa fa-exchange"></i> Wymiany</h3>
        <router-link to="/exchanges/received" @click="closeSideBar"><i class="fa fa-envelope"></i> Otrzymane propozycje</router-link>
        <router-link to="/exchanges/requested" @click="closeSideBar"><i class="fa fa-paper-plane"></i> Wysłane propozycje</router-link>
        <router-link to="/exchanges/history" @click="closeSideBar"><i class="fa fa-history"></i> Historia</router-link>
      </div>
      </div>
      <router-view :loggedUser="loggedUser" :key="$route.fullPath" @currUser="getCurrUsr($event)"/>
    </main>
    <FooterBar/>
  </div>
</template>

<script>
import HeaderBar from './components/HeaderBar.vue'
import FooterBar from './components/FooterBar.vue'
import NavBar from './components/NavBar.vue'
import axios from 'axios'
import jwt_decode from 'jwt-decode'

export default {
  name: 'App',
  components: {
    HeaderBar,
    NavBar,
    FooterBar,
  },
  data: () => {
    return{
     loggedUser: undefined
    }
  },
  methods:{

      openSideBar() {
        document.getElementById("side-bar").style.width = "330px";   
      },

      closeSideBar() {
        document.getElementById("side-bar").style.width = "0";
      },
    getCurrUsr(data){
      this.loggedUser = data
    },
     getCurrUser(){
      
        
         axios.post('Users/refresh-token',{},
                {
                    withCredentials: true
                }
                )
                .then((response)=>{
                  if (response.status === 200) {
                    axios.defaults.headers.common['Authorization'] = `Bearer ${response.data}`;
                    const decodedJwt = jwt_decode(response.data)
                    this.loggedUser = decodedJwt["name"]
                  }
                }).catch( ()=> {}); 
      }
  },

  mounted:function(){
     
        this.getCurrUser()  
  }
};


 


</script>



<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  /* font-family: Optima,Segoe,Segoe UI,Candara,Calibri,Arial,sans-serif; */
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  
  
  color: #2c3e50;
  background: rgb(250, 250, 250);
   
}

@import './assets/style/style.css'
</style>
