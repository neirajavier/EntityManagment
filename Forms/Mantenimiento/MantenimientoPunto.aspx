<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoPunto.aspx.vb" Inherits="ArtemisAdmin.MantenimientoPunto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dxq" %>
<%@ Register Assembly="Microsoft.AspNet.EntityDataSource" Namespace="Microsoft.AspNet.EntityDataSource" TagPrefix="ef" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <script src="../../Libs/js/jquery-1.9.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script src="https://raw.github.com/douglascrockford/JSON-js/master/json2.js"></script>

    <link href="https://cdn.datatables.net/1.12.1/css/jquery.dataTables.min.css" rel="stylesheet"/>
    <link href="https://cdn.datatables.net/select/1.4.0/css/select.dataTables.min.css" rel="stylesheet"/>
    <link href="https://cdn.datatables.net/select/1.4.0/css/select.bootstrap4.min.css" rel="stylesheet"/>
    <link href="https://cdn.datatables.net/1.12.1/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/buttons/2.2.3/css/buttons.bootstrap4.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/responsive/2.2.5/css/responsive.bootstrap4.min.css" rel="stylesheet" />

    <script src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.12.1/js/dataTables.bootstrap4.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/1.0.7/js/dataTables.responsive.min.js"></script>

    <script src="https://cdn.datatables.net/buttons/2.2.3/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.bootstrap4.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.5/js/dataTables.responsive.min.js"></script>
    <script src="https://cdn.datatables.net/select/1.4.0/js/dataTables.select.min.js"></script>
    <script src="https://cdn.datatables.net/fixedcolumns/3.3.1/js/dataTables.fixedColumns.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.12.1/features/pageResize/dataTables.pageResize.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.6.2/js/buttons.colVis.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.12.1/features/scrollResize/dataTables.scrollResize.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.print.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.html5.min.js"></script>

    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <link href="https://nightly.datatables.net/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <script src="https://nightly.datatables.net/js/jquery.dataTables.js"></script>
    <link href="https://nightly.datatables.net/select/css/select.dataTables.css?_=83ccd9fe32dc99a890e17a9f1bbde5a4.css" rel="stylesheet" type="text/css" />
    <script src="https://nightly.datatables.net/select/js/dataTables.select.js?_=83ccd9fe32dc99a890e17a9f1bbde5a4"></script>
    
    <script src="../../Libs/js/x-notify.min.js"></script>
    <link rel="stylesheet" href="../../Libs/css/notification.css" />
    <script src="../../Libs/js/notification.js"></script>
    <script src="../../Libs/js/dataTables.cellEdit.js"></script>

    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />--%>
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title>PUNTOS</title>

    <style type="text/css">
        .responstable {
            margin: 1em 0;
            width: 100%;
            overflow: hidden;
            background: #fff;
            color: #024457;
            /*border-radius: 8px;*/
        }
        .responstable tr {
            border-bottom: 0.1px solid #a9a8a8;
        }
        .responstable tr:hover {
            background-color: #d0d0d0;
        }
        /*tbody tr.selected td{
            background-color: #ff4c4c !important;
            color: white;
        }*/
        .row_selected td{
            background-color: #ccc !important;
            color: black;
        }
        .responstable th {
            border: none;
            background-color: #a9a8a8;
            color: white;
            padding: 1em;
            text-align: center;
        }
        .responstable td {
            border-bottom: 0.1px solid #a9a8a8;
            padding: 2em;
        }
        .responstable tbody {
            background-color: #ededed;
            color: #333333;
        }
        .responstable tbody tr {
            height: 40px;    
        }
        #resize_wrapper {
            position: absolute;
			top: 5em;
			left: 1em;
			right: 1em;
			bottom: 1em;
        }

        .tab {
            padding: 0.5em 1.5em;
        }
        .icon {
            width: 30px;
            height: 30px;
        }
        .adaptive-tab {
            display: none;
        }
        @media (max-width: 550px) {
            .tab {
                padding: 0;
            }
            .default-tab {
                display: none
            }
            .adaptive-tab {
                display: block;
            }
        }

    </style>

    <style type="text/css">
        body, html {
            width: 100%;
            height: 100%;
            padding: 0;
            margin: 0;
            overflow: auto;
        }

        .grid
        {
            margin: 0 auto;
            border-radius: 10px;
            background-color: #f2f1f0;
        }

        .mybuttonmenu { width: 40px; 
                         height:40px; 
                         border-radius: 25px 25px 25px 25px; 
                         border: none;
                         background: url('../../images/menucol.png') no-repeat 8% 8%;
        }

        .mybutton2p { border-radius: 0px 25px 25px 0px; text-align:left; border: none;}
        .mybutton2 { border-radius: 25px 25px 25px 25px; }
        .mybutton3 { border-radius: 15px 15px 15px 15px; }
        .mybuttongrid { border-radius: 10px 10px 10px 10px; 
                        border: none;
                        background: transparent;                        
        }
        .mybuttongrid2 { border-radius: 10px 10px 10px 10px; 
                         border: none;
                         background: transparent;
                         float: right;
        }
        .mybuttongrid2f { border-radius: 10px 10px 10px 10px; 
                          border: none;
                          background: transparent;
                          /*margin-left: 190px;*/
        }

        .mylist .dxlbd{ 
            border: none !important;
            height: auto !important;
        }

        /* Solid border */
        hr.solid {
          border: 0.5px solid medium;
          margin: 10px 0px 10px 0px;
        }

        hr.solid2 {
          background-color: transparent; 
          border: none;
          margin: 3px;
        }

        .headerCssClass{   
            color: darkslategrey;  
            border: none;  
            margin: 0px 0px 10px 5px;  
            padding-left: 5px;
            font-size: 13px;
            font-weight: bold;
        }
        .mytext {
            float: right;
            border-radius: 8px 8px 8px 8px;
        }

        .accordionHeader  
        {  
            font-weight: bold; 
            font-size: 13px;
            margin: 0px 0px 10px 5px;
            padding-left: 5px;
            background: url(../../images/expanded.png) no-repeat;  
            background-size: 15px;
            background-position: right;
            text-align: left;  
        }  
        .accordionHeaderSelected  
        {  
            font-weight: bold;  
            font-size: 13px;  
            margin: 0px 0px 10px 5px;
            padding-left: 5px;
            background: url(../../images/collapsed.png) no-repeat;
            background-size: 15px;
            background-position: right;
            text-align: left;  
        }

        /*.dataTables_wrapper .dataTables_paginate .paginate_button.current {
            background: darkred;
        }
        .dataTables_wrapper .dataTables_paginate .paginate_button.custom:hover {
            background: darkgrey; 
        }*/
        .dataTables-container{
            height: 100% !important;
        }

        .dataTables_scrollHeadInner, .table{
            width: 100% !important
         }

        /*.page-item.active .page-link {
            background-color: #a9a8a8 !important;
            font: bold;
            color: white !important;
            border-color: #545454;
            border-radius: 8px;
        }
        .page-link {
            color: #A3A3A5 !important;
  
        }
        .paginate_button:hover{
            background: none;
            color: black!important;
        } */

        .dataTables_paginate {
            float: left !important;
        }
        .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
            background: #d33333;
            color: white!important;
            border-radius: 5px;
            border: 1px solid #d33333;
        }
        .dataTables_wrapper .dataTables_paginate .paginate_button:active {
            background: #d33333;
            color: white!important;
            border: 1px solid #d33333;
        }

        /*.dataTables_wrapper .dataTables_paginate .paginate_button:hover {
            color: white !important;
            border: 1px solid #d33333!important;
            background-color: #d33333!important;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #d33333), color-stop(100%, #d33333))!important;
            background: -webkit-linear-gradient(top, #d33333 0%, #d33333 100%)!important;
            background: -moz-linear-gradient(top, #d33333 0%, #d33333 100%)!important;
            background: -ms-linear-gradient(top, #d33333 0%, #d33333 100%)!important;
            background: -o-linear-gradient(top, #d33333 0%, #d33333 100%)!important;
            background: linear-gradient(to bottom, #d33333 0%, #d33333 100%)!important;
        }*/

        .glossyBtn {
            background:url(../../images/search2.png) left no-repeat;
            background-color: white;
            border-radius: 8px;
            font-size: 12px;
            padding: 5px 0px 0px 15px;
            height: 40px;
        }
        input[type=text]:focus {
            border: 2px grey;
            padding-left: 40px;
            border-color: red;
        }

        input[type=checkbox] { accent-color: red !important; }

        img:hover {
              /*background: url("https://i.stack.imgur.com/7KXRp.jpg");
               padding: [image-height] [image-width] 0 0 */
              background-color: #dadad2;
        }

    </style>

    <script type="text/javascript">
        var isPageSizeCallbackRequired = false;
        const notification = new Notification();
        var showPopup = true;
        var iframe, focus;

        var puntos, puntos2;
        //var subusuarios;

        function GrupoWindow() {
            pcGrupo.Show();
            pcGrupo.SetHeaderText("Nuevo Grupo");
            tbID.SetText("0");
            tbGrupo.SetText("");
            tbDescripcion.SetText("");
        }
        function GrupoWindowHide() {
            pcGrupo.Hide();
            pcGrupo.PerformCallback();
        }
        function GrupoWindowHide2() {
            pcGrupo.Hide();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function EliminaHide() {
            popupDelete.Hide();
        }

        //GridView SelectedChanged
        //function grid_SelectionChangedPunto(s, e) {
        //    var contar = s.GetSelectedRowCount();
        //    s.GetSelectedFieldValues("id;pid", RecieveGridPuntoValues);
        //    if (contar > 0) {
        //        if (!btnAsignar.GetClientVisible()) {
        //            btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
        //        }
        //    } else {
        //        if (btnAsignar.GetClientVisible()) {
        //            btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
        //        }
        //    }
        //}
        function RecieveGridPuntoValues(values) {
            //pmGrupo2.PerformCallback(values)
            puntos = values;
        }
        function RecieveGridPuntoValues2(values) {
            puntos2 = values;
        }
        //function grid_SelectionChangedSubusuario(s, e) {
        //    var contar = s.GetSelectedRowCount();
        //    s.GetSelectedFieldValues("sid", RecieveGridSubusuarioValues);
        //    if (contar > 0) {
        //        if (!btnAsignarS.GetClientVisible()) {
        //            btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
        //        }
        //    } else {
        //        if (btnAsignarS.GetClientVisible())
        //            btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
        //    }
        //}
        //function RecieveGridSubusuarioValues(values) {
        //    //pmGrupo4.PerformCallback(values)
        //    subusuarios = values;
        //}

        function selectionPunto(count) {
            var opc = document.getElementById("hfCadena").value;

            if (opc == "1") {
                if (count > 0) {
                    if (!btnAsignar.GetClientVisible()) {
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                    }
                } else {
                    if (btnAsignar.GetClientVisible()) {
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                        puntos = '';
                    }
                }
            }
        }
        //function selectionPunto2(count) {
        //    if (count > 0) {
        //        if (!btnAsignarS.GetClientVisible()) {
        //            btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
        //        }
        //    } else {
        //        if (btnAsignarS.GetClientVisible()) {
        //            btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
        //            subusuarios = '';
        //        }
        //    }
        //}

        //GridGrupo
        function InitGridPunto(s, e) {
            var sbuser = document.getElementById("hfCadena").value;
            if (sbuser == '1') {
                if (gridGrupo.GetFocusedRowIndex() <= 0) {
                    llenargrid();
                    //llenargridS();
                }
            } else {
                llenargrid();
            }
            
        }
        function UpdateGridPunto(s, e) {
            //if (pgGrids.GetActiveTab().name == 'tbPuntos') {
            //    gridPunto.PerformCallback('1');
            //} else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridSubusuario.PerformCallback('1');
            //}
            if (s.GetFocusedRowIndex() >= 0) {
                limpiargrid();
                //limpiargrid2();
                gridGrupo.PerformCallback('update');

            }
            //gridGrupo.SetFocusedRowIndex(-1);

            //gridPunto.PerformCallback('1');
            //gridSubusuario.PerformCallback('1');
        }
        function UpdateGridPunto2(s, e) {
            pmGrupo.Hide();
        }
        function UpdateGrids(s, e) {
            //if (pgGrids.GetActiveTab().name == 'tbPuntos') {
            //    gridGrupo.SetFocusedRowIndex(-1);
            //    gridPunto.PerformCallback('puntos');
            //} else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridGrupo.SetFocusedRowIndex(-1);
            //    gridSubusuario.PerformCallback('subusuarios');
            //}
            //debugger;
            var sbuser = document.getElementById("hfCadena").value;
            if (sbuser == '1') {
                gridGrupo.SetFocusedRowIndex(-1);
                llenargrid();
            } else {
                llenargrid();
            }

            //llenargridS();
            //gridPunto.PerformCallback('puntos');
            //gridSubusuario.PerformCallback('subusuarios');
            var punto = hpuntos.value;
            //var subuser = hsubuser.value;

            //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Puntos</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + punto + '</span></div> </div>');
            //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
            lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + punto + '</span></div>');
        }
        function EndCallbackGridPunto(s, e) {
            //debugger;
            var validar = JSON.parse(gridGrupo.cpValidar);
            if (gridGrupo.GetFocusedRowIndex() > -1) {
                var lista = JSON.parse(gridGrupo.cpNumbersJS);
                var punto = lista[0];
                //var subuser = lista[1];

                document.getElementById("hpuntos").value = lista[0];
                //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Puntos</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + punto + '</span></div> </div>');
                //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
                lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + punto + '</span></div>');

                llenargrid2();
                //llenargridS2();
            } else if (gridGrupo.GetFocusedRowIndex() == -1){
                var lista = JSON.parse(gridGrupo.cpNumbersJS);
                var punto = lista[0];
                //var subuser = lista[1];

                document.getElementById("hpuntos").value = lista[0];
                //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Puntos</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + punto + '</span></div> </div>');
                //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
                lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + punto + '</span></div>');

                llenargrid();
                //llenargridS();
            }

            if (validar == "1")
                showNotification2('Ya existe el código ingresado, no se puede actualizar.','A');

        }

        function UpdateGridsText(s, e) {
            var aux = txSearch.GetText();
            if (pgGrids.GetActiveTab().name == 'tbPuntos') {
                if (aux.length >= 1) {
                    gridPunto.PerformCallback('cfpuntos');
                } else {
                    gridPunto.PerformCallback('sfpuntos');
                    gridGrupo.SetFocusedRowIndex(-1);
                }
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridGrupo.SetFocusedRowIndex(-1);
            //    gridSubusuario.PerformCallback('subusuarios');
            //}
        }
        function InitButton(s, e) {
            if (pgGrids.GetActiveTab().name == 'tbPuntos') {
                //deselectPoints2();
                btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
                //btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
                document.getElementById("global_filter").style.display = '';
                //document.getElementById("global_filter2").style.display = 'none';
                //if (btnAsignarS.GetClientVisible()) {
                //    btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
                //    //gridSubusuario.UnselectRows();
                //    llenargrid();
                //}
                llenargrid();
                btnPtoN.SetClientVisible(!btnPtoN.GetClientVisible());
                $('#grdflota').DataTable().columns.adjust().draw();
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    deselectPoints();
            //    btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
            //    btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
            //    document.getElementById("global_filter").style.display = 'none';
            //    document.getElementById("global_filter2").style.display = '';
            //    if (btnAsignar.GetClientVisible()) {
            //        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
            //        //gridPunto.UnselectRows();
            //        llenargrid2();
            //    }
            //    btnPtoN.SetClientVisible(!btnPtoN.GetClientVisible());
            //    $('#grdflota2').DataTable().columns.adjust().draw();
            //}
        }
        function onInicioTab(s, e) {
            var opc = document.getElementById("hfCadena").value;
            var tabActivo = pgGrids.GetTab(0);
            //var tabSbuser = pgGrids.GetTab(1);
            if (opc == "1") {
                tabActivo.SetVisible(true);
                //tabSbuser.SetVisible(true);
            } else {
                tabActivo.SetVisible(true);
                //tabSbuser.SetVisible(false);
            }
        }

        //InitPopus
        function OnPopupInit(s, e) {
            iframe = pmGrupo2.GetContentIFrame();
            ASPxClientUtils.AttachEventToElement(iframe, 'load', OnContentLoaded);
        }
        function OnPopupShown(s, e) {
            if (showPopup)
                lp.ShowInElement(iframe);
        }
        function OnContentLoaded(e) {
            showPopup = false;
            lp.Hide();
        }

        //Asignaciones Popup
        //function OnMoreInfoClick(contentUrl) {
        function OnMoreInfoClick(pid, pto) {
            var contentUrl = 'AsignarGrupoPto.aspx?myPid=' + pid + '&myPto=' + pto;
            //var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
            //console.log(contentUrl);
            pmGrupo.SetContentUrl(contentUrl);
            pmGrupo.Show();
        }
        
        function OnMoreInfoClick2(s, e) {
            llenarPoints();
            var contentUrl = "AsignarGrupoPto2.aspx?valores=" + puntos;
            showPopup = true;
            pmGrupo2.SetContentUrl(contentUrl);
            pmGrupo2.Show();
        }

        //function OnMoreInfoClickS(contentUrl) {
        //function OnMoreInfoClickS(sid, id) {
        //    var contentUrl = 'AsignarGrupoPtoS.aspx?mySid=' + sid + '&mySub=' + id;
        //    //var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        //    pmGrupo3.SetContentUrl(contentUrl);
        //    pmGrupo3.Show();
        //}
        
        //function OnMoreInfoClickS2(s, e) {
        //    llenarPoints2();
        //    var contentUrl = "AsignarGrupoPtoS2.aspx?valores=" + subusuarios;
        //    pmGrupo4.SetContentUrl(contentUrl);
        //    pmGrupo4.Show();
        //}

        function OnMostrarPto(s, e) {
            var contentUrl = "MapaPuntos.aspx?data=nueva";
            pmMapa.SetContentUrl(contentUrl);
            pmMapa.ShowAtPos((window.innerHeight / 2), 20);
            pmMapa.Show();
        }

        function OnMostrarPto2(pid) {
            var contentUrl = "MapaPuntos.aspx?data=" + pid;
            console.log(contentUrl);
            pmMapa.SetContentUrl(contentUrl);
            pmMapa.ShowAtPos((window.innerHeight / 2), 20);
            pmMapa.Show();
        }
        function OnEliminaPto(pid, pto) {
            var contentUrl = 'DeletePunto.aspx?myPid=' + pid + '&myPto=' + pto;
            popupDelPto.SetContentUrl(contentUrl);
            popupDelPto.Show();
        }
        function OnEditarPto2(pid) {
            var contentUrl = 'EditarPunto.aspx?myPid=' + pid;
            console.log(contentUrl);
            popupEdtPto.SetContentUrl(contentUrl);
            popupEdtPto.Show();
        }

        function HidePopup() {
            pmGrupo2.Hide();
            //gridPunto.UnselectRows();
            deselectPoints();
            if(gridGrupo.GetFocusedRowIndex() > -1)
                gridGrupo.PerformCallback('update');
            else if(gridGrupo.GetFocusedRowIndex() == -1)
                gridGrupo.PerformCallback('2');

            document.getElementById('selectAll').checked = false;
        }
        function HidePopup2() {
            pmGrupo4.Hide();
            //gridSubusuario.UnselectRows();
            //deselectPoints2();
            if (gridGrupo.GetFocusedRowIndex() > -1)
                gridGrupo.PerformCallback('update');
            else if(gridGrupo.GetFocusedRowIndex() == -1)
                gridGrupo.PerformCallback('2');

            //document.getElementById('selectAll2').checked = false;
        }

        function onCloseBtn(s, e) {
            if (gridGrupo.GetFocusedRowIndex() > -1)
                gridGrupo.PerformCallback('update');
            else if(gridGrupo.GetFocusedRowIndex() == -1)
                gridGrupo.PerformCallback('2');
        }

        //Grupos Popup
        function ShowInfo(contentUrl, estado) {
            if (estado == 'E')
                pcGrupo.SetHeaderText("Editar Grupo");
            else
                pcGrupo.SetHeaderText("Nuevo Grupo");
            pcGrupo.SetContentUrl(contentUrl);
            pcGrupo.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function ShowMessage(contentUrl) {
            popupDelete.SetContentUrl(contentUrl);
            popupDelete.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function HidePopup3() {
            pcGrupo.Hide();
            gridGrupo.PerformCallback('1');
        }
        function HidePopup4() {
            popupDelete.Hide();
            gridGrupo.PerformCallback('1');
            gridPunto.PerformCallback('puntos');
            //gridSubusuario.PerformCallback('subusuarios');
        }

        function HidePopup5() {
            popupDelPto.Hide();
            var sbuser = document.getElementById("hfCadena").value;
            if (sbuser == '1') {
                if (gridGrupo.GetFocusedRowIndex() > -1) {
                    limpiargrid();
                    //limpiargrid2();
                    gridGrupo.PerformCallback('update');
                    llenargrid2();
                    //llenargridS2();
                }
                else if(gridGrupo.GetFocusedRowIndex() == -1) {
                    limpiargrid();
                    //limpiargrid2();
                    gridGrupo.PerformCallback('2');
                    llenargrid();
                    //llenargridS();
                }
            } else {
                gridGrupo.PerformCallback('2');
            }
        }

        function HidePopup6() {
            pmMapa.Hide();
            var sbuser = document.getElementById("hfCadena").value;
            if (sbuser == '1') {
                if (gridGrupo.GetFocusedRowIndex() > -1) {
                    gridGrupo.PerformCallback('update');
                }
                else if (gridGrupo.GetFocusedRowIndex() == -1) {
                    gridGrupo.PerformCallback('2');
                }
            } else {
                gridGrupo.PerformCallback('2');
            }
        }

        function HidePopup7() {
            popupEdtPto.Hide();
            //debugger;
            var sbuser = document.getElementById("hfCadena").value;
            if (sbuser == '1') {
                if (gridGrupo.GetFocusedRowIndex() > -1) {
                    limpiargrid();
                    //limpiargrid2();
                    gridGrupo.PerformCallback('update');
                    llenargrid2();
                    //llenargridS2();
                }
                else if (gridGrupo.GetFocusedRowIndex() == -1) {
                    limpiargrid();
                    //limpiargrid2();
                    gridGrupo.PerformCallback('2');
                    llenargrid();
                    //llenargridS();
                }
            } else {
                gridGrupo.PerformCallback('2');
            }
            
        }

        //Autosize GridView
        function AdjustSizeW(flag) {
            var element2 = document.getElementById('lateral');
            if (!flag) {
                element2.style.display = 'none';
                UpdateGridWidth();
            } else {
                element2.style.display = 'block';
                UpdateGridWidthReverse();
            }
        }
        function onInit(s, e) {
            UpdateGridHeight();
        }
        function onEndCallback(s, e) {
            if (isPageSizeCallbackRequired === true) {
                UpdateGridHeight();
            }
        }

        function hideAccordionPane(mobile) {
            if (mobile)
                $find('accPanel_AccordionExtender').set_selectedIndex(-1);
            else
                $find('accPanel_AccordionExtender').set_selectedIndex(0);
        }

        function OnControlsInitialized(s, e) {
            //UpdateGridHeight();
            const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
            if (isMobile) {
                btnShow.SetClientVisible(false);
                //hideAccordionPane(isMobile);
                UpdateGridHeight2();
            } else {
                btnShow.SetClientVisible(true);
                //hideAccordionPane(isMobile);
                UpdateGridHeight();
            }
        }
        function OnBrowserWindowResized(s, e) {
            //UpdateGridHeight();
            //var data = jsGetDataTableHeightPx();
            //resizeTable(data);
            const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
            if (isMobile) {
                btnShow.SetClientVisible(false);
                hideAccordionPane(isMobile);
                UpdateGridHeight2();
            } else {
                btnShow.SetClientVisible(true);
                hideAccordionPane(isMobile);
                UpdateGridHeight();
            }
            var data = jsGetDataTableHeightPx();
            resizeTable(data);
        }

        //Button Show
        function shTable() {
            if (pgGrids.GetActiveTab().name == 'tbPuntos') {
                btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
                //gridGrupo.SetVisible(!gridGrupo.GetVisible());
                AdjustSizeW(btnTodos.GetClientVisible());
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
            //    //gridGrupo.SetVisible(!gridGrupo.GetVisible());
            //    AdjustSizeW(btnTodos2.GetClientVisible());
            //}

            //btnTodos.SetVisible(!btnTodos.GetVisible());
            //gridGrupo.SetVisible(!gridGrupo.GetVisible());
            //AdjustSizeW(btnTodos.GetVisible());
        }

        function UpdateGridHeight() {
            //gridPunto.SetHeight(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 240;
            pgGrids.SetHeight(height);
            //gridPunto.SetHeight(height);

            //gridSubusuario.SetHeight(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var height = positionInfo.height;
            //if (document.body.scrollHeight > height)
            //    height = document.body.scrollHeight - 100;
            //gridSubusuario.SetHeight(height);

            gridGrupo.SetHeight(0);
            var element = document.getElementById('lateral');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 240;
            gridGrupo.SetHeight(height);

            //var elementG = document.getElementById('mybody');
            //var positionInfoG = elementG.getBoundingClientRect();
            //var heightG = positionInfoG.height;
            document.getElementById('hfHeight').value = height;
            isPageSizeCallbackRequired = false;
        }
        function UpdateGridWidth() {
            //gridPunto.SetWidth(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var width = positionInfo.width;

            //gridSubusuario.SetWidth(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var width = positionInfo.width;

            var element2 = document.getElementById('lateral');
            var positionInfo2 = element2.getBoundingClientRect();
            var width2 = positionInfo2.width;

            //if (document.body.scrollWidth > width)
            //    width = document.body.scrollWidth - 500;

            //gridPunto.SetWidth(width + width2 - 60);
            //gridSubusuario.SetWidth(width + width2 - 60);
        }
        function UpdateGridWidthReverse() {
            //gridPunto.SetWidth(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var width = positionInfo.width - 60;
            //gridPunto.SetWidth(width);

            //gridSubusuario.SetWidth(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var width = positionInfo.width - 60;
            //gridSubusuario.SetWidth(width);
        }

        document.getElementById("central").addEventListener('resize', function (evt) {
            if (ASPxClientUtils && !ASPxClientUtils.androidPlatform)
                return;
            var activeElement = document.activeElement;
            if (activeElement && (activeElement.tagName === "INPUT" || activeElement.tagName === "TEXTAREA") && activeElement.scrollIntoViewIfNeeded)
                document.getElementById("central").setTimeout(function () { activeElement.scrollIntoViewIfNeeded(); }, 0);
        });
        window.addEventListener('resize', function (evt) {
            if (ASPxClientUtils && !ASPxClientUtils.androidPlatform)
                return;
            var activeElement = document.activeElement;
            if (activeElement && (activeElement.tagName === "INPUT" || activeElement.tagName === "TEXTAREA") && activeElement.scrollIntoViewIfNeeded)
                window.setTimeout(function () { activeElement.scrollIntoViewIfNeeded(); }, 0);
        });

        function showNotification(message, tipo) {
            if (tipo == 'S') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#36bb34',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 5000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            } else if (tipo == 'A') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#ffce38',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 5000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            } else if (tipo == 'I') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#6fa8dc',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 5000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            } else if (tipo == 'E') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#f52617',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 10000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            }
        }

        function showNotification2(message, tipo) {
            if (tipo == 'S') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#36bb34',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 5000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            } else if (tipo == 'A') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#ffce38',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 5000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            } else if (tipo == 'I') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#6fa8dc',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 5000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            } else if (tipo == 'E') {
                notification = new Notification({
                    text: message,
                    style: {
                        background: '#f52617',
                        color: '#fff',
                        width: 'fit-content',
                        height: 'fit-content'
                    },
                    position: 'bottom-center',
                    autoClose: 10000,
                    canClose: false,
                    showProgress: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false,
                    pauseOnHover: false,
                    pauseOnFocusLoss: false
                });
            }

        }
    </script>

    <script language="JavaScript" type="text/javascript">
        var testado, testado2;
        var countChk, countChk2;
        //var apuntos = new Array();
        var apuntos;
        var apuntos2;
        //var apuntosS;
        //var apuntosS2;

        class clspunto {
            constructor(secuencia, idpunto, codigo, nombre, descripcion, latitud, longitud, accion1, accion2, accion3) {
                this.secuencia = secuencia;
                this.idpunto = idpunto;
                this.codigo = codigo;
                this.nombre = nombre;
                this.descripcion = descripcion;
                this.latitud = latitud;
                this.longitud = longitud;
                this.accion1 = accion1;
                this.accion2 = accion2;
                this.accion3 = accion3;
            }

            toRow() {
                return [secuencia, idpunto, codigo, nombre, descripcion, latitud, longitud, accion1, accion2, accion3];
            }
        }

        //class clssubuser {
        //    constructor(accion, secuencia, idsubuser, subuser, nombres) {
        //        this.accion = accion;
        //        this.secuencia = secuencia;
        //        this.idsubuser = idsubuser;
        //        this.subuser = subuser;
        //        this.nombres = nombres;
        //    }

        //    toRow() {
        //        return [accion, secuencia, idsubuser, subuser, nombres];
        //    }
        //}

        $(document).ready(function () {
            initDataTable();
            //debugger;
            var sbuser = document.getElementById("hfCadena").value;
            if (sbuser == '1')
                document.getElementById("panelGrupo").style.display = '';
            else
                document.getElementById("panelGrupo").style.display = 'none';

            document.getElementById("grdflota").style.display = '';
            //document.getElementById("grdflota2").style.display = '';
            $('input.global_filter').on('keyup click', function () {
                filterGlobal();
            });

            //window.onresize = function () {
            //    var data = jsGetDataTableHeightPx();
            //    resizeTable(data);
            //};
            var table = $('#grdflota').DataTable();
            $('#selectAll').click(function () {
                //var cols = table.column(0).nodes();
                var state = this.checked;
                var p = table.rows({ page: 'current' }).nodes();
                //for (var i = 0; i < cols.length; i += 1) {
                //    cols[i].querySelector("input[type='checkbox']").checked = state;
                //    table.rows(i).select();
                //}
                for (var i = 0; i < p.length; i += 1) {
                    p[i].querySelector("input[type='checkbox']").checked = state;
                    table.rows(i).select();
                }

                var result = countChecked($('#grdflota'), '.chk');
                countChk = result.checked;

                selectionPunto(countChk);
            });

            //var table2 = $('#grdflota2').DataTable();
            //$('#selectAll2').click(function () {
            //    var state2 = this.checked;
            //    var p = table2.rows({ page: 'current' }).nodes();
            //    for (var i = 0; i < p.length; i += 1) {
            //        p[i].querySelector("input[type='checkbox']").checked = state2;
            //        table2.rows(i).select();
            //    }

            //    var result2 = countChecked($('#grdflota2'), '.chk2');
            //    countChk2 = result2.checked;

            //    selectionPunto2(countChk2);
            //});

            //table.MakeCellsEditable({
            //    "columns": [3, 4, 5],
            //    "onUpdate": myCallbackFunction
            //});
           
        });

        //var tableEdt = $('#grdflota').DataTable();
        //function myCallbackFunction(updatedCell, updatedRow, oldValue) {
        //    debugger;
        //    var newValue = updatedCell.data();
        //    console.log("The new value for the cell is: " + updatedCell.data());
        //    console.log("The old value for that cell was: " + oldValue);
        //    var print = tableEdt.row(':contains("'+ oldValue+'")').data();
        //    console.log("row editado " + print);
        //    //console.log("The values for each cell in that row are: " + updatedRow.data());
        //}

        var tableEdt = $('#grdflota').DataTable();
        const createdCell = function (cell) {
            let original;
            cell.setAttribute('contenteditable', true)
            cell.setAttribute('spellcheck', false)
            cell.addEventListener("focus", function (e) {
                original = e.target.textContent
            })
            cell.addEventListener("blur", function (e) {
                if (original !== e.target.textContent) {
                    //debugger;
                    const row = tableEdt.row(e.target.parentElement)
                    var rows = e.target.parentElement._DT_RowIndex;
                    var celda = e.target.cellIndex;
                    newValue = e.target.textContent;
                    row.invalidate();
                    printRow(newValue, original, rows, celda);
                }
            })
        }

        function initDataTable() {
            var page, alto;
            var altura = jsGetDataTableHeightPx();
            if (altura >= 500 && altura < 700) {
                alto = '65vh'
                page = 11;
            } else if (altura >= 700 && altura < 800) {
                alto = '65vh'
                page = 13;
            } else if (altura >= 800) {
                alto = '90vh'
                page = 18;
            }

            testado = $('#grdflota').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json",
                    "decimal": ".",
                    "thousands": ",",
                    "paginate": {
                        first: '<img style="width:20px;height:20px" src="../../images/first.png"/>',
                        previous: '<img style="width:20px;height:20px" src="../../images/back.png"/>',
                        next: '<img style="width:20px;height:20px" src="../../images/next.png"/>',
                        last: '<img style="width:20px;height:20px" src="../../images/last.png" />'
                    },
                    "emptyTable": "No hay datos disponibles en la tabla",
                    "info": "Página _PAGE_ de _PAGES_ (_TOTAL_ elementos)",
                    "infoEmpty": "Página 0 de 0 elementos",
                    "processing": "<span class='fa-stack fa-lg'>\n\<i class='fa fa-spinner fa-spin fa-stack-2x fa-fw'></i>\n\</span>&nbsp;&nbsp;&nbsp;&nbsp;Procesando ...",
                },
                "dom": "BtrSip",
                //"dom": "Brtip",
                "order": [4, 'asc'],
                "paging": true,
                "scrollY": alto,
                "scrollX": true,
                "scrollCollapse": true,
                "deferRender": true,
                "iDisplayLength": page,
                "filter": false,
                "serverSide": false,
                "responsive": true,
                "processing": true,
                "searching": true,
                "pagingType": "full_numbers",
                "fixedHeader": {
                    "header": true
                },
                "columnDefs": [{
                    "defaultContent": "-",
                    "targets": [0, 3, 4, 5],
                    "createdCell": createdCell
                }],
                "select": {
                    "style": 'multi',
                    "className": "responstable"
                },
                "columns": [
                    {
                        "data": "active",
                        "orderable": false,
                        "searchable": false,
                        "render": function (data, type, row) {
                            if (type === 'display') {
                                data = '<input type="checkbox" class="chk"/>';
                            }
                            return data;
                        },
                        "targets": 0,
                        "className": "dt-body-center",
                        "width": "5%"
                    },
                    {
                        "data": "secuencia",
                        "searchable": false,
                        "orderable": false,
                        "targets": 1,
                        "sClass": "text-center",
                        "visible": false,
                        "title": "Secuencia",
                    },
                    {
                        "data": "idpunto",
                        "searchable": false,
                        "orderable": false,
                        "targets": 2,
                        "sClass": "text-center",
                        "visible": false
                    },
                    {
                        "data": "codigo",
                        "searchable": true,
                        "orderable": true,
                        "targets": 3,
                        "sClass": "text-center",
                        "visible": true,
                        "title": "Codigo"
                    },
                    {
                        "data": "nombre",
                        "searchable": true,
                        "orderable": true,
                        "targets": 4,
                        "sClass": "text-left",
                        "visible": true,
                        "title": "Punto"
                    },
                    {
                        "data": "descripcion",
                        "searchable": true,
                        "orderable": true,
                        "targets": 5,
                        "sClass": "text-left",
                        "title": "Descripción",
                        "visible": true,
                        render: function (data, type, row, meta) {
                            try {
                                if (data === '()') {
                                    return new String('S/D');
                                }
                                else {
                                    return data;
                                }
                            }
                            catch (Error) {
                                return data;
                            }
                        }
                    },
                    {
                        "data": "latitud",
                        "searchable": false,
                        "orderable": false,
                        "targets": 6,
                        "sClass": "text-center",
                        "visible": false,
                        render: function (data, type, row, meta) {
                            try {
                                if (data.length > 7) {
                                    return new String(data).substr(0, 7);
                                }
                                else {
                                    return data;
                                }
                            }
                            catch (Error) {
                                return data;
                            }
                        }
                    },
                    {
                        "data": "longitud",
                        "searchable": false,
                        "orderable": false,
                        "targets": 7,
                        "sClass": "text-center",
                        "visible": false,
                        render: function (data, type, row, meta) {
                            try {
                                if (data.length > 7) {
                                    return new String(data).substr(0, 7);
                                }
                                else {
                                    return data;
                                }
                            }
                            catch (Error) {
                                return data;
                            }
                        }
                    },
                    {
                        "data": "accion1",
                        "searchable": false,
                        "orderable": false,
                        "targets": 8,
                        "sClass": "text-center",
                        "visible": true,
                        "title": " ",
                        "width": "5%"
                    },
                    {
                        "data": "accion2",
                        "searchable": false,
                        "orderable": false,
                        "targets": 9,
                        "sClass": "text-center",
                        "visible": true,
                        "title": " ",
                        "width": "5%"
                    },
                    {
                        "data": "accion3",
                        "searchable": false,
                        "orderable": false,
                        "targets": 10,
                        "sClass": "text-center",
                        "visible": true,
                        "title": " ",
                        "width": "5%"
                    }
                ],
                "buttons": [
                    {
                        extend: 'excelHtml5',
                        className: 'btn btn-success btn-sm',
                        text: 'Excel',
                        title: 'Puntos',
                        width: "120px",
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5]
                        },
                    },
                ],

                "drawCallback": function (settings) {
                    var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
                    pagination.toggle(this.api().page.info().pages > 1);
                }
            });

            //testado2 = $('#grdflota2').DataTable({
            //    "language": {
            //        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json",
            //        "decimal": ".",
            //        "thousands": ",",
            //        "processing": "<span class='fa-stack fa-lg'>\n\<i class='fa fa-spinner fa-spin fa-stack-2x fa-fw'></i>\n\</span>&nbsp;&nbsp;&nbsp;&nbsp;Procesando ...",
            //    },
            //    "dom": "BtrSp",
            //    "order": [4, 'asc'],
            //    "paging": true,
            //    "scrollY": alto,
            //    "scrollX": true,
            //    "scrollCollapse": true,
            //    "filter": false,
            //    "serverSide": false,
            //    "responsive": true,
            //    "processing": true,
            //    "searching": true,
            //    "fixedHeader": {
            //        "header": true
            //    },
            //    "columnDefs": [{
            //        "defaultContent": "-",
            //        'targets': 0,
            //        'checkboxes': {
            //            'selectRow': true,
            //        },
            //    }],
            //    "select": {
            //        "style": 'multi',
            //        //"selector": 'td:first-child',
            //        "className": "responstable"
            //    },
            //    "columns": [
            //        {
            //            "data": "active",
            //            "orderable": false,
            //            "render": function (data, type, row) {
            //                if (type === 'display') {
            //                    data = '<input type="checkbox" class="chk2"/>';
            //                }
            //                return data;
            //            },
            //            "targets": 0,
            //            "className": "dt-body-center",
            //            "width": "5%"
            //        },
            //        {
            //            "data": "accion",
            //            "searchable": false,
            //            "orderable": false,
            //            "targets": 1,
            //            "sClass": "text-center",
            //            "visible": true,
            //            "title": " ",
            //            "width": "5%"
            //        },
            //        {
            //            "data": "secuencia",
            //            "searchable": false,
            //            "orderable": false,
            //            "targets": 2,
            //            "sClass": "text-center",
            //            "visible": false,
            //            "title": "Secuencia",
            //        },
            //        {
            //            "data": "idsubuser",
            //            "searchable": false,
            //            "orderable": false,
            //            "targets": 3,
            //            "sClass": "text-center",
            //            "visible": false
            //        },
            //        {
            //            "data": "subuser",
            //            "searchable": true,
            //            "orderable": true,
            //            "targets": 4,
            //            "sClass": "text-left",
            //            "visible": true,
            //            "title": "Subusuario"
            //        },
            //        {
            //            "data": "nombres",
            //            "searchable": true,
            //            "orderable": true,
            //            "targets": 5,
            //            "sClass": "text-left",
            //            "title": "Nombres",
            //            "visible": true,
            //            render: function (data, type, row, meta) {
            //                try {
            //                    if (data === '()') {
            //                        return new String('S/D');
            //                    }
            //                    else {
            //                        return data;
            //                    }
            //                }
            //                catch (Error) {
            //                    return data;
            //                }
            //            }
            //        },
            //    ],
            //    "buttons": [
            //        {
            //            extend: 'excelHtml5',
            //            className: 'btn btn-success btn-sm',
            //            text: 'Excel',
            //            title: 'Subusuarios',
            //            width: "120px",
            //            exportOptions: {
            //                columns: [2, 3, 4, 5]
            //            },
            //        },
            //    ],
            //    "drawCallback": function (settings) {
            //        var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
            //        pagination.toggle(this.api().page.info().pages > 1);
            //    }
            //});
        }

        function printRow(nVal, oVal, indice, celda) {
            //debugger;
            testado = $('#grdflota').DataTable();
            var print = testado.row(indice).data();
            if (celda == 1) {
                console.log("Row: " + print.idpunto + " : " + print.nombre + " : " + print.descripcion + " : " + nVal);
                document.getElementById("hdregs").value = print.idpunto + "," + nVal + "," + print.nombre + "," + print.descripcion + "," + celda;
            } else if(celda == 2){
                console.log("Row: " + print.idpunto + " : " + nVal + " : " + print.descripcion + " : " + print.codigo);
                document.getElementById("hdregs").value = print.idpunto + "," + print.codigo + "," + nVal + "," + print.descripcion + "," + celda;
            }else if(celda == 3){
                console.log("Row: " + print.idpunto + " : " + print.nombre + " : " + nVal + " : " + print.codigo);
                document.getElementById("hdregs").value = print.idpunto + "," + print.codigo + "," + print.nombre + "," + nVal + "," + celda;
            }
            gridGrupo.PerformCallback('actualizar');
        }

        var countChecked = function ($table, checkboxClass) {
            if ($table) {
                var chkAll = $table.find(checkboxClass);
                var checked = chkAll.filter(':checked').length;
                return {
                    checked: checked
                }
            }
        }
        $(document).on('change', '.chk', function () {
            var result = countChecked($('#grdflota'), '.chk');
            countChk = result.checked;
            console.log(countChk);
            selectionPunto(countChk);
        });

        //var countChecked2 = function ($table, checkboxClass) {
        //    if ($table) {
        //        var chkAll = $table.find(checkboxClass);
        //        var checked = chkAll.filter(':checked').length;
        //        return {
        //            checked: checked
        //        }
        //    }
        //}
        //$(document).on('change', '.chk2', function () {
        //    var result = countChecked($('#grdflota2'), '.chk2');
        //    countChk = result.checked;
        //    selectionPunto2(countChk);
        //});

        //$(document).on('click', '.responstable tr', function () {
        //    if ($(this).hasClass('row_selected'))
        //        $(this).removeClass('row_selected');
        //    else
        //        $(this).addClass('row_selected');
        //});

        //$(document).on('click', '.responstable tr', function () {
        //    debugger;
        //    var chk = $("[type='checkbox']", this);
        //    setCheck(chk, !$(this).hasClass("selected"));
        //});

        function jsGetDataTableHeightPx() {
            var retHeightPx = 360;
            var dataTable = document.getElementById("grdflota");
            if (!dataTable) {
                return retHeightPx;
            }

            var pageHeight = document.body.scrollHeight;
            if (pageHeight < 0) {
                return retHeightPx;
            }

            var dataTableHeight = pageHeight - 120;
            retHeightPx = Math.max(100, dataTableHeight);

            return retHeightPx;
        }

        function llenarPoints() {
            var listPoints = [];
            var data = testado.rows({ selected: true }).data();

            for (var i = 0; i < data.length; i++) {
                listPoints.push(data[i].idpunto + ',' + data[i].nombre);
            }

            var sData = listPoints.join();
            RecieveGridPuntoValues(sData);
        }
        function llenarPointsMapa() {
            var listPoints = [];
            var data = testado.rows({ selected: true }).data();

            for (var i = 0; i < data.length; i++) {
                listPoints.push(data[i].idpunto + ',' + data[i].nombre + ',' + data[i].latitud + ',' + data[i].longitud + ',' + data[i].secuencia);
            }

            var sData = listPoints.join();
            RecieveGridPuntoValues2(sData);
        }
        function llenarPoints2() {
            var listPoints = [];
            var data = testado2.rows({ selected: true }).data();

            for (var i = 0; i < data.length; i++) {
                listPoints.push(data[i].idsubuser);
            }

            var sData = listPoints.join();
            RecieveGridSubusuarioValues(sData);
        }

        function deselectPoints() {
            var table = $('#grdflota').DataTable();
            table.rows({ selected: true }).deselect();

            var items = document.getElementsByClassName('chk');
            for (var i = 0; i < items.length; i++) {
                if (items[i].type == 'checkbox')
                    items[i].checked = false;
            }

            var result = countChecked($('#grdflota'), '.chk');
            countChk = result.checked;

            selectionPunto(countChk);
        }
        //function deselectPoints2() {
        //    var table = $('#grdflota2').DataTable();
        //    table.rows({ selected: true }).deselect();

        //    var items = document.getElementsByClassName('chk2');
        //    for (var i = 0; i < items.length; i++) {
        //        if (items[i].type == 'checkbox')
        //            items[i].checked = false;
        //    }

        //    var result2 = countChecked($('#grdflota2'), '.chk2');
        //    countChk2 = result2.checked;

        //    selectionPunto2(countChk2);
        //}

        function limpiargrid() {
            $('#grdflota').DataTable().clear().draw();
        }
        //function limpiargrid2() {
        //    $('#grdflota2').DataTable().clear().draw();
        //}

        function llenargrid() {
            try {
                var ind = 1;
                var pto;
                apuntos = JSON.parse(gridGrupo.cpJSONgroupT);
                testado.clear();

                for (var i = 0; i < apuntos.length; i++) {
                    try {
                        if (apuntos[i].id === '')
                            pto = 'SP';
                        else
                            pto = encodeURI(apuntos[i].id);

                        testado.row.add(new clspunto(ind, apuntos[i].pid, apuntos[i].puid, apuntos[i].id, apuntos[i].data, apuntos[i].lat, apuntos[i].lon,
                            '<img id="imgPoint1" src="../../images/mapapunto.png" height="15px" width="18px" title="Mostrar Mapa Punto" onclick="OnMostrarPto2(' + '\'' + String(apuntos[i].pid) + '\'' + ')" />',
                            '<img id="imgPoint1" src="../../images/pencil.png" height="15px" width="15px" title="Editar Datos Punto" onclick="OnEditarPto2(' + '\'' + String(apuntos[i].pid) + '\'' + ')" />',
                            '<img id="imgPoint2" src="../../images/trash.png" height="15px" width="15px" title="Eliminar Punto" onclick="OnEliminaPto(' + '\'' + String(apuntos[i].pid) + '\'' + ',' + '\'' + String(pto) + '\'' + ')" />'));
                    } catch (Error) {
                        console.log(Error);
                    }
                    ind += 1;
                }

                ind = null;
                info = null;

                testado.columns.adjust().draw();
            }
            catch (Error) {
                console.log(Error);
            }
        }
        //function llenargridS() {
        //    try {
        //        var ind = 1;
        //        var pto;
        //        apuntosS = JSON.parse(gridGrupo.cpJSONgroupST);
        //        testado2.clear();
        //        for (var i = 0; i < apuntosS.length; i++) {
        //            try {
        //                if (apuntosS[i].id === '')
        //                    pto = 'SSB';
        //                else
        //                    pto = encodeURI(apuntosS[i].id);

        //                testado2.row.add(new clssubuser('<img id="imgPoint" src="../../images/tags.png" height="12px" width="15px" onclick="OnMoreInfoClickS(' + '\'' + String(apuntosS[i].sid) + '\'' + ',' + '\'' + String(pto) + '\'' + ')" />',
        //                    ind, apuntosS[i].sid, apuntosS[i].id, apuntosS[i].data));
        //            } catch (Error) {
        //                console.log(Error);
        //            }
        //            ind += 1;
        //        }

        //        ind = null;
        //        info = null;

        //        testado2.columns.adjust().draw();
        //    }
        //    catch (Error) {
        //        console.log(Error);
        //    }
        //}

        function llenargrid2() {
            try {
                var ind = 1;
                var pto;
                apuntos2 = JSON.parse(gridGrupo.cpJSONgroup);
                testado.clear();
                //debugger;
                for (var i = 0; i < apuntos2.length; i++) {
                    try {
                        if (apuntos2[i].id === '')
                            pto = 'SP';
                        else
                            pto = encodeURI(apuntos2[i].id);

                        testado.row.add(new clspunto(ind, apuntos2[i].pid, apuntos2[i].puid, apuntos2[i].id, apuntos2[i].data, apuntos2[i].lat, apuntos2[i].lon,
                            '<img id="imgPoint1" src="../../images/mapapunto.png" height="15px" width="18px" title="Mostrar Mapa Punto" onclick="OnMostrarPto2(' + '\'' + String(apuntos2[i].pid) + '\'' + ')" />',
                            '<img id="imgPoint1" src="../../images/pencil.png" height="15px" width="15px" title="Editar Datos Punto" onclick="OnEditarPto2(' + '\'' + String(apuntos2[i].pid) + '\'' + ')" />',
                            '<img id="imgPoint2" src="../../images/trash.png" height="15px" width="15px" title="Eliminar Punto" onclick="OnEliminaPto(' + '\'' + String(apuntos2[i].pid) + '\'' + ',' + '\'' + String(pto) + '\'' + ')" />'));
                    } catch (Error) {
                        console.log(Error);
                    }
                    ind += 1;
                }

                ind = null;
                info = null;

                testado.columns.adjust().draw();
            }
            catch (Error) {
                console.log(Error);
            }
        }
        //function llenargridS2() {
        //    try {
        //        var ind = 1;
        //        var pto;
        //        apuntosS2 = JSON.parse(gridGrupo.cpJSONgroupS);
        //        testado2.clear();

        //        for (var i = 0; i < apuntosS2.length; i++) {
        //            try {
        //                if (apuntosS2[i].id === '')
        //                    pto = 'SP';
        //                else
        //                    pto = encodeURI(apuntosS2[i].id);

        //                testado2.row.add(new clssubuser('<img id="imgPoint" src="../../images/tags.png" height="12px" width="15px" onclick="OnMoreInfoClickS(' + '\'' + String(apuntosS2[i].sid) + '\'' + ',' + '\'' + String(pto) + '\'' + ')" />',
        //                    ind, apuntosS2[i].sid, apuntosS2[i].id, apuntosS2[i].data));
        //            } catch (Error) {
        //                console.log(Error);
        //            }
        //            ind += 1;
        //        }

        //        ind = null;
        //        info = null;

        //        testado2.columns.adjust().draw();
        //    }
        //    catch (Error) {
        //        console.log(Error);
        //    }
        //}

        function filterGlobal() {
            $('#grdflota').DataTable().search($('#global_filter').val(), false, true).draw();
        }

        function resizeTable(altura) {
            try {
                var alto, page;
                if (altura >= 500 && altura < 700) {
                    alto = '55vh'
                    page = 10;
                } else if (altura >= 700 && altura < 800) {
                    alto = '55vh'
                    page = 12;
                } else if (altura >= 800) {
                    alto = '90vh'
                    page = 19;
                }
                var oSettings = $('#grdflota').dataTable().fnSettings();
                oSettings.oScroll.sY = alto;
                oSettings._iDisplayLength = page;
                $('#grdflota').dataTable().fnDraw();
            }
            catch (Error) {
                console.log(Error);
            }
        }
    </script>
</head>
<body class="container-fluid">
    <form id="frmPuntos" runat="server">
        <asp:ScriptManager ID="smMaster" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <div id="cabecera" class="hstack gap-3 flex" style="margin: 5px 5px 5px 5px; background-color: #f2f1f0; padding: 10px; border-radius: 5px;">
            <div class="col-sm-3" style="display:flex; margin-right: 5px">
                <dx:ASPxButton ID="btnShow" runat="server" ClientInstanceName="btnShow" AutoPostBack="False" CssClass="mybuttonmenu">
                    <ClientSideEvents Click="function(s, e) {shTable()}"/>
                </dx:ASPxButton>
                <image src="../../images/point.png" style="width:25px; height:30px; margin: 5px 10px 0px 10px;"/>
                <h4 class="mt-1" style="font-weight: bold;">Puntos</h4>
            </div>
            <div runat="server" class="w-100">
                <input type="text" id="global_filter" placeholder="Buscar puntos" column="1" class="global_filter form-control w-100 glossyBtn" style="padding-left: 40px;"/>
                <%--<input type="text" id="global_filter2" placeholder="Buscar subusuario" column="1" class="global_filter2 form-control w-100 glossyBtn" style="padding-left: 40px; display: none"/>
                <dx:ASPxTextBox ID="txSearchPto" runat="server" ClientInstanceName="txSearchPto" NullText="Buscar Punto / Subusuario ..." Paddings-PaddingLeft="40"
                                AutoPostBack="false" CssClass="mytext" Width="70%" Height="40px" Font-Size="Small">
                    <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
                </dx:ASPxTextBox>--%>
            </div>
        </div>
        <div id="cuerpo" class="row" style="margin: 5px 5px 0px 5px; padding: 10px; background-color: #f2f1f0; border-radius: 5px;">
            <div id="lateral" class="col-sm-3" style="display:inline-block">
                <div class="col-md w-100">
                    <dx:ASPxButton ID="btnTodos" runat="server" ClientInstanceName="btnTodos" Text="Puntos" Width="100px" Font-Bold="true" Font-Size="10" 
                                   CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="false" ToolTip="Mostrar todos los puntos"
                                   BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                        <ClientSideEvents Click="UpdateGrids"/>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="lbCount" runat="server" ClientInstanceName="lbCount" EncodeHtml="false" Text=" " Font-Bold="true" Font-Size="10" 
                                   CssClass="mybuttongrid" AutoPostBack="false" HoverStyle-BackColor="Transparent" HoverStyle-ForeColor="Transparent"/>
                    <dx:ASPxButton ID="btnPtoN" runat="server" ClientInstanceName="btnPtoN" AutoPostBack="false" CssClass="mybuttongrid2" ToolTip="Nuevo Punto"
                                   Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" Image-Url="../../images/plus.png" Image-Height="25" Image-Width="25">
                        <ClientSideEvents Click="OnMostrarPto"/>
                    </dx:ASPxButton>
                </div>
                
                <%--<dx:ASPxButton ID="btnTodos2" runat="server" ClientInstanceName="btnTodos2" Text=" " Width="70%" Font-Bold="true" Font-Size="10" 
                               CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="false" ClientVisible="false" ToolTip="Mostrar todos los subusuarios"
                               BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                    <ClientSideEvents Click="UpdateGrids"/>
                </dx:ASPxButton>--%>
                
                <hr class="solid"/>
                <div id="panelGrupo">
                    <asp:Accordion ID="accPanel" runat="server" SelectedIndex="0" AutoSize="None" FadeTransitions="true" TransitionDuration="150" FramesPerSecond="10"
                               RequireOpenedPane="false" SuppressHeaderPostbacks="true" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected">
                        <Panes>
                            <asp:AccordionPane ID="AccordionPane1" runat="server">  
                                <Header>Grupos
                                    <dx:ASPxButton ID="btnGrupoN" runat="server" ClientInstanceName="btnGrupoN" AutoPostBack="false" CssClass="mybuttongrid2f" ToolTip="Nuevo Grupo" 
                                                   Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" OnInit="btnGrupoN_Init">
                                        <Image Url="../../images/plus.png" Width="20" Height="20"/>
                                    </dx:ASPxButton>
                                </Header>
                                <Content>
                                    <dx:ASPxGridView ID="gridGrupo" ClientInstanceName="gridGrupo" runat="server" KeyFieldName="id" OnHtmlDataCellPrepared="gridGrupo_HtmlDataCellPrepared"
                                                     Width="98%" AutoGenerateColumns="False" OnDataBinding="gridG_DataBinding" Border-BorderStyle="None" EnableCallBacks="true" CssClass="grid" 
                                                     OnCustomColumnDisplayText="gridGrupo_CustomColumnDisplayText" OnPreRender="gridGrupo_PreRender" OnCustomCallback="gridGrupo_CustomCallback">
                                        <%--OnFocusedRowChanged="gridGrupo_FocusedRowChanged"--%>
                                        <ClientSideEvents FocusedRowChanged="UpdateGridPunto" Init="InitGridPunto" EndCallback="EndCallbackGridPunto" RowDblClick="UpdateGrids"/>
                                        <Styles>  
                                            <Header HorizontalAlign="Center" Border-BorderStyle="None"></Header>  
                                            <Row BackColor="#f2f1f0"/> <FocusedRow BackColor="#ff4242" ForeColor="White"/>
                                        </Styles>
                                        <%--<Toolbars>
                                            <dx:GridViewToolbar Name="toolbar">
                                                <SettingsAdaptivity Enabled="true" EnableCollapseRootItemsToIcons="true" />
                                                <Items>
                                                    <dx:GridViewToolbarItem Alignment="Right">
                                                        <Template>
                                                            <dx:ASPxButton ID="btnGrupoN" runat="server" ClientInstanceName="btnGrupoN" AutoPostBack="false" CssClass="mybuttongrid" ToolTip="Nuevo Grupo" 
                                                                           Border-BorderStyle="None" HoverStyle-BackColor="#DB0632" OnInit="btnGrupoN_Init">
                                                                <Image Url="../../images/plus.png" Width="15" Height="15"/>
                                                            </dx:ASPxButton>
                                                        </Template>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>--%>
                                        <Columns>
                                            <dx:GridViewDataColumn Name="btedit" VisibleIndex="3" Width="20">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dxe:ASPxButton ID="editar" runat="server" AutoPostBack="false" ClientInstanceName="editar" Visible="true" ToolTip="Editar Grupo"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" 
                                                                    HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid" OnInit="editar_Init">
                                                    </dxe:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Name="btdel" VisibleIndex="4" Width="20">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dxe:ASPxButton ID="borrar" runat="server" AutoPostBack="False" ClientInstanceName="borrar" Visible="true" ToolTip="Eliminar Grupo"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" 
                                                                    HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid" OnInit="borrar_Init">
                                                    </dxe:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords"/>
                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <%--<SettingsSearchPanel Visible="False" CustomEditorID="txSearch"/>--%>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="200"/>
                                    </dx:ASPxGridView>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                </div>
            </div>
            <div id="central" runat="server" class="col" style="display:block">
                <dx:ASPxButton ID="btnAsignar" runat="server" Width="6%" ClientInstanceName="btnAsignar" ClientVisible="false" ToolTip="Asignar Grupos"
                               CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="18" Image-Height="22" Image-Url="../../images/pointasig.png" HoverStyle-BackColor="#dadad2"
                               AutoPostBack="False" ><%--OnInit="btnAsignar_Init"--%>
                    <ClientSideEvents Click="OnMoreInfoClick2"/>
                </dx:ASPxButton>
                <%--<dx:ASPxButton ID="btnAsignarS" runat="server" Width="6%" ClientInstanceName="btnAsignarS" ClientVisible="false" ToolTip="Asignar Grupos"
                               CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="18" Image-Height="22" Image-Url="../../images/pointasig2.png" HoverStyle-BackColor="#dadad2"
                               AutoPostBack="False" > OnInit="btnAsignarS_Init"
                    <ClientSideEvents Click="OnMoreInfoClickS2"/>
                </dx:ASPxButton>--%>
                <hr class="solid2"/>
                <dx:ASPxPageControl ID="pgGrids" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids" EnableCallBacks="false"
                                    OnInit="pgGrids_Init" BackColor="#f2f1f0">
                    <ClientSideEvents ActiveTabChanged="InitButton" Init="onInicioTab"/>
                    <TabPages>
                        <dx:TabPage Name="tbPuntos" Text="PUNTOS" ActiveTabStyle-Font-Bold="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ccPuntos" runat="server" BackColor="#f2f1f0">
                                    <%--<dx:ASPxGridView ID="gridPunto" ClientInstanceName="gridPunto" runat="server" KeyFieldName="pid" CssClass="grid" Width="100%"
                                                     AutoGenerateColumns="False" OnDataBinding="gridP_DataBinding" Border-BorderStyle="None" EnableCallBacks="true" 
                                                     OnCustomCallback="gridPunto_CustomCallback" EnableCallbackAnimation="false" EnableCallbackCompression="false" 
                                                     EnablePagingCallbackAnimation="false">   
                                        <ClientSideEvents Init="onInit" EndCallback="onEndCallback" SelectionChanged="grid_SelectionChangedPunto"/>
                                        <Styles>  
                                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                            <Cell BorderBottom-BorderStyle="Solid"/>
                                            <Row BackColor="#f2f1f0"/>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" AdaptivePriority="0" VisibleIndex="0" SelectAllCheckboxMode="Page"
                                                                      CellStyle-HorizontalAlign="Center" Width="40" HeaderStyle-Border-BorderStyle="None" CellStyle-Border-BorderStyle="None"/>
                                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btasig" VisibleIndex="0" MaxWidth="80" AdaptivePriority="0">
                                                <HeaderStyle> <Border BorderStyle="None"/> </HeaderStyle>
                                                <CellStyle> <Border BorderStyle="None"/> </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="asignar" runat="server" AutoPostBack="False" ClientInstanceName="asignar" Visible="true" RenderMode="Button"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="12" Image-Url="../../images/tags.png" ToolTip="Asignar Grupo"
                                                                    OnInit="asignar_Init" HoverStyle-BackColor="#DB0632" CssClass="mybuttongrid">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchPto"/>
                                        <SettingsBehavior AllowSelectByRowClick="true"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                        
                                    </dx:ASPxGridView>--%>

                                    <table id="grdflota" class="responstable w-100" cellspacing="0" width="100%" style="display:none; width:100%; height:100%; overflow-x:auto;">
                                        <thead>
                                            <tr>
                                                <th class="check"><input type="checkbox" value="" id="selectAll"/></th>
                                                <th></th>
                                                <th></th>
                                                <th>Codigo</th>
                                                <th>Punto</th>
                                                <th>Descripcion</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <%--<dx:TabPage Name="tbSubusuarios" Text="SUBUSUARIOS" ActiveTabStyle-Font-Bold="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ccSubusuarios" runat="server">
                                    <dx:ASPxGridView ID="gridSubusuario" ClientInstanceName="gridSubusuario" runat="server" KeyFieldName="sid" CssClass="grid" Width="100%"
                                                    AutoGenerateColumns="False" OnDataBinding="gridS_DataBinding" Border-BorderStyle="None" EnableCallBacks="true"
                                                    OnCustomCallback="gridSubusuario_CustomCallback" EnableCallbackAnimation="false" EnableCallbackCompression="false" 
                                                    EnablePagingCallbackAnimation="false">
                                        <ClientSideEvents Init="onInit" EndCallback="onEndCallback" SelectionChanged="grid_SelectionChangedSubusuario" />
                                        <Styles>  
                                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                            <Cell BorderBottom-BorderStyle="Solid"/>
                                            <Row BackColor="#f2f1f0"/>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" AdaptivePriority="0" VisibleIndex="0" SelectAllCheckboxMode="Page" 
                                                                      CellStyle-HorizontalAlign="Center" Width="40" HeaderStyle-Border-BorderStyle="None" CellStyle-Border-BorderStyle="None"/>
                                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btasig" VisibleIndex="0" MaxWidth="80" AdaptivePriority="0">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="asignarS" runat="server" AutoPostBack="False" ClientInstanceName="asignarS" Visible="true" RenderMode="Button"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="12" Image-Url="../../images/tags.png" ToolTip="Asignar Grupo"
                                                                    OnInit="asignarS_Init" HoverStyle-BackColor="#DB0632" CssClass="mybuttongrid">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchPto"/>
                                        <SettingsBehavior AllowSelectByRowClick="true"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                    </dx:ASPxGridView>
                                    <table id="grdflota2" class="responstable w-100" cellspacing="0" width="100%" style="display:none; width:100%; height:100%; overflow-x:auto;">
                                        <thead>
                                            <tr>
                                                <th><input type="checkbox" value="" id="selectAll2"/></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>Subusuario</th>
                                                <th>Nombres</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                            
                        </dx:TabPage>--%>

                        <%--<dx:TabPage Name="tbPuntos2" Text="PUNTOS NEW" ActiveTabStyle-Font-Bold="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" BackColor="#f2f1f0">
                                    <dx:ASPxGridView ID="gridPunto" ClientInstanceName="gridPunto" runat="server" KeyFieldName="IdPunto" CssClass="grid" Width="100%"
                                                     AutoGenerateColumns="False" DataSourceID="EntityServerModeDataSource" Border-BorderStyle="None" EnableCallBacks="true" 
                                                     EnableCallbackAnimation="false" EnableCallbackCompression="false" EnablePagingCallbackAnimation="false"> 
                                        <Styles>  
                                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                            <Cell BorderBottom-BorderStyle="Solid"/>
                                            <Row BackColor="#f2f1f0"/>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" AdaptivePriority="0" VisibleIndex="0" SelectAllCheckboxMode="Page"
                                                                      CellStyle-HorizontalAlign="Center" Width="40" HeaderStyle-Border-BorderStyle="None" CellStyle-Border-BorderStyle="None"/>
                                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btasig" VisibleIndex="0" MaxWidth="80" AdaptivePriority="0">
                                                <HeaderStyle> <Border BorderStyle="None"/> </HeaderStyle>
                                                <CellStyle> <Border BorderStyle="None"/> </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="asignar" runat="server" AutoPostBack="False" ClientInstanceName="asignar" Visible="true" RenderMode="Button"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="12" Image-Url="../../images/tags.png" ToolTip="Asignar Grupo"
                                                                    OnInit="asignar_Init" HoverStyle-BackColor="#DB0632" CssClass="mybuttongrid">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchPto"/>
                                        <SettingsBehavior AllowSelectByRowClick="true"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                        
                                    </dx:ASPxGridView>
                                    <dxq:EntityServerModeDataSource ID="EntityServerModeDataSource" runat="server" ContextTypeName="ArtemisAdmin.PX_DBEntities" TableName="Puntos_Referencia" 
                                       OnSelecting="EntityServerModeDataSource_Selecting" OnExceptionThrown="EntityServerModeDataSource_ExceptionThrown" OnInconsistencyDetected="EntityServerModeDataSource_InconsistencyDetected"/>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>--%>

                    </TabPages>
                </dx:ASPxPageControl>
            </div>
        </div>

        <%--PopupModalGrupo--%>
        <dx:ASPxPopupControl ID="pcGrupo" runat="server" Width="500" Height="230" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcGrupo"
                             HeaderText="Grupo" AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupMenuGrupoPuntos--%>
        <dx:ASPxPopupControl ID="pmGrupo" runat="server" ClientInstanceName="pmGrupo" Width="380px" Height="420px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign ="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true">
            <ClientSideEvents CloseButtonClick="UpdateGridPunto2"/>

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        
        <%--PopupMenuGrupoPuntos2--%>
        <dx:ASPxPopupControl ID="pmGrupo2" runat="server" ClientInstanceName="pmGrupo2" Width="380px" Height="450px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true" >
            <%--<ClientSideEvents CloseButtonClick="deselectPoints" CloseUp="deselectPoints"/>--%>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--PopupMenuGrupoSubusuarios
        <dx:ASPxPopupControl ID="pmGrupo3" runat="server" ClientInstanceName="pmGrupo3" Width="380px" Height="420px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>--%>

        <%--PopupMenuGrupoSubusuarios2
        <dx:ASPxPopupControl ID="pmGrupo4" runat="server" ClientInstanceName="pmGrupo4" Width="380px" Height="450px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true" OnWindowCallback="pmGrupo4_WindowCallback">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>--%>

        <%--PopupEliminarGrupo--%>
        <dx:ASPxPopupControl ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>   
        
        <%--PopupModalMapa--%>
        <dx:ASPxPopupControl ID="pmMapa" runat="server" Width="600" Height="580" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pmMapa"
                             HeaderText="Mapa Punto" AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
            <%--<ClientSideEvents CloseButtonClick="onCloseBtn"/>--%>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" MinHeight="450px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEliminarPto--%>
        <dx:ASPxPopupControl ID="popupDelPto" runat="server" ClientInstanceName="popupDelPto" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEditarPto--%>
        <dx:ASPxPopupControl ID="popupEdtPto" runat="server" ClientInstanceName="popupEdtPto" Width="420" Height="350" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" MinHeight="350px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        
        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" BrowserWindowResized="OnBrowserWindowResized"/>
        </dx:ASPxGlobalEvents>

        <asp:HiddenField ID="hfIndex" runat="server"/>
        <asp:HiddenField ID="hfIndex2" runat="server"/>
        <asp:HiddenField ID="hfHeight" runat="server"/>
        <asp:HiddenField ID="hfCadena" runat="server"/>

        <asp:HiddenField ID="hpuntos" runat="server"/>
        <asp:HiddenField ID="hsubuser" runat="server"/>

        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />
        <asp:HiddenField runat="server" ID="hdregs"/>
        <asp:HiddenField runat="server" ID="hvalida"/>

    </form>
</body>
</html>
