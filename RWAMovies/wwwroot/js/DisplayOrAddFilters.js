
function fillFilterItems(displayId, filterItemDisplay) {
   // const htmlElement = `<li><a class="dropdown-item" href="#">' + filterItemDisplay + '</a></li>`;
    $(displayId).append(
        $('<li>').append(
            $("<a>")
                .addClass("dropdown-item")
                .attr("href", "#")
                .text(filterItemDisplay)
            )
           
    );
};
function fillFilterItemsDefault(displayId) {
    defaultItem(displayId, "ascending");
    defaultItem(displayId, "descending");
};

function defaultItem(displayId,filterItemDisplay) {
    $(displayId).append(
        $('<li>').append(
            $("<a>")
                .addClass("dropdown-item")
                .attr("href", "#")
                .text(filterItemDisplay)
        )

    );
}

function addFilter( filterName) {
    const htmlElement =
    `<li class="nav-item dropdown"> 
        <a class="nav-link dropdown-toggle" href = "#" id = "navbarDropdown" role = "button" data-bs-toggle="dropdown" aria-expanded="false" >
            By `+ filterName + `
        </a>
        <ul class="`+ filterName + `  dropdown-menu" style="overflow-y: auto; max-height:25vh" aria-labelledby="navbarDropdown">

        </ul>
     </li>
    `;

    function addFilterItem() {
        var element = document.querySelector(".addedFilters");
        element.insertAdjacentHTML("afterbegin", htmlElement);
    }
    addFilterItem();
}

function defaultFilterItemsName(text) {
    $(".showAllItems").append(text);
    $(".showPagedItems").append(text);
}
