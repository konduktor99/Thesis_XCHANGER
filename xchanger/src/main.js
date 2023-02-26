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
import VueCookies from 'vue-cookies';
import Toaster from '@meforma/vue-toaster';

import 'bootstrap-vue/dist/bootstrap-vue.css'

axios.defaults.baseURL = '/api' //'https://localhost:5001/'

// createApp(App).use(Router).mount('#app')
createApp(App)
  .use(VueGoogleMaps, {
    load: {
      key: "AIzaSyA5KpvarAKHNklCd6YM_X-WowziB4ROVBE",
      libraries: "places"
    }
  })
  .use(Router)
  .use(VueCookies)
  .use(Toaster)
  .mount("#app");


 axios.interceptors.response.use(resp => resp, async error => {


    if (error.response.status === 401 && !error.config.headers['was-refreshed'] && error.response.data!="Nie można ingerować w ogłoszenie innego użytkownika" && error.response.data!="Nie można ingerować w nieswoją transakcję wymiany") {
        const {status, data} = await axios.post('Users/refresh-token',{},{withCredentials:true, headers:{'was-refreshed':true}} );
    
        if (status === 200) {
            
            error.config.headers['Authorization'] = `Bearer ${data}`
            axios.defaults.headers.common['Authorization'] = `Bearer ${data}`;
           
            return axios(error.config);
        }
    }

    return Promise.reject(error);
});


