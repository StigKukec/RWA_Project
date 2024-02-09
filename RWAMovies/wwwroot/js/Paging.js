
function paging(size, urlPartial, idContainer) {
    $(".pager-btn").click(function (event) {
        console.log("Pager button clicked");
        event.preventDefault();

        var orderBy = localStorage.getItem("orderBy");
        var direction = localStorage.getItem("direction");
        page = $(this).data("page");

        var ajaxData = {
            page: page,
            size: size,
            orderBy: orderBy,
            direction: direction
        };
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
                $(idContainer).html(data);

                $(".pager-btn").removeClass("btn-primary");
                $(".pager-btn").addClass("btn-outline-primary");

                $(".pager-btn[data-page=" + page + "]").removeClass("btn-outline-primary");
                $(".pager-btn[data-page=" + page + "]").addClass("btn-primary");
            },
            error: function (data) {
                console.error("Ajax GET error", data);
            }
        });
    });
}

function showPagedItems(size, classContainer, urlPartial, idHtmlContainer) {
    $(classContainer).click(function () {
        $("#pager").show();
        var orderBy = localStorage.getItem("orderBy");
        var direction = localStorage.getItem("direction");
        var ajaxData = {
            page: 0,
            size: size,
            orderBy: orderBy,
            direction: direction
        };
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