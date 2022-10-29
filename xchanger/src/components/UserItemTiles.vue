<template>
<div>
  <transition name="fade">
    <router-view/>
  </transition>

<div class="main-main">
 
  <router-link id="add-article-tile" to="/my-profile/add-item">
  
    <i  class="fa fa-plus-square"></i>
 
  </router-link>

  <router-link v-for="item in items" :key="item" :to=" `/items/${item.id}`" style="text-decoration: none; color: inherit; ">
  <ItemTile v-bind="item"  v-bind:modify="true" />
  </router-link>

</div>
</div>


</template>

<script>

import ItemTile from './ItemTile.vue'

export default {
  name: 'ItemTiles',
  components: {
    ItemTile
  },
  data: () => {
    return{
      items: [
        {
          id: 1,
          user: "gipsu99",
          title: "Kask bell",
          desc: "Some quick example text to build on the Kask Bell and make up the bulk of the card's content."
        },
        {
          id: 2,
          user: "_konduktor_",
          title: "Kask",
          desc: "blablablabalbalabalblb ablblblbabl bbbbjfilenlcekcddcewdukjwrhurwkjh."
        },
        {
          id: 3,
          user: "ochrzan",
          title: "Nie Kask",
          desc: "XDDDDDDDDDDDDDDDDDDDD."
        }
      ]
    }
  }
}
</script>
<style>
.fade-enter-from,
.fade-leave-to{
  opacity: 0;
}

.fade-enter-to,
.fade-leave-from{
  opacity: 1;
}


.fade-enter-active{
  transition: all 1.5s ease;
}
.fade-leave-active{
  transition: all 0.5s ease;
 
}

</style>


