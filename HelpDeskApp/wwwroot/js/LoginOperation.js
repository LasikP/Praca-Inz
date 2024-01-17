var LoginAction = function () {
    if (!$("#frmLogin").valid()) {
        return;
    }

    var _frmLogin = $("#frmLogin").serialize();
    $("#btnUserLogin").LoadingOverlay("show", {
        background: "rgba(165, 190, 100, 0.5)"
    });
    $("#btnUserLogin").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: _frmLogin,
        success: function (result) {
            if (result.IsSuccess) {
                location.href = "/Dashboard/Index";
            }
            else {
                toastr.error(result.AlertMessage);
                $("#btnUserLogin").LoadingOverlay("hide", true);
            }
        },
        error: function (errormessage) {
            $("#btnUserLogin").LoadingOverlay("hide", true);
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var SwalSimpleAlert = function (Message, icontype) {
    Swal.fire({
        title: Message,
        icon: icontype
    });
}