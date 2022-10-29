<template>

 <div class="article-tile">
    <img  src="../assets/images/kask3.png" />
    <h5 >{{title}}</h5>
    <p >{{desc}}</p>
    
    

    <div class="article-tile-buttons" v-if="modify">
        <router-link to="" class="btn btn-dark"><i class="fa fa-pencil"></i></router-link>
        <a href="#" class="btn btn-secondary" style="margin-left:5px;"><i class="fa fa-close" ></i></a>
    </div>
      

  </div>
</template>

<script>


export default {
  name: 'ItemTile',
   props: {
    title: String,
    desc: String,
    id:Number,
    modify: Boolean,
    
  
    
  }
}
</script>
