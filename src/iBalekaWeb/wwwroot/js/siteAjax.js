//Events
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
    var routeIds = [];
    var title, description, date, time, location;
    title = $("#Title").val();
    description = $("#Description").val();
    date = $("#datepicker").val();
    time = $("#timepicker").val();
    location = $("#autocomplete").val();
    $("#RouteId option:selected").each(function () {
        var $this = $(this);
        routeIds.push($this.val());
    });
    

    var eventModel = { EventId: eventObject.eventId, Date: date, Description: description, EventRoutes: [], Location: location, Time: time, Title: title, RouteId: routeIds };
    for (var i = 0; i < eventObject.eventRoutes.length; i++) {
        var EventRoute = { RouteId: eventObject.eventRoutes[i].routeId };
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
            window.location.href = location.origin + "/Event/EventDetails/" + eventObject.eventId;
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            var err = eval("(" + httpRequest.responseText + ")");
            alert("Error: " + err);
        }
    });
}

//Clubs
var clubObject;
var loadedClubId;

function loadDeletedClubId(clubId) {
    if (clubId !== null) {
        loadedClubId = clubId;
    }
}
function loadClubObject(loadClub) {
    if (loadClub !== null) {
        clubObject = loadClub;
    }
}
function createClubObject() {
    var clubModel = { ClubId: clubObject.ClubId, Name: clubObject.Name, Description: clubObject.Description, Location: clubObject.Location };
    return JSON.stringify(clubModel);
}
function createUpdatedClubObject() {
    var name, description, location;
    name = $("#Name").val();
    description = $("#Description").val();
    location = $("#autocomplete").val();
    var clubModel = { ClubId: clubObject.clubId, Name: name, Description: description, Location: location, DateCreated: clubObject.dateCreated };
    return JSON.stringify(clubModel);
}

function SaveClub() {
    var apiUrl = location.origin + "/Club/CreateClub";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        success: function (response) {
            window.location.href = location.origin + "/Club/Clubs";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function EditClub() {
    var apiUrl = location.origin + "/Club/Edit";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: createUpdatedClubObject(),
        success: function (response) {
            window.location.href = location.origin + "/Club/ClubDetails/" + clubObject.clubId;
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function DeleteClub() {
    var apiUrl = location.origin + "/Club/Delete";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: JSON.stringify(loadedClubId),
        success: function (response) {
            window.location.href = location.origin + "/Club/Clubs";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}



