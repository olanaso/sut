//$('#example1').DataTable()
let miTabla4 =  $('#tabla4').DataTable({
    'paging': true,
    "bTfoot": false,
    'scrollY': '230px',
    'scrollCollapse': true,
    'lengthChange': false,
    'searching': true,
    'ordering': true,
    'info': false,
    "bInfo": false,
    'autoWidth': false,
    'dom': 'Bfrtip',
    'buttons': [
        'copy', 'csv', 'excel', 'pdf', 'print'
    ],
    "language": {
        "sProcessing": "Procesando...",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningún dato disponible en esta tabla",
        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Último",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        }
        ,
        "initComplete": function (settings, json) {

        }



    }
})

//const currentURL = window.location.href;
//var { region, provincia, nivelgob } = obtenerParametrosURL(currentURL);

$.ajax({
    url: `ObtenerDatosJson?iopsp=12&parameter1=${region}&parameter2=${provincia}&parameter3=${nivelgob}`,

    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let datos = JSON.parse(response.result);
        //debugger
        console.log(datos)

        $.each(datos, function (index, value) {
            miTabla4.row.add([

                value['Nombre'],
                value['Acronimo'],
                value['Roles'],
                value['Activo'],
                value['Inactivo'],
               
            ]);
        });

        miTabla4.draw();


    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});