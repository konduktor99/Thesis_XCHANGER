<template>
<div>
<div v-if="this.exchangesType===0">
  <h2>Wysłane propozycje wymiany</h2>
  <div v-if="(exchanges && !error)">
    <div class="list-group">
      <div v-for="exchange in exchanges" :key="exchange" class="list-group-item list-group-item-action flex-column align-items-start" style="z-index:0; padding:18px">
        <p class="reject-ex-btn" style="cursor:pointer" @click="rejectExchange(exchange.id)" style.>×</p>
        <small class="text-muted"><b>Wysłano {{formatDateTime(exchange.requestDate)}}</b></small>
        <router-link :to=" `/items/${exchange.item.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item.title}}</b></h5></router-link>
        <img v-if="!exchange.imgBytes1" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
        <img v-else class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes1}`"/>
        <div v-if="exchange.mess1" class="bubble1 bubble-bottom-left1" >{{exchange.mess1}}</div>
        <div class="exchange-resp-wrapper">
          <small v-if="exchange.state!==0" class="text-muted">Użytkownik <router-link :to="{ path: `/items`, query: {user: exchange.receiver.login}}" style="text-decoration: none; color: inherit;">{{exchange.receiver.login}}</router-link> odpowiedział {{formatDateTime(exchange.replyDate)}}</small>
          <small v-else class="text-muted">Użytkownik {{exchange.receiver.login}} jeszcze nie odpowiedział</small>
          <router-link :to=" `/items/${exchange.item2.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item2.title}}</b></h5></router-link>
          <img v-if="!exchange.imgBytes2 && exchange.state===1" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
          <img v-else-if="exchange.imgBytes2" class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes2}`"/>
          <div v-if="exchange.mess2" class="bubble2 bubble-bottom-left2" >{{exchange.mess2}}</div>
        </div>
        <button v-if="exchange.state===1" type="button" class="btn btn-outline-success btn-lg" @click="proceedConfirmation(exchange.id)">Zatwierdź wymianę <i class="fa fa-check" aria-hidden="true"></i></button>
        <div  v-if="exchange.state===3"  >
          <small class="text-muted">Czy na pewno chcesz zatwierdzić wymianę?</small><br>
              <button  class="btn btn-success" @click="confirm(exchange.id,exchange.receiver.login)"><i class="fa fa-check" ></i></button>
              <button  class="btn btn-secondary" @click="cancelConfirmation(exchange.id)" style="margin-left:5px;"><i class="fa fa-close" ></i></button>
        </div>
        <i  v-if="exchange.state===2" class="fa fa-exchange"  style="font-size:55px;"></i>
      </div>
    </div>
  </div>
</div>
<div v-else-if="this.exchangesType===1">
  <h2>Otrzymane propozycje wymiany </h2>
  <div v-if="(exchanges && !error)">
    <div class="list-group">
      <div v-for="exchange in exchanges" :key="exchange" class="list-group-item list-group-item-action flex-column align-items-start" style="z-index:0; padding:18px">
          <p class="reject-ex-btn" style="cursor:pointer" @click="rejectExchange(exchange.id)" style.>×</p>
          <div class="exchange-resp-wrapper">
            <small class="text-muted"><b>Otrzymano {{formatDateTime(exchange.requestDate)}}</b></small>
            <router-link :to=" `/items/${exchange.item.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item.title}}</b></h5></router-link>
            <img v-if="!exchange.imgBytes1" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
            <img v-else class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes1}`"/>
            <div v-if="exchange.mess1" class="bubble2 bubble-bottom-left2" >{{exchange.mess1}}</div>
          </div>
          <small v-if="exchange.state===1" class="text-muted"><b>Wysłano {{formatDateTime(exchange.replyDate)}}</b></small>
          <div v-if="exchange.state===0" class="article-tile-buttons" style="margin-top:20px;">
              <router-link :to="{ path: `/items`, query: {user: exchange.initiator.login, replyingExchange:exchange.id, exchangeItem:exchange.item.title}}"  class="btn btn-success" >Odpowiedz <i class="fa fa-reply" ></i></router-link>
          </div>
          <router-link :to=" `/items/${exchange.item2.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item2.title}}</b></h5></router-link>
          <img v-if="exchange.state===1 && !exchange.imgBytes2" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
          <img v-if="exchange.imgBytes2" class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes2}`"/>
          <div v-if="exchange.mess2" class="bubble1 bubble-bottom-left1" >{{exchange.mess2}}</div>
      </div>
    </div>
  </div>
</div>
<div v-else>
  <h2>Historia wymian</h2>
  <div v-if="(exchanges && !error)">
    <div class="list-group">
      <div v-for="exchange in exchanges" :key="exchange" class="list-group-item list-group-item-action flex-column align-items-start" style="z-index:0; padding:18px">
        <div v-if="exchange.initiator.login == this.loggedUser">
          <small class="text-muted"><b>Wysłano propozycję {{formatDateTime(exchange.requestDate)}}</b></small>
          <router-link :to=" `/items/${exchange.item.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item.title}}</b></h5></router-link>
          <img v-if="!exchange.imgBytes1" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
          <img v-else class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes1}`"/>
          <div class="bubble1 bubble-bottom-left1" >{{exchange.mess1}}</div>
          <div class="exchange-resp-wrapper">
            <small class="text-muted">Użytkownik <router-link :to="{ path: `/items`, query: {user: exchange.receiver.login}}" style="text-decoration: none; color: inherit;">{{exchange.receiver.login}}</router-link> odpowiedział {{formatDateTime(exchange.replyDate)}}</small>
            <router-link :to=" `/items/${exchange.item2.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item2.title}}</b></h5></router-link>
            <img v-if="!exchange.imgBytes2 && exchange.state===1" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
            <img v-else-if="exchange.imgBytes2" class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes2}`"/>
            <div class="bubble2 bubble-bottom-left2" >{{exchange.mess2}}</div>
          </div>
        </div>
        <div v-if="exchange.receiver.login == this.loggedUser">
          <div class="exchange-resp-wrapper">
            <small class="text-muted"><b>Otrzymano propozycję {{formatDateTime(exchange.requestDate)}}</b></small>
            <router-link :to=" `/items/${exchange.item.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item.title}}</b></h5></router-link>
            <img v-if="!exchange.imgBytes1" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
            <img v-else class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes1}`"/>
            <div v-if="exchange.mess1" class="bubble2 bubble-bottom-left2" >{{exchange.mess1}}</div>
          </div>
          <small class="text-muted"><b>Wysłano {{formatDateTime(exchange.replyDate)}}</b></small>
          <router-link :to=" `/items/${exchange.item2.id}`" style="text-decoration: none; color: inherit;"><h5 class="mb-1"><b>{{exchange.item2.title}}</b></h5></router-link>
          <img v-if="!exchange.imgBytes2" class="exchanges-list-img"  src="../assets/images/no-image.jpg" />
          <img v-else class="exchanges-list-img"  :src="`data:image/jpeg;base64,${exchange.imgBytes2}`"/>
          <div v-if="exchange.mess2" class="bubble1 bubble-bottom-left1" >{{exchange.mess2}}</div>
        </div>
        <small class="text-muted"><i class="fa fa-exchange"></i><b> Wymiana zatwierdzona {{formatDateTime(exchange.acceptDate)}}</b></small>
      </div>
    </div>
  </div>
</div>
<div v-if="!exchanges && !error" class="loader-wrapper"><div class="lds-facebook"><div></div><div></div><div></div></div></div>
<div v-if="error" id="error"> 
 <h1>{{error}}</h1>
</div>
</div>
</template>

<script>

import axios from 'axios'

export default {
  name: 'ExchangesList',
  props: {
    loggedUser: String,
    category: String,
    userAccess: Boolean,
    exchangesType: Number,
  },

  data: () => {
    return{
      exchanges: undefined,
      error: undefined,
    }
  },

  methods:{

    proceedConfirmation(exId){

      var index = this.exchanges.findIndex((e => e.id == exId));
      this.exchanges[index].state =3

    },

    confirm(exId,receiver){
      var index = this.exchanges.findIndex((e => e.id == exId));
      this.exchanges[index].state =2
      this.$toast.info(`Wymiana zatwierdzona! Skontaktuj się z użytkownikiem ${receiver} w celu finalizacji.`);
      setTimeout(this.$toast.clear, 15000)

      axios.put(`Exchanges/AcceptExchange/${exId}`)
          .then(()=>{ 
           this.requested = true
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
                mess = "Wystąpił błąd2"
            }
          this.error = `${mess} :(`
        });

      
      

    },
    cancelConfirmation(exId){
        var index = this.exchanges.findIndex((e => e.id == exId));
        this.exchanges[index].state =1
    },

    formatDateTime(dateTime){
      if(dateTime){
         const date = new Date(dateTime.split('T')[0]).toLocaleDateString('pl-PL')
         const time = dateTime.split('T')[1].substring(0,5)
         return `${date} ${time}`
      }
    },


      getRequestedExchanges(){
        axios.get(`Exchanges/Requested`)
        .then((response)=>{
          this.exchanges = response.data;
        }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe żądanie"
                break;
              case 401:
                mess = "Zaloguj się"
                break;
              case 404:
                mess = "brak"
                break;
              case 500:
                mess = "Wystąpił błąd wewnętrzny serwera"
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${mess} :(`
        });
      },

      getReceivedExchanges(){
        axios.get(`Exchanges/Received`)
        .then((response)=>{ 
          this.exchanges = response.data;
        }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe żądanie"
                break;
              case 401:
                mess = "Zaloguj się"
                break;
              case 404:
                mess = "brak"
                break;
              case 500:
                mess = "Wystąpił błąd wewnętrzny serwera"
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${mess} :(`
        });
      },
      
      getHistoricalExchanges(){
        axios.get(`Exchanges/History`)
        .then((response)=>{
          this.exchanges = response.data;
        }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe żądanie"
                break;
              case 401:
                mess = "Zaloguj się"
                break;
              case 404:
                mess = "brak"
                break;
              case 500:
                mess = "Wystąpił błąd wewnętrzny serwera"
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${mess} :(`
        });
      },
      rejectExchange(exchangeId){

        if(!confirm('Czy na pewno chcesz odrzucić wymianę?'))
          return

        var index = this.exchanges.findIndex((e => e.id == exchangeId));
        this.exchanges.splice(index,1)

        axios.delete(`Exchanges/DeleteExchange/${exchangeId}`)
          .then(()=>{ 
          }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe żądanie"
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
          console.log(this.error)
        });
    },
  },



   mounted:function(){
    if(this.exchangesType===0)
      this.getRequestedExchanges();
    else if(this.exchangesType===1)
      this.getReceivedExchanges();
    else
      this.getHistoricalExchanges();
        
  }
}
</script>



