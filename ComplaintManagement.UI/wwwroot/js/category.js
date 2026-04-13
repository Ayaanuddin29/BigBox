$(document).ready(function () {
    loadCategories();    

    // 🔹 Add button
    $("#btnAdd").click(function () {
        $("#CategoryId").val(0);
        $("#CategoryName").val("");
        $("#CategoryNameAr").val("");
        $("#IsActive").prop("checked", true);
        $("#categoryModal").modal("show");
    });

    $("#btnSave").click(function () {
        const id = parseInt($("#CategoryId").val());
        const name = $("#CategoryName").val().trim();
        const namear = $("#CategoryNameAr").val().trim();
        const isActive = $("#IsActive").is(":checked");

        if (name === "") {
            alert("Please enter a category name");
            return;
        }

        const url = id === 0 ? "/Category/Create" : "/Category/Update";
        const payload = {
            Id: id,
            Name: name,
            Name_Ar: namear, // optional
            IsActive: isActive,
            CreatedBy: "admin" // or get from session
        };

        $.ajax({
            url: url,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(payload),
            success: function () {
                $("#categoryModal").modal("hide");
                if (url == "/Category/Create")
                    showToast("Category added successfully ✅");
                else
                    showToast("Category updated successfully ✅");
                loadCategories();
            },
            error: function (xhr) {
                alert("Error: " + xhr.responseText);
            }
        });
    });
});

function loadCategories() {
    $.get("/Category/GetAll", function (data) {
        const tbody = $("#categoryTable tbody");
        tbody.empty();

        $.each(data, function (i, item) {           
            const row = `
                <tr>
                    <td>${item.id}</td>
                    <td>${item.name}</td>
                    <td>${item.name_Ar}</td>
                    <td>${item.active ? "✅ Active" : "❌ Inactive"}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="editCategory(${item.id}, '${item.name}','${item.name_Ar}', ${item.isActive})">✏️ Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="deleteCategory(${item.id})">🗑 Delete</button>
                    </td>
                </tr>`;
            tbody.append(row);
        });
    });
}

function editCategory(id, name,namear, isActive) {
    $("#CategoryId").val(id);
    $("#CategoryName").val(name);
    $("#CategoryNameAr").val(namear);
    $("#IsActive").prop("checked", isActive);
    $("#categoryModal").modal("show");
}

function deleteCategory(id) {
    if (!confirm("Are you sure you want to delete this category?")) return;

    $.post("/Category/Delete", { id: id }, function (res) {
        if (res && res.success) {
            showToast("Category deleted successfully ✅");
            loadCategories();
        }
        else alert("Delete failed");
    }).fail(function (xhr) {
        alert("Error deleting category: " + xhr.responseText);
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

    setTimeout(() => {
        toast.fadeOut(400, function () {
            $(this).remove();
        });
    }, 3000); // Auto-hide after 3 seconds
}
