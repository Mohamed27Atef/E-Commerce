//let counter = document.getElementById("counter");
//counter.innerHTML = products.length;

function addToCard(id) {
    let products = JSON.parse(localStorage.getItem("cartItems")) ?? [];
    let success = document.getElementById("sucess_" + id);
    let product = { product_id: id, quantity: 1 };
    var isFound = products.find(val => val.product_id == product.product_id);
    if (isFound) {

function addToCard(id) {
    console.log(id);
    var cartCounter = document.getElementById("Cart-counter");
        $.ajax(
    {
        url: "/Product/addCard/" + id,
    success: function (result) {
        console.log(result);
        console.log(cartCounter);

                    }
                });

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

    function addToFavorites(id) {
        $.ajax({
            type: "POST",
            url: "/Product/AddToFavorites/" + id,
            success: function (result) {
                if (result.success) {
                    addProductToFavoritesLocalStorage(id);
                    console.log("Product added to favorites on the server:", result);
                } else {
                    console.error("Error adding product to favorites on the server:", result.error);
                }
            },
            error: function (error) {
                console.error("Error adding product to favorites on the server:", error);
            }
        });
    }

    function addProductToFavoritesLocalStorage(productId) {
        let favorites = JSON.parse(localStorage.getItem("favorites")) || [];

        if (!favorites.includes(productId)) {
            favorites.push(productId);
            localStorage.setItem("favorites", JSON.stringify(favorites));
            console.log("Product added to local storage favorites:", productId);
        }
    }

    function getFavoritesFromLocalStorage() {
        return JSON.parse(localStorage.getItem("favorites")) || [];
    }

//let favoriteProducts = getFavoritesFromLocalStorage();
//console.log("Favorite products from local storage:", favoriteProducts);

