var jcd = {};
var App = {};

((context) => {
    if (!context) context = {};

    context.valid = {
        isDate: (date) => {
            return date instanceof Date && !isNaN(date.valueOf());
        }
    };

    context.string = {
        format: function () {
            var s = arguments[0];
            for (var i = 0; i < arguments.length - 1; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                s = s.replace(reg, arguments[i + 1]);
            }
            return s;
        }
    };

    context.create = (className, config = null) => {
        var instance = null;
        switch (className) {
            case 'JsonStore': instance = new jcdJsonStore(config); break;
            case 'Button': instance = new jcdButton(config); break;
            case 'TextBox': instance = new jcdTextBox(config); break;
            case 'ComboBox': instance = new jcdComboBox(config); break;
            case 'Paginator': instance = new jcdPaginator(config); break;
            case 'Grid': instance = new jcdGrid(config); break;
            case 'GridColumn': instance = new jcdGridColumn(config); break;
            case 'RowNumberColumn': instance = new jcdRowNumberColumn(config); break;
            case 'DateColumn': instance = new jcdDateColumn(config); break;
            case 'TextBoxColumn': instance = new jcdTextBoxColumn(config); break;
            case 'ActionColumn': instance = new jcdActionColumn(config); break;
        }

        return instance;
    };

    class jcdBase {
        constructor(config = null) {
            if (config.id) {
                this.id = config.id;

                if (App[this.id]) throw new SyntaxError('Ya existe un objeto con el id ' + this.id);
                else App[this.id] = this;
            }
        }
    };
    class jcdStore extends jcdBase {
        constructor(config = null) {
            super(config);
            if (config) {
                this.config = config;
                this.recordId = config.recordId;
                this.fields = config.fields;// { name: 'Nombre', dataIndex: 'nombre', fn, type:'' }
                this.pageSize = config.pageSize;
                this.pageIndex = config.pageIndex;
                this.sort = config.sort;

                this.data = [];
                this.view = [];

                if (config.ajax) {
                    this.ajax = config.ajax;
                    if (this.ajax.autoLoad) this.reload();
                } else if(config.data) this.processData(config.data);
            } else {
                throw new SyntaxError('Parametros requeridos');
            }
        }
        addContainer(cmp) {
            if (!this.containers) this.containers = [];
            this.containers.push(cmp);
        }
        reload(model = null) {
            if (this.ajax) {
                if (!model) model = {};

                if (this.ajax.data) {
                    for (var key in this.ajax.data) {
                        if (!model[key]) {
                            if (typeof this.ajax.data[key] === 'function') model[key] = this.ajax.data[key]();
                            else model[key] = this.ajax.data[key];
                        }
                    }
                }

                if (!model.pageIndex) model.pageIndex = this.pageIndex;
                else this.pageIndex = model.pageIndex;

                if (!model.pageSize) model.pageSize = this.pageSize;

                var store = this;
                if (store.containers) {
                    $.each(store.containers, (i, r) => {
                        if (r.maskLoading) r.maskLoading();
                    });
                }

                $.getJSON(this.ajax.url, model,
                    (result) => {
                        if (result) {
                            store.total = result.total;
                            store.processData(result.data);
                            if (store.containers) {
                                $.each(store.containers, (i, r) => {
                                    r.update();
                                    if (r.maskLoading) r.maskLoading(false);
                                });
                            }
                        }
                    }).fail((e) => {
                        throw new SyntaxError('Error en ajax');
                    });
            } else throw new SyntaxError('No existe configuracion ajax');
        }
        setData(data) {
            this.processData(data);
        }
        processData(lst) {

        }
        getById(id) {
            var record = null;
            if (this.recordId && this.view)
                record = this.view.find((currentValue, index, arr) => {
                    return currentValue[this.recordId] === id;
                });
            return record;
        }
        getField(name) {
            var field = this.fields.find((currentValue, index, arr) => {
                return currentValue.name === name;
            });
            return field;
        }
        getData() {
            return this.view;
        }
        count() {
            var n = 0;
            if (this.view) n = this.view.length;
            return n;
        }
    };
    class jcdJsonStore extends jcdStore {
        constructor(config = null) {
            super(config);
        }
        processData(lst) {
            var lista = [];
            $.each(lst, (i, r) => {
                var item = {};
                $.each(this.fields, (k, f) => {
                    if (f.fn) {
                        item[f.name] = f.fn(r[f.dataIndex ? f.dataIndex : f.name]);
                    } else {
                        item[f.name] = r[f.dataIndex ? f.dataIndex : f.name];
                    }
                    if (f.type) {
                        switch (f.type) {
                            case 'date':
                                if (!context.valid.isDate(item[f.name])) item[f.name] = new Date(Date.parse(item[f.name]));
                                break;
                        }
                    }
                });
                lista.push(item);
            });
            this.data = lista;

            this.view = this.data;
        }
        
    };
    class jcdControl extends jcdBase {
        constructor(className, config = null) {
            super(config);

            this._className = className || '';
        }
        render() {
            throw new SyntaxError('Esta clase no puede renderizarse');
        }
        renderTo(container) {
            container.append(this.render());
            //if (this.update) this.update();
        }
        $() {
            return $(context.string.format('#{0}_{1}', this._className, this.id));
        }
        val(value) {
            if (value) {
                this.$().val(value);
            } else {
                return this.$().val();
            }
        }
    };
    class jcdButton extends jcdControl {
        constructor(config = null) {
            super('jcdButton', config);
            if (config) {
                this.text = config.text;
                this.icon = config.icon;
                this.type = config.type;
                this.typeButton = config.typeButton;
                this.handler = config.handler;
                this.aditionalClass = config.aditionalClass;
            }
        }
        render() {
            var control = $('<a></a>')
                .addClass('btn')
                .addClass(this.typeButton ? 'btn-' + this.typeButton : 'btn-default')
                .addClass(this.aditionalClass)
                .append($('<i></i>').addClass(this.icon))
                ;
            if (this.text) control.append(' ' + this.text);
            if (this.handler) {
                for (var fnName in this.handler) {
                    control.on(fnName, (e) => { this.handler[fnName](e); });
                }
            }
            return control;
        }
    };
    class jcdTextBox extends jcdControl {
        constructor(config = null) {
            super('jcdTextBox', config);
            if (config) {
                this.value = config.name;
                this.value = config.value;
                this.aditionalClass = config.aditionalClass;
                this.handler = config.handler;
            }
        }
        render() {
            var control = $('<input />')
                .attr('id', 'jcdTextBox_' + this.id)
                .attr('type', 'text')
                .addClass('form-control')
                .addClass(this.aditionalClass)
                .val(this.value || '')
                ;

            if (this.name) control.attr('name', this.name);

            if (this.handler) {
                for (var fnName in this.handler) {
                    control.on(fnName, (e) => { this.handler[fnName](e); });
                }
            }
            return control;
        }
    };
    class jcdComboBox extends jcdControl {
        constructor(config = null) {
            super('jcdComboBox', config);
            if (config) {
                this.name = config.name;
                this.value = config.value;
                this.displayField = config.displayField;
                this.valueField = config.valueField;

                this.aditionalClass = config.aditionalClass;
                this.handler = config.handler;

                if (config.store) {
                    if (typeof config.store == 'string') this.store = App[config.store];
                    else this.store = config.store;

                    this.store.addContainer(this);
                }
                else throw new SyntaxError('No se ha indicado el store');
            }
        }
        render() {
            var control = $('<select></select>')
                .attr('id', 'jcdComboBox_' + this.id)
                .addClass('form-control')
                .addClass(this.aditionalClass)
                ;

            if (this.name) control.attr('name', this.name);

            if (this.handler) {
                for (var fnName in this.handler) {
                    control.on(fnName, (e) => { this.handler[fnName](e); });
                }
            }
            return control;
        }
        update() {
            var control = this.$();
            control.find('option').remove();
            if (this.store.count() > 0) {
                $.each(this.store.getData(), (index, record) => {
                    var opt = $('<option></option>')
                        .attr('value', record[this.valueField])
                        .html(record[this.displayField])
                        ;
                    if (this.value == record[this.valueField]) opt.attr('selected', 'selected');
                    control.append(opt);
                });
            }
        }
    };
    class jcdPaginator extends jcdControl {
        constructor(config = null) {
            super('jcdPaginator', config);

            this.buttonNames = config.buttonNames || ['<<', '<', '>', '>>'];
            this.pages = config.pages || 7;
            this.cssClass = config.cssClass || 'pagination pagination-sm pull-right';

            if (config.store) {
                if (typeof config.store == 'string') this.store = App[config.store];
                else this.store = config.store;

                this.store.addContainer(this);
            }
            else throw new SyntaxError('No se ha indicado el store');
        }
        render() {
            var page = this.store.pageIndex;
            var fn = (p) => context.string.format('javascript:App.{2}.reload({0} pageIndex: {3} {1})', '{', '}', this.store.id, p);
            var nPaginas = Math.ceil((this.store.total || 0) / this.store.pageSize);

            var ul = $('<ul>');
            ul.attr('id', 'jcdPaginator_' + this.id);
            ul.addClass(this.cssClass);

            var li = $('<li>');
            li.addClass(page == 1 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(this.buttonNames[0]);
            li.children('a').attr('href', page == 1 ? '#' : fn(1));
            ul.append(li);

            li = $('<li>');
            li.addClass(page == 1 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(this.buttonNames[1]);
            li.children('a').attr('href', page == 1 ? '#' : fn(page - 1));
            ul.append(li);

            if (nPaginas > 1) {
                var g = page;
                if (page < Math.ceil(this.pages / 2)) g = Math.ceil(this.pages / 2);
                if (page > nPaginas - Math.ceil(this.pages / 2) + 1) g = nPaginas - Math.ceil(this.pages / 2) + 1;

                var ini = g - Math.ceil(this.pages / 2) + 1 <= 0 ? 1 : g - Math.ceil(this.pages / 2) + 1;
                var fin = nPaginas <= this.pages ? nPaginas : g - Math.ceil(this.pages / 2) + this.pages;

                for (var i = ini; i <= fin; i++) {
                    li = $('<li>');
                    li.addClass(page == i ? 'active' : '');
                    li.append($('<a></a>'));
                    li.children('a').html(i);
                    li.children('a').attr('href', page == i ? '#' : fn(i));
                    ul.append(li);
                }
            }


            li = $('<li>');
            li.addClass(page == nPaginas || nPaginas == 0 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(this.buttonNames[2]);
            li.children('a').attr('href', page == nPaginas || nPaginas == 0 ? '#' : fn(page + 1));
            ul.append(li);


            li = $('<li>');
            li.addClass(page == nPaginas || nPaginas == 0 ? 'disabled' : '');
            li.append($('<a>'));
            li.children('a').html(this.buttonNames[3]);
            li.children('a').attr('href', page == nPaginas || nPaginas == 0 ? '#' : fn(nPaginas));
            ul.append(li);

            return ul;
        }
        update() {
            var old = $('#jcdPaginator_' + this.id);
            if (old) {
                //var parent = old.parent();
                var paginator = this.render();
                paginator.insertBefore(old);
                old.remove();
            }
        }
    };
    class jcdGrid extends jcdControl {
        constructor(config = null) {
            super('jcdGrid', config);
            if (config) {
                this.columns = config.columns;
                this.header = config.header;
                this.footer = config.footer;

                if (config.store) {
                    if (typeof config.store == 'string') this.store = App[config.store];
                    else this.store = config.store;

                    this.store.addContainer(this);
                }
                else throw new SyntaxError('No se ha indicado el store');
            }
        }
        render() {
            var grid = $('<div></div>')
                .addClass('table-responsive')
                .append(
                $('<table></table>')
                    .attr('id', 'jcdGrid_' + this.id)
                    .addClass('table table-bordered table-striped table-hover')
                    .append((index, html) => {
                        var head = $('<thead></thead>');
                        var tr = $('<tr></tr>');
                        if (this.columns) {
                            $.each(this.columns, (i, r) => {
                                var th = r.renderHeader(this);
                                if (r.config.width) th.css('width', r.config.width);
                                tr.append(th);
                            });
                        }
                        head.append(tr);

                        if (this.header) {
                            tr = $('<tr></tr>');
                            $.each(this.header, (i, r) => {
                                var td = $('<td></td>');

                                if (typeof r === 'string') td.append(r);
                                else if (typeof r === 'function') td.append(r());
                                else {
                                    if (r.component) td.append(r.component.render());
                                    else if (r.text) td.append(r.text);
                                    else if (r.render) td.append(r.render());
                                }
                                if (r.colspan) td.attr('colspan', r.colspan);

                                tr.append(td);
                            });
                            head.append(tr);
                        }

                        return head;
                    })
                    .append($('<tbody></tbody>'))
                    .append((index, html) => {
                        var tfoot = $('<tfoot></tfoot>');
                        if (this.footer) {
                            if (typeof this.footer === 'string') this.footer = [App[this.footer]];
                            if ($.isArray(this.footer)) {
                                $.each(this.footer, (i, r) => {
                                    tfoot.append(
                                        $('<tr></tr>').append(
                                            $('<td></td>').attr('colspan', this.columns.length)
                                                .append(r.render())
                                        )
                                    )
                                });
                            }
                            else throw new SyntaxError('Debe indicar un array');
                        }
                        return tfoot;
                    })
                );
            return grid;
        }
        update() {
            var grid = $('#jcdGrid_' + this.id + ' tbody');
            grid.find('tr').remove();
            if (this.store.count() > 0) {
                $.each(this.store.getData(), (index, record) => {
                    var tr = $('<tr></tr>');
                    $.each(this.columns, (i, c) => {
                        tr.append(c.render(index, record, this));
                    });
                    grid.append(tr);
                });
            } else {
                grid.append($('<tr></tr>')
                    .append(
                        $('<td></td>')
                        .attr('colspan', this.columns.length)
                        .css({ 'text-align': 'center', 'font-weight': 'bold' })
                        .html('NO SE ENCONTRARON REGISTROS')
                    )
                );
            }
        }
        updateHeaders() {
            var grid = $('#jcdGrid_' + this.id);
            var tr = grid.find('thead tr').eq(0);
            tr.find('th').remove();
            if (this.columns) {
                $.each(this.columns, (i, r) => {
                    var th = r.renderHeader(this);
                    if (r.config.width) th.css('width', r.config.width);
                    tr.append(th);
                });
            }
        }
        maskLoading(show = true) {
            var grid = $('#jcdGrid_' + this.id);
            var parent = grid.parent().parent().parent().parent();
            if (show) {
                parent.prepend(
                    $('<div></div>')
                        .addClass('overlay')
                        .append($('<i></i>').addClass('fa fa-refresh fa-spin'))
                );
            } else {
                var mask = parent.find('div.overlay')
                if (mask) mask.remove();
            }
        }
    };
    class jcdGridColumn {
        constructor(config) {
            if (config) {
                this.config = config;
                this.name = config.name;
                this.style = config.style;
                this.dataIndex = config.dataIndex;
                this.sortable = config.sortable;
            } else {
                throw new SyntaxError('Debe indicarse el parametro config');
            }
        }
        getName() {
            var n = '';
            if (this.name) n = this.name;
            return n;
        }
        renderHeader(grid) {
            var th = $('<th></th>');
            var st = grid.store;
            if (this.sortable) {
                var a;
                var field = st.getField(this.dataIndex || this.getName());
                var sort = st.sort || { name: '', direction: '' };
                var fnSort = (sortObj) => {
                    st.sort = sortObj;
                    st.reload();
                    grid.updateHeaders();
                };

                if (sort.name == field.name) {
                    a = $('<a></a>')
                        //.addClass('jcdGridHeaderSortable')
                        .css('cursor', 'pointer')
                        .append(this.getName() + ' ')
                        .append($('<i></i>').addClass('fa fa-sort-amount-' + sort.direction));
                    a.click((e) => {
                        fnSort({ name: field.name, direction: sort.direction === 'asc' ? 'desc' : 'asc' });
                    });
                } else {
                    a = $('<span></span>')
                        //.addClass('jcdGridHeaderSortable')
                        .css('cursor', 'pointer')
                        .append(this.getName() + ' ');
                    a.click((e) => {
                        fnSort({ name: field.name, direction: 'asc' });
                    });
                }
                th.append(a);
            } else {
                th.append(this.getName());
            }
            return th;
        }
        render(index, record, grid) {
            var td = $('<td></td>');

            if (this.renderColumn) {
                td.append(this.renderColumn(index, record, grid));
            } else {
                td.append(record[this.dataIndex ? this.dataIndex : this.getName()]);
            }
            
            if (this.style) {
                if (typeof this.style === 'function') td.css(this.style({ index: index, record: record, grid: grid }));
                else td.css(this.style);
            }

            return td;
        }
    };
    class jcdRowNumberColumn extends jcdGridColumn {
        constructor(config) {
            super(config)
        }
        renderColumn(index, record, grid) {
            var result = '';
            result = (grid.store.pageIndex - 1) * grid.store.pageSize + index + 1;
            return result;
        }
    };
    class jcdDateColumn extends jcdGridColumn {
        constructor(config) {
            super(config)
        }
        renderColumn(index, record, grid) {
            var result = '';
            var date = record[this.dataIndex ? this.dataIndex : this.getName()];
            if (context.valid.isDate(date)) {
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                result = (day < 10 ? '0' + day : day) + '/' + (month < 10 ? '0' + month : month) + '/' + year;
            }
            return result;
        }
    };
    class jcdTextBoxColumn extends jcdGridColumn {
        constructor(config) {
            super(config)
            this.nameTemplate = config.nameTemplate;
        }
        renderColumn(index, record, grid) {
            var val = record[this.dataIndex ? this.dataIndex : this.getName()];
            var input = $('<input />')
                .attr('type', 'text')
                .addClass('form-control')
                .val(val || '')
                ;
            if (this.nameTemplate) input.attr('name', context.string.format(this.nameTemplate, index));
            return input;
        }
    };
    class jcdActionColumn extends jcdGridColumn {
        constructor(config) {
            super(config)
            this.commands = config.commands;
        }
        renderColumn(index, record, grid) {
            var div = $('<div></div>').addClass('btn-group');
            if (this.commands) {
                if (!$.isArray(this.commands)) this.commands = [this.commands];
                $.each(this.commands, (i, r) => {
                    var btn = $('<a></a>').addClass('btn btn-xs');
                    btn.addClass(r.color || 'btn-default');
                    if (r.icon) btn.append($('<i></i>').addClass(r.icon));
                    btn.append(r.text || '');
                    if (r.href) btn.attr('href', r.href);
                    if (r.callback) btn.click((e) => { r.callback({ event: e, index: index, record: record, grid: grid }); });
                    div.append(btn);
                });
            }
            return div;
        }
    };

}) (jcd);