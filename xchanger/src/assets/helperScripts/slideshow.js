

var i = 0; 			// Start Point
var images = [];	// Images Array
	 
// Image List
images[0] = "../images/kask.png";
images[1] = "../images/kask2.png";
images[2] = "../images/kask3.png";


function setImg(){

  document.slide.src = images[0];
}

function changeImgForward(){
  if(i < images.length - 1){
	  i++; 
	} else { 
		i = 0;
	}

	document.slide.src = images[i];

}

function changeImgBackwards(){
  if(i > 0){
	  i--; 
	} else { 
		i = images.length-1;
	}

	document.slide.src = images[i];

}

function readURL(input) {
  if (input.files && input.files[0]) {
    var reader = new FileReader();

    reader.onload = function (e) {
      $('#blah').attr('src', e.target.result).width(150).height(200);
    };

    reader.readAsDataURL(input.files[0]);
  }
}


window.onload=setImg;