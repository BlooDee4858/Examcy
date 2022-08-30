
class GraphEditor {

	initHandlers() {

		function doNothing() { }

		this.events = {
			onVertexSelect: doNothing,
			onVertexDeselect: doNothing,
			onVertexCreate: doNothing,
			onVertexRemove: doNothing,
			onEdgeSelect: doNothing,
			onEdgeDeselect: doNothing,
			onEdgeCreate: doNothing,
			onEdgeRemove: doNothing,
		};

		this.selectedBorderWidth = 6;
		this.selectedBorderColor = "#880";

		this.holdBorderWidth = 6;
		this.holdBorderColor = "#080";

		this.editVertexBorderWidth = 6;
		this.editVertexBorderColor = "#f00";

		let self = this;

		let oldPoint = { x: 0, y: 0 };
		let point = { x: 0, y: 0 };

		let vertexIndex = undefined;

		let hover = false;

		function transform(p) {
			let root = self.canvas;
			let offsetLeft = 0;
			let offsetTop = 0;
			while (root) {
				offsetLeft += root.offsetLeft - root.scrollLeft;
				offsetTop += root.offsetTop - root.scrollTop;
				root = root.offsetParent;
			}
			p.x = (p.x - offsetLeft) / self.canvas.clientWidth * self.canvas.width;
			p.y = (p.y - offsetTop) / self.canvas.clientHeight * self.canvas.height;
		}

		function cursorGetPosition(e, p) {
			p.x = e.pageX;
			p.y = e.pageY;
			transform(p);
		}

		function touchGetPosition(e, p) {
			let touch = e.changedTouches[0];
			p.x = touch.pageX;
			p.y = touch.pageY;
			transform(p);
		}

		function isVertexArea(i, x, y) {
			let v = self.graph.vertex(i);
			let r = v.radius ? v.radius : self.drawer.settings.vertexRadius;
			let dx = v.x - x;
			let dy = v.y - y;
			return dx * dx + dy * dy <= r * r;
		}

		function isEdgeArea(edge, x, y) {
			function distance(x1, y1, x2, y2, x, y) {
				let dist = 0;

				let dx = x2 - x1;
				let dy = y2 - y1;

				let k1 = dx * (x - x1) + dy * (y - y1);
				let k2 = dx * (x2 - x) + dy * (y2 - y);

				if (k1 < 0) {
					dx = x - x1;
					dy = y - y1;
					dist = Math.sqrt(dx * dx + dy * dy);
				} else if (k2 < 0) {
					dx = x - x2;
					dy = y - y2;
					dist = Math.sqrt(dx * dx + dy * dy);
				} else {
					let A = dy;
					let B = -dx;
					let C = x2 * y1 - x1 * y2;
					dist = Math.abs(A * x + B * y + C) / Math.sqrt(A * A + B * B);
				}
				return dist;
			}

			if (!edge.exist) {
				return false;
			}
			let width = edge.width ?
				edge.width :
				self.drawer.settings.edgeLineWidth;
			let u = edge.from;
			let v = edge.to;
			let d = distance(u.x, u.y, v.x, v.y, x, y);
			return d <= width;
		}

		let selected = undefined;
		let hold = undefined;

		let pressed = false;
		let clickCount = 0;
		let firstClickTime = new Date();
		let selectedEdgeExist = undefined;

		const clickCountDeltaTime = 500;

		this.canvas.oncontextmenu = function (e) {
			if (self.onlyMove) {
				return true;
			}
			cursorGetPosition(e, point);
			for (let i = 0; i < self.graph.vertexCount; ++i) {
				if (isVertexArea(i, point.x, point.y)) {
					if (self.graph.vertex(i) === selected) {
						self.events.onVertexDeselect(self.graph.vertex(i));
					}
					self.events.onVertexRemove(self.graph.vertex(i));
					self.graph.vertex(i).remove();
					hold = undefined;
					selected = undefined;
					e.preventDefault();
					self.draw();
					return false;
				}
			}

			// Выбрать имя
			let newVertexName = 0;
			for (let i = 0; i < self.graph.vertexCount; ++i) {
				let number = parseInt(self.graph.vertex(i).name);
				if (!isNaN(number)) {
					newVertexName = Math.max(newVertexName, number);
				}
			}
			++newVertexName;
			let v = self.graph.addVertex(newVertexName.toString());
			v.x = point.x;
			v.y = point.y;

			if (typeof self.events.onVertexCreate === "function") {
				self.events.onVertexCreate(v);
			}

			selected = v;
			if (typeof self.events.onVertexSelect === "function") {
				self.events.onVertexSelect(v);
			}

			return false;
		};

		function onPress(x, y) {
			pressed = true;
			let time = new Date();
			if (time - firstClickTime > clickCountDeltaTime) {
				firstClickTime = time;
				clickCount = 0;
			}
			++clickCount;
			let oldSelected = selected;
			if (selected) {
				if (selected instanceof Edge) {
					delete selected.color;
				} else {
					delete selected.borderColor;
					delete selected.borderWidth;
				}
				selected = undefined;
			}
			for (let i = 0; i < self.graph.vertexCount; ++i) {
				if (isVertexArea(i, x, y)) {
					selected = self.graph.vertex(i);
					if (clickCount == 1) {
						selected.borderColor = "#f00";
					} else {
						selected.borderColor = "#00f";
					}
					break;
				}
			}

			if (!selected) {
				// Вершина не выбрана
				for (let i = 0; i < self.graph.vertexCount && !selected; ++i) {
					for (let j = 0; j < i; ++j) {
						let edge = self.graph.edge(i, j);
						if (isEdgeArea(edge, x, y)) {
							selected = edge;
							selected.color = "#f00";
							break;
						}
					}
				}
			}

			if (selected !== oldSelected) {
				if (oldSelected) {
					if (oldSelected instanceof Vertex) {
						self.events.onVertexDeselect(oldSelected);
					} else {
						self.events.onEdgeDeselect(oldSelected);
					}
				}
				if (selected) {
					if (selected instanceof Vertex) {
						self.events.onVertexSelect(selected);
					} else {
						self.events.onEdgeSelect(selected);
					}
				}
			}
			self.draw();
		};

		function onMove(oldX, oldY, newX, newY) {
			let result = true;

			if (selected !== undefined && pressed && clickCount == 1) {
				selected.x += newX - oldX;
				selected.y += newY - oldY;
				result = false;
			}

			if (selected && clickCount >= 2) {
				result = false;
			}

			if (hold) {
				if (hold instanceof Vertex &&
					!isVertexArea(hold.index, point.x, point.y)) {
					if (hold !== selected) {
						hold.borderColor = hold.oldBorderColor;
						hold.borderWidth = hold.oldBorderWidth;
					}
					delete hold.oldBorderColor;
					delete hold.oldBorderWidth;
					if (selected && pressed && clickCount >= 2) {
						if (!self.onlyMove) {
							if (selectedEdgeExist) {
								selected.edgeTo(hold).create();
								hold.edgeTo(selected).create();
							} else {
								selected.edgeTo(hold).remove();
								hold.edgeTo(selected).remove();
							}
						}
					}
					hold = undefined;
				}

				if (hold instanceof Edge &&
					!isEdgeArea(hold, point.x, point.y)) {

					if (hold !== selected) {
						hold.color = hold.oldColor;
					}
					delete hold.oldColor;
					hold = undefined;
				}

			}

			if (!hold) {
				for (let i = 0; i < self.graph.vertexCount; ++i) {
					if (isVertexArea(i, newX, newY)) {
						hold = self.graph.vertex(i);
						if (hold !== selected) {
							hold.oldBorderColor = hold.borderColor;
							hold.borderColor = "#fa0";
							if (pressed && selected && clickCount >= 2) {
								if (!self.onlyMove) {
									selectedEdgeExist = selected.edgeTo(hold).exist;
									if (selectedEdgeExist) {
										selected.edgeTo(hold).remove();
										hold.edgeTo(selected).remove();
									} else {
										selected.edgeTo(hold).create();
										hold.edgeTo(selected).create();
									}
								}
							}
						}
						break;
					}
				}
			}

			if (!hold) {
				for (let i = 0; i < self.graph.vertexCount && !hold; ++i) {
					for (let j = 0; j < i; ++j) {
						let edge = self.graph.edge(i, j);
						if (isEdgeArea(edge, newX, newY)) {
							console.log("hold " + i + " " + j);
							hold = edge;
							if (hold !== selected) {
								hold.oldColor = hold.color;
								hold.color = "#fa0";
							}
							break;
						}
					}
				}
			}

			self.draw();

			return result;
		};

		function onRelease(x, y) {
			pressed = false;
			if (selected) {
				selected.borderColor = "#080";
			}
			self.draw();
		};

		// Нажатие
		self.canvas.onmousedown = function (e) {
			e.preventDefault();
			cursorGetPosition(e, oldPoint);
			onPress(oldPoint.x, oldPoint.y);
		}
		self.canvas.addEventListener("touchstart", function (e) {
			touchGetPosition(e, point);
			onPress(point.x, point.y);
		}, true);
		// Перемещение
		self.canvas.onmousemove = function (e) {
			oldPoint.x = point.x;
			oldPoint.y = point.y;
			cursorGetPosition(e, point);
			onMove(oldPoint.x, oldPoint.y, point.x, point.y);
		}
		self.canvas.addEventListener("touchmove", function (e) {
			oldPoint.x = point.x;
			oldPoint.y = point.y;
			touchGetPosition(e, point);
			if (!onMove(oldPoint.x, oldPoint.y, point.x, point.y)) {
				e.preventDefault();
			}
			return false;
		}, true);
		// Отжатие
		self.canvas.onmouseup = function (e) {
			cursorGetPosition(e, point);
			onRelease(point.x, point.y);
		}
		self.canvas.addEventListener("touchend", function (e) {
			touchGetPosition(e, point);
			return onRelease(point.x, point.y);
		}, true);

	}

	constructor(canvas, drawer, fullEdit) {
		if (typeof canvas == "string") {
			canvas = document.getElementById(canvas);
		}
		if (!drawer) {
			drawer = new GraphDrawer();
		}

		if (fullEdit) {
			this.onlyMove = false;
		} else {
			this.onlyMove = true;
		}

		this.canvas = canvas;
		this.drawer = drawer;

		this.initHandlers();
	}

	setGraph(graph) {
		this.graph = graph;
		this.draw();
	}

	draw() {
		if (this.graph) {
			this.drawer.drawGraph(this.canvas, this.graph);
		}
	}

}

