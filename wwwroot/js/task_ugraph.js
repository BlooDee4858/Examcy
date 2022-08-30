var answer = "";

function AddTableInfo() {
	var upper_diagonal = document.getElementById("TaskDetail").value;
	var question_matrix = document.createElement('table');
	question_matrix.innerHTML = "";

	let number_vertex = upper_diagonal[0];
	let counter = 1;
	let matrix = new Array();

	for (let i = 0; i <= number_vertex; i++) {
		var newRow = question_matrix.insertRow(i);
		matrix[i] = new Array();
		for (let j = 0; j <= number_vertex; j++) {
			var newCell = newRow.insertCell(j);
			if (i == 0 & j != 0) {
				newCell.innerHTML = j;
			}
			else if (i != 0 & j == 0) {
				newCell.innerHTML = i;
			}
			else if (i == 0 & j == 0) {
				newCell.innerHTML = "*";
			}
			else {
				if (i <= j) {
					matrix[i][j] = upper_diagonal[counter];
					newCell.innerHTML = matrix[i][j];
					counter++;
					answer += matrix[i][j];
				}
				else {
					newCell.innerHTML = matrix[j][i];
					answer += matrix[j][i];
				}
			}
		}
	}
	document.getElementById("TaskResult").value = answer;
	document.getElementById("task-detail").appendChild(question_matrix);
}
//window.AddTableInfo = AddTableInfo;
AddTableInfo();


// ADD SCRIPTS
var tag_script_graph = document.createElement("script");
tag_script_graph.src = "/js/task_graph.js";
document.body.append(tag_script_graph);

var tag_script_graph_draw = document.createElement("script");
tag_script_graph_draw.src = "/js/task_graph_draw.js";
document.body.append(tag_script_graph_draw);

var tag_script_graph_editor = document.createElement("script");
tag_script_graph_editor.src = "/js/task_graph_editor.js";
document.body.append(tag_script_graph_editor);

var tag_script_graph_handler = document.createElement("script");
tag_script_graph_handler.src = "/js/task_graph_handler.js";
document.body.append(tag_script_graph_handler);


