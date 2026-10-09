let products = {

1: {
    name: "Groceries",
    cost: 100
},

2: {
    name: "Makeup",
    cost: 250
},

3: {
    name: "Stationery",
    cost: 50
},

4: {
    name: "Clothing",
    cost: 500
},

5: {
    name: "Electronics",
    cost: 1000
}


};

let grandTotal = 0;

function generateBill() {


// Get product number

let productNo = document.getElementById("product").value;


// Get quantity

let quantity = Number(
    document.getElementById("quantity").value
);


// Check product

if (productNo == "") {

    document.getElementById("message").innerHTML =
        "Please select a product";

    return;
}


// Check quantity

if (quantity <= 0) {

    document.getElementById("message").innerHTML =
        "Please enter a valid quantity";

    return;
}


// Get product details

let product = products[productNo];


// Calculate total

let total = product.cost * quantity;


// Calculate grand total

grandTotal = grandTotal + total;


// Display product in table

document.getElementById("bill").innerHTML +=

    "<tr>" +

    "<td>" + productNo + "</td>" +

    "<td>" + product.name + "</td>" +

    "<td>" + quantity + "</td>" +

    "<td>Rs." + product.cost + "</td>" +

    "<td>Rs." + total + "</td>" +

    "</tr>";


// Display grand total

document.getElementById("grandTotal").innerHTML =
    "Grand Total: Rs." + grandTotal;


// Display message

document.getElementById("message").innerHTML =
    "Product added successfully";


// Clear input

document.getElementById("product").value = "";

document.getElementById("quantity").value = "";

}


/* ADD HERE */

function openGroceries() {
    window.location.href = "bill groceries.html";
}

/* END HERE */

/* ADD HERE */

function downloadPDF() {

    const { jsPDF } = window.jspdf;

    let doc = new jsPDF();

    doc.setFontSize(22);
    doc.text("BILL RECEIPT", 75, 20);

    doc.setFontSize(12);
    doc.text("Billing Generator", 80, 30);

    doc.line(20, 35, 190, 35);

    let y = 50;

    doc.text("No.", 20, y);
    doc.text("Product", 45, y);
    doc.text("Quantity", 100, y);
    doc.text("Cost", 135, y);
    doc.text("Total", 165, y);

    doc.line(20, y + 3, 190, y + 3);

    y = y + 12;

    let rows = document.querySelectorAll("#bill tr");

    rows.forEach(function(row) {

        let cells = row.querySelectorAll("td");

        doc.text(cells[0].innerText, 20, y);
        doc.text(cells[1].innerText, 45, y);
        doc.text(cells[2].innerText, 100, y);
        doc.text(cells[3].innerText, 135, y);
        doc.text(cells[4].innerText, 165, y);

        y = y + 10;
    });

    doc.line(20, y, 190, y);

    y = y + 12;

    doc.setFontSize(15);

    doc.text(
        document.getElementById("grandTotal").innerText,
        125,
        y
    );

    y = y + 20;

    doc.setFontSize(11);
    doc.text("Thank you for shopping!", 70, y);

    doc.save("Bill-Receipt.pdf");
}

/* END HERE */

/* ADD HERE */

window.onload = function() {

    let selectedProduct = localStorage.getItem("selectedProduct");
    let selectedPrice = localStorage.getItem("selectedPrice");

    if (selectedProduct && selectedPrice) {

        let productSelect = document.getElementById("product");

        productSelect.innerHTML +=
            '<option value="6">' +
            selectedProduct +
            '</option>';

        localStorage.removeItem("selectedProduct");
        localStorage.removeItem("selectedPrice");
    }

};

/* END HERE */