// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "language": {
            "emptyTable": "No data available in table",
            "info": "Showing _START_ to _END_ of _TOTAL_ entries",
            "infoEmpty": "Showing 0 to 0 of 0 entries",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Show _MENU_ entries per page",
            "loadingRecords": "Loading...",
            "processing": "Processing...",
            "zeroRecords": "No matching records found",
            "search": "Search",
            "paginate": {
                "next": "Next",
                "previous": "Previous",
                "first": "First",
                "last": "Last"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });
}

// Now you can use the function to initialize the DataTable on any element
getDatatable('#table-contacts');
getDatatable('#table-users');

//on click open modal with all contacts for the selected user
$('.btn-total-contacts').click(function () {
    var userId = $(this).attr('user-id');

    $.ajax({
        type:'GET',
        url: '/User/ListContactsByUserId/' + userId,
        success: function (result) {
            $('#userContactList').html(result);
            $('#userContactsModal').modal('show');
            getDatatable('#table-contacts-user');
        }
    });  
});

$(document).on('click', '.close-alert', function () {
    $('.alert').hide();
});


