let products = [];

let productInput = document.getElementById("product");
let priceInput = document.getElementById("price");

function addProduct(event) {
    if (event.key == "Enter") {

        let product = productInput.value;
        let price = priceInput.value;

        if (product != "" && price != "") {

            products.push({
                Product: product,
                Price: price
            });

            showTable();

            productInput.value = "";
            priceInput.value = "";
        }
    }
}

productInput.addEventListener("keydown", addProduct);
priceInput.addEventListener("keydown", addProduct);

function showTable() {

    let html = "<table border='1'>";

    html = html + "<tr>";
    html = html + "<th>Товар</th>";
    html = html + "<th>Ціна</th>";
    html = html + "</tr>";

    products.forEach(function(item) {

        html = html + "<tr>";
        html = html + "<td>" + item.Product + "</td>";
        html = html + "<td>" + item.Price + " грн</td>";
        html = html + "</tr>";

    });

    html = html + "</table>";

    document.getElementById("result").innerHTML = html;
}