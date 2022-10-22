<template>

<div class ="main-item">

    <div class="slideshow" >
        <!-- <img  src="../assets/images/kask3.png" alt="Card image cap"/> -->
      <img name="slide"  style="width: 100%;" :src ="imagePath"/>
      <a class="btn-prev"  @click="changeImgBackwards">&#10094;</a>
      <a class="btn-next" @click="changeImgForward">&#10095;</a>
    </div>

    <div class="details-item" >
      <h3>Kask Bell</h3>
      <h3>{{id}}</h3>
      <h4>_konduktor_</h4>
      <h5>518557138</h5>
      <a href="">Więcej od tego ogłoszeniodawcy <i class="fa fa-chevron-right" ></i></a><br/><br/>
      <button type="button" class="btn btn-success btn-block">Zaproponuj wymianę <i class="fa fa-exchange" aria-hidden="true"></i></button>

    </div>

    <div class="description-item" >
      <h4>Opis</h4>
      <a >Kask Bell Super 3r. Odpinana szczęka. System ochrony MIPS, chroniący mózg przy uderzeniu. Kask mało używany, praktycznie jak nowy. Dla zdecydowanego kupującego możliwy dowóz na terenie Wałbrzycha i okolic. W razie pytań proszę dzwonić. Zapraszam.
        Platforma do wymiany wszelkiego rodzaju przedmiotów działająca zgodnie z postawą "anti-waste", sprzyjająca rozwojowi gospodarki o obiegu zamkniętym, gdzię jak najmniej artykułów codziennego użytku kończy przedwcześnie swój żywot na wysypisku lub zapomniane, na strychu. Dzięki naszemu portalowi sprawisz, że twoje rzeczy trafią do nowego właściciela, bez żadnych pośredników, a co za tym idzie bez generowania zbędnych kosztów dla Ciebie oraz planety, przy jednoczesnym zapewnieniu, że otrzymasz w zamian to czego potrzebujesz.
      </a>
    <!-- <img src="../assets/images/kask3.png" /> -->
    </div>
</div>
</template>

<script>


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
           images : [require("../assets/images/kask.png"),require("../assets/images/kask2.png"),require("../assets/images/kask3.png")],
           imagePath: null
      }
    },

  methods:{

      
    changeImgForward(){
        if(i < this.images.length - 1){
            i++; 
        } else { 
            i = 0;
        }
       
        this.imagePath = this.images[i];
    },
     changeImgBackwards(){
        if(i > 0){
            i--; 
        } else { 
            i = this.images.length-1;
        }
        console.log(i)
        this.imagePath =  this.images[i];
    }

  },

  beforeMount(){
    this.imagePath =  this.images[0];
 },


  
}


var i = 0; 			// Start Point
// var images = [];	// Images Array	 
// // Image List
// images[0] = "../assets/images/kask2.png";
// images[1] = "../assets/images/kask2.png";
// images[2] = "../assets/images/kask3.png";


// function setImg(){
//     console.log(i)
//     document.slide.src = this.images[0];
// }




// function readURL(input) {
//   if (input.files && input.files[0]) {
//     var reader = new FileReader();

//     reader.onload = function (e) {
//       $('#blah').attr('src', e.target.result).width(150).height(200);
//     };

//     reader.readAsDataURL(input.files[0]);
//   }
// }


//window.onload=setImg;
</script>


