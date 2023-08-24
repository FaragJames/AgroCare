let ActionId = document.getElementById('ActionId');
let EstimatedStartDate = document.getElementById('EstimatedStartDate');
let EstimatedFinishDate = document.getElementById('EstimatedFinishDate');
let AgriculturalItemId = document.getElementById('AgriculturalItemId');
let Quantity1 = document.getElementById('Quantity1');

let add = document.getElementById('add');
let errorDiv = document.getElementById("errors");


let planStepDetailsView = [];

if (localStorage.product != null) {
    planStepDetailsView = JSON.parse(localStorage.product)
} else {
    planStepDetailsView = [];
}

add.onclick = function () {
    errorDiv.style.display = "none";
    let newpro =
    {
        ActionId: ActionId.value,
        EstimatedStartDate: EstimatedStartDate.value,
        EstimatedFinishDate: EstimatedFinishDate.value,
        Quantity1: parseInt(Quantity1.value),
        AgriculturalItemId: AgriculturalItemId.value
    }
    let message = "";
    if (newpro.EstimatedFinishDate == "") {
        message += "Please choose finish date.<br>";
    }
    if (newpro.EstimatedStartDate == "") {
        message += "Please choose start date.<br>";
    }
    if (!compare(newpro.EstimatedStartDate, newpro.EstimatedFinishDate)) {
        message += "It not valid to put finish date after start date.<br>";
    }

    if (!Number.isInteger(newpro.Quantity1)) {
        message += "Not valid quantity.<br>";
    }

    if ($("#ActionId").val() == "") {
        message += "Please select an Action step.<br>";
    }
    if ($("#AgriculturalItemId").val() == "") {
        message += "Please select an Agricultural Item.<br>";
    }
    if (message != "") {

        errorDiv.innerHTML = "Please enter valid input..!<br>" + message;
        errorDiv.style.color = "red";
        errorDiv.style.padding = "10px";
        errorDiv.style.padding = "10px";
        errorDiv.style.display = "block"; // show the divreturn; 
        return;
    } else {
        planStepDetailsView.push(newpro);
        showdata()
        saveButton.disabled = false;
        errorDiv.style.display = "none"; // hide the div
    }
}
function compare(startDt, endDt) {
    if ((new Date(startDt) < new Date(endDt))) {
        return true;
    } else if ((new Date(startDt) > new Date(endDt))) {
        return false;
    }
}
function showdata() {

    let table = '';
    for (let i = 0; i < planStepDetailsView.length; i++) {
        table += `
      <tr>
      <td> ${planStepDetailsView[i].ActionId}  </td>
      <td>${planStepDetailsView[i].EstimatedStartDate}   </td>
      <td> ${planStepDetailsView[i].EstimatedFinishDate}  </td>
      <td> ${planStepDetailsView[i].AgriculturalItemId}    </td>
      <td> ${planStepDetailsView[i].Quantity1}   </td>
      </tr>
      `
    }
    document.getElementById('tbody').innerHTML = table;
}
function saveData() {
    let OrderDetailId = document.getElementById('OrderDetailId').value;
    let StartDate = document.getElementById('StartDate').value;
    let FinishDate = document.getElementById('FinishDate').value;
    let LandId = document.getElementById('LandId').value;
    let Quantity = document.getElementById('Quantity').value;

    let planViewModel =
    {
        "planStepDetailsView": planStepDetailsView,
        "OrderDetailId": OrderDetailId,
        "StartDate": StartDate,
        "FinishDate": FinishDate,
        "LandId": LandId,
        "Quantity": Quantity
    };
    if (compare(StartDate, FinishDate) && LandId && OrderDetailId && Quantity) {
        $.ajax
            ({
                type: "POST", url: "/Executive/CreatePlan",
                data: planViewModel,
                dataType: "json",
                async: false,
                success: function (response) {
                    window.location.replace = response.url;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseJSON.error);
                }
            });
    }

}

let saveButton = document.getElementById("save");
saveButton.onclick = saveData;
