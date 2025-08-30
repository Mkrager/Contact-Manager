function editRow(button) {
    var row = button.closest("tr");
    var tds = row.querySelectorAll("td");

    row.dataset.oldValues = JSON.stringify({
        Name: tds[0].innerText.trim(),
        DateOfBirth: tds[1].innerText.trim(),
        Married: tds[2].innerText.trim(),
        Phone: tds[3].innerText.trim(),
        Salary: tds[4].innerText.trim()
    });

    for (var i = 0; i < tds.length - 1; i++) {
        var value = tds[i].innerText.trim();
        if (i === 1) {
            tds[i].innerHTML = `<input type="date" value="${formatForInputDate(value)}" />`;
        }
        else if (i === 2) {
            tds[i].innerHTML = `
                <select>
                    <option value="Yes" ${value === "Yes" ? "selected" : ""}>Yes</option>
                    <option value="No" ${value === "No" ? "selected" : ""}>No</option>
                </select>
            `;
        }
        else {
            tds[i].innerHTML = `<input type="text" value="${value}" />`;
        }
    }

    var actionsCell = tds[tds.length - 1];
    actionsCell.innerHTML = `
        <div class="edit-actions">
            <button class="btn btn-save">Save</button>
            <button class="btn btn-cancel">Cancel</button>
        </div>
    `;

    actionsCell.querySelector(".btn-save").onclick = function () {
        saveRow(row);
    };
    actionsCell.querySelector(".btn-cancel").onclick = function () {
        cancelEdit(row);
    };
}

function saveRow(row) {
    var tds = row.querySelectorAll("td");

    var name = tds[0].querySelector("input")?.value || tds[0].innerText.trim();
    var dateInput = tds[1].querySelector("input")?.value || tds[1].innerText.trim();
    var marriedInput = tds[2].querySelector("select");
    var marriedValue = marriedInput ? marriedInput.value.toLowerCase() === "yes" : false;
    var phone = tds[3].querySelector("input")?.value || tds[3].innerText.trim();
    var salary = parseFloat((tds[4].querySelector("input")?.value || tds[4].innerText).replace(/[^0-9.-]+/g, ""));

    var contact = {
        Id: row.dataset.id,
        Name: name,
        DateOfBirth: new Date(dateInput).toISOString(),
        Married: marriedValue,
        Phone: phone,
        Salary: salary
    };

    updateCourse(contact);

    tds[0].textContent = contact.Name;
    tds[1].textContent = formatForDisplayDate(contact.DateOfBirth);
    tds[2].innerHTML = `<span class="badge ${contact.Married ? "badge-yes" : "badge-no"}">${contact.Married ? "Yes" : "No"}</span>`;
    tds[3].textContent = contact.Phone;
    tds[4].textContent = contact.Salary.toLocaleString("en-US", { style: "currency", currency: "USD" });

    var actionsCell = tds[tds.length - 1];
    actionsCell.innerHTML = `
        <button class="btn btn-edit">Edit</button>
        <button class="btn btn-delete">Delete</button>
    `;
    actionsCell.querySelector(".btn-edit").onclick = function () { editRow(this); };
    actionsCell.querySelector(".btn-delete").onclick = function () { deleteContact(row.dataset.id); };
}

function cancelEdit(row) {
    var tds = row.querySelectorAll("td");
    var oldValues = JSON.parse(row.dataset.oldValues);

    tds[0].textContent = oldValues.Name;
    tds[1].textContent = oldValues.DateOfBirth;
    tds[2].innerHTML = `<span class="badge ${oldValues.Married === "Yes" ? "badge-yes" : "badge-no"}">${oldValues.Married}</span>`;
    tds[3].textContent = oldValues.Phone;
    tds[4].textContent = oldValues.Salary;

    var actionsCell = tds[tds.length - 1];
    actionsCell.innerHTML = `
        <button class="btn btn-edit">Edit</button>
        <button class="btn btn-delete">Delete</button>
    `;
    actionsCell.querySelector(".btn-edit").onclick = function () { editRow(this); };
    actionsCell.querySelector(".btn-delete").onclick = function () { deleteContact(row.dataset.id); };
}

function formatForInputDate(dateStr) {
    var parts = dateStr.split(".");
    if (parts.length !== 3) return "";
    return `${parts[2]}-${parts[1].padStart(2, "0")}-${parts[0].padStart(2, "0")}`;
}

function formatForDisplayDate(isoDateStr) {
    var date = new Date(isoDateStr);
    var day = date.getDate().toString().padStart(2, "0");
    var month = (date.getMonth() + 1).toString().padStart(2, "0");
    var year = date.getFullYear();
    return `${day}.${month}.${year}`;
}

function updateCourse(contact) {
    $.ajax({
        url: '/contact/update',
        type: 'PUT',
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(contact),
        success: function (response) {
            if (response.redirectToUrl) {
                window.location.href = response.redirectToUrl;
            } else {
                for (var key in response.errors) {
                    var errorMessage = response.errors[key];
                    $('#' + key).text(errorMessage);
                }
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
}