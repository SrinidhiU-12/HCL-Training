
let students = [];

function addStudent(event) {

    event.preventDefault();

    let name = document.getElementById("name").value.trim();
    let regno = document.getElementById("regno").value.trim();
    let department = document.getElementById("department").value.trim();
    let year = document.getElementById("year").value;

    // Check duplicate register number

    for (let i = 0; i < students.length; i++) {

        if (students[i].regno === regno) {
            alert("Register number already exists");
            return;
        }

    }

    // Create student object

    let student = {
        name: name,
        regno: regno,
        department: department,
        year: year
    };

    // Add student to array

    students.push(student);

    // Clear the form

    document.getElementById("name").value = "";
    document.getElementById("regno").value = "";
    document.getElementById("department").value = "";
    document.getElementById("year").value = "";

    // Display updated student list

    displayStudents();

    alert("Student added successfully");

}


// View Students

function displayStudents() {

    let table = document.getElementById("studentTable");

    table.innerHTML = "";

    // Check whether the list is empty

    if (students.length === 0) {

        table.innerHTML =
            "<tr><td colspan='6'>No student records found</td></tr>";

        return;
    }

    // Loop through the student list

    for (let i = 0; i < students.length; i++) {

        let row = document.createElement("tr");

        let values = [
            i + 1,
            students[i].name,
            students[i].regno,
            students[i].department,
            students[i].year
        ];

        // Create table cells

        for (let j = 0; j < values.length; j++) {

            let cell = document.createElement("td");

            cell.textContent = values[j];

            row.appendChild(cell);

        }

        // Create Delete button

        let actionCell = document.createElement("td");

        let deleteButton = document.createElement("button");

        deleteButton.textContent = "Delete";

        deleteButton.className = "delete-button";

        deleteButton.onclick = function () {
            deleteStudent(i);
        };

        actionCell.appendChild(deleteButton);

        row.appendChild(actionCell);

        table.appendChild(row);

    }

}


// Delete Student

function deleteStudent(index) {

    let confirmDelete = confirm(
        "Are you sure you want to delete this student?"
    );

    if (confirmDelete) {

        students.splice(index, 1);

        displayStudents();

        alert("Student deleted successfully");

    }

}


// Display the empty table when the page opens

displayStudents();
