var selectElement = document.querySelector('#mySelect');
showTable(selectElement.value);

selectElement.addEventListener('change', (event) => {
    showTable(selectElement.value);

    let selectedOption = event.target.selectedOptions[0];
    let fontWeight = window.getComputedStyle(selectedOption).getPropertyValue('font-weight');
    if (fontWeight === '700') {

        let orderId = selectedOption.getAttribute('data-order-id');
        fetch('/admin/ClickedByAdmin', {
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
    let tfoots = document.querySelectorAll('div.send');

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

function moveToDatabase() {
    window.location.href = "/database/viewEngineers";
}

function sendToDatabase(tableNumber, orderId) {
    let feedbacks = document.querySelectorAll(`tbody.table${tableNumber} input`);
    let teamId = document.querySelector(`.tfoot${tableNumber} input`);

    let data = {};
    data.TeamId = teamId.value;
    data.OrderId = orderId;
    data.Feedbacks = [];

    for (var i = 0; i < feedbacks.length; i++) {
        let value = feedbacks[i].value;
        data.Feedbacks.push({ name: value });
    }

    fetch('/admin/ReceivedOrders', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
}