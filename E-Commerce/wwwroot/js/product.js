

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
        }
