<template>

 <div class="article-tile">
    <div class="article-tile-img-wrapper" >
      <img v-if="!imgBytes"  src="../assets/images/no-image.jpg" />
      <img v-else  :src="`data:image/jpeg;base64,${imgBytes}`"/>
    </div>
    
    <h5 >{{title}}</h5>
    <p  v-if="!modify" ><b>{{location}}</b></p>
    <span >{{trimDesc(description, 120)}}</span>
    
    

    <div class="article-tile-buttons" v-if="modify">
        <router-link :to="{ path:  `/my-profile/edit-item/${id}`, query: {id:id,title:title,location:location,isNew:isNew,categoryId:categoryId,description:description}}" class="btn btn-dark"><i class="fa fa-pencil"></i></router-link>
        <router-link :to="this.$router.currentRoute" @click="deleteItem" href='' class="btn btn-secondary" style="margin-left:5px;"><i class="fa fa-close" ></i></router-link>
        
    </div>
      

  </div>
</template>

<script>

import axios from 'axios'


export default {
  name: 'ItemTile',
   props: {
    title: String,
    description: String,
    id:Number,
    location: String,
    user: String,
    isNew: Boolean,
    modify: Boolean,
    imgBytes: String,
    categoryId: Number,
    
  },


  methods:{

  trimDesc(desc, length){
  if(desc.length > length)
  return `${desc.substring(0,length)}...`

  return desc
},
deleteItem(){

if(!confirm('Czy na pewno chcesz usunąć ogłoszenie z tym przedmiotem?'))
          return
        this.$emit('deletedItem',this.id)
        axios.delete(`Items/DeleteItem/${this.id}`)
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
          this.error = `${error.response.status} ${mess} :(`
          console.log(this.error)
        });

}
}


}


</script>
