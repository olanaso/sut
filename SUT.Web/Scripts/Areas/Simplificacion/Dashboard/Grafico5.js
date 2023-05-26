
$.ajax({
    url: 'ObtenerDatosJson?iopsp=11',
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let result = JSON.parse(response.result);
        let aresult = []
        for (let row of result) {
            aresult.push({
                name: row.name
                , data: row.data.slice(1, -1).split(',').map(val => parseInt(val))
                , start: row.start
            })
        }

        $('#cant_usuarios').text(aresult[0].data.map(Number).reduce((a, b) => a + b, 0).toLocaleString('en-US'))

        crearGrafico5(aresult);


    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});

function crearGrafico5(result) {

    console.log(result)

    Highcharts.chart('grafico5', {

        title: {
            text: '',
            align: 'left'
        },

        subtitle: {
            text: 'Fuente: <a href="#" target="_blank">SUT</a>',
            align: 'left'
        },

        yAxis: {
            title: {
                text: 'Número de Expedientes'
            }
        },

        xAxis: {
            accessibility: {
                rangeDescription: 'Range: 2010 to 2023'
            }
        },

        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },

        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                },
                pointStart: result[0].start,

                dataLabels: {
                    enabled: true,
                    formatter: function () {
                        return Highcharts.numberFormat(this.y, 0, '.', ',');
                    }
                }
            },

        },

        series: result,

        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        layout: 'horizontal',
                        align: 'center',
                        verticalAlign: 'bottom'
                    }
                }
            }]
        }

    });


}
