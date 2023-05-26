// Llamando a los totales

$(document).ajaxSend(function (event, xhr, settings) {
    $('#loadingmodal').addClass('modal');
    $('#loadingmodal').show();
   
});

$(document).ajaxComplete(function (event, xhr, settings) {
   
    $('#loadingmodal').removeClass('modal');
    $('#loadingmodal').hide();
    $('#loadingmodal').hide();
});


$.ajax({
    url: 'ObtenerDatosJson?iopsp=2',
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let result = JSON.parse(response.result);
        console.log(result)
        $('#exp_publicados').text(result[0].CANT.toLocaleString('en-US'));
        $('#exp_totales').text(result[1].CANT.toLocaleString('en-US'));
        $('#exp_regulares').text(result[2].CANT.toLocaleString('en-US'));
        $('#exp_inicial').text(result[3].CANT.toLocaleString('en-US'));
    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});

//$.ajax({
//    url: 'http://192.168.120.205:9091/resolver?t=@@TOKEN',
//    type: 'GET',
//    success: function (response) {
//        // Hacer algo con la respuesta
//        let result = JSON.parse(response.result);
//        console.log(result)
//        $('#exp_publicados').text(result[0].CANT.toLocaleString('en-US'));
//        $('#exp_totales').text(result[1].CANT.toLocaleString('en-US'));
//        $('#exp_regulares').text(result[2].CANT.toLocaleString('en-US'));
//        $('#exp_inicial').text(result[3].CANT.toLocaleString('en-US'));
//    },
//    error: function (xhr, status, error) {
//        // Manejar error
//        console.error(error);
//    }
//});
