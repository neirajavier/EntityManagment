<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MapaGeocerca.aspx.vb" Inherits="ArtemisAdmin.MapaGeocerca" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />

    <link href="../../Libs/leaflet/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Libs/leaflet/css/distance.css" rel="stylesheet" />
    <link href="../../Libs/leaflet/js/leaflet-measure.css" rel="stylesheet" />
    <link href="../../Libs/leaflet/css/leaflet.contextmenu.min.css" rel="stylesheet" />
    <link href="../../Libs/leaflet/css/MarkerCluster.css" rel="stylesheet" />
    <link href="../../Libs/leaflet/css/MarkerCluster.Default.css" rel="stylesheet" />
    <link href="../../Libs/leaflet/css/Control.FullScreen.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.14/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/@geoman-io/leaflet-geoman-free@latest/dist/leaflet-geoman.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-control-geocoder@latest/dist/Control.Geocoder.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.8.0/dist/leaflet.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-draw@0.4.1/dist/leaflet.draw.css" />
    <link href="../../Libs/leaflet/css/leaflet.marker.highlight.css" rel="stylesheet" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mapa Geocerca</title>

    <script language="JavaScript" type="text/javascript"> 
        var map;
        var lmarkers = new Array();
        var lmarkersg = new Array();
        var gcolors = ["#ff0000",
            "blue",
            "gray",
            "violet",
            "#006699",
            "black",
            "green",
            "dark blue",
            "orange",
            "navy",
            "cyan",
            "yellow",
            "red",
            "#C0C0C0",
            "#808080",
            "#FF00FF",
            "#800000",
            "#fbd46d",
            "#ffa36c",
            "#523906",
            "#e3dfc8",
            "#808000",
            "#00FF00",
            "#00FFFF",
            "#008080",
            "#CD5C5C",
            "#F08080",
            "#FA8072",
            "#E9967A",
            "#FFA07A",
            "#800080",
            "#4f8a8b",
            "#838383"];
        var myRenderer, drawnItems;
        var InfoPunto, Ind, Poly, Line, Circle, InfoCentro, Rect;
        var tmpgeoeliminada;
        var drawControl;

        class clsgeocerca {
            constructor(secuencia, idgeocerca, nombre, tipo, parametro1, tipozona, idcolor, numpuntos, accion) {
                this.secuencia = secuencia;
                this.idgeocerca = idgeocerca;
                this.nombre = nombre;
                this.tipo = tipo;
                this.parametro1 = parametro1;
                this.tipozona = tipozona;
                this.idcolor = idcolor;
                this.numpuntos = numpuntos;
                this.accion = accion;
            }

            toRow() {
                return [secuencia, idgeocerca, nombre, tipo, Math.round(parametro1 * 1), tipozona, idcolor, numpuntos, accion];
            }
        }

        function getMenuOpciones() {
            var opciones = '<table style="background-color:white;border=0;bordercolor:tgray;border-collapse:separate;border:solid gray 1px;border-radius:1px;-moz-border-radius:1px;">' +
                '<td style="width:44px; height:44px; vertical-align:middle; text-align:center"><img title="Guardar cambios de geocerca" src="../../images/save.png" onclick="guardargeocercapuntos()" />'
                + '</td></tr></table>';
            return opciones;
        }

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
    </script>
</head>
<body class="container-fluid">
    <script src="../../Scripts/jquery.min.js"></script>
    <script src="../../Scripts/moment.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <script src="../../Libs/leaflet/leaflet-src.js"></script>

    <script src="../../Libs/leaflet/js/BaseMarkerMethods.js" type="text/javascript"></script>
    <script src="../../Libs/leaflet/js/control/Distance.js" type="text/javascript"></script>
    <script src="https://unpkg.com/leaflet-control-geocoder@latest/dist/Control.Geocoder.js"></script>
    <script src="../../Libs/leaflet/js/leaflet.easyPrint.js" type="text/javascript"></script>
    <script src="../../Libs/leaflet/js/leaflet.contextmenu.min.js" type="text/javascript"></script>
    <script src="../../Libs/leaflet/js/leaflet-measure.js" type="text/javascript"></script>
    <script src="../../Libs/leaflet/js/Leaflet.Control.Custom.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/OverlappingMarkerSpiderfier-Leaflet/0.2.6/oms.min.js"></script>
    <script src="../../Libs/leaflet/js/leaflet.marker.highlight-src.js"></script>

    <script src="https://unpkg.com/pouchdb@^5.2.0/dist/pouchdb.js" type="text/javascript"></script>
    <script src="https://unpkg.com/leaflet.tilelayer.pouchdbcached@latest/L.TileLayer.PouchDBCached.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.14/dist/js/bootstrap-select.min.js"></script>
    <script src="https://unpkg.com/@geoman-io/leaflet-geoman-free@latest/dist/leaflet-geoman.min.js"></script>
    <script src="https://unpkg.com/leaflet-draw@0.4.9/dist/leaflet.draw.js"></script>
    <script src="../../Libs/leaflet/js/Control.FullScreen.js"></script>
    <%--<script src="../../Libs/leaflet/js/Leaflet.draw.js"></script>--%>

    <script language="JavaScript" type="text/javascript"> 
        var estado, idgeo, tipo, nombre, parametro, user, subuser, pais, pais2;
        var listmapas2, motor2;
        var tipoGeo, tipoGeo2;
        datag = {
            puntosgeocercas: async function (idgeocerca, tipo, nombre, parametro, pais) {
                let dfr = $.Deferred();
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlgeocercasdetalle').value,
                    data: "{'IdGeocerca':'" + new String(idgeocerca) + "','Tipo':'" + new String(tipo) + "','Nombre':'" + nombre + "','Parametro':'" + new String(parametro) + "','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: false,
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            },
            setpuntosgeocercacC: function (idgeocerca, idusuario, idsubusuario, latitud, longitud, nombre, radio, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlsetgeocercasdetalleC').value,
                    data: "{'IdGeocerca':" + new String(idgeocerca) + ",'Nombre':'" + nombre + "','Radio':'" + new String(radio).replace(",", ".") + "','Latitud':'" + new String(latitud).replace(",", ".") + "','Longitud':'" + new String(longitud).replace(",", ".") + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Accion':'MOD','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: function () {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            },
            setpuntosgeocercacL: function (idgeocerca, idusuario, idsubusuario, puntos, nombre, ancho, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlsetgeocercasdetalleL').value,
                    data: "{'IdGeocerca':" + new String(idgeocerca) + ",'Nombre':'" + nombre + "','Ancho':'" + new String(ancho).replace(",", ".") + "','Puntos':'" + JSON.stringify(puntos) + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Accion':'MOD','Tipo':2,'Clasificacion':'U','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: function () {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            },
            setpuntosgeocercacP: function (idgeocerca, idusuario, idsubusuario, puntos, nombre, ancho, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlsetgeocercasdetalleP').value,
                    data: "{'IdGeocerca':" + new String(idgeocerca) + ",'Nombre':'" + nombre + "','Ancho':'" + new String(ancho).replace(",", ".") + "','Puntos':'" + JSON.stringify(puntos) + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Accion':'MOD','Tipo':1,'Clasificacion':'U','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: function () {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            },
            setgeocercaC: function (idgeocerca, idusuario, idsubusuario, latitud, longitud, nombre, radio, indice, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlsetgeocercasdetalleC').value,
                    data: "{'IdGeocerca':" + new String(idgeocerca) + ",'Nombre':'" + nombre + "','Radio':'" + new String(radio).replace(",", ".") + "','Latitud':'" + new String(latitud).replace(",", ".") + "','Longitud':'" + new String(longitud).replace(",", ".") + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Accion':'ING','Indice':'" + new String(indice) + "','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: function () {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            },
            setgeocercaL: function (idgeocerca, idusuario, idsubusuario, puntos, nombre, ancho, indice, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlsetgeocercasdetalleL').value,
                    data: "{'IdGeocerca':" + new String(idgeocerca) + ",'Nombre':'" + nombre + "','Ancho':'" + new String(ancho).replace(",", ".") + "','Puntos':'" + JSON.stringify(puntos) + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Accion':'ING','Tipo':2,'Clasificacion':'U','Indice':'" + new String(indice) + "','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: function () {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            },
            setgeocercaP: function (idgeocerca, idusuario, idsubusuario, puntos, nombre, ancho, indice, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlsetgeocercasdetalleP').value,
                    data: "{'IdGeocerca':" + new String(idgeocerca) + ",'Nombre':'" + nombre + "','Ancho':'" + new String(ancho).replace(",", ".") + "','Puntos':'" + JSON.stringify(puntos) + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Accion':'ING','Tipo':1,'Clasificacion':'U','Indice':'" + new String(indice) + "','Pais':'" + new String(pais) + "'}",
                    contentType: "application/json; utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: function () {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.textStatus);
                    },
                    success: dfr.resolve
                });
                return dfr.promise();
            }
        }

        function initializeMap(ModoMapa, mapas, motor) {
            try {
                debugger;
                var menucontext = true;
                var ilayersmapa = {};
                var olayersmapa = {};
                //var listamapas = new String(document.getElementById('hdlistamapas').value).split(',');
                //var listamapas = new String('GM,OS,IOS').split(',');
                var listamapas = mapas.split(',');
                tipoGeo = <%=geoTipo %>;
                pais2 = document.getElementById("hdpais2").value;
                var capai, ncapai;
                var ini = 0;

                const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);

                LeafIcon = L.Icon.extend({
                    options: {
                        id: "",
                        alias: "",
                        vid: "",
                        placa: "",
                        ultimoreporte: "",
                        iconSize: new L.Point(14, 14),
                        renderer: myRenderer,
                        tooltipAnchor: new L.Point(14, 14)
                    }
                });
                LeafIconV2 = L.Icon.extend({
                    options: {
                        id: "",
                        alias: "",
                        vid: "",
                        placa: "",
                        ultimoreporte: "",
                        iconSize: new L.Point(25, 25),
                        renderer: myRenderer,
                        tooltipAnchor: new L.Point(25, 25)
                    }
                });

                for (ind = 0; ind <= (listamapas.length - 1); ind++) {
                    try {
                        if (listamapas[ind] != '') {
                            switch (listamapas[ind]) {
                                case 'OS':
                                    ncapai = "Openstreet";

                                    if (ini == 0) {
                                        capai = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                                            {
                                                attribution: '&copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors',
                                                maxZoom: 19
                                            });
                                        ini += 1;
                                    }

                                    osm = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                                        {
                                            attribution: '&copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors',
                                            maxZoom: 19
                                        });

                                    ilayersmapa["OpenStreet"] = osm;

                                    break;
                                case 'GM':
                                    ncapai = "Google maps";

                                    if (ini == 0) {
                                        capai = L.gridLayer.googleMutant({
                                            maxZoom: 18,
                                            type: 'roadmap'
                                        });

                                        var trafficMutant = L.gridLayer.googleMutant({
                                            maxZoom: 18,
                                            type: 'roadmap'
                                        });
                                        //capai.addGoogleLayer('TrafficLayer');

                                        ini += 1;
                                        ilayersmapa["Google maps"] = capai;
                                    }
                                    else {
                                        gm = L.gridLayer.googleMutant({
                                            maxZoom: 18,
                                            type: 'roadmap'
                                        });

                                        var trafficMutant = L.gridLayer.googleMutant({
                                            maxZoom: 18,
                                            type: 'roadmap'
                                        });
                                        //gm.addGoogleLayer('TrafficLayer');

                                        ilayersmapa["Google maps"] = gm;
                                    }

                                    break;
                                case 'HUN':
                                    ncapai = "Hunter";

                                    if (ini == 0) {
                                        var pretiled = L.tileLayer(document.getElementById("hdservidorpe").value + '/mapa/{z}/{x}/{y}.png', { minZoom: 0, maxZoom: 15, attribution: 'Hunter' });
                                        var tiledfly = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase@EPSG:900913@png/{z}/{x}/{y}.png8', {
                                            tms: true,
                                            minZoom: 16,
                                            maxZoom: 19
                                        });
                                        capai = L.layerGroup([pretiled, tiledfly]);

                                        ini += 1;
                                        ilayersmapa["Hunter"] = capai;
                                    }
                                    else {
                                        var pretiled = L.tileLayer(document.getElementById("hdservidorpe").value + '/mapa/{z}/{x}/{y}.png', { minZoom: 0, maxZoom: 15, attribution: 'Hunter' });
                                        var tiledfly = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase@EPSG:900913@png/{z}/{x}/{y}.png8', {
                                            tms: true,
                                            minZoom: 16,
                                            maxZoom: 19
                                        });
                                        hun = L.layerGroup([pretiled, tiledfly]);

                                        ilayersmapa["Hunter"] = hun;
                                    }

                                    break;
                            }
                        }
                    }
                    catch (Error) { console.log(Error); }
                }

                if (ModoMapa == null) {
                    ModoMapa = 'P';
                }

                if (ModoMapa == 'P') {
                    menucontext = true;
                }
                else {
                    menucontext = false;
                }

                Limpiar = true;
                try {
                    LatitudInicial = parseFloat(document.getElementById("txLatitudInicial").value);
                    LongitudInicial = parseFloat(document.getElementById("txLongitudInicial").value);
                }
                catch (Error) {
                    LatitudInicial = -2.1672;
                    LongitudInicial = -79.9172;
                }

                if (!isMobile) {
                    map = new L.Map('dvmapa', {
                        center: new L.LatLng(LatitudInicial, LongitudInicial), zoom: 6,
                        layers: [capai],
                        preferCanvas: true,
                        zoomControl: true
                    });
                } else {
                    map = new L.Map('dvmapa', {
                        center: new L.LatLng(LatitudInicial, LongitudInicial), zoom: 6,
                        layers: [capai],
                        preferCanvas: true,
                        zoomControl: false
                    });
                }

                //if (document.getElementById("hdmotormapa").value == "GM") {
                if (motor == "GM") {
                    hib = L.gridLayer.googleMutant({
                        maxZoom: 18,
                        type: 'hybrid'
                    });

                    sat = L.gridLayer.googleMutant({
                        maxZoom: 18,
                        type: 'satellite'
                    });

                    ilayersmapa["Satelital google"] = sat;
                    ilayersmapa["Hibrido google"] = hib;
                }
                else {
                    hib = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
                        attribution: 'Tiles &copy; Esri &mdash; Esri, DeLorme, NAVTEQ, TomTom, Intermap, iPC, USGS, FAO, NPS, NRCAN, GeoBase, Kadaster NL, Ordnance Survey, Esri Japan, METI, Esri China (Hong Kong), and the GIS User Community'
                    });

                    sat = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
                        attribution: 'Tiles &copy; Esri &mdash; Source: Esri, i-cubed, USDA, USGS, AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community'
                    });

                    ilayersmapa["Satelital"] = sat;
                    ilayersmapa["Hibrido"] = hib;
                }

                rain = L.tileLayer('http://tile.openweathermap.org/map/precipitation_new/{z}/{x}/{y}.png?appid=c71be71d270ce3219d2e251fbd840dc7',
                    {
                        attribution: '&copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors',
                        maxZoom: 18
                    });

                if (document.getElementById("hdpais").value == 'PE1') {
                    //CargarCapasPE();

                    //if (document.getElementById("hdmotormapa").value == "GM") {
                    if (motor == "GM") {
                        var roadMutant = L.gridLayer.googleMutant({
                            maxZoom: 18,
                            type: 'roadmap'
                        });

                        var trafficMutant = L.gridLayer.googleMutant({
                            maxZoom: 18,
                            type: 'roadmap'
                        });
                        trafficMutant.addGoogleLayer('TrafficLayer');

                        olayersmapa = {
                            "Trafico Google": trafficMutant,
                            "Aeropuertos": aeropuertos,
                            "ATM": atm,
                            "Bomberos": bomberos,
                            "Centro Comerciales": ccomercial,
                            "Comisarias": militares,
                            "Educación": universidad,
                            "Estaciones de Metro": metro,
                            "Estaciones Metropolitano": metropolitano,
                            "Farmacias": farmacias,
                            "Ferreterias": ferreterias,
                            "Grifos": grifos,
                            "Hoteles": hoteles,
                            "Iglesias": iglesias,
                            "Kilometraje": kilometro,
                            "Malls": mall,
                            "Restaurantes": restaurantes,
                            "Supermercados": supermercados,
                            "Tiendas Departamentos": tiendaspordepa,
                            "Zonas de Peligro Tradicionales": zpeligro,
                            "Zonas Peligrosas Comerciales": zcomercial
                        };
                    }
                    else {
                        olayersmapa = {
                            "Aeropuertos": aeropuertos,
                            "ATM": atm,
                            "Bomberos": bomberos,
                            "Centro Comerciales": ccomercial,
                            "Comisarias": militares,
                            "Educación": universidad,
                            "Estaciones de Metro": metro,
                            "Estaciones Metropolitano": metropolitano,
                            "Farmacias": farmacias,
                            "Ferreterias": ferreterias,
                            "Grifos": grifos,
                            "Hoteles": hoteles,
                            "Iglesias": iglesias,
                            "Kilometraje": kilometro,
                            "Malls": mall,
                            "Restaurantes": restaurantes,
                            "Supermercados": supermercados,
                            "Tiendas Departamentos": tiendaspordepa,
                            "Zonas de Peligro Tradicionales": zpeligro,
                            "Zonas Peligrosas Comerciales": zcomercial
                        };
                    }
                }
                else {
                    //if (document.getElementById("hdmotormapa").value == "GM") {
                    if (motor == "GM") {
                        var roadMutant = L.gridLayer.googleMutant({
                            maxZoom: 18,
                            type: 'roadmap'
                        });

                        var trafficMutant = L.gridLayer.googleMutant({
                            maxZoom: 18,
                            type: 'roadmap'
                        });
                        trafficMutant.addGoogleLayer('TrafficLayer');

                        olayersmapa = {
                            "Lluvia": rain,
                            "Trafico Google": trafficMutant
                        }
                    }
                    else {
                        olayersmapa = {
                            "Lluvia": rain
                        }
                    }

                    if (document.getElementById("txCapasAdicionales").value != '') {
                        var capas = document.getElementById("txCapasAdicionales").value.split(";");
                        for (ind = 0; ind <= capas.length - 1; ind++) {
                            try {
                                if (capas[ind] != '') {
                                    if (capas[ind] == 'geosys:Puntos Referencia') {
                                        cargarCapasWMS(capas[ind], document.getElementById('idusuario').value);
                                    }
                                    else {
                                        cargarCapasWMS(capas[ind], '');
                                    }
                                }
                            }
                            catch (Error) { console.log(Error); }
                        }
                        capas = null;
                    }
                }

                L.control.layers(
                    ilayersmapa,
                    olayersmapa,
                    {
                        collapsed: true
                    }).addTo(map);

                drawnItems = new L.FeatureGroup();
                map.addLayer(drawnItems);
                map.attributionControl.setPrefix('');
                try {
                    //if (tipoGeo == 0) {
                    //    drawControl = new L.Control.Draw({
                    //        position: 'topleft',
                    //        draw: {
                    //            polyline: {
                    //                metric: true
                    //            },
                    //            polygon: {
                    //                allowIntersection: false,
                    //                showArea: true,
                    //                drawError: {
                    //                    color: '#b00b00',
                    //                    timeout: 1000
                    //                },
                    //                shapeOptions: {
                    //                    color: '#bada55'
                    //                }
                    //            },
                    //            circle: {
                    //                shapeOptions: {
                    //                    color: '#662d91'
                    //                }
                    //            },
                    //            rectangle: false,
                    //            marker: false
                    //        },
                    //        edit: {
                    //            featureGroup: drawnItems,
                    //            remove: true
                    //        }
                    //    });
                    //} else if (tipoGeo == 1) {
                    //    drawControl = new L.Control.Draw({
                    //        position: 'topleft',
                    //        draw: {
                    //            polyline: false,
                    //            polygon: {
                    //                allowIntersection: false,
                    //                showArea: true,
                    //                drawError: {
                    //                    color: '#b00b00',
                    //                    timeout: 1000
                    //                },
                    //                shapeOptions: {
                    //                    color: '#bada55'
                    //                }
                    //            },
                    //            circle: false,
                    //            rectangle: false,
                    //            marker: false
                    //        },
                    //        edit: {
                    //            featureGroup: drawnItems,
                    //            remove: false
                    //        }
                    //    });
                    //} else if (tipoGeo == 2) {
                    //    drawControl = new L.Control.Draw({
                    //        position: 'topleft',
                    //        draw: {
                    //            polyline: {
                    //                metric: true
                    //            },
                    //            polygon: false,
                    //            circle: false,
                    //            rectangle: false,
                    //            marker: false
                    //        },
                    //        edit: {
                    //            featureGroup: drawnItems,
                    //            remove: false
                    //        }
                    //    });
                    //} else if (tipoGeo == 3) {
                    //    drawControl = new L.Control.Draw({
                    //        position: 'topleft',
                    //        draw: {
                    //            polyline: false,
                    //            polygon: false,
                    //            circle: {
                    //                shapeOptions: {
                    //                    color: '#662d91'
                    //                }
                    //            },
                    //            rectangle: false,
                    //            marker: false
                    //        },
                    //        edit: {
                    //            featureGroup: drawnItems,
                    //            remove: false
                    //        }
                    //    });
                    //}

                    drawControl = new L.Control.Draw({
                        position: 'topleft',
                        draw: {
                            polyline: {
                                metric: true
                            },
                            polygon: {
                                allowIntersection: false,
                                showArea: true,
                                drawError: {
                                    color: '#b00b00',
                                    timeout: 1000
                                },
                                shapeOptions: {
                                    color: '#bada55'
                                }
                            },
                            circle: {
                                shapeOptions: {
                                    color: '#662d91'
                                }
                            },
                            rectangle: false,
                            marker: false,
                            circlemarker: false,

                        },
                        edit: {
                            featureGroup: drawnItems,
                            remove: true,

                            toolbar: {
                                actions: {
                                    save: {
                                        title: 'Save changes',
                                        text: 'Save'
                                    },
                                    cancel: {
                                        title: 'Cancel editing, discards all changes',
                                        text: 'Cancel'
                                    },
                                    clearAll: {
                                        title: 'Clear all layers',
                                        text: 'Clear All'
                                    }
                                },
                                buttons: {
                                    edit: 'Editar capas',
                                    editDisabled: 'No layers to edit',
                                    remove: 'Delete layers',
                                    removeDisabled: 'No layers to delete'
                                }
                            },
                            handlers: {
                                edit: {
                                    tooltip: {
                                        text: 'Drag handles or markers to edit features.',
                                        subtext: 'Click cancel to undo changes.'
                                    }
                                },
                                remove: {
                                    tooltip: {
                                        text: 'Click on a feature to remove.'
                                    }
                                }
                            }
                        }
                    });

                    map.addControl(drawControl);
                    map.on('draw:created', function (e) {
                        var type = e.layerType,
                            layer = e.layer;
                        //debugger;
                        if (tipoGeo == 0) {
                            if (type === 'polyline') {
                                if (window.confirm("Desea agregar la geocerca lineal al mapa ?")) {
                                    let pgeocercal = window.prompt("Ingrese el nombre de la geocerca lineal");
                                    if (pgeocercal == '' || pgeocercal == undefined) {
                                        alert('Ingreso de Geocerca lineal cancelado');
                                        map.removeLayer(layer);
                                        return false;
                                    }

                                    let anchol = window.prompt("Ingrese el ancho de la geocerca lineal", 50);
                                    if (anchol == '' || anchol == undefined || anchol == '0') {
                                        alert('Ingreso de Geocerca lineal cancelado, por valor de ancho no valido');
                                        map.removeLayer(layer);
                                        return false;
                                    }

                                    datag.setgeocercaL(0,
                                        $("#idusuario").val(),
                                        $("#hdidsubusuario").val(),
                                        layer._latlngs,
                                        pgeocercal,
                                        String(anchol).replace(",", "."),
                                        0,
                                        pais2
                                    ).then(function (data) {
                                        try {
                                            console.log(data.d);
                                            if (data.d.includes('OK')) {
                                                let index = data.d.split("_");
                                                alert('Geocerca lineal creada con exito');
                                                index = null;
                                                window.parent.HidePopup6();
                                            }
                                        }
                                        catch (Error) {
                                            console.log(Error);
                                        }
                                    });
                                }
                                else {
                                    map.removeLayer(layer);
                                }
                            }

                            if (type === 'polygon') {
                                if (window.confirm("Desea agregar la geocerca poligonal al mapa ?")) {
                                    let pgeocercap = window.prompt("Ingrese el nombre de la geocerca poligonal");
                                    if (pgeocercap == '' || pgeocercap == undefined) {
                                        alert('Ingreso de Geocerca poligonal cancelado');
                                        map.removeLayer(layer);
                                        return false;
                                    }

                                    datag.setgeocercaP(0,
                                        $("#idusuario").val(),
                                        $("#hdidsubusuario").val(),
                                        layer._latlngs[0],
                                        pgeocercap,
                                        0,
                                        0,
                                        pais2
                                    ).then(function (data) {
                                        try {
                                            console.log(data.d);
                                            if (data.d.includes('OK')) {
                                                let index = data.d.split("_");
                                                alert('Geocerca poligonal creada con exito');
                                                index = null;
                                                window.parent.HidePopup6();
                                            }
                                        }
                                        catch (Error) {
                                            alert('No se puede modificar la geocerca');
                                            console.log(Error);
                                        }
                                    });
                                }
                                else {
                                    alert('Ingreso de geocerca cancelado');
                                    map.removeLayer(layer);
                                }
                            }

                            if (type === 'circle') {
                                if (window.confirm("Desea agregar la geocerca circular al mapa ?")) {
                                    let pgeocercac = window.prompt("Ingrese el nombre de la geocerca circular");
                                    if (pgeocercac == '' || pgeocercac == undefined) {
                                        alert('Ingreso de Geocerca circular cancelado');
                                        //window.parent.showNotification2('Ingreso de Geocerca circular cancelado.', 'E');
                                        map.removeLayer(layer);
                                        return false;
                                    }

                                    datag.setgeocercaC(0,
                                        $("#idusuario").val(),
                                        $("#hdidsubusuario").val(),
                                        String(layer._latlng.lat).replace(",", "."),
                                        String(layer._latlng.lng).replace(",", "."),
                                        pgeocercac,
                                        String(layer._mRadius).replace(",", "."),
                                        0,
                                        pais2
                                    ).then(function (data) {
                                        try {
                                            if (data.d.includes('OK')) {
                                                let index = data.d.split("_");
                                                alert('Geocerca circular creada con exito');
                                                //window.parent.showNotification2('Geocerca circular creada con éxito.', 'S');
                                                index = null;
                                                window.parent.HidePopup6();
                                            }
                                        }
                                        catch (Error) {
                                            alert('No se puede modificar la geocerca');
                                            //window.parent.showNotification2('No se puede modificar la geocerca.', 'E');
                                            console.log(Error);
                                        }
                                    });
                                }
                                else {
                                    alert('Ingreso de geocerca cancelado');
                                    //window.parent.showNotification2('Ingreso de geocerca cancelado.', 'A');
                                    map.removeLayer(layer);
                                }
                            }

                            //drawnItems.addLayer(layer);
                            if (type === 'marker') {
                                lmarkersp.push(layer);
                            }
                            else {
                                cgeos.addLayer(layer);
                                lmarkersg.push(layer);
                            }
                        } else {
                            if (lmarkersg.length >= 1) {
                                alert('Solo se permite una geocerca en el mapa');
                                //window.parent.showNotification2('Solo se permite una geocerca en el mapa.', 'A');
                                drawnItems.removeLayer(layer);
                                return false;
                            }

                            Poly = null;
                            Circle = null;
                            Line = null;

                            switch (type) {
                                case 'polygon':
                                    ctype = 1;
                                    break;
                                case 'polyline':
                                    ctype = 2;
                                    break;
                                case 'circle':
                                    ctype = 3;
                                    break;
                            }

                            if (ctype != Number(tipoGeo)) {
                                tipoGeo = ctype;
                            }

                            switch (tipoGeo) {
                                case 1:
                                    Poly = layer;
                                    console.log(Poly);
                                    break;
                                case 2:
                                    Line = layer;
                                    console.log(Line);
                                    break;
                                case 3:
                                    Circle = layer;
                                    console.log(Circle);
                                    break;
                            }

                            lmarkersg.push(layer);
                            drawnItems.addLayer(layer);
                        }
                    });

                    map.on('draw:deleted', function (e) {
                        let type = e.type,
                            layer = e.layer;

                        lmarkersg = null;
                        lmarkersg = new Array();

                        alert('Se ha quitado la geocerca del mapa');
                        //window.parent.showNotification2('Se ha quitado la geocerca del mapa.', 'I');
                    });

                    map.on('draw:edited', function (e) {
                        let layers = e.layers;
                        let countOfEditedLayers = 0;
                        alert('Se ha editado los puntos de la geocerca, puede proceder a guardarla.');

                        layers.eachLayer(function (layer) {
                            console.log(layer.layerType);
                            console.log(layer);
                            countOfEditedLayers++;
                        }); layers = null;
                        countOfEditedLayers = null;
                    });
                }
                catch (Error) {
                    console.log(Error);
                }

                try {
                    var control = L.control.geonames({ username: 'cbi.test' });
                    map.addControl(control);
                }
                catch (Error) { console.log(Error); }

                //var measureControl = new L.Control.Measure(
                //    {
                //        position: 'bottomright',
                //        primaryLengthUnit: 'kilometers',
                //        secondaryLengthUnit: 'mi',
                //        localization: 'es'
                //    });
                //measureControl.addTo(map);

                map.addControl(new L.Control.Scale());

                //debugger;
                if (!isMobile) {
                    L.easyPrint().addTo(map);
                }
                
                L.control.custom({
                    position: 'topright',
                    content: getMenuOpciones(),
                    classes: 'leaflet-control-easyPrint-button leaflet-bar-part',
                    style:
                    {
                        margin: '9px',
                        padding: '0 0 0 0',
                        cursor: 'pointer'
                    },
                    datas:
                    {
                        'foo': 'bar'
                    },
                    events:
                    {
                        click: function (data) {
                        },
                        dblclick: function (data) {

                        },
                        contextmenu: function (data) {
                        },
                    }
                }).addTo(map);

                switch (ModoMapa) {
                    case 'P':
                        break;
                    case 'GL':
                        break;
                    case 'GC':
                        break;
                    case 'GP':
                        break;
                    case 'PR':
                        break;
                    case 'RR':
                        break;
                }
            }
            catch (Error) { console.log(Error); }
        }

        $(document).ready(function () {
            //debugger;
            console.log(tipoGeo2);
            estado = document.getElementById("hdindex").value;
            idgeo = document.getElementById("hId").value;
            nombre = document.getElementById("hNombre").value;
            if (estado == 'N')
                tipo = "";
            else
                tipo = document.getElementById("hTipo").value;
            //tipo = document.getElementById("hTipo").value;
            parametro = document.getElementById("hParametro").value;
            pais = document.getElementById("hdpais2").value;

            user = document.getElementById("idusuario").value;
            subuser = document.getElementById("hdidsubusuario").value;

            listmapas2 = document.getElementById("hdlistamapas").value;
            motor2 = document.getElementById("hdmotormapa").value;
            //listmapas2 = "GM,OS,IOS";
            //motor2 = "GM";

            console.log(idgeo + ":" + nombre + ":" + tipo + ":" + parametro + ":" + user + ":" + subuser + ":" + pais);

            try {
                LimpiarMapa();
                datag.puntosgeocercas(idgeo, tipo, nombre, parametro, pais).then(function (datap) {
                    try {
                        var dPuntos = datap.d;
                        var sPuntos = '';
                        var infoGeo;
                        var Centro = '';
                        var dGeo = dPuntos[0].split(';');
                        dPuntos.splice(0, 1);
                        dPuntos.forEach(async (indg) => {
                            try {
                                infoGeo = null;
                                infoGeo = indg.split("_");

                                sPuntos += new String(new String(infoGeo[1]) + '_' + new String(infoGeo[2])) + ';';
                            } catch (Error) {
                                console.log(Error);
                            }
                        });

                        if (dGeo[1] == '3') {
                            try {
                                Centro = dPuntos[0].split('_')[2].replace(",", ".") + ' ' + dPuntos[0].split('_')[1].replace(",", ".");
                            } catch (Error) {
                                console.log(Error);
                            }
                        }
                        else {
                            sPuntos += new String(new String(dPuntos[0].split('_')[1]).replace(",", ".") + '_' + new String(dPuntos[0].split('_')[2])).replace(",", ".") + ';';
                        }

                        MostrarGeocercaMapa(dGeo[0], dGeo[1], sPuntos, dGeo[3], Centro, dGeo[2], true, false, '');
                    } catch (Error) {
                        console.log(Error);
                    }
                });
            } catch (Error) {
                console.log(Error);
            }

            PosicionarMapa();
            javascript: initializeMap(null, listmapas2, motor2);
        });

        window.onresize = async function () {
            PosicionarMapa(true);
        }

        function MostrarGeocercaMapa(Id, Tipo, Datos, Parametro, Centro, Nombre, Fit, addli, check) {
            //debugger;
            var IngfoGeo = Datos.split(';');
            var PuntosGeo = [];
            var limite = 1;

            var colorgeocerca = gcolors[Math.floor(Math.random() * (gcolors.length - 1))];

            if (Fit == null) {
                Fit = true;
            }

            if (Centro != '') {
                try {
                    InfoCentro = Centro.split(' ');
                }
                catch (Error) { console.log(Error); }
            }
            console.log(InfoCentro);
            if (addli) {
                try {
                    $("#ulgeocercas").append(getligeocercas(Nombre, Tipo, Id, check));
                }
                catch (Error) {
                    console.log(Error);
                }
            }

            if (Tipo == 2) {
                limite = 2;
            }

            try {
                var control = '';
                control = '#'
            }
            catch (Error) {
                console.log(Error);
            }

            Line = null;
            Circle = null;
            Poly = null;
            try {
                for (Ind = 0; Ind < IngfoGeo.length - limite; Ind++) {
                    try {
                        InfoPunto = null;
                        if (IngfoGeo[Ind] != '') {
                            InfoPunto = IngfoGeo[Ind].split('_');
                            PuntosGeo.push(L.latLng(InfoPunto[0], InfoPunto[1]));
                        }
                    }
                    catch (Error) { }
                }
                if (PuntosGeo.length > 2) {
                    if (new Number(Tipo) == 2) {
                        Line = null;
                        Line = L.polyline(PuntosGeo, {
                            color: colorgeocerca,
                            weight: 5,
                            id: Id,
                            fillOpacity: 0.10,
                            nombre: Nombre,
                            tipo: Tipo,
                            parametro: Parametro,
                            centro: Centro,
                            datos: Datos,
                            renderer: myRenderer
                        }).addTo(map);
                        Line.bindTooltip(Nombre, {
                            permanent: true,
                            className: 'leaflet-tooltip-geocercas'
                        });
                        Line.openTooltip();
                        Line.bindPopup(Nombre, {
                            noHide: true
                        }).addTo(map);
                        drawnItems.addLayer(Line);
                        lmarkersg.push(Line);
                    }
                    else {
                        if (new Number(Tipo) == 3) {
                            Circle = null;
                            Circle = new L.circle([InfoCentro[1], InfoCentro[0]], {
                                radius: new Number(Parametro),
                                id: Id,
                                color: colorgeocerca,
                                fillOpacity: 0.10,
                                nombre: Nombre,
                                tipo: Tipo,
                                parametro: Parametro,
                                centro: Centro,
                                datos: Datos,
                                renderer: myRenderer
                            },
                                {
                                    color: colorgeocerca,
                                    fillColor: colorgeocerca,
                                    fillOpacity: 0.20,
                                    renderer: myRenderer
                                }).addTo(map);
                            Circle.bindTooltip(Nombre, {
                                permanent: true,
                                className: 'leaflet-tooltip-geocercas'
                            });
                            Circle.openTooltip();
                            Circle.bindPopup(Nombre, {
                                noHide: true
                            }).addTo(map);
                            drawnItems.addLayer(Circle);
                        }
                        else {
                            Poly = null;
                            Poly = L.polygon(PuntosGeo, {
                                color: colorgeocerca,
                                fillOpacity: 0.10,
                                renderer: myRenderer,
                                id: Id
                            }).addTo(map);
                            Poly.bindTooltip(Nombre,
                                {
                                    permanent: true,
                                    className: 'leaflet-tooltip-geocercas'
                                });
                            Poly.openTooltip();
                            Poly.bindPopup(Nombre, {
                                noHide: true,
                                fillOpacity: 0.10,
                                id: Id,
                                nombre: Nombre,
                                tipo: Tipo,
                                parametro: Parametro,
                                centro: Centro,
                                datos: Datos,
                                renderer: myRenderer
                            }).addTo(map);
                            drawnItems.addLayer(Poly);
                            lmarkersg.push(Poly);
                        }
                    }
                }
                else {
                    if (Tipo == '3') {
                        Circle = null;
                        console.log(InfoCentro);
                        Circle = new L.circle([InfoCentro[1], InfoCentro[0]], {
                            radius: new Number(Parametro),
                            id: Id,
                            color: colorgeocerca,
                            fillOpacity: 0.10,
                            nombre: Nombre,
                            tipo: Tipo,
                            parametro: Parametro,
                            centro: Centro,
                            datos: Datos,
                            renderer: myRenderer
                        },
                            {
                                color: colorgeocerca,
                                fillColor: colorgeocerca,
                                fillOpacity: 0.10,
                                renderer: myRenderer
                            }).addTo(map);
                        Circle.bindTooltip(Nombre, {
                            permanent: true,
                            className: 'leaflet-tooltip-geocercas'
                        });
                        Circle.openTooltip();
                        Circle.bindPopup(Nombre, {
                            maxWidth: "544px"
                        }).addTo(map);
                        drawnItems.addLayer(Circle);
                        lmarkersg.push(Circle);
                    }
                }
                if (Fit) {
                    try { map.fitBounds(lmarkersg[0].getBounds()); } catch (Error) { console.log(Error); }
                }
                console.log(drawnItems);
            }
            catch (Error) {
                console.log(Error);
            }
        }

        function PosicionarMapa(limpiar) {
            try {
                var size = getClientSize();
                var omapa = document.getElementById("dvmapa");

                omapa.style.left = '-10px';
                //omapa.style.width = (size.width - 820) + 'px';
                omapa.style.width = '100%';
                omapa.style.height = (size.height - 30) + 'px';

                try { map.invalidateSize(false) } catch (Error) { console.log(Error); };

                omapa = null;
                size = null;
            }
            catch (Error) {
                console.log(Error);
            }
        }

        function LimpiarMapa() {
            try {
                LatitudInicial = parseFloat(document.getElementById("txLatitudInicial").value);
                LongitudInicial = parseFloat(document.getElementById("txLongitudInicial").value);
            }
            catch (Error) {
                LatitudInicial = -2.1672;
                LongitudInicial = -79.9172;
            }

            for (var i = 0; i <= lmarkersg.length - 1; i++) {
                try {
                    map.removeLayer(lmarkersg[i]);
                }
                catch (Error) { console.log(Error); }
            }
            lmarkersg = null;
            lmarkersg = new Array();

            cgeos = null;
            cgeos = new L.featureGroup();

            cgeoseg = null;
            cgeoseg = new L.featureGroup();

            $("#hTipo").val('');
            $("#hId").val('');
            $("#hNombre").val('');
            $("#hParametro").val('');
            ccapas = 0;
        }

        function guardargeocercapuntos() {
            try {
                //debugger;
                let ogeocerca;
                var opcion = tipoGeo;
                console.log(idgeo + ":" + nombre + ":" + tipoGeo + ":" + parametro + ":" + pais);
                //let id, tipo, nombre, par1;

                //id = document.getElementById("hId").value
                //nombre = document.getElementById("hNombre").value
                //tipo = document.getElementById("hTipo").value
                //par1 = document.getElementById("hParametro").value
                //debugger;
                
                try {
                    switch (opcion) {
                        case 1:
                            ogeocerca = Poly;
                            datag.setpuntosgeocercacP(idgeo,
                                $("#idusuario").val(),
                                $("#hdidsubusuario").val(),
                                ogeocerca._latlngs[0],
                                nombre,
                                parametro,
                                pais
                            ).then(function (data) {
                                try {
                                    if (data.d.includes('OK')) {
                                        let index = data.d.split("_");
                                        location.reload();
                                        alert('Geocerca poligonal actualizada con exito');
                                    }
                                }
                                catch (Error) {
                                    alert('No se puede modificar la geocerca');
                                    console.log(Error);
                                }
                            });
                            break;
                        case 2:
                            ogeocerca = Line;
                            datag.setpuntosgeocercacL(idgeo,
                                $("#idusuario").val(),
                                $("#hdidsubusuario").val(),
                                ogeocerca._latlngs,
                                nombre,
                                parametro,
                                pais
                            ).then(function (data) {
                                try {
                                    if (data.d.includes('OK')) {
                                        let index = data.d.split("_");
                                        location.reload();
                                        alert('Geocerca lineal actualizada con exito');
                                    }
                                }
                                catch (Error) {
                                    alert('No se puede modificar la geocerca');
                                    console.log(Error);
                                }
                            });
                            break;
                        case 3:
                            ogeocerca = Circle;
                            datag.setpuntosgeocercacC(idgeo,
                                $("#idusuario").val(),
                                $("#hdidsubusuario").val(),
                                new String(ogeocerca._latlng.lat).replace(",", "."),
                                new String(ogeocerca._latlng.lng).replace(",", "."),
                                nombre,
                                new String(ogeocerca._mRadius).replace(",", "."),
                                pais
                            ).then(function (data) {
                                try {
                                    if (data.d.includes('OK')) {
                                        let index = data.d.split("_");
                                        location.reload();
                                        alert('Geocerca circular actualizada con exito');
                                    }
                                }
                                catch (Error) {
                                    alert('No se puede modificar la geocerca');
                                    console.log(Error);
                                }
                            });

                            break;
                    }
                } catch (Error){
                    console.log(Error);
                }
                    

                LimpiarMapa();

                tipo = null;
                id = null;
                nombre = null;
                par1 = null;
                ogeocerca = null;
                document.getElementById("hTipo").value = opcion;
                tipoGeo2 = opcion;
            }
            catch (Error) {
                console.log(Error);
            }
        }
    </script>
    
    <form id="form1" runat="server">
        <%=ReferenciaGoogle2 %>
        <%If (ReferenciaGoogle2 <> "") Then %>
        <%--<script src="../../Libs/leaflet/js/Leaflet.GoogleMutant.js" type="text/javascript"></script>--%>
        <script src="https://unpkg.com/leaflet.gridlayer.googlemutant@latest/dist/Leaflet.GoogleMutant.js"></script>
        <%end if %>
        <asp:ScriptManager ID="smMaster" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <div class="row">
            <div id="central" runat="server" class="col" style="display:inline-block">
                <div id="dvmapa" style="background-color: lightgray;"></div>
            </div>
        </div>

        <asp:HiddenField ID="idusuario" runat="server" />
        <asp:HiddenField ID="hdidusuario" runat="server" />
        <asp:HiddenField ID="hdidsubusuario" runat="server" />
        <asp:HiddenField ID="hdindex" runat="server" />

        <asp:HiddenField ID="hdurlgeocercas" runat="server" />
        <asp:HiddenField ID="hdurlgeocercasdetalle" runat="server" />
        <asp:HiddenField ID="hdurlsetgeocercas" runat="server" />
        <asp:HiddenField ID="hdurlsetgeocercasdetalleC" runat="server" />
        <asp:HiddenField ID="hdurlsetgeocercasdetalleR" runat="server" />
        <asp:HiddenField ID="hdurlsetgeocercasdetalleL" runat="server" />
        <asp:HiddenField ID="hdurlsetgeocercasdetalleP" runat="server" />

        <asp:HiddenField ID="hdlistamapas" runat="server" />
        <asp:HiddenField ID="hdservidorpe" runat="server" />
        <asp:HiddenField ID="hdservidorcapaspe" runat="server" />
        <asp:HiddenField ID="txLatitudInicial" runat="server" />
        <asp:HiddenField ID="txLongitudInicial" runat="server" />
        <asp:HiddenField ID="hdmotormapa" runat="server" />
        <asp:HiddenField ID="hdpais" runat="server" />
        <asp:HiddenField ID="hdpais2" runat="server" />
        <asp:HiddenField ID="txCapasAdicionales" runat="server" />

        <asp:HiddenField ID="hId" runat="server"/>
        <asp:HiddenField ID="hNombre" runat="server"/>
        <asp:HiddenField ID="hTipo" runat="server"/>
        <asp:HiddenField ID="hParametro" runat="server"/>
    </form>
</body>
</html>
