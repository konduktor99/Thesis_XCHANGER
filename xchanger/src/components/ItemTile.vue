<template>

 <div class="article-tile">
    <img  src="../assets/images/kask3.png" alt="Card image cap"/>
      <h5 >{{title}}</h5>
      <p >{{desc}}</p>
      <button href="#" class="btn btn-dark btn-lg btn-block btn-space"><i class="fa fa-pencil"></i></button>
      <button href="#" class="btn btn-secondary btn-lg" style="margin-left:5px;"><i class="fa fa-close"></i></button>
  </div>
</template>

<script>


export default {
  name: 'ItemTile',
   props: {
    title: String,
    desc: String,
    
  }
}
</script>


