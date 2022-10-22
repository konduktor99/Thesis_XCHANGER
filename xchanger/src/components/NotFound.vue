<template>


<div id="not-found"> 

 <h1>Nie znaleziono zasobu w "{{this.$route.path}}" :( </h1>

</div>


</template>

<script>
export default {
  name: 'NotFound'
}
</script>
<style >
#not-found{

text-align: center;

margin: 10%;
}

#not-found h1{
    font-weight: bolder;
    color:rgb(147, 147, 186);
}
</style>


