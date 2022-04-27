$(document).ready(function () {
    /*-= Login Form Start =-*/

    $("#pCreate").validate({
        rules: {
            password: {
                minlength: 6
            }
        },
        errorElement: "label",
        errorClass: "help-small no-left-padding",
        errorPlacement: function (error, element) {
            element.parents("div.controls").append(error);
        }
    });

    $("#username").focus();

    /*-= Login Form End =-*/
});