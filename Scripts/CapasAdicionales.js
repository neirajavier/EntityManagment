var pretiled = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/mapa/{z}/{x}/{y}.png', { minZoom: 0, maxZoom: 15, attribution: 'Hunter' });
var tiledfly = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 16,
    maxZoom: 22
});


var baseHunter = L.layerGroup([pretiled, tiledfly])

/*
MARCADORES EN MAPA
*/

var kilometro = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:kmfinal@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var aeropuertos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:aeropuertos@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var ccomercial = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:ccomerciales@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var atm = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:atm@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var mercagale = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:mercagale@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var supermercados = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:supermercados@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var grifos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:grifos2016@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var farmacias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:farmacias@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var mejorhogar = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:mejoramientohogar@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var tiendaspordepa = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:tiendaspordepa@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var electrodomesticos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:electrodomesticos@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var mall = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:mall@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var hoteles = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:hoteles@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var casinos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:casinos@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var cines = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:cines@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var fastfood = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:fastfood@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var clubes = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:clubes@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var metropolitano = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:metropolitano@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var metro = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:metro@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var ferreterias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:ferreterias@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var restaurantes = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:restaurantes@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var salud = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:salud@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var universidad = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:universidades_institutos@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var museos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:museos@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var iglesias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:iglesias@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var militares = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:militares@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var bomberos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:bomberos@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var agencias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:agencias@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var seguros = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:seguros@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var agentes = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:agentes@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var casas = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:casas@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});

var financieras = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/mapabase:financieras@EPSG:900913@png/{z}/{x}/{y}.png8', {
    tms: true,
    minZoom: 1,
    maxZoom: 22
});


var overlayMapsHunter = {
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
    "Tiendas Departamentos": tiendaspordepa
    //"Tiendas de Hogar": mejorhogar,
    //"Tiendas Electrodomesticos": electrodomesticos,
    //"Mercados Galerias": mercagale,
    //"Salud": salud,
    //"Museos": museos,
    //"Casinos": casinos,
    //"Cines": cines,
    //"Comida Rapida": fastfood,
    //"Clubes": clubes,
    //"Financieras": financieras,
    //"Agencias": agencias,
    //"Seguros AFPs": seguros,
    //"Agentes Bancarios": agentes,
    //"Casas Cambio": casas
};

/*
FUNCIONES CONTEXMENU
*/

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

function street_view1(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    /*String((parseFloat(e.latlng.lng)+0.005))*/
    window.open('https://www.google.com/maps?q&layer=c&cbll=' + lat + ',' + lng + '&cbp=12,0,0,0,0&z=18');
}

function street_view2(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    /*map.panTo(e.latlng);*/

    window.open('https://www.google.com/maps?q&layer=c&cbll=' + lat + ',' + lng + '&cbp=12,0,0,0,0&z=18', '', 'width=430,height=650,left=0,top=0,location=no');
}

function googlesatelite(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    map.panTo(e.latlng);
    window.open('https://www.google.com.pe/maps/@' + lat + ',' + lng + ',376m/data=!3m1!1e3', '', 'width=430,height=650,left=0,top=0');

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

function googletrafico(e) {
    var lat = e.latlng.lat;
    var lng = e.latlng.lng;
    window.open('https://www.google.com.pe/maps/@' + lat + ',' + lng + ',18z/data=!5m1!1e1', '', 'width=550,height=650,left=0,top=0');
}

var ContextMenuHunter = [{
    text: 'Coordenadas',
    callback: showCoordinates
}, {
    text: 'Waze Trafico',
    callback: wazer
}, {
    text: 'Google Trafico',
    callback: googletrafico
}, {
    text: 'Google Satelite',
    callback: googlesatelite
}, {
    text: 'Google Maps',
    callback: mapagoogle1
}, {
    text: 'Google StreetView',
    callback: street_view2
}, {
    text: 'Centrar Mapa',
    callback: centerMap
}, '-', {
    text: 'Zoom in',
    icon: '../images/zoom-in.png',
    callback: zoomIn
}, {
    text: 'Zoom out',
    icon: '../images/zoom-out.png',
    callback: zoomOut
}];



function paneoMapa(mapa) {
    var surOeste = L.latLng(0.41748, -84.73755),
        norEste = L.latLng(-21.02058, -65.79712),
        LimitesPeru = L.latLngBounds(surOeste, norEste);

    mapa.setMaxBounds(LimitesPeru);
    mapa.on("drag", function () {
        mapa.panInsideBounds(LimitesPeru, { anime: true })
    });
}


$(document).ready(function () {
    //$('.leaflet-control-layers-list').css('text-align', 'left');
    //$('.leaflet-contextmenu').css('text-align', 'left');
});