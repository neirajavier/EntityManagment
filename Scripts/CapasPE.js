var kilometro, aeropuertos, ccomercial, atm, mercagale, supermercados, grifos, farmacias, mejorhogar, tiendaspordepa, electrodomesticos, mall, hoteles, casinos, cines, fastfood;
var clubes, metropolitano, metro, ferreterias, restaurantes, salud, universidad, museos, iglesias, militares, bomberos, agencias, seguros, agentes, casas, financieras, zpeligro, zcomercial;

function CargarCapasPE()
{
            var zmax = 19;

            kilometro = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:kilometraje@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            aeropuertos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:aeropuertos@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            ccomercial = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:ccomerciales@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            atm = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:atms@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            mercagale = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:mercagale@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            supermercados = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:supermercados@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            grifos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:grifos2016@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            farmacias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:farmacias@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            mejorhogar = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:mejoramientohogar@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            tiendaspordepa = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:tiendaspordepa@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            electrodomesticos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:electrodomesticos@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            mall = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:mall@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            hoteles = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:hoteles@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            casinos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:casinos@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            cines = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:cines@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            fastfood = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:fastfood@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            clubes = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:clubes@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            metropolitano = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:metropolitano@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            metro = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:metro@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            ferreterias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:ferreterias@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            restaurantes = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:restaurants@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            salud = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:clinicas_hospitales@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            universidad = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:universidades_institutos@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            museos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:museos_bibliotecas@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            iglesias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:iglesias@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            militares = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:militares@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            bomberos = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:bomberos@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            agencias = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:agencias@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            seguros = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:seguros_afps@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            agentes = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:agentes@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            casas = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:transporte_envio_dinero@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            financieras = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:financieras_cajas@EPSG:900913@png/{z}/{x}/{y}.png8', {
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            zpeligro = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:riesgo2@EPSG:900913@png/{z}/{x}/{y}.png8', {
                transparent: true,
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });

            zcomercial = L.tileLayer(document.getElementById("hdservidorcapaspe").value + '/geoserver/gwc/service/tms/1.0.0/hunter:zcomercio@EPSG:900913@png/{z}/{x}/{y}.png8', {
                transparent: true,
                tms: true,
                minZoom: 1,
                maxZoom: zmax
            });            
}