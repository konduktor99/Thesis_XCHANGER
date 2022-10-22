<template>

 <div class="article-tile">
    <img  src="../assets/images/kask3.png" />
          <div class="article-tile-buttons">
      <button href="#" class="btn btn-dark  btn-block btn-space"><i class="fa fa-pencil"></i></button>
      <button href="#" class="btn btn-secondary " style="margin-left:5px;"><i class="fa fa-close"></i></button>
      </div>
      <h5 >{{title}}</h5>
      <p >{{desc}}</p>


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


<style scoped>

</style>