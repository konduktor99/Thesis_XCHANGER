<template>

<div class="main-item-form">

      <form class="item-form" name="item-form" @submit="validateForm">

        <h2 class="form-header"> Dodaj ogłoszenie</h2>

        <div class="form-group">
          <label for="upload-pics">Zdjęcia</label>
          <div class="upload-pics-box">
            <input type="file" id="upload-pics" multiple accept="image/png, image/jpeg" data-buttonText="Dodaj" @change="onPicsSelected"> 
          </div>
          <span class="inputAlert" visible="false"></span> 

          <output id="result-pics" />
        </div>
          <div class="form-group">
            <label for="title">Tytuł</label>
            <input type="text" class="form-control" id="title" name="title" v-model="title" @change="validateTitle"  placeholder="np. rower">
            <span class="inputAlert" id="title-alert" >{{titleError}}</span> 
          </div>

          <div class="form-group">
           
            <!-- <vue-google-autocomplete id="map" classname="form-control" placeholder="Start typing" v-on:placechanged="getAddressData"> -->
            <!-- @place_changed="setPlace" -->
            <!-- </vue-google-autocomplete> -->
              <label for="address">Lokalizacja</label>
             <GMapAutocomplete class="form-control" id="autocomplete-loc" placeholder="Wybierz lokalizację przedmiotu" :options="autocompleteOptions" @change="locationChanged"  @place_changed="validateLocation"  >           
                    
            </GMapAutocomplete>
          <span class="inputAlert" id="location-alert">{{locationError}}</span> 
          </div>
          <div class="form-group">
            <label for="desc">Opis</label>
            <textarea class="form-control" id="desc" rows="5"  name="description" v-model="description" @change="validateDesc" ></textarea>
            <span class="inputAlert" id="desc-alert">{{descriptionError}}</span> 
          </div>
        
          <div class="form-group">
            <div class="form-check">
              <input class="form-check-input" type="radio" name="stan" id="uzywanyRadio" value="używany" checked>
              <label  for="uzywanyRadio">
                Używany
              </label>
            </div>
            <div class="form-check">
              <input class="form-check-input" type="radio" name="stan" id="nowyRadio" value="nowy">
              <label  for="nowyRadio">
                Nowy
              </label>
            </div>
            
          </div>

          <button type="submit" class="form-button-submit">Dodaj  <i class="fa fa-plus"></i></button>
          <button type="button" class="form-button-cancel">Anuluj <i class="fa fa-times"></i></button>
      
      </form>
      
    </div>

</template>

<script>

var i = 1
i = false
console.log(i)
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
    name: 'ItemForm',


    
  

    data()
    {
        return {

            item: this.$route.query,
            
            title:null,
            titleError:null,
            // email:null,
            // emailError:null,
            location:null,
            locationError:null,
            placeSelected:false,
            description:null,
            descriptionError:null,
            autocompleteOptions : {
                types: ['(cities)'],
                componentRestrictions: {country: "pl"}
              },
            tooManyPics:false
        }
    },



     methods: {

       onPicsSelected(e){

         if (window.File && window.FileReader && window.FileList && window.Blob) { 
              const files = e.target.files; 
            if(files.length>6){
              alert("Można wybrać maksymalnie 6 zdjęć");
              this.tooManyPics=true;
              return
            }
              const output = document.querySelector("#result-pics");
              output.innerHTML="";
              for (let i = 0; i < files.length; i++) { 
                  if (!files[i].type.match("image")) continue; 
                  const picReader = new FileReader(); 
                  picReader.addEventListener("load", function (event) { 
                    const picFile = event.target;
                    const img = document.createElement("img");
                    img.classList.add("uploaded-pic");
                    img.src = picFile.result;
                    //const img = document.createElement("div");
                    //div.innerHTML = `<img class="uploaded-pic" src="${picFile.result}" title="${picFile.name}"/>`;
                    output.appendChild(img);
                  });
                  picReader.readAsDataURL(files[i]); 
              }
            } else {
              alert("Przeglądarka nie wspiera File API");
            }

       },
      

        // validateForm(e) {
        //     const validLogin = this.validateLogin();
        //     //const validEmail = this.validateEmail();
        //     const validPassword = this.validatePassword();
        //     if(!(validLogin && validPassword ))
        //         e.preventDefault()
        // }


         validateTitle() {
            var errorText=""
            this.titleError=""
            var input = document.getElementById("title");
            input.classList.remove("error-input");
            input.classList.add("correct-input");
            
        
            if (!this.title) {
                errorText = "Podaj tytuł ogłoszenia."
                this.titleError = errorText
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            if (checkDanger(this.title))
            {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Tytuł zawiera niebezpieczne znaki."
                this.titleError = errorText
                return false;
            } 
            if (!checkTextLengthRange(this.title,5,30)) {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Tytuł powinien zawierać od 5 do 30 znaków."
                this.titleError = errorText
                return false;
            }
            
            return true;
            
        },
        validateDesc() {
            var errorText=""
            this.descriptionError=""
            var input = document.getElementById("desc");
            input.classList.remove("error-input");
            input.classList.add("correct-input");

            if (!this.description){
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Podaj opis ogłoszenia."
                this.descriptionError = errorText
                return false;
            }
            if (checkDanger(this.description))
            {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Opis zwiera niebezpieczne znaki."
                this.descriptionError = errorText
                return false;
            } 
            if (!checkTextLengthRange(this.description,100,850)) {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Opis powinien zawierać od 100 do 850 znaków."
                this.descriptionError = errorText
                return false;
                }    
                
                return true;
        },
            locationChanged(){
                this.placeSelected= false
                var input = document.getElementById("autocomplete-loc");
                var errorText=""
                this.locationError=""
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Wybierz lokalizacje z listy."
                this.locationError = errorText
                return false;
            },
            

            validateLocation() {
              
              this.location = document.getElementById("autocomplete-loc").value
              console.log(this.location);
              this.placeSelected = true;
              
              this.locationError=""
              var input = document.getElementById("autocomplete-loc");
              input.classList.remove("error-input");
              input.classList.add("correct-input");
        
           
              // if (checkDanger(this.location))
              // {
              //     input.classList.remove("correct-input");
              //     input.classList.add("error-input");
              //     errorText = "Lokalizacja zawiera niebezpieczne znaki."
              //     this.locationError = errorText
              //     this.placeSelected = false;
              // } 
    
            
        },
       
          validateForm(e) {
            const validTitle = this.validateTitle();
            const validDesc = this.validateDesc();
           
           if(!this.placeSelected)
              this.locationError = "Wybierz lokalizacje z listy."
            

            if(!(validTitle && validDesc && this.placeSelected))
                e.preventDefault()
        }
    },


 };
</script>
