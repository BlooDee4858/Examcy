//document.querySelector('body').style

const notificationElem = document.querySelector('.notification')//не актуально
var timeout_open_notification = 500; //пол секунды
var timeout_notification = 9000; //девять секунд

//Для уведомления верного решения
function createNotification() {
	var notif = document.createElement("div");
	notif.classList += "notification";

	//создание img и p с добавлением классов
	var notif_img = document.createElement("img"); var notif_title = document.createElement("p");
	notif_img.classList += "notif_img"; notif_title.classList += "notif_title";
	//Заполнение
	var src_img = "/img/7.png";  var src_p = "Решено верно!";
	notif_img.src = src_img; notif_title.innerText = src_p;
	//Добавление к уведомлению
	notif.appendChild(notif_img); notif.appendChild(notif_title);
	//Вывод уведомления
	document.getElementById("body").appendChild(notif);

	//таймер открытия уведомления
	setTimeout(() => {
		notif.classList += " open";
	}, timeout_open_notification);
	//таймер закрытия уведомления
	setTimeout(() => {
		notif.classList.remove("open");
	}, timeout_notification - timeout_open_notification);
	//таймер удаления уведомления
	setTimeout(() => {
		document.getElementById("body").removeChild(notif);
	}, timeout_notification);
}
//"Универсальное" уведомление с двумя параметрами
function createNotification(_img,_title) {
	var notif = document.createElement("div");
	notif.classList += "notification";
	//создание img и p с добавлением классов
	var notif_img = document.createElement("img");	var notif_title = document.createElement("p");
	notif_img.classList += "notif_img";				notif_title.classList += "notif_title";
	//Заполнение
	notif_img.src = _img;							notif_title.innerText = _title;
	//Добавление к уведомлению
	notif.appendChild(notif_img);					notif.appendChild(notif_title);
	//Вывод уведомления
	document.getElementById("body").appendChild(notif);

	//таймер открытия уведомления
	setTimeout(() => {
		notif.classList += " open";
	}, timeout_open_notification);
	//таймер закрытия уведомления
	setTimeout(() => {
		notif.classList.remove("open");
	}, timeout_notification - timeout_open_notification);
	//таймер удаления уведомления
	setTimeout(() => {
		document.getElementById("body").removeChild(notif);
	}, timeout_notification);
}

//для открытия окна из консоли браузера
window.createNotification = createNotification;