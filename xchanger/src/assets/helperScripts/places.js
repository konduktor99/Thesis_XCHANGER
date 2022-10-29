


function initialize() {
    var options = {
        types: ['address']
    }
     

    var input = document.getElementById('address');
    var autocomplete = new google.maps.places.Autocomplete(input, options);

    autocomplete.addListener('place_changed', fillInAddress);
  }

  google.maps.event.addDomListener(window, 'load', initialize);
