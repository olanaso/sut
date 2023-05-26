sut.editPlanTrabajo = {
    guardar: () => {
        debugger;
        if ($("#form-editar").valid()) {
            $('#form-editar input[name="ArchivoAdjuntoId"]').val($('#form-archivo-adjunto input[name="ArchivoAdjuntoId"]').val()); 
            $('#form-editar input[name="ArMotivoAdjuntoId"]').val($('#form-armotivo-adjunto input[name="ArMotivoAdjuntoId"]').val());

            var model = $('#form-editar').serialize();
            $.ajax({
                type: "POST",
                //url: "/sut/Simplificacion/Expediente/GuardarNorma", 
                url: "/Simplificacion/Expediente/GuardarNorma",
                data: model,
                dataType: 'json',
                beforeSend: function () {
                    $("#msgEditar").hide();
                    sut.wait.appendProgress('.modal-footer');
                    $('#form-editar button').eq(0).attr("disabled", "disabled");
                    $('#form-editar button').eq(1).attr("disabled", "disabled");
                },
                complete: function () {
                    sut.wait.removeProgress('.modal-footer');
                    $('#form-editar button').eq(0).removeAttr("disabled");
                    $('#form-editar button').eq(1).removeAttr("disabled");
                },
                success: function (result) {
                    $('#modal-ventana').modal('hide');
                    //sut.Procedimiento.NormaAprobacion.initactualizar();
                    sut.msg.success(result.mensaje, function () {
                        //sut.PlanTrabajo.listar(1);
                        //App.stNorma.reload({ExpedienteId : @Model.ExpedienteId });
                        //sut.Procedimiento.NormaAprobacion.init();
                        //sut.Procedimiento.listar(1);
                        //sut.Procedimiento.NormaAprobacion.initactualizar();
                        //sut.Procedimiento.NormaAprobacion.editar(0);
                        //sut.Procedimiento.NormaAprobacion.lista();
                        //$('#divNorma').html(data);
                        window.location.reload();
                    });
                },
                error: function (result) {
                    sut.wait.removeProgress('.modal-footer');
                    $('#form-editar button').eq(0).removeAttr("disabled");
                    $('#form-editar button').eq(1).removeAttr("disabled");
                    sut.error.show('msgEditar', result.responseText);
                }
            });
        }
    },
    guardarpublicado: () => {
        debugger;
        if ($("#form-editar").valid()) {
            $('#form-editar input[name="ArchivoAdjuntoId"]').val($('#form-archivo-adjunto input[name="ArchivoAdjuntoId"]').val()); 
            $('#form-editar input[name="ArMotivoAdjuntoId"]').val($('#form-armotivo-adjunto input[name="ArMotivoAdjuntoId"]').val());

            var model = $('#form-editar').serialize();
            $.ajax({
                type: "POST",
                //url: "/sut/Simplificacion/Expediente/GuardarPublicado",
                url: "/Simplificacion/Expediente/GuardarPublicado",
                data: model,
                dataType: 'json',
                beforeSend: function () {
                    $("#msgEditar").hide();
                    sut.wait.appendProgress('.modal-footer');
                    $('#form-editar button').eq(0).attr("disabled", "disabled");
                    $('#form-editar button').eq(1).attr("disabled", "disabled");
                },
                complete: function () {
                    sut.wait.removeProgress('.modal-footer');
                    $('#form-editar button').eq(0).removeAttr("disabled");
                    $('#form-editar button').eq(1).removeAttr("disabled");
                },
                success: function (result) {
                    $('#modal-ventana').modal('hide');
                    //sut.Procedimiento.NormaAprobacion.initactualizar();
                    sut.msg.success(result.mensaje, function () {
                        //sut.PlanTrabajo.listar(1);
                        //App.stNorma.reload({ExpedienteId : @Model.ExpedienteId });
                        //sut.Procedimiento.NormaAprobacion.init();
                        //sut.Procedimiento.listar(1);
                        //sut.Procedimiento.NormaAprobacion.initactualizar();
                        //sut.Procedimiento.NormaAprobacion.editar(0);
                        //sut.Procedimiento.NormaAprobacion.lista();
                        //$('#divNorma').html(data);
                        window.location.reload();
                    });
                },
                error: function (result) {
                    sut.wait.removeProgress('.modal-footer');
                    $('#form-editar button').eq(0).removeAttr("disabled");
                    $('#form-editar button').eq(1).removeAttr("disabled");
                    sut.error.show('msgEditar', result.responseText);
                }
            });
        }
    }
};
