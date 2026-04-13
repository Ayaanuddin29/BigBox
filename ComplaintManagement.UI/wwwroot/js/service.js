$(document).ready(function () {
    loadSubCategories();
    loadServices();

    $("#ddlSubCategory").change(function () {
        //const subCategoryId = $(this).val();
        //if (subCategoryId)
            loadServices();
    });

    $("#btnAdd").click(function () {
        $("#ServiceId").val(0);
        $("#ServiceName").val("");
        $("#ServiceNameAr").val("");
        $("#IsActive").prop("checked", true);
        $("#serviceModal").modal("show");
    });

    $("#btnSave").click(function () {
        const id = parseInt($("#ServiceId").val());
        const subCategoryId = $("#ddlSubCategory").val();//parseInt($("#ddlSubCategory").val());
        const name = $("#ServiceName").val().trim();
        const nameAr = $("#ServiceNameAr").val().trim();
        const isActive = $("#IsActive").is(":checked");

        if (!subCategoryId) {
            alert("Please select a sub-category first.");
            return;
        }

        if (name === "") {
            alert("Please enter a service name.");
            return;
        }

        console.log(subCategoryId);
        const url = id === 0 ? "/Service/Create" : "/Service/Update";
        const payload = {
            Id: id,
            subCategoryId: subCategoryId,
            Name: name,
            Name_Ar: nameAr,
            Active: isActive,
            login_created: "admin"
        };

        console.log("Payload:", JSON.stringify(payload));

        $.ajax({
            url: url,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(payload),
            success: function () {
                $("#serviceModal").modal("hide");
                showToast(id === 0 ? "Service added successfully ✅" : "Service updated ✅");
                loadServices(subCategoryId);
            },
            error: function (xhr) {
                alert("Error: " + xhr.responseText);
            }
        });
    });
});

function loadSubCategories() {
    $.get("/SubCategory/GetAll", function (data) {
        const ddl = $("#ddlSubCategory");
        ddl.empty();
        ddl.append(`<option value="">-- Select SubCategory --</option>`);
        $.each(data, function (i, item) {
            ddl.append(`<option value="${item.id}">${item.name}</option>`);
        });
    });
}

function loadServices() {
    const subCategoryId = $("#ddlSubCategory").val();
    const tbody = $("#serviceTable tbody");
    tbody.empty();

    const url = subCategoryId
        ? `/Service/GetBySubCategory?subCategoryId=${subCategoryId}`
        : `/Service/GetAll`;
    $.get(url, function (data) {
        

        if (!data || data.length === 0) {
            $("#noServiceMessage").show();
            $("#serviceTable").hide();
            return;
        }

        $("#noServiceMessage").hide();
        $("#serviceTable").show();
        console.log(data);
        $.each(data, function (i, item) {
            const row = `
                <tr>
                    <td>${item.id}</td>
                    <td>${item.name}</td>
                    <td>${item.name_Ar}</td>
                    <td>${item.active ? "✅ Active" : "❌ Inactive"}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="editService(${item.id}, '${item.name}', '${item.name_Ar}', ${item.active})">✏️ Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="deleteService(${item.id})">🗑 Delete</button>
                    </td>
                </tr>`;
            tbody.append(row);
        });
    }).fail(function (xhr) {
        //console.error("Error loading subcategories:", xhr);
        $("#noServiceMessage").show().text("❌ Error loading Services.");
        $("#serviceTable").hide();
    });
}

function editService(id, name, nameAr, active) {
    $("#ServiceId").val(id);
    $("#ServiceName").val(name);
    $("#ServiceNameAr").val(nameAr);
    $("#IsActive").prop("checked", active);
    $("#serviceModal").modal("show");
}

function deleteService(id) {
    if (!confirm("Are you sure you want to delete this service?")) return;

    $.post("/Service/Delete", { id: id }, function (res) {
        if (res && res.success) {
            showToast("Service deleted successfully ✅");
            const subCategoryId = $("#ddlSubCategory").val();
            loadServices(subCategoryId);
        } else alert("Delete failed");
    }).fail(function (xhr) {
        alert("Error deleting service: " + xhr.responseText);
    });
}

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
    setTimeout(() => toast.fadeOut(400, () => toast.remove()), 3000);
}