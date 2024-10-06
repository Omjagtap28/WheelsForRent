var gaugeChart;

window.onload = function () {
    gaugeChart = new CanvasJS.Chart('gaugeChartContainer', {
        animationEnabled: true,
        title: {
            text: 'Booking Utilization'
        },
        axisY: {
            minimum: 0,
            maximum: 100,
            suffix: '%'
        },
        data: [{
            type: 'doughnut',
            yValueFormatString: '#,##0.00\"%\"',
            indexLabel: '{y}',
            dataPoints: [
                { y: bookingUtilization, color: '#5cbae6' },
                { y: (100 - bookingUtilization), color: '#e6e6e6' }
            ]
        }]
    });

    gaugeChart.render();
}
