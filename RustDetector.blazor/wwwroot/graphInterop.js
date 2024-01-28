window.initializeLineGraph = function (data) {
    var ctx = document.getElementById('graph').getContext('2d');
    var graph = new Chart(ctx, {
        type: 'line',
        data: {
            labels: data.map(item => `${item.month}/${item.year}`),
            datasets: [
                {
                    label: 'Rust',
                    data: data.map(item => item.rustCount),
                    borderColor: 'rgba(255, 0, 0, 1)',
                    borderWidth: 2,
                    fill: false
                },
                {
                    label: 'Python',
                    data: data.map(item => item.pythonCount),
                    borderColor: 'rgba(0, 128, 0, 1)',
                    borderWidth: 2,
                    fill: false
                },
                {
                    label: 'Go',
                    data: data.map(item => item.goCount),
                    borderColor: 'rgba(0, 0, 255, 1)',
                    borderWidth: 2,
                    fill: false
                }
            ]
        },
        options: {
            scales: {
                x: {
                    position: 'bottom',
                    title: {
                        display: true,
                        text: 'Month/Year',
                    }
                },
                y: {
                    position: 'left',
                    title: {
                        display: true,
                        text: 'Job Opening Count',
                    }
                }
            }
        }
    });
};
