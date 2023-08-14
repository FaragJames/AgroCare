let openShopping = document.querySelector('.shopping');
let closeShopping = document.querySelector('.closeShopping');
let list = document.querySelector('.list');
let listCard = document.querySelector('.listCard');
let body = document.querySelector('body');
let total = document.querySelector('.total');
let quantity = document.querySelector('.quantity');

openShopping.addEventListener('click', () => {
    body.classList.add('active');
});
closeShopping.addEventListener('click', () => {
    body.classList.remove('active');
});

function addToCard(id, name, price) {
    if (listCards[id] == null) {
        var item = {
            name: name,
            price: price,
            quantity: 1,
            date: '',
            totalPrice: price
        };
        listCards[id] = item;
    }
    reloadCard();
}

function reloadCard() {
    listCard.innerHTML = '';
    let count = 0;
    let totalPrice = 0;
    let index = 0;
    listCards.forEach((value, key) => {
        if (value != null) {
            totalPrice = totalPrice + value.totalPrice;
            count = count + value.quantity;
            let newDiv = document.createElement('li');
            newDiv.innerHTML = `
                    <div><img src=""/></div>
                    <div>${value.name}</div>
                    <input type="date" value="${value.date}" class="date" id="deliveryDate${index}" name="items[${index}].deliveryDate" onchange="saveDate(${key}, ${index})">
                    <input type="number" id="itemId" name="items[${index}].itemId" value="${key}" hidden>
                    <input type="number" id="kiloPrice" name="items[${index}].kiloPrice" value="${value.price}" hidden>
                    <div>${value.totalPrice.toLocaleString()}</div>
                    <div>
                        <button onclick="changeQuantity(${key}, ${value.quantity - 1})">-</button>
                        <input type="number" name="items[${index}].kilos" id="kilos" value="${value.quantity}" hidden>
                        <div class="count">${value.quantity}</div>
                        <button onclick="changeQuantity(${key}, ${value.quantity + 1})">+</button>
                    </div>`;
            listCard.appendChild(newDiv);
            index++;
        }
    });
    total.innerText = totalPrice.toLocaleString();
    quantity.innerText = count;
}

function changeQuantity(key, quantity) {
    if (quantity == 0) {
        delete listCards[key];
    } else {
        listCards[key].quantity = quantity;
        listCards[key].totalPrice = quantity * listCards[key].price;
    }
    reloadCard();
}
function saveDate(key, index) {
    listCards[key].date = document.querySelector(`#deliveryDate${index}`).value;
}

reloadCard();