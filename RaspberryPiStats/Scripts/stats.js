$(function () {

    $.ajax({
        type: "GET",
        url: "/stats/getstats",
        cache: false,
        success: DataRetrived,
        dataType: "json"
    });

});

function DataRetrived(data) {
    var d1 = [];
    for (var i = 0; i < data.length; i++) {
        console.log(data[i]);
        d1.push([data[i].creationDate, data[i].memoryUsed]);
    }

    var begin = new Date();
    begin.setTime(data[0].creationDate);
    var end = new Date();
    end.setTime(data[data.length - 1].creationDate);

    $.plot("#placeholder",
            [{ label: "Node 2", data: d1 }],
            {
                series: { lines: { show: true } },
                xaxis: {
                    mode: "time",
                    minTickSize: [1, "minute"],
                }
            }
        );
}