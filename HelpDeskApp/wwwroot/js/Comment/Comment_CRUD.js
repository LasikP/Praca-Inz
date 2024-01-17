var Details = function (id) {
    var url = "/Comment/Details?id=" + id;
    $('#titleMediumModal').html("Szczegó³y komentarza");
    loadMediumModal(url);
};

var AddEdit = function (id) {
    var url = "/Comment/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edytuj komentarz");
    }
    else {
        $('#titleMediumModal').html("Dodaj komentarz");
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
                url: "/Comment/Delete?id=" + id,
                success: function (result) {
                    var message = "Komentarz zosta³ usuniêty. ID komentarza: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Deleted!',
                        onAfterClose: () => {
                            //location.reload();
                            StatusUpdate(result.ComplaintId);
                        }
                    });
                }
            });
        }
    });
};
