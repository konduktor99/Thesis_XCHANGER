<template>

<div class ="main-item" v-if="item && !error">

    <div class="slideshow" >
        <!-- <img  src="../assets/images/kask3.png" alt="Card image cap"/> -->
      <img name="slide"  style="width: 100%;" :src ="imageSrc"/>
      <a v-if="imagesBytes.length>1" class="btn-prev"  @click="changeImgBackwards">&#10094;</a>
      <a v-if="imagesBytes.length>1" class="btn-next" @click="changeImgForward">&#10095;</a>
    </div>

    <div class="details-item-user" >
      <h3>{{item.title}}</h3>
      <h4>{{item.user.login}}</h4>
      <h5>{{item.location}}</h5>
      <h5>{{item.user.phoneNumber}}</h5>
      <b v-if="item.isNew" class="new-label">NOWE</b><b v-else class="used-label">UŻYWANE</b><br>
      <!-- <b style="color:grey;">{{item.category.name}} </b><br> -->
      <router-link :to="{ path: `/items`, query: {user:item.user.login}}" style="text-decoration: none; color: inherit;"><b>Więcej od tego ogłoszeniodawcy</b> <i class="fa fa-chevron-right" ></i></router-link><br/><br/>
      <div v-if="loggedUser">
      <div v-if="loggedUser!=item.user.login">
      <button v-if="replyingExchange && !proceedExReq" type="button" @click="proceedExRequest" class="btn btn-success btn-block"> Wymień za {{this.exchangeItem}} <i class="fa fa-exchange" aria-hidden="true"></i></button>
      <button v-else-if="!proceedExReq && !requested" type="button" @click="proceedExRequest" class="btn btn-success btn-block">Zaproponuj wymianę <i class="fa fa-exchange" aria-hidden="true"></i></button>
      <div v-else-if="!requested">
        <textarea class="form-control" v-model="requestMessage" id="desc" rows="2" maxlength="90" name="description" placeholder="Wiadomość do ogłoszeniodawcy" ></textarea>
            <div class="article-tile-buttons" >
              <button  class="btn btn-success" @click="exchange"><i class="fa fa-paper-plane" ></i></button>
              <button  class="btn btn-secondary" @click="cancelExRequest" style="margin-left:5px;"><i class="fa fa-close" ></i></button>
            </div>
      </div>
      <b v-if="requested" class="already-requested-exchange">Zaproponowano wymianę <i class="fa fa-check-circle" ></i></b>
      </div>
      </div>
      <router-link v-else to="/login" class="btn btn-success btn-block">Zaloguj się, by zaproponować wymianę.</router-link>
      
    </div>

    <div class="description-item" >
      <h4>Opis</h4>
      <a >{{item.description}}</a>

    <!-- <img src="../assets/images/kask3.png" /> -->
    </div>
</div>
<div v-else-if="!item && !error" class="loader-wrapper"><div class="lds-facebook"><div></div><div></div><div></div></div></div>
<div v-else id="error" style="text-align: center;"> 
 <h1 style="font-weight: bolder;color:rgb(147, 147, 186);">{{error}}</h1>
</div>
</template>

<script>

import axios from 'axios'
import jwt_decode from 'jwt-decode';

export default {
  name: 'ItemDetails',
   props: {
    title: String,
    desc: String,
    ownerNickname:String,
    ownerPhoneNum:String,
    loggedUser: String,
    replyingExchange: Number,
    exchangeItem: String,
    
    
  },
    data(){
      return {

           
           id: this.$route.params.id, 
           imagesBytes : [],
           imageSrc: require("../assets/images/no-image.jpg"),
           item: undefined,
           error: undefined,
           proceedExReq: false,
           requestMessage: undefined,
           requested: false,
           
      }
    },
  methods:{

    proceedExRequest(){ console.log("ZLEe")
      this.proceedExReq = true
    },
    cancelExRequest(){
      this.proceedExReq = false
    },
    
    getItem(id){
      axios.get(`Items/${id}`)
      .then((response)=>{
        this.item = response.data;
        if(this.item.imgBytesList.length>0){
          this.imagesBytes = this.item.imgBytesList;
          this.imageSrc=`data:image/jpeg;base64,${this.imagesBytes[0]}`

          if(response.config.headers['Authorization'])
          {
              const decodedJwt = jwt_decode(response.config.headers['Authorization'].substring(7))
              this.$emit('currUser',decodedJwt["name"])
          }
          
          
        }
      }).catch(error => {
       
       let mess;
          if(!error.response){
            this.error = 'Wystąpił błąd'
          }else{
            switch (error.response.status) {
            case 400:
              mess = "Nieprawidłowe rządanie"
              break;
            case 500:
              mess = "Wystąpił błąd wewnętrzny serwera"
              break;
            default:
              mess = error.response.data
          }
            this.error = `${error.response.status} ${mess} :(`    
          }
       });
       
    },

    exchange(){
      if(this.replyingExchange){
         axios.put(`Exchanges/ReplyExchangeRequest/${this.replyingExchange}`, {
          item2Id: this.item.id,
          mess2: this.requestMessage
        })
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
          this.error = `${error.response.status} ${mess} :(`
          console.log(this.error)
        });
      }else{
         axios.post(`Exchanges/RequestExchange`, {
          itemId: this.item.id,
          mess1: this.requestMessage
        })
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
          this.error = `${error.response.status} ${mess} :(`
          console.log(this.error)
        });
      }
       
    },

    isRequested(id){
        axios.get(`Exchanges/IsRequested/${id}`)
          .then( response=>{ 
           this.requested = response.data
          }).catch(error => {console.log(error)});
    },
      
    changeImgForward(){
        if(i < this.imagesBytes.length - 1){
            i++; 
        } else { 
            i = 0;
        }
       
        this.imageSrc = `data:image/jpeg;base64,${this.imagesBytes[i]}`;
    },
     changeImgBackwards(){
        if(i > 0){
            i--; 
        } else { 
            i = this.imagesBytes.length-1;
        }
        this.imageSrc =  `data:image/jpeg;base64,${this.imagesBytes[i]}`;
    }

  },

  beforeMount(){
   // this.imageBytes =  this.imagesBytes[0];
 },
  mounted:function(){
      this.getItem(this.id);
      if(this.loggedUser)
        this.isRequested(this.id)
  }


  
}
var i = 0; 

</script>


