function loadMap(routeId) {
    var apiUrl = location.origin + "/map/GetRoute";
    var route = {};
    $.ajax({
        method: "GET",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: JSON.stringify(routeId),
        success: function (response) {
            route = response.routeView;
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });

    alert("Map Loaded: "+route.RouteId);
}
