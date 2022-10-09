<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    <HeaderBar/>
    <NavBar register="false"/>
    <main>
      <ItemTiles/>
      <SignLogInForm/>
    </main>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'
import HeaderBar from './components/HeaderBar.vue'
import NavBar from './components/NavBar.vue'
import ItemTiles from './components/ItemTiles.vue'
import SignLogInForm from './components/SignLogInForm.vue'

export default {
  name: 'App',
  components: {
    HelloWorld,
    HeaderBar,
    NavBar,
    ItemTiles,
    SignLogInForm
  }
}
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
