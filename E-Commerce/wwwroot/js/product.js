//let counter = document.getElementById("counter");
//counter.innerHTML = products.length;

let products = JSON.parse(localStorage.getItem("cartItems")) ?? [];
function addToCard(id) {
    console.log("Not   Authorize");

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

    
}

    




function addToCardAuthorize(id){
    console.log("Authorize id= "+id);
    $.ajax({
        type: "get",
        url: "/Home/AddProductToDB/"+id,
        data: id,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            console.log("1111111111111111111111111");
            localStorage.clear();
        },
        error: function (error) {

            console.error(error);
        }
    });
}


//atef
let Favorites = JSON.parse(localStorage.getItem("favoriteItem")) ?? [];
function addToFavorite(id) {
    let success = document.getElementById("sucess_" + id);
    let Favorite = { product_id: id};
    var isFound = Favorites.find(val => val.product_id == Favorite.product_id);
    if (isFound) {


    } else {
        Favorites.push(Favorite);
        getAllFavorite();
        localStorage.setItem("favoriteItem", JSON.stringify(Favorites));
    }
    success.style.display = "block";
    setFavoriteCounter();
}


function getAllFavorite() {
    console.log("eisa");
    let Favorites = JSON.parse(localStorage.getItem("favoriteItem")) ?? [];
    const sideBarCardItem = document.getElementById("favorite-items");
    sideBarCardItem.innerHTML = "";
    Favorites.map(item => {
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
                                <button class="btn btn-danger"  onclick='removefromlocalstorage(${result.id})'>Remove</button>
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



    //$.ajax({
    //    url: "/Product/getbyid/" + id,
    //    success: function (result) {




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
                                <button class="btn btn-danger"  onclick='removefromlocalstorage(${result.id})'>Remove</button>
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


