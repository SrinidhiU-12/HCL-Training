
/* Selected product category */
let selectedCategory = "All";

/* Product enquiry button */
function enquire(product) {
    window.location.href =
        "enquire.html?product=" + encodeURIComponent(product);
}

/* Filter products by category */
function filterCategory(category, button) {
    selectedCategory = category;

    document.querySelectorAll(".filter-btn").forEach(function (btn) {
        btn.classList.remove("selected");
    });

    button.classList.add("selected");

    filterProducts();
}

/* Search products and apply category filter */
function filterProducts() {
    let search = document.getElementById("searchInput");

    if (!search) {
        return;
    }

    let keyword = search.value.toLowerCase();
    let products = document.querySelectorAll(".product-item");
    let count = 0;

    products.forEach(function (item) {
        let name = item.querySelector("h5").textContent.toLowerCase();
        let category = item.dataset.category;

        let matchesSearch = name.includes(keyword);
        let matchesCategory =
            selectedCategory === "All" ||
            category === selectedCategory;

        if (matchesSearch && matchesCategory) {
            item.style.display = "";
            count++;
        } else {
            item.style.display = "none";
        }
    });

    document.getElementById("noResults").hidden = count !== 0;
}

/* Contact and enquiry forms */
function handleForm(event, type) {
    event.preventDefault();

    if (type === "contact") {
        let name = document.getElementById("contactName").value.trim();
        let message = document.getElementById("contactSuccess");

        message.textContent =
            "Thank you, " + name + "! Your message has been recorded.";
        message.hidden = false;
    }

    if (type === "enquiry") {
        let name = document.getElementById("enquiryName").value.trim();
        let product = document.getElementById("enquiryProduct").value;
        let message = document.getElementById("enquirySuccess");

        message.textContent =
            "Thank you, " + name +
            "! Your enquiry for " + product + " has been recorded.";
        message.hidden = false;
    }

    event.target.reset();
}

/* Select the product from the product card */
document.addEventListener("DOMContentLoaded", function () {
    let params = new URLSearchParams(window.location.search);
    let selectedProduct = params.get("product");
    let productSelect = document.getElementById("enquiryProduct");

    if (selectedProduct && productSelect) {
        productSelect.value = selectedProduct;
    }

    /* Set the All category button as selected */
    let allButton = document.querySelector(
        '.filter-btn[onclick*="All"]'
    );

    if (allButton) {
        allButton.classList.add("selected");
    }
});
