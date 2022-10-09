function checkTextLengthRange(value,min,max) {
    var trimmedVal = value.trim();

    if(min && trimmedVal.length<min)
        return false
     
    if(max && trimmedVal.length>max)
        return false
        

    return true
}

function checkNotEmpty(value) {
    if(!value)
        return false

    var trimmedVal = value.trim();
    if(!trimmedVal)
        return false

    return true
}

function checkDanger(value) {
    
    var dangerous = /.*(?=.*[\%\/\)\(\+\>\<\"\'\-]).*/;

    if(String(value).match(dangerous))
        return true

    return false
}

function validateName() {
    var input = document.forms["item-form"]["article-name"]
    var alert = document.getElementById('name-alert')

    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Podaj tytuł ogłoszenia."
        return false;
    }
    if (checkDanger(input.value))
    {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Tytuł zawiera nieebezpieczne znaki."
        return false;
    } 
    if (!checkTextLengthRange(input.value,5,30)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Tytuł powinien zawierać od 5 do 30 znaków."
        return false;
    }
    input.classList.remove("error-input");
    input.classList.add("correct-input");
    alert.innerText = ""
    return true;
    
  }
  function validateDesc() {
    var input = document.forms["item-form"]["description"]
    var alert = document.getElementById('desc-alert')

    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Podaj opis przedmiotu."
        return false;
    }
    if (checkDanger(input.value))
    {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Opis zawiera nieebezpieczne znaki."
        return false;
    } 
    if (!checkTextLengthRange(input.value,100,850)) {
            input.classList.remove("correct-input");
            input.classList.add("error-input");
            alert.innerText = "Opis powinien zawierać od 100 do 850 znaków."
          return false;
        }    
        input.classList.remove("error-input");
        input.classList.add("correct-input");
        alert.innerText = ""
        return true;
  }

  function validateLocation() {
    var input = document.forms["item-form"]["location"]
    var alert = document.getElementById('location-alert')

    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Podaj lokalizację."
        return false;
    }
    if (checkDanger(input.value))
    {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Lokalizacja zawiera nieebezpieczne znaki ."
        return false;
    } 
    input.classList.remove("error-input");
    input.classList.add("correct-input");
    alert.innerText = ""
    return true
  }

  function validateItemForm(){
    var validLocation = validateLocation()
    var validName = validateName()
    var validDesc = validateDesc()

      if(validDesc && validLocation && validName)
        return true
      
    alert("Formularz zawiera niepoprawne dane")
    return false
     
  }

  function validateLogin() {
    var input = document.forms["sign-log-in-form"]["login"]
    var alert = document.getElementById('login-alert')

    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Podaj login."
        return false;
    }
    if (checkDanger(input.value))
    {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Login zawiera nieebezpieczne znaki."
        return false;
    } 
    if (!checkTextLengthRange(input.value,7)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Login powinien zawierać min. 7 znaków."
        return false;
    } 

    input.classList.remove("error-input");
    input.classList.add("correct-input");
    alert.innerText = ""
    return true
  }

  function validateEmail() {
    var input = document.forms["sign-log-in-form"]["email"]
    var alert = document.getElementById('email-alert')
    var regexEmail = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Podaj Email."
        return false;
    }
    if (checkDanger(input.value))
    {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Adres zawiera nieebezpieczne znaki."
        return false;
    } 
    if (!String(input.value).toLowerCase().match(regexEmail))
     {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Nieprawidłowy adres email."
        return false;
    } 

    input.classList.remove("error-input");
    input.classList.add("correct-input");
    alert.innerText = ""
    return true
  }

  function validatePassword() {
    var input = document.forms["sign-log-in-form"]["password"]
    var alert = document.getElementById('password-alert')
    var regexPassword = /(?=.*[\\\!\@\#\$\^\*\\/)\(+\=\._-])(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}/;
    
    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Podaj hasło."
        return false;
    }
    if (checkDanger(input.value))
     {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Hasło zawiera nieebezpieczne znaki."
        return false;
    } 
    if (!String(input.value).match(regexPassword))
     {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Hasło powinno zawierać min. 8 znaków w tym małą, wielką literę i znak specjalny."
        return false;
    } 

    input.classList.remove("error-input");
    input.classList.add("correct-input");
    alert.innerText = ""
    return true
  }

  function validateConfirmPass() {
    var input = document.forms["sign-log-in-form"]["confirm-password"]
    var alert = document.getElementById('confirm-pass-alert')
    var password = document.forms["sign-log-in-form"]["password"].value

    if (!checkNotEmpty(password)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Wpisz najpierw hasło powyżej."
        return false
    }
    if (!checkNotEmpty(input.value)) {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Potwierdź hasło"
        return false
    }
    if (checkDanger(input.value))
     {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Hasło zawiera nieebezpieczne znaki."
        return false;
    }
    if (input.value != password)
     {
        input.classList.remove("correct-input");
        input.classList.add("error-input");
        alert.innerText = "Hasło niezgodne z powyższym."
        return false;
    } 

    input.classList.remove("error-input");
    input.classList.add("correct-input");
    alert.innerText = ""
    return true
  }

  function validateSignForm(){
    var validLogin = validateLogin()
    var validEmail = validateEmail()
    var validPassword = validatePassword()
    var validPasswordConfirmation = validateConfirmPass()

      if(validLogin && validEmail && validPassword && validPasswordConfirmation)
        return true
      
    alert("Formularz zawiera niepoprawne dane")
    return false
     
  }
  function validateLogInForm(){
    var validLogin = validateLogin()
    //var validEmail = validateEmail()
    var validPassword = validatePassword()

      if(validLogin && validPassword )
        return true
      
    alert("Formularz zawiera niepoprawne dane")
    return false
     
  }

  