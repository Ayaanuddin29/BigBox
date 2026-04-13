/**************************************
 * GLOBALS
 **************************************/
let workflow = {
    nodes: [],
    links: []
};

const masterMap = {
    "Category": "category",
    "Priority": "robox_m_priority",
    "Impact": "robox_m_impact",
    "Urgency": "robox_m_urgency",
    "Channel": "robox_m_channel"
};

const instance = jsPlumb.getInstance({
    Container: "canvas",
    Connector: ["Flowchart", { cornerRadius: 6 }],
    Endpoint: ["Dot", { radius: 4 }],
    EndpointStyle: { fill: "#111827" },
    PaintStyle: { stroke: "#374151", strokeWidth: 2 }
});


/**************************************
 * INIT
 **************************************/
document.addEventListener("DOMContentLoaded", () => {

    const canvas = document.getElementById("canvas");

    console.log(canvas);

    canvas.addEventListener("dragover", e => e.preventDefault());
    canvas.addEventListener("drop", drop);

    document.querySelectorAll(".node").forEach(node => {
        node.addEventListener("dragstart", e => {
            e.dataTransfer.setData("text/plain", node.dataset.type);
        });
    });

    // LOAD EDIT MODE
    const json = document.getElementById("workflowJson")?.value;
    console.log(json);
    if (json) {
        const data = JSON.parse(json);
        loadWorkflowToCanvas(data);
    }

});

/* DRAW EXISTING WORKFLOW */
function loadWorkflowToCanvas(data) {
    data.nodes.forEach(n => {
        createNode(n);
    });

    data.links.forEach(l => {
        instance.connect({
            source: l.from,
            target: l.to,
            overlays: [
                ["Arrow", { location: 1 }],
                ["Label", { label: l.label, location: 0.5 }]
            ]
        });
    });
}

/* CREATE NODE */
function createNode(n) {
    const el = document.createElement("div");
    el.className = "canvas-node";
    el.dataset.id = n.id;
    el.dataset.type = n.type;
    el.style.left = n.x + "px";
    el.style.top = n.y + "px";

    if (n.type === "condition") {
        el.innerHTML = `
            <span>${n.config?.label || "Condition"}</span>
            <div class="yes-handle"></div>
            <div class="no-handle"></div>`;
    } else {
        el.innerHTML = `<div class="node-title">${n.config?.label || n.type}</div>
                        <div class="node-handle"></div>`;
    }

    document.getElementById("canvas").appendChild(el);

    instance.draggable(el, { containment: "parent" });
    instance.makeTarget(el, { anchor: "Top" });

    if (n.type === "condition") {
        instance.makeSource(el.querySelector(".yes-handle"), {
            anchor: "Right", parameters: { label: "YES" }
        });
        instance.makeSource(el.querySelector(".no-handle"), {
            anchor: "Bottom", parameters: { label: "NO" }
        });
    } else {
        instance.makeSource(el.querySelector(".node-handle"), {
            anchor: "Bottom"
        });
    }

    workflow.nodes.push(n);
}


/**************************************
 * CONNECTION EVENTS
 **************************************/
instance.bind("connection", function (info) {

    // Arrow
    info.connection.addOverlay([
        "Arrow",
        { location: 1, width: 12, length: 12 }
    ]);

    // YES / NO label
    const label = info.sourceEndpoint.getParameter("label");

    if (label) {
        info.connection.addOverlay([
            "Label",
            {
                label: label,
                location: 0.5,
                cssClass: "flow-label"
            }
        ]);
    }

    const fromNode = info.source.parentElement.dataset.id;
    const toNode = info.target.dataset.id;

    workflow.links.push({
        from: fromNode,
        to: toNode,
        label: label || ""
    });

    console.log("LINK ADDED:", fromNode, "->", toNode);
    console.log(workflow);
});


/**************************************
 * DROP NODE
 **************************************/
function drop(e) {
    e.preventDefault();

    const type = e.dataTransfer.getData("text/plain");
    if (!type) return;

    const canvas = document.getElementById("canvas");
    const rect = canvas.getBoundingClientRect();

    const x = e.clientX - rect.left;
    const y = e.clientY - rect.top;

    const nodeId = Date.now();

    const newNode = document.createElement("div");
    newNode.className = "canvas-node";
    newNode.style.left = x + "px";
    newNode.style.top = y + "px";
    newNode.dataset.id = nodeId;
    newNode.dataset.type = type;

    // UI
    if (type === "condition") {
        newNode.classList.add("condition");
        newNode.innerHTML = `
        <span>Condition</span>

        <div class="yes-handle"></div>
        <div class="no-handle"></div>
    `;
    } else {
       newNode.innerHTML = `
    <div class="node-title">${type.toUpperCase()}</div>
    <div class="node-handle"></div>
`;
    }

    canvas.appendChild(newNode);

    // Drag inside canvas
    instance.draggable(newNode, {
        containment: "parent",
        filter: ".yes-handle, .no-handle",
        filterExclude: true
    });

    // Target (incoming)
    instance.makeTarget(newNode, {
        anchor: "Top",
        allowLoopback: false
    });

    // Source (outgoing)
    if (type === "condition") {

        const yesHandle = newNode.querySelector(".yes-handle");
        const noHandle = newNode.querySelector(".no-handle");

        instance.makeSource(yesHandle, {
            parent: newNode,
            anchor: "Right",
            maxConnections: 1,
            parameters: { label: "YES" }
        });

        instance.makeSource(noHandle, {
            parent: newNode,
            anchor: "Bottom",
            maxConnections: 1,
            parameters: { label: "NO" }
        });
    } else {
        instance.makeSource(newNode.querySelector(".node-handle"), {
            parent: newNode,
            anchor: "Bottom",
            maxConnections: -1
        });
    }

    // Edit on double click
    newNode.addEventListener("dblclick", () => openNodeEditor(nodeId));

    workflow.nodes.push({
        id: nodeId,
        type,
        x,
        y,
        config: {}
    });

    console.log(workflow);
}


/**************************************
 * EDIT NODE
 **************************************/
function openNodeEditor(id) {

    const node = workflow.nodes.find(n => n.id === id);
    if (!node) return;

    document.getElementById("editNodeId").value = id;
    document.getElementById("nodeLabel").value = node.config.label || "";

    hideAllEditors();

    if (node.type === "condition") {
        document.getElementById("editor-condition").classList.remove("d-none");

        document.getElementById("conditionField").value = node.config.field || "Category";
        document.getElementById("conditionOperator").value = node.config.operator || "==";

        onConditionFieldChange();

        setTimeout(() => {
            document.getElementById("conditionValueSelect").value = node.config.value || "";
        }, 200);
    }

    if (node.type === "assign") {
        document.getElementById("editor-assign").classList.remove("d-none");

        document.getElementById("assignType").value = node.config.field || "Associate";
        loadAssignValues(document.getElementById("assignType").value);

        setTimeout(() => {
            document.getElementById("assignValue").value = node.config.value || "";
        }, 200);
    }

    if (node.type === "status") {
        document.getElementById("editor-status").classList.remove("d-none");
        loadStatusDropdown();

        setTimeout(() => {
            document.getElementById("statusValue").value = node.config.value || "";
        }, 200);
    }

    if (node.type === "notify") {
        document.getElementById("editor-notify").classList.remove("d-none");
        document.getElementById("notifyValue").value = node.config.value || "";
    }

    new bootstrap.Modal(document.getElementById("nodeModal")).show();
}

function saveNodeConfig() {

    const id = Number(document.getElementById("editNodeId").value);
    const node = workflow.nodes.find(n => n.id === id);
    if (!node) return;

    node.config.label = document.getElementById("nodeLabel").value;

    if (node.type === "condition") {
        node.config.field = document.getElementById("conditionField").value;
        node.config.operator = document.getElementById("conditionOperator").value;
        node.config.value = document.getElementById("conditionValueSelect").value;
    }

    if (node.type === "assign") {
        node.config.field = document.getElementById("assignType").value;
        node.config.value = document.getElementById("assignValue").value;
    }

    if (node.type === "status") {
        node.config.value = document.getElementById("statusValue").value;
    }

    if (node.type === "notify") {
        node.config.value = document.getElementById("notifyValue").value;
    }

    // Update canvas text
    const el = document.querySelector(`[data-id='${id}']`);
    if (el) {
        const title = el.querySelector(".node-title") || el.querySelector("span");
        if (title) title.innerText = node.config.label || node.type.toUpperCase();
        instance.repaint(el);
    }

    bootstrap.Modal.getInstance(document.getElementById("nodeModal")).hide();
}


/**************************************
 * SAVE WORKFLOW
 **************************************/
function saveWorkflow() {

    const conditions = workflow.nodes
        .filter(n => n.type === "condition")
        .map(n => ({
            Field: n.config.field,
            Operator: n.config.operator,
            Value: n.config.value
        }));

    console.log(conditions);

    const payload = {
        workflowId: document.getElementById("WorkflowId")?.value || 0,
        workflowName: document.getElementById("WorkflowName").value,
        module: 1,
        loginCreated: "admin",

        workflowJson: JSON.stringify(workflow),
        conditionJson: conditions.length > 0
            ? JSON.stringify({ Logic: "AND", Rules: conditions })
            : null
    };

    fetch("/workflow/save", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    })
        .then(r => r.json())
        .then(_ => window.location.href = "/Workflow/List");
        //.then(res => alert("Workflow Saved ID : " + res.workflowId));
}

document.getElementById("conditionField")
    .addEventListener("change", onConditionFieldChange);

function onConditionFieldChange() {
    const field = document.getElementById("conditionField").value;
    const table = masterMap[field];

    if (!table) {
        showTextbox();
        return;
    }

    if (field == "Category") {
        fetch("/Category/GetAll")
            .then(r => r.json())
            .then(data => populateDropdown(data))
            .catch(() => showTextbox());
    } 
    else {
        fetch(`/Master/GetAll?tableName=${table}`)
            .then(r => r.json())
            .then(data => populateDropdown(data))
            .catch(() => showTextbox());
    }
}

function populateDropdown(data) {
    const ddl = document.getElementById("conditionValueSelect");
    const txt = document.getElementById("conditionValueText");

    ddl.innerHTML = "";
    const placeholder = new Option("Select", "");
    placeholder.disabled = true;
    placeholder.selected = true;
    ddl.add(placeholder);
    data.forEach(x => {
        ddl.add(new Option(x.name, x.id));
    });

    ddl.classList.remove("d-none");
    txt.classList.add("d-none");
}

function showTextbox() {
    //document.getElementById("conditionValueSelect").classList.add("d-none");
    //document.getElementById("conditionValueText").classList.remove("d-none");
}

function hideAllEditors() {
    document.querySelectorAll(".editor").forEach(e => e.classList.add("d-none"));
}

async function loadStatusDropdown() {
    const ddl = document.getElementById("statusValue");
    if (!ddl) return;

    ddl.innerHTML = `<option value="">Loading...</option>`;

    try {
        const res = await fetch("/Master/GetAll?tableName=robox_m_status_incident");
        const data = await res.json();

        ddl.innerHTML = `<option value="">-- Select Status --</option>`;

        data.forEach(x => {
            ddl.innerHTML += `<option value="${x.id}">${x.name}</option>`;
        });
    } catch (err) {
        console.error("Failed to load statuses", err);
        ddl.innerHTML = `<option value="">Error</option>`;
    }
}

async function loadAssignValues(assignType) {
    const ddl = document.getElementById("assignValue");
    if (!ddl) return;

    ddl.innerHTML = `<option value="">Loading...</option>`;

    let url = "";
    switch (assignType) {
        case "Associate":
            url = "/Master/GetAssociates";
            break;

        case "Group":
            url = "/Master/GetAssociateGroups";
            break;

        default:
            ddl.innerHTML = `<option value="">Unsupported</option>`;
            return;
    }

    try {
        const res = await fetch(url);
        const data = await res.json();

        ddl.innerHTML = `<option value="">-- Select --</option>`;

        data.forEach(x => {
            if (assignType == "Associate")
                ddl.innerHTML += `<option value="${x.associate_id}">${x.associate_name}</option>`;
            else
                ddl.innerHTML += `<option value="${x.associate_group_id}">${x.group_name}</option>`;
        });
    } catch (err) {
        console.error("Failed to load assign values", err);
        ddl.innerHTML = `<option value="">Error</option>`;
    }
}

document.getElementById("assignType").addEventListener("change", function () {
    loadAssignValues(this.value);
});

