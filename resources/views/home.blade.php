@extends('layout.app')

@section('title', 'SUBUSUARIOS')

@section('css')
    <link rel="stylesheet" href="{{asset('css/estilos.css')}}">
@endsection

@section('content')
    {{-- <div class="navbar navbar-light bg-dark">
        <a class="navbar-brand" href="#">
            <img src="http://www2.huntermonitoreo.com/artemisdev/Login/images/hmn2/logo_hmn2.png" width="130" height="40" class="d-inline-block align-top">
        </a>
        <p class="text-right text-white">ADMINISTRACION DE SUBUSUARIOS</p>
    </div> --}}
    <div class="container-fluid pt-1">
        <div class="row pb-1">
            <div class="col-12">
                <div class="card fondo_card">
                    <div class="pt-2 pb-2">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-1 col-3 text-center">
                                <button id="collapse" data-toggle="collapse" href="#lista_card" class="btn border-0" title="Mostrar/Ocultar Grupos"><img class="imag-icon" src="{{asset('iconos/hamburguesa.png')}}"></button>
                            </div>
                            <div class="col-sm-9 col-md-9 col-lg-2 col-9">
                                <h5 class=""><img class="imag-icon m-auto" src="{{asset('iconos/subusuarios.png')}}" alt="subusuarios"><b class="m-auto">Subusuarios</b>{{-- <span id="numero_total_subusuarios" class="badge badge-dark"></span> --}} {{-- <button id="crear_nuevo_subusuario" class="btn"><img class="mb-1 imag-icon" src="{{asset('iconos/agregar.png')}}" alt="agregar"></button> --}}</h5>
                            </div>
                            <div class="col-sm-12 col-md-12 col-lg-9 col-12">
                                <div class="row">
                                    <div class="input-group col-12 col-md-12">
                                        {{-- <span class="input-group-append">
                                            <button class="border-right-0 border" type="button">
                                                <i class="fa fa-search"></i>
                                            </button>
                                        </span> --}}
                                        <input class="form-control border mx-4" type="search" placeholder="Buscar Subusuarios ..." id="busqueda_general">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row fondo_card m-auto" style="height:83vh;">
            <div id="lista_card" class="col-sm-3 fondo_card col-md-4 col-xl-3 col-12 pt-1 collapse width show">
                <div class="card fondo_card" {{-- style="height:83vh;" --}}>
                    <div class="row">
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <button id="boton_reiniciar_subusuarios" class="botones_tabla btn col-10 m-auto" style="text-align:left" title="Mostrar todos los subusuarios"><b class="mr-2">Subusuarios</b><span id="numero_total_subusuarios" class="badge badge-dark m-auto"></span></button><button id="crear_nuevo_subusuario" class="col-2 btn botones_tabla" title="Agregar Subusuario"><img class="imag-icon" src="{{asset('iconos/agregar.png')}}" alt="Agregar Subusuario"></button>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <table id="grupos" width="100%">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Grupos<button id="agregar_grupos" data-toggle="modal" data-target="#grupoSubusuarioModal" class="btn botones_tabla" title="Agregar Grupos"><img class="imag-icon" src="{{asset('iconos/agregar.png')}}" alt="Agregar Grupo"></button></th>
                                            <th>Descripcion</th>
                                            <th style="width:40%;" class="text-right"> <button id="colapsar_grupos" class="btn botones_tabla" title="Contraer/Expandir Grupos"><img class="imag-icon" src="{{asset('iconos/doble flecha.png')}}" alt="Eliminar"></button>
                                                {{-- <button id="agregar_grupos" data-toggle="modal" data-target="#grupoSubusuarioModal" class="btn" title="Agregar"><img class="imag-icon" src="{{asset('iconos/agregar.png')}}" alt="agregar"></button>
                                                <button id="colapsar_grupos" class="btn" ><img class="imag-icon" src="{{asset('iconos/doble flecha g.png')}}" title="Eliminar" alt="Eliminar"></button> --}}
                                            </th>
                                            <th>GrupoCompleto</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                    {{-- <div class="col-sm-1 col-2">
                        <button id="collapse" title="ocultar" style="border-radius:25px 0px 0px 25px;" data-toggle="collapse" href="#lista_card" class="btn btn-outline-secondary bg-dark border-left-0 img-rotate"><img class="imag-icon" src="{{asset('iconos/derecha.png')}}"></button>
                    </div> --}}
                </div>
            </div>
            <div class="col-sm-9 col-md-8 col-xl-9 col-12 pt-1 fondo_card" id="spinner">
                <div id="datos" class="card fondo_card mb-1" {{-- style="height:83vh;" --}}>
                    <button id="boton_asignar_subusuarios_grupos" class="btn col-lg-1 col-2 botones_tabla d-none"><img class="imag-icon" src="{{asset('iconos/asignar grupo.png')}}" alt="asignar subusuarios"></button>
                    <div style="padding-top:0px; padding-button:0px;" class="card-body collapse show" id="datos_completos_usuarios">
                        {{-- <ul class="nav nav-tabs" id="myTab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active seleccionada" id="home-tab" data-toggle="tab" href="#datos-subusuario" role="tab" aria-controls="home" aria-selected="true"><h5><b>SUBUSUARIOS</b></h5></a>
                            </li>
                        </ul> --}}

                        <div class="tab-content" id="myTabContent">
                            <ul class="nav nav-tabs pb-1" id="myTab" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active seleccionada" id="home-tab" data-toggle="tab" href="#d" role="tab" aria-controls="home" aria-selected="true"><h2 style="font-size: 12px;"><b>SUBUSUARIOS</b></h2></a>
                                </li>
                            </ul>
                            <div id="vehiculos-asignados" role="tabpanel" class=" p-1 bg-white table-responsive">
                                <table id="subusuarios" width="100%" class="p-auto">
                                    <thead class="fondo_cabecera" style="color:white;">
                                        <tr>
                                            <th><input class="mycheck check_subusuarios d-block m-auto" type="checkbox" id="checkbox_principal_subusuarios"></th>
                                            <th style="font-size:13px;">Código</th>
                                            <th style="font-size:13px;">Nombre</th>
                                            <th style="font-size:13px;">Subusuario</th>
                                            <th style="font-size:13px;">Caducidad</th>
                                            <th style="font-size:13px;">Categoría <button id="abrir_filtro_categorias" class="btn" title="Filtrar Categorias"><img class="mb-1" style="height:20px; width:20px;" src="{{asset('iconos/clasificar b.png')}}" alt="Filtrar Categorias"></button></th>
                                            <th style=""></th>
                                        </tr>
                                    </thead>
                                    <tbody class="fondo_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    {{-- <div class="col-sm-1 col-2">
                        <button id="collapse2" style="border-radius:0px 25px 25px 0px;" title="mostrar" data-toggle="collapse" href="#lista_card" class="btn btn-outline-secondary bg-dark border-left-0 d-none"><img class="imag-icon" src="{{asset('iconos/derecha.png')}}"></button>
                    </div> --}}
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="grupoSubusuarioModal" tabindex="-1" aria-labelledby="grupoSubusuarioModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" style="color: black;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-10 text-center mt-2">
                                <b id="titulo_modal_grupos" class="subrayado">NUEVO GRUPO</b>
                            </div>
                            <div class="col-2"><button id="guardar_grupo" class="btn botones_tabla" title="Guardar"><img class="imag-icon" src="{{asset('iconos/guardar r.png')}}"></button></div>
                        </div>

                        <div style="background: white; border-radius:10px;" class="pl-3 pr-3">
                            <div class="row">
                                <input type="hidden" id="id_grupo">
                                <div class="col-12">
                                    <label for="nombre_grupo" class="estilos_label_input">Grupo</label>
                                    <input class="form-control" type="text" name="nombre_grupo" id="nombre_grupo">
                                    <div class="invalid-feedback">Nombre es requerido.</div>
                                </div>
                                <br>
                                <div class="col-12">
                                    <label for="descripcion_grupo" class="estilos_label_input">Descripcion</label>
                                    <input class="form-control" type="text" name="descripcion_grupo" id="descripcion_grupo">
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>

    {{-- Modal eliminar grupo --}}
    <div class="modal" id="modal_eliminar_grupo" tabindex="-1" aria-labelledby="eliminarModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-sm">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-11"></div>
                            <input type="hidden" id="id_grupo_eliminar">
                            <button type="button" class="col-1 close float-right" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                              </button>
                        </div>
                        <div class="row">
                            <div class="col-12 m-auto text-center pt-3 pb-3">
                                <b id="titulo_modal_eliminar_grupo" class="titulo_modales_eliminar">¿Quieres eliminar este grupo?</b>
                            </div>
                                <button id="confirmar_eliminacion_grupo" class="col-4 btn btn-danger btn-sm d-block m-auto">Aceptar</button>
                        </div>
                        <br>

                    </div>

                </div>
            </div>
        </div>
    </div>
    {{-- Fin modal eliminar grupo --}}

    {{-- Asignar subusuarios grupo --}}
    <!-- Small modal -->
    <div id="modal_asignar_subusuarios_grupo" class="modal fade bd-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-sm-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" style="color: black;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <input id="input_buscar_lista_subusuarios_asignar" type="text" class="col-10 form-control m-auto" placeholder="Buscar por grupo">
                    </div>
                    <div class="row mt-1">
                        <div class="offset-1 col-1"><input type="checkbox" id="seleccionar_todos_subusuarios_asignar"></div><label class="col-9" for="seleccionar_todos_subusuarios_asignar"> SELECCIONAR TODOS</label>
                    </div>
                    {{-- Todos elementos de los grupos subusuarios --}}
                    <div class="row" id="lista_subusuarios_asignar" style="background: #dedede; margin: 2px; border-radius:5px;">
                    </div>
                    <div class="row">
                        <button class="btn btn-danger m-3" id="asignar_subusuarios_grupos">Asignar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    {{-- Fin subusuarios grupo --}}

    {{-- Filtrar subusuarios por categoria --}}
    <!-- Small modal -->
    <div id="modal_filtrar_subusuarios_categoria" class="modal bd-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-md modal-md-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <input id="input_buscar_categoria" type="text" class="col-10 form-control m-auto" placeholder="Buscar por categoria">
                    </div>
                    <div class="row mt-1">
                        <div class="offset-1 col-1"><input type="checkbox" id="seleccionar_todos_filtros_categoria"></div><label class="col-9" for="seleccionar_todos_filtros_categoria"> SELECCIONAR TODOS</label>
                    </div>
                    {{-- Todos elementos de los grupos subusuarios --}}
                    <div id="categorias" style="background: #dedede; margin: 2px; border-radius:5px;">
                        @foreach ($categorias as $categoria)
                            <div class="row" categoria={{$categoria->Descripcion}}>
                                <div class="offset-2 col-9 offset-1">
                                    <input class="mr-1" type="checkbox" idcategoria="{{$categoria->IdCategoria}}" id="categoria_{{$categoria->IdCategoria}}">
                                    <label for="categoria_{{$categoria->IdCategoria}}">{{$categoria->Descripcion}}</label>
                                </div>
                            </div>
                        @endforeach
                    </div>
                    {{-- <div class="row">
                        <button class="btn btn-danger m-3">Filtrar</button>
                    </div> --}}
                </div>
            </div>
        </div>
    </div>
    {{-- Fin filtrar subusuarios por categoria--}}

    {{-- Modal eliminar subusuario --}}
    <div class="modal" id="modal_eliminar_subusuario" tabindex="-1" aria-labelledby="eliminarModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-sm">
            <div class="modal-content" style="background: #ebebeb;">
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-11"></div>
                            <input type="hidden" id="id_subusuario_eliminar">
                            <button type="button" class="col-1 close float-right" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="row">
                            <div class="col-12 m-auto text-center pt-3 pb-3">
                                <b id="titulo_modal_eliminar_subusuario" class="titulo_modales_eliminar">¿Quieres eliminar este subusuario?</b>
                            </div>
                                <button id="confirmar_eliminacion_subusuario" class="col-4 btn btn-danger btn-sm d-block m-auto">Aceptar</button>
                        </div>
                        <br>

                    </div>

                </div>
            </div>
        </div>
    </div>
    {{-- Fin modal eliminar subusuario --}}

    {{-- Modal mostrar detalles del subusuario --}}
    <div class="modal" id="modal_mostrar_detalles_subusuario" tabindex="-1" aria-labelledby="mostrarDetallesModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <ul class="tree">
                            <li>
                              <details open id="arbol_principal_mostrar_subusuario">
                                <summary><b>Detalles</b></summary>
                                <ul>
                                  <li>
                                    <details>
                                      <summary><b>Vehiculos</b></summary>
                                      <ul id="lista_vehiculos_mostrar"></ul>
                                    </details>
                                  </li>
                                  {{-- <li>
                                    <details>
                                      <summary><b>Puntos de Referencia</b></summary>
                                      <ul id="lista_puntos_mostrar"></ul>
                                    </details>
                                  </li> --}}
                                  {{-- <li>
                                    <details>
                                      <summary><b>Geocercas</b></summary>
                                      <ul  id="lista_geocercas_mostrar"></ul>
                                    </details>
                                  </li> --}}
                                  <li>
                                    <details>
                                      <summary><b>Alertas Asignadas</b></summary>
                                      <ul id="lista_alertas_mostrar"></ul>
                                    </details>
                                  </li>
                                  <li>
                                    <details>
                                      <summary><b>Modulos Asignados</b></summary>
                                      <ul id="lista_modulos_mostrar">
                                      </ul>
                                    </details>
                                  </li>
                                  <li>
                                    <details>
                                      <summary><b>Gupo Puntos</b></summary>
                                      <ul id="lista_grupo_puntos_mostrar">
                                      </ul>
                                    </details>
                                  </li>
                                  <li>
                                    <details>
                                      <summary><b>Grupo Geocercas</b></summary>
                                      <ul id="lista_grupo_geocercas_mostrar">
                                      </ul>
                                    </details>
                                  </li>
                                </ul>
                              </details>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    {{-- Fin modal mostrar detalles del subusuario --}}


    {{-- modal crear subusuario --}}
    <div class="modal" id="modal-crear-subusuarios" tabindex="-1" aria-labelledby="modal-crear-subusuarios" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" style="color: black;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="id_subusuario_editar">
                    <div class="tab-content">
                        <div id="ventana_detalles_subusuarios" class="tab-pane fade show active" role="tabpanel" aria-selected="true" aria-labelledby="ventana_detalles_subusuarios">
                            <div class="row">
                                <b class="col-sm-10 col-10 mt-2" style="text-align:center;">DATOS DEL SUBUSUARIO</b>
                                <a class="btn nav-link botones_tabla" id="boton_abrir_ventana_recursos_subusuario" title="Siguiente" onclick="abrir_ventana_recursos();"><img class="imag-icon" src="{{asset('iconos/flecha derecha.png')}}" alt="siguiente"></a>
                            </div>
                            <div class="px-3">
                                <div class="row">
                                    <div class="col-12">
                                        <label for="nombrecompleto_subusuario" class="estilos_label_input">NOMBRE COMPLETO</label><input id="nombrecompleto_subusuario" maxlength="200" class="form-control" type="text"><div class="invalid-feedback">Nombre requerido.</div>
                                    </div>
                                    <div class="col-12">
                                        <label for="nombre_subusuario" class="estilos_label_input">SUBUSUARIO</label><input id="nombre_subusuario" maxlength="50"  class="form-control" type="text"><div class="invalid-feedback">Subusuario requerido.</div>
                                    </div>
                                    <div class="col-12">
                                        <label for="clave_subusuario" class="estilos_label_input">CLAVE</label><input id="clave_subusuario" maxlength="50"  class="form-control" type="text"><div class="invalid-feedback">Clave requerida.</div>
                                    </div>
                                    {{-- <label class="col-md-12 estilos_label_input" for="clave_subusuario">Clave</label>
                                    <div class="input-group input-group-md col-md-12">
                                        <input id="clave_subusuario" type="password" class="form-control">
                                        <div class="invalid-feedback">Clave requerida.</div>
                                        <span class="input-group-append">
                                        <button id="ver_password" title="Mostrar" type="button" style="vertical-align:inherit;" class="btn btn-danger btn-flat"><i class="fa fa-eye"></i></button>
                                        </span>
                                    </div> --}}

                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <label class="estilos_label_input">CON CADUCIDAD</label>
                                    </div>
                                    <div class="col-4 col-sm-3">
                                        <select id="agregar_fecha_caducidad" class="form-control">
                                            <option value="1">Si</option>
                                            <option value="0">No</option>
                                        </select>
                                    </div>
                                    <div class="col-sm-9 col-8">
                                        <input id="validohasta_subusuario" class="form-control" type="date">
                                    </div>
                                    <div class="col-12">
                                        <label for="categoria_subusuario" class="estilos_label_input">CATEGORIA</label>
                                        <select id="categoria_subusuario" class="form-control">
                                            @foreach ($categorias as $categoria)
                                                <option value="{{$categoria->IdCategoria}}">{{$categoria->Descripcion}}</option>
                                            @endforeach
                                        </select>
                                    </div>
                                    <div class="col-12">
                                        <label for="email_subusuario" class="estilos_label_input">EMAIL</label><input id="email_subusuario" maxlength="120" class="form-control" type="email"><div class="invalid-feedback">Ingrese email valido.</div>
                                    </div>
                                </div>
                            </div>

                            <div class="px-3">
                                <div class="row py-1">
                                    <div class="col-12">
                                        <b class="subrayado">Funcionalidades</b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <input type="checkbox" id="ver_kilometrajes"> <label for="ver_kilometrajes" class="estilos_label_input">VER KILOMETRAJES</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="ver_seguimientos"> <label for="ver_seguimientos" class="estilos_label_input">VER SEGUIMIENTOS</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="administrar_alertas"> <label for="administrar_alertas" class="estilos_label_input">ADMINISTRAR ALERTAS</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="adm_puntos_referencia"> <label for="adm_puntos_referencia" class="estilos_label_input">ADM. PUNTOS REFERENCIA</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <input type="checkbox" id="editar_etiqueta"> <label for="editar_etiqueta" class="estilos_label_input">EDITAR ETIQUETA</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="adm_configuracion"> <label for="adm_configuracion" class="estilos_label_input">ADM. CONFIGURACION</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="ver_recorridos"> <label for="ver_recorridos" class="estilos_label_input">VER RECORRIDOS</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="ver_alertas"> <label for="ver_alertas" class="estilos_label_input">VER ALERTAS</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <input type="checkbox" id="ver_dashboard"> <label for="ver_dashboard" class="estilos_label_input">VER DASHBOARD</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="adm_links_seguimiento"> <label for="adm_links_seguimiento" class="estilos_label_input">ADM. LINKS SEGUIMIENTO</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="adm_geocercas"> <label for="adm_geocercas" class="estilos_label_input">ADMINISTRAR GEOCERCAS</label>
                                    </div>
                                    <div class="col-6">
                                        <input type="checkbox" id="envio_comandos"> <label for="envio_comandos" class="estilos_label_input">ENVIO DE COMANDOS</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <input type="checkbox" id="reasignar_despachos"> <label for="reasignar_despachos" class="estilos_label_input">REASIGNAR DESPACHOS</label>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div id="ventana_recursos_subusuario" class="tab-pane fade" role="tabpanel" aria-selected="false" aria-labelledby="ventana_recursos_subusuario">
                            <div class="row">
                                <b class="col-sm-9 col-9 mt-2" style="text-align:center;">DATOS DE ASIGNACIÓN</b><a class="btn nav-link botones_tabla" id="boton_guardar_subusuario" title="Guardar Subusuario"><img class="imag-icon" src="{{asset('iconos/guardar r.png')}}" alt="Guardar"></a>
                                <a class="btn nav-link botones_tabla" id="boton_abrir_ventana_detalles_subusuarios" onclick="abrir_ventana_detalles();" title="Regresar"><img class="imag-icon img-rotate" src="{{asset('iconos/flecha derecha.png')}}" alt="siguiente"></a>
                            </div>
                            <div class="row mb-3">
                                <div class="col-1"></div>
                                <input class="col-8 form-control" type="search" id="busqueda_agrega_recursos_subusuario" placeholder="Buscar por ...">
                                <div class="col-3"></div>
                            </div>
                            <div>
                                <ul class="tree">
                                    <li>
                                      <details open id="arbol_principal_guardar_subusuario">
                                        <summary><b>Detalles</b></summary>
                                        <ul>
                                          <li>
                                            <details>
                                              <summary> <input class="ml-2" id="checkbox_principal_asignar_vehiculo" claseCheck="checkbox_asignar_vehiculo" type="checkbox" onclick="asignar_masivo(event);"> <b>Vehiculos</b> </summary>
                                              <ul id="lista_vehiculos_agregar"></ul>
                                            </details>
                                          </li>
                                          {{-- <li>
                                            <details>
                                              <summary> <input class="ml-2" id="checkbox_principal_asignar_punto" claseCheck="checkbox_asignar_punto" type="checkbox" onclick="asignar_masivo(event);">  <b>Puntos de Referencia</b> </summary>
                                              <ul id="lista_puntos_agregar"></ul>
                                            </details>
                                          </li>
                                          <li>
                                            <details>
                                              <summary><input class="ml-2" id="checkbox_principal_asignar_geocerca" claseCheck="checkbox_asignar_geocerca" type="checkbox" onclick="asignar_masivo(event);"> <b>Geocercas</b> </summary>
                                              <ul  id="lista_geocercas_agregar"></ul>
                                            </details>
                                          </li> --}}
                                          <li>
                                            <details>
                                              <summary><input class="ml-2" id="checkbox_principal_asignar_alerta" claseCheck="checkbox_asignar_alerta" type="checkbox" onclick="asignar_masivo(event);"> <b>Alertas Asignadas</b> </summary>
                                              <ul id="lista_alertas_agregar"></ul>
                                            </details>
                                          </li>
                                          <li>
                                            <details>
                                              <summary><input class="ml-2" id="checkbox_principal_asignar_modulo" claseCheck="checkbox_asignar_modulo" type="checkbox" onclick="asignar_masivo(event);"> <b>Modulos Asignados</b> </summary>
                                              <ul id="lista_modulos_agregar"></ul>
                                            </details>
                                          </li>
                                          <li>
                                            <details>
                                              <summary><input class="ml-2" id="checkbox_principal_asignar_grupo_puntos" claseCheck="checkbox_asignar_grupo_puntos" type="checkbox" onclick="asignar_masivo(event);"> <b>Gupo Puntos</b> </summary>
                                              <ul id="lista_grupo_puntos_agregar"></ul>
                                            </details>
                                          </li>
                                          <li>
                                            <details>
                                              <summary><input class="ml-2" id="checkbox_principal_asignar_grupo_geocercas" claseCheck="checkbox_asignar_grupo_geocercas" type="checkbox" onclick="asignar_masivo(event);"> <b>Grupo Geocercas</b> </summary>
                                              <ul id="lista_grupo_geocercas_agregar"></ul>
                                            </details>
                                          </li>
                                        </ul>
                                      </details>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    {{-- fin modal crear subusuario --}}
@endsection


@section('js')
    <script src="{{asset('js/script.js')}}"></script>
@endsection
