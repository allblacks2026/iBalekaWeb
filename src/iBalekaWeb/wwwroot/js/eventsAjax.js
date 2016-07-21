var eventObject;
function loadEventObject(loadEvent) {
    if (loadEvent !== null)
        eventObject = loadEvent;
}
function addDetails() {
    var apiUrl = location.origin + "/Event/AddDetails";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: event,
        success: function (response) {
            window.location.href = location.origin + "/Event/AddRoutes";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function addRoutes() {
    var apiUrl = location.origin + "/Event/SaveRoutes";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: event,
        success: function (response) {
            window.location.href = location.origin + "/Event/FinalizeEvent";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function saveEvent() {
    var apiUrl = location.origin + "/Event/SaveEvent";
    $.ajax({
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        processData: false,
        data: event,
        success: function (response) {
            window.location.href = location.origin + "/map/Events";
        },
        error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
            alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
        }
    });
}
function back(page) {
    var apiUrl;
    if (page === "toCreateEvent") {
        apiUrl = location.origin + "/Event/CreateEvent";
        $.ajax({
            method: "GET",
            url: apiUrl,
            contentType: "application/json;charset=utf-8",
            processData: false,
            data: event,
            //success: function (response) {
            //    window.location.href = location.origin + "/map/SavedRoutes";
            //},
            error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
                alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
            }
        });
    }
    if (page === "toAddRoute") {
        apiUrl = location.origin + "/Event/AddRoutes";
        $.ajax({
            method: "GET",
            url: apiUrl,
            contentType: "application/json;charset=utf-8",
            processData: false,
            data: event,
            //success: function (response) {
            //    window.location.href = location.origin + "/map/SavedRoutes";
            //},
            error: function (httpRequest, textStatus, errorThrown) {  // detailed error messsage 
                alert("Error: " + textStatus + " " + errorThrown + " " + httpRequest);
            }
        });
    }

}