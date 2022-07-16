// https://www.chartjs.org/docs/latest/samples/information.html
export function renderChart(elem, type, title, labels, dataSets, hideLegend = false, indexAxis = 'x') {
    const loading = elem.querySelector('.loading');
    const chart = elem.querySelector('.chart');
    if (!chart || !loading) return;
    loading.style.display = 'none';

    const data = {
        labels: labels,
        datasets: dataSets,
    };

    const config = {
        type: type,
        data: data,
        options: {
            indexAxis: indexAxis,
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom',
                    align: 'center',
                    display: !hideLegend,
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