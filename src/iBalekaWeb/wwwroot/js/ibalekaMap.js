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
    var toolBox = document.getElementById('toolBoxWrapper');
    toolBox.index = 1;
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(toolBox);
    // Add a listener for the click event
    map.addListener('click', addCheckPoint);
}
//**************End Load Google Maps************************//

//Get Current Location
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        //demoBox.innerHTML = "Geolocation is not supported by this browser.";
        //error message overly
    }
}
function showPosition(position) {
    //add marker animation
    //demoBox.innerHTML = "Current Location:<br />Latitude: " + position.coords.latitude + "<br/>Longitude: " + position.coords.longitude;
    map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
}
//**************End Current Location************************//


//Checkpoints
var routePath, marker, totalDistance, prevLocation;
var markers = [];
var markersOrders = [];
var routePoints = [];
var nrCheckpointsText = document.getElementById('nrCheckpoints');
var totalDistanceText = document.getElementById('totalDistance');
var totalDistance = 0;
var distanceCoords = [];
//get unique marker methods
var getMarkerUniqueId = function (lat, lng) {
    return lat + '_' + lng;
};
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
        title: 'Checkpoint ' + routePoints.length,
        id: routePoints.length,
        map: map
    });
    //update toolbox
    markers[markerId] = marker;
    markersOrders.push(marker);
    dragMarkerEvent(marker);
    updateToolboxStats();
    //totalDistanceText.innerHTML = "Total Distance: ";
    bindMarkerPolylineEvents(marker);
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
            }
            for (var key in markers) {
                if (markers.hasOwnProperty(key)) {
                    if (key === getMarkerUniqueId(prevLocation.lat(), prevLocation.lng())) {
                        var markerId = getMarkerUniqueId(event.latLng.lat(), event.latLng.lng());
                        markers[markerId] = marker;
                        delete markers[key];
                        break;
                    }
                }
            }
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
//marker events
function bindMarkerPolylineEvents(marker) {
    //remove any listeners on marker first
    google.maps.event.addListener(marker, "rightclick", function (point) {
        var markerId = getMarkerUniqueId(point.latLng.lat(), point.latLng.lng());
        var _marker = markers[markerId];
        removeCheckpoint(_marker, markerId);
        removePolyline(point);
        resetPolyline();
        refreshMarkers();
    });
}
//Delete Checkpoint
function removeCheckpoint(_marker, markerId) {
    _marker.setMap(null);
    delete markers[markerId];

    for (var i = 0; i < markersOrders.length; i++) {
        if (markersOrders[i] === _marker) {
            markersOrders.splice(i, 1);
            break;
        }
    }
    updateToolboxStats();

}
function removePolyline(point) {
    for (var i = 0; i < routePoints.length; i++) {
        if (routePoints[i] === point.latLng) {
            routePoints.splice(i, 1);

            break;
        }
    }
}
//reset route
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

//Map HUD
function createToolbox(toolboxDiv, map) {


}
function saveRoute() {

    var apiUrl = location.origin + "/map/AddRoute";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createObject(),
        success: function (response) {
            window.location.href = location.origin + "/map/SavedRoutes";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}


function createObject() {
    var title = prompt("Enter the Route Title");
    var routeModel = { Title: title,Checkpoints: [] ,TotalDistance:totalDistance};
    for (var i = 0; i < markersOrders.length; i++) {
        var latlng = markersOrders[i].getPosition();
        var Checkpoint = {
            'Latitude': latlng.lat(),
            'Longitude': latlng.lng()
        };
        routeModel.Checkpoints.push(Checkpoint);
    }
    return JSON.stringify(routeModel);
}
function clearRoute() {
    for (var key in markers) {
        markers[key].setMap(null);
    }
    markersOrders = [];
    markers = [];
    routePoints = [];
    resetPolyline();
    updateToolboxStats();
}
//toolbox stats
function updateToolboxStats() {
    nrCheckpointsText.innerHTML = "Nr of Checkpoints: " + getCheckpointLength();
    totalDistanceText.innerHTML = "Total Distance " + getTotalDistance();
}
function getTotalDistance() {

    refreshDistance();
    if (totalDistance > 999) {
        return totalDistance / 1000 + "km";
    }
    return totalDistance + "m";
}
function calculateDistance(distanceArray) {
    var latlng1, latlng2;
    latlng1 = distanceCoords[0];
    latlng2 = distanceCoords[1];
    var distance = google.maps.geometry.spherical.computeDistanceBetween(latlng1, latlng2);
    return Math.round(distance);
}
function refreshDistance() {
    distanceCoords = [];
    totalDistance = 0;
    for (var i = 0; i < markersOrders.length; i++) {
        loadDistanceCoords(markersOrders[i]);
        if (i >= 1) {
            totalDistance += calculateDistance(distanceCoords);
        }
    }
}
function loadDistanceCoords(marker) {
    distanceCoords.push(marker.getPosition());
    if (distanceCoords.length > 2) {
        distanceCoords.splice(0, 1);
    }
}
function getCheckpointLength() {
    return markersOrders.length;
}
//**************End Map HUD************************//