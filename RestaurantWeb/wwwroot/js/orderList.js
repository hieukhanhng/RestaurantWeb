var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled");
    } else {
        if (url.includes("completed")) {
            loadList("completed");
        } else {
            if (url.includes("ready")) {
                loadList("ready");
            } else {
                loadList("inprocess");
            }
        }
    }
});

function loadList(param) {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/Order?status="+param,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "id", "width": "10%"},
            {"data": "pickupName", "width": "15%"},
            {"data": "appUser.email", "width": "15%"},
            {"data": "orderTotal", "width": "15%"},
            {"data": "pickupTime", "width": "25%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group"><a href="/Admin/Order/OrderDetails?id=${data}" 
                                    class="btn btn-info text-white mx-2">
                                    <i class="bi bi-pencil-square"></i></a>
                                    </div>`
                },
                "width": "15%"
            }
        ],
        "width": "100%"
    });
}