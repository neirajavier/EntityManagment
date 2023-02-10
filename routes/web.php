<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\SubsuarioController;
use App\Http\Controllers\GrupoController;
use App\Http\Controllers\CategoriaController;
/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', [SubsuarioController::class, 'index'])->name('subusuario.index');
Route::get('subusuarios', [SubsuarioController::class, 'index']);

Route::post('grupo', [GrupoController::class, 'store']);
Route::get('grupos/consultar', [GrupoController::class, 'index']);
Route::get('grupos/{id}/editar', [GrupoController::class, 'edit']);
Route::post('grupos/{id}', [GrupoController::class, 'update']);
Route::delete('grupos/{id}', [GrupoController::class, 'destroy']);

Route::post('subusuarios', [SubsuarioController::class, 'store'])->name('subusuario.store');
Route::post('subusuarios/edicion_tabla', [SubsuarioController::class, 'edicion_tabla'])->name('subusuario.edicion_tabla');
Route::get('subusuarios/create', [SubsuarioController::class, 'create'])->name('subusuario.create'); //llenar tablas y combo de categorias
Route::get('subusuarios/{id}/editar', [SubsuarioController::class, 'edit']);
Route::get('subusuarios/{id}/mostrar', [SubsuarioController::class, 'show']);
Route::post('subusuarios/revisar_grupos_subusuarios', [SubsuarioController::class, 'revisar_grupos_subusuarios']);
Route::post('subusuarios/asignar_grupos_subusuarios', [SubsuarioController::class, 'asignar_subusuarios_grupos']); //asignar subusuarios a grupos
Route::post('subusuarios/{id}/actualizar', [SubsuarioController::class, 'update']);
Route::get('subusuarios/consultar', [SubsuarioController::class, 'consultadata']);
Route::delete('subusuarios/{id}', [SubsuarioController::class, 'destroy']);

Route::get('categorias', [CategoriaController::class, 'index']);

//Route::get('version', function(){ phpinfo(); });
