var selectElement = document.querySelector('#mySelect');
showTable(selectElement.value);

selectElement.addEventListener('change', (event) => {
    showTable(selectElement.value);

    let selectedOption = event.target.selectedOptions[0];
    let fontWeight = window.getComputedStyle(selectedOption).getPropertyValue('font-weight');
    if (fontWeight === '700') {

        let orderId = selectedOption.getAttribute('data-order-id');
        fetch('/buyer/ClickedByBuyer', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ orderId: orderId })
        }).then(() => {
            selectedOption.style.fontWeight = 'normal';

            let spanElement = document.querySelector('span');
            let currentValue = parseInt(spanElement.textContent);
            spanElement.textContent = currentValue - 1;
        });
    }
});

function showTable(selectedValue) {
    let tbodies = document.querySelectorAll('tbody');
    let tfoots = document.querySelectorAll('.tfoot');

    for (let i = 0; i < tbodies.length; i++) {
        let tbody = tbodies[i];

        if (tbody.classList.contains(selectedValue)) {
            tbody.style.display = 'table-row-group';
            tfoots[i].style.display = '';
        } else {
            tbody.style.display = 'none';
            tfoots[i].style.display = 'none';
        }
    }
}