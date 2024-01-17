var Details = function (id) {
    var url = "/AttachmentFile/Details?id=" + id;
    $('#titleMediumModal').html("Szczeg�y za��cznik�w");
    loadMediumModal(url);
};

var DownloadFile = function (id) {
    location.href = "/Complaint/DownloadFile?id=" + id;
};


var AddEdit = function (id) {
    var url = "/AttachmentFile/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edytuj za�acznik");
    }
    else {
        $('#titleMediumModal').html("Dodaj za��cznik");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Czy chcesz usun�� ten element?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/AttachmentFile/Delete?id=" + id,
                success: function (result) {
                    var message = "Za��cznik zosta� usuni�ty. ID za��cznika: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Deleted!',
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};
