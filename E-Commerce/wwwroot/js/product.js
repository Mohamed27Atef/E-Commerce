//let counter = document.getElementById("counter");
//counter.innerHTML = products.length;

let products = JSON.parse(localStorage.getItem("cartItems")) ?? [];
function addToCard(id) {
    let success = document.getElementById("sucess_" + id);
    let product = { product_id: id, quantity: 1 };
    var isFound = products.find(val => val.product_id == product.product_id);
    if (isFound) {


    } else {
        products.push(product);
        getAllCartItems();
            localStorage.setItem("cartItems", JSON.stringify(products));
        }
        counter.innerHTML = products.length;
        success.style.display = "block";

        //$.get({

        //    url: "/Product/getById/" + 1002, // Replace with the appropriate URL
        //    success: function (result) {
        //        // Handle the result from the AJAX request for each ID
        //        console.log(result);
        //        // You can add your logic here to process the result for each ID
        //    },
        //    error: function (error) {
        //        // Handle errors, if any
        //        console.error("Error:", error);

        //    }
        //});
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


function getAllCartItems() {
    const sideBarCardItem = document.getElementById("side-bar-crad-item");
    sideBarCardItem.innerHTML = "";
    products.map(item => {
        $.ajax({
            type: 'Get',
            dataType: 'json',
            data: { id: item.product_id },
            url: "/Product/getById",
            success: function (result) {
                sideBarCardItem.innerHTML += `
                <div class="sidebar-card-item">
                    <div class="card">
                        <div class="card-body">
                            <img src="/images/${result.image}" class="card-img-top" alt="Product Image">
                            <h5 class="card-title">${result.name}</h5>
                            <div class="price-rating">
                                <p class="card-text product-price">$${result.price}</p>
                                <button class="btn btn-danger">Remove</button>
                            </div>
                        </div>
                    </div>
                </div>
                `
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    });
}
