var g = new Graph(0);

let match = document.URL.match(/g6=[^&]+/);
if (match) {
    let g6 = decodeURIComponent(match[0].substr(3));
    g = Graph.fromGraph6(g6);
}

var editor = new GraphEditor("editor_canvas", undefined, true);
document.editor = editor;
editor.setGraph(g);
/*
document.getElementById("editor-selected-edge-width").oninput = function () {
    if (this.edge) {
        console.log("change edge width");
        this.edge.width = this.value;
        editor.draw();
    }
}

document.getElementById("editor-selected-object-name").oninput = function () {
    if (this.vertex) {
        this.vertex.name = this.value;
        editor.draw();
    }
};

document.getElementById("editor-selected-object-color").oninput = function () {
    if (this.vertex) {
        this.vertex.color = this.value;
        editor.draw();
    }
    if (this.edge) {
        this.edge.color2 = this.value;
        editor.draw();
    }
};

document.getElementById("editor-selected-object-name-color").oninput = function () {
    if (this.vertex) {
        this.vertex.nameColor = this.value;
        editor.draw();
    }
};

document.getElementById("editor-selected-object-radius").oninput = function () {
    if (this.vertex) {
        this.vertex.radius = this.value;
        editor.draw();
    }
};


editor.events.onVertexSelect = function (v) {
    let nameField = document.getElementById("editor-selected-object-name");
    nameField.disabled = false;
    nameField.vertex = v;
    nameField.value = v.name;

    let colorField = document.getElementById("editor-selected-object-color");
    colorField.disabled = false;
    colorField.vertex = v;
    colorField.value = v.color ? v.color : "";

    let radiusField = document.getElementById("editor-selected-object-radius");
    radiusField.disabled = false;
    radiusField.vertex = v;
    radiusField.value = v.radius ? v.radius : "";

    let nameColorField = document.getElementById("editor-selected-object-name-color");
    nameColorField.disabled = false;
    nameColorField.vertex = v;
    nameColorField.value = v.nameColor ? v.nameColor : "";

    document.getElementById("editor-selected-object-degree").innerHTML = v.degree;
}

editor.events.onVertexDeselect = function (v) {
    let nameField = document.getElementById("editor-selected-object-name");
    delete nameField.vertex;
    nameField.disabled = true;
    nameField.value = "";

    let colorField = document.getElementById("editor-selected-object-color");
    delete colorField.vertex;
    colorField.disabled = true;
    colorField.value = "";

    let radiusField = document.getElementById("editor-selected-object-radius");
    delete radiusField.vertex;
    radiusField.disabled = true;
    radiusField.value = "";

    let nameColorField = document.getElementById("editor-selected-object-name-color");
    delete nameColorField.vertex;
    nameColorField.disabled = true;
    nameColorField.value = "";

    document.getElementById("editor-selected-object-degree").innerHTML = "";
}

editor.events.onEdgeSelect = function (e) {
    let colorField = document.getElementById("editor-selected-object-color");
    colorField.edge = e;
    colorField.disabled = false;
    colorField.value = e.color2 ? e.color2 : "";

    let widthField = document.getElementById("editor-selected-edge-width");
    console.log(widthField)
    widthField.edge = e;
    widthField.disabled = false;
    widthField.value = e.width ? e.width : "";
}

editor.events.onEdgeDeselect = function (e) {
    let colorField = document.getElementById("editor-selected-object-color");
    delete colorField.edge;
    colorField.disabled = true;
    colorField.value = "";

    let widthField = document.getElementById("editor-selected-edge-width");
    delete widthField.edge;
    widthField.disabled = true;
    widthField.value = "";
}

//===========================================================

var onchange = function (e) {

    document.editor.drawer.settings = {
        canvasWidth: document.getElementById("editor-image-resolution").value,
        canvasHeight: document.getElementById("editor-image-resolution").value,
        vertexRadius: document.getElementById("editor-vertex-radius").value,
        vertexColor: document.getElementById("editor-vertex-color").value,
        vertexBorderColor: document.getElementById("editor-vertex-border-color").value,
        vertexTextColor: document.getElementById("editor-vertex-text-color").value,
        vertexBorderWidth: document.getElementById("editor-vertex-border-width").value,
        edgeColor: document.getElementById("editor-edge-color").value,
        backgroundColor: document.getElementById("editor-background-color").value,
        edgeLineWidth: document.getElementById("editor-edge-line-width").value,
        vertexFontSize: document.getElementById("editor-vertex-font-size").value,
        showVertexLabels: document.getElementById("editor-show-vertex-labels").checked,
        arrowLength: 20,
        arrowHeight: 5
    };

    let resolution = document.getElementById("editor-image-resolution").value;
    let ratio = resolution.match(/[0-9]+/g);
    let resolutionWidth = parseInt(ratio[0]);
    let resolutionHeight = parseInt(ratio[1]);
    let canvas = document.getElementById("editor_canvas");

    canvas.style.width = resolutionWidth + "px";
    canvas.style.height = resolutionHeight + "px";

    canvas.width = resolutionWidth;
    canvas.height = resolutionHeight;

    document.editor.draw();
};

onchange();

for (let e of document.getElementsByTagName("input")) {
    e.onchange = onchange;
}

for (let e of document.getElementsByTagName("select")) {
    e.onchange = onchange;
}*/

//==================================================

/*
for (let input of document.getElementsByTagName("input")) {
    if (input.type === "color") {
        console.log(input);
    }
}*/

//=====================================================


function show_info(id) {
    document.getElementById(id).style.display = document.getElementById(id).style.display == 'none' ? 'block' : 'none';
}

function check_task() {
    let s = "";
    for (let i = 0; i < editor.graph.vertexCount; ++i) {
        s += editor.graph.edge(i, 0).exist ? "1" : "0";
        for (let j = 1; j < editor.graph.vertexCount; ++j) {
            s += editor.graph.edge(i, j).exist ? "1" : "0";
        }
    }

    var _aswer = document.getElementById("TaskResult").value
    if (s == _aswer) {
        document.getElementById("Answer").value = s;
        clearInterval(check_timer);

        createNotification("/img/7.png", "Решено верно!");
        setTimeout(() => {
            //redirect
        }, 10000);
    }

}
var check_timer = setInterval(check_task, 2000);