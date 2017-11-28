$(document).ready(function () {
    $("input.search").each(function (index) {
        $(this).focusout(function () {

            $.ajax({
                url: "Customer/FindById", success: function (result) {
                    $("#div1").html(result);
                }
            });
        });
    });
    

}