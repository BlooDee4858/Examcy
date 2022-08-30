class Vertex {

	constructor(graph, index, edges, name) {
		this.graph = graph;
		this.index = index;
		this.edges = edges;
		this.name = name;
		this.inDegree = 0;
		this.outDegree = 0;
	}

	edgeTo(vertex) {
		if (vertex instanceof Vertex) {
			vertex = vertex.index;
		}
		return this.edges[vertex];
	}

	remove() {
		this.graph.vertexCount -= 1;
		this.graph.vertexes.splice(this.index, 1);
		for (let i = 0; i < this.graph.vertexes.length; ++i) {
			let vertex = this.graph.vertex(i);
			vertex.index = i;
			vertex.edgeTo(this.index).remove();
			this.edgeTo(i).remove();
			vertex.edges.splice(this.index, 1);
		}
	}

	get degree() {
		if (this.inDegree == this.outDegree) {
			return this.inDegree;
		}
	}

}

class Edge {

	constructor(graph, from, to) {
		this.graph = graph;
		this.from = from;
		this.to = to;
		this.exist = false;
		this.weight = undefined;
		this.color = undefined;
	}

	create() {
		if (this.exist) {
			return false;
		}
		++this.from.outDegree;
		++this.to.inDegree;
		this.exist = true;
		return true;
	}

	remove() {
		if (!this.exist) {
			return false;
		}
		--this.from.outDegree;
		--this.to.inDegree;
		this.exist = false;
		return true;
	}
}

class Graph {

	constructor(vertexCount) {
		this.vertexCount = vertexCount;
		this.vertexes = [];
		for (let i = 0; i < vertexCount; ++i) {
			this.vertexes[i] = new Vertex(this, i, [], (i + 1).toString());
		}
		for (let i = 0; i < vertexCount; ++i) {
			for (let j = 0; j < vertexCount; ++j) {
				this.vertexes[i].edges.push(
					new Edge(this,
						this.vertexes[i],
						this.vertexes[j]));
			}
		}
	}

	vertex(i) {
		return this.vertexes[i];
	}

	edge(i, j) {
		return this.vertex(i).edgeTo(j);
	}

	isUnoriented() {
		for (let i = 0; i < this.vertexCount; ++i) {
			for (let j = 0; j < this.vertexCount; ++j) {
				if (i == j) {
					if (this.edge(i, j).exist) {
						return false;
					}
				} else {
					if (this.edge(i, j).exist !== this.edge(j, i).exist) {
						return false;
					}
				}
			}
		}
		return true;
	}

	encodeToGraph6() {
		let offset = 63;
		let bitVector = 0;
		let idx = 5;
		let result = String.fromCharCode(this.vertexCount + offset);
		for (let i = 0; i < this.vertexCount; ++i) {
			for (let j = 0; j < i; ++j) {
				if (this.edge(i, j).exist) {
					bitVector |= 1 << idx;
				}
				if (idx-- == 0) {
					result += String.fromCharCode(bitVector + 63);
					idx = 5;
					bitVector = 0;
				}
			}
		}
		if (idx != 5) {
			result += String.fromCharCode(bitVector + 63);
		}
		return result;
	}

	static fromGraph6(graph6) {
		let offset = 63;
		let vertexCount = graph6.charCodeAt(0) - offset;
		let bitVector = [];
		for (let i = 1; i < graph6.length; ++i) {
			let code = graph6.charCodeAt(i) - offset;
			for (let bit = 5; bit >= 0; --bit) {
				bitVector[bitVector.length] = ((code >> bit) & 1);
			}
		}
		let graph = new Graph(vertexCount);
		let idx = 0;
		for (let i = 0; i < vertexCount; ++i) {
			for (let j = 0; j < i; ++j) {
				if (bitVector[idx++]) {
					graph.edge(i, j).create();
					graph.edge(j, i).create();
				}
			}
		}
		return graph;
	}

	getEdgeCount() {
		let eCnt = 0;
		for (let i = 0; i < this.vertexCount; ++i) {
			for (let j = i + 1; j < this.vertexCount; ++j) {
				if (this.getEdge(i, j) !== 0) {
					++eCnt;
				}
			}
		}
		return eCnt;
	}

	addVertex(name) {
		let idx = this.vertexCount;
		++this.vertexCount;
		this.vertexes.push(new Vertex(this, idx, [], name));
		let newVertex = this.vertex(idx);
		for (let i = 0; i < idx; ++i) {
			let vertex = this.vertex(i);
			vertex.index = i;
			vertex.edges.push(new Edge(this, vertex, newVertex));
			newVertex.edges.push(new Edge(this, newVertex, vertex));
		}
		newVertex.edges.push(new Edge(this, newVertex, newVertex));
		return newVertex;
	}

	clone() {
		let n = this.vertexCount;
		let g = new Graph(n);
		for (let i = 0; i < n; ++i) {
			for (let j = 0; j < n; ++j) {
				if (this.edge(i, j).exist) {
					g.edge(i, j).create();
				}
			}
		}
		return g;
	}

}
