var testado, map;
var ageocercas = new Array();
var agrupos = new Array();
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

datag = {
    puntosgeocercas: async function (idgeocerca, tipo, nombre, parametro) {
        let dfr = $.Deferred();
        $.ajax({
            type: "POST",
            url: document.getElementById('hdurlgeocercasdetalle').value,
            data: "{'IdGeocerca':'" + new String(idgeocerca) + "','Tipo':'" + new String(tipo) + "','Nombre':'" + nombre + "','Parametro':'" + new String(parametro) + "'}",
            contentType: "application/json; utf-8",
            dataType: "json",
            async: false,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.textStatus);
            },
            success: dfr.resolve
        });
        return dfr.promise();
    }
}

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

function initializeMap(ModoMapa) {
    try {
        var menucontext = true;
        var ilayersmapa = {};
        var olayersmapa = {};
        var listamapas = new String(document.getElementById('hdlistamapas').value).split(',');
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

        map = new L.Map('dvmapa', {
            center: new L.LatLng(LatitudInicial, LongitudInicial), zoom: 6,
            layers: [capai],
            preferCanvas: true
        });

        if (document.getElementById("hdmotormapa").value == "GM") {
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
                    marker: false
                },
                edit: {
                    featureGroup: drawnItems,
                    remove: true
                }
            });
            map.addControl(drawControl);
            map.on('draw:created', function (e) {
                let type = e.layerType,
                    layer = e.layer;
                let ctype;

                if (lmarkersg.length >= 1) {
                    alert('Solo se permite una geocerca en el mapa');
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

                if (ctype != Number($("#hdtipo").val())) {
                    $("#hdtipo").val(ctype);
                }

                switch ($("#hdtipo").val()) {
                    case '1':
                        Poly = layer;
                        console.log(Poly);
                        break;
                    case '2':
                        Line = layer;
                        console.log(Line);
                        break;
                    case '3':
                        Circle = layer;
                        console.log(Circle);
                        break;
                }

                lmarkersg.push(layer);
                drawnItems.addLayer(layer);
            });

            map.on('draw:deleted', function (e) {
                let type = e.type,
                    layer = e.layer;

                //console.log(e.layer);

                //tmpgeoeliminada = null;
                //tmpgeoeliminada = e.layer;

                //console.log(tmpgeoeliminada);
                //drawnItems.removeLayer(layer);

                lmarkersg = null;
                lmarkersg = new Array();

                alert('Se ha quitado la geocerca del mapa');
            });

            map.on('draw:edited', function (e) {
                let layers = e.layers;
                let countOfEditedLayers = 0;

                alert('Se ha editado los puntos de la geocerca, puede proceder a guardarla ');

                layers.eachLayer(function (layer) {
                    console.log(layer.layerType);
                    console.log(layer);
                    countOfEditedLayers++;
                });

                layers = null;
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

function getMenuOpciones() {
    var opciones = '<table style="background-color:white;border=0;bordercolor:tgray;border-collapse:separate;border:solid gray 1px;border-radius:1px;-moz-border-radius:1px;">' +
        '<td style="width:44px; height:44px; vertical-align:middle; text-align:center"><img title="guardar cambios de la geocerca" src="../../images/save.png" onclick="guardargeocercapuntos()" />'
        + '</td></tr></table>';
    return opciones;
}

function MostrarGeocercaMapa(Id, Tipo, Datos, Parametro, Centro, Nombre, Fit, addli, check) {
    var IngfoGeo = Datos.split(';');
    var PuntosGeo = [];
    var limite = 1;
    //var colorgeocerca = gcolors[Math.floor(Math.random() * gcolors.length)];
    var colorgeocerca = gcolors[Math.floor(Math.random() * (gcolors.length - 1))];

    if (Fit == null) {
        Fit = true;
    }

    console.log(Id);
    console.log(Tipo);
    console.log(Datos);
    console.log(Parametro);
    console.log(Centro);
    console.log(Nombre);
    console.log(Fit);
    console.log(addli);
    console.log(check);

    $("#hdtipo").val(Tipo);
    $("#hdidgeocerca").val(Id);
    $("#hdnombre").val(Nombre);
    $("#hdparametro1").val(Parametro);
    $("#hdcentro").val(Centro);

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

$(document).ready(function () {
    $('#grdflota tbody').on('click', 'tr', function () {
        //var data = testado.row(this).data();
        var id, tipo, nombre, parametro;
        
        id = document.getElementById("hId").value
        nombre = document.getElementById("hNombre").value
        tipo = document.getElementById("hTipo").value
        parametro = document.getElementById("hParametro").value
        //if (data != null) {
            try {
                LimpiarMapa();
                datag.puntosgeocercas(id, tipo, nombre, parametro).then(function (datap) {
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
                            }
                            catch (Error) {
                                console.log(Error);
                            }
                        });

                        if (dGeo[1] == '3') {
                            try {
                                Centro = dPuntos[0].split('_')[2].replace(",", ".") + ' ' + dPuntos[0].split('_')[1].replace(",", ".");
                            }
                            catch (Error) {
                                console.log(Error);
                            }
                        }
                        else {
                            sPuntos += new String(new String(dPuntos[0].split('_')[1]).replace(",", ".") + '_' + new String(dPuntos[0].split('_')[2])).replace(",", ".") + ';';
                        }

                        MostrarGeocercaMapa(dGeo[0], dGeo[1], sPuntos, dGeo[3], Centro, dGeo[2], true, false, '');

                        //map.pm.disableDraw('Polygon');
                        //map.pm.disableDraw('Circle');
                        //map.pm.disableDraw('Line');
                        //map.pm.disableDraw('Rectangle');
                        //switch (parseInt(dGeo[1])) {
                        //    case 1:
                        //        map.pm.enableDraw('Polygon');
                        //        map.pm.enableDraw('Rectangle');
                        //        break;
                        //    case 2:
                        //        map.pm.enableDraw('Line');
                        //        break;
                        //    case 3:
                        //        map.pm.enableDraw('Circle');
                        //        break;
                        //}
                    }
                    catch (Error) {
                        console.log(Error);
                    }
                });
            }
            catch (Error) {
                console.log(Error);
            }
        //}

        data.etiqueta = 'new';
        data = null;
    });



    PosicionarMapa();
    javascript: initializeMap();
});

function setAllMap(map) {
    for (var i = 0; i < Marcadores.length; i++) { Marcadores[i].setMap(map); }
    try {
        LatitudInicial = parseFloat(document.getElementById("txLatitudInicial").value);
        LongitudInicial = parseFloat(document.getElementById("txLongitudInicial").value);
    }
    catch (Error) {
        LatitudInicial = -2.1672;
        LongitudInicial = -79.9172;
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

    $("#hdtipo").val('');
    $("#hdidgeocerca").val('');
    $("#hdnombre").val('');
    $("#hdparametro1").val('');
    $("#hdcentro").val('');
    ccapas = 0;
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

window.onresize = async function () {
    PosicionarMapa(true);
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