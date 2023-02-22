var testado;
var apuntos = new Array();

class clspunto {
    constructor(secuencia, idpunto, nombre, descripcion, latitud, longitud, idicono, estado, idpuntousadocliente, idcolor, idcategoria, idsubcategoria, categoriageneral, accion) {
        this.secuencia = secuencia;
        this.idpunto = idpunto;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.latitud = latitud;
        this.longitud = longitud;
        this.idicono = idicono;
        this.idusuario = idusuario;
        this.estado = estado;
        this.idpuntousadocliente = idpuntousadocliente;
        this.idcolor = idcolor;
        this.idcategoria = idcategoria;
        this.idsubcategoria = idsubcategoria;
        this.categoriageneral = categoriageneral;
        this.accion = accion;
    }

    toRow() {
        return [secuencia, idpunto, nombre, descripcion, latitud, longitud, idicono, estado, idpuntousadocliente, idcolor, idcategoria, idsubcategoria, categoriageneral, accion];
    }
}

$(document).ready(function () {
    testado = $('#grdflota').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json",
            "decimal": ".",
            "thousands": ","
        },
        "dom": "BftrSp",
        "fixedColumns": {
            leftColumns: 2
        },
        "order": [2, 'asc'],
        "paging": true,
        "scrollY": '64vh',
        "scrollX": false,
        "info": false,
        "scrollCollapse": false,
        "filter": true,
        "select": true,
        "serverSide": false,
        "responsive": false,
        "processing": false,
        "columnDefs": [
            {
                targets: 1,
                className: 'noVis'
            }
        ],
        "columns": [
            {
                "data": "accion",
                "searchable": false,
                "orderable": true,
                "targets": 0,
                "sClass": "text-center",
                "visible": true
            },
            {
                "data": "secuencia",
                "searchable": false,
                "orderable": false,
                "targets": 1,
                "sClass": "text-center",
                "visible": false,
                "title": "Secuencia",
            },
            {
                "data": "idpunto",
                "searchable": false,
                "orderable": false,
                "targets": 2,
                "sClass": "text-center",
                "visible": false
            },
            {
                "data": "nombre",
                "searchable": true,
                "orderable": true,
                "targets": 3,
                "sClass": "text-center",
                "visible": true,
                "title": "PUNTO",
            },
            {
                "data": "descripcion",
                "searchable": true,
                "orderable": true,
                "targets": 4,
                "sClass": "text-center",
                "visible": false
            },
        ],
    });

    $('#grdflota tbody').on('click', 'tr', function () {
        var data = testado.row(this).data();

        data.etiqueta = 'new';
        data = null;
    });

    llenargrid();
    //document.getElementById("pnlcargando").style.display = 'none';
    document.getElementById("grdflota").style.display = '';
});

function getClientSize() {
    let width = 0;
    let height = 0;

    try {
        if (typeof (window.innerWidth) == 'number') {
            width = window.innerWidth;
            height = window.innerHeight;
        } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
            width = document.documentElement.clientWidth;
            height = document.documentElement.clientHeight;
        } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
            width = document.body.clientWidth;
            height = document.body.clientHeight;
        }
        return { width: width.toString().replace('px', ''), height: height.toString().replace('px', '') };
    }
    catch (Error) {
        console.log(Error);
    }
    finally {
        width = null;
        height = null;
    }
}

function llenargrid() {
    try {
        var ind = 1;
        var info;
        testado.clear();
        apuntos.forEach((punto) => {
            try {
                info = null;
                info = punto.split('_');
                testado.row.add(new clspunto(ind, info[1], info[0], info[4], parseFloat(info[2]), parseFloat(info[3]), info[6], 'A', info[5], info[6], info[7], info[9], info[8],
                    '<img id="imgPoint" src="../../images/tags.png" style="cursor:pointer" data-toggle="modal" data-target="#mventana" onclick="editarpunto(' + info[1] + ',' + String(ind - 1) + ')" />'));
            }
            catch (Error) {
                console.log(Error);
            }
            ind += 1;
        });
        ind = null;
        info = null;

        testado.columns.adjust().draw();
    }
    catch (Error) {
        console.log(Error);
    }
}