var Details = function (id) {
    var url = "/ComplaintStatus/Details?id=" + id;
    $('#titleBigModal').html("Szczeg�y zg�oszenia");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/ComplaintStatus/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edytuj szczeg�y zg�oszenia");
    }
    else {
        $('#titleMediumModal').html("Dodaj szczeg�y zg�oszenia");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Czy chcesz usun�� ten element?',
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
