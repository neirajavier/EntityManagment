//variables globales para editar los recursos del subusuario
var vehiculos_asignados_usuario = [];
/* var puntos_asignados_usuario = [];
var geocercas_asignadas_usuario = []; */
var alertas_asignadas_usuario = [];
var modulos_asignados_usuario = [];
var grupopuntos_asignados_usuario = [];
var grupogeocercas_asignados_usuario = [];
//fin variables globales para editar los recursos del subusuario

//variable global para la busqueda de subusuarios
var busqueda = {};
busqueda.categorias = [];
busqueda.nombre = '';
busqueda.grupo = 0;
//fin variable global para la busqueda de subusuarios

//variables globales para edicion del subusuario desde la tabla subusuarios
var id_tabla_subusuario_anterior_actualizar;
var nombre_completo_subusuario_tabla;
var nombre_subusuario_tabla;
var campo_actualizar_tabla = '';
//fin variables globales para edicion del subusuario desde la tabla

//variable global para los checkeados en asignar grupos a subusuarios
var checks_grupos_asignados = [];
//finvariable global para los checkeados en asignar grupos a subusuarios

document.getElementById('busqueda_general').addEventListener('input', (e)=>{

    document.getElementById('checkbox_principal_subusuarios').checked = false;
    let size = $('#subusuarios').DataTable().data().length;
    let checkeados = 0;

    /* $('#subusuarios').DataTable().data().search(e.target.value, false, false, true).draw(); */

    busqueda.nombre = e.target.value;

    $('#subusuarios').DataTable().ajax.url(`subusuarios/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}&criterio=${JSON.stringify(busqueda)}`);
    $('#subusuarios').DataTable().ajax.reload();

    //recorrer toda la tabla para verificar si estan seleccionados todos los checks de subusuarios, si no estan todos entonces se desmarca la opcion
    /* if(document.getElementById('checkbox_principal_subusuarios').checked)
    {
        for (let i = 0; i < size; i++)
        {
            if( !$('#subusuarios').DataTable().cell(i,0).node().children[0].checked)
            {
                document.getElementById('checkbox_principal_subusuarios').checked = false;
                break;
            }
        }
    } */
})

$(document).ready( function () {
    $('#grupos').DataTable({
        /* select: true, */
        paging: false,
        info: false,
        ordering: false,
        scrollY: screen.height - ((screen.height*47)/100),
        scrollX: '100%',
        scrollCollapse: true,
        searching: false,
        language:
        {
            "lengthMenu": "Mostrar _MENU_ registros",
            "search": "Buscar:",
            "zeroRecords": " ",
        },
        columnDefs: [
            {
                target: 0,
                visible: false,
                searchable: false,
            },
            {
                target: 2,
                visible: false,
                searchable: false,
            },
            {
                target: 4,
                visible: false,
                searchable: false,
            },
        ],
        ajax: `grupos/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`

    });

    $('#subusuarios').DataTable({
        paging: true,
        pageLength: detectar_paginacion(),
        info: false,
        ordering: false,
        /* responsive: true, */
        /* fixedColumns:   {
    heightMatch: 'semiauto'
}, */

        columnDefs: [
        { width: "1%", targets: 0 },
        { width: "11%", targets: 1 },
        { width: "20%", targets: 2 },
        { width: "20%", targets: 3 },
        { width: "10%", targets: 4 },
        { width: "15%", targets: 5 },
        { width: "20%", targets: 6 }
        ],
        fixedHeader: {
            header: true,
            footer: true
        },
        language:
        {
            "lengthMenu": "",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "search": "Buscar:",
            "zeroRecords": " ",
            "emptyTable": " ",
        },
        columnDefs: [
            {
                target: 1,
                visible: false,
                searchable: false,
            },
        ],
        ajax: `subusuarios/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}&criterio=${JSON.stringify(busqueda)}`

    });

});

/* $('#grupos tbody tr').on('click', async function grupos(e)
{
    let filas = $('#grupos').DataTable().data();

    for (let i = 0; i < filas.length; i++)
    {
        $('#grupos').DataTable().row(i).node().classList.remove('seleccionada');
    }

    $('#grupos').DataTable().row(this).node().classList.add('seleccionada');

    document.getElementById('datos').classList.add('d-none');

    let divp = document.createElement('div');
    divp.classList.add('d-flex', 'justify-content-center', 'pt-2');
    divp.id="spinner_temporal";

    let div= document.createElement('div');
    div.classList.add('spinner-border', 'text-danger');
    div.setAttribute('role', 'status');

    let span = document.createElement('span');
    span.classList.add('s-only');

    div.append(span);
    divp.append(div);
    document.getElementById('spinner').append(divp);

    document.getElementById('spinner_temporal').remove();
    document.getElementById('datos').classList.remove('d-none')

})
*/
//al checkear el principal de la cabecera de la tabla subusuarios se checkean todos automaticamente
document.getElementById('checkbox_principal_subusuarios').addEventListener('click',(e)=>
{
    let size = Number($('#subusuarios').DataTable().data().length)

    if(e.target.checked)
    {
        for (let i = 0; i < size; i++)
        {
            $('#subusuarios').DataTable().cell(i,0).node().children[0].checked = true;
        }
        //luego de check en todos, se habilita el boton para asignar a grupos de subsuarios
        document.getElementById('boton_asignar_subusuarios_grupos').classList.remove('d-none');
    }
    else
    {
        for (let i = 0; i < size; i++)
        {
            $('#subusuarios').DataTable().cell(i,0).node().children[0].checked = false;
        }
        //luego de check en todos, se deshabilita el boton para asignar a grupos de subsuarios
        document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');
    }
})

//activar el boton de asignacion de grupos cuando se presione en un check
//en intervalo para esperar que se termine de cargar la tabla, sin eso no detecta
/* setTimeout(() => {

}, 3000); */

//fin activar el boton de asignacion de grupos cuando se presione en un check

//mostrar la lista de grupos de subusuarios
$('#lista_card').on('show.bs.collapse', function ()
{
    document.getElementById('spinner').classList.remove('col-sm-12','col-md-12','col-xl-12');
    document.getElementById('spinner').classList.add('col-sm-9','col-md-8');
    /* document.getElementById('spinner').classList.add('col-md-8'); */
    document.getElementById('spinner').classList.remove();
    /* document.getElementById('collapse').children[0].classList.remove('img-rotate'); */
    /* document.getElementById('collapse2').classList.add('d-none'); */
})

//mostrar la lista de grupos de subusuarios
$('#lista_card').on('hidden.bs.collapse', function ()
{
    document.getElementById('spinner').classList.remove('col-sm-9','col-md-8')
    document.getElementById('spinner').classList.add('col-sm-12','col-md-12', 'col-xl-12')
    /* document.getElementById('spinner').classList.remove('col-md-8'); */
    /* document.getElementById('spinner').classList.add('col-md-12'); */
    /* document.getElementById('collapse').children[0].classList.add('img-rotate'); */
    /* document.getElementById('collapse2').classList.remove('d-none'); */
})

//en el modal de crear subusuarios abrir la seccion de recursos
function abrir_ventana_recursos()
{
    //validar formulario
    if(validar_formulario_subusuarios()) return;

    document.getElementById('ventana_recursos_subusuario').classList.add('show', 'active');
    document.getElementById('ventana_detalles_subusuarios').classList.remove('show', 'active');
    document.getElementById('busqueda_agrega_recursos_subusuario').focus();
}

//en el modal de crear subusuarios abrir la seccion de detalle
function abrir_ventana_detalles()
{
    document.getElementById('ventana_recursos_subusuario').classList.remove('show', 'active');
    document.getElementById('ventana_detalles_subusuarios').classList.add('show', 'active');
    document.getElementById('nombrecompleto_subusuario').focus();
}

//CREAR NUEVO SUBUSUARIO
//al momento de presionar el boton para agregar nuevo subusuario y se abra el modal para ingresar los datos
document.getElementById('crear_nuevo_subusuario').addEventListener('click', async ()=>{

    //limpiar de errores antes de validarlos
    document.getElementById('nombrecompleto_subusuario').classList.remove('is-invalid');
    document.getElementById('nombre_subusuario').classList.remove('is-invalid');
    document.getElementById('clave_subusuario').classList.remove('is-invalid');
    document.getElementById('email_subusuario').classList.remove('is-invalid');
    //fin de limpiar errores

    //ventana abierta por defecto la de detalles subusuario
    document.getElementById('ventana_recursos_subusuario').classList.remove('show', 'active');
    document.getElementById('ventana_detalles_subusuarios').classList.add('show', 'active');
    //Fin ventana abierta por defecto la de detalles subusuario

    //por defecto caducidad en si
    document.getElementById('agregar_fecha_caducidad').value = "0";
    document.getElementById('validohasta_subusuario').classList.add('d-none');
    //fin por defecto caducidad en si

    //vaciar inputs
    document.getElementById('nombrecompleto_subusuario').value = "";
    document.getElementById('nombre_subusuario').value = "";
    document.getElementById('clave_subusuario').value = "";
    document.getElementById('validohasta_subusuario').value = "";
    document.getElementById('email_subusuario').value = "";
    document.getElementById('ver_seguimientos').checked = false;
    document.getElementById('adm_puntos_referencia').checked = false;
    document.getElementById('adm_configuracion').checked = false;
    document.getElementById('ver_alertas').checked = false;
    document.getElementById('adm_links_seguimiento').checked = false;
    document.getElementById('adm_geocercas').checked = false;
    document.getElementById('reasignar_despachos').checked = false;
    document.getElementById('ver_kilometrajes').checked = false;
    document.getElementById('administrar_alertas').checked = false;
    document.getElementById('editar_etiqueta').checked = false;
    document.getElementById('ver_recorridos').checked = false;
    document.getElementById('ver_dashboard').checked = false;
    document.getElementById('envio_comandos').checked = false;
    //fin vaciar inputs

    document.getElementById('arbol_principal_guardar_subusuario').setAttribute('open', '');

    document.getElementById('id_subusuario_editar').value = ""; //para saber que es un nuevo subusuario

    document.getElementById('validohasta_subusuario').value = new Date().toJSON().slice(0,10);

    //document.body.style.cursor = 'wait';
    //document.getElementById('crear_nuevo_subusuario').setAttribute('disabled', 'disabled');

    $('#modal-crear-subusuarios').modal({backdrop: 'static', keyboard: false});

    //bloquear boton siguiente mientras se precargan los otros recursos
    document.getElementById('boton_abrir_ventana_recursos_subusuario').classList.add('disabled');
    //fin bloquear boton siguiente mientras se precargan los otros recursos

    await fetch(`subusuarios/create?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`)
    .then(res => res.json())
    .then(res => {
        //console.log(res);
        //document.body.style.cursor = 'default';
        //document.getElementById('crear_nuevo_subusuario').removeAttribute('disabled');
        document.getElementById('busqueda_agrega_recursos_subusuario').value = "";

        //Quitar checks principales para marcar todos los recursos
        document.getElementById('checkbox_principal_asignar_vehiculo').checked = false;
        /* document.getElementById('checkbox_principal_asignar_punto').checked = false;
        document.getElementById('checkbox_principal_asignar_geocerca').checked = false; */
        document.getElementById('checkbox_principal_asignar_alerta').checked = false;
        document.getElementById('checkbox_principal_asignar_modulo').checked = false;
        document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = false;
        document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = false;
        //Fin quitar checks principales para marcar todos los recursos

        //Minimizar todos los recursos
        document.getElementById('lista_vehiculos_agregar').parentElement.removeAttribute('open');
        /* document.getElementById('lista_puntos_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_geocercas_agregar').parentElement.removeAttribute('open'); */
        document.getElementById('lista_alertas_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_modulos_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_grupo_puntos_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_grupo_geocercas_agregar').parentElement.removeAttribute('open');
        //Fin minimizar todos los recursos

        //eliminar las categorias que estaban antes
        //[...document.getElementById('categoria_subusuario').children].map( data => data.remove());

        [...document.getElementById('lista_vehiculos_agregar').children].map( data => data.remove());
        /* [...document.getElementById('lista_puntos_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_geocercas_agregar').children].map( data => data.remove()); */
        [...document.getElementById('lista_alertas_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_modulos_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_grupo_puntos_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_grupo_geocercas_agregar').children].map( data => data.remove());

        //agregar categorias al combo
        /* for(const categoria of res.categorias)
        {
            let option = document.createElement('option');
            option.textContent = categoria.Descripcion;
            option.value = categoria.IdCategoria;
            document.getElementById('categoria_subusuario').append(option);
        } */
        //fin agregar categorias al combo

        for(const vehiculo of res.vehiculos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idvehiculo', vehiculo.IdActivo);
            input.classList.add('checkbox_asignar_vehiculo', 'mr-2');
            input.type = 'checkbox';

            if(vehiculo.Alias.length>30)
            {
                label.textContent = vehiculo.Alias.substring(0,30) + '...';
                label.title = vehiculo.Alias;
            }
            else
            {
                label.textContent = vehiculo.Alias;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_vehiculos_agregar').append(li);
        }

        /* for(const geocerca of res.geocercas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idgeocerca', geocerca.IdGeocerca);
            input.classList.add('checkbox_asignar_geocerca','mr-2');
            input.type = 'checkbox';
            label.textContent = geocerca.Nombre;
            li.append(input);
            li.append(label);
            document.getElementById('lista_geocercas_agregar').append(li);
        } */

        /* for(const punto of res.puntos)
        {
            if(punto.Nombre == "") continue;
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idpunto', punto.IdPunto);
            input.classList.add('checkbox_asignar_punto','mr-2');
            input.type = 'checkbox';
            label.textContent = punto.Nombre;
            li.append(input);
            li.append(label);
            document.getElementById('lista_puntos_agregar').append(li);
        } */

        for(const alerta of res.alertas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idalerta', alerta.idAlerta);
            input.classList.add('checkbox_asignar_alerta','mr-2');
            input.type = 'checkbox';

            if(alerta.Evento.length>30)
            {
                label.textContent = alerta.Evento.substring(0,30) + '...';
                label.title = alerta.Evento;
            }
            else
            {
                label.textContent = alerta.Evento;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_alertas_agregar').append(li);
        }

        for(const modulo of res.modulos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idmodulo', modulo.idModulo);
            input.classList.add('checkbox_asignar_modulo','mr-2');
            input.type = 'checkbox';

            if(modulo.Nombre.length>30)
            {
                label.textContent = modulo.Nombre.substring(0,30);
                label.title = modulo.Nombre;
            }
            else
            {
                label.textContent = modulo.Nombre;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_modulos_agregar').append(li);
        }

        for(const grupo_punto of res.grupo_puntos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idgrupopunto', grupo_punto.IdGrupoPunto);
            input.classList.add('checkbox_asignar_grupo_punto', 'mr-2');
            input.type = 'checkbox';

            if(grupo_punto.Grupo.length>30)
            {
                label.textContent = grupo_punto.Grupo.substring(0,30);
                label.title = grupo_punto.Grupo;
            }
            else
            {
                label.textContent = grupo_punto.Grupo;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_grupo_puntos_agregar').append(li);
        }

        for(const grupo_geocerca of res.grupo_geocercas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idgrupogeocerca', grupo_geocerca.IdGrupoGeocerca);
            input.classList.add('checkbox_asignar_grupo_geocerca', 'mr-2');
            input.type = 'checkbox';

            if(grupo_geocerca.Nombre.length>30)
            {
                label.textContent = grupo_geocerca.Nombre.substring(0,30);
                label.title = grupo_geocerca.Nombre;
            }
            else
            {
                label.textContent = grupo_geocerca.Nombre;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_grupo_geocercas_agregar').append(li);
        }

    })
    .catch(error => {
        new Notification({
            text: 'Ocurrio un error al consultar el Subusuario',
            style: {
                background: '#DB0632',
                color: '#fff',
                width: '250px',
                height: '70px'
            },
            position: 'bottom-center',
            autoClose: 10000,
            canClose: false,
            showProgress: false,
            pauseOnHover: false,
            pauseOnFocusLoss: false,
            pauseOnHover: false,
            pauseOnFocusLoss: false
        });
    });

    document.getElementById('boton_abrir_ventana_recursos_subusuario').classList.remove('disabled');
    /* setTimeout(() => {

    }, 500); */

})
//FIN CREAR NUEVO SUBUSUARIO

//EDITAR SUBUSUARIO
async function editar_subusuario(id)
{
    document.getElementById('ventana_recursos_subusuario').classList.remove('show', 'active');
    document.getElementById('ventana_detalles_subusuarios').classList.add('show', 'active');

    //limpiar de errores antes de validarlos
    document.getElementById('nombrecompleto_subusuario').classList.remove('is-invalid');
    document.getElementById('nombre_subusuario').classList.remove('is-invalid');
    document.getElementById('clave_subusuario').classList.remove('is-invalid');
    document.getElementById('email_subusuario').classList.remove('is-invalid');
    //fin de limpiar errores

    document.body.style.cursor = 'wait';

    //variables para ver si el check de todos los seleccionados se marca o se queda desmarcado
    let bandera_check_todos_seleccionados_vehiculos = 1;
    let bandera_check_todos_seleccionados_modulos = 1;
    let bandera_check_todos_seleccionados_grupopuntos = 1;
    let bandera_check_todos_seleccionados_grupogeocercas = 1;
    let bandera_check_todos_seleccionados_alertas = 1;

    await fetch(`subusuarios/${id}/editar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`)
    .then(res => res.json())
    .then(res => {
        //console.log(res);
        document.body.style.cursor = 'default';

        //Quitar checks principales para marcar todos los recursos
        document.getElementById('checkbox_principal_asignar_vehiculo').checked = false;
        /* document.getElementById('checkbox_principal_asignar_punto').checked = false;
        document.getElementById('checkbox_principal_asignar_geocerca').checked = false; */
        document.getElementById('checkbox_principal_asignar_alerta').checked = false;
        document.getElementById('checkbox_principal_asignar_modulo').checked = false;
        document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = false;
        document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = false;
        //Fin quitar checks principales para marcar todos los recursos

        document.getElementById('busqueda_agrega_recursos_subusuario').value = "";

        document.getElementById('arbol_principal_guardar_subusuario').setAttribute('open', '');

        //Minimizar todos los recursos
        document.getElementById('lista_vehiculos_agregar').parentElement.removeAttribute('open');
        /* document.getElementById('lista_puntos_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_geocercas_agregar').parentElement.removeAttribute('open'); */
        document.getElementById('lista_alertas_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_modulos_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_grupo_puntos_agregar').parentElement.removeAttribute('open');
        document.getElementById('lista_grupo_geocercas_agregar').parentElement.removeAttribute('open');
        //Fin minimizar todos los recursos


        //[...document.getElementById('categoria_subusuario').children].map( data => data.remove());
        [...document.getElementById('lista_vehiculos_agregar').children].map( data => data.remove());
        /* [...document.getElementById('lista_puntos_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_geocercas_agregar').children].map( data => data.remove()); */
        [...document.getElementById('lista_alertas_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_modulos_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_grupo_puntos_agregar').children].map( data => data.remove());
        [...document.getElementById('lista_grupo_geocercas_agregar').children].map( data => data.remove());

        document.getElementById('id_subusuario_editar').value = res.sms[0].IdSubUsuario;

        //agregar categorias al combo
        /* for(const categoria of res.categorias)
        {
            let option = document.createElement('option');
            option.textContent = categoria.Descripcion;
            option.value = categoria.IdCategoria;
            document.getElementById('categoria_subusuario').append(option);
        } */
        //fin agregar categorias al combo

        //si la fecha la trae en nulo entonces ocultar fecha
        if( res.sms[0].FechaCaducidad != null )
        {
            document.getElementById('agregar_fecha_caducidad').value = "1";
            document.getElementById('validohasta_subusuario').classList.remove('d-none');
        }
        else
        {
            document.getElementById('agregar_fecha_caducidad').value = "0";
            document.getElementById('validohasta_subusuario').classList.add('d-none');
            document.getElementById('validohasta_subusuario').value = new Date().toJSON().slice(0,10);
        }
        //fin si la fecha la trae en nulo entonces ocultar fecha

        document.getElementById('nombrecompleto_subusuario').value = res.sms[0].NombreCompleto;
        document.getElementById('nombre_subusuario').value = res.sms[0].SubUsuario;
        document.getElementById('clave_subusuario').value = res.sms[0].Clave;
        document.getElementById('validohasta_subusuario').value = (res.sms[0].FechaCaducidad != null) ? res.sms[0].FechaCaducidad.slice(0,10) : '';
        document.getElementById('categoria_subusuario').value = res.sms[0].idCategoria;
        document.getElementById('email_subusuario').value = res.sms[0].Email;
        document.getElementById('ver_seguimientos').checked = (res.sms[0].VerSeguimiento == "1") ? true : false;
        document.getElementById('adm_puntos_referencia').checked = (res.sms[0].AdmPuntosReferencia == "1") ? true : false;
        document.getElementById('adm_configuracion').checked = (res.sms[0].AdmConfiguracion == "1") ? true : false;
        document.getElementById('ver_alertas').checked = (res.sms[0].VerAlertas == "1") ? true : false;
        document.getElementById('adm_links_seguimiento').checked = (res.sms[0].CrearLinksSeguimiento == "1") ? true : false;
        document.getElementById('adm_geocercas').checked = (res.sms[0].AdmGeocercas == "1") ? true : false;
        document.getElementById('reasignar_despachos').checked = (res.sms[0].ReasignarDespachos == "1") ? true : false;
        document.getElementById('ver_kilometrajes').checked = (res.sms[0].VerKilometraje == "1") ? true : false;
        document.getElementById('administrar_alertas').checked = (res.sms[0].AdmAlertas == "1") ? true : false;
        document.getElementById('editar_etiqueta').checked = (res.sms[0].EditarEtiqueta == "1") ? true : false;
        document.getElementById('ver_recorridos').checked = (res.sms[0].VerRecorridos == "1") ? true : false;
        document.getElementById('ver_dashboard').checked = (res.sms[0].verDashBoard == "1") ? true : false;
        document.getElementById('envio_comandos').checked = (res.sms[0].EnvioComandos == "1") ? true : false;

        //vaciar las variables globales de asignaciones
        vehiculos_asignados_usuario = [];
        /* puntos_asignados_usuario = [];
        geocercas_asignadas_usuario = []; */
        alertas_asignadas_usuario = [];
        modulos_asignados_usuario = [];
        grupopuntos_asignados_usuario = [];
        grupogeocercas_asignados_usuario = [];
        //fin vaciar las variables globales de asignaciones

        //agregar asignaciones a las variables globales
        vehiculos_asignados_usuario = res.vehiculos_pertenecen;
        /* puntos_asignados_usuario = res.puntos_pertenecen;
        geocercas_asignadas_usuario = res.geocercas_pertenecen; */
        alertas_asignadas_usuario = res.alertas_pertenecen;
        modulos_asignados_usuario = res.modulos_pertenecen;
        grupopuntos_asignados_usuario = res.grupo_puntos_pertenecen;
        grupogeocercas_asignados_usuario = res.grupo_geocercas_pertenecen;
        //fin agregar asignaciones a las variables globales

        //console.log('hola: ', vehiculos_asignados_usuario, puntos_asignados_usuario, geocercas_asignadas_usuario, alertas_asignadas_usuario, modulos_asignados_usuario, grupopuntos_asignados_usuario, grupogeocercas_asignados_usuario);

        for(const vehiculo of res.vehiculos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idvehiculo', vehiculo.IdActivo);
            input.classList.add('checkbox_asignar_vehiculo', 'mr-2');
            input.type = 'checkbox';
            if(res.vehiculos_pertenecen.includes(vehiculo.IdActivo)) input.checked = true;
            else bandera_check_todos_seleccionados_vehiculos = 0; //si algun check esta desmarcado entonces no se marca el check de todos los vehiculos

            if(vehiculo.Alias.length>30)
            {
                label.textContent = vehiculo.Alias.substring(0,30) + '...';
                label.title = vehiculo.Alias;
            }
            else
            {
                label.textContent = vehiculo.Alias;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_vehiculos_agregar').append(li);
        }

        /* for(const geocerca of res.geocercas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idgeocerca', geocerca.IdGeocerca);
            if(res.geocercas_pertenecen.includes(geocerca.IdGeocerca)) input.checked = true;
            input.classList.add('checkbox_asignar_geocerca','mr-2');
            input.type = 'checkbox';
            label.textContent = geocerca.Nombre;
            li.append(input);
            li.append(label);
            document.getElementById('lista_geocercas_agregar').append(li);
        } */

        /* for(const punto of res.puntos)
        {
            if(punto.Nombre == "") continue;
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idpunto', punto.IdPunto);
            if(res.puntos_pertenecen.includes(punto.IdPunto)) input.checked = true;
            input.classList.add('checkbox_asignar_punto','mr-2');
            input.type = 'checkbox';
            label.textContent = punto.Nombre;
            li.append(input);
            li.append(label);
            document.getElementById('lista_puntos_agregar').append(li);
        } */

        for(const alerta of res.alertas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idalerta', alerta.idAlerta);
            if(res.alertas_pertenecen.includes(alerta.idAlerta)) input.checked = true;
            else bandera_check_todos_seleccionados_alertas = 0;
            input.classList.add('checkbox_asignar_alerta','mr-2');
            input.type = 'checkbox';

            if(alerta.Evento.length>30)
            {
                label.textContent = alerta.Evento.substring(0,30) + '...';
                label.title = alerta.Evento;
            }
            else
            {
                label.textContent = alerta.Evento;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_alertas_agregar').append(li);
        }

        for(const modulo of res.modulos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            input.setAttribute('idmodulo', modulo.idModulo);
            if(res.modulos_pertenecen.includes(modulo.idModulo)) input.checked = true;
            else bandera_check_todos_seleccionados_modulos = 0;
            input.classList.add('checkbox_asignar_modulo','mr-2');
            input.type = 'checkbox';

            if(modulo.Nombre.length>30)
            {
                label.textContent = modulo.Nombre.substring(0,30) + '...';
                label.title = modulo.Nombre;
            }
            else
            {
                label.textContent = modulo.Nombre;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_modulos_agregar').append(li);
        }

        for(const grupo_punto of res.grupo_puntos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            if(res.grupo_puntos_pertenecen.includes(grupo_punto.IdGrupoPunto)) input.checked = true;
            else bandera_check_todos_seleccionados_grupopuntos = 0;
            input.setAttribute('idgrupopunto', grupo_punto.IdGrupoPunto);
            input.classList.add('checkbox_asignar_grupo_punto', 'mr-2');
            input.type = 'checkbox';

            if(grupo_punto.Grupo.length>30)
            {
                label.textContent = grupo_punto.Grupo.substring(0,30) + '...';
                label.title = grupo_punto.Grupo;
            }
            else
            {
                label.textContent = grupo_punto.Grupo;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_grupo_puntos_agregar').append(li);
        }

        for(const grupo_geocerca of res.grupo_geocercas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            if(res.grupo_geocercas_pertenecen.includes(grupo_geocerca.IdGrupoGeocerca)) input.checked = true;
            else bandera_check_todos_seleccionados_grupogeocercas = 0;
            input.setAttribute('idgrupogeocerca', grupo_geocerca.IdGrupoGeocerca);
            input.classList.add('checkbox_asignar_grupo_geocerca', 'mr-2');
            input.type = 'checkbox';

            if(grupo_geocerca.Nombre.length>30)
            {
                label.textContent = grupo_geocerca.Nombre.substring(0,30) + '...';
                label.title = grupo_geocerca.Nombre;
            }
            else
            {
                label.textContent = grupo_geocerca.Nombre;
            }

            label.style.fontSize = '12px';
            li.append(input);
            li.append(label);
            document.getElementById('lista_grupo_geocercas_agregar').append(li);
        }

        if( bandera_check_todos_seleccionados_vehiculos &&  res.vehiculos.length>0) document.getElementById('checkbox_principal_asignar_vehiculo').checked = true;
        if( bandera_check_todos_seleccionados_alertas &&  res.alertas.length>0) document.getElementById('checkbox_principal_asignar_alerta').checked = true;
        if( bandera_check_todos_seleccionados_modulos &&  res.modulos.length>0) document.getElementById('checkbox_principal_asignar_modulo').checked = true;
        if( bandera_check_todos_seleccionados_grupopuntos &&  res.grupo_puntos.length>0) document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = true;
        if( bandera_check_todos_seleccionados_grupogeocercas &&  res.grupo_geocercas.length>0) document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = true;

    });

    $('#modal-crear-subusuarios').modal();

    /* setTimeout(() => {
        document.getElementById('nombrecompleto_subusuario').focus();
        document.getElementById('ver_password').nextElementSibling.children[0].children[0].classList.remove('fa-eye-slash');
        document.getElementById('ver_password').setAttribute('type', 'password');
        document.getElementById('ver_password').nextElementSibling.children[0].children[0].classList.add('fa-eye');

    }, 500); */
}
//FIN EDITAR SUBUSUARIO

//ELIMINAR SUBUSUARIO abrir modal
function eliminar_subusuario(id, subusuario)
{
    document.getElementById('id_subusuario_eliminar').value = id;
    document.getElementById('titulo_modal_eliminar_subusuario').textContent = `¿Quieres eliminar a ${subusuario}?`;
    $('#modal_eliminar_subusuario').modal();

}
//FIN ELIMINAR SUBUSUARIO

//VER DETALLES SUBUSUARIO
async function ver_detalle_subusuario(id)
{
    document.body.style.cursor = 'wait';

    await fetch(`subusuarios/${id}/mostrar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`)
    .then(res => res.json())
    .then(res => {
        //console.log(res);

        document.body.style.cursor = 'default';

        [...document.getElementById('lista_vehiculos_mostrar').children].map( data => data.remove());
        /* [...document.getElementById('lista_puntos_mostrar').children].map( data => data.remove()); */
        /* [...document.getElementById('lista_geocercas_mostrar').children].map( data => data.remove()); */
        [...document.getElementById('lista_alertas_mostrar').children].map( data => data.remove());
        [...document.getElementById('lista_modulos_mostrar').children].map( data => data.remove());
        [...document.getElementById('lista_grupo_puntos_mostrar').children].map( data => data.remove());
        [...document.getElementById('lista_grupo_geocercas_mostrar').children].map( data => data.remove());

        document.getElementById('arbol_principal_mostrar_subusuario').setAttribute('open', '');

        //Minimizar todos los recursos
        document.getElementById('lista_vehiculos_mostrar').parentElement.removeAttribute('open');
        /* document.getElementById('lista_puntos_mostrar').parentElement.removeAttribute('open'); */
        /* document.getElementById('lista_geocercas_mostrar').parentElement.removeAttribute('open'); */
        document.getElementById('lista_alertas_mostrar').parentElement.removeAttribute('open');
        document.getElementById('lista_modulos_mostrar').parentElement.removeAttribute('open');
        document.getElementById('lista_grupo_puntos_mostrar').parentElement.removeAttribute('open');
        document.getElementById('lista_grupo_geocercas_mostrar').parentElement.removeAttribute('open');
        //Fin minimizar todos los recursos

        for(const vehiculo of res.vehiculos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');

            if(vehiculo.Alias.length>30)
            {
                label.textContent = vehiculo.Alias.substring(0,30) + '...';
                label.title = vehiculo.Alias;
            }
            else
            {
                label.textContent = vehiculo.Alias;
            }

            label.style.fontSize = '12px';
            li.append(label);
            document.getElementById('lista_vehiculos_mostrar').append(li);
        }

        /* for(const geocerca of res.geocercas)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            label.textContent = geocerca.Nombre;
            li.append(label);
            document.getElementById('lista_geocercas_mostrar').append(li);
        } */

        /* for(const punto of res.puntos)
        {
            let li = document.createElement('li');
            let input = document.createElement('input');
            let label = document.createElement('label');
            label.textContent = punto.Nombre;
            li.append(label);
            document.getElementById('lista_puntos_mostrar').append(li);
        } */

        for(const alerta of res.alertas)
        {
            let li = document.createElement('li');
            let label = document.createElement('label');

            if(alerta.Alerta.length>30)
            {
                label.textContent = alerta.Alerta.substring(0,30) + '...';
                label.title = alerta.Alerta;
            }
            else
            {
                label.textContent = alerta.Alerta;
            }

            label.style.fontSize = '12px';
            li.append(label);
            document.getElementById('lista_alertas_mostrar').append(li);
        }

        for(const modulo of res.modulos)
        {
            let li = document.createElement('li');
            let label = document.createElement('label');

            if(modulo.Nombre.length>30)
            {
                label.textContent = modulo.Nombre.substring(0,30) + '...';
                label.title = modulo.Nombre;
            }
            else
            {
                label.textContent = modulo.Nombre;
            }

            label.style.fontSize = '12px';
            li.append(label);
            document.getElementById('lista_modulos_mostrar').append(li);
        }

        for(const grupo_punto of res.grupo_puntos)
        {
            let li = document.createElement('li');
            let label = document.createElement('label');

            if(grupo_punto.Grupo.length>30)
            {
                label.textContent = grupo_punto.Grupo.substring(0,30) + '...';
                label.title = grupo_punto.Grupo;
            }
            else
            {
                label.textContent = grupo_punto.Grupo;
            }

            label.style.fontSize = '12px';
            li.append(label);
            document.getElementById('lista_grupo_puntos_mostrar').append(li);
        }

        for(const grupo_geocerca of res.grupo_geocercas)
        {
            let li = document.createElement('li');
            let label = document.createElement('label');

            if(grupo_geocerca.Nombre.length>30)
            {
                label.textContent = grupo_geocerca.Nombre.substring(0,30) + '...';
                label.title = grupo_geocerca.Nombre;
            }
            else
            {
                label.textContent = grupo_geocerca.Nombre;
            }

            label.style.fontSize = '12px';
            li.append(label);
            document.getElementById('lista_grupo_geocercas_mostrar').append(li);
        }

    })

    $('#modal_mostrar_detalles_subusuario').modal();
}
//FIN VER DETALLES SUBUSUARIO

//CONFIRMACION ELIMINACION SUBUSUARIO
document.getElementById('confirmar_eliminacion_subusuario').addEventListener('click', async ()=>{

    document.body.style.cursor = 'wait';

    await fetch(`subusuarios/${document.getElementById('id_subusuario_eliminar').value} ?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`,{
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')},
        method: 'delete'
    })
    .then(res => res.json())
    .then(res => {
        document.body.style.cursor = 'default';

        if(res.sms == 'ok')
        {
            /* $.toast({
                heading: 'Eliminado',
                text: 'Subusuario eliminado con exito',
                position: 'bottom-center',
                icon: 'success',
                hideAfter: 1000,
                loader: false
            }); */

            new Notification({
                text: 'Subusuario eliminado con exito',
                style: {
                    background: '#36bb34',
                    color: '#fff',
                    width: '230px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 3000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

            $('#subusuarios').DataTable().ajax.reload();
            $('#modal_eliminar_subusuario').modal('hide');
        }

    })
    .catch(error => {
        /* $.toast({
            heading: 'Error',
            text: 'Ocurrió un error al eliminar el Subusuario',
            position: 'bottom-center',
            icon: 'error',
            hideAfter: 1000,
            loader: false
        }); */

        new Notification({
            text: 'Ocurrio un error al eliminar el Subusuario',
            style: {
                background: '#DB0632',
                color: '#fff',
                width: '250px',
                height: '60px'
            },
            position: 'bottom-center',
            autoClose: 10000,
            canClose: false,
            showProgress: false,
            pauseOnHover: false,
            pauseOnFocusLoss: false,
            pauseOnHover: false,
            pauseOnFocusLoss: false
        });

    });

})
//FIN CONFIRMACION ELIMINACION SUBUSUARIO

//GUARDAR SUBUSUARIO
document.getElementById('boton_guardar_subusuario').addEventListener('click', ()=>{

    let id_subusuario = document.getElementById('id_subusuario_editar').value;
    let data = new FormData();
    /* let puntos_referencia_asignados = []; */
    /* let geocercas_asignadas = []; */
    let vehiculos_asignados = [];
    let alertas_asignadas = [];
    let modulos_asignados = [];
    let grupopuntos_asignados = [];
    let grupogeocercas_asignadas = [];

    document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');

    [...document.getElementById('lista_vehiculos_agregar').children].map(data => (data.children[0].checked) ? vehiculos_asignados.push(data.children[0].getAttribute('idvehiculo')) : '');
    //[...document.getElementById('lista_puntos_agregar').children].map(data => (data.children[0].checked) ? puntos_referencia_asignados.push(data.children[0].getAttribute('idpunto')) : '');
    //[...document.getElementById('lista_geocercas_agregar').children].map(data => (data.children[0].checked) ? geocercas_asignadas.push(data.children[0].getAttribute('idgeocerca')) : '');
    [...document.getElementById('lista_alertas_agregar').children].map(data => (data.children[0].checked) ? alertas_asignadas.push(data.children[0].getAttribute('idalerta')) : '');
    [...document.getElementById('lista_modulos_agregar').children].map(data => (data.children[0].checked) ? modulos_asignados.push(data.children[0].getAttribute('idmodulo')) : '');
    [...document.getElementById('lista_grupo_puntos_agregar').children].map(data => (data.children[0].checked) ? grupopuntos_asignados.push(data.children[0].getAttribute('idgrupopunto')) : '');
    [...document.getElementById('lista_grupo_geocercas_agregar').children].map(data => (data.children[0].checked) ? grupogeocercas_asignadas.push(data.children[0].getAttribute('idgrupogeocerca')) : '');

    //recursos para asignar y guardar subusuario
    data.append('vehiculos_asignados', JSON.stringify(vehiculos_asignados));
    /* data.append('puntos_referencia_asignados', JSON.stringify(puntos_referencia_asignados));
    data.append('geocercas_asignadas', JSON.stringify(geocercas_asignadas)); */
    data.append('alertas_asignadas', JSON.stringify(alertas_asignadas));
    data.append('modulos_asignados', JSON.stringify(modulos_asignados));
    data.append('grupopuntos_asignados', JSON.stringify(grupopuntos_asignados));
    data.append('grupogeocercas_asignadas', JSON.stringify(grupogeocercas_asignadas));
    //fin recursos para asignar y guardar subusuario

    //recursos anteriormente asignados
    data.append('vehiculos_anteriores', JSON.stringify(vehiculos_asignados_usuario));
    /* data.append('puntos_referencia_anteriores', JSON.stringify(puntos_asignados_usuario)); */
    /* data.append('geocercas_anteriores', JSON.stringify(geocercas_asignadas_usuario)); */
    data.append('alertas_anteriores', JSON.stringify(alertas_asignadas_usuario));
    data.append('modulos_anteriores', JSON.stringify(modulos_asignados_usuario));
    data.append('grupopuntos_anteriores', JSON.stringify(grupopuntos_asignados_usuario));
    data.append('grupogeocercas_anteriores', JSON.stringify(grupogeocercas_asignados_usuario));
    //fin recursos anteriormente asignados

    data.append('nombrecompleto_subusuario', document.getElementById('nombrecompleto_subusuario').value);
    data.append('nombre_subusuario', document.getElementById('nombre_subusuario').value);
    data.append('clave_subusuario', document.getElementById('clave_subusuario').value);
    if(document.getElementById('agregar_fecha_caducidad').value == "1") data.append('validohasta_subusuario', document.getElementById('validohasta_subusuario').value); else data.append('validohasta_subusuario', '');
    data.append('categoria_subusuario', document.getElementById('categoria_subusuario').value);
    data.append('email_subusuario', document.getElementById('email_subusuario').value);
    /* data.append('sin_caducidad', document.getElementById('sin_caducidad').checked); */
    data.append('ver_kilometrajes', document.getElementById('ver_kilometrajes').checked);
    data.append('ver_seguimientos', document.getElementById('ver_seguimientos').checked);
    data.append('administrar_alertas', document.getElementById('administrar_alertas').checked);
    data.append('adm_puntos_referencia', document.getElementById('adm_puntos_referencia').checked);
    data.append('editar_etiqueta', (document.getElementById('editar_etiqueta').checked) ? 1 : 0);
    data.append('adm_configuracion', (document.getElementById('adm_configuracion').checked) ? 1 : 0 );
    data.append('ver_recorridos', document.getElementById('ver_recorridos').checked);
    data.append('ver_alertas', document.getElementById('ver_alertas').checked);
    data.append('ver_dashboard', document.getElementById('ver_dashboard').checked);
    data.append('adm_links_seguimiento', document.getElementById('adm_links_seguimiento').checked);
    data.append('adm_geocercas', document.getElementById('adm_geocercas').checked);
    data.append('envio_comandos', document.getElementById('envio_comandos').checked);
    data.append('reasignar_despachos', (document.getElementById('reasignar_despachos').checked) ? 1 : 0 );

    if( document.getElementById('id_subusuario_editar').value == "" ) //guardar
    {
        fetch(`subusuarios?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`, {
        method: 'post',
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')},
        body: data
        })
        .then(res => res.json())
        .then(res => {
            if(res.sms == 'ok')
            {
                /* $.toast({
                    heading: 'Guardado',
                    text: 'Subusuario guardado con exito',
                    position: 'bottom-center',
                    icon: 'success',
                    hideAfter: 1000,
                    loader: false
                }); */

                new Notification({
                    text: 'Subusuario guardado con exito',
                    style: {
                        background: '#36BB34',
                        color: '#fff',
                        width: '230px',
                        height: '60px'
                    },
                    position: 'bottom-center',
                    autoClose: 3000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });

                $('#modal-crear-subusuarios').modal('hide');
                $('#subusuarios').DataTable().ajax.reload();
            }
            else
            {
                /* $.toast({
                    heading: 'Error',
                    text: res.sms,
                    position: 'bottom-center',
                    icon: 'error',
                    hideAfter: 2000,
                    loader: false
                }); */

                new Notification({
                    text: res.sms,
                    style: {
                        background: '#DB0632',
                        color: '#fff',
                        width: '300px',
                        height: ajustar_altura(res.sms.length),
                    },
                    position: 'bottom-center',
                    autoClose: 10000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            }
        })
        .catch(error => {
            /* $.toast({
                heading: 'Error',
                text: 'Ocurrió un error al guardar el subusuario',
                position: 'bottom-center',
                icon: 'error',
                hideAfter: 1000,
                loader: false
            }); */

            new Notification({
                text: 'Ocurrió un error al guardar el subusuario',
                style: {
                    background: '#DB0632',
                    color: '#fff',
                    width: '300px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 10000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

        })
    }
    else //editar
    {
        fetch(`subusuarios/${id_subusuario}/actualizar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`, {
        method: 'post',
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')},
        body: data
        })
        .then(res => res.json())
        .then(res => {
            //console.log(res);
            if(res.sms == 'ok')
            {
                /* $.toast({
                    heading: 'Actualizado',
                    text: 'Subusuario actualizado con exito',
                    position: 'bottom-center',
                    icon: 'success',
                    hideAfter: 1000,
                    loader: false
                }); */

                new Notification({
                    text: 'Subusuario actualizado con exito',
                    style: {
                        background: '#36BB34',
                        color: '#fff',
                        width: '250px',
                        height: '60px'
                    },
                    position: 'bottom-center',
                    autoClose: 3000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });

                $('#modal-crear-subusuarios').modal('hide');
                $('#subusuarios').DataTable().ajax.reload();
            }
            else
            {
                /* $.toast({
                    heading: 'Error',
                    text: res.sms,
                    position: 'bottom-center',
                    icon: 'error',
                    hideAfter: 2000,
                    loader: false
                }); */

                new Notification({
                    text: res.sms,
                    style: {
                        background: '#DB0632',
                        color: '#fff',
                        width: '300px',
                        height: ajustar_altura(res.sms.length),
                    },
                    position: 'bottom-center',
                    autoClose: 10000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            }
        })
        .catch(error => {
            /* $.toast({
                heading: 'Error',
                text: 'Ocurrió un error al actualizar el subusuario',
                position: 'bottom-center',
                icon: 'error',
                hideAfter: 1000,
                loader: false
            }) */

            new Notification({
                text: 'Ocurrió un error al actualizar el subusuario',
                style: {
                    background: '#DB0632',
                    color: '#fff',
                    width: '300px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 10000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });
        })
    }

    //$('#modal-crear-subusuarios').modal('hide');
})
//fin GUARDAR SUBUSUARIO

//boton que muestra y oculta la lista de grupos de subusuarios
document.getElementById('colapsar_grupos').addEventListener('click', ()=>{

    if( !document.getElementById('grupos').children[1].classList.contains('d-none') )
    {
        document.getElementById('grupos').children[1].classList.add('d-none');
        document.getElementById('colapsar_grupos').classList.add('img-rotate');
    }
    else
    {
        document.getElementById('grupos').children[1].classList.remove('d-none');
        document.getElementById('colapsar_grupos').classList.remove('img-rotate');
    }

})


document.getElementById('input_buscar_categoria').addEventListener('input', (e)=>{

    [...document.getElementById('categorias').children].map(data => {
        if(data.getAttribute('categoria').toLowerCase().includes(e.target.value.toLowerCase()))
        {
            data.classList.remove('d-none');
        }
        else
        {
            data.classList.add('d-none');
        }
    });

    /* if( document.getElementById('seleccionar_todos_filtros_categoria').checked )
    {
        [...document.getElementById('categorias').children].map(data => {
            if(!data.children[0].children[0].checked && !data.classList.contains('d-none')) document.getElementById('seleccionar_todos_filtros_categoria').checked = false;
        })
    } */
    let total_checks = 0;
    let checkeados = 0;
    [...document.getElementById('categorias').children].map(data => {
        if(!data.children[0].children[0].checked && !data.classList.contains('d-none'))
        {
            document.getElementById('seleccionar_todos_filtros_categoria').checked = false;
        }

        if(!data.classList.contains('d-none')) total_checks++;

        if(data.children[0].children[0].checked && !data.classList.contains('d-none')) checkeados++;
    });

    if(total_checks == checkeados && checkeados>0) document.getElementById('seleccionar_todos_filtros_categoria').checked = true;

})


/* GUARDADOS */

//editar y guardar, si es a guardar uno nuevo entonces el id_grupo debe estar vacio, caso contrario se edita
document.getElementById('guardar_grupo').addEventListener('click', async ()=>{

    let id_grupo = document.getElementById('id_grupo').value;
    let data = new FormData();
    let nombre_grupo = document.getElementById('nombre_grupo').value;
    let descripcion_grupo = document.getElementById('descripcion_grupo').value;

    data.append('nombre', nombre_grupo);
    data.append('descripcion', descripcion_grupo);

    if(validar_formulario_grupo()) return;

    //guardar grupo
    if( id_grupo == '')
    {
        await fetch(`grupo?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`, {
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')},
        method: 'post',
        body: data,
        })
        .then(res => res.json())
        .then(res => {
            if(res.sms == 'ok')
            {
                $('#grupos').DataTable().ajax.reload();

                /* $.toast({
                    heading: 'Guardado',
                    text: 'Grupo guardado con exito',
                    position: 'bottom-center',
                    icon: 'success',
                    hideAfter: 1000,
                    loader: false
                }); */

                new Notification({
                    text: 'Grupo guardado con exito',
                    style: {
                        background: '#36BB34',
                        color: '#fff',
                        width: '200px',
                        height: '60px'
                    },
                    position: 'bottom-center',
                    autoClose: 3000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });

                $('#grupoSubusuarioModal').modal('hide');

            }
            else
            {
                /* $.toast({
                    heading: 'Error',
                    text: 'Ocurrió un error al guardar el grupo',
                    position: 'bottom-center',
                    icon: 'error',
                    hideAfter: 1000,
                    loader: false
                }) */

                new Notification({
                    text: res.sms,
                    style: {
                        background: '#DB0632',
                        color: '#fff',
                        width: '180px',
                        height: ajustar_altura(res.sms.length),
                    },
                    position: 'bottom-center',
                    autoClose: 10000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            }
        })
        .catch(error => {
            /* $.toast({
                heading: 'Error',
                text: 'Ocurrió un error al guardar el grupo',
                position: 'bottom-center',
                icon: 'error',
                hideAfter: 1000,
                loader: false
            }) */

            new Notification({
                text: 'Ocurrió un error al guardar el grupo',
                style: {
                    background: '#DB0632',
                    color: '#fff',
                    width: '300px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 10000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

        })
    }
    else //editar grupo
    {
        await fetch(`grupos/${id_grupo}?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`, {
        headers:
        {
            'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content'),
        },
        method: 'POST',
        body: data,
        })
        .then(res => res.json())
        .then(res => {
            if(res.sms == 'ok')
            {
                $('#grupos').DataTable().ajax.reload()

                /* $.toast({
                    heading: 'Guardado',
                    text: 'Grupo editado con exito',
                    position: 'bottom-center',
                    icon: 'success',
                    hideAfter: 1000,
                    loader: false
                }); */

                new Notification({
                    text: 'Grupo editado con exito',
                    style: {
                        background: '#36BB34',
                        color: '#fff',
                        width: '200px',
                        height: '60px'
                    },
                    position: 'bottom-center',
                    autoClose: 3000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });

                $('#grupoSubusuarioModal').modal('hide');

            }
            else
            {
                /* $.toast({
                    heading: 'Error',
                    text: 'Ocurrió un error al editar el grupo',
                    position: 'bottom-center',
                    icon: 'error',
                    hideAfter: 1000,
                    loader: false
                }); */

                new Notification({
                    text: res.sms,
                    style: {
                        background: '#DB0632',
                        color: '#fff',
                        width: '300px',
                        height: ajustar_altura(res.sms.length),
                    },
                    position: 'bottom-center',
                    autoClose: 10000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            }
        })
        .catch(error => {
            /* $.toast({
                heading: 'Error',
                text: 'Ocurrió un error al editar el grupo',
                position: 'bottom-center',
                icon: 'error',
                hideAfter: 1000,
                loader: false
            }); */

            new Notification({
                text: 'Ocurrió un error al editar el grupo',
                style: {
                    background: '#DB0632',
                    color: '#fff',
                    width: '300px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 10000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

        })
    }

    //limpiar campos
    document.getElementById('nombre_grupo').value = '';
    document.getElementById('descripcion_grupo').value = '';

})

//cuando se quiera agregar un nuevo grupo, se elimina el valor del campo oculto id_grupo para saber que es un registro nuevo
document.getElementById('agregar_grupos').addEventListener('click', ()=>{
    document.getElementById('id_grupo').value = '';
    document.getElementById('titulo_modal_grupos').textContent = 'NUEVO GRUPO';
    document.getElementById('nombre_grupo').value = '';
    document.getElementById('descripcion_grupo').value = '';

    setTimeout(() => {
        document.getElementById('nombre_grupo').focus();
    }, 700);

})

//levantar modal con los datos del grupo para su edicion
async function editar_grupo(id)
{
    document.getElementById('titulo_modal_grupos').textContent = 'EDITAR GRUPO';

    document.body.style.cursor = 'wait';

    await fetch(`grupos/${id}/editar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`)
    .then(res => res.json())
    .then(res =>{
        //console.log(res);
        document.body.style.cursor = 'default';

        document.getElementById('id_grupo').value = res.sms[0].IdGrupoSubUsuario;
        document.getElementById('nombre_grupo').value = res.sms[0].Grupo;
        document.getElementById('descripcion_grupo').value = res.sms[0].Descripcion;
        //console.log(res.sms[0].IdCategoria)
    });

    $('#grupoSubusuarioModal').modal();

    setTimeout(() => {
        document.getElementById('nombre_grupo').focus();
    }, 700);
}

//levantar modal para confirmar eliminacion del grupo
function eliminar_grupo(id, grupo)
{
    document.getElementById('id_grupo_eliminar').value = id;
    document.getElementById('titulo_modal_eliminar_grupo').textContent = `¿Quieres eliminar a ${grupo}?`;
    $('#modal_eliminar_grupo').modal();
}

document.getElementById('confirmar_eliminacion_grupo').addEventListener('click', async ()=>{

    document.body.style.cursor = 'wait';

    await fetch(`grupos/${document.getElementById('id_grupo_eliminar').value}`,{
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')},
        method: 'delete'
    })
    .then(res => res.json())
    .then(res => {

        document.body.style.cursor = 'default';

        if(res.sms == 'ok')
        {

            /* $.toast({
                heading: 'Eliminado',
                text: 'Grupo eliminado con exito',
                position: 'bottom-center',
                icon: 'success',
                hideAfter: 1000,
                loader: false
            }); */

            new Notification({
                text: 'Grupo eliminado con exito',
                style: {
                    background: '#36BB34',
                    color: '#fff',
                    width: '200px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 3000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

            busqueda.grupo = 0;

            $('#grupos').DataTable().ajax.reload();
            $('#subusuarios').DataTable().ajax.url(`subusuarios/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}&criterio=${JSON.stringify(busqueda)}`);
            $('#subusuarios').DataTable().ajax.reload();
            $('#modal_eliminar_grupo').modal('hide');

            document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');
        }

    })
    .catch(error => {
            /* $.toast({
                heading: 'Error',
                text: 'Ocurrió un error al eliminar el Grupo',
                position: 'bottom-center',
                icon: 'error',
                hideAfter: 1000,
                loader: false
            }) */

        new Notification({
            text: 'Ocurrió un error al eliminar el Grupo',
            style: {
                background: '#DB0632',
                color: '#fff',
                width: '300px',
                height: '60px'
            },
            position: 'bottom-center',
            autoClose: 10000,
            canClose: false,
            showProgress: false,
            pauseOnHover: false,
            pauseOnFocusLoss: false,
            pauseOnHover: false,
            pauseOnFocusLoss: false
        });
    });

})

document.getElementById('boton_asignar_subusuarios_grupos').addEventListener('click', async ()=>{

    let subusuarios_checkeados = [];
    let subusuarios_size = $('#subusuarios').DataTable().data().length;
    let data = new FormData();

    document.getElementById('input_buscar_lista_subusuarios_asignar').value = '';

    document.body.style.cursor = 'wait';

    for(let i=0; i<subusuarios_size; i++)
    {
        if($('#subusuarios').DataTable().cell(i,0).node().children[0].checked)
        {
            subusuarios_checkeados.push($('#subusuarios').DataTable().cell(i,1).data());
        }
    }

    data.append('id_subusuarios', JSON.stringify(subusuarios_checkeados));

    await fetch(`subusuarios/revisar_grupos_subusuarios?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`,
    {
        method: 'post',
        body: data,
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')}

    })
    .then(res => res.json())
    .then(res => {

        document.body.style.cursor = 'default';

        //guardando opciones previamente checkeadas en variable global
        checks_grupos_asignados = [];
        checks_grupos_asignados = res.checks;

        [...document.getElementById('lista_subusuarios_asignar').children].map(data => data.remove());

        /* for (let i = 0; i < res.grupos.length; i++)
        {
            let div = document.createElement('div');
            let input = document.createElement('input');
            let label = document.createElement('label');

            div.classList.add('offset-1','col-11');
            input.setAttribute('type', 'checkbox');
            input.classList.add('subusuarios_asignar', 'mr-1');
            input.id = $('#grupos').DataTable().data()[i][0];
            label.textContent = $('#grupos').DataTable().data()[i][1];
            if(res.checks.includes($('#grupos').DataTable().data()[i][0])) input.checked = true;
            div.append(input);
            div.append(label);

            document.getElementById('lista_subusuarios_asignar').append(div);
        } */

        for (const grupo of res.grupos)
        {
            let div = document.createElement('div');
            let input = document.createElement('input');
            let label = document.createElement('label');

            div.classList.add('offset-1','col-11');
            input.setAttribute('type', 'checkbox');
            input.classList.add('subusuarios_asignar', 'mr-1');
            input.id = grupo.IdGrupoSubUsuario;
            label.textContent = grupo.Grupo.substring(0,18);
            label.setAttribute('for', grupo.IdGrupoSubUsuario);
            label.title = grupo.Grupo;
            if(res.checks.includes(grupo.IdGrupoSubUsuario)) input.checked = true;
            div.append(input);
            div.append(label);

            document.getElementById('lista_subusuarios_asignar').append(div);
        }

        //marcar casilla todos en caso de que esten todos seleccionados

        let conteo_grupos_marcados = 0;
        [...document.getElementById('lista_subusuarios_asignar').children].map(data =>{
            if(data.children[0].checked)
            {
                conteo_grupos_marcados++;
            }
        });

        if( [...document.getElementById('lista_subusuarios_asignar').children].length == conteo_grupos_marcados && conteo_grupos_marcados>0) document.getElementById('seleccionar_todos_subusuarios_asignar').checked = true;
        else document.getElementById('seleccionar_todos_subusuarios_asignar').checked = false;
        $('#modal_asignar_subusuarios_grupo').modal();

    });
})

//seleccionar todos los subusuarios a asignar
document.getElementById('seleccionar_todos_subusuarios_asignar').addEventListener('click', (e)=>{

    if(e.target.checked)
    {
        [...document.getElementsByClassName('subusuarios_asignar')].map(data => {

            if(!data.parentElement.classList.contains('d-none'))
            {
                data.checked = true;
            }

        });
    }
    else
    {
        [...document.getElementsByClassName('subusuarios_asignar')].map(data => {

            if(!data.parentElement.classList.contains('d-none'))
            {
                data.checked = false;
            }

        });
    }

});

//filtrar array con datos unicos
/* function onlyUnique(value, index, self)
{
    return self.indexOf(value) === index;
} */
//fin filtrar array con datos unicos

$('#grupos').on('draw.dt', function()
{
    let size = $('#grupos').DataTable().data().length;

    for (let i = 0; i < size; i++)
    {
        //agregar title a los nombres de los grupos incompletos
        $('#grupos').DataTable().row(i).node().setAttribute('title', $('#grupos').DataTable().cell(i,4).data());
    }

});

$('#subusuarios').on('draw.dt', function()
{
    document.getElementById('numero_total_subusuarios').textContent = $('#subusuarios').DataTable().data().length;

    verificar_checkeo_principal_subusuarios();
    verificar_paginacion();

    $('.check_subusuarios').on('click', ()=>{

        let size = Number($('#subusuarios').DataTable().data().length)
        let checkeados = 0;

        for (let i = 0; i < size; i++)
        {
            if( $('#subusuarios').DataTable().cell(i,0).node().children[0].checked == true ) checkeados++;
        }

        if(checkeados == 0) document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');
        else document.getElementById('boton_asignar_subusuarios_grupos').classList.remove('d-none');

        if( $('#subusuarios').DataTable().data().length == checkeados ) document.getElementById('checkbox_principal_subusuarios').checked = true;
        else document.getElementById('checkbox_principal_subusuarios').checked = false;

    });

});

//no se usa por cargar las catetgorias al cargar la pagina
/* async function actualizar_categorias()
{
    [...document.getElementById('categorias').children].map( e => e.remove());

    await fetch('categorias')
    .then(res => res.json())
    .then(res => {
        console.log(res);

        for (let i = 0; i < res.categorias.length; i++)
        {
            let div = document.createElement('div');
            div.classList.add('row');
            div.setAttribute('categoria', res.categorias[i].Descripcion);
            let div2 = document.createElement('div');
            div2.classList.add('offset-2', 'col-9', 'offset-1');
            let input = document.createElement('input');
            let label = document.createElement('label');
            label.textContent = res.categorias[i].Descripcion;
            div.setAttribute('categoria', res.categorias[i].Descripcion);
            input.setAttribute('type', 'checkbox');
            input.setAttribute('idcategoria', res.categorias[i].IdCategoria);
            input.classList.add('mr-1');
            div2.append(input);
            div2.append(label);
            div.append(div2);

            document.getElementById('categorias').append(div);
        }
    });

    let array_dibujar = $('#subusuarios').DataTable().column(3).data().toArray().filter(onlyUnique);
} */

//checkear todas los recursos correspondientes al momento de agregar subusuario
function asignar_masivo(e){

    if(e.target.checked == true)
    {
        [...document.getElementsByClassName(e.target.getAttribute('clasecheck'))].map(data => {
            if(!data.parentElement.classList.contains('d-none'))
            {
                data.checked = true
            }
        });
    }
    else
    {
        [...document.getElementsByClassName(e.target.getAttribute('clasecheck'))].map(data => {
            if(!data.parentElement.classList.contains('d-none'))
            {
                data.checked = false;
            }
        });
    }
}

//filtrar datos al agregarlos
document.getElementById('busqueda_agrega_recursos_subusuario').addEventListener('input', (e)=>{

    //console.log('busqueda: ', e.target.value);

    let total_vehiculos = 0;
    let total_vehiculos_marcados = 0;
    let total_alertas = 0;
    let total_alertas_marcadas = 0;
    let total_modulos = 0;
    let total_modulos_marcados = 0;
    let total_grupopuntos = 0;
    let total_grupopuntos_marcados = 0;
    let total_grupogeocercas = 0;
    let total_grupogeocercas_marcados = 0;

    //variables que cambiando cuando se encuentra al 1 menos un recurso para poder abrir el arbol
    let vehiculos_encontrados = false;
    let alertas_encontradas = false;
    let modulos_encontrados = false;
    let grupopuntos_encontrados = false;
    let grupogeocercas_encontradas = false;

    //filtrando en la tabla de vehiculos
    [...document.getElementById('lista_vehiculos_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            vehiculos_encontrados = true;
            data.classList.add('d-block');
            data.classList.remove('d-none');
            total_vehiculos++;
            if(data.children[0].checked) total_vehiculos_marcados++;
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    });
    //filtrando en la tabla de puntos
    /* [...document.getElementById('lista_puntos_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            data.classList.add('d-block');
            data.classList.remove('d-none');
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    }); */
    //filtrando en la tabla de geocercas
    /* [...document.getElementById('lista_geocercas_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            data.classList.add('d-block');
            data.classList.remove('d-none');
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    }); */
    //filtrando en la tabla de alertas
    [...document.getElementById('lista_alertas_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            alertas_encontradas = true;
            data.classList.add('d-block');
            data.classList.remove('d-none');
            total_alertas++;
            if(data.children[0].checked) total_alertas_marcadas++;
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    });
    //filtrando en la tabla de modulos
    [...document.getElementById('lista_modulos_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            modulos_encontrados = true;
            data.classList.add('d-block');
            data.classList.remove('d-none');
            total_modulos++;
            if(data.children[0].checked) total_modulos_marcados++;
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    });
    //filtrando en la tabla de grupo_puntos
    [...document.getElementById('lista_grupo_puntos_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            grupopuntos_encontrados = true;
            data.classList.add('d-block');
            data.classList.remove('d-none');
            total_grupopuntos++;
            if(data.children[0].checked) total_grupopuntos_marcados++;
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    });
    //filtrando en la tabla de grupo_geocercas
    [...document.getElementById('lista_grupo_geocercas_agregar').children].map(data => {
        if(String(data.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            grupogeocercas_encontradas = true;
            data.classList.add('d-block');
            data.classList.remove('d-none');
            total_grupogeocercas++;
            if(data.children[0].checked) total_grupogeocercas_marcados++;
        }
        else
        {
            data.classList.add('d-none');
            data.classList.remove('d-block');
        }
    });

    /* console.log('total_vehiculos: ', total_vehiculos);
    console.log('total_vehiculos_marcados: ', total_vehiculos);
    console.log('total_alertas: ', total_alertas);
    console.log('total_alertas_marcados: ', total_alertas);
    console.log('total_modulos: ', total_modulos);
    console.log('total_modulos_marcados: ', total_modulos);
    console.log('total_grupopuntos: ', total_grupopuntos);
    console.log('total_grupopuntos_marcados: ', total_grupopuntos);
    console.log('total_grupogeocercas: ', total_grupogeocercas);
    console.log('total_grupogeocercas_marcados: ', total_grupogeocercas); */

    //verificar si todos los recursos encontrados estan marcados para dejar marcado su check principal de cada recurso
    if(total_vehiculos == total_vehiculos_marcados && total_vehiculos_marcados>0)
    {
        document.getElementById('checkbox_principal_asignar_vehiculo').checked = true;
    }
    else
    {
        document.getElementById('checkbox_principal_asignar_vehiculo').checked = false;
    }

    if(total_alertas == total_alertas_marcadas && total_alertas_marcadas>0)
    {
        document.getElementById('checkbox_principal_asignar_alerta').checked = true;
    }
    else
    {
        document.getElementById('checkbox_principal_asignar_alerta').checked = false;
    }

    if(total_modulos == total_modulos_marcados && total_modulos_marcados>0)
    {
        document.getElementById('checkbox_principal_asignar_modulo').checked = true;
    }
    else
    {
        document.getElementById('checkbox_principal_asignar_modulo').checked = false;
    }

    if(total_grupopuntos == total_grupopuntos_marcados && total_grupopuntos_marcados>0)
    {
        document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = true;
    }
    else
    {
        document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = false;
    }

    if(total_grupogeocercas == total_grupogeocercas_marcados && total_grupogeocercas_marcados>0)
    {
        document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = true;
    }
    else
    {
        document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = false;
    }
    //fin verificacion

    //verificar que se haya encontrado al menos algun registro de un recurso para dejar abierto el arbol en ese recurso
    if(vehiculos_encontrados && e.target.value !='') document.getElementById('lista_vehiculos_agregar').parentElement.setAttribute('open', '');
    else document.getElementById('lista_vehiculos_agregar').parentElement.removeAttribute('open');

    if(alertas_encontradas  && e.target.value !='') document.getElementById('lista_alertas_agregar').parentElement.setAttribute('open', '');
    else document.getElementById('lista_alertas_agregar').parentElement.removeAttribute('open');

    if(modulos_encontrados  && e.target.value !='') document.getElementById('lista_modulos_agregar').parentElement.setAttribute('open', '');
    else document.getElementById('lista_modulos_agregar').parentElement.removeAttribute('open');

    if(grupopuntos_encontrados  && e.target.value !='') document.getElementById('lista_grupo_puntos_agregar').parentElement.setAttribute('open', '');
    else document.getElementById('lista_grupo_puntos_agregar').parentElement.removeAttribute('open');

    if(grupogeocercas_encontradas  && e.target.value !='') document.getElementById('lista_grupo_geocercas_agregar').parentElement.setAttribute('open', '');
    else document.getElementById('lista_grupo_geocercas_agregar').parentElement.removeAttribute('open');

    //fin verificacion

    //console.log('vehiculos_encontrados: ', vehiculos_encontrados);
    //console.log('alertas_encontradas: ', alertas_encontradas);
    //console.log('modulos_encontrados: ', modulos_encontrados);
    //console.log('grupopuntos_encontrados: ', grupopuntos_encontrados);
    //console.log('grupogeocercas_encontradas: ', grupogeocercas_encontradas);

})

document.getElementById('input_buscar_lista_subusuarios_asignar').addEventListener('input', (e)=>{

    [...document.getElementsByClassName('subusuarios_asignar')].map(data => {

        if(String(data.nextElementSibling.textContent).toLowerCase().includes(e.target.value.toLowerCase()))
        {
            data.parentElement.classList.add('d-block');
            data.parentElement.classList.remove('d-none');
        }
        else
        {
            data.parentElement.classList.add('d-none');
            data.parentElement.classList.remove('d-block');
        }
    });

    //quitar visto en seleccionar todos cuando se vuelva a buscar y no todos esten seleccionados

    let visibles = 0;
    let total_checkeados = 0;
    [...document.getElementsByClassName('subusuarios_asignar')].map(data => {

        if(!data.checked && !data.parentElement.classList.contains('d-none'))
        {
            document.getElementById('seleccionar_todos_subusuarios_asignar').checked = false;
            //return;
        }
        else if(data.checked && !data.parentElement.classList.contains('d-none'))
        {
            total_checkeados++;
        }

        if( !data.parentElement.classList.contains('d-none') ) visibles++;

        //console.log(visibles);

    });

    if(visibles == total_checkeados && total_checkeados>0) document.getElementById('seleccionar_todos_subusuarios_asignar').checked = true;

})


document.getElementById('abrir_filtro_categorias').addEventListener('click', ()=>{
    //actualizar_categorias();
    $('#modal_filtrar_subusuarios_categoria').modal();
    document.getElementById('input_buscar_categoria').focus();
})

document.getElementById('seleccionar_todos_filtros_categoria').addEventListener('click', ()=>{

    if(document.getElementById('seleccionar_todos_filtros_categoria').checked)
    {
        [...document.getElementById('categorias').children].map(data => {
            if(!data.classList.contains('d-none')) data.children[0].children[0].checked = true;
        });
    }
    else
    {
        [...document.getElementById('categorias').children].map(data => {
            if(!data.classList.contains('d-none')) { data.children[0].children[0].checked = false; }
        });
    }
})

//al presionar los checks de categorias para la interaccion con el check principal
document.getElementById('categorias').addEventListener('click', (e)=>{

    let total_inputs = [...document.getElementById('categorias').children].length;
    let inputs_checkeados = 0;
    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('categorias').children].map( data => {

            if( !data.classList.contains('d-none') )
            {
                if( !data.children[0].children[0].checked ) document.getElementById('seleccionar_todos_filtros_categoria').checked = false;
                else inputs_checkeados++;
            }
        });

        if(total_inputs == inputs_checkeados) document.getElementById('seleccionar_todos_filtros_categoria').checked = true;
    }
})
//fin al presionar checks de categorias

document.getElementById('asignar_subusuarios_grupos').addEventListener('click', ()=>{

    let ids_subusuarios = [];
    let ids_grupos = [];
    let size_subusuarios = $('#subusuarios').DataTable().data().length;

    [...document.getElementById('lista_subusuarios_asignar').children].map(data => {
        if(data.children[0].checked) ids_grupos.push(data.children[0].id);
    });

    for (let i = 0; i < size_subusuarios; i++)
    {
        if( $('#subusuarios').DataTable().cell(i,0).node().children[0].checked ) ids_subusuarios.push( $('#subusuarios').DataTable().cell(i,1).data() );
    }

    let data = new FormData();
    data.append('ids_grupos',JSON.stringify(ids_grupos));
    data.append('ids_subusuarios',JSON.stringify(ids_subusuarios));
    data.append('checks_anteriores',JSON.stringify(checks_grupos_asignados));

    fetch(`subusuarios/asignar_grupos_subusuarios?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`, {
        method: 'post',
        body: data,
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')}
    })
    .then(res => res.json())
    .then(res => {

        if(res.sms == 'ok')
        {

            /* $.toast({
                heading: 'Asignado',
                text: 'Grupo asignado con exito',
                position: 'bottom-center',
                icon: 'success',
                hideAfter: 1000,
                loader: false
            }) */

            new Notification({
                text: 'Grupo asignado con exito',
                style: {
                    background: '#36BB34',
                    color: '#fff',
                    width: '260px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 3000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

            $('#subusuarios').DataTable().ajax.reload();

        }

    })
    .catch(error => {
            /* $.toast({
                heading: 'Error',
                text: 'Ocurrió un error al asignar el Grupo',
                position: 'bottom-center',
                icon: 'error',
                hideAfter: 1000,
                loader: false
            }); */

            new Notification({
                text: 'Ocurrió un error al asignar el Grupo',
                style: {
                    background: '#DB0632',
                    color: '#fff',
                    width: '300px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 10000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });
    });

    $('#modal_asignar_subusuarios_grupo').modal('hide');
    $('#modal_eliminar_grupo').modal('hide');

    document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');

    /* console.log('ids_grupos: ', ids_grupos);
    console.log('ids_subusuarios: ', ids_subusuarios); */
});

//al cerrar el modal filtrar por categorias
$("#modal_filtrar_subusuarios_categoria").on('hidden.bs.modal', function () {
    busqueda.categorias = [];
    let bandera_marcar_todas = true;
    let marcados = 0; //para saber cuantos checkeds están marcados

    [...document.getElementById('categorias').children].map(data => {
        if(data.children[0].children[0].checked)
        {
            busqueda.categorias.push(Number(data.children[0].children[0].getAttribute('idcategoria')));
        }
    })
    $('#subusuarios').DataTable().ajax.url(`subusuarios/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}&criterio=${JSON.stringify(busqueda)}`);
    $('#subusuarios').DataTable().ajax.reload();

    if(busqueda.categorias.length>0)
    {
        document.getElementById('abrir_filtro_categorias').children[0].setAttribute('src',  document.getElementById('abrir_filtro_categorias').children[0].getAttribute('src').replace('clasificar b', 'filtrado') );
    }
    else
    {
        document.getElementById('abrir_filtro_categorias').children[0].setAttribute('src',  document.getElementById('abrir_filtro_categorias').children[0].getAttribute('src').replace('filtrado', 'clasificar b') );
    }

    //limpiar el input y mostrar ocultos
    document.getElementById('input_buscar_categoria').value = '';
    document.getElementById('seleccionar_todos_filtros_categoria').checked = false;
    [...document.getElementById('categorias').children].map(data => data.classList.remove('d-none'));

    //verificar si todos los checks estan marcados para poner el checked principal marcado

    [...document.getElementById('categorias').children].map(data => {
        if(data.children[0].children[0].checked == false) bandera_marcar_todas = false;
        else marcados++;
    });

    if(bandera_marcar_todas == true && marcados>0) document.getElementById('seleccionar_todos_filtros_categoria').checked = true;
    else document.getElementById('seleccionar_todos_filtros_categoria').checked = false;


});


/* $('#modal_filtrar_subusuarios_categoria').on('shown.bs.modal', ()=>{

}); */
//fin al cerrar el modal filtrar por categorias

/* $('#grupos').on('click', 'tr', function(e){
    console.log('click', e.target.nodeName);
    if(e.target.nodeName != 'BUTTON' && e.target.nodeName != 'IMG')
    {
        console.log( $('#grupos').DataTable().row( this ).data() );
        console.log($('#grupos').DataTable().rows({selected:true}).count())
    }
}); */

//Buscando subusuarios por grupo
$('#grupos').on( 'click', 'tbody tr', function (e)
{
    busqueda.grupo = 0;

    if(e.target.nodeName == 'IMG' || e.target.nodeName == 'BUTTON') return; //evitar que al editar o eliminar se seleccione un registro de los grupos

    if ( $('#grupos').DataTable().row( this, { selected: true } ).any() )
    {
        $('#grupos').DataTable().row( this ).deselect();
    }
    else
    {
        //para que solo se seleccione 1
        if($('#grupos').DataTable().rows({selected:true}).count()>0) $('#grupos').DataTable().rows().deselect();

        $('#grupos').DataTable().row( this ).select();
    }

    if( $('#grupos').DataTable().rows({selected:true}).count()>0)
    {
        busqueda.grupo = $('#grupos').DataTable().rows({selected:true}).data()[0][0];
    }

    $('#subusuarios').DataTable().ajax.url(`subusuarios/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}&criterio=${JSON.stringify(busqueda)}`);
    $('#subusuarios').DataTable().ajax.reload();

    document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');

} );
//Fin buscando subusuarios por grupo

//Verificar si se envia campo de fecha de caducidad
document.getElementById('agregar_fecha_caducidad').addEventListener('change', (e)=>{
    if(e.target.value == "1")
    {
        if( document.getElementById('validohasta_subusuario').value == '') document.getElementById('validohasta_subusuario').value = new Date().toJSON().slice(0,10);
        document.getElementById('validohasta_subusuario').classList.remove('d-none');
    }
    else
    {
        document.getElementById('validohasta_subusuario').classList.add('d-none');
    }
});
//Fin verificar si se envia campo de fecha de caducidad

//Detecta cambio de las dimensiones de la pantalla
//*Con la finalidad de ajustar la paginacion de los subusuarios
addEventListener('resize', ()=>{
    //console.log('resize');
    if(screen.width>800)
    {
        if( screen.height > 700 ) $('#subusuarios').DataTable().page.len( 10 ).draw();
        else $('#subusuarios').DataTable().page.len( 7 ).draw();
    }
    else
    {
        if( screen.height > 700 ) $('#subusuarios').DataTable().page.len( 7 ).draw();
        else $('#subusuarios').DataTable().page.len( 4 ).draw();
    }

});
//Fin detecta cambio de las dimensiones de la pantalla

//obtener parametros de url
function getParameterByName(name)
{
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
    results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

//funcion para verificar si el check principal de subusuarios permanece marcado o no
function verificar_checkeo_principal_subusuarios()
{
    let size = Number($('#subusuarios').DataTable().data().length)
    let checkeados = 0;

    for (let i = 0; i < size; i++)
    {
        if( $('#subusuarios').DataTable().cell(i,0).node().children[0].checked == true ) checkeados++;
    }

    if( $('#subusuarios').DataTable().data().length == checkeados && checkeados>0) document.getElementById('checkbox_principal_subusuarios').checked = true;
    else document.getElementById('checkbox_principal_subusuarios').checked = false;
}

function detectar_paginacion()
{
    if(screen.width>800)
    {
        if( screen.height > 700 ) return 10;
        else return 7;
    }
    else
    {
        if( screen.height > 700 ) return 7;
        else return 4;
    }

}

document.getElementById('lista_subusuarios_asignar').addEventListener('click', (e)=>{

    let bandera_marcar_check_principal = 1;

    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('lista_subusuarios_asignar').children].map( data => {

            if(data.children[0].checked == false && !data.classList.contains('d-none'))
            {
                bandera_marcar_check_principal = 0;
            }

        });
    }

    if(bandera_marcar_check_principal == 1) document.getElementById('seleccionar_todos_subusuarios_asignar').checked = true;
    else document.getElementById('seleccionar_todos_subusuarios_asignar').checked = false;

});

//edicion en columnas
$('#subusuarios tbody').on('click', 'tr td', async (e)=>{

    if(e.target.nodeName != 'TD') return;

    id_tabla_subusuario_anterior_actualizar = '';
    nombre_completo_subusuario_tabla = '';
    nombre_subusuario_tabla = '';
    campo_actualizar_tabla = '';

    //console.log( $('#subusuarios').DataTable().cell(e.target._DT_CellIndex.row, e.target._DT_CellIndex.column).node() );

    let id_subusuario;

    if(e.target.cellIndex == 1 || e.target.cellIndex == 2)
    {
        //console.log(e.target.cellIndex);
        $('#subusuarios').DataTable().row(e.target._DT_CellIndex.row).data();

        id_subusuario = $('#subusuarios').DataTable().row(e.target._DT_CellIndex.row).data()[1];

        $('#subusuarios').DataTable().cell(e.target._DT_CellIndex.row, e.target._DT_CellIndex.column).data('<input onblur="guardado_rapido(event)" class="form-control" value="'+e.target.textContent+'">');

        //focus al imput invocado
        $('#subusuarios').DataTable().cell(e.target._DT_CellIndex.row, e.target._DT_CellIndex.column).node().firstChild.focus();

        //guardar valores anteriores para compararlos y si hay cambios guardar
        id_tabla_subusuario_anterior_actualizar = id_subusuario;

        //guardar en la variable nombrecompleto subusuarios o subusuarios
        if(e.target.cellIndex == 1)
        {
            nombre_completo_subusuario_tabla = e.target.children[0].value;
            nombre_subusuario_tabla = '';
            campo_actualizar_tabla = 'nombrecompleto';
        }

        if(e.target.cellIndex == 2)
        {
            nombre_completo_subusuario_tabla = '';
            nombre_subusuario_tabla = e.target.children[0].value;
            campo_actualizar_tabla = 'subusuario';
        }
        //console.log('e.target.textContent:', e.target.children[0].value);
        //console.log('nombre_completo_subusuario_tabla', nombre_completo_subusuario_tabla);
        //console.log('nombre_subusuario_tabla', nombre_subusuario_tabla);

    }
});

//para guardar el campo luego del desenfoque
async function guardado_rapido(event)
{
    //console.log('guardado rapido: ', event.target.value);
    let data = new FormData();
    let size = $('#subusuarios').DataTable().data().length;

    // si los valores son iguales entonces no se actualiza
    if( campo_actualizar_tabla == 'nombrecompleto')
    {
        if(nombre_completo_subusuario_tabla == event.target.value)
        {
            $('#subusuarios').DataTable().ajax.reload();
            return;
        }

        nombre_completo_subusuario_tabla = event.target.value;
    }

    if( campo_actualizar_tabla ==  'subusuario')
    {
        if(nombre_subusuario_tabla == event.target.value)
        {
            $('#subusuarios').DataTable().ajax.reload();
            return;
        }

        nombre_subusuario_tabla = event.target.value;
    }

    //quitar input y poner el campo como estaba
    /* for (let i = 0; i < size; i++)
    {
        if( $('#subusuarios').DataTable().cell(i,1).data() ==  id_tabla_subusuario_anterior_actualizar)
        {
            if( campo_actualizar_tabla == 'nombrecompleto')
            {
                $('#subusuarios').DataTable().cell(i,2).data(event.target.value);
            }
            else if( campo_actualizar_tabla == 'subusuario' )
            {
                $('#subusuarios').DataTable().cell(i,3).data(event.target.value);
            }
        }
    } */

    data.append('id',id_tabla_subusuario_anterior_actualizar);
    data.append('nombre',nombre_completo_subusuario_tabla);
    data.append('subusuario',nombre_subusuario_tabla);
    data.append('campo_actualizar',campo_actualizar_tabla);

    await fetch(`subusuarios/edicion_tabla?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}`, {
        method: 'post',
        body: data,
        headers: {'X-CSRF-TOKEN': document.querySelector('meta[name=csrf-token]').getAttribute('content')},
    })
    .then( res => res.json())
    .then(res => {

        if(res.sms == 'ok')
        {
            new Notification({
                text: (campo_actualizar_tabla == 'nombrecompleto') ? 'Nombre cambiado con exito' : 'Subusuario cambiado con exito',
                style: {
                    background: '#36BB34',
                    color: '#fff',
                    width: '230px',
                    height: '60px'
                },
                position: 'bottom-center',
                autoClose: 3000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

            $('#subusuarios').DataTable().ajax.reload();
            document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');

            //quitar input y poner el campo como estaba
            /* for (let i = 0; i < size; i++)
            {
                if( $('#subusuarios').DataTable().cell(i,1).data() ==  id_tabla_subusuario_anterior_actualizar)
                {
                    if( campo_actualizar_tabla == 'nombrecompleto')
                    {
                        $('#subusuarios').DataTable().cell(i,2).data(event.target.value);
                    }
                    else if( campo_actualizar_tabla == 'subusuario' )
                    {
                        $('#subusuarios').DataTable().cell(i,3).data(event.target.value);
                    }
                }
            } */

        }
        else
        {
            new Notification({
                text: res.sms,
                style: {
                    background: '#DB0632',
                    color: '#fff',
                    width: '230px',
                    height: ajustar_altura(res.sms.length),
                },
                position: 'bottom-center',
                autoClose: 10000,
                canClose: false,
                showProgress: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false,
                pauseOnHover: false,
                pauseOnFocusLoss: false
            });

            /* console.log('nombre_completo_subusuario_tabla', nombre_completo_subusuario_tabla);
            console.log('nombre_subusuario_tabla', nombre_subusuario_tabla); */

            /* for (let i = 0; i < size; i++)
            {
                if( $('#subusuarios').DataTable().cell(i,1).data() ==  id_tabla_subusuario_anterior_actualizar)
                {
                    if( campo_actualizar_tabla == 'nombrecompleto')
                    {
                        $('#subusuarios').DataTable().cell(i,2).data(nombre_completo_subusuario_tabla);
                    }
                    else if( campo_actualizar_tabla == 'subusuario' )
                    {
                        $('#subusuarios').DataTable().cell(i,3).data(nombre_subusuario_tabla);
                    }
                }
            } */

            $('#subusuarios').DataTable().ajax.reload();
            document.getElementById('boton_asignar_subusuarios_grupos').classList.add('d-none');

        }

    });
}

document.getElementById('lista_vehiculos_agregar').addEventListener('click', (e)=>{

    let marcados = 0;
    let total_inputs = 0;
    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('lista_vehiculos_agregar').children].map(data => {
            if(data.children[0].checked && !data.classList.contains('d-none')) marcados++;
            if(!data.classList.contains('d-none')) total_inputs++;
        });
    }

    if( (total_inputs == marcados) && marcados>0 ) document.getElementById('checkbox_principal_asignar_vehiculo').checked = true;
    else document.getElementById('checkbox_principal_asignar_vehiculo').checked = false;

});


document.getElementById('lista_alertas_agregar').addEventListener('click', (e)=>{

    let marcados = 0;
    let total_inputs = 0;
    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('lista_alertas_agregar').children].map(data => {
            if(data.children[0].checked && !data.classList.contains('d-none')) marcados++;
            if(!data.classList.contains('d-none')) total_inputs++;
        });
    }

    if( (total_inputs == marcados) && marcados>0 ) document.getElementById('checkbox_principal_asignar_alerta').checked = true;
    else document.getElementById('checkbox_principal_asignar_alerta').checked = false;

});

document.getElementById('lista_modulos_agregar').addEventListener('click', (e)=>{

    let marcados = 0;
    let total_inputs = 0;
    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('lista_modulos_agregar').children].map(data => {
            if(data.children[0].checked && !data.classList.contains('d-none')) marcados++;
            if(!data.classList.contains('d-none')) total_inputs++;
        });
    }

    if( (total_inputs == marcados) && marcados>0 ) document.getElementById('checkbox_principal_asignar_modulo').checked = true;
    else document.getElementById('checkbox_principal_asignar_modulo').checked = false;

});

document.getElementById('lista_grupo_puntos_agregar').addEventListener('click', (e)=>{

    let marcados = 0;
    let total_inputs = 0;
    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('lista_grupo_puntos_agregar').children].map(data => {
            if(data.children[0].checked && !data.classList.contains('d-none')) marcados++;
            if(!data.classList.contains('d-none')) total_inputs++;
        });
    }

    if( (total_inputs == marcados) && marcados>0 ) document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = true;
    else document.getElementById('checkbox_principal_asignar_grupo_puntos').checked = false;

});

document.getElementById('lista_grupo_geocercas_agregar').addEventListener('click', (e)=>{

    let marcados = 0;
    let total_inputs = 0;
    if(e.target.nodeName == 'INPUT')
    {
        [...document.getElementById('lista_grupo_geocercas_agregar').children].map(data => {
            if(data.children[0].checked && !data.classList.contains('d-none')) marcados++;
            if(!data.classList.contains('d-none')) total_inputs++;
        });
    }

    if( (total_inputs == marcados) && marcados>0 ) document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = true;
    else document.getElementById('checkbox_principal_asignar_grupo_geocercas').checked = false;

});

//ajusta la altura de la notificacion dependiendo de cuantos errores haya encontrado la validacion
function ajustar_altura(talla_errores)
{
    if(talla_errores == 0) return '0px';
    else if(talla_errores == 1) return '60px';
    else if(talla_errores == 2) return '90px';
    else if(talla_errores == 3) return '110px';
    else if(talla_errores == 4) return '130px';
    else return '220px';
}

document.getElementById('boton_reiniciar_subusuarios').addEventListener('click', ()=>{
    $('#grupos').DataTable().$('tr.selected').removeClass('selected');

    busqueda.grupo = 0;

    busqueda.categorias = [];
    busqueda.nombre = '';
    document.getElementById('busqueda_general').value = '';

    document.getElementById('input_buscar_categoria').value = '';
    document.getElementById('abrir_filtro_categorias').children[0].setAttribute('src',  document.getElementById('abrir_filtro_categorias').children[0].getAttribute('src').replace('filtrado', 'clasificar b') );

    [...document.getElementById('categorias').children].map(data =>{
        data.classList.remove('d-none');
        data.children[0].children[0].checked = false;
    });

    document.getElementById('seleccionar_todos_filtros_categoria').checked = false;

    $('#subusuarios').DataTable().ajax.url(`subusuarios/consultar?o=${getParameterByName('o')}&cp=${getParameterByName('cp')}&criterio=${JSON.stringify(busqueda)}`);
    $('#subusuarios').DataTable().ajax.reload();
});

//cuando hay errores de subusuario cerra la pagina actual
$('#subusuarios').on('error.dt', function ( e, settings, techNote, message )
{
    close();
});

//para mostrar o esconder la paginacion dependiendo de la cantidad de registros que aparezcan al agregar o eliminar subusuarios
function verificar_paginacion()
{
    if(document.getElementById('subusuarios_paginate').children[1].children.length>1)
    {
        document.getElementById('subusuarios_paginate').classList.remove('d-none');
    }
    else
    {
        document.getElementById('subusuarios_paginate').classList.add('d-none');
    }
}

//validar formulario de subusuarios, si devuelve true es porque ha encontrado un error
function validar_formulario_subusuarios()
{
    let bandera_error = false; //se vuelve true cuando encuentra un error
    let focus = [];    //verificar que ningun campo obligatorio quede vacio

    //limpiar de errores antes de validarlos
    document.getElementById('nombrecompleto_subusuario').classList.remove('is-invalid');
    document.getElementById('nombre_subusuario').classList.remove('is-invalid');
    document.getElementById('clave_subusuario').classList.remove('is-invalid');
    document.getElementById('email_subusuario').classList.remove('is-invalid');
    //fin de limpiar errores

    if(document.getElementById('nombrecompleto_subusuario').value.trim() == '')
    {
        document.getElementById('nombrecompleto_subusuario').focus();
        document.getElementById('nombrecompleto_subusuario').classList.add('is-invalid');
        bandera_error = true;
        focus.push('nombrecompleto_subusuario');
    }
    else
    {
        document.getElementById('nombrecompleto_subusuario').classList.remove('is-invalid');
    }

    if(document.getElementById('nombre_subusuario').value.trim() == '')
    {
        document.getElementById('nombre_subusuario').focus();
        document.getElementById('nombre_subusuario').classList.add('is-invalid');
        bandera_error = true;
        focus.push('nombre_subusuario');
    }
    else
    {
        document.getElementById('nombre_subusuario').classList.remove('is-invalid');
    }

    if(document.getElementById('clave_subusuario').value.trim() == '')
    {
        document.getElementById('clave_subusuario').focus();
        document.getElementById('clave_subusuario').classList.add('is-invalid');
        bandera_error = true;
        focus.push('clave_subusuario');
    }
    else
    {
        document.getElementById('clave_subusuario').classList.remove('is-invalid');
    }

    if( !/^\w+([.-_+]?\w+)*@\w+([.-]?\w+)*(\.\w{2,10})+$/.test(document.getElementById('email_subusuario').value))
    {
        document.getElementById('email_subusuario').focus();
        document.getElementById('email_subusuario').classList.add('is-invalid');
        bandera_error = true;
        focus.push('email_subusuario');
    }
    else
    {
        document.getElementById('email_subusuario').classList.remove('is-invalid');
    }
    //console.log(focus);
    if(focus.length>0) document.getElementById(focus[0]).focus();
    return bandera_error;
}

function validar_formulario_grupo()
{
    let bandera_error = false;
    let focus = [];

    if(document.getElementById('nombre_grupo').value == "")
    {
        document.getElementById('nombre_grupo').classList.add('is-invalid');
        focus.push('nombre_grupo');
        bandera_error= true;
    }
    else
    {
        document.getElementById('nombre_grupo').classList.remove('is-invalid');
    }

    if(focus.length>0) document.getElementById(focus[0]).focus();

    return bandera_error;
}

/* document.getElementById('ver_password').addEventListener('click', ()=>{
    if(document.getElementById('ver_password').children[0].classList.contains('fa-eye'))
    {
        document.getElementById('ver_password').children[0].classList.remove('fa-eye');
        document.getElementById('ver_password').children[0].classList.add('fa-eye-slash');
        document.getElementById('clave_subusuario').setAttribute('type', 'text');
        document.getElementById('ver_password').setAttribute('data-original-title', 'Ocultar');
    }
    else
    {
        document.getElementById('ver_password').children[0].classList.remove('fa-eye-slash');
        document.getElementById('ver_password').children[0].classList.add('fa-eye');
        document.getElementById('clave_subusuario').setAttribute('type', 'password');
        document.getElementById('ver_password').setAttribute('data-original-title', 'Mostrar');
    }
}); */


//detectar cuando modal se abre de agregar subusuarios
$('#modal-crear-subusuarios').on('shown.bs.modal', ()=>{
    document.getElementById('nombrecompleto_subusuario').focus();
   /*  document.getElementById('clave_subusuario').setAttribute('type', 'password');
    document.getElementById('ver_password').children[0].classList.remove('fa-eye-slash');
    document.getElementById('ver_password').children[0].classList.add('fa-eye'); */
});

//evitar que existan espacios en blanco en subusuario
document.getElementById('nombre_subusuario').addEventListener('input', (e) => {
    document.getElementById('nombre_subusuario').value = e.target.value.trim();
});
//fin evitar que existan espacios en blanco en subusuario
