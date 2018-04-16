// Write your JavaScript code.

var map, infoWindow;
var defaultLocation = {lat: -43.537923, lng: 172.643018};
var markersArray = [];

var HttpClient = function() {
    this.get = function(aUrl, aCallback) {
        var anHttpRequest = new XMLHttpRequest();
        anHttpRequest.onreadystatechange = function() { 
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200)
                aCallback(anHttpRequest.responseText);
        }

        anHttpRequest.open( "GET", aUrl, true );            
        anHttpRequest.send( null );
    }
}

function AlertName(name)
{
    alert('You clicked '+ name +"!");
}

function initMap()
{
        map = new google.maps.Map(document.getElementById('map'), {
          zoom: 19,
          center: defaultLocation
        });
        infoWindow = new google.maps.InfoWindow;
        setMapToDefaultLocation();
}

function deleteAllMarkers()
{
  if (markersArray) {
    var arrayLength = markersArray.length;
    for (var i = 0; i < arrayLength; i++) {
      markersArray[i].setMap(null);
    }
    markersArray.length = 0;
  }
}

function setMapToDefaultLocation()
{
    deleteAllMarkers();
    map.setCenter(defaultLocation);
    markersArray[0] = new google.maps.Marker({
        position: defaultLocation,
        map: map,
        title: 'DefaultLocation'
    });
}

function setMarkers(positionsArray)
{
    for (var i = 0; i < positionsArray.length; i++) {
        marker = new google.maps.Marker({
        map: map,
        position: {lat: positionsArray[i].lat, lng: positionsArray[i].lng}
      });
      markersArray[i] = marker;
      bounds.extend(positionsArray[i]);
    }
    map.fitBounds(bounds);
}

function getGeolocation()
{
    deleteAllMarkers();
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
      },{enableHighAccuracy: true}
      );
    } else {
      // Browser doesn't support Geolocation
      handleLocationError(false, infoWindow, map.getCenter());
    }

}

function getLocationFromServer(group)
{
    var client = new HttpClient();
    client.get("http://localhost:3001/tests/"+group, function(response) {
        console.log(response);
        var jsonData = JSON.parse(response);
        var positionsArray = [];
        for (var i = 0; i < jsonData.data.length; i++) {
            var data = jsonData.data[i];
            positionsArray[i] = data;
            console.log(data.lat);
        }
        setMarkers(positionsArray);
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) 
{
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
                          'Error: The Geolocation service failed.' :
                          'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
 }

