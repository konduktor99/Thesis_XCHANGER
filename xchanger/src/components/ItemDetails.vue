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
      <router-link :to="{ path: `/items`, query: {user:item.user.login}}" style="text-decoration: none; color: inherit;">Więcej od tego ogłoszeniodawcy <i class="fa fa-chevron-right" ></i></router-link><br/><br/>
      <button type="button" class="btn btn-success btn-block">Zaproponuj wymianę <i class="fa fa-exchange" aria-hidden="true"></i></button>

    </div>

    <div class="description-item" >
      <h4>Opis</h4>
      <a >{{item.description}}</a>

    <!-- <img src="../assets/images/kask3.png" /> -->
    </div>
</div>
<div v-else-if="!items && !error" class="loader-wrapper"><div class="lds-facebook"><div></div><div></div><div></div></div></div>
<div v-else id="error" style="text-align: center;"> 
 <h1 style="font-weight: bolder;color:rgb(147, 147, 186);">{{error}}</h1>
</div>
</template>

<script>

import axios from 'axios'

export default {
  name: 'ItemDetails',
   props: {
    title: String,
    desc: String,
    ownerNickname:String,
    ownerPhoneNum:String,
    
    
  },
    data(){
      return {

           
           id: this.$route.params.id, 
           imagesBytes : [],
           imageSrc: require("../assets/images/no-image.jpg"),
           item: undefined,
           error: undefined
      }
    },

  methods:{

    
    getItem(id){
      axios.get(`Items/${id}`)
      .then((response)=>{
        this.item = response.data;
        if(this.item.imgBytesList.length>0){
          this.imagesBytes = this.item.imgBytesList;
          this.imageSrc=`data:image/jpeg;base64,${this.imagesBytes[0]}`
        }
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
            mess = "Wystąpił błąd wewnętrzny serwera"
            break;
        }
          this.error = `${error.response.status} ${mess} :(`
       
        });
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
  }


  
}


var i = 0; 			// Start Point
// var images = [];	// Images Array	 
// // Image List
// images[0] = "../assets/images/kask2.png";
// images[1] = "../assets/images/kask2.png";
// images[2] = "../assets/images/kask3.png";


// function setImg(){
//     console.log(i)
//     document.slide.Bytes = this.images[0];
// }




// function readURL(input) {
//   if (input.files && input.files[0]) {
//     var reader = new FileReader();

//     reader.onload = function (e) {
//       $('#blah').attr('Bytes', e.target.result).width(150).height(200);
//     };

//     reader.readAsDataURL(input.files[0]);
//   }
// }


//window.onload=setImg;
</script>


