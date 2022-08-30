var clicks = 0;
var diffopen = false;
var level = 0.002;

var curr_num;
var end_num;

var clickUp;
var clickUpSymbol;

var clickDown;
var clickDownSymbol;


var p_current_number = document.createElement("p.task-text");
var p_end_number = document.createElement("p.task-text");
var p_up_number = document.createElement("p.task-text");
var p_down_number = document.createElement("p.task-text");

function filterInt(value) {
	if (/^(\-|\+)?([0-9]+|Infinity)$/.test(value))
		return Number(value);
	return NaN;
}

function init() {
	var taskDetail = document.getElementById("TaskDetail").value;
	console.log(taskDetail);
	array_vars = taskDetail.split(' ');

	var t_clickUp = array_vars[0];
	var t_clickDown = array_vars[1];

	clickUpSymbol = t_clickUp[0]; clickDownSymbol = t_clickDown[0];
	//clickUp = filterInt(t_clickUp);
	clickUp = t_clickUp[1];
	console.log(clickUp);

	//clickDown = filterInt(t_clickDown);
	clickDown = t_clickDown[1];
	console.log(clickDown);

	curr_num = array_vars[2];
	end_num = array_vars[3];

	p_up_number.style.fontSize = "25px";
	p_down_number.style.fontSize = "25px";
	p_up_number.style.color = "#000000";
	p_down_number.style.color = "#000000";
	p_up_number.style.backgroundColor = "#ab95e3";
	p_down_number.style.backgroundColor = "#f2f85e";
	p_up_number.style.height = "40px"; p_up_number.style.width = "40px";
	p_down_number.style.height = "40px"; p_down_number.style.width = "40px";
	p_up_number.innerHTML = t_clickUp[0] + t_clickUp[1];
	p_down_number.innerHTML = t_clickDown[0] + t_clickDown[1];

	document.getElementById("task-detail").append(p_current_number);
	document.getElementById("task-detail").append(p_end_number);
	document.getElementById("task-detail").append(p_up_number);
	document.getElementById("task-detail").append(p_down_number);
}
init();

//p_current_number.className = "";
//document.getElementById("task-info").append(p_current_number);

//p_end_number.className = "task-text";
//document.getElementById("task-info").append(p_end_number);





function clickMain(typeClick) {
	if (curr_num != end_num) {
		if (typeClick == clickUp) {
			curr_num = parseInt(curr_num) + parseInt(typeClick);
		}
		else if (typeClick == clickDown) {
			if (clickDownSymbol == "*") {
				curr_num = parseInt(curr_num) * parseInt(typeClick);
			} else {
				curr_num = parseInt(curr_num) - parseInt(typeClick);
            }
		}
	}
	
}
function reload() {
	p_current_number.innerHTML = "Ваше число: " + curr_num;
	p_end_number.innerHTML = "Нужно: " + end_num;
}
var ReloadInterval = setInterval(reload, 0);

function getCoords(elem) {
	let box = document.getElementById(elem).getBoundingClientRect();

	return {
		top: box.top + window.pageYOffset,
		right: box.right + window.pageXOffset,
		bottom: box.bottom + window.pageYOffset,
		left: box.left + window.pageXOffset
	};
}

function createNumbers(num) {
	var div = document.createElement("div");
	div.id = Math.random() * 1000000000;
	div.innerText = num;
	div.style.fontSize = "25px";
	div.style.color = "#ab95e3";
	div.style.fontWeight = "bold";
	div.style.position = "fixed";
	div.class = "numDiv";
	div.style.top = Math.random() * 66 + 14 + "vh";
	div.style.left = Math.random() * 90 + "vw";
	div.style.opacity = 1;

	document.getElementById("editor").appendChild(div);
	var num = setInterval(function () {
		if (div.style.opacity < 0.101) {
			clearInterval(num);
			document.getElementById("editor").removeChild(div);
		}
		div.style.opacity -= 0.002
	}, 0);
}

function clicked() { createNumbers(clickUpSymbol + clickUp); clickMain(clickUp); clicks++; /*clickcount.innerHTML="clicks:"+clicks*/ }

function createShapes() {
	var div = document.createElement("div");
	div.id = Math.random() * 1000000000;
	div.style.fontSize = "25px";
	div.style.backgroundColor = "#ab95e3";
	div.style.fontWeight = "bold";
	div.style.position = "fixed";
	div.class = "numDiv";
	div.style.width = "50px";
	div.style.height = "50px";
	div.style.top = Math.random() * ((512 + getCoords("editor_canvas").top - 50) - getCoords("editor_canvas").top) + getCoords("editor_canvas").top + "px";
	div.style.left = Math.random() * ((512 + getCoords("editor_canvas").left - 50) - getCoords("editor_canvas").left) + getCoords("editor_canvas").left + "px";
	div.style.opacity = 1;
	div.addEventListener('click', clicked);
	document.getElementById("editor").appendChild(div);
	var shape = setInterval(function () {
		if (div.style.opacity < 0.101) {
			clearInterval(shape);
			document.getElementById("editor").removeChild(div);
		}
		div.style.opacity -= level
	}, 0);
}

var IntervalCreateShapes = setInterval(createShapes, Math.round(Math.random() * 3000))


function clickedg() { createNumbers(clickDownSymbol + clickDown); clickMain(clickDown); clicks++; /*clickcount.innerHTML="clicks:"+clicks*/ }

function createShapes2() {
	var div = document.createElement("div");
	div.id = Math.random() * 1000000000;
	div.style.fontSize = "25px";
	div.style.backgroundColor = "#f2f85e";
	div.style.fontWeight = "bold";
	div.style.position = "fixed";
	div.class = "numDiv";
	div.style.width = "50px";
	div.style.height = "50px";
	div.style.top = Math.random() * ((512 + getCoords("editor_canvas").top - 50) - getCoords("editor_canvas").top) + getCoords("editor_canvas").top + "px";
	div.style.left = Math.random() * ((512 + getCoords("editor_canvas").left - 50) - getCoords("editor_canvas").left) + getCoords("editor_canvas").left + "px";
	div.style.opacity = 1;
	div.addEventListener('click', clickedg);
	document.getElementById("editor").appendChild(div);
	var shape = setInterval(function () {
		if (div.style.opacity < 0.101) {
			clearInterval(shape);
			document.getElementById("editor").removeChild(div);
		}
		div.style.opacity -= level
	}, 0);
}

var IntervalCreateShapes2 = setInterval(createShapes2, Math.round(Math.random() * 8000))



function check_task() {
	if (curr_num == end_num) {
		clearInterval(IntervalCreateShapes2); clearInterval(IntervalCreateShapes);
		clearInterval(ReloadInterval);
		createNotification("/img/7.png", "Решено верно!");
		document.getElementById("Aswer").value = curr_num;
		clearInterval(check_timer);
		setTimeout(() => {
			//redirect
		}, 10000);
	}
	if (clickDownSymbol == "*" && curr_num > end_num) {
		clearInterval(IntervalCreateShapes2); clearInterval(IntervalCreateShapes);
		clearInterval(ReloadInterval);
		createNotification("/img/YouCanMakeMistakes.svg", "Ошибаться можно!");
		clearInterval(check_timer);
		setTimeout(() => {
			//redirect
		}, 10000);
    }
}
var check_timer = setInterval(check_task, 2000);