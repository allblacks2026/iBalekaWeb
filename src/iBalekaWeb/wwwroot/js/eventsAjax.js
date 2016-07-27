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
    title = document.getElementById("Title").value;
    description = document.getElementById("Description").value;
    date = document.getElementById("Date").value;
    time = document.getElementById("Time").value;
    location = document.getElementById("Location").value;
    var eventModel = { EventId: eventObject.eventId, Date: date, Description: description, EventRoutes: [], Location: location, Time: time, Title: title };
    for (var i = 0; i < eventObject.eventRoutes.length; i++) {
        var EventRoute = { RouteId: eventObject.eventRoutes[i].routeId };
        eventModel.EventRoutes.push(EventRoute);
    }
    var routeList = document.getElementById("RouteId");
    var routeIds = [];
    for (var j = 0; j < routeList.options.length; j++) {
        if (routeList.options[j].selected === true) {
            routeIds.push(routeList.options[j].value);
        }
    }
    eventModel.RouteId = routeIds
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
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
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
        success: function (responseData) {
            window.location.href = responseData.url
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}


