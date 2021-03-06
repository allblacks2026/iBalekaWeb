﻿// Write your Javascript code.
$(".button-collapse").sideNav();


// initialize the select element
$(document).ready(function () {
    $('select').material_select();
});


$(function () {
    Materialize.updateTextFields();
});

function initAutocomplete() {
    // Create the autocomplete object, restricting the search to geographical
    // location types.
    autocomplete = new google.maps.places.Autocomplete(
        /** @type {!HTMLInputElement} */(document.getElementById('autocomplete')),
        { types: ['geocode'] });

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete.addListener('place_changed', fillInAddress);
}