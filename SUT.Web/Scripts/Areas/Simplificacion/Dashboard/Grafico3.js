
function stringtoArray3(myString) {
    return myString.slice(1, -1).split(',').map(val => parseInt(val));
}
//const currentURL = window.location.href;
//var { region, provincia, nivelgob } = obtenerParametrosURL(currentURL);

$.ajax({
    url: `ObtenerDatosJson?iopsp=5&parameter1=${region}&parameter2=${provincia}&parameter3=${nivelgob}`,
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let result = JSON.parse(response.result);
        let aresult = []
        for (let row of result) {
            aresult.push({ name: row.name, data: stringtoArray3(row.data), start: row.start })
        }

     
        crearGrafico3(aresult);


    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});


function crearGrafico3(result) {

    console.log(result)

    Highcharts.chart('grafico3', {

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
