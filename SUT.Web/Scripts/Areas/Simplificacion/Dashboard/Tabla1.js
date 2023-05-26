//$('#example1').DataTable()
//alert(1)
let mytable1 = $('#tabla1').DataTable({
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



$.ajax({
    url: 'ObtenerDatosJson?iopsp=10',
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let datos = JSON.parse(response.result);
        console.log(datos)

        $.each(datos, function (index, value) {
            mytable1.row.add([
                value['entidad'],
                value['codigoexp'],
                value['expediente'],
                value['cantproc']
            ]);
        });

        mytable1.draw();


    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});


let mytable1_1 = $('#tabla1_1').DataTable({
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





$.ajax({
    url: 'ObtenerDatosJson?iopsp=13',
    type: 'GET',
    success: function (response) {
        // Hacer algo con la respuesta
        let datos = JSON.parse(response.result);
        console.log(datos)

        $.each(datos, function (index, value) {
            mytable1_1.row.add([
                value['ESTADO'],
                value['CARGA INICIAL'],
                value['EXPEDIENTE REGULAR'],
            ]);
        });

        mytable1_1.draw();


    },
    error: function (xhr, status, error) {
        // Manejar error
        console.error(error);
    }
});
