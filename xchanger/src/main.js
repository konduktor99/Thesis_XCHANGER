import { createApp } from 'vue'
import App from './App.vue'
import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/js/all.js";
//import "./assets/helperScripts/clientValidation/formValidation.js"
import Router from './index.js'


createApp(App).use(Router).mount('#app')
