

function showAllItemsFilter(classContainer, urlPartial, idHtmlContainer) {
    $(classContainer).click(function () {
        $("#pager").hide();

        $.ajax({
            type: "GET",
            url: urlPartial,
            /*
            Headers: {
                'Authorization': `Bearer ${token}`
            },
            */
            success: function (data) {
                $(idHtmlContainer).html(data);
                $("#removeAllFilters").removeClass("disabled");
            },
            error: function (data) {
                console.error("Ajax GET error", data);
            }
        });
    });
}


function itemFilter(size, orderBy, classContainer, urlPartial, idHtmlContainer) {
    $(classContainer).click(function () {
        var ajaxData = {
            page: 0,
            size: size,
            orderBy: orderBy,
            direction: this.text
        };
        localStorage.setItem("orderBy", orderBy);
        localStorage.setItem("direction", this.text);
        $("#pager").show();
        $.ajax({
            type: "GET",
            url: urlPartial,
            data: ajaxData,
            /*
            Headers: {
                'Authorization': `Bearer ${token}`
            },
            */
            success: function (data) {
                $(idHtmlContainer).html(data);
                $(".pager-btn").removeClass("btn-primary");
                $(".pager-btn").addClass("btn-outline-primary");

                $(".pager-btn[data-page=" + 0 + "]").removeClass("btn-outline-primary");
                $(".pager-btn[data-page=" + 0 + "]").addClass("btn-primary");
                $("#removeAllFilters").removeClass("disabled");
            },
            error: function (data) {
                console.error("Ajax GET error", data);
            }
        });
    });
}

function removeAllFilters(size, urlPartial, idHtmlContainer) {
    $('#removeAllFilters').click(function () {
        var ajaxData = {
            page: 0,
            size: size,
            orderBy: "",
            direction: ""
        };
        localStorage.removeItem("orderBy");
        localStorage.removeItem("direction");
        $("#pager").show();
        $.ajax({
            type: "GET",
            url: urlPartial,
            data: ajaxData,
            /*
            Headers: {
                'Authorization': `Bearer ${token}`
            },
            */
            success: function (data) {
                $(idHtmlContainer).html(data);
                $(".pager-btn").removeClass("btn-primary");
                $(".pager-btn").addClass("btn-outline-primary");

                $(".pager-btn[data-page=" + 0 + "]").removeClass("btn-outline-primary");
                $(".pager-btn[data-page=" + 0 + "]").addClass("btn-primary");
                $("#removeAllFilters").addClass("disabled");
            },
            error: function (data) {
                console.error("Ajax GET error", data);
            }
        });
    });
}

function searchFilter(size, urlPartial, idHtmlContainer) {
    $('#btnSearch').click(function () {
        var orderBy = localStorage.getItem("orderBy");
        var direction = localStorage.getItem("direction");
        var searchText = document.querySelector('#autocomplete-input').value;
        var ajaxData = {
            page: 0,
            size: size,
            orderBy: orderBy,
            direction: direction,
            search: searchText
        };
        $("#pager").show();
        $.ajax({
            type: "GET",
            url: urlPartial,
            data: ajaxData,
            /*
            Headers: {
                'Authorization': `Bearer ${token}`
            },
            */
            success: function (data) {
                $(idHtmlContainer).html(data);
                $(".pager-btn").removeClass("btn-primary");
                $(".pager-btn").addClass("btn-outline-primary");

                $(".pager-btn[data-page=" + 0 + "]").removeClass("btn-outline-primary");
                $(".pager-btn[data-page=" + 0 + "]").addClass("btn-primary");
                $("#removeAllFilters").removeClass("disabled");
            },
            error: function (data) {
                console.error("Ajax GET error", data);
            }
        });
    });
}