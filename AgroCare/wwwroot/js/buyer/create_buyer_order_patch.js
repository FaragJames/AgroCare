let openShopping = document.querySelector('.shopping');
let closeShopping = document.querySelector('.closeShopping');
let body = document.querySelector('body');
var listItems = document.querySelectorAll('li');

openShopping.addEventListener('click', () => {
    body.classList.add('active');
});
closeShopping.addEventListener('click', () => {
    body.classList.remove('active');
});


// Store the initial values of the input fields
var initialValues = {};
listItems.forEach(function (listItem) {
    var itemId = listItem.querySelector('#itemId').value;
    initialValues[itemId] = {};

    var inputs = listItem.querySelectorAll('input');
    inputs.forEach(function (input) {
        initialValues[itemId][input.name] = input.value;
    });
});

// Listen for the submit event on the form
document.querySelector('form').addEventListener('submit', function (event) {
    // Prevent the default form submission behavior
    event.preventDefault();

    // Initialize the changedFields object
    var changedFields = {};

    // Check which input fields have changed
    listItems.forEach(function (listItem) {
        var itemId = listItem.querySelector('#itemId').value;
        changedFields[itemId] = [];

        var inputs = listItem.querySelectorAll('input');
        inputs.forEach(function (input) {
            if (input.value !== initialValues[itemId][input.name]) {
                changedFields[itemId].push({
                    "op": "replace",
                    "path": input.name,
                    "value": input.value
                });
            }
        });
    });

    fetch(`/buyer/createorder/${orderId}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: changedFields
    }).then(function (response) {
        response.json().then(function (data) {
            window.location.href = '/buyer/showpendingorders';
        });
    });
});



function changeQuantity(key, quantity) {
    var listItem = document.querySelector(`#listItem${key}`);

    var divCounter = listItem.querySelector('div.count');
    divCounter.textContent = quantity;

    var inputCounter = listItem.querySelector('input.count');
    inputCounter.value = quantity;

    var kiloPrice = parseFloat(listItem.querySelector('#kiloPrice').value);
    listItem.querySelector('.total-kilo-price').textContent = kiloPrice * quantity;
    refreshCard();
}
function refreshCard() {
    var total = 0;

    listItems.forEach(function (listItem) {
        total += listItem.querySelector('.total-kilo-price').textContent;
    });

    document.querySelector('.total').textContent = total;
}