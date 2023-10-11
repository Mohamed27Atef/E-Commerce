



function addToCard(id) {
    console.log(id);

        $.ajax(
    {
        url: "/Product/addCard/" + id,
    success: function (result) {
        console.log(result);
   
                    }
                });
        }
