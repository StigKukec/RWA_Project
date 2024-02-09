
function autocomplete(dataUrl, urlPartial, idContainer, propertySearch, idPropertySearch) {
    var token = localStorage.getItem("jwt");
    let items = [];
    let itemNames = [];
    $.ajax({
        type: 'GET',
        url: dataUrl,
        /*
        Headers: {
            'Authorization': `Bearer ${token}`
        },
        */
        success: function (data) {
            items = data;
            itemNames = items.map(item => item[propertySearch]);
            console.log("Data recieved.");
            $("#autocomplete-input").autocomplete({
                source: itemNames,
                select: function (event, ui) {
                    let selectedItem = items.find(item => item[propertySearch] === ui.item.value);
                    var ajaxData = { [idPropertySearch]: selectedItem[idPropertySearch] };
                    $.ajax({
                        type: 'GET',
                        url: urlPartial,
                        data: ajaxData,
                        success: function (data) {
                            $(idContainer).html(data);
                        },
                        error: function () {
                            console.error("Could not fetch video data.");
                            //window.location.href = "/UserView/LogIn";
                        }
                    });
                }
            });
        },
        error: function () {
            console.error("Could not fetch video data.");
            //window.location.href = "/UserView/LogIn";
        }
    });
}

function autocompleteDisplay(placeholder) {
    $("#autocomplete-input").attr("placeholder", placeholder);
}