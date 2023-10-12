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

    $.get({

        url: "/Product/getById/" + 1002, // Replace with the appropriate URL
        success: function (result) {
            // Handle the result from the AJAX request for each ID
            console.log(result);
            // You can add your logic here to process the result for each ID
        },
        error: function (error) {
            // Handle errors, if any
            console.error("Error:", error);
        }
    });



}




    //$.ajax({
    //    url: "/Product/getbyid/" + id,
    //    success: function (result) {

    //    }
    //});


