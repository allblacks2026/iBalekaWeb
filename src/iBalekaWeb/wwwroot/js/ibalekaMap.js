//Load Google Maps
var map;
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: { lat: -33.9913503, lng: 25.6568993 },
        zoomControl: true,
        scaleControl: true
    });
    getLocation();
    // Add a listener for the click event
    map.addListener('click', addCheckPoint);
}
//**************End Load Google Maps************************//

//Get Current Location
var demoBox = document.getElementById("demo");
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        demoBox.innerHTML = "Geolocation is not supported by this browser.";
        myLatlng = { lat: -34.0034, lng: 25.6680 };
    }
}
function showPosition(position) {
    var pos = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
    };
    demoBox.innerHTML = "Current Location:<br />Latitude: " + position.coords.latitude + "<br/>Longitude: " + position.coords.longitude;
    map.setCenter(pos);
}
//**************End Current Location************************//

//Checkpoints
var routePath;
var markers = [];
var markersOrders = new Array(0);
var routePoints = new Array(0);
var prevLocation;
var marker;
//get unique marker methods
var getMarkerUniqueId = function (lat, lng) {
    return lat + '_' + lng;
}
//Add Markers and Polylines
function addCheckPoint(event) {
    routePoints.push(event.latLng);
    if (!(routePath === undefined)) {
        routePath.setMap(null);
    }
    routePath = getRoutePath();
    routePath.setMap(map);

    var markerId = getMarkerUniqueId(event.latLng.lat(), event.latLng.lng());
    marker = new google.maps.Marker({
        position: event.latLng,
        draggable: true,
        //animation: google.maps.Animation.DROP,
        title: 'Checkpoint ' + (routePoints.length),
        id: routePoints.length,
        map: map
    });

    markers[markerId] = marker;
    markersOrders.push(marker);
    dragMarkerEvent(marker);
    bindMarkerPolylineEvents(marker)
}
function dragMarkerEvent(marker) {
    google.maps.event.addListener(marker, 'dragstart', function (event) {
        prevLocation = marker.getPosition();
        google.maps.event.addListener(marker, 'dragend', function (event) {
            for (var i = 0; i < routePoints.length; i++) {
                if (routePoints[i] === prevLocation) {
                    routePoints[i] = event.latLng;
                    break;
                }
            };
            for (var key in markers) {
                if (markers.hasOwnProperty(key)) {
                    if (markers[key].id === ('marker_' + getMarkerUniqueId(prevLocation.lat(), prevLocation.lng()))) {
                        var markerId = getMarkerUniqueId(event.latLng.lat(), event.latLng.lng());
                        markers[markerId] = marker;
                        delete markers[key];
                        break;
                    }
                }
            };
            resetPolyline();
        });

    });
}
function getRoutePath() {
    var routePath = new google.maps.Polyline({
        path: routePoints,
        strokeColor: '#000000',
        strokeOpacity: 1.0,
        strokeWeight: 3,
        geodesic: true

    });
    return routePath;

}
//Delete Checkpoint
function bindMarkerPolylineEvents(marker) {
    google.maps.event.addListener(marker, "rightclick", function (point) {
        var markerId = getMarkerUniqueId(point.latLng.lat(), point.latLng.lng());
        var _marker = markers[markerId];
        removeCheckpoint(_marker, markerId);
        removePolyline(point);
        resetPolyline();
        refreshMarkers();
    });
}
function removeCheckpoint(_marker, markerId) {
    _marker.setMap(null);
    delete markers[markerId];

    for (var i = 0; i < markersOrders.length; i++) {
        if (markersOrders[i] === _marker) {
            markersOrders.splice(i, 1);
            break;
        }
    }
    

}
function removePolyline(point) {
    for (var i = 0; i < routePoints.length; i++) {
        if (routePoints[i] === point.latLng) {
            routePoints.splice(i, 1);
            break;
        }
    }
}
function resetPolyline() {
    //remove polyline
    routePath.setMap(null);
    //add new polyline
    routePath = getRoutePath();
    routePath.setMap(map);

}

function refreshMarkers() {
    for (var i = 0; i < markersOrders.length; i++) {
        markersOrders[i].setTitle('Checkpoint ' + (i + 1));
    }
    for (var p = 0; p < markersOrders.length; p++) {
        for (var key in markers) {
            if (markersOrders[p].getPosition() === markers[key].getPosition()) {
                markers[key].setTitle('Checkpoint ' + (p + 1));
                break;
            }
        }
    }
}
//**************End Checkpoints************************//