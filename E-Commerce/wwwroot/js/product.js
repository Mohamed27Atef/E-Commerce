//let counter = document.getElementById("counter");
//counter.innerHTML = products.length;

function addToCard(id) {
    let products = JSON.parse(localStorage.getItem("cartItems")) ?? [];
    let success = document.getElementById("sucess_" + id);
    let product = { product_id: id, quantity: 1 };
    var isFound = products.find(val => val.product_id == product.product_id);
    if (isFound) {

    } else {
        products.push(product);
        localStorage.setItem("cartItems", JSON.stringify(products));
    }
    counter.innerHTML = products.length;
    success.style.display = "block";





}




    //$.ajax({
    //    url: "/Product/getbyid/" + id,
    //    success: function (result) {

    //    }
    //});


