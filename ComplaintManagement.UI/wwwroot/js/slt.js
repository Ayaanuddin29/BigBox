$(document).ready(function () {

    loadSlt();

    let allData = [];

    function loadSlt() {
        $.get("/Slt/GetAll", function (data) {
            allData = data;

            //Added filter by defaullt
            let status = $("#ddlStatus").val();

            let filtered = allData.filter(item => {
                if (status === "All") return true;
                return status === "Active" ? item.active : !item.active;
            });

            $("#tblSlt").DataTable().destroy();
            renderTable(filtered);

           // renderTable(allData);
        });
    }

    function renderTable(data) {
        let tbody = $("#tblSlt tbody");
        tbody.empty();

        $.each(data, function (i, item) {
            tbody.append(`
            <tr>
                <td>${item.slaId}</td>
                <td>${item.slaName}</td>
                <td>${item.expectedInitialResponseDays}</td>
                <td>${item.expectedInitialResponseHours}</td>
                <td>${item.expectedInitialResponseMinutes}</td>
                <td>${item.expectedCloseDays}</td>
                <td>${item.expectedCloseHours}</td>
                <td>${item.expectedCloseMinutes}</td>
                <td>${item.slaThreatenDays}</td>
                <td>${item.slaThreatenHours}</td>
                <td>${item.slaThreatenMinutes}</td>
                
                <td><input type="checkbox" disabled ${item.active ? "checked" : ""}/></td>
                <td>
                <div class="d-flex gap-2">
                    <button class="btn btn-sm btn-warning edit" data-id="${item.slaId}">✏️</button>
                    <button class="btn btn-sm btn-danger delete" data-id="${item.slaId}">🗑</button>
                </div>
                </td>
            </tr>
        `);
        });

        $("#tblSlt").DataTable({
            destroy: true,
            pageLength: 10,
            lengthMenu: [5, 10, 25, 50],
            searching: true,
            ordering: true,
            order: [[0, "desc"]],
            columnDefs: [{ orderable: false, targets: [11, 12] }]
        });
    }

    $("#btnFilter").click(function () {
        let status = $("#ddlStatus").val();

        let filtered = allData.filter(item => {
            if (status === "All") return true;
            return status === "Active" ? item.active : !item.active;
        });

        $("#tblSlt").DataTable().destroy();
        renderTable(filtered);
    });


    // SHOW EMPTY FORM FOR NEW
    $("#btnNew").click(function () {
        $("#formTitle").text("Add New SLA Target");
        $("#formSection").show();
        clearForm();
    });


    // EDIT
    $(document).on("click", ".edit", function () {
        console.log('edit');
        let id = $(this).data("id");

        $.get("/Slt/GetById/" + id, function (item) {

            console.log(item);

            $("#formTitle").text("Edit SLA Target");

            $("#SlaId").val(item.slaId);
            $("#SlaName").val(item.slaName);
            $("#SlaNameAr").val(item.slaNameAr);
            $("#SlaNameOth").val(item.slaNameOth);
            $("#InitialResponseDays").val(item.expectedInitialResponseDays);
            $("#InitialResponseHours").val(item.expectedInitialResponseHours);
            $("#InitialResponseMinutes").val(item.expectedInitialResponseMinutes);
            $("#TargetCloseDays").val(item.expectedCloseDays);
            $("#TargetCloseHours").val(item.expectedCloseHours);
            $("#TargetCloseMinutes").val(item.expectedCloseMinutes);
            $("#SlaThreatenDays").val(item.slaThreatenDays);
            $("#SlaThreatenHours").val(item.slaThreatenHours);
            $("#SlaThreatenMinutes").val(item.slaThreatenMinutes);
            $("#SlaType").val(item.slaTypeId);
            $("#IsActive").prop("checked", item.active);
            $("#SlaDetails").val(item.details);

            $("#formSection").show();
        });
    });


    // SAVE
    $("#btnSave").click(function () {
        let dto = {
            slaId: $("#SlaId").val(),
            slaName: $("#SlaName").val(),
            slaNameAr: $("#SlaNameAr").val(),
            slaNameOth: $("#SlaNameOth").val(),
            details: $("#SlaDetails").val(),
            expectedInitialResponseDays: $("#InitialResponseDays").val(),
            expectedInitialResponseHours: $("#InitialResponseHours").val(),
            expectedInitialResponseMinutes: $("#InitialResponseMinutes").val(),
            expectedCloseDays: $("#TargetCloseDays").val(),
            expectedCloseHours: $("#TargetCloseHours").val(),
            expectedCloseMinutes: $("#TargetCloseMinutes").val(),
            slaThreatenDays: $("#SlaThreatenDays").val(),
            slaThreatenHours: $("#SlaThreatenHours").val(),
            slaThreatenMinutes: $("#SlaThreatenMinutes").val(),
            slaTypeId: $("#SlaType").val(),
            active: $("#IsActive").is(":checked")
        };

        console.log("dto data:", dto);

        $.ajax({
            url: "/Slt/Save",
            type: "POST",
            data: JSON.stringify(dto),
            contentType: "application/json",
            success: function (res) {
                alert("Saved successfully");
                loadSlt();
                clearForm();
                $("#formSection").hide();
            }
        });
    });


    // DELETE
    $(document).on("click", ".delete", function () {
        if (!confirm("Are you sure?")) return;

        let id = $(this).data("id");

        $.ajax({
            url: "/Slt/Delete?id=" + id,
            type: "DELETE",
            success: function () {
                alert("Deleted");
                loadSlt();
            }
        });
    });


    // RESET
    $("#btnReset").click(function () {
        clearForm();
    });

    function clearForm() {
        $("#SlaId").val(0);
        $("#SlaName").val("");
        $("#SlaNameAr").val("");
        $("#SlaNameOth").val("");
        $("#SlaDetails").val("");
        $("#InitialResponseDays").val("");
        $("#InitialResponseHours").val("");
        $("#InitialResponseMinutes").val("");
        $("#TargetCloseDays").val("");
        $("#TargetCloseHours").val("");
        $("#TargetCloseMinutes").val("");
        $("#SlaThreatenDays").val("");
        $("#SlaThreatenHours").val("");
        $("#SlaThreatenMinutes").val("");
        $("#SlaType").val("2");
        $("#IsActive").val("true");
    }

});
