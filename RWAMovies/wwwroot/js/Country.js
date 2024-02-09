
document.getElementById('loadedCountries').addEventListener('change', function () {
    var selectedValue = this.selectedIndex;
    var itemId = this.value;
    var selectedText = this.options[selectedValue].text;
    var selectedItem = { value: itemId, text: selectedText };

    if (selectedValue < 1) {
        return;
    }

    var countryId = "";
    countryId += selectedItem.value;
    document.getElementById("hiddenCountry").value = countryId;
})
