if (document.getElementById("TaskTitle").value == "DAddMachin") {
    var tag_script = document.createElement("script");
    tag_script.src = "/js/task_addmachin.js";

    document.body.append(tag_script);
}
else if (document.getElementById("TaskTitle").value == "AddMachin") {
    var tag_script = document.createElement("script");
    tag_script.src = "/js/task_addmachin.js";

    document.body.append(tag_script);
}
else if (document.getElementById("TaskTitle").value == "Graph") {
    var tag_script = document.createElement("script");
    tag_script.src = "/js/task_ugraph.js";

    document.body.append(tag_script);
}
else if (document.getElementById("TaskTitle").value == "UGraph") {
    var tag_script = document.createElement("script");
    tag_script.src = "/js/task_ugraph.js";

    document.body.append(tag_script);
}