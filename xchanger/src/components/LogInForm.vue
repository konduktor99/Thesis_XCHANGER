<template>

 <div class="main-item-form">
     
      <form class="sign-log-in-form" name="sign-log-in-form" @submit="validateForm">
        <h2 class="form-header"> Logowanie</h2>

         <router-link  to="/signin">Rejestracja <i class="fa fa-chevron-right" ></i></router-link>
          <div class="form-group">
            <label for="login">Login</label>
            <input type="text" name="login" class="form-control" id="login" maxlength=20  v-model="login" @change="validateLogin">
            <span class="inputAlert" id="login-alert">{{loginError}}</span> 
          </div>
          <div class="form-group">
            <label for="password">Hasło</label>
            <input type="password" class="form-control" id="password" maxlength=30 v-model="password" @change="validatePassword">
            <span class="inputAlert" id="password-alert">{{emailError}}</span> 
          </div>
        <br>

            <button type="submit" class="form-button-submit">Zaloguj się  <i class="fa fa-sign-in"></i></button>
            <router-link to="/">
              <button type="button" class="form-button-cancel">Anuluj <i class="fa fa-times"></i></button>
            </router-link>
      

      </form>     
    </div>
</template>

<script>
function checkDanger(value) {
    
    var dangerous = /.*(?=.*[%/)(+><"'-]).*/;

    if(String(value).match(dangerous))
        return true
    return false
}
function checkTextLengthRange(value,min,max) {
    
    var trimmedVal = value.trim();

    if(min && trimmedVal.length<min)
        return false
     
    if(max && trimmedVal.length>max)
        return false
        
    return true
}

export default {
    name: 'LogInForm',

    data()
    {
        return {
            error:[],
            login:null,
            loginError:null,
            // email:null,
            // emailError:null,
            password:null,
            passwordError:null,
        }
    },

     methods: {
        validateLogin(){
        var errorText=""
        this.loginError=""
        var input = document.getElementById("login");
        input.classList.remove("error-input");
        input.classList.add("correct-input");
       
        if (!this.login) {
            errorText = "Podaj login."
            this.loginError = errorText
            this.error.push(errorText)
            input.classList.remove("correct-input");
            input.classList.add("error-input");
            return false
        }
        if (checkDanger(this.login))
        {
            errorText = "Login zawiera niebezpieczne znaki."
            this.loginError = errorText
            this.error.push(errorText)
            input.classList.remove("correct-input");
            input.classList.add("error-input");
            return false
        }   
        if (!checkTextLengthRange(this.login,7,20)) {
            errorText = "Login musi zawierać od 7 do 20 znaków."
            this.loginError = errorText
            this.error.push(errorText)
            input.classList.remove("correct-input");
            input.classList.add("error-input");
            return false
        } 
        return true
        
        },
        validateEmail() {
        var errorText=""
        this.emailError=""
        var regexEmail = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/;
        var input = document.getElementById("email");
        input.classList.remove("error-input");
        input.classList.add("correct-input");

        if (!this.email) {
            errorText = "Podaj adres email."
            this.emailError = errorText
            this.error.push(errorText)
            input.classList.remove("correct-input");
            input.classList.add("error-input");
            return false
        }
        if (checkDanger(this.email))
        {
            errorText = "Adres email zawiera niebezpieczne znaki."
            this.emailError = errorText
            this.error.push(errorText)
            return false
        } 
        if (!String(this.email).toLowerCase().match(regexEmail))
        {
            errorText = "Wprowadzono nieprawidłowy adres e-mail."
            this.emailError = errorText
            this.error.push(errorText)
            input.classList.remove("correct-input");
            input.classList.add("error-input");
            return false
        } 
        

        return true
        },
         validatePassword() {
            var errorText=""
            this.passwordError=""
            var regexPassword = /(?=.*[\\!@#$^*\\/)(+=._-])(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}/;
            var input = document.getElementById("password");
            input.classList.remove("error-input");
            input.classList.add("correct-input");

            if (!this.password) {
                errorText = "Podaj hasło."
                this.emailError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            if (checkDanger(this.password))
            {
                errorText = "Hasło zawiera niebezpieczne znaki."
                this.passwordError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            } 
            if (!String(this.password).match(regexPassword))
            {
                errorText = "Hasło powinno zawierać od. 8 do 30 znaków w tym małą, wielką literę i znak specjalny."
                this.passwordError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            } 
          
            
            return true
        },
        validateForm(e) {
            const validLogin = this.validateLogin();
            //const validEmail = this.validateEmail();
            const validPassword = this.validatePassword();
            if(!(validLogin && validPassword ))
                e.preventDefault()
        }
    },


 };
</script>
