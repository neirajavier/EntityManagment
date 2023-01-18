<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Validator;
use Exception;

class GrupoController extends Controller
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
            return $datos_usuario[0];
        }
        catch(Exception $e)
        {
            return NULL;
        }

    }
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Request $request)
    {
        if($this->consultarKey($request->o,$request->cp) == NULL)
        {
            return response()->json(['data' => []]);
        }

        $id_usuario = $this->consultarKey($request->o,$request->cp)->IdUsuario;
        $conexion = $this->obtener_cadena($request->cp);

        $grupos = DB::connection($conexion)->select('exec spGruposSubUsuariosConsultar ?,?', [$id_usuario, NULL]);
        $json_final = [];
        $boton_editar = asset('iconos/editar.png');
        $boton_eliminar = asset('iconos/eliminar.png');

        foreach ($grupos as $grupo)
        {
            array_push($json_final, [
                $grupo->IdGrupoSubUsuario,
                ( strlen($grupo->Grupo) > 14 ) ? substr(trim($grupo->Grupo), 0, 11).'...' : trim($grupo->Grupo),
                $grupo->Descripcion,
                '<div class="row">
                    <button class="btn botones_tabla" onClick="editar_grupo('.$grupo->IdGrupoSubUsuario.')" title="Editar Grupo"><img class="imag-icon" src="'.$boton_editar.'" alt=""></button>
                    <button class="btn botones_tabla" onClick="eliminar_grupo('.$grupo->IdGrupoSubUsuario.','."'".$grupo->Grupo."'".')" title="Eliminar Grupo"><img class="imag-icon" src="'.$boton_eliminar.'" alt=""></button>
                </div>',
                $grupo->Grupo
            ]);
        }

        return response()->json(['data' => $json_final]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        $id_usuario = $this->consultarKey($request->o,$request->cp)->IdUsuario;
        $conexion = $this->obtener_cadena($request->cp);

        $nombre_usuario = $this->consultarKey($request->o,$request->cp)->Usuario;
        $grupo = $request->nombre;
        $descripcion = $request->descripcion;

        $validar = Validator::make(
            $request->all(),
            [
                'nombre' => 'bail|required|min:1|max:200',
                'descripcion' => 'bail|max:50',
            ]
        );

        if($validar->fails())
        {
            return response()->json(['sms' => $validar->errors()->all()]);
        }

        //validar que el nombre del grupo no se repita
        $repetido = DB::connection($conexion)->select('select IdGrupoSubUsuario, NombreGrupoSubUsuario from GrupoSubUsuario where IdUsuario=? and NombreGrupoSubUsuario=?', [$id_usuario, $grupo]);
        if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Grupo ya existe']]);


        DB::connection($conexion)->insert('exec spGruposSubusuariosIngresar ?,?,?,?',[$grupo, $descripcion, $id_usuario, $nombre_usuario]);

        return response()->json(['sms' => 'ok']);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit(Request $request, $id)
    {
        $id_usuario = $this->consultarKey($request->o,$request->cp)->IdUsuario;
        $conexion = $this->obtener_cadena($request->cp);
        $grupos = DB::connection($conexion)->select('spGruposSubUsuariosConsultar ?,?', [$id_usuario, $id]);

        return response()->json(['sms' => $grupos]);
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
        $id_usuario = $this->consultarKey($request->o,$request->cp)->IdUsuario;
        $conexion = $this->obtener_cadena($request->cp);
        $nombre_usuario = $this->consultarKey($request->o,$request->cp)->Usuario;
        $grupo = $request->nombre;
        $descripcion = $request->descripcion;

        $validar = Validator::make(
            $request->all(),
            [
                'nombre' => 'bail|required|min:1|max:200',
                'descripcion' => 'bail|max:50',
            ]
        );

        if($validar->fails())
        {
            return response()->json(['sms' => $validar->errors()->all()]);
        }

        //validar que el nombre del grupo no se repita
        $repetido = DB::connection($conexion)->select('select IdGrupoSubUsuario, NombreGrupoSubUsuario from GrupoSubUsuario where IdUsuario=? and not IdGrupoSubUsuario=? and NombreGrupoSubUsuario=?', [$id_usuario, $id, $grupo]);
        if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Grupo ya existe']]);

        DB::connection($conexion)->update('exec spGruposSubusuariosActualizar ?,?,?,?,?,?',[$id, $grupo,$descripcion, 'A', $id_usuario, $nombre_usuario]);

        return response()->json(['sms' => 'ok']);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Request $request, $id)
    {
        $conexion = $this->obtener_cadena($request->cp);
        DB::connection($conexion)->update('exec spGruposSubUsuariosEliminar ?', [$id]);
        return response()->json(['sms' => 'ok']);
    }
}
