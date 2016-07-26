var eventObject;
function loadEventObject(loadEvent) {
    if (loadEvent !== null)
        eventObject = loadEvent;
}
function createEventObject() {
    var eventModel = { Date: eventObject.date, Description: eventObject.description, EventRoutes: [], Location: eventObject.location, Time: eventObject.time, Title: eventObject.title };
    for(var i=0; i < eventObject.eventRoutes.length; i++){
        var EventRoute = { RouteId: eventObject.eventRoutes[i].routeId, Distance: eventObject.eventRoutes[i].distance, Title: eventObject.eventRoutes[i].title };
        eventModel.EventRoutes.push(EventRoute);
    }
    return JSON.stringify(eventModel);
}
function saveEvent() {
    var apiUrl = location.origin + "/Event/FinalizeEvent";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createEventObject(),
        success: function (response) {
            window.location.href = location.origin + "/Event/Events";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function back() {
    var apiUrl;
 
    apiUrl = location.origin + "/Event/CreateEventReload";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createEventObject(),
        success: function (response) {
            window.location.href = location.origin + "/Event/CreateEventReload";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}


