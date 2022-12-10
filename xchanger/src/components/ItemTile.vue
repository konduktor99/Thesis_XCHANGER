<template>

 <div class="article-tile">
    <img  src="../assets/images/kask3.png" />
    <h5 >{{title}}</h5>
    <p v-if="!modify" >{{location}}</p>
    <span >{{trimDesc(description, 90)}}</span>
    
    

    <div class="article-tile-buttons" v-if="modify">
        <router-link :to="{ path:  `/my-profile/edit-item/${id}`, query: {id:id,title:title,location:location,isNew:isNew,description:description}}" class="btn btn-dark"><i class="fa fa-pencil"></i></router-link>
        <a href="#" class="btn btn-secondary" style="margin-left:5px;"><i class="fa fa-close" ></i></a>
    </div>
      

  </div>
</template>

<script>




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
    
  },

  methods:{

  trimDesc(desc, length){
  if(desc.length > length)
  return `${desc.substring(0,length)}...`

  return desc
}
}


}


</script>
