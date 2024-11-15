// Logout
function uLogout() {
    debugger;
    $.ajax({
        "url": "/Home/Logout"
    }).then((result) => {
        window.location.href = '/'
    })
}

// Questions

$(function () {
    debugger;
    var q = $("#questions").DataTable({
        "columnDefs": [
            {
                "searchable": false,
                "orderable": false,
                "targets": 0
            },
            {
                "orderable": false,
                "targets": 4
            }
        ],
        "order": [[1, 'asc']]
    })
    q.on('order.dt search.dt', function () {
        q.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function qClearScreen() {
    $("#agreementNumber").val('');
    $("#branchId").val('');
    $("#noBPKB").val('');
    $("#bpkbInDate").val('null');
    $("#bpkbDate").val('null');
    $("#noFaktur").val('');
    $("#fakturDate").val('null');
    $("#noPolisi").val('');
    $("#locationText").val('null');
    $("#locationText").trigger('change');
    $("#qSave").show();
}

function qSave() {
    debugger;
    var transaction = new Object();
    transaction.agreement_number = $("#agreementNumber").val();
    transaction.bpkb_no = $("#noBPKB").val();
    transaction.branch_id = $("#branchId").val();
    transaction.bpkb_date = $("#bpkbDate").val();
    transaction.faktur_no = $("#noFaktur").val();
    transaction.faktur_date = $("#fakturDate").val();
    transaction.location_id = $("#locationText").find("option:selected").attr("id");
    transaction.police_no = $("#noPolisi").val();
    transaction.bpkb_date_in = $("#bpkbInDate").val();
    $.ajax({
        "url": "/Transaction/CreateTransaction",
        "type": "POST",
        "dataType": "json",
        "data": transaction
    }).then((result) => {
        if (result.data.statusCode === 200) {
            Swal.fire({
                icon: 'success',
                title: 'Your data has been saved',
                text: 'Success!'
            }).then((hasil) => {
                location.reload();
            });
            $("#qst-modal").modal("hide");
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Your data not saved',
                text: 'Failed!'
            })
        }
    })
}