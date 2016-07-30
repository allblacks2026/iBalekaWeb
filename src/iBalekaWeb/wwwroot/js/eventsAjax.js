var eventObject;
var loadedEventId;
function loadDeletedEventId(eventId) {
    if (eventId !== null) {
        loadedEventId = eventId;
    }
}
function loadEventObject(loadEvent) {
    if (loadEvent !== null)
        eventObject = loadEvent;
}
function createEventObject() {
    var eventModel = { EventId: eventObject.eventId, Date: eventObject.date, Description: eventObject.description, EventRoutes: [], Location: eventObject.location, Time: eventObject.time, Title: eventObject.title };
    for (var i = 0; i < eventObject.eventRoutes.length; i++) {
        var EventRoute = { RouteId: eventObject.eventRoutes[i].routeId, Distance: eventObject.eventRoutes[i].distance, Title: eventObject.eventRoutes[i].title, DateAdded: eventObject.eventRoutes[i].dateAdded };
        eventModel.EventRoutes.push(EventRoute);
    }
    return JSON.stringify(eventModel);
}
function createUpdatedEventObject() {
    var title, description, date, time, location;
    title = $("#Title").val();
    description = $("#Description").val();
    date = $("#datepicker").val();
    time = $("#timepicker").val();
    location = $("#Location").val();
    var routeList = $("#RouteId option");
    var routeIds = [];
    
    var eventModel = { EventId: eventObject.eventId, Date: date, Description: description, EventRoutes: [], Location: location, Time: time, Title: title,RouteId:[] };
    for (var i = 0; i < eventObject.eventRoutes.length; i++) {
        var EventRoute = { RouteId: eventObject.eventRoutes[i].routeId };
        eventModel.EventRoutes.push(EventRoute);
    }
    for (var j = 0; j < routeList.length; j++) {
        if (routeList[j].selected === true) {
            var id = routeList[j].value;
            eventModel.RouteId.push(id);
        }
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

function DeleteEvent() {
    var apiUrl = location.origin + "/Event/Delete";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: JSON.stringify(loadedEventId),
        success: function (response) {
            window.location.href = location.origin + "/Event/Events";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            var err = eval("(" + httpRequest.responseText + ")");
            alert("Error: " + err);
        }
    });
}

function EditEvent() {
    var apiUrl = location.origin + "/Event/Edit";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createUpdatedEventObject(),
        success: function (response) {
            window.location.href = location.origin + "/Event/EventDetails/"+eventObject.eventId;
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            var err = eval("(" + httpRequest.responseText + ")");
            alert("Error: " + err);
        }
    });
}


