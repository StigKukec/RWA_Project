// *****Genre
var genreArray = [];
var gi = 0;
function saveSelectedGenres(genreDisplayId) {
    document.getElementById('loadedGenre').addEventListener('change', function () {
        var selectedValue = this.selectedIndex;
        var itemId = this.value;
        var selectedText = this.options[selectedValue].text;
        var selectedItem = { value: itemId, text: selectedText };

        const btnDelete = document.createElement('button');
        btnDelete.setAttribute('id', 'btnDeleteGenre' + gi);
        gi++;
        btnDelete.se
        btnDelete.innerText = 'x';

        if (selectedValue < 1 || genreArray.length > 2 || genreArray.some(obj => obj.text === selectedItem.text)) {
            return;
        }

        genreDisplayId.append(selectedItem.text);
        genreDisplayId.appendChild(btnDelete);
        genreArray.push(selectedItem);

        var genreIds = "";
        for (var i = 0; i < genreArray.length; i++) {

            if (i == (genreArray.length - 1)) {
                genreIds += (genreArray[i].value);
            }
            else {
                genreIds += (genreArray[i].value + ",");
            }
        }
        document.getElementById("hiddenGenres").value = genreIds;
    })

    document.getElementById('deleteAllGenres').addEventListener('click', function () {
        genreArray = [];
        gi = 0;
        genreDisplayId.innerHTML = '';
    });
}


// *****Tag
var tagArray = [];
var ti = 0;
function saveSelectedTags(tagDisplayId) {
    document.getElementById('loadedTags').addEventListener('change', function () {
        var selectedValue = this.selectedIndex;
        var itemId = this.value;
        var selectedText = this.options[selectedValue].text;
        var selectedItem = { value: itemId, text: selectedText };

        const btnDelete = document.createElement('button');
        btnDelete.setAttribute('id', 'btnDeleteTag' + ti);
        ti++;
        btnDelete.se
        btnDelete.innerText = 'x';

        if (selectedValue < 1 || tagArray.some(obj => obj.text === selectedItem.text)) {
            return;
        }

        tagDisplayId.append(selectedItem.text);
        tagDisplayId.appendChild(btnDelete);
        tagArray.push(selectedItem);

        var tagIds = "";
        for (var i = 0; i < tagArray.length; i++) {

            if (i == (tagArray.length - 1)) {
                tagIds += (tagArray[i].value);
            }
            else {
                tagIds += (tagArray[i].value + ",");
            }
        }
        document.getElementById("hiddenTags").value = tagIds;
    })

    document.getElementById('deleteAllTags').addEventListener('click', function () {
        tagArray = [];
        ti = 0;
        tagDisplayId.innerHTML = '';
    });
}

function restrictCountrySelection() {
    document.getElementById('loadedCountries').addEventListener('change', function () {
        var selectedValue = this.selectedIndex;

        if (selectedValue < 1) {
            return;
        }
    })
}

function restrictUserTypeSelection() {
    document.getElementById('loadedUserTypes').addEventListener('change', function () {
        var selectedValue = this.selectedIndex;

        if (selectedValue < 1) {
            return;
        }
    })
}

function removeItem(id) {
    $("#" + id).remove();
}




/*
var dataToPost = JSON.stringify({
    genreId: genreArray
});

var submit = document.getElementById("createVideo");
submit.addEventListener("click", function (event) {
    event.preventDefault();
    $.ajax({
        type: "POST",
        url: "\Create",
        data: dataToPost,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            return response.success;
        },
        error: function (response) {
            return response.error;
        }
    });
});
*/
