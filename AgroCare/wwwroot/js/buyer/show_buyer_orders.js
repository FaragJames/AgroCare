var selectElement = document.querySelector('#mySelect');
selectElement.addEventListener('change', function () {
    var selectedValue = selectElement.value;
    showTable(selectedValue);
});

function showTable(selectedValue) {
    var tbodies = document.querySelectorAll('tbody');
    var tfoots = document.querySelectorAll('tfoot');

    for (var i = 0; i < tbodies.length; i++) {
        var tbody = tbodies[i];

        if (tbody.classList.contains(selectedValue)) {
            tbody.style.display = 'table-row-group';
            tfoots[i].style.display = '';
        } else {
            tbody.style.display = 'none';
            tfoots[i].style.display = 'none';
        }
    }
}