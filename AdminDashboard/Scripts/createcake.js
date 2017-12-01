$(document).ready(function () {
    $("div.cake-button").each(function (index) {
        $(this).click(function (e) {
            if (!$(this).hasClass("active")) {
                if (this.id == "exists-image") {
                    $("#new-image").removeClass("active");
                    ajaxExistsImage();
                    $(this).addClass("active");
                }
                else if(this.id == "new-image")
                {
                    $("#exists-image").removeClass("active");
                    ajaxNewImage();
                    $(this).addClass("active");
                }

            }
        });
    });

    var ajaxExistsImage = function () {
        var data = "rows=10&page=1";
        $.ajax({
            data: data,
            url: "/Image/PagesData",
            success: function (result) {
                $("#update").html(result);
            }
        });
    }

    var ajaxNewImage = function () {
        $.ajax({
            url: "/Image/UploadImageModel",
            success: function (result) {
                $("#update").html(result);
            }
        });
    }
});

var changeParentColor = function (e) {
    $('#update').find('input[type="radio"]').each(function () {
        var ischecked = $(this).is(":checked");
        if (!ischecked && $(this).parent().parent().hasClass("green-back")) {
            $(this).parent().parent().removeClass("green-back");
        }
        else if (ischecked) {
            $(this).parent().parent().addClass("green-back");
        }
    });
}