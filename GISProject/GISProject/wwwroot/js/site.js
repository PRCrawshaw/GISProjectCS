// Write your JavaScript code.

var map, infoWindow;
var defaultLocation = {lat: -43.5470767, lng: 172.669525};

function AlertName(name)
{
    alert('You clicked '+ name +"!");
}

function initMap(){
        var map = new google.maps.Map(document.getElementById('map'), {
          zoom: 19,
          center: defaultLocation
        });
        infoWindow = new google.maps.InfoWindow;

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(function(position) {
        var pos = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };

        infoWindow.setPosition(pos);
        infoWindow.setContent(position.coords.latitude + ', '+position.coords.longitude);
        infoWindow.open(map);
        map.setCenter(pos);
      }, function() {
        handleLocationError(true, infoWindow, map.getCenter());
      });
    } else {
      // Browser doesn't support Geolocation
      handleLocationError(false, infoWindow, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
                          'Error: The Geolocation service failed.' :
                          'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
 }

