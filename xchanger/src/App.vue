<template>
  <div id="app">
    <HeaderBar/>
    <NavBar register="false"/>
    <main>
      <router-view/>
        
    </main>
    <FooterBar/>
  </div>
</template>

<script>
import HeaderBar from './components/HeaderBar.vue'
import FooterBar from './components/FooterBar.vue'
import NavBar from './components/NavBar.vue'
//import ItemDetails from './components/ItemDetails.vue'

//import ItemTiles from './components/ItemTiles.vue'

export default {
  name: 'App',
  components: {
    HeaderBar,
    NavBar,
    FooterBar,
    //ItemDetails
//    ItemTiles
  },
   
};

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  
  color: #2c3e50;
  background: rgb(250, 250, 250);
}

@import './assets/style/style.css'
</style>
