

document.getElementById("myForm").addEventListener("submit", function(event) {
    event.preventDefault();

    let person = {
        name: document.getElementById("name").value,
        year: document.getElementById("year").value
    };

    console.log(person);
});