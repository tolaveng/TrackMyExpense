// https://www.chartjs.org/docs/latest/charts/bar.html#horizontal-bar-chart
export function renderBarChart(elem, barType, title, labels, dataSet, dataLabel, hideLegend) {
    const loading = elem.querySelector('.loading');
    const chart = elem.querySelector('.chart');
    if (!chart || !loading) return;
    loading.style.display = 'none';

    const data = {
        labels: labels,
        datasets: [{
            backgroundColor: [
                '#1AC3B3',
                '#D5F1A8',
                '#96C31A',
                '#C3BF1A',
                '#E5BDBD',
                '#D400BB',
                '#1AC3A5',
                '#5FFFFF',
                '#85DBDB',
                '#96D0D0',
                '#FFFECC',
                '#FE0001',
                '#E57D7D',
                '#C9A350',
            ],
            data: dataSet,
            fill: false,
            label: dataLabel,
            axis: barType? barType: 'x',
        }]
    };

    const config = {
        type: 'bar',
        data: data,
        options: {
            indexAxis: barType ? barType : 'x',
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom',
                    align: 'center',
                    display: !hideLegend
                },
                title: {
                    display: !!title,
                    text: title,
                    position: 'top',
                    align: 'start',
                    font: {
                        size: 16
                    }
                }
            }
        }
    };

    // detory if Canvas is already
    const chartStatus = Chart.getChart(chart.id);
    if (chartStatus !== undefined) {
        chartStatus.destroy();
    }

    // Create new
    new Chart(chart, config);
}