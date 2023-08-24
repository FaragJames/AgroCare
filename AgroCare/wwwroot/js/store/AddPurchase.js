let Item = document.getElementById('Item');
let price = document.getElementById('price');
let quantity = document.getElementById('quantity');
let details = document.getElementById('details');
let IsRented = document.getElementById('IsRented');
let add = document.getElementById('add');
let PurchaseDetailsViews = [];
let errorDiv = document.getElementById("errors");

if (localStorage.product != null) {
    PurchaseDetailsViews = JSON.parse(localStorage.value);
} else {
    PurchaseDetailsViews = [];
}
add.onclick = function () {
    errorDiv.style.display = "none";
    let newpro =
    {
        Item: Item.value.trim(),
        price: price.value,
        quantity: parseInt(quantity.value),
        details: details.value.trim(),
        IsRented: IsRented.value
    }
    let message = "";
    if (newpro.Item == "") {
        message += "Please enter an item name.<br>";
    }
    if (isNaN(document.getElementById("price").value) || !document.getElementById("price").value) {
        message += "Not valid price.<br>";
    }
    if (!Number.isInteger(newpro.quantity)) {
        message += "Not valid quantity.<br>";
    }

    if (document.getElementById("IsRented").value == "") {
        message += "Select Buying or Renting for IsRented.<br>";
    }
    if (message != "") {

        errorDiv.innerHTML = "Please enter invalid input..!<br>" + message;
        errorDiv.style.color = "red";
        errorDiv.style.border = "1px solid black";
        errorDiv.style.padding = "10px";
        errorDiv.style.padding = "10px";
        errorDiv.style.display = "block"; // show the divreturn; 
        return;
    } else {
        PurchaseDetailsViews.push(newpro);
        showdata()
        saveButton.disabled = false;
        errorDiv.style.display = "none"; // hide the div
    }
}
function showdata() {

    let table = '';
    for (let i = 0; i < PurchaseDetailsViews.length; i++) {
        table += `
      <tr>
      <td> ${PurchaseDetailsViews[i].Item}  </td>
      <td>${PurchaseDetailsViews[i].price}   </td>
      <td> ${PurchaseDetailsViews[i].quantity}  </td>
      <td> ${PurchaseDetailsViews[i].details}    </td>
      <td> ${PurchaseDetailsViews[i].IsRented}   </td>
      </tr>
      `
    }
    document.getElementById('tbody').innerHTML = table;
}
function saveData() {
    let FarmerId = document.getElementById("farmerId").value;
    let Code = document.getElementById("code").value;

    let purchaseAllDataView =
        { "purchaseDetailsViews": PurchaseDetailsViews, "FarmerId": FarmerId, "Code": Code };
    if (FarmerId && Code) {
        $.ajax
            ({
                type: "POST", url: "/Store/CreatePurchase",
                data: purchaseAllDataView,
                dataType: "json",
                async: false,
                success: function (response) {
                    window.location.replace = "/Store/ShowPurchases";
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseJSON.error);
                }
            });
    }
}

let saveButton = document.getElementById("save");
saveButton.onclick = saveData;