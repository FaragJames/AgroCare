//.details is a land.
//Testing something with this comment.
let items = document.querySelectorAll(".details");
items.forEach((ele) => {
    ele.onclick = () => {
        //.plan is all the steps.
        document.querySelectorAll(".plan").forEach((element) => {
            if (ele.id != element.id) {
                element.classList.remove("show");
            }
            if (ele.id === element.id) {
                element.classList.toggle("show");
            }
        });
    };
});

//.kind is a single step.
let kinds = document.querySelectorAll(".kind");
kinds.forEach((ele) => {
    ele.onclick = () => {
        kinds.forEach((element) => {
            if (ele != element) {
                element.classList.remove("expand");
            }
        });
        ele.classList.toggle("expand");
    };
});

document.querySelectorAll('input[type="checkbox"]').forEach((checkbox) => {
    checkbox.addEventListener('click', (event) => {

        //checkbox.checked means unchecked.
        if (!checkbox.checked) {
            alert('Already checked!');
            event.preventDefault();
        }
        else {
            let checkboxes = document.querySelectorAll(`input.${checkbox.className}`);
            let checked = true;
            for (let i = 0; i < checkboxes.length; i++) {
                if (checkbox == checkboxes[i]) {
                    break;
                }

                if (!checkboxes[i].checked) {
                    checked = false;
                    break;
                }
            }

            if (checked) {
                let confirmation = confirm('Are you sure you want to check this checkbox?');
                if (!confirmation) {
                    event.preventDefault();
                }
                else {
                    checkbox.parentElement.classList.add("yes");

                    let options = {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ stepId: checkbox.id })
                    };
                    fetch('farmer/checkstep', options);
                }
            }
            else {
                event.preventDefault();
                alert('Please check the previous checkbox first!');
            }
        }
    });
});

document.querySelectorAll('input[type="checkbox"]').forEach((checkbox) => {
    if (checkbox.checked) {
        checkbox.parentElement.classList.add("yes");
    }
})