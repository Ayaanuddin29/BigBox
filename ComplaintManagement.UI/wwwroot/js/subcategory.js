$(document).ready(function () {
    loadCategories();
    loadSubCategories();

    $("#ddlCategory").change(function () {
        loadSubCategories();
    });

    $("#btnAdd").click(function () {
        $("#SubCategoryId").val(0);
        $("#SubCategoryName").val("");
        $("#SubCategoryNameAr").val("");
        $("#IsActive").prop("checked", true);
        $("#subcategoryModal").modal("show");
    });

    $("#btnSave").click(function () {
        const id = parseInt($("#SubCategoryId").val());
        const name = $("#SubCategoryName").val().trim();
        const namear = $("#SubCategoryNameAr").val().trim();
        const categoryId = $("#ddlCategory").val();
        const isActive = $("#IsActive").is(":checked");

        if (!categoryId) {
            alert("Please select a category first");
            return;
        }

        if (name === "") {
            alert("Please enter a sub-category name");
            return;
        }

        const url = id === 0 ? "/SubCategory/Create" : "/SubCategory/Update";
        const payload = {
            Id: id,
            CategoryId: parseInt(categoryId),
            Name: name,
            Name_Ar: namear,
            IsActive: isActive,
            CreatedBy: "admin"
        };

        $.ajax({
            url: url,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(payload),
            success: function () {
                $("#subcategoryModal").modal("hide");
                showToast(id === 0 ? "SubCategory added successfully ✅" : "SubCategory updated ✅");
                loadSubCategories();
            },
            error: function (xhr) {
                alert("Error: " + xhr.responseText);
            }
        });
    });
});

function loadCategories() {
    $.get("/Category/GetAll", function (data) {
        const ddl = $("#ddlCategory");
        ddl.empty().append('<option value="">-- Select Category --</option>');
        $.each(data, function (i, item) {
            ddl.append(`<option value="${item.id}">${item.name}</option>`);
        });
    });
}

function loadSubCategories() {
    const categoryId = $("#ddlCategory").val();
    const tbody = $("#subcategoryTable tbody");
    tbody.empty();

    const url = categoryId
        ? `/SubCategory/GetByCategory?categoryId=${categoryId}`
        : `/SubCategory/GetAll`;


    $.get(url, function (data) {

        if (!data || data.length === 0) {
            $("#noSubCategoryMessage").show();
            $("#subCategoryTable").hide();
            return;
        }

        $("#noSubCategoryMessage").hide();
        $("#subCategoryTable").show();

        $.each(data, function (i, item) {
            const row = `
                <tr>
                    <td>${item.id}</td>
                    <td>${item.name}</td>
                    <td>${item.name_Ar || ""}</td>
                    <td>${item.active ? "✅ Active" : "❌ Inactive"}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="editSubCategory(${item.id}, '${item.name}', '${item.name_Ar || ""}', ${item.active})">✏️ Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="deleteSubCategory(${item.id})">🗑 Delete</button>
                    </td>
                </tr>`;
            tbody.append(row);
        });
    }).fail(function (xhr) {
        //console.error("Error loading subcategories:", xhr);
        $("#noSubCategoryMessage").show().text("❌ Error loading sub-categories.");
        $("#subCategoryTable").hide();
    });
}

function editSubCategory(id, name, nameAr, isActive) {
    $("#SubCategoryId").val(id);
    $("#SubCategoryName").val(name);
    $("#SubCategoryNameAr").val(nameAr);
    $("#IsActive").prop("checked", isActive);
    $("#subcategoryModal").modal("show");
}

function deleteSubCategory(id) {
    if (!confirm("Are you sure you want to delete this subcategory?")) return;

    $.post("/SubCategory/Delete", { id: id }, function (res) {
        if (res && res.success) {
            showToast("SubCategory deleted ✅");
            loadSubCategories();
        }
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