const Graph = ForceGraph3D()(document.getElementById("3d-graph"));
let curDataSetIdx;
const dataSets = getGraphDataSets();

let toggleData;

let currentNodes = null;

(toggleData = function() {
	curDataSetIdx = curDataSetIdx === undefined ? 0 : (curDataSetIdx+1)%dataSets.length;
	const dataSet = dataSets[curDataSetIdx];
	Graph.resetProps(); // Wipe current state

	dataSet(Graph, function(data){
		currentNodes = data.nodes;
	});

	document.getElementById('graph-data-description').innerHTML = dataSet.description ? `Viewing ${dataSet.description}` : '';
})(); // IIFE init