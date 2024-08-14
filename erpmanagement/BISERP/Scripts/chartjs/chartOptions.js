var ChartToolTip = {
    backgroundColor: "#f5f5f5",
    titleFontColor: "#333",
    bodyFontColor: "#000",
    bodySpacing: 4,
    xPadding: 12,
    mode: "nearest",
    intersect: 0,
    position: "nearest"
}

var ChartToolTip_Index = {
    titleFontSize: 10,
    titleSpacing: 0,
    bodyFontSize: 10,
    mode: "index",
    intersect: true,
    position: 'average',
    yAlign: 'center'
}

var LineChart_ScaleXaxis = [
    {
        gridLines: {
            drawBorder: false,
            color: "rgba(225,78,202,0.2)",
            zeroLineColor: "rgba(225,78,202,0.1)"
        },
        ticks: {
            beginAtZero: true,
            padding: 0,
            //fontColor: "#f0e68c"
            //fontFamily: "Verdana",
            fontColor: "#000",
        },
        scaleLabel: {
            display: true,
            //labelString: 'Sale Amount',
            //fontColor: "#f0e68c"
            fontColor: "#000",
            fontFamily: 'Verdhana',
            font: "10px",
        },
    }
];

var LineChart_ScaleYaxis =
    [
        {
            gridLines: {
                drawBorder: true,
                color: "rgba(225,78,202,0.2)",
                zeroLineColor: "rgba(225,78,202,0.1)"
            },
            scaleLabel: {
                display: true,
                //labelString: 'Sale Amount',
                //fontColor: "#f0e68c"
                fontColor: "#000",
                fontFamily: 'Verdhana',
                font: "10px",
            },
            ticks: {
                beginAtZero: true,
                padding:0 ,
                //fontColor: "#f0e68c"
                //fontFamily: "Verdana",
                fontColor: "#000",
            }
        }
    ];

var BarChart_ScaleYaxis = [
    {
        gridLines: {
            drawBorder: true,
            color: "rgba(225,78,202,0.2)",
            color: "rgba(225,78,202,0.1)",
        },
        ticks: {
            beginAtZero: true,
            padding: 0,
            fontColor: "#000",
        },
        scaleLabel: {
            display: true,
            //labelString: 'Sale Amount',
            //fontColor: "#f0e68c"
            fontColor: "#000",
            fontFamily: 'Verdhana',
            font: "10px",
        }
    }
];
var BarChart_ScaleXaxis = [
    {
        gridLines: {
            drawBorder: true,
            color: "rgba(225,78,202,0.2)",
            color: "rgba(225,78,202,0.1)",
        },
        ticks: {
            padding: 2,
            fontFamily: "Verdana",
            fontColor: "#000000",
        },
        scaleLabel: {
            display: true,
            //labelString: 'Sale Amount',
            //fontColor: "#f0e68c"
            fontColor: "#000",
            fontFamily: 'Verdhana',
            font: "10px",
        }

    }
];

var LineChart_options = {
    animation: {
        duration: 200
    },
  maintainAspectRatio: false,
    legend: {
        display: false,
        labels: {
            //fontColor: "#f0e68c",
            fontColor: "#000",
            fontSize: 11,
            padding: 5,
        }
    },
    layout: {
        padding: {
            left: -10,
            right: 20,
            top: 0,
            bottom: -15
        }
    },
    tooltips: {
        backgroundColor: "#f5f5f5",
        titleFontColor: "#333",
        bodyFontColor: "#666",
        bodySpacing: 50,
        titleFontSize: 10,
        titleSpacing: 5,
        mode: "nearest",
        intersect: 0,
        position: "nearest",
        bodyFontSize: 14,
        displayColors: false
    },
  responsive: true,
  scales: {
      yAxes: LineChart_ScaleYaxis,
      xAxes: LineChart_ScaleXaxis
  }
};

var BarChart_options = {
    animation: {
        duration: 1000
    },
    layout: {
        padding: {
            left: -10,
            right: 20,
            top: 0,
            bottom: -15
        }
    },
    maintainAspectRatio: false,
    legend: {
        display: false,
        labels: {
            //fontColor: "#f0e68c",
            fontColor: "#000",
            fontSize: 11
        }
    },
    tooltips: ChartToolTip,
    responsive: true,
    scales: {
        yAxes: BarChart_ScaleYaxis,
        xAxes: BarChart_ScaleXaxis
    }
}

var DoughnutChart_options = {
    animation: {
        duration: 2000
    },
    maintainAspectRatio: false,
    legend: {
        display: false,
        labels: {
            fontColor: "#000",
            fontSize: 11
        }
    },
    //tooltips: ChartToolTip,
    animation: {
        animateScale: true,
        animateRotate: true
    },
    responsive: true,
}

