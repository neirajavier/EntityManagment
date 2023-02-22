<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MapaPuntos.aspx.vb" Inherits="ArtemisAdmin.MapaPuntos" %>

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
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.14/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/@geoman-io/leaflet-geoman-free@latest/dist/leaflet-geoman.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-control-geocoder@latest/dist/Control.Geocoder.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.8.0/dist/leaflet.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-draw@0.4.1/dist/leaflet.draw.css" />
    <link href="../../Libs/leaflet/css/leaflet.marker.highlight.css" rel="stylesheet" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mapa Puntos</title>

    <script language="JavaScript" type="text/javascript"> 
        var map;
        var agrupos = new Array();
        var lmarkers = new Array();
        var myRenderer, drawnItems;
        var InfoPunto, Ind, InfoCentro, Rect;
        var drawControl;

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
    <script src="https://unpkg.com/leaflet-draw@0.4.9/dist/leaflet.draw.js"></script>
    <script src="https://unpkg.com/@geoman-io/leaflet-geoman-free@latest/dist/leaflet-geoman.min.js"></script>
    <script src="../../Libs/leaflet/js/Control.FullScreen.js"></script>

    <script language="JavaScript" type="text/javascript"> 
        var idpto, nombre, latitud, longitud, color, maximo, user, subuser, generar, pais, pais2;
        var listmapas2, motor2;
        var seleccion = "0";

        datap = {
            setubicacionpunto: async function (idpunto, latitud, longitud, idusuario, idsubusuario, indice, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlpuntosubicacion').value,
                    data: "{'IdPunto':" + new String(idpunto) + ",'Latitud':'" + new String(latitud) + "','Longitud':'" + new String(longitud) + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','Indice':'" + new String(indice) + "','Pais':'" + new String(pais) + "'}",
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
            setpunto: async function (idpunto, latitud, longitud, nombre, descripcion, codigo, idusuario, idsubusuario, idcolor, idcategoria, idsubcategoria, celular, email, accion, indice, pais) {
                let dfr = $.Deferred();
                if (idsubusuario == undefined) {
                    idsubusuario = '0';
                }
                $.ajax({
                    type: "POST",
                    url: document.getElementById('hdurlpuntos').value,
                    data: "{'IdPunto':" + new String(idpunto) + ",'Latitud':'" + new String(latitud) + "','Longitud':'" + new String(longitud) + "','Nombre':'" + nombre + "','Descripcion':'" + descripcion + "','Codigo':'" + codigo + "','IdUsuario':'" + new String(idusuario) + "','IdSubUsuario':'" + new String(idsubusuario) + "','IdColor':'" + new String(idcolor) + "','IdCategoria':'" + new String(idcategoria) + "','IdSubCategoria':'" + new String(idsubcategoria) + "','Email':'" + String(email) + "','Celular':'" + String(celular) + "','Accion':'" + accion + "','Indice':'" + new String(indice) + "','Pais':'" + new String(pais) + "'}",
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
                //debugger;
                var menucontext = true;
                var ilayersmapa = {};
                var olayersmapa = {};
                //var listamapas = new String(document.getElementById('hdlistamapas').value).split(',');
                //var listamapas = new String('GM,OS,IOS').split(',');
                pais2 = document.getElementById("hdpais2").value;
                var listamapas = mapas.split(',');
                var capai, ncapai;
                var ini = 0;

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
                                        capai = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                                            {
                                                attribution: '&copy; <a href="https://openstreetmap.org">OpenStreetMap</a> contributors',
                                                maxZoom: 19
                                            });
                                        ini += 1;
                                    }

                                    osm = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                                        {
                                            attribution: '&copy; <a href="https://openstreetmap.org">OpenStreetMap</a> contributors',
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

                map = new L.Map('dvmapa', {
                    center: new L.LatLng(LatitudInicial, LongitudInicial), zoom: 6,
                    layers: [capai]
                });

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

                rain = L.tileLayer('https://tile.openweathermap.org/map/precipitation_new/{z}/{x}/{y}.png?appid=c71be71d270ce3219d2e251fbd840dc7',
                    {
                        attribution: '&copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors',
                        maxZoom: 18
                    });

                if (document.getElementById("hdpais").value == 'PE1') {
                    //CargarCapasPE();

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
                    drawControl = new L.Control.Draw({
                        position: 'topleft',
                        draw: {
                            polyline: false,
                            polygon: false,
                            circle: false,
                            rectangle: false,
                            marker: true
                        },
                        edit: {
                            featureGroup: drawnItems,
                            remove: false
                        }
                    });
                    map.addControl(drawControl);
                } catch (Error) {
                    console.log(Error);
                }

                try {
                    map.on('draw:created', function (e) {
                        var type = e.layerType,
                            layer = e.layer;

                        debugger;
                        if (type === 'marker') {
                            let pcodigo = "";

                            if (window.confirm("Desea agregar este nuevo Punto al mapa ?")) {
                                let pnombre = window.prompt("Ingrese el nombre del punto");

                                //document.getElementById("hdregs").value = pnombre;
                                //gridMapa.PerformCallback('validar');
                                //seleccion = JSON.parse(gridMapa.cpValidar);

                                if (generar == "0") {
                                    if (seleccion == "1") {
                                        pcodigo = window.prompt("Ingrese el codigo del punto");
                                    } else {
                                        pcodigo = "";
                                    }
                                } else {
                                    pcodigo = maximo;
                                }

                                if (pnombre == '' || pnombre == undefined) {
                                    alert('Ingreso de punto referencial cancelado');
                                    map.removeLayer(layer);
                                    return false;
                                }

                                datap.setpunto(0,
                                    layer.getLatLng().lat,
                                    layer.getLatLng().lng,
                                    pnombre,
                                    '',
                                    pcodigo,
                                    $("#idusuario").val(),
                                    $("#hdidsubusuario").val(),
                                    0,
                                    0,
                                    0,
                                    '',
                                    '',
                                    'ING',
                                    1,
                                    pais
                                ).then(function (data) {
                                    try {
                                        if (data.d.includes('OK')) {
                                            let index = data.d.split("_");
                                            //try {
                                            //    $("#ulpuntos").append(getlipuntos(index[2], index[3], index[4], index[1], ''));
                                            //}
                                            //catch (Error) {
                                            //    console.log(Error);
                                            //}
                                            index = null;

                                            alert("Punto ingresado con Exito");
                                            try {
                                                map.setView(index[3], index[4], 6, {
                                                    reset: true
                                                });
                                            }
                                            catch (Error) {
                                                console.log(Error);
                                            }
                                            window.parent.HidePopup6();
                                            //$("#sptotalpuntos").text(apuntos.length);
                                        } else if (data.d.includes('Error_-2')) {
                                            seleccion = "1";
                                            window.parent.showNotification2('Nombre de punto ya existente, ingrese nuevamente el punto y agregar el código.', 'A');
                                        }
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                });

                                try {
                                    layer.setIcon(new LeafIcon({
                                        iconUrl: 'images/infowin/gps0.png',
                                        shadowSize: [0, 0],
                                        iconSize: [14, 14]
                                    }));

                                    layer.bindTooltip(pnombre + '', {
                                        permanent: true,
                                        className: 'leaflet-tooltip-puntos'
                                    });
                                    layer.openTooltip();
                                }
                                catch (Error) {
                                    console.log(Error);
                                }
                            }
                            else {
                                alert('Ingreso de punto referencial cancelado');
                                map.removeLayer(layer);
                            }
                        }

                        //drawnItems.addLayer(layer);
                        //if (type === 'marker') {
                        //    lmarkersp.push(layer);
                        //}
                        //else {
                        //    cgeos.addLayer(layer);
                        //    lmarkersg.push(layer);
                        //}
                    });
                } catch (Error) {
                    console.log(Error);
                }

                try {
                    var control = L.control.geonames({ username: 'cbi.test' });
                    map.addControl(control);
                }
                catch (Error) { console.log(Error); }

                var measureControl = new L.Control.Measure(
                    {
                        position: 'bottomright',
                        primaryLengthUnit: 'kilometers',
                        secondaryLengthUnit: 'mi',
                        localization: 'es'
                    });
                measureControl.addTo(map);

                map.addControl(new L.Control.Scale());
                L.easyPrint().addTo(map);

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
            idpto = document.getElementById("hId").value;
            nombre = document.getElementById("hNombre").value;
            latitud = document.getElementById("hLatitud").value;
            longitud = document.getElementById("hLongitud").value;
            color = document.getElementById("hColor").value;
            maximo = document.getElementById("hMaximo").value;
            generar = document.getElementById("hGenera").value;
            pais = document.getElementById("hdpais2").value;

            user = document.getElementById("idusuario").value;
            subuser = document.getElementById("hdidsubusuario").value;

            listmapas2 = document.getElementById("hdlistamapas").value;
            motor2 = document.getElementById("hdmotormapa").value;
            //listmapas2 = "GM,OS,IOS";
            //motor2 = "GM";

            if (color == "") {
                color = "0";
            }
            console.log(idpto + ":" + nombre + ":" + latitud + ":" + longitud + ":" + color + ":" + user + ":" + subuser);

            //javascript: initializeMap();
            try {
                if (idpto != "") {
                    PosicionarMapa();
                    javascript: initializeMap(null, listmapas2, motor2);
                    addMarcadorPuntoReferencial(idpto, nombre, latitud, longitud, 18, 0, color, 0, 1, 1);
                } else {
                    PosicionarMapa();
                    javascript: initializeMap(null, listmapas2, motor2);
                }
            }
            catch (Error) {
                console.log(Error);
            }

        });

        window.onresize = async function () {
            PosicionarMapa(true);
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

            for (var i = 0; i <= lmarkers.length - 1; i++) {
                try {
                    map.removeLayer(lmarkers[i]);
                }
                catch (Error) { console.log(Error); }
            }
            lmarkers = null;
            lmarkers = new Array();

            cgeos = null;
            cgeos = new L.featureGroup();

            cgeoseg = null;
            cgeoseg = new L.featureGroup();

            ccapas = 0;
        }

        function addMarcadorPuntoReferencial(id, Punto, Lat, Lon, Zoom, Tipo, Color, Alerta, Limpiar, Indice) {
            //debugger;
            var Texto = '';
            var Point, MarcadorPunto;

            if (Limpiar == 1) {
                LimpiarMapa();
                map.setView(new L.latLng(0, 0));
                map.panTo(new L.latLng(0, 0));
            }

            if (Alerta == 1) {
                Texto += '<div align="left"><font color="#000000"><table class="style2000"><tr><td class="style2000b" colspan="2"><b>Información de la Alerta</b></td></tr>' + '<tr><td colspan="2" class="style2000"><hr width="100%"></td></tr>';
                Texto += '<tr><td class="style2000b">Alerta: </td><td class="style2000">' + Punto + '</td></tr>';
            }
            else {
                Texto += '<div align="left"><font color="#000000"><table class="style2000"><tr><td class="style2000b" colspan="2"><b>Información del Punto</b></td></tr>' + '<tr><td colspan="2" class="style2000"><hr width="100%"></td></tr>';
                Texto += '<tr><td class="style2000b">Punto: </td><td class="style2000">' + Punto + '</td></tr>';
            }

            //if (document.getElementById("txMostrarCoordenadas").value == 'True') {
            //    Texto += '<tr><td class="style2000b"> Lat y Long: </td><td class="style2000b">' + getCoordenadas(document.getElementById("txUnidadCoordenadas").value, Lat, Lon) + '</td></tr>';
            //}
            Icon = null;

            if (Alerta == 1) {
                Imagen = '../../images/icoAlerta.png';
            }
            else {
                try { Imagen = '../../images/icoPuntosReferencialesD' + new String(Color) + '.png'; }
                catch (Error) { Imagen = '../../images/icoPuntosReferencialesD.png'; }
                //map.setView(new L.latLng(Lat, Lon));
            }

            Icon = null;
            Icon = new LeafIcon({ iconUrl: Imagen, shadowSize: [0, 0] });

            MarcadorPunto = null;
            MarcadorPunto = null;
            MarcadorPunto = L.marker(new L.latLng(Lat, Lon),
                {
                    icon: Icon,
                    id: new String(id),
                    placa: new String(Punto),
                    lat: parseFloat(Lat),
                    lon: parseFloat(Lon),
                    title: Punto,
                    indice: Indice,
                    draggable: true
                });

            MarcadorPunto.on('dragend', function (d, c) {
                try {
                    let lat;
                    let lon;
                    if (window.confirm('Desea guardar el cambio de ubicacion del punto seleccionado')) {
                        lat = this.getLatLng().lat;
                        lon = this.getLatLng().lng;

                        datap.setubicacionpunto(this.options.id,
                            lat,
                            lon,
                            $("#idusuario").val(),
                            $("#hdidsubusuario").val(),
                            this.options.indice,
                            pais
                        ).then(function (data) {
                            if (data.d.includes('OK')) {
                                let index = data.d.split("_");
                                testado.rows().every(function (rowIdx, tableLoop, rowLoop) {
                                    let data = testado.data()[rowIdx];
                                    if (data.idpunto == index[1]) {

                                        try {
                                            data.latitud = new String(lat).substr(0, 8);
                                        }
                                        catch (Error) {
                                            data.latitud = lat;
                                        }

                                        try {
                                            data.longitud = new String(lon).substr(0, 8);
                                        }
                                        catch (Error) {
                                            data.longitud = lon;
                                        }

                                        try {
                                            parent.opener.document.getElementById("hdpc_" + data.idpunto).innerHTML = '<img loading="lazy" style="width14px; height:14px" src="Images/infowin/gps.png" />&nbsp;' + data.latitud + ' ' + data.longitud;
                                        }
                                        catch (Error) {
                                            console.log(Error);
                                        }

                                        try {
                                            let infoc = new String(parent.opener.document.getElementById("hdpc_" + data.idpunto).value).split("_");

                                            infoc[2] = data.latitud;
                                            infoc[3] = data.longitud;

                                            parent.opener.document.getElementById("hdpc_" + data.idpunto).value = infoc[0] + '_' + infoc[1] + '_' + infoc[2] + '_' + infoc[3];
                                            infoc = null;
                                        }
                                        catch (Error) {
                                            console.log(Error);
                                        }

                                        testado.row(rowIdx).data(data);
                                    }
                                    data = null;
                                });
                                alert('Ubicacion del punto actualizada con exito');
                                testado.draw();

                                lat = null;
                                lon = null;

                                index = null;
                            }
                        });
                    }
                }
                catch (Error) {
                    console.log(Error);
                }
            });

            MarcadorPunto.bindTooltip(Punto, { permanent: true, className: 'leaflet-label' });
            MarcadorPunto.openTooltip();

            MarcadorPunto.alt = new String(Punto);
            MarcadorPunto.bindPopup(Texto, { maxWidth: "544px" }).addTo(map);
            lmarkers.push(MarcadorPunto);

            if (Limpiar == 1) {
                map.setView(new L.latLng(Lat, Lon));
                map.panTo(new L.latLng(Lat, Lon));
                map.setZoom(Zoom);
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

        function setColor(Color) {
            var id = '';

            if (Color == '#00457E') { id = '0'; }
            if (Color == '#FF0000') { id = '1'; }
            if (Color == '#000000') { id = '2'; }
            if (Color == '#77FF00') { id = '3'; }
            if (Color == '#FF9900') { id = '4'; }
            if (Color == '#FFDA44') { id = '5'; }
            if (Color == '#933EC5') { id = '6'; }

            return new String(id);
        }

        function getColor(id) {
            var color = '#00457E';
            if (id == '0') { color = '#00457E'; }
            if (id == '1') { color = '#FF0000'; }
            if (id == '2') { color = '#000000'; }
            if (id == '3') { color = '#77FF00'; }
            if (id == '4') { color = '#FF9900'; }
            if (id == '5') { color = '#FFDA44'; }
            if (id == '6') { color = '#933EC5'; }
            return color;
        }

    </script>

    <form id="form1" runat="server">
        <%=ReferenciaGoogle3 %>
        <%If (ReferenciaGoogle3 <> "") Then %>
        <%--<script src="../../Libs/leaflet/js/Leaflet.GoogleMutant.js" type="text/javascript"></script>--%>
        <script src="https://unpkg.com/leaflet.gridlayer.googlemutant@latest/dist/Leaflet.GoogleMutant.js"></script>
        <%end if %>
        <asp:ScriptManager ID="smMaster" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <div class="row">
            <div id="central" runat="server" class="col" style="display: inline-block">
                <div id="dvmapa" style="background-color: lightgray;"></div>
                <dx:ASPxGridView runat="server" ID="gridMapa" ClientInstanceName="gridMapa" Width="100%" AutoGenerateColumns="False" ClientVisible="false"
                    OnCustomCallback="gridMapa_CustomCallback" />
            </div>
        </div>

        <asp:HiddenField ID="idusuario" runat="server" />
        <asp:HiddenField ID="hdidusuario" runat="server" />
        <asp:HiddenField ID="hdidsubusuario" runat="server" />
        <asp:HiddenField ID="hdindex" runat="server" />
        <asp:HiddenField ID="hdregs" runat="server" />

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

        <asp:HiddenField ID="hId" runat="server" />
        <asp:HiddenField ID="hNombre" runat="server" />
        <asp:HiddenField ID="hLatitud" runat="server" />
        <asp:HiddenField ID="hLongitud" runat="server" />
        <asp:HiddenField ID="hColor" runat="server" />
        <asp:HiddenField ID="hGenera" runat="server" />
        <asp:HiddenField ID="hMaximo" runat="server" />

        <asp:HiddenField ID="hdurlpuntosubicacion" runat="server" />
        <asp:HiddenField ID="hdurlpuntos" runat="server" />
    </form>
</body>
</html>
