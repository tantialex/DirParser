function getGraphDataSets() {

    const loadProcedures = function(Graph, callback) {
        $.get('procedureReport.json', function(json) {
            const data = JSON.parse(json);
            console.log(data);
            Graph
            .cooldownTicks(200)
            .nodeLabel('id')
            .nodeAutoColorBy('group')
            .forceEngine('ngraph')
            .graphData(data);

            callback(data);
        });
    };

    const loadProjects = function(Graph, callback) {
        $.get('projectReport.json', function(json) {
            const data = JSON.parse(json);
            console.log(data);
            Graph
            .cooldownTicks(200)
            .nodeLabel('id')
            .nodeAutoColorBy('group')
            .forceEngine('ngraph')
            .graphData(data);

            callback(data);
        });
    };


    loadProcedures.description = "<em>Procedures</em> data (<a href='https://bl.ocks.org/mbostock/4062045'>4062045</a>)";
    loadProjects.description = "<em>Projects</em> data (<a href='https://bl.ocks.org/mbostock/4062045'>4062045</a>)";

    return [loadProcedures, loadProjects];
}