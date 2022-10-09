<template>

<div class="main-main">
 
  <ItemTile v-for="item in items" :key="item" :title="item.title" :desc=item.desc />
 

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
          title: "Kask bell",
          desc: "Some quick example text to build on the Kask Bell and make up the bulk of the card's content."
        },
        {
          title: "Kask",
          desc: "blablablabalbalabalblb ablblblbabl bbbbjfilenlcekcddcewdukjwrhurwkjh."
        },
        {
          title: "Nie Kask",
          desc: "XDDDDDDDDDDDDDDDDDDDD."
        }
      ]
    }
  }
}
</script>


