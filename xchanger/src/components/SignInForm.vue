<template>

 <div class="main-item-form">
     
      <form class="sign-log-in-form" name="sign-log-in-form" @submit.prevent="submit">
        <h2 class="form-header">Rejestracja</h2>

          <div class="form-group">
            <label for="login">Login</label>
            <input type="text" name="login" class="form-control" id="login" maxlength=20 placeholder="min. 7 znaków" v-model="login" @change="validateLogin" >
            <span class="inputAlert" id="login-alert">{{loginError}}</span> 
          </div>
          <div class="form-group">
            <label for="email">Email</label>
            <input type="email" id="email" class="form-control"  placeholder='adres z nazwą domeny np. "@gmail.com"' maxlength=30 v-model="email" @change="validateEmail" >
            <span class="inputAlert" id="email-alert">{{emailError}}</span> 
          </div>

          <div class="form-group">
            <label for="phone">Numer telefonu</label>
            <input type="tel" class="form-control" id="phone"   placeholder="format: np. 600500400" maxlength=30 v-model="phone" @change="validatePhone">
            <span class="inputAlert" id="phone-alert">{{phoneError}}</span> 
          </div>

          <div class="form-group">
            <label for="password">Hasło</label>
            <input type="password" class="form-control" id="password"  placeholder="min. 8 znaków w tym cyfra i znak specjalny" maxlength=30 v-model="password" @change="validatePassword">
            <span class="inputAlert" id="password-alert">{{passwordError}}</span> 
          </div>
          <div class="form-group">
            <label for="confirm-password">Potwierdź hasło</label>
            <input type="password" class="form-control" id="confirm-password" maxlength=30 v-model="passwordConf" @change="validateConfirmPass">
            <span class="inputAlert" id="confirm-pass-alert">{{passConfError}}</span> 
          </div>
        <br>

            <button type="submit" class="form-button-submit">Zarejestruj się  <i class="fa fa-user-plus"></i></button>
            <router-link :to="this.returnPrevious">
            <button type="button" class="form-button-cancel">Anuluj <i class="fa fa-times"></i></button>
            </router-link>
      

      </form>
    </div>
</template>

<script>

import axios from 'axios'
import jwt_decode from 'jwt-decode'

function checkDanger(value) {
    
    var dangerous = /.*(?=.*[/)(><{}'-]).*/;

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
    name: 'SignInForm',

    data()
    {
        return {
            error:[],
            login:null,
            loginError:null,
            email:null,
            emailError:null,
            password:null,
            passwordError:null,
            passwordConf:null,
            passConfError:null,
            phone:null,
            phoneError:null,
            returnPrevious: !this.$router.options.history.state.back || this.$router.options.history.state.back =='/login' ? '/items' : this.$router.options.history.state.back,
            occupiedLogin:null
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
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            if (checkDanger(this.login))
            {
                errorText = "Login zawiera niebezpieczne znaki."
                this.loginError = errorText
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

            if (this.login == this.occupiedLogin) {
                errorText = "Użytkownik o takim loginie już istnieje."
                this.loginError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            } 
            return true
        
        },
        validatePhone(){
            var errorText=""
            this.phoneError=""
            var regexPhone = /^[0-9]{9}$/;
            var input = document.getElementById("phone");
            input.classList.remove("error-input");
            input.classList.add("correct-input");
        

            if (!String(this.phone).match(regexPhone))
            {
                errorText = "Wprowadzono nieprawidłowy numer telefonu."
                this.phoneError = errorText
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
              if (!checkTextLengthRange(this.email,5,35)) {
                errorText = "Adres e-mail musi zawierać od 7 do 35 znaków."
                this.loginError = errorText
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
                this.passwordError = errorText
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
        validateConfirmPass() {
        
            var errorText=""
            this.passConfError=""
            var input = document.getElementById("confirm-password");
            input.classList.remove("error-input");
            input.classList.add("correct-input");

            if (!this.password) {
                errorText = "Podaj hasło."
                this.passConfError = errorText
                this.error.push(errorText)
                document.getElementById("password").classList.remove("correct-input");
                document.getElementById("password").classList.add("error-input");
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            if (!this.passwordConf) {
                errorText = "Potwierdź hasło."
                this.passConfError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            if (checkDanger(this.passwordConf))
            {
                errorText = "Hasło zawiera niebezpieczne znaki."
                this.passConfError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            } 
            
            if (this.passwordConf != this.password)
            {
                errorText = "Hasło niezgodne z podanym wyżej."
                this.passConfError = errorText
                this.error.push(errorText)
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            } 
            
            return true
        },
        submit() {
            const validLogin = this.validateLogin();
            const validEmail = this.validateEmail();
            const validPhone = this.validatePhone();
            const validPassword = this.validatePassword();
            const validConfirmPass = this.validateConfirmPass();

            if(validLogin && validEmail && validPassword && validConfirmPass && validPhone){

               
                axios.post(`Users/Register`,{
                    login: this.login,
                    email: this.email,
                    phoneNumber: this.phone,
                    password: this.password
                },
                {
                    withCredentials: true
                })
                .then((response)=>{

                    axios.defaults.headers.common['Authorization'] = `Bearer ${response.data}`
                    const decodedJwt = jwt_decode(response.data)
                    this.$emit('currUser',decodedJwt["name"])
                    this.$router.push(this.returnPrevious);
                }).catch(error => {
                let mess;
                switch (error.response.status) {
                    case 400:
                    mess = "Nieprawidłowe rządanie"
                    break;
                    case 404:
                    mess = error.response.data
                    break;
                    case 409:
                    this.loginError = error.response.data
                    var input = document.getElementById("login");
                    input.classList.add("error-input");
                    input.classList.remove("correct-input");
                    return;
                    case 500:
                    mess = error.response.data
                    break;
                    default:
                    mess = "Wystąpił błąd"
                }
                this.error = `${error.response.status} ${mess} :(`
            });


            }
               
        }
    },


 };

</script>
