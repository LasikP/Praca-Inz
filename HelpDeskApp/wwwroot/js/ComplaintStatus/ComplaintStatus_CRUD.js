var Details = function (id) {
    var url = "/ComplaintStatus/Details?id=" + id;
    $('#titleBigModal').html("Szczegó³y zg³oszenia");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/ComplaintStatus/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edytuj szczegó³y zg³oszenia");
    }
    else {
        $('#titleMediumModal').html("Dodaj szczegó³y zg³oszenia");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Czy chcesz usun¹æ ten element?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/ComplaintStatus/Delete?id=" + id,
                success: function (result) {
                    var message = "Complaint Status has been deleted successfully. Complaint Status ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblComplaintStatus').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
