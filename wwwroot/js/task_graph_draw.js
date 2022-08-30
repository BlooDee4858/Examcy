class GraphDrawer {

	constructor(settings) {
		if (settings === undefined) {
			this.settings = {
				canvasWidth: 256,
				canvasHeight: 256,
				vertexRadius: 18,
				vertexColor: '#eef',
				vertexBorderColor: '#000',
				vertexTextColor: '#000',
				vertexBorderWidth: 2,
				edgeColor: '#000',
				backgroundColor: '#fff',
				edgeLineWidth: 2,
				vertexFontSize: 25,
				showVertexLabels: true,
				arrowLength: 20,
				arrowHeight: 5
			};
		} else {
			this.settings = settings;
		}
	}

	updateGraphImage(graph, width, height) {

		let w = width / 2;
		let h = height / 2;

		let xc = w;
		let yc = h;

		let vc = graph.vertexCount;

		let angleOffset = vc % 2 == 0 ? 0.5 : -0.25;

		if (vc > 1) {

			for (let i = 0; i < vc; ++i) {

				let x = xc + w * Math.cos(Math.PI * 2 * (i + angleOffset) / vc);
				let y = yc + h * Math.sin(Math.PI * 2 * (i + angleOffset) / vc);

				graph.vertex(i).x = x;
				graph.vertex(i).y = y;
			}
		} else if (vc == 0) {
			graph.vertex(0).x = xc;
			graph.vertex(0).y = yc;
		}

	};

	vertexRadius(vertex) {
		return vertex.radius ? vertex.radius : this.settings.vertexRadius;
	}

	vertexColor(vertex) {
		return vertex.color ? vertex.radius : this.settings.vertexColor;
	}

	vertexBorderWidth(vertex) {
		return vertex.borderWidth ? vertex.radius : this.settings.vertexBorderWidth;
	}

	vertexTextColor(vertex) {
		return vertex.textColor ? vertex.radius : this.settings.vertexTextColor;
	}

	drawEdgeArrow(context, edge) {

		context.lineWidth = 0;
		context.fillStyle = edge.color ? edge.color : this.settings.edgeColor;

		// 
		let aL = this.settings.arrowLength;
		let aH = this.settings.arrowHeight;

		// Стрелка
		let xL = edge.from.x - edge.to.x;
		let yL = edge.from.y - edge.to.y;

		let len = Math.sqrt(xL * xL + yL * yL);

		let vx = 0;
		let vy = 0;
		if (len > 0.001) {
			vx = xL / len;
			vy = yL / len;
		}

		let r = this.radius(edge.to);
		let x = edge.to.x + vx * r;
		let y = edge.to.y + vy * r;
		let dx = vx;
		let dy = vy;

		context.beginPath();
		context.moveTo(x, y);
		context.lineTo(x, y);
		context.lineTo(x + dx * aL + dy * aH, y + dy * aL - dx * aH);
		context.lineTo(x + dx * aL - dy * aH, y + dy * aL + dx * aH);
		context.lineTo(x, y);
		context.stroke();
		context.fill();
	}

	drawEdge(context, edge) {
		// Соединение
		context.lineWidth = edge.width ? edge.width : this.settings.edgeLineWidth;
		context.strokeStyle = edge.color ? edge.color :
			edge.color2 ? edge.color2 :
				this.settings.edgeColor;
		context.beginPath();
		context.moveTo(edge.from.x, edge.from.y);
		context.lineTo(edge.to.x, edge.to.y);
		context.stroke();
	}

	drawVertex(context,
		vertex) {

		let r = this.vertexRadius(vertex);
		let x = vertex.x;
		let y = vertex.y;
		context.fillStyle = vertex.color ? vertex.color
			: this.settings.vertexColor;
		context.strokeStyle = vertex.borderColor ? vertex.borderColor
			: this.settings.vertexBorderColor;
		context.lineWidth = vertex.borderWidth ? vertex.borderWidth
			: this.settings.vertexBorderWidth;
		context.beginPath();
		context.arc(x, y, r, 0, 2 * Math.PI, true);
		context.fill();
		context.arc(x, y, r, 0, 2 * Math.PI, true);
		context.stroke();
		if (this.settings.showVertexLabels) {
			context.fillStyle = vertex.nameColor ? vertex.nameColor
				: this.settings.vertexTextColor;
			context.fillText(vertex.name, x, y);
		}
	};

	drawGraph(canvas, graph) {

		var width = canvas.width;
		var height = canvas.height;

		if (graph.vertexCount > 0 && graph.vertex(0).x === undefined) {
			this.updateGraphImage(
				graph,
				width - 2 * (this.settings.vertexRadius + this.settings.vertexBorderWidth * 0.5),
				height - 2 * (this.settings.vertexRadius + this.settings.vertexBorderWidth * 0.5));

			for (let i = 0; i < graph.vertexCount; ++i) {
				graph.vertex(i).x += this.settings.vertexRadius + this.settings.vertexBorderWidth * 0.5;
				graph.vertex(i).y += this.settings.vertexRadius + this.settings.vertexBorderWidth * 0.5;
			}
		}

		let context = canvas.getContext('2d');
		context.fillStyle = this.settings.backgroundColor;
		context.fillRect(0, 0, canvas.width, canvas.height);

		// Отрисовка рёбер		
		for (let i = 0; i < graph.vertexCount; ++i) {
			for (let j = 0; j < i; ++j) {
				let e1 = graph.edge(i, j);
				let e2 = graph.edge(j, i);
				if (e1.exist || e2.exist) {
					this.drawEdge(context, e1);
				}
			}
		}

		context.lineWidth = this.settings.vertexBorderWidth;
		context.strokeStyle = this.settings.vertexBorderColor;
		context.font = this.settings.vertexFontSize + "px serif";
		context.textBaseline = "middle";
		context.textAlign = "center";
		for (let i = graph.vertexCount - 1; i >= 0; --i) {
			this.drawVertex(context, graph.vertex(i));
		}

	}

}

