import { createApp } from 'vue'
import App from './App.vue'
import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/js/all.js";
//import "./assets/helperScripts/clientValidation/formValidation.js"
import Router from './index.js'
import  "vue-google-autocomplete";
import VueGoogleMaps from '@fawmi/vue-google-maps'
// import urls from 'urls.js'
import axios from 'axios'

 //axios.defaults.baseURL = 'https://192.168.1.14:44320/'
axios.defaults.baseURL = 'https://localhost:44320/'


// createApp(App).use(Router).mount('#app')
createApp(App)
  .use(VueGoogleMaps, {
    load: {
      key: "AIzaSyB2SBS-AKo7ajr2zpRPQrDXz-f8pBdW--M",
      libraries: "places"
    }
  })
  .use(Router)

  .mount("#app");