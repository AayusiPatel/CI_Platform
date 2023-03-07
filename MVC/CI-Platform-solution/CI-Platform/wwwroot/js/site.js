// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function DisableBackButton() {
    window.history.forward();
}
DisableBackButton();
window.onload = DisableBackButton;
window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton(); }
window.onunload = function () { void (0); };


function GetCity() {
    var countryId = $('#countryId').find(":selected").val();
    debugger;
    $.ajax({
        url: "/Home/GetCitys",
        method: "GET",
        data: {
            "countryId": countryId
        },
        success: function (data) {
            data = JSON.parse(data);
            $("#selectCityList").empty();
            data.forEach((name) => {
                document.getElementById("selectCityList").innerHTML += `
        <option value=${name} >${name.Name}</option>
        `;
            })
        }
        ,
        error: function (request, error) {
            console.log(error);
        }
    })
}




///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// (3) filtering the missions 

$(document).ready(function () {
    var clearAllButton = $('.selected-options .clear-all');
    clearAllButton.hide();
    // Get the dropdown menu element
    const dropdownMenu = $('.dropdown-menu');
    // Prevent the dropdown menu from closing when an option is clicked
    dropdownMenu.on('click', function (event) {
        event.stopPropagation();
    });
});

$(function () {
    var dropdownMenu = $('.dropdown-menu');
    var selectedOptionsList = $('.selected-options ul');
    var clearAllButton = $('.selected-options .clear-all');

    // Handle checkbox click
    dropdownMenu.on('click', 'input[type="checkbox"]', function () {
        var option = $(this).parent();
        var optionValue = $(this).val();

        if ($(this).prop('checked')) {
            // Add the selected option to the displayed list
            selectedOptionsList.append(`<li data-value="${optionValue}">${option.text()}<span class="remove-option" >&#x2715;</span></li>`);
        } else {
            // Remove the selected option from the displayed list
            selectedOptionsList.find(`li[data-value="${optionValue}"]`).remove();
        }

        // Show or hide the Clear All button
        if (selectedOptionsList.children().length > 0) {
            clearAllButton.show();
        } else {
            clearAllButton.hide();
        }
    });

    // Handle remove button click
    selectedOptionsList.on('click', 'span.remove-option', function () {
        // Get the value of the option that was removed
        var optionValue = $(this).parent().data('value');

        // Uncheck the corresponding checkbox in the dropdown menu
        dropdownMenu.find(`input[value="${optionValue}"]`).prop('checked', false);

        // Remove the selected option from the displayed list
        $(this).parent().remove();

        // Show or hide the Clear All button
        if (selectedOptionsList.children().length > 0) {
            clearAllButton.show();
        } else {
            clearAllButton.hide();
        }

        temp();
    });

