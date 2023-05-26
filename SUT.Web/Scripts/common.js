$(function () {

    $.ajaxSetup({
        cache: false
    });

    jQuery.validator.addMethod("ruc", function (value, element, params) {
        return this.optional(element) || jcd.validator.validarRUC(value) || !params;
    }, "El valor ingresado no es un RUC válido");

    $.extend($.validator.messages, {
        required: "Este campo es obligatorio.",
        remote: "Por favor, llene este campo.",
        email: "Por favor, escriba un correo electrónico válido.",
        url: "Por favor, escriba una URL válida.",
        date: "Por favor, escriba una fecha válida.",
        dateISO: "Por favor, escriba una fecha (ISO) válida.",
        number: "Por favor, escriba un número válido.",
        digits: "Por favor, escriba sólo dígitos.",
        creditcard: "Por favor, escriba un número de tarjeta válido.",
        equalTo: "Por favor, escriba el mismo valor de nuevo.",
        extension: "Por favor, escriba un valor con una extensión permitida.",
        maxlength: $.validator.format("Por favor, no escriba más de {0} caracteres."),
        minlength: $.validator.format("Por favor, no escriba menos de {0} caracteres."),
        rangelength: $.validator.format("Por favor, escriba un valor entre {0} y {1} caracteres."),
        range: $.validator.format("Por favor, escriba un valor entre {0} y {1}."),
        max: $.validator.format("Alerta: Valor TUPA mayor al {0}% ."),
        min: $.validator.format("Por favor, escriba un valor mayor o igual a {0}."),
        max1: $.validator.format("Alerta: Valor TUPA mayor al 100% {0}."),
        min1: $.validator.format("Alerta: Valor TUPA mayor al 100% {0}."),
        nifES: "Por favor, escriba un NIF válido.",
        nieES: "Por favor, escriba un NIE válido.",
        cifES: "Por favor, escriba un CIF válido.",

        integer: 'Debe ingresar solo números.'
    });




    $('.icheck').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '20%' // optional
    });

    $('.select2').select2();

});

function showNotification(from, align, mensaje, Tipo) {
    var EstadoNot;
    if (Tipo == 1) {
        //style = '<div data-notify="container" style="background: rgb(45, 203, 101);color: #FFFFFF;" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert"><button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div></div>';
        EstadoNot = "Exito - " + mensaje + "";
        $.notify(EstadoNot, "success");
    } else if (Tipo == 2) {
        EstadoNot = "Información - " + mensaje + "";
        $.notify(EstadoNot, "info");
        //style = '<div data-notify="container" style="background: rgb(186, 151, 22);color: #FFFFFF;" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert"><button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div></div>';
    } else if (Tipo == 3) {
        //style = '<div data-notify="container" style="background: rgb(208, 71, 79);color: #FFFFFF;" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert"><button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div></div>';
        EstadoNot = "Peligro - " + mensaje + "";
        $.notify(EstadoNot, "error");
    } else if (Tipo == 4) {
        //style = '<div data-notify="container" style="background: rgb(208, 71, 79);color: #FFFFFF;" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert"><button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div></div>';
        EstadoNot = "Advertencia - " + mensaje + "";
        $.notify(EstadoNot, "warn");
    }


    //$.notify({
    //    icon: "pe-7s-gift",
    //    //message: "Welcome to <b>Light Bootstrap Dashboard</b> - a beautiful freebie for every web developer."
    //    message: EstadoNot, 
    //},
    //    {
    //        timer: 50,
    //        z_index: 2000,
    //        placement: {
    //            from: from,
    //            align: align
    //        }
    //    });
}