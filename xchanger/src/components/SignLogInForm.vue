<template>

 <div class="main-item-form">
     
      <form class="sign-log-in-form" name="sign-log-in-form" onsubmit="return validateSignForm()">
        <h2 class="form-header"> {{ isRegister ? 'Rejestracja' : 'Logowanie'}}</h2>

         <a  :style="{visibility: !isRegister ? 'visible' : 'hidden'}" href="">Rejestracja <i class="fa fa-chevron-right" ></i></a>
          <div class="form-group">
            <label for="exampleFormControlInput1">Login</label>
            <input type="text" name="login" class="form-control" id="exampleFormControlInput1" :placeholder="isRegister ? 'min. 7 znaków' : ''" onchange="validateLogin()">
            <span class="inputAlert" id="login-alert"></span> 
          </div>
          <div v-if="isRegister" class="form-group">
            <label for="email">Email</label>
            <input type="email" id="email" class="form-control"  placeholder='adres z nazwą domeny np. "@gmail.com"' onchange="validateEmail()">
            <span class="inputAlert" id="email-alert"></span> 
          </div>

          <div class="form-group">
            <label for="password">Hasło</label>
            <input type="password" class="form-control" id="password"  :placeholder="isRegister ? 'min. 8 znaków w tym cyfra i znak specjalny' : ''" onchange="validatePassword()">
            <span class="inputAlert" id="password-alert"></span> 
          </div>
          <div v-if="isRegister" class="form-group">
            <label for="confirm-password">Potwierdź hasło</label>
            <input type="password" class="form-control" id="confirm-password" onchange="validateConfirmPass()">
            <span class="inputAlert" id="confirm-pass-alert"></span> 
          </div>
        <br>

            <button v-if="isRegister" type="submit" class="form-button-submit">Zarejestruj się  <i class="fa fa-user-plus"></i></button>
            <button v-else type="submit" class="form-button-submit">Zaloguj się  <i class="fa fa-sign-in"></i></button>
            <button type="button" class="form-button-cancel">Anuluj <i class="fa fa-times"></i></button>
      

      </form>     
    </div>
</template>

<script>


export default {
    name: 'SignLogInForm',
    props:  {
        register: Boolean
    },
    data: () =>{
        return{
            isRegister: false
        }
    }
}
</script>
