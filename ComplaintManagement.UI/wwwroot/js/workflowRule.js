$(document).ready(function () {

    loadRules();
    loadDropdowns();
    loadDropdownLanguage();

    $("#btnNew").click(function () {
        $("#formTitle").text("New Workflow Rule");
        $("#ruleFormContainer").slideDown();
        clearForm();
    });

    $("#btnCancel").click(function () {
        $("#ruleFormContainer").slideUp();
        clearForm();
    });

    $("#btnSave").click(function () {

        var payload = {
            RuleId: $("#ruleId").val() || 0,
            RuleName: $("#ruleName").val(),
            OrderType: $("#orderType").val(),
            Category: $("#category").val(),
            ServiceType: $("#serviceType").val(),
            ServiceItem: $("#serviceItem").val(),
            Priority: $("#priority").val(),
            SltId: $("#slt").val(),
            AssignToAssociate: $("#associate").val(),
            AssignToGroup: $("#group").val()
        };

        $.ajax({
            url: "/WorkflowRule/Save",
            type: "POST",
            data: payload,
            success: function (res) {
                if (res.success) {
                    loadRules();
                    $("#ruleFormContainer").slideUp();
                    clearForm();
                } else {
                    alert(res.message);
                }
            },
            error: function (xhr) {
                console.error("Save error:", xhr.responseText);
            }
        });
    });

    $(document).on("click", ".edit-rule", function () {

        let id = $(this).data("id");

        $.get(`/WorkflowRule/GetById?id=${id}`, function (rule) {

            $("#formTitle").text("Edit Workflow Rule");
            $("#ruleFormContainer").slideDown();

            $("#ruleId").val(rule.ruleId);
            $("#ruleName").val(rule.ruleName);
            $("#orderType").val(rule.orderType);
            $("#category").val(rule.category);
            $("#serviceType").val(rule.serviceType);
            $("#serviceItem").val(rule.serviceItem);
            $("#priority").val(rule.priority);
            $("#slt").val(rule.sltId);
            $("#associate").val(rule.assignToAssociate);
            $("#group").val(rule.assignToGroup);
        }).fail(err => console.error("GetById error:", err));
    });

    function clearForm() {
        $("#ruleFormContainer input, #ruleFormContainer select").val("");
        $("#ruleId").val("");
    }

    function loadRules() {

        $.get("/WorkflowRule/GetAll", function (data) {

            let tbody = $("#tblRules tbody");
            tbody.empty();

            $.each(data, function (i, r) {

                tbody.append(`
                    <tr>
                        <td>${r.ruleId}</td>
                        <td>${r.ruleName}</td>
                        <td>${r.orderTypeName}</td>
                        <td>${r.categoryName}</td>
                        <td>${r.serviceTypeName}</td>
                        <td>${r.serviceItemName}</td>
                        <td>${r.priorityName}</td>
                        <td>${r.sltName}</td>
                        <td>${r.associateName}</td>
                        <td>${r.groupName}</td>
                        <td>
                            <button class="btn btn-primary btn-sm edit-rule" data-id="${r.ruleId}">
                                Edit
                            </button>
                        </td>
                    </tr>
                `);
            });

        }).fail(err => console.error("GetAll error:", err));
    }


    function loadDropdowns() {
        loadDropdown("/Master/GetAll?tableName=robox_m_order_types", "#orderType");
        loadDropdown("/Master/GetAll?tableName=robox_m_category", "#category");
        loadDropdown("/Master/GetAll?tableName=robox_m_priority", "#priority");

        loadDropdown("/SLT/GetAll", "#slt");

        // Uncomment when API available
        // loadDropdown("/Users/GetAllAssociates", "#associate");
        // loadDropdown("/Users/GetAllGroups", "#group");
    }


    function loadDropdown(url, selector) {
        console.log(url);
        $.get(url, function (data) {

            let ddl = $(selector);
            ddl.empty();
            ddl.append(`<option value="">-- Select --</option>`);

            $.each(data, function (i, item) {
                if (selector === "#slt") {
                    ddl.append(`<option value="${item.slaId}">${item.slaName}</option>`);
                }
                else {
                    ddl.append(`<option value="${item.id}">${item.name}</option>`);
                }
            });

        }).fail(err => console.error("Dropdown load failed:", selector, err));
    }

    function loadDropdownLanguage() {

        let ddl = $("#language");
            ddl.empty();
        ddl.append(`<option value="">Any</option>`);
        ddl.append(`<option value="1">English</option>`);
    }

    $("#category").change(function () {
        let categoryId = $(this).val();

        if (!categoryId) {
            $("#serviceType").empty()
                .append('<option value="">-- Select Type --</option>');
            return;
        }
        const url = `/SubCategory/GetByCategory?categoryId=${categoryId}`;

        $.get(url, function (data) {

            let ddl = $("#serviceType");
            ddl.empty();
            ddl.append(`<option value="">-- Select --</option>`);

            $.each(data, function (i, item) {                
                    ddl.append(`<option value="${item.id}">${item.name}</option>`);
               });

        }).fail(err => console.error("Dropdown load failed:", selector, err));
       
    });

    $("#serviceType").change(function () {
        let typeId = $(this).val();

        if (!typeId) {
            $("#serviceItem").empty()
                .append('<option value="">-- Select Service --</option>');
            return;
        }
        const url = `/Service/GetBySubCategory?subCategoryId=${typeId}`;

        $.get(url, function (data) {

            let ddl = $("#serviceItem");
            ddl.empty();
            ddl.append(`<option value="">-- Select Service --</option>`);

            $.each(data, function (i, item) {
                ddl.append(`<option value="${item.id}">${item.name}</option>`);
            });

        }).fail(err => console.error("Dropdown load failed:", selector, err));

    });

});
