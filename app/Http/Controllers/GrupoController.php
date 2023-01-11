<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Validator;
use Exception;

class GrupoController extends Controller
{
    public function consultarKey($key)
    {
        try
        {
            $datos_usuario = DB::select('exec spUsuarioConsultarKey ?', [$key]);
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
        if($this->consultarKey($request->o) == NULL)
        {
            return response()->json(['data' => []]);
        }

        $id_usuario = $this->consultarKey($request->o)->IdUsuario;

        $grupos = DB::select('spGruposSubUsuariosConsultar ?,?', [$id_usuario, NULL]);
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
        $id_usuario = $this->consultarKey($request->o)->IdUsuario;
        $nombre_usuario = $this->consultarKey($request->o)->Usuario;
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
        $repetido = DB::select('select IdGrupoSubUsuario, NombreGrupoSubUsuario from GrupoSubUsuario where IdUsuario=? and NombreGrupoSubUsuario=?', [$id_usuario, $grupo]);
        if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Grupo ya existe']]);


        DB::insert('exec spGruposSubusuariosIngresar ?,?,?,?',[$grupo, $descripcion, $id_usuario, $nombre_usuario]);

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
        $id_usuario = $this->consultarKey($request->o)->IdUsuario;
        $grupos = DB::select('spGruposSubUsuariosConsultar ?,?', [$id_usuario, $id]);

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
        $id_usuario = $this->consultarKey($request->o)->IdUsuario;
        $nombre_usuario = $this->consultarKey($request->o)->Usuario;
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
        $repetido = DB::select('select IdGrupoSubUsuario, NombreGrupoSubUsuario from GrupoSubUsuario where IdUsuario=? and NombreGrupoSubUsuario=?', [$id_usuario, $grupo]);
        if( sizeof($repetido) > 0 ) return response()->json(['sms' => ['Grupo ya existe']]);

        DB::update('exec spGruposSubusuariosActualizar ?,?,?,?,?,?',[$id, $grupo,$descripcion, 'A', $id_usuario, $nombre_usuario]);

        return response()->json(['sms' => 'ok']);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        DB::update('exec spGruposSubUsuariosEliminar ?', [$id]);
        return response()->json(['sms' => 'ok']);
    }
}
