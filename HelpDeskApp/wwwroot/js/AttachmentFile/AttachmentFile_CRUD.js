var Details = function (id) {
    var url = "/AttachmentFile/Details?id=" + id;
    $('#titleMediumModal').html("Szczegó³y za³¹czników");
    loadMediumModal(url);
};

var DownloadFile = function (id) {
    location.href = "/Complaint/DownloadFile?id=" + id;
};


var AddEdit = function (id) {
    var url = "/AttachmentFile/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edytuj za³acznik");
    }
    else {
        $('#titleMediumModal').html("Dodaj za³¹cznik");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Czy chcesz usun¹æ ten element?',
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
                    var message = "Za³¹cznik zosta³ usuniêty. ID za³¹cznika: " + result.Id;
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
