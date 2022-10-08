document.querySelector("#upload-pics").addEventListener("change", (e) => { 
    if (window.File && window.FileReader && window.FileList && window.Blob) { 
      const files = e.target.files; 
    if(files.length>6){
      alert("Można wybrać maksymalnie 6 zdjęć");
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
  });