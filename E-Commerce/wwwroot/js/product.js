
//let products = JSON.parse(localStorage.getItem("cartItems")) ?? [];
//function addToCard(id) {
    
//    let success = document.getElementById("sucess_" + id);
//    let product = { product_id: id, quantity: 1 };
//    var isFound = products.find(val => val.product_id == product.product_id);
//    if (isFound) {


//    } else {
//        products.push(product);
//        getAllCartItems();
//            localStorage.setItem("cartItems", JSON.stringify(products));
//        }
//        counter.innerHTML = products.length;
//        success.style.display = "block";

    
//}

//function getProductById(id) {
//    $.ajax({
//        type: 'Get',
//        dataType: 'json',
//        data: { id: id},
//        url: "/Product/getById",
//        success: function (result) {
//            return result;
//        }
//}

//let Favorites = JSON.parse(localStorage.getItem("favoriteItem")) ?? [];
//function addToFavorite(id) {
//    let success = document.getElementById("sucess_" + id);
//    let Favorite = { product_id: id};
//    var isFound = Favorites.find(val => val.product_id == Favorite.product_id);
//    if (isFound) {


//    } else {
//        Favorites.push(Favorite);
//        getAllFavorite();
//        localStorage.setItem("favoriteItem", JSON.stringify(Favorites));
//    }
//    success.style.display = "block";
//    setFavoriteCounter();
//}

