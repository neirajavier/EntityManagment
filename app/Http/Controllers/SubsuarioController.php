<?php

namespace App\Http\Controllers;

use Exception;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Validator;
use Illuminate\Support\Facades\Log;
/* use Illuminate\Support\Facades\Cache; */

class SubsuarioController extends Controller
{
    public function obtener_cadena($codigo_pais)
    {
        $codigo_pais = strtolower($codigo_pais);

        if($codigo_pais == 'pe')
        {
            return 'sqlsrvperu';
        }
        elseif ($codigo_pais == 'co')
        {
            return 'sqlsrvcolombia';
        }
        elseif($codigo_pais == 'ch')
        {
            return 'sqlsrvchile';
        }
        elseif($codigo_pais == 'pa')
        {
            return 'sqlsrvpanama';
        }
        elseif($codigo_pais == 'mx')
        {
            return 'sqlsrvmexico';
        }
        elseif($codigo_pais == 'ec')
        {
            return 'sqlsrv';
        }
        else
        {
            return 'sqlsrv';
        }
    }

    public function consultarKey($key,$codigo_pais)
    {
        $conexion = $this->obtener_cadena($codigo_pais);

        try
        {
            $datos_usuario = DB::connection($conexion)->select('exec spUsuarioConsultarKey ?', [$key]);
            return $datos_usuario[0]->IdUsuario;
        }
        catch(Exception $e)
        {
            return NULL;
        }
    }

    public function consultarKeyCampos($key, $codigo_pais)
    {
        $conexion = $this->obtener_cadena($codigo_pais);
        $datos_usuario = DB::connection($conexion)->select('exec spUsuarioConsultarKey ?', [$key]);
        return $datos_usuario[0];
    }
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Request $request)
    {
        //$datos_usuario = DB::select('exec spUsuarioConsultarKey ?', [$request->o]);
        //Cache::put($request->o, $datos_usuario[0]->IdUsuario, 6000);
        $cadena = $this->obtener_cadena($request->cp);
        $categorias = DB::connection($cadena)->select('select IdCategoria, Descripcion from SubUsuarioCategoria');
        return view('home', compact('categorias'));
    }

    public function consultadata(Request $request)
    {
        /* if(Cache::has($request->o))
        {
            $id_usuario = cache($request->o);
        }
        else
        {
            $id_usuario = $this->consultarKey($request->o);
        } */
        $conexion = $this->obtener_cadena($request->cp);
        $id_usuario = $this->consultarKey($request->o, $request->cp);

        /* if($id_usuario == NULL)
        {
            return response()->json(['data' => ['asas']]);
        } */

        $criterios = json_decode($request->criterio);
        $categorias_filtrar = [];
        $busqueda_filtrar = $criterios->nombre;
        $grupo_filtrar = $criterios->grupo;
        $array_asociativo_categorias = [];
        $array_grupo_filtrar = [];

        foreach($criterios->categorias as $categoria)
        {
            array_push($categorias_filtrar, $categoria);
        }

        $subusuarios = DB::connection($conexion)->select('exec spSubUsuarioConsultarV3 ?,?,?', [$id_usuario, NULL, 0]);
        $categorias = DB::connection($conexion)->select('select IdCategoria, Descripcion from SubUsuarioCategoria');
        $grupos_subusuarios = DB::connection($conexion)->select('exec spSubUsuarioGrupoSubUsuariosConsultar ?,?,?', [$grupo_filtrar,$id_usuario,0]);

        foreach($grupos_subusuarios as $grupo)
        {
            array_push($array_grupo_filtrar, $grupo->IdSubUsuario);
        }

        //poner las categorias en array asociativo para luego emparejarlas con sus ids
        for($i=0;$i<count($categorias); $i++)
        {
            $array_asociativo_categorias[$categorias[$i]->IdCategoria] = $categorias[$i]->Descripcion;
        }

        $jsonfinal = [];
        $icono_editar = asset('iconos/editar2.png');
        $icono_eliminar = asset('iconos/eliminar2.png');
        $icono_detalle = asset('iconos/ver mas2.png');

        foreach ($subusuarios as $subusuario)
        {
            if(count($categorias_filtrar)>0)
            {
                if( !in_array($subusuario->IdCategoria, $categorias_filtrar) ) continue;
            }
            if($busqueda_filtrar != '')
            {
                if( !str_contains($subusuario->SubUsuario, $busqueda_filtrar) ) continue;
            }
            if($grupo_filtrar != 0)
            {
                if( !in_array($subusuario->IdSubUsuario, $array_grupo_filtrar) ) continue;
            }
            array_push($jsonfinal,
            [
                '<input class="mycheck check_subusuarios d-block m-auto" type="checkbox">',
                $subusuario->IdSubUsuario,
                $subusuario->NombreCompleto,
                $subusuario->SubUsuario,
                ($subusuario->FechaCaducidad == NULL) ? 'No' : date('d/m/y', strtotime($subusuario->FechaCaducidad)),
                $array_asociativo_categorias[$subusuario->IdCategoria], //aqui se pone la categoria
                '<div style="text-align:left;"><button class="btn btn-outline botones_tabla" onclick="editar_subusuario('.$subusuario->IdSubUsuario.')" title="Editar Subusuario"><img class="imag-icon" src="'.$icono_editar.'"></button>
                <button class="btn btn-outline botones_tabla" onclick="eliminar_subusuario('.$subusuario->IdSubUsuario.','."'".$subusuario->SubUsuario."'".')" title="Eliminar Subusuario"><img class="imag-icon" src="'.$icono_eliminar.'"></button>
                <button class="btn btn-outline botones_tabla" onclick="ver_detalle_subusuario('.$subusuario->IdSubUsuario.')" title="Ver Detalles"><img style="height:20px; width:20px;" src="'.$icono_detalle.'"></button></div>',
            ]
        );
        }

        return response()->json(['data' => $jsonfinal]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create(Request $request)
    {
        $id_usuario = $this->consultarKey($request->o, $request->cp);
        $conexion = $this->obtener_cadena($request->cp);

        if($id_usuario == NULL)
        {
            return [];
        }

        $vehiculos = DB::connection($conexion)->select('exec spActivosUsuarioConsultarV2 ?', [$id_usuario]);

        /* $geocercas = DB::select('exec spGeocercaConsultar ?', [$id_usuario]); */
        $alertas = DB::connection($conexion)->select('exec spD_AlertaConsultar ?,?', [$id_usuario,0]);
        $modulos = DB::connection($conexion)->select('exec spModuloConsultar ?', [$id_usuario]);
        $grupo_puntos = DB::connection($conexion)->select('exec spGrupoPuntoConsultar ?, ?', [$id_usuario, 0]);
        $grupos_vehiculos = DB::connection($conexion)->select('exec spGrupoConsultar ?', [$id_usuario]);
        $grupo_geocercas = DB::connection($conexion)->select('exec spGrupoGeocercaConsultar ?, ?', [$id_usuario, 0]);
        /* $categorias = DB::connection($conexion)->select('exec spLlenarCombo ?', ['CATEGORIASUB']); */
        //$categorias = DB::connection($conexion)->table('SubUsuarioCategoria')->select('IdCategoria', 'Descripcion')->get();

        return response()->json(['alertas' => $alertas, 'modulos' => $modulos, 'grupo_puntos' => $grupo_puntos, 'grupo_geocercas' => $grupo_geocercas, 'vehiculos' => $vehiculos, 'grupo_vehiculos' => $grupos_vehiculos]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        $id_usuario = $this->consultarKey($request->o, $request->cp);
        $conexion = $this->obtener_cadena($request->cp);

        $vehiculos_asignados = json_decode($request->vehiculos_asignados);
        /* $puntos_referencia = json_decode($request->puntos_referencia_asignados);
        $geocercas_asignadas = json_decode($request->geocercas_asignadas); */

        $grupovehiculos_asignados = json_decode($request->grupovehiculos_asignados);
        $alertas_asignadas = json_decode($request->alertas_asignadas);
        $modulos_asignados = json_decode($request->modulos_asignados);
        $grupopuntos_asignados = json_decode($request->grupopuntos_asignados);
        $grupogeocercas_asignadas = json_decode($request->grupogeocercas_asignadas);

        $validar = Validator::make(
            $request->all(),
            [
                'nombrecompleto_subusuario' => 'bail|required|min:1|max:200',
                'nombre_subusuario' => 'bail|required|min:1|max:50',
                'clave_subusuario'  => 'bail|required|min:1|max:50',
                'email_subusuario' => 'bail|required|min:1|max:120|email'
            ]
        );

        if($validar->fails())
        {
            return response()->json(['sms' => $validar->errors()->all()]);
        }

        //verificar que el subusuario no este repetido
        $repetido = DB::connection($conexion)->select('select IdSubUsuario, SubUsuario from SubUsuario where SubUsuario = ?', [$request->nombre_subusuario]);

        if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Subusuario ya existe']]);

        try
        {

            DB::connection($conexion)->beginTransaction();

            $result = DB::connection($conexion)->select('DECLARE @parIdSubUsuario int
                exec spSubUsuarioIngresarV2 ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, @parIdSubUsuario output
                select @parIdSubUsuario as idsubusuario',
                [
                    $id_usuario,
                    $request->nombre_subusuario,
                    $request->nombrecompleto_subusuario,
                    $request->clave_subusuario,
                    ($request->validohasta_subusuario !=  '') ? date('Ymd', strtotime($request->validohasta_subusuario)) : ' ',
                    $request->ver_recorridos,
                    $request->ver_kilometrajes,
                    $request->ver_alertas,
                    $request->administrar_alertas,
                    $request->adm_puntos_referencia,
                    $request->adm_geocercas,
                    $request->ver_seguimientos,
                    $request->ver_dashboard,
                    $request->adm_links_seguimiento,
                    $request->categoria_subusuario,
                    $request->envio_comandos,
                    ($request->email_subusuario != '') ? $request->email_subusuario : ' ',
                    $request->editar_etiqueta,
                    $request->adm_configuracion,
                    $request->reasignar_despachos,
                ]);


            /* foreach($puntos_referencia as $punto)
            {
                DB::insert('exec spPuntoSubUsuarioIngresar ?,?,?', [$punto, $id_usuario, $result[0]->idsubusuario]);
            } */

            foreach($vehiculos_asignados as $vehiculo)
            {
                DB::connection($conexion)->insert('exec spActivoSubUsuarioIngresar ?,?,?', [$vehiculo, $id_usuario, $result[0]->idsubusuario]);
            }

            /* foreach($geocercas_asignadas as $geocerca)
            {
                DB::insert('exec spGeocercaSubUsuarioIngresar ?,?,?', [$geocerca, $id_usuario, $result[0]->idsubusuario]);
            } */

            foreach($alertas_asignadas as $alerta)
            {
                DB::connection($conexion)->insert('exec spD_AlertaSubUsuarioIngresar ?,?,?', [$alerta, $id_usuario, $result[0]->idsubusuario]);
            }

            foreach($modulos_asignados as $modulo)
            {
                DB::connection($conexion)->insert('exec spModulosSubUsuarioIngresar ?,?,?', [$modulo, $id_usuario, $result[0]->idsubusuario]);
            }

            foreach($grupovehiculos_asignados as $grupovehiculo)
            {
                DB::connection($conexion)->insert('exec spGrupoSubUsuariosIngresarV2 ?,?,?,?', [$id_usuario, $result[0]->idsubusuario, $grupovehiculo,$id_usuario]);
            }

            foreach($grupopuntos_asignados as $grupopunto)
            {
                DB::connection($conexion)->insert('exec spGrupoPuntoSubUsuarioIngresar ?,?,?', [$grupopunto, $id_usuario, $result[0]->idsubusuario]); //parIdGrupoPunto, parIDUsuario, parIdSUBUSUARIO
            }

            foreach($grupogeocercas_asignadas as $grupogeocerca)
            {
                DB::connection($conexion)->insert('exec spGrupoGeocercaSubUsuarioIngresar ?,?,?', [$grupogeocerca, $id_usuario, $result[0]->idsubusuario]);
            }

            DB::connection($conexion)->commit();

            return response()->json(['sms' => 'ok']);

        }
        catch(Exception $e)
        {
            DB::connection($conexion)->rollBack();
            Log::critical($e->getMessage(), ['conexion' => $conexion, 'code' => $e->getCode(), 'trace' => $e->getTrace()]);
            return response()->json(['sms' => $e]);
        }

    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function show(Request $request, $id)
    {
        $id_usuario = $this->consultarKey($request->o,$request->cp);
        $conexion = $this->obtener_cadena($request->cp);
        $vehiculos = DB::connection($conexion)->select('exec spActivoSubUsuarioConsultar ?,?', [$id_usuario, $id]);
        /* $puntos = DB::select('exec spPuntoSubUsuarioConsultar ?,?', [$id_usuario, $id]); */
        $modulos = DB::connection($conexion)->select('exec spModuloSubusuarioConsultar ?,?', [$id_usuario, $id]);
        /* $geocercas = DB::select('exec spGeocercaSubusuarioConsultar ?,?', [$id_usuario, $id]); */
        /* $grupo_vehiculos = DB::connection($conexion)->select('exec spGrupoSubUsuariosConsultarV2 ?,?,?', [$id_usuario, $id,0]); */
        $grupo_vehiculos = DB::connection($conexion)->table('SubUsuarioGrupo')->join('Grupo', 'SubUsuarioGrupo.idGrupo', '=', 'Grupo.IdGrupo')->select('Grupo.Grupo')->where('SubUsuarioGrupo.idUsuario',$id_usuario)->where('SubUsuarioGrupo.IdSubUsuario',$id)->get();
        $alertas = DB::connection($conexion)->select('exec spD_AlertaSubUsuarioConsultar ?,?', [$id_usuario, $id]);
        $grupo_puntos = DB::connection($conexion)->select('exec spGrupoPuntoSubusuarioConsultar ?,?,?', [0, $id_usuario, $id]);
        $grupo_geocercas = DB::connection($conexion)->select('exec spGrupoGeocercaSubusuarioConsultar ?,?', [$id_usuario, $id]);

        return response()->json(['vehiculos' => $vehiculos, 'modulos' => $modulos, 'alertas' => $alertas, 'grupo_geocercas' => $grupo_geocercas, 'grupo_puntos' => $grupo_puntos, 'grupo_vehiculos' => $grupo_vehiculos]);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit(Request $request, $id)
    {
        $id_usuario = $this->consultarKey($request->o,$request->cp);
        $conexion = $this->obtener_cadena($request->cp);
        $vehiculos_check = [];
        /* $puntos_check = [];
        $geocercas_check = []; */
        $modulos_check = [];
        $alertas_check = [];
        $grupovehiculos_check = [];
        $grupopuntos_check = [];
        $grupogeocercas_check = [];

        //todos los recursos del usuario
        $vehiculos = DB::connection($conexion)->select('exec spActivosUsuarioConsultarV2 ?', [$id_usuario]);
        /* $puntos = DB::select('exec spPuntoConsultar ?, ?', [0, $id_usuario]);
        $geocercas = DB::select('exec spGeocercaConsultar ?', [$id_usuario]); */

        /* $grupo_vehiculos = DB::connection($conexion)->select('exec spGrupoConsultar ?', [$id_usuario]); */
        $alertas = DB::connection($conexion)->select('exec spD_AlertaConsultar ?,?', [$id_usuario,0]);
        $modulos = DB::connection($conexion)->select('exec spModuloConsultar ?', [$id_usuario]);
        $grupo_puntos = DB::connection($conexion)->select('exec spGrupoPuntoConsultar ?, ?', [$id_usuario, 0]);
        $grupo_vehiculos = DB::connection($conexion)->select('exec spGrupoConsultar ?', [$id_usuario]);
        $grupo_geocercas = DB::connection($conexion)->select('exec spGrupoGeocercaConsultar ?, ?', [$id_usuario, 0]);

        //los recursos que tiene el subusuario, se consulta para saber cuales estan checkeados
        $vehiculos_pertenecen = DB::connection($conexion)->select('exec spActivoSubUsuarioConsultar ?,?', [$id_usuario, $id]);
        /* $puntos_pertenecen = DB::select('exec spPuntoSubUsuarioConsultar ?,?', [$id_usuario, $id]);
        $geocercas_pertenecen = DB::select('exec spGeocercaSubusuarioConsultar ?,?', [$id_usuario, $id]); */
        $modulos_pertenecen = DB::connection($conexion)->select('exec spModuloSubusuarioConsultar ?,?', [$id_usuario, $id]);
        $alertas_pertenecen = DB::connection($conexion)->select('exec spD_AlertaSubUsuarioConsultar ?,?', [$id_usuario, $id]);
        $grupo_puntos_pertenecen = DB::connection($conexion)->select('exec spGrupoPuntoSubusuarioConsultar ?,?,?', [0, $id_usuario, $id]);
        $grupo_vehiculos_pertenecen = DB::connection($conexion)->table('SubUsuarioGrupo')->join('Grupo', 'SubUsuarioGrupo.idGrupo', '=', 'Grupo.IdGrupo')->select('Grupo.Grupo', 'Grupo.IdGrupo as IdGrupo')->where('SubUsuarioGrupo.idUsuario',$id_usuario)->where('SubUsuarioGrupo.IdSubUsuario',$id)->get();
        $grupo_geocercas_pertenecen = DB::connection($conexion)->select('exec spGrupoGeocercaSubusuarioConsultar ?,?', [$id_usuario, $id]);

        foreach($vehiculos_pertenecen as $vehiculo)
        {
            array_push($vehiculos_check, $vehiculo->IdActivo);
        }

        /* foreach($puntos_pertenecen as $punto)
        {
            array_push($puntos_check, $punto->IdPunto);
        } */

        /* foreach($geocercas_pertenecen as $geocerca)
        {
            array_push($geocercas_check, $geocerca->IdGeocerca);
        } */

        foreach($modulos_pertenecen as $modulo)
        {
            array_push($modulos_check, $modulo->idModulo);
        }

        foreach($alertas_pertenecen as $alerta)
        {
            array_push($alertas_check, $alerta->IdAlerta);
        }

        foreach($grupo_vehiculos_pertenecen as $grupovehiculo)
        {
            array_push($grupovehiculos_check, $grupovehiculo->IdGrupo);
        }

        foreach($grupo_puntos_pertenecen as $grupopunto)
        {
            array_push($grupopuntos_check, $grupopunto->IdGrupoPunto);
        }

        foreach($grupo_geocercas_pertenecen as $grupogeocerca)
        {
            array_push($grupogeocercas_check, $grupogeocerca->IdGrupoGeocerca);
        }

        $subusuario = DB::connection($conexion)->select('exec spSubUsuarioDatosConsultar ?,?', [$id_usuario, $id]);
        //$categorias = DB::table('SubUsuarioCategoria')->select('IdCategoria', 'Descripcion')->get();

        return response()->json(['sms' => $subusuario, 'vehiculos' => $vehiculos, 'modulos' => $modulos, 'alertas' => $alertas, 'grupo_geocercas' => $grupo_geocercas, 'grupo_puntos' => $grupo_puntos, 'grupo_vehiculos' => $grupo_vehiculos , 'vehiculos_pertenecen' => $vehiculos_check, 'modulos_pertenecen' => $modulos_check, 'alertas_pertenecen' => $alertas_check, 'grupo_puntos_pertenecen' => $grupopuntos_check, 'grupo_geocercas_pertenecen' => $grupogeocercas_check, 'grupo_vehiculos_pertenecen' => $grupovehiculos_check]);
    }

    public function revisar_grupos_subusuarios(Request $request)
    {
        $id_usuario = $this->consultarKey($request->o,$request->cp);
        $conexion = $this->obtener_cadena($request->cp);

        $id_subusuarios = json_decode($request->id_subusuarios);
        $array_temp = [];
        $array_total_grupos = []; //todos los ids de los grupos para verificar cuales son los checkeados
        $grupos_subusuarios = []; //se forma un arreglo multidimensional con
        $grupos_checkear = []; //enviar los grupos que se marcaran como checkeados
        $count = 0;
        //$grupos_subusuarios_ejemplo = [1,[1,2]];
        //$id_subusuarios_ejemplo = [1,2,3,4];

        $grupos = DB::connection($conexion)->select('exec spGruposSubUsuariosConsultar ?,?', [$id_usuario, NULL]);

        //recorrer id de subusuarios para obtener grupos a los que pertenecen

        for ($i=0; $i < count($id_subusuarios); $i++)
        {
            $grupos_pertenecientes = DB::connection($conexion)->select('exec spSubUsuarioGrupoSubUsuariosConsultar ?,?,?', [ 0, $id_usuario, $id_subusuarios[$i] ] );

            //si tiene 1 grupo o mas se añaden en arreglos
            if(count($grupos_pertenecientes)>0)
            {
                foreach($grupos_pertenecientes as $grupo)
                {
                    array_push($array_temp, $grupo->IdGrupoSubUsuario);
                    array_push($array_total_grupos, $grupo->IdGrupoSubUsuario);
                    $count++;
                    if($count == count($grupos_pertenecientes))
                    {
                        array_push($grupos_subusuarios, $array_temp);
                        $array_temp = [];
                        $count = 0;
                    }
                }
            }
            else
            {
                array_push($grupos_subusuarios, [0]);
            }

        }

        //$array_total_grupos = array_unique($array_total_grupos);

        /* $array_total_grupos = [1,3];

        $grupos_subusuarios = [[0,3], [3]]; */

        //cuando son varios ids de subusuarios y hay que ver los grupos de interseccion para hacerles checks
        for($i=0;$i<count($array_total_grupos);$i++)
        {
            for($j=0;$j<count($grupos_subusuarios);$j++)
            {
                if( !in_array($array_total_grupos[$i], $grupos_subusuarios[$j]) )
                {
                    break;
                }

                if($j+1 == count($grupos_subusuarios))
                {
                    array_push($grupos_checkear, $array_total_grupos[$i]);
                }
            }
        }

        return response()->json(['idsubusuarios' => $id_subusuarios, 'checks' => $grupos_checkear, 'grupos' => $grupos]);
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, $id)
    {
        $id_usuario = $this->consultarKey($request->o, $request->cp);
        $conexion = $this->obtener_cadena($request->cp);

        $validar = Validator::make(
            $request->all(),
            [
                'nombrecompleto_subusuario' => 'bail|required|min:1|max:200',
                'nombre_subusuario' => 'bail|required|min:1|max:50',
                'clave_subusuario'  => 'bail|required|min:1|max:50',
                'email_subusuario' => 'bail|required|min:1|max:120|email'
            ]
        );

        if($validar->fails())
        {
            return response()->json(['sms' => $validar->errors()->all()]);
        }

        //verificar que el subusuario no este repetido
        $repetido = DB::connection($conexion)->select('select IdSubUsuario, SubUsuario from SubUsuario where not IdSubUsuario=? and SubUsuario = ?', [$id, $request->nombre_subusuario]);

        if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Subusuario ya existe']]);

        //IDS recursos checkeados anteriormente
        $vehiculos_anteriores = json_decode($request->vehiculos_anteriores);
        /* $puntos_referencia_anteriores = json_decode($request->puntos_referencia_anteriores);
        $geocercas_anteriores = json_decode($request->geocercas_anteriores); */
        $alertas_anteriores = json_decode($request->alertas_anteriores);
        $modulos_anteriores = json_decode($request->modulos_anteriores);
        $grupopuntos_anteriores = json_decode($request->grupopuntos_anteriores);
        $grupovehiculos_anteriores = json_decode($request->grupovehiculos_anteriores);
        $grupogeocercas_anteriores = json_decode($request->grupogeocercas_anteriores);
        //Fin IDS recursos checkeados anteriormente

        //IDS recursos para asignar
        $vehiculos_asignados = json_decode($request->vehiculos_asignados);
        /* $puntos_referencia_asignados = json_decode($request->puntos_referencia_asignados);
        $geocercas_asignadas = json_decode($request->geocercas_asignadas); */
        $alertas_asignadas = json_decode($request->alertas_asignadas);
        $modulos_asignados = json_decode($request->modulos_asignados);
        $grupopuntos_asignados = json_decode($request->grupopuntos_asignados);
        $grupovehiculos_asignados = json_decode($request->grupovehiculos_asignados);
        $grupogeocercas_asignadas = json_decode($request->grupogeocercas_asignadas);
        //fin IDS recursos para asignar

        //IDS arreglos de ids para guardar los recursos que se van a asignar
        $id_vehiculos_asignados = [];
        /* id_puntos_asignados = [];
        $id_geocercas_asignadas = []; */
        $id_alertas_asignadas = [];
        $id_modulos_asignados = [];
        $id_grupopuntos_asignados = [];
        $id_grupovehiculos_asignados = [];
        $id_grupogeocercas_asignadas = [];
        //Fin IDS arreglos de ids para guardar los recursos que se van a asignar

        //Arreglos para eliminar los recursos que estaban marcados y se desmarcaron
        $id_vehiculos_para_desmarcar = [];
        /* $id_puntos_para_desmarcar = [];
        $id_geocercas_para_desmarcar = []; */
        $id_alertas_para_desmarcar = [];
        $id_modulos_para_desmarcar = [];
        $id_grupopuntos_para_desmarcar = [];
        $id_grupovehiculos_para_desmarcar = [];
        $id_grupogeocercas_para_desmarcar = [];
        //Fin arreglos para eliminar los recursos que estaban marcados y se desmarcaron

        //asignar recursos checkeados
        // * Condicion de verificacion para no volverlo a asignar si ya estaba previamente marcado
        foreach($vehiculos_asignados as $vehiculo)
        {
            if( !in_array( $vehiculo, $vehiculos_anteriores ) ) array_push($id_vehiculos_asignados, $vehiculo);
        }

        /* foreach($puntos_referencia_asignados as $punto)
        {
            if( !in_array( $punto, $puntos_referencia_anteriores ) ) array_push($id_puntos_asignados, $punto);
        } */

        /* foreach($geocercas_asignadas as $geocerca)
        {
            if( !in_array( $geocerca, $geocercas_anteriores ) ) array_push($id_geocercas_asignadas, $geocerca);
        } */

        foreach($modulos_asignados as $modulo)
        {
            if( !in_array( $modulo, $modulos_anteriores ) ) array_push($id_modulos_asignados, $modulo);
        }

        foreach($alertas_asignadas as $alerta)
        {
            if( !in_array( $alerta, $alertas_anteriores ) ) array_push($id_alertas_asignadas, $alerta);
        }

        foreach($grupopuntos_asignados as $grupopunto)
        {
            if( !in_array( $grupopunto, $grupopuntos_anteriores ) ) array_push($id_grupopuntos_asignados, $grupopunto);
        }

        foreach($grupovehiculos_asignados as $grupovehiculo)
        {
            if( !in_array( $grupovehiculo, $grupovehiculos_anteriores ) ) array_push($id_grupovehiculos_asignados, $grupovehiculo);
        }

        foreach($grupogeocercas_asignadas as $grupogeocerca)
        {
            if( !in_array( $grupogeocerca, $grupogeocercas_anteriores ) ) array_push($id_grupogeocercas_asignadas, $grupogeocerca);
        }
        //fin asignar recursos checkeados



        //Verificar los checks desmarcados para eliminar la asignacion del recurso en cada subusuario
        foreach($vehiculos_anteriores as $vehiculo)
        {
            if( !in_array($vehiculo, $vehiculos_asignados)) array_push($id_vehiculos_para_desmarcar, $vehiculo);
        }

        /* foreach($puntos_referencia_anteriores as $punto)
        {
            if( !in_array($punto, $puntos_referencia_asignados)) array_push($id_puntos_para_desmarcar, $punto);
        } */

        /* foreach($geocercas_anteriores as $geocerca)
        {
            if( !in_array($geocerca, $geocercas_asignadas)) array_push($id_geocercas_para_desmarcar, $geocerca);
        } */

        foreach($alertas_anteriores as $alerta)
        {
            if( !in_array($alerta, $alertas_asignadas)) array_push($id_alertas_para_desmarcar, $alerta);
        }

        foreach($modulos_anteriores as $modulo)
        {
            if( !in_array($modulo, $modulos_asignados)) array_push($id_modulos_para_desmarcar, $modulo);
        }

        foreach($grupopuntos_anteriores as $grupopunto)
        {
            if( !in_array($grupopunto, $grupopuntos_asignados)) array_push($id_grupopuntos_para_desmarcar, $grupopunto);
        }

        foreach($grupovehiculos_anteriores as $grupovehiculo)
        {
            if( !in_array($grupovehiculo, $grupovehiculos_asignados)) array_push($id_grupovehiculos_para_desmarcar, $grupovehiculo);
        }

        foreach($grupogeocercas_anteriores as $grupogeocerca)
        {
            if( !in_array($grupogeocerca, $grupogeocercas_asignadas)) array_push($id_grupogeocercas_para_desmarcar, $grupogeocerca);
        }
        //Fin verificar los checks desmarcados para eliminar la asignacion del recurso en cada subusuario

        DB::connection($conexion)->beginTransaction();

        try
        {
            DB::connection($conexion)->update('exec spSubUsuarioActualizar ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?',
            [
                $id_usuario,
                $id,
                $request->nombre_subusuario,
                $request->nombrecompleto_subusuario,
                $request->clave_subusuario,
                ($request->validohasta_subusuario !=  '') ? date('Ymd', strtotime($request->validohasta_subusuario)) : ' ',
                $request->ver_recorridos,
                $request->ver_kilometrajes,
                $request->ver_alertas,
                $request->administrar_alertas,
                $request->adm_puntos_referencia,
                $request->adm_geocercas,
                $request->ver_seguimientos,
                $request->ver_dashboard,
                $request->adm_links_seguimiento,
                $request->categoria_subusuario,
                ($request->envio_comandos == "true") ? 1 : 0,
                ($request->email_subusuario != '') ? $request->email_subusuario : ' ',
                $request->editar_etiqueta,
                $request->adm_configuracion,
                $request->reasignar_despachos
            ]);

            //Asignar recursos
            //*Aqui se agregan solo los nuevos recursos marcados
            foreach($id_vehiculos_asignados as $vehiculo)
            {
                DB::connection($conexion)->insert('exec spActivoSubUsuarioIngresar ?,?,?', [$vehiculo, $id_usuario, $id]);
            }

            /* foreach($id_puntos_asignados as $punto)
            {
                DB::insert('exec spPuntoSubUsuarioIngresar ?,?,?', [$punto, $id_usuario, $id]);
            } */

            /* foreach($id_geocercas_asignadas as $geocerca)
            {
                DB::insert('exec spGeocercaSubUsuarioIngresar ?,?,?', [$geocerca, $id_usuario, $id]);
            } */

            foreach($id_modulos_asignados as $modulo)
            {
                DB::connection($conexion)->insert('exec spModulosSubUsuarioIngresar ?,?,?', [$modulo, $id_usuario, $id]);
            }

            foreach($id_alertas_asignadas as $alerta)
            {
                DB::connection($conexion)->insert('exec spD_AlertaSubUsuarioIngresar ?,?,?', [$alerta, $id_usuario, $id]);
            }

            foreach($id_grupopuntos_asignados as $grupopunto)
            {
                DB::connection($conexion)->insert('exec spGrupoPuntoSubUsuarioIngresar ?,?,?', [$grupopunto, $id_usuario, $id]);
            }

            foreach($id_grupovehiculos_asignados as $grupovehiculo)
            {
                DB::connection($conexion)->insert('exec spGrupoSubUsuariosIngresarV2 ?,?,?,?', [$id_usuario, $id, $grupovehiculo, $id_usuario]);
            }

            foreach($id_grupogeocercas_asignadas as $grupogeocerca)
            {
                DB::connection($conexion)->insert('exec spGrupoGeocercaSubUsuarioIngresar ?,?,?', [$grupogeocerca, $id_usuario, $id]);
            }
            //Fin asignar recursos

            //Eliminar recursos

            //*Aqui se eliminan los recursos que se desmarcaron
            foreach($id_vehiculos_para_desmarcar as $vehiculo)
            {
                DB::connection($conexion)->delete('exec spActivoSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $vehiculo]);
            }

            /* foreach($id_puntos_para_desmarcar as $punto)
            {
                DB::delete('exec spPuntoSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $punto]);
            } */

            /* foreach($id_geocercas_para_desmarcar as $geocerca)
            {
                DB::delete('exec spGeocercaSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $geocerca]);
            } */

            foreach($id_modulos_para_desmarcar as $modulo)
            {
                DB::connection($conexion)->delete('exec spModuloSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $modulo]);
            }

            foreach($id_alertas_para_desmarcar as $alerta)
            {
                DB::connection($conexion)->delete('exec spD_AlertaSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $alerta]);
            }

            foreach($id_grupopuntos_para_desmarcar as $grupopunto)
            {
                DB::connection($conexion)->delete('exec spGrupoPuntoSubUsuarioEliminar ?,?,?', [$grupopunto, $id_usuario, $id]);
            }

            foreach($id_grupovehiculos_para_desmarcar as $grupovehiculo)
            {
                DB::connection($conexion)->delete('exec spGrupoSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $grupovehiculo]);
            }

            foreach($id_grupogeocercas_para_desmarcar as $grupogeocerca)
            {
                DB::connection($conexion)->delete('exec spGrupoGeocercaSubUsuarioEliminar ?,?,?', [$id_usuario, $id, $grupogeocerca]);
            }

            //Fin eliminar recursos

            DB::connection($conexion)->commit();
            return response()->json(['sms' => 'ok']);
        }
        catch(Exception $e)
        {
            DB::connection($conexion)->rollBack();
            Log::critical($e->getMessage(), ['conexion' => $conexion,'code' => $e->getCode(), 'trace' => $e->getTrace()]);
            return response()->json(['sms' => $e]);
        }

        /* return response()->json([
            'puntos_marcar' => $id_puntos_asignados,
            'puntos desmarcar' => $id_puntos_para_desmarcar,
            'geocercas marcar' => $id_geocercas_asignadas,
            'geocercas desmarcar' => $id_geocercas_para_desmarcar,
            'vehiculos marcar' => $id_vehiculos_asignados,
            'vehiculos desmarcar' => $id_vehiculos_para_desmarcar,
            'alertas marcar' => $id_alertas_asignadas,
            'alertas desmarcar' => $id_alertas_para_desmarcar,
            'modulos marcar' => $id_modulos_asignados,
            'modulos desmarcar' => $id_modulos_para_desmarcar,
            'grupopuntos marcar' => $id_grupopuntos_asignados,
            'grupopuntos desmarcar' => $id_grupopuntos_para_desmarcar,
            'grupogeocercas marcar' => $id_grupogeocercas_asignadas,
            'grupogeocercas desmarcar' => $id_grupogeocercas_para_desmarcar,
            'idusuario' => $id_usuario,
            'idsubusuario' => $id
        ]); */

    }

    public function asignar_subusuarios_grupos(Request $request)
    {
        $campos = $this->consultarKeyCampos($request->o, $request->cp);
        $conexion = $this->obtener_cadena($request->cp);

        $id_usuario = $campos->IdUsuario;
        $nombre_usuario = $campos->Usuario;
        $id_subusuarios = json_decode($request->ids_subusuarios);
        $id_grupos_checkear = json_decode($request->ids_grupos);
        $checks_anteriores = json_decode($request->checks_anteriores);
        $checks_totales_marcados = []; // esta variable se usa para comparar y ver cuales checks se deben desmarcar

        DB::connection($conexion)->beginTransaction();

        try
        {
            foreach($id_subusuarios as $idsubusuario)
            {
                foreach($id_grupos_checkear as $idgrupo)
                {
                    array_push($checks_totales_marcados, $idgrupo);
                    if( in_array($idgrupo, $checks_anteriores) ) continue;
                    DB::connection($conexion)->insert('exec spSubUsuarioGrupoSubUsuariosIngresar ?,?,?,?', [$idgrupo, $id_usuario, $idsubusuario, $nombre_usuario]);
                }
            }

            foreach($id_subusuarios as $idsubusuario)
            {
                foreach($checks_anteriores as $idgrupo)
                {
                    if( in_array($idgrupo,$checks_totales_marcados) ) continue;
                    DB::connection($conexion)->update('exec spSubUsuarioGrupoSubUsuariosEliminar ?,?,?', [$idgrupo, $id_usuario, $idsubusuario]);
                }

            }

            DB::connection($conexion)->commit();
            return response()->json(['sms' => 'ok']);
        }
        catch(Exception $e)
        {
            DB::connection($conexion)->rollBack();
            Log::critical($e->getMessage(), ['conexion' => $conexion,'code' => $e->getCode(), 'trace' => $e->getTrace()]);
            return response()->json(['sms' => $e]);
        }

        return response()->json(['sms' => 'ok', 'nombre' => $nombre_usuario]);

        //return response()->json(['idusuario' => $id_usuario, 'id_subusuarios'=> $id_subusuarios, 'id_grupos_chequear' => $id_grupos_checkear, 'checks anteriores' => $checks_anteriores]);
    }

    public function edicion_tabla(Request $request)
    {
        $id_usuario = $this->consultarKey($request->o, $request->cp);
        $conexion = $this->obtener_cadena($request->cp);

        $id_subusuario = $request->id;
        $nombre = $request->nombre;
        $subusuario_enviado = $request->subusuario;
        $campo = $request->campo_actualizar;

        if($campo == 'nombrecompleto')
        {
            $validar = Validator::make(
                $request->all(),
                [
                    'nombre' => 'bail|required|min:1|max:200',
                ]
            );
        }
        elseif($campo == 'subusuario')
        {
            $validar = Validator::make(
                $request->all(),
                [
                    'subusuario' => 'bail|required|min:1|max:50',
                ]
            );

            //verificar que el subusuario no este repetido
            $repetido = DB::connection($conexion)->select('select IdSubUsuario, SubUsuario from SubUsuario where not IdSubUsuario=? and SubUsuario = ?', [$id_subusuario, $subusuario_enviado]);

            if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Subusuario ya existe']]);

        }

        if($validar->fails())
        {
            return response()->json(['sms' => $validar->errors()->all()]);
        }

        try
        {
            DB::connection($conexion)->beginTransaction();

            $subusuario = DB::connection($conexion)->select('exec spSubUsuarioDatosConsultar ?,?', [$id_usuario, $id_subusuario])[0];

            DB::connection($conexion)->update('exec spSubUsuarioActualizar ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?',
                [
                    $id_usuario,
                    $id_subusuario,
                    ($subusuario_enviado != '') ? $subusuario_enviado : $subusuario->SubUsuario,
                    ($nombre != '') ? $nombre : $subusuario->NombreCompleto,
                    $subusuario->Clave,
                    ($subusuario->FechaCaducidad == NULL) ? ' ' :  date('Ymd', strtotime($subusuario->FechaCaducidad)),
                    $subusuario->VerRecorridos,
                    $subusuario->VerKilometraje,
                    $subusuario->VerAlertas,
                    $subusuario->AdmAlertas,
                    $subusuario->AdmPuntosReferencia,
                    $subusuario->AdmGeocercas,
                    $subusuario->VerSeguimiento,
                    $subusuario->verDashBoard,
                    $subusuario->CrearLinksSeguimiento,
                    $subusuario->idCategoria,
                    ($request->envio_comandos == "1") ? 1 : 0,
                    ($subusuario->Email == NULL) ? ' ' : $subusuario->Email,
                    $subusuario->EditarEtiqueta,
                    $subusuario->AdmConfiguracion,
                    $subusuario->ReasignarDespachos
                ]);

                DB::connection($conexion)->commit();
                return response()->json(['sms' => 'ok']);
        }
        catch(Exception $e)
        {
            DB::connection($conexion)->rollBack();
            Log::critical($e->getMessage(), ['conexion' => $conexion, 'code' => $e->getCode(), 'trace' => $e->getTrace()]);
            return response()->json(['sms' => $e]);
        }
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Request $request, $id)
    {
        $id_usuario = $this->consultarKey($request->o, $request->cp);
        $conexion = $this->obtener_cadena($request->cp);
        $total_eliminados = DB::connection($conexion)->update('exec spSubUsuarioEliminar ?, ?', [$id_usuario, $id]);
        return response()->json(['sms' => 'ok']);
    }
}
