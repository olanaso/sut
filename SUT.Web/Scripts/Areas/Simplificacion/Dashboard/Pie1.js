
$.ajax({
    url: 'ObtenerDatosJson?iopsp=8',
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let resultado = JSON.parse(response.result);
        console.log(resultado);
        crearPie1(resultado);
    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});


function crearPie1(result) {
    
    Highcharts.chart('pie1', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: '',
            align: 'left'
        },
   
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        series: [{
            name: 'Brands',
            colorByPoint: true,
            data: result
        }]
    });

}

//


$.ajax({
    url: 'ObtenerDatosJson?iopsp=9',
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let resultado = JSON.parse(response.result);
        console.log(resultado);
        crearPie2(resultado);
    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});


function crearPie2(result) {

    Highcharts.chart('pie2', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        colors: ['purple', 'red', 'green'],
        title: {
            text: '',
            align: 'left'
        },
        
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        series: [{
            name: 'Brands',
            colorByPoint: true,
            data: result
        }]
    });

}