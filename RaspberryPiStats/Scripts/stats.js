$(function () {

});

function DataRetrived(data) {
    var d1 = [];
    for (var i = 0; i < data.length; i++) {
        console.log(data[i]);
        d1.push([data[i].CurrentTime, data[i].MemoryUsage]);
    }

    $.plot("#placeholder",
            [d1],
            {
                xaxis: {
                    mode: "time",
                    minTickSize: [1, "minute"],
                }
            }
        );
}
function GatherStats() {
    var node = $("#txtNode").val();
    var run = $("#txtRun").val();
    $.ajax({
        type: "GET",
        url: "/stats/Memoryusageovertime?node=" + node + "&runname=" + run,
        cache: false,
        success: DataRetrived,
        dataType: "json"
    });
}

function GatherNumbers() {
    var run = $("#txtRunNumbers").val();
    $.ajax({
        type: "GET",
        url: "/stats/gathernumbers?runname=" + run,
        cache: false,
        success: DataRetrived,
        dataType: "json"
    });
}


function DataRetrivedNumbers(data) {
    var d1 = [];
    for (var i = 0; i < data.length; i++) {
        console.log(data[i]);
        d1.push([data[i].Node, data[i].NumberOfTransactions]);
    }

    $.plot("#placeholderNumbers",
            [d1], {
                series: { bars: { show: true, barWidth: 0.6, align: "center" } },
                xaxis: { mode: "categories" }
            }
        );
}