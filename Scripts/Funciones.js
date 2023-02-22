var map, myIcon, myPoly, lines, txtFile, geoCoder, Icon, markerOptions, subIcon, obdIcon;
var evtLoadMapa, Limpiar, Nivel, Direccion, vnPuntos, startLat, startLng; var VidMarcadores = new Array(); var TextoMarcadores = new Array();
var bounds, states, NodoSeleccionado, oTree, mgr, mgrOptions, Puntos;
var CodigoEventos = new Array(); var DescEventos = new Array(); var NumeroEvento = new Array(); var ProductoActivo = new Array();
var LatitudInicial = 0.0; var LongitudInicial = 0.0; var VIDSeleccionado; var lPuntosGeo = new Array(); var win;
startLat = 0.0; startLon = 0.0;
var Fuente = 'Geneva';
var LINK = 0; var CHECKBOX = 1; var TREEVIEW_ID = "trvUnidades";
var copyOSM; var tilesMapnik; var tilesOsmarender; var mapMapnik; var mapOsmarender; var ovMap;
var mini; var miniZoom = 0; var FilaActualEstadoFlota;
var weatherLayer, cloudLayer, trafficLayer, LayersMapa, LayersRuler, ReglaActiva, lConductores;
var toggleState = 0;
var mapafull = false;
var mostrarfull = false;
var panorama, sv;
var hupdate, halertas;
var hubicacion;
var lmarkers = new Array();
var lmarkersa = new Array();
var lmarkersp = new Array();
var lmarkersg = new Array();
var cgeos = new L.featureGroup();
var cgeoseg = new L.featureGroup();
var ccapas = 0;
var hib, sat, osm, gm, hun;
var capasadicionales = false;
var lUnidades;
var capai, ncapai;
var sidebar;
var lRecorrido;
var clusterr = false;
var lastZoom;
var drawnItems;
var alertasLogin = 0;
var mzbound;
var ciLayer;
var aentidades;
var aunidades;
var aliunidades = new Array();
var apuntos = new Array();
var ageocercas = new Array();
var aalertas = new Array();
var aconductores = new Array();
var adalertas = new Array();
var agrupos;
var agruposunidad;
var agruposgeo;
var agrupopuntos;
var abounds, drawnItems;

var lsonidos = new Array();
var myRenderer;
var estadop = false;
var estadog = false;
var mostrarp = false;
var mostrarg = false;
var factor, newY, newX;
var oms;
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

try {
    cIcono = L.Icon.extend({
        options: {
            iconSize: [24, 24],
            iconAnchor: [12, 20],
            popupAnchor: [-2, -20]
        }
    });
}
catch (Error) { console.log(Error); }

function getInfoWindow() {
    try {
        let opciones = '';
        opciones = '<div id="dvinfowindow" style="display:none;background-color:transparent;border=0;border-collapse:separate;height:242px;width: 373px; text-align:center;top:30px">'
            + '</div>';

        return opciones;
    }
    catch (Error) {
        console.log(Error);
    }
}

function AbrirGeocercaAdm() {
    window.open('Geofences.aspx?TIME=' + String(document.getElementById("hdwuid").value), '_blank');
}

function RegistrarUnidad(s, e) {
    if (window.confirm('Desea Registrar un Vehiculo Nuevo a su Usuario (Válido solo para Clientes con Vehiculos Nuevos Toyota)') == true) { vnRegistroUnidadC.Show(); }
}

function CrearSubUsuario(s, e) {
    window.open('Last/Registro/SubUsrs.aspx?TIME=' + new String(document.getElementById("hdwuid").value), '_blank');
}

function AbrirPuntoRef() {
    window.open('Points.aspx?TIME=' + String(document.getElementById("hdwuid").value), '_blank');
}

function showCoordinates(e) {
    alert(e.latlng);
}

function centerMap(e) {
    map.panTo(e.latlng);
}

function zoomIn(e) {
    map.zoomIn();
}

function zoomOut(e) {
    map.zoomOut();
}

function hideMenu(e) {
    map.contextmenu.hide();
}

function addPoint(e) {
    try {
        document.getElementById("txLatitudMapa").value = String(e.latlng.lat);
        document.getElementById("txLongitudMapa").value = String(e.latlng.lng);
        AgregarPuntoReferencial();
    }
    catch (Error) { console.log(Error); }
}

function addGeoC(e) {
    try {
        document.getElementById("txLatitudMapa").value = String(e.latlng.lat);
        document.getElementById("txLongitudMapa").value = String(e.latlng.lng);
        AgregarGeocercaC();
    }
    catch (Error) { console.log(Error); }
}

function setearValorCheck(item) {
    try {
        item.checked = !item.checked;
    }
    catch (Error) {
        console.log(Error);
    }
}

function searchUnits(e) {
    try {
        let distancia;
        document.getElementById("txLatitudMapa").value = String(e.latlng.lat);
        document.getElementById("txLongitudMapa").value = String(e.latlng.lng);
        document.getElementById('hdcercanos').value = '';
        document.getElementById('hdcercanosp').value = '';

        distancia = window.prompt("Ingrese el Radio de Busqueda (kms)", 2);


        if (distancia != null) {
            document.getElementById('hddistancia').value = distancia;
            BuscarCercanos(1, 1, '');
        }
        else {
            alert("Radio de Busqueda no Valido");
        }
    }
    catch (Error) { console.log(Error); }
}

function processSVData(data, status) {
    var Rumbo;
    if (document.getElementById("txRumbo").value == "") {
        Rumbo = 45;
    }
    else {
        Rumbo = new Number(document.getElementById("txRumbo").value) + 180;
    }

    if (status === google.maps.StreetViewStatus.OK) {
        let markerPanoID = data.location.pano;
        panorama.setPano(markerPanoID);
        panorama.setPov({
            heading: Rumbo,
            pitch: 0
        });
        panorama.setVisible(true);
    } else {
        //document.getElementById("sv").style.display = 'none';
        //EnviarMSJ('No Existen imagenes para la locacion seleccionada');
    }
}

function CenterControlSV(controlDiv, mapa) {
    // Set CSS for the control border.
    var controlUI = document.createElement('div');
    controlUI.style.backgroundColor = 'red';
    controlUI.style.border = '2px solid red';
    controlUI.style.borderRadius = '3px';
    controlUI.style.boxShadow = '0 2px 6px rgba(0,0,0,.3)';
    controlUI.style.cursor = 'pointer';
    controlUI.style.marginBottom = '12px';
    controlUI.style.textAlign = 'center';
    controlUI.title = 'Cerrar';
    controlDiv.appendChild(controlUI);

    // Set CSS for the control interior.
    var controlText = document.createElement('div');
    controlText.style.color = 'rgb(25,25,25)';
    controlText.style.fontFamily = Fuente;
    controlText.style.fontSize = '12px';
    controlText.style.fontStyle.bold = '1';
    controlText.style.color = 'white';
    controlText.style.lineHeight = '38px';
    controlText.style.paddingLeft = '5px';
    controlText.style.paddingRight = '5px';
    controlText.innerHTML = 'X';

    controlUI.addEventListener('click', function () {
        document.getElementById('sv').style.display = 'none';
    });

    controlUI.appendChild(controlText);
}

function AbrirLink(Alias) {
    try {
        window.open('Last/ActivoLink.aspx?P=' + Alias, '_blank');
    }
    catch (Error) { console.log(Error); }
}

function AbrirInfo(Alias) {
    try {
        window.open('Last/ActivoInformacion.aspx?P=' + Alias, '_blank');
    }
    catch (Error) { console.log(Error); }
}

function UbicarWZ(Latitud, Longitud, Rumbo, Actualizar) {
    try {
        window.open('https://www.waze.com/es-419/livemap?zoom=15&lat=' + String(Latitud) + '&lon=' + String(Longitud), '_blank');
    }
    catch (Error) {
        console.log(Error);
    }
}

function UbicarSV(Latitud, Longitud, Rumbo, Actualizar) {
    if (!Actualizar) {
        if (document.getElementById("sv").style.display == '') {
            document.getElementById("sv").style.display = 'none';
        }
        else {
            document.getElementById("sv").style.display = '';
        }
    }

    if (panorama == null) {
        panorama = new google.maps.StreetViewPanorama(document.getElementById('sv'));

        var centerControlDiv = document.createElement('div');
        var centerControl = new CenterControlSV(centerControlDiv, map);

        centerControlDiv.index = 1;
        panorama.controls[google.maps.ControlPosition.TOP_RIGHT].push(centerControlDiv);
    }

    if (mostrarfull) {
        document.getElementById("sv").style.left = '34px';
        Latitud = $("#txLatitudMapa").val();
        Longitud = $("#txLongitudMapa").val();
    }
    else {
        document.getElementById("sv").style.left = '320px';
    }

    try {
        sv.getPanorama({ location: new google.maps.LatLng(Latitud, Longitud), radius: 50 }, processSVData);
    }
    catch (Error) { console.log(Error); }
}

function EditarConductor(Conductor) { return false; }

function MostrarPuntos(event) {
    var vertices = this.getPath();
    var contentString = '<b>Puntos Graficados</b><br>';
    contentString += 'Coord. Central: <br>' + event.latLng.lat() + ',' + event.latLng.lng() + '<br>';
    for (var i = 0; i < (vertices.length - 1); i++) {
        var xy = vertices.getAt(i);
        contentString += '<br>' + 'Coordenada: ' + (i + 1) + '<br>' + xy.lat() + ',' + xy.lng();
    }
    infowindow.setContent(contentString);
    infowindow.setPosition(event.latLng);
    infowindow.open(map);
}

function EditarEtiqueta(VID) {
    if (document.getElementById("txEditarEtiquetas").value == "False") { alert('Opcion no Habilitada para su Usuario'); return false; }
    try {
        document.getElementById("txVID").value = VID;
        vnEtiquetaC.SetContentUrl('Etiqueta.aspx?ID=' + VID);
        vnEtiquetaC.Show();
    }
    catch (Error) { alert('No se Puede Editar la Etiqueta'); }
}

function EditarAgenda(VID) { }

function CentrarEnPunto(Lat, Lon, Zoom) { map.setCenter(new GLatLng(Lat, Lon), Zoom); }

function LimpiarLogin() { document.getElementById("txUsuario").value = ''; document.getElementById("txContraseña").value = ''; document.getElementById("txUsuario").focus(); }

function CargarCapas(Capas) {
    return false;
    if (document.getElementById("txCargarBeriles").value === '0') { return false }
    try {
        var geoXMLGal = new google.maps.KmlLayer("http://www.huntermonitoreoperu.com/Geo/Kml/limitesgalapagos.kml", { clickable: false, preserveViewport: true });
        var geoXMLLimCosta = new google.maps.KmlLayer("http://www.huntermonitoreoperu.com/Geo/Kml/limitescosta.kml", { clickable: false, preserveViewport: true });
        var geoXMLProfCosta = new google.maps.KmlLayer("http://www.huntermonitoreoperu.com/Geo/Kml/profundidadcosta.kml", { clickable: false, preserveViewport: true });
        geoXMLGal.setMap(map);
        geoXMLLimCosta.setMap(map);
        geoXMLProfCosta.setMap(map);
        geoXMLGal = null; geoXMLLimCosta = null; geoXMLProfCosta = null;
    }
    catch (Error) { console.log(Error); }
}

function cargarCapasWMS(Capa, Criterio) {
    var wcapa;
    try {
        return false;
        if (document.getElementById("hdpais").value != 'EC') {
            return false;
        }
        var url = "https://www.huntermonitoreo.com:8888/geoserver/geosys/wms?";
        //url += "&service=WMS";           //WMS service
        //url += "&version=1.1.0";         //WMS version 
        //url += "&request=GetMap";        //WMS operation
        //url += "&styles=";               //use default style
        //url += "&format=image/png";      //image format
        //url += "&TRANSPARENT=TRUE";      //only draw areas where we have data         
        //url += "&width=256";             //tile size used by google
        //url += "&height=256";
        //url += "&tiled=true";
        //if (Criterio != '')
        //{
        //    url += '&viewparams=IDUSUARIO:' + Criterio;
        //}        

        if (Criterio == '') {
            wcapa = L.tileLayer.wms(url, {
                layers: Capa,
                format: 'image/png',
                transparent: true,
                attribution: "Carseg 2017",
                version: "1.1.0",
                srs: "EPSG:4326",
                request: "GetMap",
                styles: "",
                zIndex: 5000,
                tiled: true
            });
        }
        else {
            wcapa = L.tileLayer.wms(url, {
                layers: Capa,
                format: 'image/png',
                transparent: true,
                attribution: "Carseg 2017",
                version: "1.1.0",
                srs: "EPSG:4326",
                request: "GetMap",
                styles: "",
                zIndex: 5000,
                tiled: true,
                viewparams: "IDUSUARIO:" + String(Criterio)
            });
        }

        wcapa.addTo(map)
        wcapa = null; 1
    }
    catch (Error) { console.log(Error); }
}

function LimpiarMapaAlertas() {
    try {
        for (let i = 0; i <= lmarkersa.length - 1; i++) {
            try {
                $("#chka_" + lmarkersa[i].options.id).attr('checked', false);

                if ((Math.floor(Date.now() / 1000) - lmarkersa[i].options.time > 10)) {
                    map.removeLayer(lmarkersa[i]);
                }
            }
            catch (Error) {
                console.log(Error);

            }
        }
        aalertas = null;
        aalertas = new Array();
    }
    catch (Error) {
        console.log(Error);
    }
}

function LimpiarMapaPuntos(Tipo) {
    try {
        if (Tipo == undefined) {
            Tipo = 0;
        }

        if (Tipo == 2) {            
            lmarkersp.forEach(async (punto) => {
                try {
                    $("#chkp_" + punto.options.id).attr('checked', false);

                    if ((Math.floor(Date.now() / 1000) - punto.options.time > 10)) {
                        map.removeLayer(punto);
                    }
                }
                catch (Error) {
                    console.log(Error);
                }
            });
        }
        else {           
            lmarkersp.forEach(async (punto) => {
                try {
                    $("#chkp_" + punto.options.id).attr('checked', false);

                    if (punto.options.tipo == Tipo) {
                        map.removeLayer(punto);
                    }
                }
                catch (Error) {
                    console.log(Error);
                }
            });
        }

        lmarkersp = null;
        lmarkersp = new Array();
    }
    catch (Error) {
        console.log(Error);
    }
}

function LimpiarMapaGeocercas() {
    try {
        for (let i = 0; i <= lmarkersg.length - 1; i++) {
            try {
                map.removeLayer(lmarkersg[i]);
            }
            catch (Error) {
                console.log(Error);

            }
        }

        lmarkersg = null;
        lmarkersg = new Array();
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

    //console.log(lmarkers);

    for (let i = 0; i <= lmarkers.length - 1; i++) {
        try {
            map.removeLayer(lmarkers[i]);

            if (clusterr) {
                try {
                    lRecorrido.removeLayer(lmarkers[i]);
                }
                catch (Error) {
                    console.log(Error);
                }
            }
        }
        catch (Error) {
            console.log(Error);
        }
    }

    if (clusterr) {
        try {
            lRecorrido.refreshClusters();
        }
        catch (Error) { console.log(Error); }

        try {
            lRecorrido.clearLayers();
        }
        catch (Error) { console.log(Error); }

        //lRecorrido.setMap(null);            
        //map.removeLayer(lRecorrido);
        //lRecorrido = null;
        //lRecorrido = L.markerClusterGroup({
        //    animate: false,
        //    disableClusteringAtZoom: 16
        //});
    }

    lmarkers = null;
    lmarkers = new Array();

    cgeos = null;
    cgeos = new L.featureGroup();

    cgeoseg = null;
    cgeoseg = new L.featureGroup();

    ccapas = 0;
}

function LimpiarMapaBusqueda() {
    ReglaActiva = false;
    Limpiar = true;
    Bounds = null;
    Bounds = new google.maps.LatLngBounds();
    for (let i = 0; i < LayersMapa.length; i++) {
        LayersMapa[i].setMap(null);
    }
    for (var j = 0; j < LayersRuler.length; j++) {
        LayersRuler[j].setMap(null);
    }
    LayersMapa = null;
    LayersMapa = [];
    LayersRuler = null;
    LayersRuler = [];
    CargarCapas('GAL;LIM;PROF');
    $('.contextmenu').remove();
    return false;
}

function street_view1(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    /*String((parseFloat(e.latlng.lng)+0.005))*/
    window.open('https://www.google.com/maps?q&layer=c&cbll=' + lat + ',' + lng + '&cbp=12,0,0,0,0&z=18');
}

function UbicarSV2(latitud, longitud, Rumbo, Actualizar) {
    try {
        if (document.getElementById("txRumbo").value != "") {
            Rumbo = new Number(document.getElementById("txRumbo").value) + 180;
        }
        else {
            Rumbo += 180;
        }

        window.open('https://www.google.com/maps?q&layer=c&cbll=' + latitud + ',' + longitud + '&cbp=12,0,0,0,0&z=18,' + Rumbo + 'h', '', 'width=430,height=650,left=0,top=0,location=no');
        return false;
    }
    catch (Error) {
        console.log(Error);
    }
}

function street_view2(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;

    UbicarSV2(lat, lng, 45, false);

    lat = null;
    lng = null;
}

function googlesatelite(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    map.panTo(e.latlng);
    window.open('https://www.google.com.ec/maps/@' + lat + ',' + lng + ',376m/data=!3m1!1e3', '', 'width=430,height=650,left=0,top=0');
}

function googlehibrido(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    map.panTo(e.latlng);
    window.open('https://www.google.com.ec/maps/@' + lat + ',' + lng + ',376m/data=!3m1!1e3', '', 'width=430,height=650,left=0,top=0');
}

function wazer(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    map.panTo(e.latlng);
    window.open('https://www.waze.com/es-419/livemap?zoom=18&lat=' + lat + '&lon=' + lng + '', '', 'width=800,height=650,left=0,top=0');

}

function mapagoogle1(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    map.panTo(e.latlng);
    window.open('https://www.google.com.pe/maps/@' + lat + ',' + lng + ',18z', '', 'width=430,height=650,left=0,top=0');
}

function clusterclick(e) {
    if (document.getElementById("hdcluster").value == "true") {
        document.getElementById("hdcluster").value = "false";
    }
    else {
        document.getElementById("hdcluster").value = "true";
    }
}

function AbrirPuntosRef() {
    try {
        window.open('PuntoReferencial.aspx', '_blank');
    }
    catch (ex) {
        console.log(ex);
    }
}

function googletrafico(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    window.open('https://www.google.com.pe/maps/@' + lat + ',' + lng + ',18z/data=!5m1!1e1', '', 'width=550,height=650,left=0,top=0');
}

function AbrirAdmAlertas() {
    window.open('AlertasAdministracion.aspx?TIME=' + String(document.getElementById("hdwuid").value), '_blank');
}

function getMenuAlertasDetalle() {
    try {
        var opciones = '';
        opciones = '<div id="dvdetallealertas" title="De un click para Mostrar/Ocultar el Detalle de estas Alertas" style="overflow-y:scroll;display:none;background-color:rgba(255, 255, 255, 0.5);border=1;bordercolor:tgray;border-collapse:separate;border:solid gray 1px;border-radius:4px;-moz-border-radius:4px;height:430px;width: 250px; text-align:center">' +
            '<h5 class="small"><b>&nbsp;&nbsp;DETALLE DE ALERTAS OCURRIDAS&nbsp;&nbsp;</b></h5><hr><ul class="list-group" style="width:230px"></ul>' +
            '</div>';

        return opciones;
    }
    catch (Error) {
        console.log(Error);
    }
}

function MostrarOcultarListadoAlertas() {
    try {
        if (document.getElementById("hdestiloalertas").value == 'V') {
            return false;
        }

        if (document.getElementById("hdmostraralertas").value == '0') {
            return false;
        }

        if (document.getElementById("dvdetallealertas").style.display == '') {
            document.getElementById("dvdetallealertas").style.display = 'none';
            document.getElementById("imgalertasmapa").src = "Images/botones/menu_right/alarm.png";
        }
        else {
            document.getElementById("dvdetallealertas").style.display = '';
            document.getElementById("imgalertasmapa").src = "Images/botones/menu_right/alarmon.png";
        }
    }
    catch (Error) {
        console.log(Error);
    }
}

function getMenuAlertas() {
    try {
        var opciones = '';
        opciones = '<div onclick="MostrarOcultarListadoAlertas();" title="De un click para Mostrar/Ocultar el Listado de Alertas" style="background-color:white;border=1;bordercolor:tgray;border-collapse:separate;border:solid gray 1px;border-radius:4px;-moz-border-radius:4px;width:47px;text-align:center">' +
            //'<span style="color:Red" class="fa fa-exclamation-triangle fa-sm"></span>&nbsp;' +
            '<img id="imgalertasmapa" src="Images/botones/menu_right/alarm.png" alt="Alertes Ocurridas desde el Login" />' +
            '&nbsp;<span style="color:Red" id="spcontador"><b>' + String(alertasLogin) + '</b></span></div>';

        return opciones;
    }
    catch (Error) {
        console.log(Error);
        return '&nbsp;';
    }
}

function getMenuOpciones() {
    try {
        var opciones = '<table style="background-color:white;border=0;bordercolor:tgray;border-collapse:separate;border:solid gray 1px;border-radius:1px;-moz-border-radius:1px;">' +
            '<tr><td><img id="imgFlota" width="26px" onclick="MostrarEstadoFlota();" title="Estado de la Flota" src="Images/botones/menu_top/opc1.png" /></td></tr>';

        if (document.getElementById('hddashboard').value == 1) {
            //opciones += '<tr id="trdashboard"><td><img ID="imgDash" title="DashBoard Flota" onclick="window.open(' + '\'' + 'dashboard/dashboard.html' + '\'' + ',' + '\'' + '_blank' + '\'' + ');" width="25px" src="Images/botones/menu_top/opc8.png" /></td></tr>';
            if (document.getElementById('hdpaisdashboard').value == 'EC') {
                opciones += '<tr id="trdashboard"><td><img ID="imgDash" title="DashBoard Flota" onclick="window.open(' + '\'' + 'http://www.huntermonitoreo.com/GeoApps/DashBoard/dashflota.aspx?o=' + String(document.getElementById('txuid').value) + '&c=W&ncli=' + String(document.getElementById('hdncli').value) + '\'' + ',' + '\'' + '_blank' + '\'' + ');" width="26px" src="Images/botones/menu_top/opc8.png" /></td></tr>';
            }
            else {
                opciones += '<tr id="trdashboard"><td><img ID="imgDash" title="DashBoard Flota" onclick="window.open(' + '\'' + 'http://www.huntermonitoreoperu.com/GeoApps/DashBoard/dashflota.aspx?o=' + String(document.getElementById('txuid').value) + '&c=W&ncli=' + String(document.getElementById('hdncli').value) + '\'' + ',' + '\'' + '_blank' + '\'' + ');" width="26px" src="Images/botones/menu_top/opc8.png" /></td></tr>';
            }
        }

        if (document.getElementById('hdpuntosr').value == 1) {
            opciones += '<tr id="trPuntosReferenciales"><td><img ID="imgPuntos" title="Administracion de Puntos" width="26px" onclick="AbrirPuntoRef();" src="Images/botones/menu_top/opc2.png" /></td></tr>';
        }

        if (document.getElementById('hdgeor').value == 1) {
            opciones += '<tr id="trGeocercas"><td><img ID="imgGeocercas" onclick="AbrirGeocercaAdm();" title="Admministracion de Geocercas" width="26px" src="Images/botones/menu_top/opc3.png" /></td></tr>';
        }

        if (document.getElementById('hdalertasr').value == 1) {
            //opciones += '<tr id="trAlertas"><td><img id="imgAlertas" onclick="AbrirAdmAlertas()" title="Alertas" width="20px" src="Images/botones/menu_top/opc4.png" /></td></tr>';
        }

        if (document.getElementById('hdsmsr').value == 1) {
            //opciones += '<tr id="trCelularesAutorizados"><td><img id="imgSMS" onclick="vnConfigSMSC.Show();" title="Celulares Autorizados" width="20px" src="Images/botones/menu_top/opc5.png" /></td></tr>';
        }
        //opciones += '<tr><td><img id="imgAsistencia" onclick="AbrirAsistencia();" title="Abrir Asistencia" width="20px" src="Images/botones/menu_top/opc6.png" /></td></tr>';

        if (document.getElementById('hdsubsrr').value == 1) {
            opciones += '<tr id="imgSubUsuario"><td><img id="imgSubUsuario" onclick="CrearSubUsuario();" title="Registro de SubUsuarios (Temporales)" width="26px" src="Images/botones/menu_top/opc9.png" /></td></tr>';
        }

        if (document.getElementById('hdsegr').value == 1) {
            //opciones += '<tr id="trSeguimiento"><td><img id="imgSeguimiento" title="Seguimiento" onclick="vnSeguimientoMC.Show();" width="20px" src="Images/botones/menu_top/opc7.png" /></td></tr>';
        }

        if (document.getElementById('hdprogr').value == 1) {
            //opciones += '<tr id="trProgramacion"><td><img id="imgProgramacion" title="Programar Envio de Reportes" onclick="vnProgramacionC.Show();" width="20px" src="Images/botones/menu_top/opc10.png" /></td></tr>';
        }

        if (document.getElementById('hdregr').value == 1) {
            //opciones += '<tr id="trRegistro"><td><img width="20px" id="imgRegistro" title="Registro de Unidades al Usuario" onclick="vnRegistroC.Show();" src="Images/botones/menu_top/opc13.png" /></td></tr>';
        }
        //opciones += '<tr><td>&nbsp;</td></tr>';

        if (document.getElementById('hdpanel').value == 1) {
            //opciones += '<tr><td style="text-align:center"><img id="imgContraer" title="Expandir/Contraer Panel de Reportes" onclick=" $(' + ".leftmenutrigger" + ').click(); " width="20px" src="Images/botones/menu_top/opc12.png" /></td></tr>';
        }
        opciones += '</table>';

        return opciones;
    }
    catch (Error) {
        console.log(Error);
        return "&nbsp;";
    }
}

function initializeMap(ModoMapa) {
    try {
        var menucontext = false;
        var ilayersmapa = {};
        var olayersmapa = {};

        if (document.getElementById('hdlistamapas').value == '') {
            document.getElementById('hdlistamapas').value = 'OS,GM,IOS';
        }

        var listamapas = String(document.getElementById('hdlistamapas').value).split(',');
        var ini = 0;
        var obj = '';
        var lmapa = true;        

        myRenderer = L.canvas({ padding: 0.1 });

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

        document.getElementById("hdcluster").value = "false";
        try {
            if (document.getElementById("hdcluster").value == "true") {
                cachemapa = false;
                clusterr = true;
            }
            else {
                cachemapa = true;
                clusterr = false;
            }
        }
        catch (Error) {
            console.log(Error);
            cachemapa = true;
            clusterr = false;
        }

        if (clusterr) {
            try {
                lRecorrido = L.markerClusterGroup({
                    animate: false,
                    disableClusteringAtZoom: new Number(document.getElementById("hdzoomcluster").value),
                    removeOutsideVisibleBounds: true
                });

                //lRecorrido.on('clustermouseover', function (a) {
                //    console.log(a.layer.getAllChildMarkers());
                //});
            }
            catch (Error) {
                alert(Error);
            }
        }

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

                                //capai = L.tileLayer('//{s}.tile.stamen.com/toner-lite/{z}/{x}/{y}.png', {
                                //    attribution: 'Map tiles by <a href="http://stamen.com">Stamen Design</a>, <a href="http://creativecommons.org/licenses/by/3.0">CC BY 3.0</a> &mdash; Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
                                //    subdomains: 'abcd',
                                //    maxZoom: 19,
                                //    minZoom: 2
                                //});
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
                                    maxZoom: 19,
                                    type: 'roadmap'
                                });

                                var trafficMutant = L.gridLayer.googleMutant({
                                    maxZoom: 19,
                                    type: 'roadmap'
                                });
                                //capai.addGoogleLayer('TrafficLayer');

                                ini += 1;
                                ilayersmapa["Google maps"] = capai;
                            }
                            else {
                                gm = L.gridLayer.googleMutant({
                                    maxZoom: 19,
                                    type: 'roadmap'
                                });

                                var trafficMutant = L.gridLayer.googleMutant({
                                    maxZoom: 19,
                                    type: 'roadmap'
                                });
                                //gm.addGoogleLayer('TrafficLayer');

                                ilayersmapa["Google maps"] = gm;
                            }

                            break;
                        case 'HUN':
                            ncapai = "Hunter";
                            capasadicionales = true;

                            if (ini == 0) {
                                var pretiled = L.tileLayer(document.getElementById("hdservidorpe").value + '/mapa/{z}/{x}/{y}.png', { minZoom: 0, maxZoom: 15, attribution: 'Hunter' });
                                var tiledfly = L.tileLayer(document.getElementById("hdservidorpex").value + '/geoserver/gwc/service/tms/1.0.0/mapabase@EPSG:900913@png/{z}/{x}/{y}.png8', {
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
                                var tiledfly = L.tileLayer(document.getElementById("hdservidorpex").value + '/geoserver/gwc/service/tms/1.0.0/mapabase@EPSG:900913@png/{z}/{x}/{y}.png8', {
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

        map = new L.Map('mapa', {
            layers: [capai],
            //contextmenu: menucontext,
            contextmenuWidth: 190,
            fullscreenControl: false,
            useCache: false,
            updateWhenZooming: false,
            updateWhenIdle: true,
            preferCanvas: true,
            keepBuffer: 5000,
            contextmenuItems: [
                {
                    text: 'Agregar punto referencial',
                    callback: addPoint
                },
                {
                    text: 'Agregar geocerca circular',
                    callback: addGeoC
                },
                '-',
                {
                    text: '<input type="checkbox" onmouseover="setearValorCheck(this)" id="chkBPuntoCercanoC" name="chkBPuntoCercanoC"/>&nbsp;&nbsp;&nbsp;Incluir Puntos al ...'
                },
                {
                    text: 'Buscar unidades cercanas a',
                    callback: searchUnits
                },
                '-',
                {
                    text: 'Mostrar coordenadas',
                    callback: showCoordinates
                },
                {
                    text: 'Centrar mapa aqui',
                    callback: centerMap
                },
                '-',
                {
                    text: 'Zoom in',
                    icon: 'images/zoom-in.png',
                    callback: zoomIn
                },
                {
                    text: 'Zoom out',
                    icon: 'images/zoom-out.png',
                    callback: zoomOut
                }, '-',
                {
                    text: 'Ocultar Menú',
                    callback: hideMenu
                }, '-',
                {
                    text: "Ver/Ocultar Street View",
                    callback: street_view2
                },
                {
                    text: "Waze",
                    callback: wazer
                }, '-',
                {
                    text: "Satelite Google",
                    callback: googlesatelite
                },
                {
                    text: "Tráfico Google",
                    callback: googletrafico
                }]
        });

        if (document.getElementById("hdmotormapa").value == "GM") {
            hib = L.gridLayer.googleMutant({
                maxZoom: 18,
                type: 'hybrid',
                preferCanvas: true
            });

            sat = L.gridLayer.googleMutant({
                maxZoom: 18,
                type: 'satellite',
                preferCanvas: true
            });

            ilayersmapa["Satelital google"] = sat;
            ilayersmapa["Hibrido google"] = hib;
        }
        else {
            hib = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
                attribution: 'Tiles &copy; Esri &mdash; Esri, DeLorme, NAVTEQ, TomTom, Intermap, iPC, USGS, FAO, NPS, NRCAN, GeoBase, Kadaster NL, Ordnance Survey, Esri Japan, METI, Esri China (Hong Kong), and the GIS User Community',
                preferCanvas: true
            });

            sat = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
                attribution: 'Tiles &copy; Esri &mdash; Source: Esri, i-cubed, USDA, USGS, AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community',
                preferCanvas: true
            });

            ilayersmapa["Satelital"] = sat;
            ilayersmapa["Hibrido"] = hib;
        }

        rain = L.tileLayer('https://tile.openweathermap.org/map/precipitation_new/{z}/{x}/{y}.png?appid=66a9ca2cea9d7926e74e780d21e40e76',
            {
                attribution: '&copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors',
                maxZoom: 18,
                preferCanvas: true
            });

        if (document.getElementById("hdpais").value == 'PE') {
            if (capasadicionales) {
                CargarCapasPE();
            }

            if (document.getElementById("hdmotormapa").value == "GM") {
                var roadMutant = L.gridLayer.googleMutant({
                    maxZoom: 18,
                    type: 'roadmap'
                });

                var trafficMutant = L.gridLayer.googleMutant({
                    maxZoom: 18,
                    type: 'roadmap'
                });
                trafficMutant.addGoogleLayer('TrafficLayer');

                if (capasadicionales) {
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
                        "Trafico Google": trafficMutant
                    };
                }
            }
            else {
                if (capasadicionales) {
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
                else {
                    olayersmapa = {
                        "Ninguna": null
                    };
                }
            }
        }
        else {
            if (document.getElementById("hdmotormapa").value == "GM") {
                var roadMutant = L.gridLayer.googleMutant({
                    maxZoom: 18,
                    type: 'roadmap',
                    preferCanvas: true
                });

                var trafficMutant = L.gridLayer.googleMutant({
                    maxZoom: 18,
                    type: 'roadmap',
                    preferCanvas: true
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
                collapsed: true,
                hideSingleBase: true,
                autoZIndex: true
            }).addTo(map);

        try {
            map.addControl(new L.Control.Scale());
        }
        catch (Error) {
            console.log(Error);
        }               

        try {
            L.easyPrint().addTo(map);
        }
        catch (Error) {
            console.log(Error);
        }

        let legend = L.control({ position: 'bottomright' });

        legend.onAdd = function (map) {
            var div = L.DomUtil.create('ul', 'list-group-flush'),
                grades = [0, 50, 70, 99],
                labels = [],
                from, to;

            for (var i = 0; i < grades.length; i++) {
                from = grades[i];
                to = grades[i + 1];

                labels.push(
                    '<li class="list-group-item" style="background:' + getColor(from + 1) + '; font-family:' + Fuente + '; font-size:11px; color:black; font-width:bold"></li> ' +
                    (from + 1) + (to ? '&ndash;' + to : '+ km/H '));
            }
            div.title = "Colores segun la velocidad (km/H)";
            div.innerHTML = labels.join('<br>');
            return div;
        };

        try {
            legend.addTo(map);
        }
        catch (Error) {
            console.log(Error);
        }

        try {
            let bpuntos = true;
            let bgeos = true;

            if (document.getElementById("hdgeor").value == 0) {
                bgeos = false
            }

            if (document.getElementById("hdpuntosr").value == 0) {
                bpuntos = false
            }

            drawnItems = new L.FeatureGroup();
            map.addLayer(drawnItems);
            var drawControl = new L.Control.Draw({
                position: 'topright',
                draw: {                  
                    polygon: bgeos,
                    polyline: bgeos,
                    circle: bgeos,
                    rectangle: false,
                    marker: bpuntos
                },
                edit: {
                    featureGroup: drawnItems,
                    remove: false,
                    edit: false
                }
            });
            map.addControl(drawControl);            
            map.on('draw:created', function (e) {
                var type = e.layerType,
                    layer = e.layer;

                //polyline; polygon; rectangle; circle; marker
                if (type === 'marker') {
                    if (window.confirm("Desea agregar este nuevo Punto al mapa ?")) {
                        let pnombre = window.prompt("Ingrese el nombre del punto");
                        if (pnombre == '' || pnombre == undefined) {
                            alert('Ingreso de punto referencial cancelado');
                            map.removeLayer(layer);
                            return false;
                        }

                        data.setpunto(0,
                            layer.getLatLng().lat,
                            layer.getLatLng().lng,
                            pnombre,
                            '',
                            '',
                            $("#idusuario").val(),
                            $("#idsubusuario").val(),
                            0,
                            0,
                            0,
                            'ING',
                            apuntos.length
                        ).then(function (data) {
                            try {
                                if (data.d.includes('OK')) {
                                    let index = data.d.split("_");
                                    try {
                                        $("#ulpuntos").append(getlipuntos(index[2], index[3], index[4], index[1], ''));
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }                                   
                                    index = null;

                                    alert("Punto ingresado con Exito");

                                    $("#sptotalpuntos").text(apuntos.length);
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

                        data.setgeocercaL(0,
                            $("#idusuario").val(),
                            $("#idsubusuario").val(),
                            layer._latlngs,
                            pgeocercal,
                            new String(anchol).replace(",", "."),
                            0
                        ).then(function (data) {
                            try {
                                if (data.d.includes('OK')) {
                                    let index = data.d.split("_");
                                    alert('Geocerca lineal creada con exito');
                                    ageocercas.push(data.d);
                                    precargargeocercas();
                                    index = null;

                                    let infoGeoG = data.d.split("_");
                                    try {
                                        $("#ulgeocercas").append(getligeocercas(infoGeoG[0], infoGeoG[3], infoGeoG[1], '', infoGeoG[4], infoGeoG[7]));
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                    try {
                                        localStorage.setItem(infoGeoG[0], infoGeoG[1]);
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                    infoGeoG = null;

                                    try {
                                        $('#ulgeocercas').empty();
                                        infoGeoG = null;
                                        ageocercas.forEach(async (igeo) => {
                                            try {
                                                infoGeoG = igeo.split("_");

                                                try {
                                                    $("#ulgeocercas").append(getligeocercas(infoGeoG[0], infoGeoG[3], infoGeoG[1], '', infoGeoG[4], infoGeoG[7]));
                                                }
                                                catch (Error) {
                                                    console.log(Error);
                                                }

                                                try {
                                                    localStorage.setItem(infoGeoG[0], infoGeoG[1]);
                                                }
                                                catch (Error) {
                                                    console.log(Error);
                                                }
                                            }
                                            catch (Error) {
                                                console.log(Error);
                                            }
                                        });
                                        infoGeoG = null;

                                        $("#sptotalgeocercas").text(ageocercas.length);
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
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

                if (type === 'polygon') {
                    if (window.confirm("Desea agregar la geocerca poligonal al mapa ?")) {
                        let pgeocercap = window.prompt("Ingrese el nombre de la geocerca poligonal");
                        if (pgeocercap == '' || pgeocercap == undefined) {
                            alert('Ingreso de Geocerca lineal cancelado');
                            map.removeLayer(layer);
                            return false;
                        }                        

                        data.setgeocercaP(0,
                            $("#idusuario").val(),
                            $("#idsubusuario").val(),
                            layer._latlngs[0],
                            pgeocercap,
                            0,
                            0
                        ).then(function (data) {
                            try {
                                if (data.d.includes('OK')) {
                                    let index = data.d.split("_");
                                    alert('Geocerca poligonal creada con exito');
                                    ageocercas.push(data.d);
                                    precargargeocercas();
                                    index = null;

                                    let infoGeoG = data.d.split("_");
                                    try {
                                        $("#ulgeocercas").append(getligeocercas(infoGeoG[0], infoGeoG[3], infoGeoG[1], '', infoGeoG[4], infoGeoG[7]));
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                    try {
                                        localStorage.setItem(infoGeoG[0], infoGeoG[1]);
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                    infoGeoG = null;

                                    try {
                                        $('#ulgeocercas').empty();
                                        infoGeoG = null;
                                        ageocercas.forEach(async (igeo) => {
                                            try {
                                                infoGeoG = igeo.split("_");

                                                try {
                                                    $("#ulgeocercas").append(getligeocercas(infoGeoG[0], infoGeoG[3], infoGeoG[1], '', infoGeoG[4], infoGeoG[7]));
                                                }
                                                catch (Error) {
                                                    console.log(Error);
                                                }

                                                try {
                                                    localStorage.setItem(infoGeoG[0], infoGeoG[1]);
                                                }
                                                catch (Error) {
                                                    console.log(Error);
                                                }
                                            }
                                            catch (Error) {
                                                console.log(Error);
                                            }
                                        });
                                        infoGeoG = null;

                                        $("#sptotalgeocercas").text(ageocercas.length);
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
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
                            map.removeLayer(layer);
                            return false;
                        }

                        data.setgeocercaC(0,
                            $("#idusuario").val(),
                            $("#idsubusuario").val(),
                            new String(layer._latlng.lat).replace(",", "."),
                            new String(layer._latlng.lng).replace(",", "."),
                            pgeocercac,
                            new String(layer._mRadius).replace(",", "."),
                            0
                        ).then(function (data) {
                            try {
                                if (data.d.includes('OK')) {
                                    let index = data.d.split("_");
                                    alert('Geocerca circular creada con exito');
                                    ageocercas.push(data.d);
                                    precargargeocercas();
                                    index = null;
                                    let infoGeoG = data.d.split("_");
                                    try {
                                        $("#ulgeocercas").append(getligeocercas(infoGeoG[0], infoGeoG[3], infoGeoG[1], '', infoGeoG[4], infoGeoG[7]));
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                    try {
                                        localStorage.setItem(infoGeoG[0], infoGeoG[1]);
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
                                    infoGeoG = null;

                                    try {
                                        $('#ulgeocercas').empty();
                                        infoGeoG = null;
                                        ageocercas.forEach(async (igeo) => {
                                            try {
                                                infoGeoG = igeo.split("_");

                                                try {
                                                    $("#ulgeocercas").append(getligeocercas(infoGeoG[0], infoGeoG[3], infoGeoG[1], '', infoGeoG[4], infoGeoG[7]));
                                                }
                                                catch (Error) {
                                                    console.log(Error);
                                                }

                                                try {
                                                    localStorage.setItem(infoGeoG[0], infoGeoG[1]);
                                                }
                                                catch (Error) {
                                                    console.log(Error);
                                                }
                                            }
                                            catch (Error) {
                                                console.log(Error);
                                            }
                                        });
                                        infoGeoG = null;

                                        $("#sptotalgeocercas").text(ageocercas.length);
                                    }
                                    catch (Error) {
                                        console.log(Error);
                                    }
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

                //drawnItems.addLayer(layer);
                if (type === 'marker') {
                    lmarkersp.push(layer);
                }
                else {
                    cgeos.addLayer(layer);
                    lmarkersg.push(layer);
                }
            });
        }
        catch (Error) {
            console.log(Error);
        }

        try {         
            controlbus = new L.Control.Geocoder({
                geocoder: L.Control.Geocoder.google({
                    apiKey: 'AIzaSyCCM3rLAORo5veOQEVx78sWjRBH8ylKyNY'
                }),
                defaultMarkGeocode: true,
                position: 'bottomleft',
                placeholder: 'ej busqueda: Ciudad, Calle',
            }).addTo(map);
        }
        catch (Error) { console.log(Error); }

        $(".leaflet-geonames-icon").title = "Busqueda por Ciudad, Locacion o Coordenadas";

        let measureControl = new L.Control.Measure(
            {
                position: 'bottomleft',
                primaryLengthUnit: 'kilometers',
                secondaryLengthUnit: 'mi',
                localization: 'es'
            });
        measureControl.addTo(map);

        L.control.custom({
            position: 'topleft',
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

        //L.control.custom({
        //    position: 'topright',
        //    content: getMenuAlertas(),
        //    classes: 'btn-group-vertical btn-group-sm',
        //    style:
        //    {
        //        margin: '11px',
        //        padding: '0px 0 0 0',
        //        cursor: 'pointer'
        //    },
        //    datas:
        //    {
        //        'foo': 'bar'
        //    },
        //    events:
        //    {
        //        click: function (data) {
        //            console.log('wrapper div element clicked');
        //            console.log(data);
        //        },
        //        dblclick: function (data) {
        //            console.log('wrapper div element dblclicked');
        //            console.log(data);
        //        },
        //        contextmenu: function (data) {
        //            console.log('wrapper div element contextmenu');
        //            console.log(data);
        //        },
        //    }
        //}).addTo(map);

        //L.control.custom({
        //    position: 'topright',
        //    content: getMenuAlertasDetalle(),
        //    classes: 'btn-group-vertical btn-group-sm',
        //    style:
        //    {
        //        margin: '11px',
        //        padding: '0px 0 0 0',
        //        cursor: 'pointer'
        //    },
        //    datas:
        //    {
        //        'foo': 'bar'
        //    },
        //    events:
        //    {
        //        click: function (data) {
        //            console.log('wrapper div element clicked');
        //            console.log(data);
        //        },
        //        dblclick: function (data) {
        //            console.log('wrapper div element dblclicked');
        //            console.log(data);
        //        },
        //        contextmenu: function (data) {
        //            console.log('wrapper div element contextmenu');
        //            console.log(data);
        //        },
        //    }
        //}).addTo(map);

        L.control.custom({
            position: 'topright',
            content: getInfoWindow(),
            classes: 'btn-group-horizontal btn-group-sm',
            style:
            {
                margin: '53px',
                padding: '0px 0 0 0',
                cursor: 'pointer'
            },
            datas:
            {
                'foo': 'bar'
            },
            events:
            {
                click: function (data) {                 
                    console.log(data);
                },
                dblclick: function (data) {
                    console.log(data);
                },
                contextmenu: function (data) {
                    console.log(data);
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
                map.on('click', function (e) {
                    //addMarcadorPuntoReferencial(document.getElementById("txNombrePunto").value, Datos[1], Datos[2], 17, 1, String(Datos[5]), 0, 1);
                    try {
                        LimpiarMapa();
                        document.getElementById("txLatitud").value = e.latlng.lat;
                        document.getElementById("txLongitud").value = e.latlng.lng;

                        addMarcadorPuntoReferencial(document.getElementById("txNombrePunto").value, e.latlng.lat, e.latlng.lng, map.getZoom(), 1, document.getElementById('hdcolor').value, 0, 1);
                    } catch (e) {
                        console.log(Error);
                    }
                });
                break;
            case 'RR':
                break;
        }

        GestionarMapa();
        if (document.getElementById("hdpais").value == 'PE') {
            map.on('baselayerchange', function (e) {
                //try {
                //if (listamapas.length > 1 && lmapa == false) {
                //    map.removeLayer(capai);
                //}
                //}
                //catch (Error) {
                //    console.log(Error);
                //}

                try {
                    if (cgeos.getLayers().length == 0) {
                        if (document.getElementById("hdmotormapa").value != "GM") {
                            map.setView([LatitudInicial, LongitudInicial], 5, {
                                reset: true
                            });
                        }
                    }
                    else {
                        map.fitBounds(cgeos.getBounds());
                    }
                }
                catch (Error) { console.log(Error); }
            });
        }
        else {
            map.on('baselayerchange', function (e) {
                try {
                    if (listamapas.length > 1 && lmapa == false) {
                        map.removeLayer(capai);
                    }
                }
                catch (Error) {
                    console.log(Error);
                }
            });
        }


        if (aunidades.length == 1) {
            map.setView([LatitudInicial, LongitudInicial], 14, {
                reset: true
            });
        }
        else {
            map.setView([LatitudInicial, LongitudInicial], 6, {
                reset: true
            });
        }

        //drawnItems = new L.FeatureGroup();
        //map.addLayer(drawnItems);
        //try {
        //    var drawControl = new L.Control.Draw({
        //        position: 'topright',
        //        draw: {
        //            polyline: {
        //                metric: true,
        //                shapeOptions: {
        //                    color: '#0000ff',
        //                    weight: 10
        //                }
        //            },
        //            polygon: {
        //                allowIntersection: false,
        //                showArea: true,
        //                drawError: {
        //                    color: '#b00b00',
        //                    timeout: 1000
        //                },
        //                shapeOptions: {
        //                    color: '#ff0000'
        //                }
        //            },
        //            circle: {
        //                shapeOptions: {
        //                    color: '#000000'
        //                }
        //            },
        //            marker: {
        //                icon: new cIcono({ iconUrl: 'images/marker-icon.png', iconSize: [16, 26] })
        //            },
        //            rectangle: {
        //                shapeOptions: {
        //                    color: '#00ff30'
        //                }
        //            }
        //        },
        //        edit: {
        //            featureGroup: drawnItems,
        //            remove: false,
        //            edit: false
        //        }
        //    });
        //    map.addControl(drawControl);
        //}
        //catch (Error) { console.log(Error); }

        map.invalidateSize();
        lmapa = false;
    }
    catch (Error) { console.log(Error); }
}

function showContextMenu(caurrentLatLng) {
    return false;
    var projection, contextmenuDir;
    projection = map.getProjection();
    document.getElementById('hddistancia').value = '';
    $('.contextmenu').remove();
    contextmenuDir = document.createElement("div");
    contextmenuDir.className = 'contextmenu';
    //'<a id="menu1"><div onclick="LimpiarMapa();">limpiar mapa<\/div><\/a>'

    if (document.getElementById("idperfil").value == "4") {
        contextmenuDir.innerHTML = '<a id="menu2"><div onclick="AgregarPuntoReferencial();">Agregar punto referencial<\/div><\/a>'
            + '<a id="menu9"><div onclick="AgregarGeocercaC();">Agregar geocerca (circular)<\/div><\/a>'
            //+'<a id="menu3"><div onclick="zoomMapa(true);">Zoom In</div></a>'
            //+'<a id="menu4"><div onclick="zoomMapa(false);">Zoom Out</div></a>'
            + '<a id="menu5"><div onclick="EsconderMenu();">Ocultar Menu</div></a>'
            + '<a id="menu7"><div>Unidades Cercanas a &nbsp;<input id="txDistancia" value="2" size=2 style="width:20px; height:16px; font-size: x-small" onchange="AsignarValor(this.value);"></input>&nbsp;Km(s)&nbsp;<input style="height=18;font-size: x-small" type="button" value="buscar" onclick="BuscarCercanos(1);"></input></div></a>'
        //+ '<a id="menu8"><div onclick="BuscarCercanos(0);">Buscar Puntos Cercanos</div></a>';
    }
    else {
        contextmenuDir.innerHTML = '<a id="menu3"><div onclick="zoomMapa(true);">Zoom In</div></a>'
            + '<a id="menu4"><div onclick="zoomMapa(false);">Zoom Out</div></a>'
            + '<a id="menu5"><div onclick="EsconderMenu();">Ocultar Menu</div></a>'
            + '<a id="menu7"><div>Unidades Cercanas a &nbsp;<input id="txDistancia" value="2" size=2 style="width:20px; height:16px; font-size: x-small" onchange="AsignarValor(this.value);"></input>&nbsp;Km(s)&nbsp;<input style="height=18;font-size: x-small" type="button" value="buscar" onclick="BuscarCercanos(1);"></input></div></a>';
    }

    $(map.getDiv()).append(contextmenuDir);
    setMenuXY(caurrentLatLng);
    contextmenuDir.style.visibility = "visible";
    setTimeout("$('.contextmenu').remove()", 15000);
}

function AsignarValor(Valor) { document.getElementById('hddistancia').value = Valor; }

function BuscarCercanos(SonUnidades, Mensaje, Placa) {
    var SinReportar = 0;
    if (Mensaje == 0) {
        SinReportar = 1;
    }
    if (Mensaje == 1) {
        LimpiarMapa();
    }
    if (SonUnidades) {
        datos.cargar("", SinReportar).then(function (data) {
            var Data, Total, j, jp;
            Total = 0;
            j = 0;
            for (j = 0; j <= (data.d.length - 1); j++) {
                try {
                    Data = null;
                    Data = data.d[j].split(';');
                    //addMarcadoresGoogle(Vid,Alias,Lat,Lon,Vel,Rumbo,Kil,Fecha,Punto,Calle,Zoom,Tipo, EstadoIgnicion,Evento,Entradas,Icono,Conductor,Temperatura,Bateria,NivelGasolinaOBD, TiempoDetenido,Horometro,NivelBateria,VoltajeBateria,VoltajeAlimentacioN,EA2,EO){

                    document.getElementById('hdcercanos').value += String(Data[2]) + ';';

                    if (Placa == '') {
                        addMarcadorGoogle(Data[0], Data[2], Data[6], Data[7], Data[5], Data[4], Data[8], Data[11], Data[10], Data[9], 10, 0, Data[3], Data[15], '', Data[55], '', Data[42], Data[51], Data[48], Data[49], Data[50], Data[51], Data[52], Data[53], Data[43], Data[54]);
                    }
                    else {
                        if (Placa != Data[2]) {
                            addMarcadorGoogle(Data[0], Data[2], Data[6], Data[7], Data[5], Data[4], Data[8], Data[11], Data[10], Data[9], 10, 0, Data[3], Data[15], '', Data[55], '', Data[42], Data[51], Data[48], Data[49], Data[50], Data[51], Data[52], Data[53], Data[43], Data[54]);
                        }
                    }
                    Total += 1;
                    Data = null;
                }
                catch (Error) { console.log(Error); }
            }

            if (Mensaje == 1) {
                if (j == 0) { EnviarMSJ('<b>No Existen Unidades Cercanas al Punto Ubicado.</b> Solo se toman en cuenta unidades cuya ultimo reporte es el dia de hoy'); }
                else { EnviarMSJ('<b>Se encontró ' + String(j) + ' unidad(es) cercana(s) al punto seleccionado</b>'); }
            }

            if (document.getElementById("chkBPuntoCercanoC").checked) {
                try {
                    datos.cargarp("", SinReportar).then(function (data) {
                        var Datap, Totalp, jp;
                        Totalp = 0;
                        jp = 0;
                        console.log(data);
                        for (jp = 0; jp <= (data.d.length - 1); jp++) {
                            try {
                                Datap = null;
                                Datap = data.d[jp].split(';');

                                document.getElementById('hdcercanosp').value += String(Datap[1]) + ';';

                                addMarcadorPuntoReferencial(Datap[1], Datap[2], Datap[3], (document.getElementById("txZoom").value == '' ? 13 : document.getElementById("txZoom").value), 1, 0, 0, 0);
                            }
                            catch (Error) {
                                console.log(Error);
                            }
                        }
                    });
                }
                catch (Error) {
                    console.log(Error);
                }
            }
        });
        return false;
    }
    else {
        //alert(SonUnidades);
    }
}
datos = {
    cargar: function (Tipo, SinReportar) {
        var dfr = $.Deferred();
        var Distancia = document.getElementById('hddistancia').value;
        var Latitud = document.getElementById('txLatitudMapa').value;
        var Longitud = document.getElementById('txLongitudMapa').value;
        var IdUsuario = document.getElementById('idusuario').value;
        if (isNaN(Distancia)) { Distancia = 2; }
        $.ajax({
            type: "POST",
            url: '../Servicios/Busquedas.asmx/ObtenerUnidadesCercanas',
            data: "{'IdUsuario':'" + IdUsuario + "','Latitud':'" + Latitud + "','Longitud':'" + Longitud + "','Distancia':'" + String((new Number(Distancia) * 1000.00)) + "','SinReportar':'" + String(SinReportar) + "'}",
            contentType: "application/json; utf-8",
            dataType: "json",
            ajaxSend: function () {
                $('#response_login').html('<div class="centered"><img src="images/loading.gif" /></div>');
            },
            success: dfr.resolve,
            error: function (response) { alert(response); },
            faliure: function (response) { alert(response); }
        });
        return dfr.promise();
    },
    cargarp: function (Tipo, SinReportar) {
        var dfr = $.Deferred();
        var Distancia = document.getElementById('hddistancia').value;
        var Latitud = document.getElementById('txLatitudMapa').value;
        var Longitud = document.getElementById('txLongitudMapa').value;
        var IdUsuario = document.getElementById('idusuario').value;
        if (isNaN(Distancia)) { Distancia = 2; }
        $.ajax({
            type: "POST",
            url: '../Servicios/Busquedas.asmx/ObtenerPuntosCercanos',
            data: "{'IdUsuario':'" + IdUsuario + "','Latitud':'" + Latitud + "','Longitud':'" + Longitud + "','Distancia':'" + String((new Number(Distancia) * 1000.00)) + "','SinReportar':'" + String(SinReportar) + "'}",
            contentType: "application/json; utf-8",
            dataType: "json",
            ajaxSend: function () {
                $('#response_login').html('<div class="centered"><img src="images/loading.gif" /></div>');
            },
            success: dfr.resolve,
            error: function (response) { alert(response); },
            faliure: function (response) { alert(response); }
        });
        return dfr.promise();
    }
}

function EsconderMenu() { $('.contextmenu').remove(); }

function zoomMapa(IN) {
    map.setCenter(new google.maps.LatLng(document.getElementById("txLatitudMapa").value, document.getElementById("txLongitudMapa").value));
    if (IN) { map.setZoom(map.getZoom() + 2); }
    else { map.setZoom(map.getZoom() - 2); }
    $(".contextmenu").remove();
}

function getCanvasXY(caurrentLatLng) {
    var scale = Math.pow(2, map.getZoom());
    var nw = new google.maps.LatLng(
        map.getBounds().getNorthEast().lat(),
        map.getBounds().getSouthWest().lng()
    );
    var worldCoordinateNW = map.getProjection().fromLatLngToPoint(nw);
    var worldCoordinate = map.getProjection().fromLatLngToPoint(caurrentLatLng);
    var caurrentLatLngOffset = new google.maps.Point(
        Math.floor((worldCoordinate.x - worldCoordinateNW.x) * scale),
        Math.floor((worldCoordinate.y - worldCoordinateNW.y) * scale)
    );
    return caurrentLatLngOffset;
}

function setMenuXY(caurrentLatLng) {
    var mapWidth = $('#map_canvas').width();
    var mapHeight = $('#map_canvas').height();
    var menuWidth = $('.contextmenu').width();
    var menuHeight = $('.contextmenu').height();
    var clickedPosition = getCanvasXY(caurrentLatLng);
    var x = clickedPosition.x;
    var y = clickedPosition.y;
    if ((mapWidth - x) < menuWidth)
        x = x - menuWidth;
    if ((mapHeight - y) < menuHeight)
        y = y - menuHeight;
    $('.contextmenu').css('left', x);
    $('.contextmenu').css('top', y);
}

function AgregarPuntoReferencial() {
    try {
        if (window.confirm('Desea Agregar un Punto Referencial en el Lugar donde dio Click en el Mapa') == true) {
            vnPuntosReferencialRC.SetContentUrl('PuntoReferencialR.aspx?LAT=' + String(document.getElementById("txLatitudMapa").value) + '&LON=' + String(document.getElementById("txLongitudMapa").value) + '&T=P');
            vnPuntosReferencialRC.Show();
        }
        $('.contextmenu').remove();
    }
    catch (Error) { }
}

function AgregarGeocercaC() {
    try {
        if (window.confirm('Desea Agregar una Geocerca Circular en el Lugar donde dio Click en el Mapa') == true) {
            vnPuntosReferencialRC.SetContentUrl('PuntoReferencialR.aspx?LAT=' + String(document.getElementById("txLatitudMapa").value) + '&LON=' + String(document.getElementById("txLongitudMapa").value) + '&T=G');
            vnPuntosReferencialRC.Show();
        }
        $('.contextmenu').remove();
    }
    catch (Error) { }
}

async function MostrarGeocercaMapa(Id, Tipo, Datos, Parametro, Centro, Nombre, Fit, addli, check) {
    var IngfoGeo = Datos.split(';');
    var InfoPunto, Ind, Poly, Line, Circle, InfoCentro;
    var PuntosGeo = [];
    var limite = 1;
    //var colorgeocerca = gcolors[Math.floor(Math.random() * gcolors.length)];
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
                cgeos.addLayer(Line);
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
                    cgeos.addLayer(Circle);
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
                    cgeos.addLayer(Poly);
                    lmarkersg.push(Poly);
                }
            }
        }
        else {
            if (Tipo == '3') {
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
                cgeos.addLayer(Circle);
                lmarkersg.push(Circle);
            }
        }
        if (Fit) {
            try { map.fitBounds(cgeos.getBounds()); } catch (Error) { console.lo(Error); }
        }
    }
    catch (Error) {
        console.log(Error);
    }
}

function showAddress(response) {
    try { Direccion = ''; Direccion = response.Placemark[0].address; }
    catch (Error) { Direccion = "S/N"; }
}

function addMarcadorPuntoReferencial(id, Punto, Lat, Lon, Zoom, Tipo, Color, Alerta, Limpiar, addli, check) {
    let MarcadorPunto, Texto = '';
    let indp = -1;

    if (lmarkersp.length > 0) {
        indp = lmarkersp.findIndex(punto => punto.options.id == id);
        if (indp != -1) {
            return false;
        }
    }

    if (Limpiar == 1) {
        LimpiarMapa();
        map.setView(new L.latLng(0, 0));
        map.panTo(new L.latLng(0, 0));
    }       

    if (Alerta == 1) {
        Texto += '<div align="left"><font color="#000000"><table class="style2000"><tr><td colspan="2" class="style2000"><hr width="200px"></td></tr>';
        Texto += '<tr><td class="style2000" colspan="2">' + Punto + '</td></tr>';
    }
    else {
        Texto += '<div align="left"><font color="#000000"><table class="style2000">';
        Texto += '<tr><td class="style2000b">Punto: </td><td class="style2000"><b>' + Punto + '</b></td></tr>';
    }

    if (document.getElementById("txMostrarCoordenadas").value == 'True') {
        Texto += '<tr><td class="style2000b"> Lat y Long: </td><td class="style2000b">' + getCoordenadas(document.getElementById("txUnidadCoordenadas").value, Lat, Lon) + '</td></tr>';
    }
    Icon = null;

    if (Alerta == 1) {
        Imagen = 'Images/icoAlerta.png';
    }
    else {
        try {
            //Imagen = 'Images/icoPuntosReferencialesD' + String(Color) + '.png';
            Imagen = 'Images/infowin/gps' + String(Color) + '.png';
        }
        catch (Error) {
            //Imagen = 'Images/icoPuntosReferencialesD.png';
            Imagen = 'Images/infowin/gps.png'
        }
        //map.setView(new L.latLng(Lat, Lon));
    }

    Icon = null;
    Icon = new LeafIcon({
        iconUrl: Imagen,
        shadowSize: [0, 0]
    });

    MarcadorPunto = L.marker(new L.latLng(Lat, Lon),
        {
            icon: Icon,
            id: String(id),
            placa: String(Punto),
            renderer: myRenderer,
            setOpacity: 0.01,
            iconSize: new L.Point(17, 17),
            tipo: Tipo,
            time: Math.floor(Date.now() / 1000),
            tooltipAnchor: new L.Point(10, 0),
            pmIgnore: true
        });

    if (addli) {
        try {
            $("#ulpuntos").append(getlipuntos(Punto, Lat, Lon, id, check));
        }
        catch (Error) {
            console.log(Error);
        }
    }

    //MarcadorPunto.alt = String(Punto);
    //MarcadorPunto.bindPopup(Texto, { maxWidth: "544px" }).addTo(map);
    try {
        try {
            if (Alerta == 1) {
                MarcadorPunto.bindTooltip(Punto, {
                    permanent: false,
                    className: 'leaflet-tooltip-alertas'
                });
                MarcadorPunto.openTooltip();
            }
            else {
                MarcadorPunto.bindTooltip(Punto, {
                    permanent: true,
                    className: 'leaflet-tooltip-puntos'
                });
                MarcadorPunto.openTooltip();
            }
        }
        catch (Error) { console.log(Error); }
    }
    catch (Error) { console.log(Error); }

    if (Tipo == 1) {
        lmarkersa.push(MarcadorPunto);
    }
    else {
        lmarkersp.push(MarcadorPunto);
    }

    try { cgeos.addLayer(MarcadorPunto); } catch (Error) { console.log(Error); }

    try {
        MarcadorPunto.addTo(map);
    }
    catch (Error) {
        MarcadorPunto.addTo(map);
    }

    if (Limpiar == 1) {
        map.setView(new L.latLng(Lat, Lon));
        map.panTo(new L.latLng(Lat, Lon));
        map.setZoom(15);
    }
    else {
        if (Alerta == 0) {
            //try { map.fitBounds(cgeos.getBounds()); } catch (Error) { console.log(Error); }
        }
    }
}

function convertMinute(valor, unidad) {
    let evalor;

    switch (unidad) {
        case 'minute':
            evalor = 1;
            break;
        case 'minutes':
            evalor = 1;
            break;
        case 'hour':
            evalor = 60;
            break;
        case 'hours':
            evalor = 60;
            break;
        case 'day':
            evalor = 1440;
            break;
        case 'days':
            evalor = 1440;
            break;
        case 'week':
            evalor = 10080;
            break;
        case 'weeks':
            evalor = 10080;
            break;
        case 'month':
            evalor = 262980;
            break;
        case 'months':
            evalor = 262980;
            break;
        case 'year':
            evalor = 525960;
            break;
        case 'years':
            evalor = 525960;
            break;
        default:
            evalor = 1;
    }

    return (evalor * valor);
}

function addMarcadorGoogle(Vid, Alias, Lat, Lon, Vel, Rumbo, Kil, Fecha, Punto, Zoom, Tipo, EstadoIgnicion, Evento, Entradas, Calle, Icono, Conductor, Temperatura, Bateria, NivelGasolinaOBD, TiempoDetenido, Horometro, NivelBateria, VoltajeBateria, VoltajeAlimentacion, EA2, EO, TipoVehiculo, Propietario, TipoSA1, OBD, addli, ulnombre, zona, SA1, SA2, SA3) {
    let Texto = '';
    let Marcador, subMarcador, obdMarcador;
    let m; let vel = '';
    let Imagen = '';
    let subImagen = '';
    let tdiff, tdiffm, tdiffmt, control;

    if (ulnombre == undefined) {
        ulnombre = '';
    }

    var now = new Date().toLocaleDateString() + ' ' + new Date().toLocaleTimeString();
    var pFecha, phFecha;
    var pnFecha, pnhFecha;

    pFecha = String(Fecha).split(' ');
    phFecha = pFecha[0].split('/');

    pnFecha = String(now).split(' ');
    pnhFecha = pnFecha[0].split('/');

    try {
        tdiff = moment.utc(moment(pnhFecha[1] + '/' + pnhFecha[0] + '/' + pnhFecha[2] + ' ' + new Date().toLocaleTimeString(), "DD/MM/YYYY HH:mm:ss").diff(moment(Fecha, "DD/MM/YYYY HH:mm:ss"))).format("HH:mm");
        tdiffm = moment(Fecha, "DD/MM/YYYY HH:mm:ss").fromNow().replace(' ago', '').replace("an", "1").replace("a few", "1").split(' ');
        tidffmt = convertMinute(tdiffm[0], tdiffm[1]);
    }
    catch (Error) {
        console.log(Error);
    }

    if (String(Vel) == '0') { v = ''; }
    else { v = 'm'; }
    try {
        if (Vid != document.getElementById("txVID").value && Vid == '') {
            Vid = document.getElementById("txVID").value;
        }
        if (Vid == 'Sin Datos') { return false; }
    }
    catch (Error) { console.log(Error); }
    try { PtoCercano = Punto.split('*'); }
    catch (Error) { PtoCercano = ''; }

    if (Calle == 'undefined' || Calle == 'UNDEFINED') {
        Calle = 'N/D';
    }

    try {
        if (Icono == null) { Icono = ''; }
        if (Tipo == 1) {
            Imagen = "Images/icoPuntosReferencialesD.png";
        }
        else {
            if (Icono == '') {
                if (new Number(tidffmt) > 60 && document.getElementById('hdsubicono').value == "1") {
                    subImagen = 'Images/map/radio.png';
                }

                if (GetCourse(Rumbo) == '') { vel = 'STOP'; }
                else {
                    if (String(Vel) == '0' || String(Vel) == '0.0E0') {
                        vel = 'STOP';
                    }
                    else {
                        vel = 'v';
                    }
                }
                if (Icono == null) { Icono = ''; }
                if (Icono == '') {
                    if (vel == 'STOP' || vel == '' && (String(Vel) == '0' || String(Vel) == '0.0E0')) {
                        if (vel == '') {
                            vel = 'STOP';
                        }
                        Imagen = "Images/map/" + vel + ".png";
                    }
                    else {
                        var xvel = getVel(new Number(Vel));
                        switch (true) {
                            case (xvel <= 50):
                                Imagen = "Images/map/" + vel + GetCourse(Rumbo) + ".png";
                                break;
                            case (xvel >= 51 && xvel <= 70):
                                Imagen = "Images/map/a" + GetCourse(Rumbo) + ".png";
                                break;
                            case (xvel >= 71 && xvel <= 100):
                                Imagen = "Images/map/o" + GetCourse(Rumbo) + ".png";
                                break;
                            case (xvel >= 101):
                                Imagen = "Images/map/" + GetCourse(Rumbo) + ".png";
                                break;
                            default:
                                Imagen = "Images/map/" + vel + GetCourse(Rumbo) + ".png";
                                break;
                        }
                        xvel = null;
                    }
                }
                else {
                    Imagen = Icono;
                }
            }
            else {
                Imagen = Icono;

                if (vel == 'STOP' || v == '') {
                    if (vel == '') {
                        vel = 'STOP';
                    }

                    if (document.getElementById('hdsubicono').value == "1" && document.getElementById('hdsubicono').value == "1") {
                        subImagen = "Images/map/" + vel + ".png";
                    }
                }
                else {
                    switch (true) {
                        case (xvel <= 50):
                            subImagen = "Images/map/" + vel + Rumbo + ".png";
                            break;
                        case (xvel >= 51 && xvel <= 70):
                            subImagen = "Images/map/a" + Rumbo + ".png";
                            break;
                        case (xvel >= 71 && xvel <= 100):
                            subImagen = "Images/map/o" + Rumbo + ".png";
                            break;
                        case (xvel >= 101):
                            subImagen = "Images/map/" + Rumbo + ".png";
                            break;
                        default:
                            subImagen = "Images/map/" + vel + Rumbo + ".png";
                            break;
                    }
                    xvels = null;

                    if (document.getElementById('hdsubicono').value == "0") {
                        subImagen = "";
                    }
                }
                if (new Number(tidffmt) > 60 && document.getElementById('hdsubicono').value == "1") {
                    subImagen = 'Images/map/radio.png';
                }
            }
        }
        document.getElementById("txMarcador").value = '';
        Texto = getTextoInfo(Vid, Alias, Lat, Lon, getVel(Vel), Rumbo, Kil, Fecha, Punto, Zoom, Tipo, EstadoIgnicion, Evento, Entradas, Calle, Icono, Conductor, Temperatura, Bateria, NivelGasolinaOBD, TiempoDetenido, Horometro, NivelBateria, VoltajeBateria, VoltajeAlimentacion, EA2, EO, TipoVehiculo, Propietario, TipoSA1, OBD, zona, SA1, SA2, SA3);

        Icon = null;
        Icon = new LeafIcon({
            iconUrl: Imagen,
            shadowSize: [0, 0]
        });

        Marcador = null;
        Marcador = L.marker(L.latLng(Lat, Lon),
            {
                icon: Icon,
                id: String(Vid),
                vid: String(Vid),
                placa: String(Alias),
                ultimoreporte: String(Fecha),
                renderer: myRenderer,
                iconSize: new L.Point(15, 15),
                tooltipAnchor: new L.Point(14, 14),
                pmIgnore: true,
                keyboard: false
            });

        //if (subImagen != '') {
        //    try {
        //        var sTexto;
        //        subIcon = null;
        //        subIcon = new LeafIcon({
        //            iconUrl: subImagen,
        //            shadowSize: [0, 0],
        //            iconAnchor: [0, 24]
        //        });

        //        subMarcador = null;
        //        subMarcador = L.marker(new L.latLng(Lat, Lon),
        //            {
        //                icon: subIcon,
        //                id: String(Vid),
        //                vid: String(Vid),
        //                placa: String(Alias),
        //                ultimoreporte: String(Fecha)
        //            });

        //        sTexto = '';

        //        sTexto += '<div align="left" style="overflow-x: hidden"><font color="#000000"><table style="width:450px" cellspacing="0">';
        //        sTexto += '<tr><td colspan="2"><hr width="100%"></td></tr>';
        //        sTexto += '<tr style="background-color: black; font-family:Arial; font-size:small; font-weight: bold; color:white"><td colspan="2">' + String(Alias) + '<hr width="100%"></td></tr>';
        //        if (new Number(tidffmt) > 60) {
        //            sTexto += '<tr><td class="style2000b">TIEMPO SIN REPORTAR (HHHH:MM) </td><td class="style2000">' + String(tdiff) + '</td></tr>';
        //        }
        //        else {
        //            Texto += '&nbsp;';
        //        }
        //        sTexto += '</table></font></div>';

        //        if (sTexto != '') {
        //            try { subMarcador.bindPopup(sTexto, { maxWidth: "254px" }); } catch (Error) { console.log(Error); }
        //        }
        //    }
        //    catch (Error) {
        //        console.log(Error);
        //    }
        //}

        //if (OBD != null && document.getElementById("hdpais").value == 'EC') {
        //    if (OBD == '1') {
        //        obdIcon = null;
        //        obdIcon = new LeafIcon({
        //            iconUrl: 'Imagenes/map/obd.png',
        //            shadowSize: [0, 0],
        //            iconAnchor: [24, 48]
        //        });

        //        obdMarcador = null;
        //        obdMarcador = L.marker(new L.latLng(Lat, Lon),
        //            {
        //                icon: obdIcon,
        //                id: String(Vid),
        //                vid: String(Vid),
        //                placa: String(Alias),
        //                ultimoreporte: String(Fecha)
        //            });

        //        obdMarcador.on('click', function () {
        //            try {
        //                data.getobdactivo(String(this.options.id), String(this.options.placa), document.getElementById('idusuario').value).then(function (data) {
        //                    try {
        //                        try { obdMarcador.bindPopup(data.d, { maxWidth: "254px" }); } catch (Error) { console.log(Error); }
        //                    }
        //                    catch (Error) {
        //                        console.log(Error);
        //                    }
        //                });
        //            }
        //            catch (Error) {
        //                console.log(Error);
        //            }
        //        });
        //    }
        //}        

        Marcador.on('click', function (e) {
            try {
                if (localStorage.getItem("mrk." + e.target.options.id) != null) {
                    try {
                        let infow = document.getElementById("dvinfowindow");
                        infow.innerHTML = "";
                        infow.innerHTML = localStorage.getItem("mrk." + e.target.options.id);
                        infow.style.display = "";
                        infow = null;
                    }
                    catch (Error) {
                        console.log(Error);
                    }
                }
                document.getElementById('txPlaca').value = e.target.options.placa;
                document.getElementById('hdVidPopup').value = e.target.options.vid;

                if (e.latlng != undefined) {
                    document.getElementById('txLatitudMapa').value = e.latlng.lat;
                    document.getElementById('txLongitudMapa').value = e.latlng.lng;
                }               

                //$("#imgtracking").attr('src', 'Images/tracking-appgreen.png');
                //$("#li_" + String(e.target.options.vid.replace('tdu_', ''))).addClass('active');
                //$("#txfiltrounidad").val(e.target.options.placa);
                //$("#txfiltrounidad").keyup();

                //try {
                //    $("#imgtracking").attr('title', 'Seguimiento habilitado para: ' + e.target.options.placa);
                //}
                //catch (Error) {
                //    console.log(Error);
                //}
                //enviarmensaje('Seguimiento habilitdo para: ' + $("#tdu_" + String(e.target.options.vid.replace('tdu_', ''))).text());
            }
            catch (Error) { console.log(Error); }
        });
        Marcador.on('mouseover', function (e) {
            try {
                this.openTooltip();
            }
            catch (Error) { console.log(Error); }
        });

        try {
            Marcador.bindTooltip(String(Alias) + '', {
                permanent: true,
                className: 'leaflet-label'
            });
            Marcador.setTooltipContent(String(Alias) + '', {
                permanent: true,
                className: 'leaflet-label'
            });
            Marcador.closeTooltip();

            if (new Number($("#sptotalunidades").text()) > 50) {
                Marcador.closeTooltip();
            }
        }
        catch (Error) { console.log(Error); }

        try {
            localStorage.setItem('mrk.' + Vid, Texto);
        }
        catch (Error) {
            //try { Marcador.bindPopup(Texto, { maxWidth: "444px" }); } catch (Error) { console.log(Error); } 
            console.log(Error);
        }

        try {
            cgeos.addLayer(Marcador);
        }
        catch (Error) {
            console.log(Error);
        }

        //if (subImagen != '') {
        //    try {
        //        cgeos.addLayer(subMarcador);
        //    }
        //    catch (Error) {
        //        console.log(Error);
        //    }            
        //}

        if (clusterr) {
            lRecorrido.addLayer(Marcador, { chunkedLoading: true });

            if (subImagen != '') {
                lRecorrido.addLayer(subMarcador, { chunkedLoading: true });
            }

            if (OBD != null) {
                if (OBD == '1') {
                    lRecorrido.addLayer(obdMarcador, { chunkedLoading: true });
                }
            }
        }
        else {
            lmarkers.push(Marcador);
            try {
                Marcador.addTo(map);
            }
            catch (Error) {
                //Marcador.addTo(map);
            }

            //if (subImagen != '') {
            //    subMarcador.addTo(map);
            //}

            //if (OBD != null && document.getElementById("hdpais").value == 'EC') {
            //    if (OBD == '1') {
            //        obdMarcador.addTo(map);
            //    }
            //}
        }

        //if (subImagen != '') {
        //    lmarkers.push(subMarcador);
        //}

        //if (OBD != null) {
        //    if (OBD == '1') {
        //        lmarkers.push(obdMarcador);
        //    }
        //}

        //map.setZoom(16);
        //LayersMapa.push(Marcador);
        //if (document.getElementById("dview").style.display == "") {
        //    UbicarSV2(Lat, Lon, Rumbo, true)
        //}
    }
    catch (Error) { console.log(Error.message); }
    finally {
        Texto = null; Point = null; geocoder = null;
    }
}

function isNumberKey(evt) {
    let charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function cerrarpopup(opcion) {
    try {
        document.getElementById("dvinfowindow").innerHTML = "";
        document.getElementById("dvinfowindow").style.display = 'none';
        $("hdVidPopup").val('');
    }
    catch (Error) {
        console.log(Error);
    }
}

function expandirpopup(opcion) {
    try {
        if (document.getElementById("trbarrapopup").style.display == 'none') {
            document.getElementById("trbarrapopup").style.display = '';
            document.getElementById("triconospopup1").style.display = '';
            document.getElementById("triconospopup2").style.display = '';
            document.getElementById("triconospopup3").style.display = '';
            opcion.src = 'images/contraer.png';
        }
        else {
            document.getElementById("trbarrapopup").style.display = 'none';
            document.getElementById("triconospopup1").style.display = 'none';
            document.getElementById("triconospopup2").style.display = 'none';
            document.getElementById("triconospopup3").style.display = 'none';
            opcion.src = 'images/expandir.png';
        }
    }
    catch (Error) {
        console.log(Error);
    }
}

function getTextoInfo(Vid, Alias, Lat, Lon, Vel, Rumbo, Kil, Fecha, Punto, Zoom, Tipo, EstadoIgnicion, Evento, Entradas, Calle, Icono, Conductor, Temperatura, Bateria, NivelGasolinaOBD, TiempoDetenido, Horometro, NivelBateria, VoltajeBateria, VoltajeAlimentacion, EA2, EO, TipoVehiculo, Propietario, TipoSA1, OBD, zona, SA1, SA2, SA3) {
    let Texto = '';
    let color, Imagen, PtoCercano;

    try {
        try { PtoCercano = Punto.split('*'); }
        catch (Error) { PtoCercano = ''; }

        if (Calle == 'undefined' || Calle == 'UNDEFINED') {
            Calle = 'N/D';
        }

        color = "#000000";
        if (EstadoIgnicion == '1' || EstadoIgnicion == 'True') {
            color = "#000000";
            if (new Number(tidffmt) >= 0 && new Number(tidffmt) < 11) {
                color = "#000000";
            }
            else {
                if (new Number(tidffmt) >= 11 && new Number(tidffmt) < 16) {
                    color = "Orange";
                }
                else {
                    color = "Red";
                }
            }
        }
        else {
            if (new Number(tidffmt) >= 0 && new Number(tidffmt) < 61) {
                color = "#000000";
            }
            else {
                if (new Number(tidffmt) >= 61 && new Number(tidffmt) < 121) {
                    color = "Orange";
                }
                else {
                    color = "Red";
                }
            }
        }

        if (Icono == null) { Icono = ''; }
        if (Tipo == 1) {
            Imagen = "Images/icoPuntosReferencialesD.png";
        }
        else {
            if (Icono == '') {
                if (new Number(tidffmt) > 60 && document.getElementById('hdsubicono').value == "1") {
                    subImagen = 'Images/map/radio.png';
                }

                if (GetCourse(Rumbo) == '') { vel = 'STOP'; }
                else {
                    if (String(Vel) == '0') {
                        vel = 'STOP';
                    }
                    else {
                        vel = 'v';
                    }
                }
                if (Icono == null) { Icono = ''; }
                if (Icono == '') {
                    if (vel == 'STOP' || v == '') {
                        if (vel == '') {
                            vel = 'STOP';
                        }
                        Imagen = "Images/map/" + vel + ".png";
                    }
                    else {
                        var xvel = new Number(Vel);
                        switch (true) {
                            case (xvel <= 50):
                                Imagen = "Images/map/" + vel + GetCourse(Rumbo) + ".png";
                                break;
                            case (xvel >= 51 && xvel <= 70):
                                Imagen = "Images/map/a" + GetCourse(Rumbo) + ".png";
                                break;
                            case (xvel >= 71 && xvel <= 100):
                                Imagen = "Images/map/o" + GetCourse(Rumbo) + ".png";
                                break;
                            case (xvel >= 101):
                                Imagen = "Images/map/" + GetCourse(Rumbo) + ".png";
                                break;
                            default:
                                Imagen = "Images/map/" + vel + GetCourse(Rumbo) + ".png";
                                break;
                        }
                        xvel = null;
                    }
                }
                else {
                    Imagen = Icono;
                }
            }
            else {
                Imagen = Icono;

                if (vel == 'STOP' || v == '') {
                    if (vel == '') {
                        vel = 'STOP';
                    }

                    if (document.getElementById('hdsubicono').value == "1" && document.getElementById('hdsubicono').value == "1") {
                        subImagen = "Images/map/" + vel + ".png";
                    }
                }
                else {
                    switch (true) {
                        case (xvel <= 50):
                            subImagen = "Images/map/" + vel + Rumbo + ".png";
                            break;
                        case (xvel >= 51 && xvel <= 70):
                            subImagen = "Images/map/a" + Rumbo + ".png";
                            break;
                        case (xvel >= 71 && xvel <= 100):
                            subImagen = "Images/map/o" + Rumbo + ".png";
                            break;
                        case (xvel >= 101):
                            subImagen = "Images/map/" + Rumbo + ".png";
                            break;
                        default:
                            subImagen = "Images/map/" + vel + Rumbo + ".png";
                            break;
                    }
                    xvels = null;

                    if (document.getElementById('hdsubicono').value == "0") {
                        subImagen = "";
                    }
                }

                if (new Number(tidffmt) > 60 && document.getElementById('hdsubicono').value == "1") {
                    subImagen = 'Images/map/radio.png';
                }
            }
        }


        if (Tipo == 1) {
            Texto += '<div align="left" style="width:250px; backcolor:transparent"><font color="#000000"><table class="small" style="border-collapse:separate;border: solid black 1px;border-radius:6px;-moz-border-radius:6px;" border="0" cellspacing="0">' + '<tr><td colspan="2" class="style2000"><hr width="100%"></td></tr>';
            Texto += '<tr><td class="style2000b">ID: </td><td class="style2000">' + Vid + '</td></tr>';
            Texto += '<tr><td class="style2000b">Nombre: </td><td class="style2000"><span class="btn btn-sm btn-primary">' + Alias + '</span></td></tr>';
            if (document.getElementById("txMostrarCoordenadas").value == 'True') {
                Texto += '<tr><td class="style2000b"> Lat y Long: </td><td class="style2000">' + getCoordenadas(document.getElementById("txUnidadCoordenadas").value, Lat, Lon) + '</td></tr>';
            }
        }
        else {
            Texto += '<div align="left" style="overflow-x: hidden"><font color="transparent"><table id="tbpopup_' + String(Vid) + '" class="table-condensed small" style="width:330px" cellspacing="0">';
            //Texto += '<tr><td colspan="2"><hr width="100%"></td></tr>';
            Texto += '<tr style="background-color: #006699; opacity: 0.65; font-family:' + Fuente + '; font-size:13pt; font-weight: bold; color:white"><td colspan="1">' + '<span class="badge badge-light" onclick="iraunidadinfo(' + String(Vid) + ');" >' + String(Alias) + '</span>' + '</td><td style="width:170px; text-align:right; opacity: 1">&nbsp;&nbsp;&nbsp;<img id="imgpopup" src="Images/expandir.png" onclick="expandirpopup(this);" style="cursor:arrow" />&nbsp;<img title="Expandir/Contrer infowindow" src="Images/cerrarpopup.png" onclick="cerrarpopup(this);" style="cursor:arrow" /></td></tr>';
            Texto += '<tr id="trbarrapopup" style="background-color: white; display:none"><td colspan="2"><table border=0 font color="white"><tr>';
            if (document.getElementById('idsubusuario').value == '' || document.getElementById('idsubusuario').value == '0') {
                if (Tipo != 1 && document.getElementById("txEditarEtiquetas").value != "False") {
                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/etiqueta.png" onclick="javascript:CargarEtiqueta(' + '\'' + String(Vid) + '\'' + ');" alt="Editar etiqueta" title="Editar etiqueta" data-toggle="modal" data-target="#mventanaetiqueta" style="cursor:pointer"></img></td>';
                }
                if (Tipo != 1) {
                    if (document.getElementById('idperfil').value == 4) {
                        Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/seguimiento.png" onclick="javascript:AbrirSeguimientoMovil(' + '\'' + String(Alias) + '*' + String(Vid) + '\'' + ',false);" alt="Iniciar Seguimiento" title="Iniciar Seguimiento" style="cursor:pointer"></img></td>';
                    }

                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/configuracion.png" onclick="javascript:AbrirConfiguracion(' + '\'' + String(Alias) + '\'' + ');" alt="Abrir Configuracion" title="Abrir Configuracion" data-toggle="modal" data-target="#mactivoconfig" style="cursor:pointer"></img></td>';
                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/sview.png" onclick="UbicarSV2(' + Lat + ',' + Lon + ',\'' + String(Rumbo) + '\'' + ',false);" alt="Mostrar/Ocultar Street View" title="Mostrar Street View" style="cursor:pointer"></img></td>';
                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/waze.png" onclick="UbicarWZ(' + Lat + ',' + Lon + ',\'' + String(Rumbo) + '\'' + ',false);" alt="Mostrar Trafico en Waze" title="Mostrar Trafico en Waze" style="cursor:pointer"></img></td>';

                    if (TipoSA1 == 'MSJ') {
                        Texto += '<td class="style2000bt">&nbsp;</td>';
                        Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/msj.png" onclick="mostrarVentana(' + '\'' + 'mnuMensajeria' + '\'' + ',' + '\'' + String(Vid) + '\'' + ');" alt="Mostrar Chat de Mensajes" title="Mostrar Chat de Mensajes" style="cursor:pointer"></img></td>';
                    }

                    Texto += '<td>&nbsp;</td>';

                    if (document.getElementById('idperfil').value == 4) {
                        Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/link.png" onclick="AbrirLink(' + '\'' + String(Alias) + '\'' + ');" alt="Enviar Link de Seguimiento" title="Enviar Link de Seguimiento" style="cursor:pointer"></img></td>';
                    }

                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/info.png" onclick="javascript:AbrirInformacion(' + '\'' + String(Alias) + '_' + String(Vid)  + '\'' + ');" alt="Abrir Informacion" title="Abrir Informacion" data-toggle="modal" data-target="#mactivoinfo"  style="cursor:pointer"></img></td>';
                }
            }

            Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" style="width:14px;height:16px" src="Images/poll.png" onclick="javascript:pollunidadinfo(' + '\'' + String(Vid) + '\'' + ');" alt="Solicitar ubicacion" title="Solicitar ubicacion" style="cursor:pointer"></img></td>';
            if (String(document.getElementById("txseguimiento").value) == '1') {
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" style="width:14px;height:16px" src="Images/infowin/seguimiento.png" onclick="javascript:AbrirSeguimientoMovil(' + '\'' + String(Alias) + '*' + String(Vid) + '\'' + ');" alt="Iniciar Seguimiento" title="Iniciar Seguimiento" style="cursor:pointer"></img></td>';
            }
            if (String(document.getElementById("txEnviarComando").value) == 'True') {
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" style="width:14px;height:16px" src="Images/infowin/accion.png" onclick="javascript:CargarComandos(' + '\'' + String(Vid) + '\'' + ');" alt="Enviar Comandos" title="Enviar Comandos" data-toggle="modal" data-target="#mventana" style="cursor:pointer"></img></td>';
            }


            if (document.getElementById('idsubusuario').value != '' && document.getElementById('idsubusuario').value != '0') {
                if (document.getElementById("hdeditetiquitasub").value == 1) {
                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/etiqueta.png" onclick="javascript:CargarEtiqueta(' + '\'' + String(Vid) + '\'' + ');" alt="Editar etiqueta" title="Editar etiqueta" data-toggle="modal" data-target="#mventanaetiqueta" style="cursor:pointer"></img></td>';
                }

                if (document.getElementById("hdconfigactivosub").value == 1) {
                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/configuracion.png" onclick="javascript:AbrirConfiguracion(' + '\'' + String(Alias) + '\'' + ');" alt="Abrir Configuracion" title="Abrir Configuracion" data-toggle="modal" data-target="#mactivoconfig" style="cursor:pointer"></img></td>';
                }

                if (document.getElementById('hdsegr').value == 1) {
                    Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/link.png" onclick="AbrirLink(' + '\'' + String(Alias) + '\'' + ');" alt="Enviar Link de Seguimiento" title="Enviar Link de Seguimiento" style="cursor:pointer"></img></td>';
                }
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/info.png" onclick="javascript:AbrirInformacion(' + '\'' + String(Alias) + '_' + String(Vid) + '\'' + ');" alt="Abrir Informacion" title="Abrir Informacion" data-toggle="modal" data-target="#mactivoinfo"  style="cursor:pointer"></img></td>';
            }

            Texto += '<td>&nbsp;</td>';
            Texto += '<td style="background-color:#006699; color:white;width:3px"></td>';
            Texto += '<td>&nbsp;</td>';
            if (String(document.getElementById("hdrecorrido").value) == '1') {
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/recorrido.png" style="cursor:pointer" alt="" title="Ver Detallado de Hoy" onclick="AbrirDetalladoHoy(' + '\'' + String(Vid) + '\'' + ',' + '\'' + String(Alias) + '\'' + ');" /></td>';
                Texto += '<td>&nbsp;</td>';
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/stops.png" style="cursor:pointer" alt="" title="Ver Puntos y Zonas de Hoy" onclick="AbrirPuntosZonasHoy(' + '\'' + String(Vid) + '\'' + ',' + '\'' + String(Alias) + '\'' + ');" /></td>';
                Texto += '<td>&nbsp;</td>';
                //Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/Alertas.png" style="cursor:pointer" alt="" title="Ver Alertas de Hoy" onclick="AbrirAlertasHoy(' + '\'' + String(Vid) + '\'' + ',' + '\'' + String(Alias) + '\'' + ');" /></td>';
                //Texto += '<td>&nbsp;</td>';
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/Trayectos.png" style="cursor:pointer" alt="" title="Ver Trayectos de Hoy" onclick="AbrirTrayectosHoy(' + '\'' + String(Vid) + '\'' + ',' + '\'' + String(Alias) + '\'' + ');" /></td>';
            }
            if (String(document.getElementById("hdlogistica").value) == '1') {
                Texto += '<td>&nbsp;</td>';
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/delivery.png" style="cursor:pointer" alt="" title="Ver Grafico Despacho del Dia" onclick="GraficarDespachoHoy(' + '\'' + String(Vid) + '\'' + ',' + '\'' + String(1) + '\'' + ');" /></td>';
            }
            Texto += '<td>&nbsp;</td>';

            Texto += '<td style="width:140px; background-color: white">&nbsp;<td>';
            Texto += '<td class="style2000bt"><h5>&nbsp;</h5><td>';
            Texto += '<td>&nbsp;</td>';
            if (String(EstadoIgnicion) != '' && String(EstadoIgnicion) != 'NULL') {
                if (EstadoIgnicion == '1' || EstadoIgnicion == 'True') { Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="images/infowin/key_on.png" width="16px" height="16px" title="Ignicion ON" /></td>'; }
                else { Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/key_off.png" width="16px" height="16px" title="Ignicion OFF" /></td>'; }
            }
            Texto += '<td>&nbsp;</td>';
            if (trim(String(Rumbo)) != '') { Texto += '<td class="style2000bt">' + '<img style="width:14px;height:16px" src="' + Imagen + '" width="20px" height="20px" title="Rumbo: ' + String(Rumbo) + '" />' + '</td>'; }
            Texto += '</tr></table></td></tr>';
            Texto += '<tr id="triconospopup" style="border-top-style:solid"><td class="style2000bt" colspan="2"><table border=0 width=100%><tr id="triconospopup1" style="display:none">';

            if (String(document.getElementById("hdverkms").value) == '1') {
                Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/kms.png" alt="Kilometraje Actual" title="Kilometraje Actual" /></td><td class="style200025">' + String(Kil) + ' Km' + '</td>';
            }
            else {
                Texto += '<td class="style2000bt"><img style="width:14px;height:16px" src="Images/infowin/kms.png" alt="Kilometraje Actual" title="Kilometraje Actual" /></td><td class="style200025">' + ' --- ' + '</td>';
            }

            Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/alternator.png" alt="Voltaje Alternador" title="Voltaje Alternador" /></td><td class="style200025">' + String(VoltajeAlimentacion) + ' v.</td>';
            Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/nb.png" alt="Nivel de Bateria" title="Nivel de Bateria" /></td><td class="style200025">' + String(NivelBateria) + ' %</td>';
            Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/s1.png" alt="Sensor 1" title="Sensor 1" /></td><td class="style200025">' + String(Bateria) + '</td>';
            Texto += '</tr><tr id="triconospopup2" style="display:none">';
            Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/hhtt.png" alt="Horómetro" title="Horómetro" /></td><td class="style200025">' + String(Horometro) + '</td>';
            Texto += '<td class="style2000bt"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/vb.png" alt="Voltaje Bateria" title="Voltaje Bateria" /></td><td class="style200025">' + String(VoltajeBateria) + ' v.</td>';
            Texto += '<td><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/s2.png" alt="Sensor 2" title="Sensor 2" /></td><td class="style200025">' + String(EA2) + '</td>';
            Texto += '<td><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/s3.png" alt="Sensor 3" title="Sensor 3" /></td><td class="style200025">' + String(Temperatura) + '</td>';
            Texto += '</tr><tr id="triconospopup3" style="display:none">';
            Texto += '<td><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/s1.png" alt="Sensor 4" title="Sensor 4" /></td><td class="style200025">' + String(SA1) + '</td>';
            Texto += '<td><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/s2.png" alt="Sensor 5" title="Sensor 5" /></td><td class="style200025">' + String(SA2) + '</td>';
            Texto += '<td><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/s3.png" alt="Sensor 6" title="Sensor 6" /></td><td class="style200025">' + String(SA3) + '</td>';
            Texto += '<td></td><td class="style200025"></td>';
            Texto += '</tr><tr style="border-top-style:solid;">';
            Texto += '<td style="width:16px"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/gps.png" alt="Latitud y Longitud" title="Latitud y Longitud" /></td><td class="style200025" style="width:116px" colspan=3>' + getCoordenadas(document.getElementById("txUnidadCoordenadas").value, Lat, Lon) + '</td>';
            Texto += '<td style="width:16px"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/speed.png" alt="Velocidad" title="Velocidad" /></td><td class="style200025"><b>' + String(Vel) + ' ' + document.getElementById("txUnidadVelocidad").value + '</b></td>';
            if (Vel == '0' && TiempoDetenido != '') {
                Texto += '<td style="width:16px"><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/stop.png" alt="Tiempo Detenido" title="Tiempo Detenido" /></td><td class="style200025"><b>' + String(TiempoDetenido) + '</b></td>';
            }
            else {
                Texto += '<td><img loading="lazy" style="width:14px;height:16px" src="Images/infowin/stop.png" alt="Tiempo Detenido" title="Tiempo Detenido" /></td><td class="style200025"> --- </td>';
            }
            Texto += '</tr></table></td></tr>';

            //Texto += '<tr><td colspan="2"><hr width="100%"></td></tr>';
            //Texto+='<tr><td class="style2000b">Vehículo: </td><td class="style2000">'+Alias+'</td></tr>';
            //if (document.getElementById("txMostrarCoordenadas").value == 'True') {
            //    Texto += '<tr><td class="style2000b"> Lat y Long: </td><td class="style2000">' + getCoordenadas(document.getElementById("txUnidadCoordenadas").value, Lat, Lon) + '</td></tr>';
            //}
            //Texto += '<tr><td class="style2000b">Velocidad: </td><td class="style2000">' + String(Vel) + ' ' + document.getElementById("txUnidadVelocidad").value + '</td></tr>';

            Texto += '<tr style="border-top-style:solid;"><td class="style2000"><b><font color="' + color + '">&nbsp;' + String(Fecha) + '</font></b></td><td class="style2000">' + Evento + '</td></tr>';

            //if (String(Rumbo)!=''){Texto +='<tr><td class="style2000b">Rumbo: </td><td class="style2000">'+String(Rumbo)+'</td></tr>';}
            //if (String(Kil)!='0'){Texto+='<tr><td class="style2000b">Kilometraje: </td><td class="style2000">'+String(Kil)+' Km(s).'+'</td></tr>';}


            //if (trim(Calle) != '') {
            //    Texto += '<tr><td class="style2000b">Calle / Referencia: </td><td class="style2000"><b>' + Calle + '</b></td></tr>';
            //}
            if (Calle != '') {
                Texto += '<tr><td colspan=2 class="style2000 small text-primary">&nbsp;' + String(Calle).toUpperCase().replace('AVENIDA', 'AV.').replace("GUAYAQUIL", "GYE") + '</td></tr>';
            }
            if (PtoCercano.length < 5) {
                if (trim(Punto) != '') {
                    Texto += '<tr><td colspan=2 class="style2 small text-success">&nbsp;' + String(Punto) + ' mt(s).</td></tr>';
                }
                //document.getElementById("txMarcador").value = Vid + '_' + Alias + '_' + String(Lat) + '_' + String(Lon) + '_' + String(Vel) + '_' + String(Rumbo) + '_' + String(Kil) + '_' + String(Fecha) + '_' + String(Punto) + '_' + String(EstadoIgnicion) + '_' + String(Evento) + '_' + String(Entradas);
            }
            else {
                if (String(trim(PtoCercano[0])) != '') {
                    Texto += '<tr><td colspan=2 class="style2000 small text-success">&nbsp;' + String(PtoCercano[0]) + ' a ' + String(PtoCercano[3]) + ' m.</td></tr>'
                }
                //document.getElementById("txMarcador").value = Vid + '_' + Alias + '_' + String(Lat) + '_' + String(Lon) + '_' + String(Vel) + '_' + String(Rumbo) + '_' + String(Kil) + '_' + String(Fecha) + '_' + String(PtoCercano[0]) + ' a ' + String(PtoCercano[3]) + ' mt(s)' + '_' + String(EstadoIgnicion) + '_' + String(Evento) + '_' + String(Entradas);
            }
            if (zona != '') {
                Texto += '<tr><td colspan=2 class="style2000 small text-info">&nbsp;<b>' + zona + '</b></td></tr>';
            }
            if (Conductor != '') {
                Texto += '<tr><td colspan=2 class="style2000"><b>&nbsp;<a href="#" onclick="javascript:EditarConductor(' + '\'' + String(Conductor) + '\'' + ');">&nbsp;' + Conductor + '</a></b></td></tr>';
            }
            //if (Temperatura!='0'){
            //    Texto+='<tr><td class="style2000b">Temperatura: </td><td class="style2000">' + String(Temperatura) + ' °C</td></tr>';
            //}
            //if (Bateria!='0'){
            //    Texto+='<tr><td class="style2000b">Sensor1: </td><td class="style2000">' +String(Bateria) + '</td></tr>';
            //}
            if (NivelGasolinaOBD != '0' && NivelGasolinaOBD != '') {
                Texto += '<tr><td colspan=2 class="style2000">&nbsp;' + String(NivelGasolinaOBD) + '%</td></tr>';
            }
            //if (Vel == '0' && TiempoDetenido != '') {
            //    Texto += '<tr><td class="style2000b">Tiempo Detenido: </td><td class="style2000">' + String(TiempoDetenido) + '</td></tr>';
            //}
            if (Propietario != '') {
                Texto += '<tr><td colspan=2 class="style2000">&nbsp;' + String(Propietario) + '</td></tr>';
            }
            if (TipoVehiculo != '') {
                Texto += '<tr><td class="style2000">&nbsp;' + String(TipoVehiculo) + '</td><td colspan=2 class="style2000"><b>' + String(EO) + '</b></td></tr>';
            }
            //Texto += '<tr><td class="style2000b">Street View: </td><td class="style2000"><a href="#" onclick=UbicarSV(' + Lat + ',' + Lon + ',"' + String(Rumbo) + '",false)>Ver</a></td></tr>';
            Texto += '</table></font></div>';
        }

        return Texto;
    }
    catch (Error) {
        console.log(Error);
        return '';
    }
}

function AccionCancelar() { return false; }

function AbrirPublicidad() { window.open('Publicidad.aspx', 'vnPublicidad', 'status=0,left=200,top=10,fullscreen=no,width=780,height=275,menubar=no,resizable=no,titlebar=0,scrollbars=0', true); }

function AddItem(Combo, Text, Value) {
    var opt = document.createElement("option");
    document.getElementById(Combo).options.add(opt);
    opt.text = Text; opt.value = Value;
}

function SeleccionarTipoReporte(Obj) {
    var indCombo; var Largo;
    Largo = document.getElementById("cbIntervalo").length;
    for (indCombo = Largo; indCombo >= 0; indCombo--) { document.getElementById("cbIntervalo").remove(indCombo); }
    indCombo = null; Largo = null;
    AddItem("cbIntervalo", " ", "0");
    if (Obj.value == 'RM') {
        AddItem("cbIntervalo", "Rango de Fechas", "P");
        document.getElementById("cbIntervalo").selectedIndex = 1;
        MostrarReporte(document.getElementById("cbVehiculos").value, document.getElementById("cbVehiculos").options[document.getElementById("cbVehiculos").selectedIndex].text, document.getElementById("cbIntervalo").value, document.getElementById("cbTipo").value);
    }
    if (Obj.value == 'RC') {
        AddItem("cbIntervalo", "Dia de Hoy", "H");
        AddItem("cbIntervalo", "Dia de Ayer", "Y");
        AddItem("cbIntervalo", "Antes de Ayer", "A");
        AddItem("cbIntervalo", "Las Ultimas 6 Horas", "S");
        AddItem("cbIntervalo", "Las Ultimas 3 Horas", "T");
        AddItem("cbIntervalo", "Las Ultimas 2 Horas", "D");
        AddItem("cbIntervalo", "La Ultima Hora", "U");
        AddItem("cbIntervalo", "Rango de Fechas", "P");
    }
    if (Obj.value == 'RK') {
        AddItem("cbIntervalo", "Rango de Fechas", "P");
        document.getElementById("cbIntervalo").selectedIndex = 1;
        MostrarReporte(document.getElementById("cbVehiculos").value, document.getElementById("cbVehiculos").options[document.getElementById("cbVehiculos").selectedIndex].text, document.getElementById("cbIntervalo").value, document.getElementById("cbTipo").value);
    }
    if (Obj.value == 'RE') {
        AddItem("cbIntervalo", "Rango de Fechas", "P");
        document.getElementById("cbIntervalo").selectedIndex = 1;
        MostrarReporte(document.getElementById("cbVehiculos").value, document.getElementById("cbVehiculos").options[document.getElementById("cbVehiculos").selectedIndex].text, document.getElementById("cbIntervalo").value, document.getElementById("cbTipo").value);
    }
    if (Obj.value == 'RV') {
        AddItem("cbIntervalo", "Rango de Fechas", "P");
        document.getElementById("cbIntervalo").selectedIndex = 1;
        MostrarReporte(document.getElementById("cbVehiculos").value, document.getElementById("cbVehiculos").options[document.getElementById("cbVehiculos").selectedIndex].text, document.getElementById("cbIntervalo").value, document.getElementById("cbTipo").value);
    }
    if (Obj.value == 'RP') {
        AddItem("cbIntervalo", "Rango de Fechas", "P");
        document.getElementById("cbIntervalo").selectedIndex = 1;
        MostrarReporte(document.getElementById("cbVehiculos").value, document.getElementById("cbVehiculos").options[document.getElementById("cbVehiculos").selectedIndex].text, document.getElementById("cbIntervalo").value, document.getElementById("cbTipo").value);
    }
    if (Obj.value == 'RH') {
        AddItem("cbIntervalo", "Rango de Fechas", "P");
        document.getElementById("cbIntervalo").selectedIndex = 1;
        MostrarReporte(document.getElementById("cbVehiculos").value, document.getElementById("cbVehiculos").options[document.getElementById("cbVehiculos").selectedIndex].text, document.getElementById("cbIntervalo").value, document.getElementById("cbTipo").value);
    }
}

function CambiarTextoBoton(Valor) {
    if (Valor == 'P') { document.getElementById("btnMostrarReporte").value = 'Elegir Fechas'; }
    else { document.getElementById("btnMostrarReporte").value = 'Mostrar'; }
}

function BuscarUnidades(Placa) {
    if (Placa != '') { }
    else { alert(Placa); }
}

function MarcarPlacas(Obj) {
    LimpiarMapa();
    if (Obj.checked) { } else { }
}

function trim(myString) { return myString.replace(/^\s+/g, '').replace(/\s+$/g, ''); }

function getCoordenadas(Formato, Latitud, Longitud) {
    var Resultado = '';
    try {
        switch (Formato) {
            case 'GMS':
                Resultado = gramise(Latitud, Longitud);
                break;
            case 'DEC':
                Resultado = String(Latitud) + ' ; ' + String(Longitud);
                break;
            case 'GM':
                Resultado = grami(Latitud, Longitud);
                break;
            case 'UTM':
                var xy = new Array(2);
                var zone;
                zone = Math.floor((Longitud + 180.0) / 6) + 1;
                zone = LatLonToUTMXY(DegToRad(Latitud), DegToRad(Longitud), zone, xy);
                Resultado = String(Math.round(xy[0])) + ' ; ' + String(Math.round(xy[1])) + ' Zona ' + String(zone) + getLetraZona(Latitud);
                zone = null;
                break;
            case 'MGRS':
                Resultado = LatLonToMGRS(Latitud, Longitud);
                break;
        }
    }
    catch (Error) { Resultado = ''; }
    return Resultado;
}

function grami(Latitud, Longitud) {
    var Resultado = '';
    var error = 0; var signlat = 0; var signlon = 0;
    var latitudGPS = Latitud; var longitudGPS = Longitud;
    if (Latitud < 0) { signlat = -1; } else { signlat = 0; }
    latAbs = Math.abs(Math.round(Latitud * 1000000.));
    if (latAbs > (90 * 1000000)) {
        alert('ERROR: Los Grados (º) de la Latitud varian entre -90 y 90.');
        latAbs = 0; error = 1;
    }
    if (Longitud < 0) { signlon = -1; } else { signlon = 0; }
    lonAbs = Math.abs(Math.round(Longitud * 1000000.));
    if (lonAbs > (180 * 1000000)) {
        alert('ERROR: Los Grados (º) de la Longitud varian entre -180 y 180.');
        lonAbs = 0; error = 1;
    }
    if (error == 0) {
        if (signlat >= 0) { Resultado = 'N '; }
        else { Resultado = 'S '; }
        Resultado += ((Math.floor(latAbs / 1000000)) + 'º ' + Math.floor(((latAbs / 1000000) - Math.floor(latAbs / 1000000)) * 60)) + '\' ';
        if (signlon >= 0) { Resultado += ' , E '; }
        else { Resultado += ' , W '; }
        Resultado += ((Math.floor(lonAbs / 1000000)) + 'º ' + Math.floor(((lonAbs / 1000000) - Math.floor(lonAbs / 1000000)) * 60)) + '\' ';
    }
    signlat = 1; signlon = 1;
    latitudGPS = null; longitudGPS = null;
    return Resultado;
}

function gramise(Latitud, Longitud) {
    var Resultado = '';
    var error = 0; var signlat = 0; var signlon = 0;
    var latitudGPS = Latitud; var longitudGPS = Longitud;
    if (Latitud < 0) { signlat = -1; } else { signlat = 0; }
    latAbs = Math.abs(Math.round(Latitud * 1000000.));
    if (latAbs > (90 * 1000000)) {
        alert('ERROR: Los Grados (º) de la Latitud varian entre -90 y 90.');
        latAbs = 0; error = 1;
    }
    if (Longitud < 0) { signlon = -1; } else { signlon = 0; }
    lonAbs = Math.abs(Math.round(Longitud * 1000000.));
    if (lonAbs > (180 * 1000000)) {
        alert('ERROR: Los Grados (º) de la Longitud varian entre -180 y 180.');
        lonAbs = 0; error = 1;
    }
    if (error == 0) {
        if (signlat >= 0) { Resultado = 'N '; }
        else { Resultado = 'S '; }
        Resultado += ((Math.floor(latAbs / 1000000)) + 'º ' + Math.floor(((latAbs / 1000000) - Math.floor(latAbs / 1000000)) * 60) + '\' ' + (Math.floor(((((latAbs / 1000000) - Math.floor(latAbs / 1000000)) * 60) - Math.floor(((latAbs / 1000000) - Math.floor(latAbs / 1000000)) * 60)) * 100000) * 60 / 100000) + '\"');
        if (signlon >= 0) { Resultado += ' , E '; }
        else { Resultado += ' , W '; }
        Resultado += ((Math.floor(lonAbs / 1000000)) + 'º ' + Math.floor(((lonAbs / 1000000) - Math.floor(lonAbs / 1000000)) * 60) + '\' ' + (Math.floor(((((lonAbs / 1000000) - Math.floor(lonAbs / 1000000)) * 60) - Math.floor(((lonAbs / 1000000) - Math.floor(lonAbs / 1000000)) * 60)) * 100000) * 60 / 100000) + '\"');
    }
    signlat = 1; signlon = 1;
    latitudGPS = null; longitudGPS = null;
    return Resultado;
}

function getLetraZona(Latitud) {
    var letter = "?";
    var lat, Pos;
    var Letters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
    try {
        lat = parseFloat(Latitud);
        Pos = Math.floor((lat + 80) / 8 + 2);
        if ((Pos > 1) && (Pos < 21)) { letter = Letters.substring(Pos, Pos + 1); }
        if ((lat >= 72) && (lat < 84)) { letter = "X"; }
    }
    catch (Error) { letter = '' }
    return letter;
}

function LatLonToMGRS(Latitud, Longitud) {
    var xy = new Array(2);
    var Letters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
    var Pos, P, GR, LongZone, LatZone, North, NorthStr, East, EastStr, N100km, E100km, Prec, scale, rLatZone;
    lat = parseFloat(Latitud);
    if ((lat >= 84) || (lat < -80)) { return false; }
    Pos = Math.floor(lat / 8) + 12;
    LatZone = Letters.substring(Pos, Pos + 1);
    if (LatZone > 'X') { LatZone = 'X'; }
    lon = parseFloat(Longitud);
    LongZone = calculatedLatZone(lon, lat);
    rLatZone = LatZone;
    LongZone = LatLonToUTMXY(DegToRad(lat), DegToRad(lon), LongZone, xy);
    East = xy[0];
    North = xy[1];
    Pos = Math.floor(North / 100000);
    Pos %= 20;
    if ((LongZone % 2) == 0) {
        Pos += 5;
        Pos %= 20;
    }
    N100km = Letters.substring(Pos, Pos + 1);
    Pos = Math.floor(East / 100000);
    P = LongZone + 2;
    P %= 3;
    Pos = Pos + P * 8 - 1;
    Pos %= 24;
    E100km = Letters.substring(Pos, Pos + 1);
    Prec = 5;
    scale = Math.pow(10, (5 - Prec));
    EastStr = '' + Math.round(East / scale);
    NorthStr = '' + Math.round(North / scale);
    while (EastStr.length < 7) { EastStr = '0' + EastStr; }
    while (NorthStr.length < 7) { NorthStr = '0' + NorthStr; }
    coordinates = EastStr.substring(7 - Prec, 7) + ' ' + NorthStr.substring(7 - Prec, 7);
    GR = LongZone + LatZone + E100km + N100km + ' ' + coordinates;
    return GR;
}

function calculatedLatZone(lon, lat) {
    LongZone = Math.floor((lon / 6) + 31);
    if ((lat >= 56) && (lat < 64) && (lon >= 3) && (lon < 12)) { LongZone = 32; }
    if ((lat >= 72) && (lat < 84)) {
        if ((lon > 0) && (lon < 9)) { LongZone = 31; }
        if ((lon >= 9) && (lon < 21)) { LongZone = 33; }
        if ((lon >= 21) && (lon < 33)) { LongZone = 35; }
        if ((lon >= 33) && (lon < 42)) { LongZone = 37; }
    }
    if ((lat > 53) && (lat < 58) && (lon > 7) && (lon < 13) && (document.frmConverter.forceZone32.checked)) { LongZone = 32; }
    return LongZone;
}

function MostrarOcultarRegla() {
    if (ReglaActiva) { dellruler(); ReglaActiva = false; }
    else { addruler(); ReglaActiva = true; }
}

function dellruler() {
    try {
        for (varj = 0; j < LayersRuler.length; j++) { LayersRuler[j].setMap(null); }
        LayersRuler = null; LayersRuler = []; ReglaActiva = false;
    }
    catch (Error) { console.log(Error); }
}

function addruler() {
    ReglaActiva = true;
    var Icon1, Icon2;
    Icon1 = new google.maps.MarkerImage('Imagenes/ubicar.gif',
        new google.maps.Size(16, 16),
        new google.maps.Point(0, 0),
        new google.maps.Point(0, -4),
        new google.maps.Size(16, 16));
    var ruler1 = new google.maps.Marker({
        icon: Icon1,
        position: map.getCenter(),
        map: map,
        draggable: true
    });
    Icon2 = new google.maps.MarkerImage('Imagenes/ubicar.gif',
        new google.maps.Size(16, 16),
        new google.maps.Point(0, 0),
        new google.maps.Point(0, -10),
        new google.maps.Size(16, 16));
    var ruler2 = new google.maps.Marker({
        icon: Icon2,
        position: map.getCenter(),
        map: map,
        draggable: true
    });
    var ruler1label = new Label({ map: map });
    var ruler2label = new Label({ map: map });
    ruler1label.bindTo('position', ruler1, 'position');
    ruler2label.bindTo('position', ruler2, 'position');
    var rulerpoly = new google.maps.Polyline({
        path: [ruler1.position, ruler2.position],
        strokeColor: "#00FF00",
        strokeOpacity: .4,
        strokeWeight: 10
    });
    rulerpoly.setMap(map);
    LayersRuler.push(ruler1);
    LayersRuler.push(ruler2);
    LayersRuler.push(rulerpoly);
    LayersRuler.push(ruler1label);
    LayersRuler.push(ruler2label);
    ruler1label.set('text', distance(ruler1.getPosition().lat(), ruler1.getPosition().lng(), ruler2.getPosition().lat(), ruler2.getPosition().lng()));
    ruler2label.set('text', distance(ruler1.getPosition().lat(), ruler1.getPosition().lng(), ruler2.getPosition().lat(), ruler2.getPosition().lng()));
    google.maps.event.addListener(ruler1, 'drag', function () {
        rulerpoly.setPath([ruler1.getPosition(), ruler2.getPosition()]);
        ruler1label.set('text', distance(ruler1.getPosition().lat(), ruler1.getPosition().lng(), ruler2.getPosition().lat(), ruler2.getPosition().lng()));
        ruler2label.set('text', distance(ruler1.getPosition().lat(), ruler1.getPosition().lng(), ruler2.getPosition().lat(), ruler2.getPosition().lng()));
    });
    google.maps.event.addListener(ruler2, 'drag', function () {
        rulerpoly.setPath([ruler1.getPosition(), ruler2.getPosition()]);
        ruler1label.set('text', distance(ruler1.getPosition().lat(), ruler1.getPosition().lng(), ruler2.getPosition().lat(), ruler2.getPosition().lng()));
        ruler2label.set('text', distance(ruler1.getPosition().lat(), ruler1.getPosition().lng(), ruler2.getPosition().lat(), ruler2.getPosition().lng()));
    });
}

function distance(lat1, lon1, lat2, lon2) {
    var R = 6371;
    var dLat = (lat2 - lat1) * Math.PI / 180;
    var dLon = (lon2 - lon1) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    if (d > 1) return Math.round(d) + "km";
    else if (d <= 1) return Math.round(d * 1000) + "m";
    return d;
}

function GetCourse(nCurso) {
    if (nCurso > 360) {
        nCurso = nCurso - 360;
    }
    if (nCurso == 0) {
        return '';
    }
    if (nCurso >= 1 && nCurso <= 10) {
        return "N";
    }
    if (nCurso >= 351 && nCurso <= 3600) {
        return "N";
    }
    if (nCurso >= 11 && nCurso <= 80) {
        return "NE";
    }
    if (nCurso >= 81 && nCurso <= 100) {
        return "E";
    }
    if (nCurso >= 101 && nCurso <= 170) {
        return "SE";
    }
    if (nCurso >= 171 && nCurso <= 190) {
        return "S";
    }
    if (nCurso >= 191 && nCurso <= 260) {
        return "SO";
    }
    if (nCurso >= 261 && nCurso <= 280) {
        return "O";
    }
    if (nCurso >= 281 && nCurso <= 350) {
        return "NO";
    }
}

function getVel(Velocidad) {
    try {
        let nVelocidad = 0;

        switch (document.getElementById("txUnidadVelocidad").value) {
            case 'Nm/H':
                nVelocidad = Math.round(new Number(Velocidad * 0.868976240408186)) / 1;
                break;
            case 'Km/H':
                nVelocidad = Math.round(new Number(Velocidad * 1.609344)) / 1;
                break;
            case 'Mi/H':
                nVelocidad = Velocidad;
                break;
        }

        if (isNaN(nVelocidad)) {
            return '---';
        }
        else {
            return nVelocidad;
        }
    }
    catch (Error) {
        return Velocidad;
    }
}