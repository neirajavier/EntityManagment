<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminMenus.aspx.vb" Inherits="ArtemisAdmin.AdminMenus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.6.0/dist/leaflet.css" />

    <link href="/Libs/leaflet/css/distance.css" rel="stylesheet"/>
    <link href="/Libs/leaflet/css/L.Control.Geonames.css" rel="stylesheet"/>
    <link href="/Libs/leaflet/js/leaflet-measure.css" rel="stylesheet"/>
    <link href="/Libs/leaflet/css/leaflet.contextmenu.min.css" rel="stylesheet"/>
    <%--<link href="../leaflet/css/leaflet.label.css" rel="stylesheet"/>--%>
    <link href="/Libs/leaflet/css/MarkerCluster.css" rel="stylesheet"/>
    <link href="/Libs/leaflet/css/MarkerCluster.Default.css" rel="stylesheet"/>
    <link href="/Libs/leaflet/css/font-awesome.min.css" rel="stylesheet"/>
    <link href='https://api.mapbox.com/mapbox.js/plugins/leaflet-fullscreen/v1.0.1/leaflet.fullscreen.css' rel='stylesheet'/>

    <link href="https://cdn.datatables.net/1.10.21/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/buttons/1.6.3/css/buttons.bootstrap4.min.css" rel="stylesheet" />

    <link href="/Themes/smoothness/jquery-ui.css" rel="stylesheet" />
    <link href="/Themes/smoothness/theme.css" rel="stylesheet" />

    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet" />
</head>
<body style="background-color: black; font-size:14px; color:black">
    <script src="/Scripts/jquery.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <script src="https://unpkg.com/leaflet@1.6.0/dist/leaflet.js"></script>

    <script src="/Libs/leaflet/js/BaseMarkerMethods.js" type="text/javascript"></script>
    <script src="/Libs/leaflet/js/control/Distance.js" type="text/javascript"></script>
    <script src="/Libs/leaflet/js/L.Control.Geonames.js" type="text/javascript"></script>
    <script src="/Libs/leaflet/js/leaflet.easyPrint.js" type="text/javascript"></script>
    <script src="/Libs/leaflet/js/leaflet.contextmenu.min.js" type="text/javascript"></script>
    <script src="/Libs/leaflet/js/leaflet-measure.js" type="text/javascript"></script>
    <script src="/Libs/leaflet/js/Leaflet.Control.Custom.js" type="text/javascript"></script>

    <script src="https://unpkg.com/pouchdb@^5.2.0/dist/pouchdb.js" type="text/javascript"></script>
    <script src="https://unpkg.com/leaflet.tilelayer.pouchdbcached@latest/L.TileLayer.PouchDBCached.js" type="text/javascript"></script>

    <script src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.21/js/dataTables.bootstrap4.min.js"></script>

    <form id="frmmenu" runat="server">
        <div id="wrapper" class="animate">
            <nav class="navbar header-top fixed-top navbar-expand-lg  navbar-dark hunter">
                <%--<span class="navbar-toggler-icon leftmenutrigger"></span>--%>
                <a class="navbar-brand" href="#" target="fOpciones">
                    <img id="imglogosuperior" runat="server" src="/images/hmn2/logo_hmn2.png" />
                </a>
                <hr />
                <br />
                <span class="badge badge-light small" id="spusuario" runat="server"></span>
                <hr />
                <div class="col-sm-2 h-100 text-light">
                    <hr />
                    <h4>Administración</h4>
                    <br />
                </div>
                <div class="navbar-collapse" id="navbarText">
                    <ul class="navbar-nav ml-md-auto d-md-flex">
                        <%--<li class="nav-item">
                            <a class="nav-link" href="#" target="fOpciones">Vehiculos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" target="fOpciones">Puntos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/Pruebas2.aspx" target="fOpciones">Pruebas</a>
                        </li>--%>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/MantenimientoGrupo.aspx" target="fOpciones">Vehiculos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/MantenimientoConductor.aspx" target="fOpciones">Conductores</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/MantenimientoGeocerca.aspx" target="fOpciones">Geocercas</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/MantenimientoPunto.aspx" target="fOpciones">Puntos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/MantenimientoAlertas.aspx" target="fOpciones">Alertas</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Mantenimiento/MantenimientoPassKey.aspx" target="fOpciones">PassKey</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Logistica/MantenimientoParametrizacion.aspx" target="fOpciones">Parametrizacion</a>
                            <hr />
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Forms/Admin/MenuPrincipal.aspx" target="fOpciones">Menu</a>
                            <hr />
                        </li>
                        <li>
                            <div class="dropdown-divider"></div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-light" href="#" onclick="parent.close();"><b><%=Now() %></b></a>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />
        <asp:HiddenField runat="server" ID="hdregs" />
    </form>
</body>
</html>
