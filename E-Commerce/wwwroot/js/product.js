



function addToCard(id) {
    console.log(id);
    $.post({
        url: "/Product/addCard/" + id,
        success: function (result) {
            
        }
    });
}
