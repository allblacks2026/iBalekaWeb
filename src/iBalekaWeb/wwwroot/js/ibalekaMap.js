//Load Google Maps
var map, editButton, statsPanel, settingsPanel, searchPanel, searchInput, autocomplete, geocoder;
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15,
        center: { lat: -33.9913503, lng: 25.6568993 },
        zoomControl: true,
        streetViewControl: false,
        scaleControl: true
    });
    getLocation();
    editButton = document.getElementById('editButton');
    statsPanel = document.getElementById('statsPanel');
    settingsPanel = document.getElementById('settingsPanel');
    searchPanel = document.getElementById('searchPanel');

    map.controls[google.maps.ControlPosition.LEFT_BOTTOM].push(editButton);
    map.controls[google.maps.ControlPosition.RIGHT_TOP].push(statsPanel);
    map.controls[google.maps.ControlPosition.BOTTOM_CENTER].push(settingsPanel);

    initializeAutocomplete();
    geocoder = new google.maps.Geocoder;
    map.controls[google.maps.ControlPosition.BOTTOM_CENTER].push(searchPanel);
    searchPanel.style.display = "none";
    settingsPanel.style.display = "none";
    statsPanel.style.display = "none";
    // Add a listener for the click event
    map.addListener('click', addCheckPoint);
}
//method to initalize automcomplete
function initializeAutocomplete() {
    autocomplete = new google.maps.places.Autocomplete(document.getElementById('autocomplete'), { types: ['geocode'] });
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        fillInAddress();
    });
}
function fillInAddress() {
    // Get the place details from the autocomplete object.
    var place = autocomplete.getPlace();

    for (var component in componentForm) {
        document.getElementById(component).value = '';
        document.getElementById(component).disabled = false;
    }

    // Get each component of the address from the place details
    // and fill the corresponding field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }
}
//Map UI

function toastMessage() {
    if (settingsPanel.style.display === "none") {
        Materialize.toast('Map Click Enabled', 3000);
    } else {
        Materialize.toast('Map Click Suspended', 3000);
    }
}
function settingsPanelToggle() {
    if (settingsPanel.style.display === "none") {
        if (searchPanel.style.display !== "none") {
            searchPanel.style.display = "none";
        }
        openSettingsPanel();
    } else {
        closeSettingsPanel();
    }

}
function openSettingsPanel() {
    google.maps.event.clearListeners(map, 'click');
    removeMarkerEvents();
    settingsPanel.style.display = "block";
    toastMessage();
}
function closeSettingsPanel() {
    map.addListener('click', addCheckPoint);
    addMarkerEvents();
    settingsPanel.style.display = "none";
    toastMessage();
}
//update route title
var routeTitleText = document.getElementById('routeTitle');
var routeTitleStat = document.getElementById('routeTitleStat');
function updateRouteTitle() {
    routeTitleText = document.getElementById('routeTitle');
    routeTitleStat.innerHTML = routeTitleText.value;
}
//statistics panel
function statsPanelToggle() {
    if (statsPanel.style.display === "none") {
        statsPanel.style.display = "block";
    } else {
        statsPanel.style.display = "none";
    }
}
function closeStatsPanel() {
    statsPanel.style.display = "none";
}
//search panel
function searchLocation() {
    var place = autocomplete.getPlace();
    if (!place.geometry) {
        window.alert("Autocomplete's returned place contains no geometry");
        return;
    }
    // If the place has a geometry, then present it on a map.
    if (place.geometry.viewport) {
        map.fitBounds(place.geometry.viewport);
    } else {
        map.setCenter(place.geometry.location);
        map.setZoom(16);
    }
}
function searchPanelToggle() {
    if (searchPanel.style.display === "none") {
        if (settingsPanel.style.display !== 'none') {
            settingsPanel.style.display = 'none';
        }
        google.maps.event.clearListeners(map, 'click');
        searchPanel.style.display = "block";
        removeMarkerEvents();
        Materialize.toast('Map Click Suspended', 3000);
    } else {
        closeSearchPanel();
    }
}
function closeSearchPanel() {
    map.addListener('click', addCheckPoint);
    addMarkerEvents();
    searchPanel.style.display = 'none';
    Materialize.toast('Map Click Enabled', 3000);
}
//**************End Load Google Maps************************//

//Get Current Location
var routePoints = [];
function routeLocation() {
    if (routePoints[0] !== null) {
        var latlng = markersOrders[0].getPosition();
        map.setCenter(new google.maps.LatLng(latlng.lat(), latlng.lng()));
        map.setZoom = 15;
        Materialize.toast('Positioned at Route Location', 3000);
    } else {
        Materialize.toast('No Checkpoints dropped', 3000);
    }
}
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
        Materialize.toast('Positioned at Current Location', 3000);
    } else {
        Materialize.toast('Current Location could not be determined..', 3000);
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
    dragMarkerEvent(marker);
    markersOrders.push(marker);
    updateToolboxStats();
    //totalDistanceText.innerHTML = "Total Distance: ";
    bindMarkerPolylineEvents(marker);
    if (routePoints[1] === null) {
        statsPanel.style.display = "block";
    }
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
//save route
function saveRoute() {
    var title;
    if (routePoints[2] !== null) {
        if (routeTitleText.value === "") {
            $('#routeTitleModal').openModal();

        } else {
            title = routeTitleText.value;
            saveRouteAJAX(title);
        }
    } else {
        Materialize.toast('Not Enough Checkpoints to Save Route', 3000);
    }


}
function saveRouteModal() {
    var title = document.getElementById('routeTitleTextModal');
    routeTitleText.value = title.value;
    updateRouteTitle();
    saveRouteAJAX(title.value);
}
function saveRouteAJAX(title) {
    $('#btnSaveRoute').prop('disabled', true);
    var $toastContent = $('<span>Saving Route...</span>');
    Materialize.toast($toastContent, 9000);
    var apiUrl = location.origin + "/map/AddRoute";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createObject(title),
        success: function (response) {
            window.location.href = location.origin + "/map/SavedRoutes";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function createObject(title) {
    var routeModel = { Title: title, Checkpoints: [], TotalDistance: totalDistance, Location: "" };
    for (var i = 0; i < markersOrders.length; i++) {
        if (i === 1) {
            var routeLocationCoord = markersOrders[i].getPosition();
           
            routeModel.Location = getRouteLocation(routeLocationCoord);
        }
        var latlng = markersOrders[i].getPosition();
        var Checkpoint = {
            'Latitude': latlng.lat(),
            'Longitude': latlng.lng()
        };
        routeModel.Checkpoints.push(Checkpoint);
    }
    return JSON.stringify(routeModel);
}
function getRouteLocation(latlng) {
    var locationName = "";    
    geocoder.geocode({ 'location': latlng }, function (results, status) {
        if (status === 'OK') {
            for (var i in results.address_components) {
                if (results.address_components[i].types[0] === "locality")
                    locationName += results.address_components[i].long_name + ",";
                if (results.address_components[i].types[0] === "administrative_area_level_1")
                    locationName += results.address_components[i].long_name + ",";
                if (results.address_components[i].types[0] === "country")
                    locationName += results.address_components[i].long_name;
            } 
        } else {
            window.alert('Geocoder failed due to: ' + status);
        }
    });
   
return locationName;
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

//Edit Route

//Load Route

var loadedRoute;
function loadRoute(route) {
    routeTitleText.value = route.title;
    updateRouteTitle();
    map.setCenter(new google.maps.LatLng(route.checkpoints[0].latitude, route.checkpoints[0].longitude));
    Materialize.toast('Route Succesfully Loaded', 3000);
    for (var i = 0; i < route.checkpoints.length; i++) {
        loadCheckpoints(route.checkpoints[i]);
    }
    loadedRoute = route;
    statsPanel.style.display = "block";
}
function loadCheckpoints(coOrds) {
    var point = new google.maps.LatLng(coOrds.latitude, coOrds.longitude);
    routePoints.push(point);
    if (!(routePath === undefined)) {
        routePath.setMap(null);
    }
    routePath = getRoutePath();
    routePath.setMap(map);

    var markerId = getMarkerUniqueId(coOrds.latitude, coOrds.longitude);
    marker = new google.maps.Marker({
        position: point,
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

//update Route
function updateRouteModal() {
    var title = document.getElementById('routeTitleTextModal');
    routeTitleText.value = title.value;
    updateRouteTitle();
    updateRouteAJAX();
}
function updateRoute() {
    if (routePoints[2] !== null) {
        if (routeTitleText.value !== "") {
            $('#btnUpdateRoute').prop('disabled', true);
            var $toastContent = $('<span>Updating Route...</span>');
            Materialize.toast($toastContent, 9000);
            updateRouteAJAX();
        } else {
            $('#routeTitleModal').openModal();
        }
    } else {
        Materialize.toast('Not Enough Checkpoints to Save Route', 3000);
    }
}
function updateRouteAJAX() {
    var apiUrl = location.origin + "/map/Edit";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createUpdatedObject(),
        success: function (response) {
            window.location.href = location.origin + "/map/SavedRoutes";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function cancelRoute() {
    var apiUrl = location.origin + "/map/SavedRoutes";
    $.ajax({
        method: "GET",
        url: apiUrl,
        processData: false,
        success: function (response) {
            window.location.href = location.origin + "/map/SavedRoutes";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}

function createUpdatedObject() {

    var routeModel = { RouteId: loadedRoute.routeId, Title: routeTitleText.value, Checkpoints: [], TotalDistance: totalDistance };
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

//**************End Edit Route************************//

//delete route
var loadedRouteId;
function loadDeletedRouteId(routeId) {
    if (routeId !== null) {
        loadedRouteId = routeId;
    }
}
function deleteRoute() {
    var apiUrl = location.origin + "/map/Delete";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: JSON.stringify(loadedRouteId),
        success: function (response) {
            window.location.href = location.origin + "/map/SavedRoutes";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            console.log("Error: " + textStatus.toString() + " " + errorThrown.toString() + " " + httpRequest.toString());
        }
    });
}
//**************End delete rout************************//

//events
function removeMarkerEvents() {
    if (markersOrders !== null) {
        for (var i = 0; i < markersOrders.length; i++) {
            google.maps.event.clearListeners(markersOrders[i], 'rightclick');
            marker.setDraggable(false);
        }
    }
}
function addMarkerEvents() {
    if (markersOrders !== null) {
        for (var i = 0; i < markersOrders.length; i++) {
            bindMarkerPolylineEvents(markersOrders[i]);
            marker.setDraggable(true);
        }
    }
}
//**************End delete rout************************//