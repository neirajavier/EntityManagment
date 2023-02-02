<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>@yield('title')</title>
    <link rel="shortcut icon" href="{{asset('iconos/favicon.ico')}}">
    <link rel="stylesheet" href="{{asset('css/bootstrap.min.css')}}">
    <script src="{{asset('js/jquery-3.5.1.js')}}"></script>
    <script src="{{asset('js/bootstrap.bundle.min.js')}}"></script>
    <link rel="stylesheet" href="{{asset('css/bootstrap-float-label.min.css')}}"/>
    <link rel="stylesheet" href="{{asset('css/jquery.dataTables.min.css')}}">
    <script src="{{asset('js/jquery.dataTables.min.js')}}"></script>
    <link rel="stylesheet" href="{{asset('css/select.dataTables.min.css')}}">
    <script src="{{asset('js/dataTables.select.min.js')}}"></script>
    <script src="{{asset('js/dataTables.buttons.min.js')}}"></script>
    <link rel="stylesheet" href="{{asset('css/notification.css')}}">
    <script src="{{asset('js/notification.js')}}"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.4.0/css/responsive.dataTables.min.css">
    <script src="https://cdn.datatables.net/responsive/2.3.0/js/dataTables.responsive.min.js"></script>
    <script src="{{asset('js/dataTables.rowReorder.min.js')}}"></script>
    {{-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-toast-plugin/1.3.2/jquery.toast.css"> --}}
    {{-- <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-toast-plugin/1.3.2/jquery.toast.js"></script> --}}

    <meta name="csrf-token" content="{{ csrf_token() }}">
    <link rel="stylesheet" href="{{asset('font-awesome-4.7.0/css/font-awesome.min.css')}}">
    @yield('css')
</head>
<body>

    {{-- <a href="{{route('mensaje.index')}}" class="btn @if(Request::path() == 'mensaje') btn-danger @endif">index</a>
    <a href="{{route('mensaje.create')}}" class="btn @if(Request::path() == 'mensaje/create') btn-danger @endif">create</a> --}}
    {{-- {{Request::path()}} --}}
    @section('content')
    @show
    @yield('js')
</body>
</html>
