<template>
<div>
<h1 v-if="error">{{error}}</h1>
<div v-else class="main-item-form">

      <form class="item-form" name="item-form" @submit.prevent="submit">

        <h2 v-if="create" class="form-header"> Dodaj ogłoszenie</h2>
        <h2 v-else class="form-header"> Edytuj ogłoszenie</h2>

        <div  class="form-group">
          <label for="upload-pics">Zdjęcia</label>
          <div class="upload-pics-box">
            <input type="file" id="upload-pics" multiple accept="image/png, image/jpeg" data-buttonText="Dodaj" @change="onImgsSelected"> 
          </div>
          <b v-if="rozmiarMB" :class="sizeSum<30000000? 'good-size-label':'exceeded-size-label'" >{{`${rozmiarMB} / 28 MB`}}</b> 

          <output id="result-pics" />
        </div>
          <div class="form-group">
            <label for="title">Tytuł</label>
            <input type="text" class="form-control" id="title" name="title" v-model="item.title" @change="validateTitle" placeholder="np. rower">
            <span class="inputAlert" id="title-alert" >{{titleError}}</span> 
          </div>

          <div class="form-group">
    
              <label for="address">Lokalizacja</label>
             <GMapAutocomplete class="form-control" id="autocomplete-loc" placeholder="Wybierz lokalizację przedmiotu" :options="autocompleteOptions" :value="item.location"   @change="locationChanged"  @place_changed="validateLocation"  >  
            </GMapAutocomplete>
          <span class="inputAlert" id="location-alert">{{locationError}}</span> 
          </div>
          <div class="form-group">
             <label for="category">Kategoria</label>
            <select id="category" class="form-control" v-model="item.categoryId" @change="validateCategory">
              <option value="" disabled >- Wybierz kategorię -</option>
              <option v-for="category in categories" :key="category" :value="category.id">{{category.name}}</option>
            </select>
             <span class="inputAlert" id="cat-alert">{{categoryError}}</span> 
          </div>
          <div class="form-group">
            <label for="desc">Opis</label>
            <textarea class="form-control" id="desc" rows="5"  name="description" v-model="item.description" @change="validateDesc" ></textarea>
            <span class="inputAlert" id="desc-alert">{{descriptionError}}</span> 
          </div>
        
          <div class="form-group">
            <div class="form-check">
              <input v-if="create" class="form-check-input" type="radio" name="stan" id="newRadio" :value="true" v-model="item.isNew" checked >
              <input v-else class="form-check-input" type="radio" name="stan" id="newRadio" :value="true" v-model="item.isNew" :checked="item.isNew === 'true'" >
              <label  for="newRadio">
                Nowy
              </label>
            </div>
            <div class="form-check">
              <input class="form-check-input" type="radio" name="stan" id="usedRadio" :value="false"  v-model="item.isNew" :checked="item.isNew === 'false'"  >
              <label  for="usedRadio">
                Używany
              </label>
            </div>
            
          </div>

          <button v-if="create" type="submit" class="form-button-submit">Dodaj  <i class="fa fa-plus"></i></button>
          <button v-else type="submit" class="form-button-submit">Edytuj  <i class="fa fa-plus"></i></button>
          <router-link :to="returnPrevious">
            <button type="button" class="form-button-cancel">Anuluj <i class="fa fa-times"></i></button>
          </router-link>
         
      
      </form>
      
    </div>
</div>
</template>

<script>
import axios from 'axios'

function checkDanger(value) {
    
    var dangerous = /.*(?=.*[/><{}'-]).*/;

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
    name: 'EditItemForm',
    props: {
    titleProp: String,
    desc: String,
    id:Number,
    user: String,
    isNew: Boolean,
    create: Boolean,
    
}   ,

  

    data()
    {
        return {
           // id: this.$route.params.id, 
            item: this.$route.query,
            error:null,
            titleError:null,
            filesError:null,
            // email:null,
            // emailError:null,
            categoryError:null,
            location:null,
            locationError:null,
            placeSelected:true,
            description:null,
            descriptionError:null,
            autocompleteOptions : {
                types: ['(cities)'],
                componentRestrictions: {country: "pl"}
              },
            tooBigPics:false,
            files: [],
            returnPrevious: !this.$router.options.history.state.back ? '/items' : this.$router.options.history.state.back,
            rozmiarMB: null,
            sizeSum:null,
            categories:[]

           
        }
    },




     methods: {


      submit(){
      this.item.userId = 6;
      //this.item.categoryId =1;
      if(this.validateForm()){

        var formData = new FormData();
        for( var key in this.item)
          formData.append(key,this.item[key]);

       Array.prototype.forEach.call(this.files,file=>{
        formData.append("files", file);
        });

         if(this.create){

          axios.post(`Items/CreateItem`, formData)
          .then(()=>{ 
              this.$toast.info(`Dodano ogłoszenie.`);
              setTimeout(this.$toast.clear, 5000)
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
              case 500:
                mess = error.response.data
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${error.response.status} ${mess} :(`
        });
        }else{
          
          axios.put(`Items/${this.item.id}`,formData)
          .then(()=>{
            this.$toast.info(`Zaktualizowano ogłoszenie.`);
            setTimeout(this.$toast.clear, 5000)
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

      retrieveImages(){
         axios.get(`Items/images/${this.item.id}`)
          .then((response)=>{
           this.files = response.data
           this.showImages()
          }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe rządanie"
                break;
              case 404:
                mess = error.response.data
                break;
              case 500:
                mess = error.response.data
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${error.response.status} ${mess} :(`
        });
      },

      getCategories(){
         axios.get('Categories')
          .then((response)=>{
           this.categories = response.data
           this.showImages()
          }).catch(error => {
          let mess;
          switch (error.response.status) {
              case 400:
                mess = "Nieprawidłowe rządanie"
                break;
              case 404:
                mess = error.response.data
                break;
              case 500:
                mess = error.response.data
                break;
              default:
                mess = "Wystąpił błąd"
            }
          this.error = `${error.response.status} ${mess} :(`
        });
      },

       onImgsSelected(e){
         if (window.File && window.FileReader && window.FileList && window.Blob) { 
               this.files = e.target.files; 

          this.sizeSum = 0
          for (let i = 0; i < this.files.length; i++) { 
            this.sizeSum+= this.files[i].size;
          }
          this.rozmiarMB = Math.floor(this.sizeSum / (1024*1024))

          if(this.sizeSum>30000000){
            alert(`Zbyt duży rozmiar plików.  aktualnie ${this.rozmiarMB} MB (maks. 28 MB)`);
            this.tooBigPics=true;
            return
          }
            const output = document.querySelector("#result-pics");
            output.innerHTML=""
            for (let i = 0; i < this.files.length; i++) { 
                if (!this.files[i].type.match("image")) continue; 
                const picReader = new FileReader(); 
                picReader.addEventListener("load", function (event) { 
                  const picFile = event.target;
                  const img = document.createElement("img");
                  img.classList.add("uploaded-pic");
                  img.src = picFile.result;
                  output.appendChild(img);
                });
                picReader.readAsDataURL(this.files[i]); 
            }
           
            } else {
              alert("Przeglądarka nie wspiera File API");
            }

       },

       showImages(){
          const output = document.querySelector("#result-pics");
          for (let i = 0; i < this.files.length; i++) {  
                  const img = document.createElement("img");
                  img.classList.add("uploaded-pic");
                  img.src = `data:image/jpeg;base64,${this.files[i]}`
                  output.appendChild(img);     
          }
          this.files=[]
            
            
       },


         validateTitle() {
            var errorText=""
            this.titleError=""
            var input = document.getElementById("title");
            input.classList.remove("error-input");
            input.classList.add("correct-input");
            
        
            if (!this.item.title) {
                errorText = "Podaj tytuł ogłoszenia."
                this.titleError = errorText
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            if (checkDanger(this.item.title))
            {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Tytuł zawiera niebezpieczne znaki."
                this.titleError = errorText
                return false;
            } 
            if (!checkTextLengthRange(this.item.title,5,45)) {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Tytuł powinien zawierać od 5 do 45 znaków."
                this.titleError = errorText
                return false;
            }
            
            return true;
            
        },
        validateCategory() {
            var errorText=""
            this.CategoryError=""
            var input = document.getElementById("category");
            input.classList.remove("error-input");
            input.classList.add("correct-input");
            
        
            if (!this.item.categoryId) {
                errorText = "Wybierz kategorię."
                this.categoryError = errorText
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                return false
            }
            
            
            return true;
            
        },
        validateDesc() {
            var errorText=""
            this.descriptionError=""
            var input = document.getElementById("desc");
            input.classList.remove("error-input");
            input.classList.add("correct-input");

            if (!this.item.description){
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Podaj opis ogłoszenia."
                this.descriptionError = errorText
                return false;
            }
            if (checkDanger(this.item.description))
            {
                input.classList.remove("correct-input");
                input.classList.add("error-input");
                errorText = "Opis zwiera niebezpieczne znaki."
                this.descriptionError = errorText
                return false;
            } 
            if (!checkTextLengthRange(this.item.description,100,850)) {
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
              
              this.item.location = document.getElementById("autocomplete-loc").value
              this.placeSelected = true;
              
              this.locationError=""
              var input = document.getElementById("autocomplete-loc");
              input.classList.remove("error-input");
              input.classList.add("correct-input");
            
        },
       
          validateForm() {
            const validTitle = this.validateTitle();
            const validDesc = this.validateDesc();
            const validCategory = this.validateCategory();
            //this.validateLocation();
           
           if(!this.placeSelected)
              this.locationError = "Wybierz lokalizacje z listy."
            
           
            return (validTitle && validDesc && validCategory && !this.tooBigPics)
               
        }
    },

    mounted:function(){
      this.getCategories()
      if(!this.create)
        this.retrieveImages()
        
      
        
  }


 };
</script>
