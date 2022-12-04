<template>

 <div class="article-tile">
    <img  src="../assets/images/kask3.png" />
    <h5 >{{title}}</h5>
    <p >{{location}}</p>
    <p >{{trimDesc(description, 90)}}</p>
    
    

    <div class="article-tile-buttons" v-if="modify">
        <router-link :to="{ path:  `/my-profile/edit-item/${id}`, query: {id:id,title:title,location:location,desc:desc}}" class="btn btn-dark"><i class="fa fa-pencil"></i></router-link>
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
    new: Boolean,
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
