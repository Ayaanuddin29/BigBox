$(document).ready(function () {
    loadData();

    function showToast(message) {
        const toast = $(`
        <div class="toast align-items-center text-white bg-success border-0 show" role="alert" aria-live="assertive" aria-atomic="true" style="min-width: 250px;">
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    `);

        $("#toastContainer").append(toast);

        setTimeout(() => {
            toast.fadeOut(400, function () {
                $(this).remove();
            });
        }, 3000); // Auto-hide after 3 seconds
    }
    function loadData() {
        $.ajax({
            url: `/Master/GetAll?tableName=${tableName}`,
            type: "GET",
            success: function (data) {
                console.log("Data loaded:", data);
                let tbody = $("#tblMaster tbody");
                tbody.empty();

                $.each(data, function (i, item) {
                    console.log("Data loaded:", item.name);
                    console.log("Full item:", item)
                    const row = $('<tr>');
                    row.append($('<td>').text(item.id));
                    row.append($('<td>').append($('<input>', {
                        type: 'text',
                        class: 'form-control form-control-sm',
                        id: `name_${item.id}`,
                        value: item.name
                    })));
                    row.append($('<td>').append($('<input>', {
                        type: 'text',
                        class: 'form-control form-control-sm',
                        id: `name_ar_${item.id}`,
                        value: item.name_ar || '',
                        dir: 'rtl'
                    })));
                    row.append($('<td>').append($('<input>', {
                        type: 'checkbox',
                        id: `active_${item.id}`,
                        checked: item.active === true 
                    })));
                    row.append($('<td>').append(
                        $('<button>', {
                            class: 'btn btn-success btn-sm update',
                            text: 'Update',
                            'data-id': item.id
                        }),
                        $('<button>', {
                            class: 'btn btn-danger btn-sm delete',
                            text: 'Delete',
                            'data-id': item.id
                        })
                    ));
                    $('#tblMaster tbody').append(row);
                });
            },
            error: function (xhr) {
                alert("Error loading data");
                console.log(xhr);
            }
        });
    }

    $("#btnAdd").click(function () {
        var name = $("#txtName").val();
        var name_ar = $("#txtNameAr").val();
        if (name === "") {
            alert("Please enter name");
            return;
        }

        $.post("/Master/Create", { tableName: tableName, name: name, name_ar: name_ar, createdBy: "Admin" }, function (res) {
            if (res.success) {
                $("#txtName").val("");
                $("#txtNameAr").val("");
                loadData();
                showToast("Added successfully ✅");
            } else {
                alert(res.message);
            }
        });
    });

    $(document).on("click", ".update", function () {
        var id = $(this).data("id");
        var name = $(`#name_${id}`).val();
        var name_ar = $(`#name_ar_${id}`).val();
        var active = $(`#active_${id}`).is(':checked');

        $.post("/Master/Update", { id: id, tableName: tableName, name: name, name_ar: name_ar, Active: active }, function (res) {
            if (res.success) {
                loadData();
                showToast("Updated successfully ✅");
            }
            else alert(res.message);
        });
    });

    $(document).on("click", ".delete", function () {
        if (!confirm("Are you sure you want to delete this record?")) return;

        var id = $(this).data("id");
        $.ajax({
            url: `/Master/Delete?id=${id}&tableName=${tableName}`,
            type: "DELETE",
            success: function (res) {
                if (res.success)
                {
                    showToast("Deleted successfully ✅");
                    loadData();
                }
                else alert(res.message);
            }
        });
    });
});