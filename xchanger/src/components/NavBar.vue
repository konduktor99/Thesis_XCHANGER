<template>
 <nav>

<ul>
    <li><router-link to="/">Strona Główna </router-link> </li>
    <li><div class="dropdown">
       <a href="" >Kategorie </a>
        <div class="dropdown-content">
          <a href="#">Elektronika
          </a>
          <a href="#">Sport
          </a>
          <a href="#">Literatura
          </a>
        </div>
      </div></li>
    
</ul>

</nav>

</template>

<script>
export default {
  name: 'NavBar'
}
</script>


