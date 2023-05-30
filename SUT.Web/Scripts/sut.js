sut = {
    msg: {
        success: function (msg, fn) {
            fn = fn == null ? function () { } : fn;
            //bootbox.alert(msg, fn);
            swal({
                title: 'Éxito!',
                text: msg,
                type: 'success'
            }).then(
              fn,
              function (dismiss) {
                  fn();
              }
            );
        }, 
        alerta: function (msg, fn) {
            fn = fn == null ? function () { } : fn;
            //bootbox.alert(msg, fn); 
            swal({
                title: 'Alerta!',
                text: msg.replace(',', '\r\r\r\r\r'),
                type: 'question'
            }).then(
                fn,
                function (dismiss) {
                    fn();
                }
            );
        },
        alerta2: function (msg, fn) {
            fn = fn == null ? function () { } : fn;
            //bootbox.alert(msg, fn);
            swal({
                title: 'Alerta!',
                text: msg,
                type: 'question'
            }).then(
                fn,
                function (dismiss) {
                    fn();
                }
            );
        },
        confirm: function (msg, fn) {
            fn = fn == null ? function () { } : fn;
            
            swal({
                title: '¿Estás seguro?',
                text: msg,
                type: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar',
                cancelButtonText: 'Cancelar'
            }).then(function () {
                fn();
            });
        },
        salir: function (msg, fn, urlSalir) {
            fn = fn == null ? function () { } : fn;

            swal({
                title: '¿Estás seguro?',
                text: msg,
                type: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {
                if (isConfirm) {
                    swal.showLoading();
                    fn();
                } else {

                }
            }, function (dismiss) {
                urlSalir();
            });

        },
        CamposVacios: function (msg, fn) {


            swal({
                title: '¿Estás seguro?',
                text: msg,
                type: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {
                if (isConfirm)
                    fn();
                else
                    swal.close();
            });
        },
        msgUsuario: function (msg, tittle) {
            swal({
                title: tittle,
                text: msg,
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {

                swal.close();
            });
        }
        ,
        msgError2: function (title, message) {

            //let error = JSON.parse(message);

            var existingToast = document.getElementById("toast");
            if (existingToast) {
                existingToast.remove();
            }

            var toastHTML = `
  <div id="toastsut">
    <span id="closeBtn">&times;</span>
    <p id="toastTitle"></p>
    <p id="toastMessage"></p>
  </div>
`;
            document.body.insertAdjacentHTML('beforeend', toastHTML);

            var css = `
            #toastsut {
      visibility: hidden;
      max-width: 50%;
      margin: auto;
      background-color: red;
      color: #fff;
      text-align: center;
      border-radius: 2px;
      padding: 16px;
      position: fixed;
      z-index: 1;
      left: 50%;
      top: 50%;
      transform: translate(-50%, -50%);
      font-size: 17px;
max-height: 500px;  
    overflow-y: auto; 


    }
    .show {
      visibility: visible !important;
    }
    #closeBtn {
      color: #aaa;
      font-size: 22px;
      line-height: 20px;
      cursor: pointer;
      position: absolute;
      top: 5px;
      right: 10px;
    }
    #toastTitle {
      font-weight: bold;
    }
    #toastMessage {
      margin-top: 10px;
    }
           `;

            var head = document.head || document.getElementsByTagName('head')[0];
            var style = document.createElement('style', '');
            style.type = 'text/css';
            if (style.styleSheet) {
                style.styleSheet.cssText = css;
            } else {
                style.appendChild(document.createTextNode(css));
            }
            head.appendChild(style);

            let toastEl = document.getElementById("toastsut");
            let toastTitleEl = document.getElementById("toastTitle");
            let toastMessageEl = document.getElementById("toastMessage");
            let closeBtnEl = document.getElementById("closeBtn");

            toastTitleEl.innerHTML = title;
            toastMessageEl.innerHTML = message;

            // '<b>Valores errados en el formato:</b><br> ' + error.lista.join('<br>');
            toastEl.classList.add("show");

            closeBtnEl.onclick = function () {
                toastEl.classList.remove("show");
            };
        }
    },
    

    table: {
        selectRow: function (table, multi, fn) {
            $(table).on('click', 'tbody tr td', null, function (e) {
                if (multi) {
                    var chk = $(e.target).find('input');
                    if (chk.length == 0) {
                        chk = $(e.target).parent().find('td input');
                        //chk.iCheck(chk.is(':checked') ? 'uncheck' : 'check');
                        chk.prop('checked', !chk.is(':checked'));
                    }
                } else {
                    fn($(e.target).parent());
                }
            });
        }
    },

    error: {
        show: function (idTarget, htmlText) {
            $('#' + idTarget).html(htmlText).hide();
            $('html, body').animate({ scrollTop: 0 }, '500', 'swing', function () {
                $('#' + idTarget).slideDown(500, function () { });
            });
        },
        clientErrorShow: function (idTarget, error) {
            var error = (error == null ? [] : error);
            error = Array.isArray(error) ? error : [error];
            var htmlText = '<div class="alert alert-danger alert-dismissible" style="height: inherit;padding: inherit; display: block;">'
                     + '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>'
                     + '<ul>';
            $.each(error, function (i, r) {
                htmlText += '<li>' + r + '</li>';
            });
            htmlText += '</ul></div>';
            this.show(idTarget, htmlText);
        }
    },

    wait: {
        modal: function (idModal) {
            var htmlText = '<div class="modal-dialog">'
                       + '<div class="modal-content modal-width">'
                       + '<div class="modal-header">'
                       + '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>'
                       + '<h4 class="modal-title">Cargando</h4>'
                       + '</div>'
                       + '<div class="modal-body form-horizontal">'
                       + '<div id="uploadProgress" class="progress progress-striped active">'
                       + '<div class="progress-bar" role="progressbar" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100" style="width: 100%">'
                       + '<span class="sr-only">Cargando</span>'
                       + '</div>'
                       + '</div>'
                       + '</div>'
                       + '<div class="modal-footer">'
                       + '</div>'
                       + '</div>'
                       + '</div>';
            $("#" + idModal).html(htmlText);
        },
        appendProgress: function (selector) {
            var htmlText = '<div class="progress progress-striped active sut-progressbar ">'
                       + '<div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100" style="width: 100%">'
                             + '<span class="sr-only">Cargando</span>'
                       + '</div>'
                        + '</div>';

            var a = selector.charAt(0);
            if (a != '.' && a != '#') selector = '#' + selector;

            $(selector).prepend(htmlText);
        },
        removeProgress: function (selector) {
            var a = selector.charAt(0);
            if (a != '.' && a != '#') selector = '#' + selector;

            $(selector + ' .sut-progressbar').remove();
        },
        appendOverlay: function (selector) {
            var a = selector.charAt(0);
            if (a != '.' && a != '#') selector = '#' + selector;

            $(selector).append('<div class="overlay"><i class="fa fa-refresh fa-spin"></i></div>');
        },
        removeOverlay: function (selector) {
            var a = selector.charAt(0);
            if (a != '.' && a != '#') selector = '#' + selector;

            $(selector + ' .overlay').remove();
        }
    },

    string: {
        format: function () {
            var s = arguments[0];
            for (var i = 0; i < arguments.length - 1; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                s = s.replace(reg, arguments[i + 1]);
            }
            return s;
        }
    },

    pagination: {
        //btnNames: ['Primero', 'Anterior', 'Siguiente', 'Último'],
        btnNames: ['<<', '<', '>', '>>'],
        btnPages: 7,
        btnClass: 'pagination pagination-sm pull-right',
        update: function (table, btnPages, btnNames, btnClass) {
            if (btnPages == null) btnPages = sut.pagination.btnPages;
            if (btnNames == null) btnNames = sut.pagination.btnNames;
            if (btnClass == null) btnClass = sut.pagination.btnClass;

            var paginator = $('#' + table.data('paginator'));
            var page = table.data('page');
            var fn = table.data('function');
            var nPaginas = Math.ceil(table.data('totalrows') / table.data('pagesize'));
            paginator.html(null);

            var ul = $('<ul>');
            ul.addClass(btnClass);

            var li = $('<li>');
            li.addClass(page == 1 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(btnNames[0]);
            li.children('a').attr('href', page == 1 ? '#' : sut.string.format('javascript:{0}(1)', fn));
            ul.append(li);

            li = $('<li>');
            li.addClass(page == 1 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(btnNames[1]);
            li.children('a').attr('href', page == 1 ? '#' : sut.string.format('javascript:{0}({1})', fn, page - 1));
            ul.append(li);

            if (nPaginas > 1) {
                var g = page;
                if (page < Math.ceil(btnPages / 2)) g = Math.ceil(btnPages / 2);
                if (page > nPaginas - Math.ceil(btnPages / 2) + 1) g = nPaginas - Math.ceil(btnPages / 2) + 1;

                var ini = g - Math.ceil(btnPages / 2) + 1 <= 0 ? 1 : g - Math.ceil(btnPages / 2) + 1;
                var fin = nPaginas <= btnPages ? nPaginas : g - Math.ceil(btnPages / 2) + btnPages;

                for (var i = ini; i <= fin; i++) {
                    li = $('<li>');
                    li.addClass(page == i ? 'active' : '');
                    li.append($('<a></a>'));
                    li.children('a').html(i);
                    li.children('a').attr('href', page == i ? '#' : sut.string.format('javascript:{0}({1})', fn, i));
                    ul.append(li);
                }
            }


            li = $('<li>');
            li.addClass(page == nPaginas || nPaginas == 0 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(btnNames[2]);
            li.children('a').attr('href', page == nPaginas || nPaginas == 0 ? '#' : sut.string.format('javascript:{0}({1})', fn, page + 1));
            ul.append(li);


            li = $('<li>');
            li.addClass(page == nPaginas || nPaginas == 0 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(btnNames[3]);
            li.children('a').attr('href', page == nPaginas || nPaginas == 0 ? '#' : sut.string.format('javascript:{0}({1})', fn, nPaginas));
            ul.append(li);

            paginator.append(ul);
        }
    },

    format: {
        decimal: function (value) {
            //return Number(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
            //return Number(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,').replace(',','');

            //var num = theform.original.value, rounded = theform.rounded
            //var with2Decimals = num.toString().match(/^-?\d+(?:\.\d{0,2})?/)[0]
            //rounded.value = with2Decimals

            return Number(value).toFixed(3).slice(0, -1);
        },
        decimalEspecial: function (value, n) {
            var re = '\\d(?=(\\d{3})+' + (n > 0 ? '\\.' : '$') + ')';
            return Number(value).toFixed(Math.max(0, ~ ~n)).replace(new RegExp(re, 'g'), '$&,').replace(',', '');
        },
        decimalEspecialTrunc: function (value, n) {
            let t = value.toString();
            let regex = /(\d*.\d{0,1})/;
            return t.match(regex)[0];
        },
        decimal2EspecialTrunc: function (value, n) {
            //let t = value.toString();
            //let regex = /(\d*.\d{0,2})/;
            //return t.match(regex)[0];

            let t = value.toString();
            let regex = /(\d*.\d{0,2})/;
            return t.match(regex)[0];
        },
        decimalEspecialdos: function (number, length) {
            function pad(input, length, padding) {
                var str = input + "";
                return (length <= str.length) ? str : pad(str + padding, length, padding);
            }
            var str = number + "";
            var dot = str.lastIndexOf('.');
            var isDecimal = dot != -1;
            var integer = isDecimal ? str.substr(0, dot) : str;
            var decimals = isDecimal ? str.substr(dot + 1) : "";
            decimals = pad(decimals, 2, 0);
            return integer + '.' + decimals;
        }
    },

    validator: {
        validarRUC: function (ruc) {
            var factores = new String("5432765432");
            var ultimoIndex = ruc.length - 1;
            var sumaTotal = 0, residuo = 0;
            var ultimoDigitoRUC = 0, ultimoDigitoCalc = 0;
	    
            for(var i = 0; i < ultimoIndex; i++) {
                sumaTotal += (parseInt(ruc.charAt(i)) * parseInt(factores.charAt(i)));
            }
            residuo = sumaTotal%11;
	    
            ultimoDigitoCalc = (residuo == 10) ? 0: ((residuo == 11) ? 1:(11 - residuo)%10);
            ultimoDigitoRUC = parseInt(ruc.charAt(ultimoIndex));
	    
            return ultimoDigitoRUC == ultimoDigitoCalc;
        }
    },

    control: {
         auto_grow: function(element) {
             element.style.height = "20px"; 
            element.style.height = (element.scrollHeight)+"px"; 
            element.style.overflow = "hidden";  
        },
        uploadfileFecha1: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Ing_Sol"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Ing_Sol"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfileFecha2: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Emision_Cert_ITSE"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Emision_Cert_ITSE"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfileFecha3: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Notificacion_Cert_ITSE"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Notificacion_Cert_ITSE"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },

        uploadfileFecha4: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Emision_Licencia_Funcionamiento"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Emision_Licencia_Funcionamiento"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfileFecha5: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Notificacion_Licencia_Funcionamiento"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Notificacion_Licencia_Funcionamiento"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        }, 

        uploadfileFecha11: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Suspencion_ITSE"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Suspencion_ITSE"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        }, 


        uploadfileFecha6: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Suspencion_ITSE_Subsanacion"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Suspencion_ITSE_Subsanacion"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        }, 
        uploadfileFecha7: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Resolucion_denegatoria"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Resolucion_denegatoria"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        }, 
        uploadfileFecha10: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="Archivo_Fec_Revocacion_Licencia_Funcionamiento"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="Archivo_Fec_Revocacion_Licencia_Funcionamiento"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        }, 


        uploadfile: function (formId) {
         
            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="ArchivoAdjuntoId"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="ArchivoAdjuntoId"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                       
                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfileTodo: function (formId) {

            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="ArchivoAdjuntoId"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });


            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/SUT/General/Adjunto/Upload",
                    url: "/General/Adjunto/UploadTodo",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos con Extensión .pdf, .xls, .xlsx, .doc, .docx, .ppt, .pptx");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos con Extensión .pdf, .xls, .xlsx, .doc, .docx, .ppt, .pptx");
                            return;
                        }
                        $('#' + formId + ' input[name="ArchivoAdjuntoId"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfilemotivo: function (formId, pantalla) {
            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="ArMotivoAdjuntoId"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });
            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/sut/General/MotivoAdjunto/Upload",
                    url: "/General/MotivoAdjunto/Upload",
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        if (result == "") {
                            sut.msg.confirm('El archivo no es un pdf', () => { 
                                $('#' + formId + ' .file-list').addClass('file-empty');
                            });
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF");
                            return;
                        }
                        $('#' + formId + ' input[name="ArMotivoAdjuntoId"]').val(result.ArMotivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/MotivoAdjunto/Descargar/' + result.ArMotivoAdjuntoId).html(result.FileName);
                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/MotivoAdjunto/Descargar/' + result.ArMotivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfileword: function (formId, url1) {
            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="ArchivoAdjuntoId"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });
            $('#' + formId).submit(function (e) { 

                $.ajax({
                    //url: "/SUT/General/Adjunto/Uploadword", 
                    //url: "/General/Adjunto/Uploadword",
                    url: url1,
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {
                        debugger;
                        var ver = result.ArchivoAdjuntoId;

                        if (result.ArchivoAdjuntoId == undefined) {
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF, WORD y EXCEL");
                            return;
                        }
                        if (result == "") {
                            //sut.msg.confirm('El archivo no es un pdf', () => { 
                            //    $('#' + formId + ' .file-list').addClass('file-empty');
                            //});
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF, WORD y EXCEL");
                            return;
                        }
                        $('#' + formId + ' input[name="ArchivoAdjuntoId"]').val(result.ArchivoAdjuntoId);
                        $('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $('#' + formId + ' .file-list ul li span.text a').attr('href', '/General/Adjunto/Descargar/' + result.ArchivoAdjuntoId).html(result.FileName);

                        $("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        uploadfilemotivoword: function (formId, pantalla) {
            debugger;
            $('#' + formId + ' .file-list ul li div.tools i').on('click', function (e) {
                sut.msg.confirm('Desea eliminar el archivo adjunto.', () => {
                    $('#' + formId + ' input[name="ArMotivoAdjuntoId"]').val('0');
                    $('#' + formId + ' .file-list').addClass('file-empty');
                });
            });
            $('#' + formId + ' input[name="PostedFile"]').on('change', function (e) {
                $('#' + formId).submit();
            });
            $('#' + formId).submit(function (e) {
                $.ajax({
                    //url: "/sut/General/MotivoAdjunto/Uploadword",
                    //url: "/General/MotivoAdjunto/Uploadword",
                    url: pantalla,
                    type: 'POST',
                    data: new FormData(this),
                    cache: false,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //$('#msgEditar').hide();
                        //sut.wait.append('.modal-footer');
                    },
                    complete: function () {
                        //sut.wait.remove('.modal-footer');
                    },
                    success: function (result) {

                        debugger;
                        if (result == "") {
                            sut.msg.confirm('El archivo no es un pdf', () => {
                                $('#' + formId + ' .file-list').addClass('file-empty');
                            });
                            sut.error.clientErrorShow('msgEditar', "Solo se Puede Cargar Archivos PDF y WORD");
                            return;
                        } 
                        //$('#' + formId + ' input[name="ArMotivoAdjuntoId"]').val(result.ArMotivoAdjuntoId);
                        //$('#' + formId + ' .file-list').removeClass('file-empty');
                        //$('#' + formId + ' .file-list ul li span.text a').attr('href', '/sut/General/MotivoAdjunto/Descargar/' + result.ArMotivoAdjuntoId).html(result.FileName);

                        //$("#msgEditar").css("display", "none");
                        sut.msg.success('Archivo cargado correctamente.');
                        sut.ArchivoMod.listar(1);
                    },
                    error: function (result) {
                        //sut.wait.remove('.modal-footer');
                        sut.error.show('msgEditar', result.responseText);
                    }
                });

                e.preventDefault();
            });
        },
        Actualizartexto: function () {


            // application
            var speed2 = 60;
            var j2 = document.querySelector('j2');
            var l = document.querySelector('l1');
            var delay = j2.innerHTML.length * speed2 + speed2;

            // type affect to header
            typeEffect2(j2, speed2);

            // type affect to body
            setTimeout(function () {
                l.style.display = "inline-block";
                typeEffect2(l, speed2);

            }, delay);

            function typeEffect2(element, speed2) {
                var text = element.innerHTML.trim();

                //alert(text.length);
                element.innerHTML = "";

                var r = 0;
                var timer1 = setInterval(function () {

                    if (r < text.length) {
                        element.append(text.charAt(r));
                        r++;
                    } else {
                        $('#clic').show();
                        clearInterval(timer1);
                    }
                }, speed2);
            } 
        },
        Actualizartexto: function () {


            // application
            var speed2 = 60;
            var j2 = document.querySelector('j2');
            var l = document.querySelector('l1');
            var delay = j2.innerHTML.length * speed2 + speed2;

            // type affect to header
            typeEffect2(j2, speed2);

            // type affect to body
            setTimeout(function () {
                l.style.display = "inline-block";
                typeEffect2(l, speed2);

            }, delay);

            function typeEffect2(element, speed2) {
                var text = element.innerHTML.trim();

                //alert(text.length);
                element.innerHTML = "";

                var r = 0;
                var timer1 = setInterval(function () {

                    if (r < text.length) {
                        element.append(text.charAt(r));
                        r++;
                    } else {
                        $('#clic').show();
                        clearInterval(timer1);
                    }
                }, speed2);
            } 
        }

 },
    compareArrays(a, b) {
        if (a.length !== b.length) {
            return { areEqual: false, diff: [...a, ...b] };
        }
        let diff = [];
        for (let i = 0; i < a.length; i++) {
            if (!b.includes(a[i])) {
                diff.push(a[i]);
            }
        }
        for (let i = 0; i < b.length; i++) {
            if (!a.includes(b[i])) {
                diff.push(b[i]);
            }
        }
        if (diff.length === 0) {
            return { areEqual: true, diff: [] };
        }
        return { areEqual: false, diff: diff };
    }
   , subirExcel: function (file,cabecerasdefinidas) {


        return new Promise((resolve, reject) => {

           // fileInput.addEventListener('change', (event) => {
                //const file = event.target.files[0]; // el primer archivo

                console.log('Archivo seleccionado:', file.name);

                const reader = new FileReader();

                reader.onload = (e) => {

                    const data = new Uint8Array(e.target.result);

                    const workbook = XLSX.read(data, { type: 'array' });

                    // Leer la primera hoja de cálculo
                    const sheet = workbook.Sheets[workbook.SheetNames[0]];

                    // Convertir los datos a un arreglo de objetos
                    const jsonData = XLSX.utils.sheet_to_json(sheet);

                    if (jsonData.length == 0) {

                        this.msg.msgError2("Archivo Vacio", `El archivo no tiene ningúna fila registrada.`)
                        return;
                    }

                    // ahora jsonData[0] debe contener las cabeceras
                    const headers = Object.keys(jsonData[0]);
                    let comparearrays = this.compareArrays(headers, cabecerasdefinidas)
                    console.log(cabecerasdefinidas)

                    // validar las cabeceras
                    if (comparearrays.areEqual) {
                        return resolve(jsonData);
                    }
                    else {
                        this.msg.msgError2("Error en las cabeceras", `Las cabeceras ${comparearrays.diff.join(',')} es desconocida o no está en la lista de cabeceras válidas.`)
                        return;
                    }

                    


                    //console.log(jsonData)

                    // Imprimir los datos
                  
                };

                reader.onerror = (e) => {
                    // Rechazar la promesa con el error
                    reject(e);
                };

                reader.readAsArrayBuffer(file);
            });


        //});
    }

};



/***********************************************/

//// application
//var speed = 60;
//var h1 = document.querySelector('l');
////var p = document.querySelector('l');
//var delay = h1.innerHTML.length * speed + speed;

//// type affect to header
//typeEffect(h1, speed);


//// type affect to body
////setTimeout(function () {
////    p.style.display = "inline-block";
////    typeEffect(p, speed);
////}, delay);


//function typeEffect(element, speed) {
//    var text = element.innerHTML.trim();

//    //alert(text.length);
//    element.innerHTML = "";

//    var i = 0;
//    var timer = setInterval(function () {

//        if (i < text.length) {
//            element.append(text.charAt(i));
//            i++;
//        } else {
//            clearInterval(timer);
//        }
//    }, speed);
//}





//// application
//var speed2 = 60;
//var j2 = document.querySelector('j2');
//var l = document.querySelector('l1');
//var delay = j2.innerHTML.length * speed2 + speed2;

//// type affect to header
//typeEffect2(j2, speed2);

//// type affect to body
//setTimeout(function () {
//    l.style.display = "inline-block";
//    typeEffect2(l, speed2);

//}, delay);

//function typeEffect2(element, speed2) {
//    var text = element.innerHTML.trim();

//    //alert(text.length);
//    element.innerHTML = "";

//    var r = 0;
//    var timer1 = setInterval(function () {

//        if (r < text.length) {
//            element.append(text.charAt(r));
//            r++;
//        } else {
//            $('#clic').show();
//            clearInterval(timer1);
//        }
//    }, speed2);
//}


/*********************************************/