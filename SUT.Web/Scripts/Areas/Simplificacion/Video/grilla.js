var jQuery2 = jQuery.noConflict(true);
jQuery2(document).ready(function () {

    var validarHTML5 = (pidform, pFunction) =>{

        var isvalidate = jQuery2("#" + pidform)[0].checkValidity();
        debugger

        if (isvalidate) {
            pFunction()
        } else {
            return isvalidate
        }

    }


    jQuery2('#btnGuardar').on('click', function (event) {


        //event.preventDefault();

        validarHTML5('form', function () {
            if ($('#btnGuardar').text() === 'Guardar') {
                guardar($('#btnGuardar'));
               // alert('Guardando')
            }
            if ($('#btnGuardar').text() === 'Actualizar') {
                actualizar($('#btnGuardar'));
            }
        })


    });





    var datos = [
        { videoid: 1, nombre: 'Ejemplo 1' },
        { videoid: 2, nombre: 'Ejemplo 2' },
        { videoid: 3, nombre: 'Ejemplo 3' }
    ];

    // Definir la configuración del jqGrid
    function crearGrilla() {

        var grid = jQuery2("#grid");

        grid.jqGrid({
            /*data: mydata,*/
            datatype: "local",
            height: 400,
            
            ignoreCase: true,
            caption: "Lista Videos",
            styleUI: 'Bootstrap',
            colNames: ["Video ID", "Título", "Descripción", "URL", "Path", "Duracción", "Acciones"],
            colModel: [
                {
                    name: 'videoID',
                    index: 'videoID',
                    editable: false,
                    align: "center",
                    width: 40,
                    search: false,
                    hidden: true
                },
                {
                    name: 'titulo',
                    index: 'titulo',
                    editable: false,
                   
                    width: 180,
                    search: false,
                    hidden: false
                },
                {
                    name: 'Descripcion',
                    index: 'Descripcion',
                    editable: false,
                    width: 290,
                    search: false,
                    hidden: false
                }, {
                    name: 'url',
                    index: 'url',
                    editable: false,
                    width: 280,
                    search: false,
                    hidden: false,
                    formatter: function (cellvalue, options, rowObject) {
                             return '<a target="_blank" href="' + cellvalue + '">' + cellvalue + '</a>';
                    }
                }, {
                    name: 'path',
                    index: 'path',
                    editable: false,
                    width: 250,
                    hidden: false
                }, {
                    name: 'duracion',
                    index: 'denominacion',
                    editable: false,
                    width: 90,
                    search: false,
                    hidden: false
                }, {
                    name: 'acciones',
                    index: 'acciones',
                    editable: false,
                    width: 150,
                    align: "center",
                    search: false,
                    formatter: accionesFormatter,
                    hidden: false
                }],
            pager: '#pager',
            //onSelectRow: viewGeometry,
            viewrecords: true,
            shrinkToFit: false,
            //multiselect: true
        });
        jQuery2("#grid").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

        //grid.jqGrid({

        //    datatype: "local",
        //    ignoreCase: true,
        //    celledit: true,
        //    cellsubmit: "local",
        //    multiselect: false,
        
        //    styleUI: 'Bootstrap',
        //    colModel: [
        //        { label: 'videod', name: 'videoID', key: true, width: 50 },
        //        { label: 'Título', name: 'titulo', width: 100 },
        //        { label: 'Descripción', name: 'Descripcion', width: 150 },
        //        {
        //            label: 'URL', name: 'url', width: 100, formatter: function (cellvalue, options, rowObject) {
        //                return '<a target="_blank" href="' + cellvalue + '">' + cellvalue + '</a>';
        //            }
        //        },
        //        { label: 'Path', name: 'path', width: 100 },
        //        { label: 'Duración', name: 'duracion', width: 75 },
        //        {
        //            label: 'Acciones',
        //            name: 'acciones',
        //            width: 100,
        //            formatter: accionesFormatter,
        //            align: 'center',
        //            sortable: false,
        //            search: false
        //        }

        //        // ... otras columnas según necesites
        //    ],
        //    autowidth: true,
        //    height: 400,
        //    rowNum: 10,
        //    rowList: [10, 20, 30],
        //    pager: '#pager',
        //    sortname: 'id',
        //    viewrecords: true,
        //    sortorder: 'asc',
        //    caption: 'Listado de videos'
        //});

        //Función para formatear la columna de acciones
        function accionesFormatter(cellvalue, options, rowObject) {
            var editar = '<a href="#" class="editar" videoid="' + options.rowId + '">Editar</a>';
            var eliminar = '<a href="#" class="eliminar" videoid="' + options.rowId + '">Eliminar</a>';



            return editar + ' | ' + eliminar;


        }
        grid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false, defaultSearch: "cn" });
       
    }
    


    function cargarGrilla() {
       
        $.ajax({
            url: '/Simplificacion/Video/ListarVideo',
            dataType: 'json',
            success: function (data) {
                // Aquí, 'data' es la respuesta de la solicitud AJAX.
                // Puedes transformarla como necesites antes de pasarla a jqGrid.
                let processdata = JSON.parse(data.result)
                jQuery2('#grid').jqGrid('clearGridData');
                jQuery2("#grid").jqGrid('setGridParam', { data: processdata }).trigger('reloadGrid');
               

                jQuery2('.editar').on('click', function (event) {

                    let videoId = jQuery2(this).attr("videoid");
                    var rowData = jQuery2("#grid").jqGrid('getRowData', videoId);
                    console.log(rowData); // e
                    $('#modal-default').modal('show');
                    $('#btnGuardar').text("Actualizar");
                    $('#data').data(rowData)
                    $('#txttitulo').val(rowData.titulo)
                    $('#txtdescripcion').val(rowData.Descripcion)
                    $('#txtpath').val(rowData.path)
                    $('#txturl').val(jQuery2(rowData.url).attr('href'))
                    $('#txtduracion').val(rowData.duracion)


                });

                jQuery2('.eliminar').on('click', function (event) {

                    let videoId = jQuery2(this).attr("videoid");
                    var rowData = jQuery2("#grid").jqGrid('getRowData', videoId);
                    $('#data').data(rowData)
                    $('#confirmationModal').modal('show');


                });
            }
        });

     
    }
    crearGrilla()
    cargarGrilla()



    jQuery2('#txtpath').on('focusout', function (event) {
        jQuery2('#txtpath').val(obtenerRutaCompleta(jQuery2('#txtpath').val()));
    });



    jQuery2('#btnagregar').on('click', function (event) {

        $('#form')[0].reset();
        $('#modal-default').modal('show');
        $('#btnGuardar').text("Guardar");
        $('#btnGuardar').prop('disabled', false);

    });

    function guardar(btn) {

        btn.prop('disabled', true);

        // Cambiar el texto del botón a "Guardando..."
        btn.text('Guardando...');

        let model = $('#form').serializeArray();
        model.iOpSp = 1;

        $.ajax({
            type: 'POST',
            url: "/Simplificacion/Video/RegistrarVideo",
            data: model,
            dataType: 'json',
            beforeSend: function () {
               // $("#mensajes").hide();
            },
            complete: function () {

            },
            success: function (result) {

                btn.text('Guardar');
                cargarGrilla()
                // Desbloquear el botón
                btn.prop('disabled', false);
                $('#modal-default').modal('hide');
               
            },
            error: function (result) {
                //sut.error.show('mensajes', result.responseText);
            }
        });

    };


    function actualizar(btn) {

        btn.prop('disabled', true);

        // Cambiar el texto del botón a "Guardando..."
        btn.text('Actualizando...');

        let model = $('#form').serializeArray();

        model.push({ name: 'parameter7', value: $('#data').data().videoID }) 
        

        console.log(model)

        $.ajax({
            type: 'POST',
            url: "/Simplificacion/Video/EditarVideo",
            data: model,
            dataType: 'json',
            beforeSend: function () {
                // $("#mensajes").hide();
            },
            complete: function () {

            },
            success: function (result) {

                btn.text('Guardar');
                cargarGrilla()
                // Desbloquear el botón
                btn.prop('disabled', false);
                $('#modal-default').modal('hide');

            },
            error: function (result) {
                //sut.error.show('mensajes', result.responseText);
            }
        });

    };

    function obtenerRutaCompleta(url) {
        var ruta = new URL(url).pathname;
        return ruta;
    }

    

    jQuery2('#btneliminar').on('click', function (event) {
        let model = [];
        let btn = $('#btneliminar')

        btn.prop('disabled', true);

        // Cambiar el texto del botón a "Guardando..."
        btn.text('Eliminando...');

        model.push({ name: 'parameter7', value: $('#data').data().videoID }) 

        $.ajax({
            type: 'POST',
            url: "/Simplificacion/Video/EliminarVideo",
            data: model,
            dataType: 'json',
            beforeSend: function () {
                // $("#mensajes").hide();
            },
            complete: function () {

            },
            success: function (result) {

                btn.text('Eliminar');
                cargarGrilla()
                // Desbloquear el botón
                btn.prop('disabled', false);
                $('#confirmationModal').modal('hide');

            },
            error: function (result) {
                //sut.error.show('mensajes', result.responseText);
            }
        });
        

    });

    //Ejemplo de uso
    //var ruta = obtenerRuta();
    //console.log(ruta);


    function buscador(lista, terminoBusqueda) {
        return lista.filter((video) => {
            return (
                video.titulo.toLowerCase().includes(terminoBusqueda.toLowerCase()) ||
                video.Descripcion.toLowerCase().includes(terminoBusqueda.toLowerCase()) ||
                video.path.toLowerCase().includes(terminoBusqueda.toLowerCase())
            );
        });
    }


});

