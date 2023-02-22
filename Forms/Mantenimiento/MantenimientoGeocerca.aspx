<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoGeocerca.aspx.vb" Inherits="ArtemisAdmin.MantenimientoGeocerca" %>
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
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <script src="../../Libs/js/jquery-1.9.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
    
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.14/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-control-geocoder@latest/dist/Control.Geocoder.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.8.0/dist/leaflet.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-draw@0.4.1/dist/leaflet.draw.css" />
    <link href="../../Libs/leaflet/css/leaflet.marker.highlight.css" rel="stylesheet" />
    
    <script src="../../Libs/js/x-notify.min.js"></script>
    <link rel="stylesheet" href="../../Libs/css/notification.css" />
    <script src="../../Libs/js/notification.js"></script>

    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title>GEOCERCAS</title>

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

        .mybuttonmenu {  width: 40px; 
                         height: 40px; 
                         border-radius: 25px 25px 25px 25px; 
                         border: none;
                         background: url('../../images/menucol.png') no-repeat 8% 8%;
        }

        .mybutton2p { border-radius: 0px 25px 25px 0px; text-align:left; border: none; }
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
        .accordionContent 
        {
            height: 100%;
        }
        #resize_wrapper {
            position: absolute;
			top: 5em;
			left: 1em;
			right: 1em;
			bottom: 1em;
        }

    </style>

    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            document.getElementById('clWidth').value = window.innerWidth;
            document.getElementById('clHeight').value = window.innerHeight;
        });
    </script>

    <script type="text/javascript">
        var isPageSizeCallbackRequired = false;
        const notification = new Notification();
        var showPopup = true;
        var iframe;

        //const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);

        var geocercas, geocercas2;
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
        function EliminaHide2() {
            popupDelGeo.Hide();
        }

        //GridView SelectedChanged
        function grid_SelectionChangedGeocerca(s, e) {
            var contar = s.GetSelectedRowCount();
            var opc = document.getElementById("hdregs").value;

            if (opc == "1") {
                s.GetSelectedFieldValues("id;gid", RecieveGridGeocercaValues);
                if (contar > 0) {
                    if (!btnAsignar.GetClientVisible()) {
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                    }
                } else {
                    if (btnAsignar.GetClientVisible())
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                }
                s.GetSelectedFieldValues("id;gid;data;gpa", RecieveGridGeocercaValues2);
            } 
            
        }
        function RecieveGridGeocercaValues(values) {
            geocercas = values;
        }
        function RecieveGridGeocercaValues2(values) {
            geocercas2 = values;
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
        //    subusuarios = values;
        //}

        //Primero END y luego SAVE
        function onBatchStar(s, e) {
            if (e.focusedColumn.fieldName == "data")
                e.cancel = true;
            var keyIndex = s.GetColumnByField("data").index;
            var key = e.rowValues[keyIndex].value;
            if (e.focusedColumn.fieldName == "gpa" && key == "Poligonal") {
                e.cancel = true;
            }
        }
        function onBatchEnd(s, e) {
            setTimeout(function () {
                if (s.batchEditApi.HasChanges()) {
                    s.UpdateEdit();
                }
            }, 500);
        }
        function onCloseBtn(s, e) {
            var opc = document.getElementById("hdregs").value;
            
            if (opc == "1") {
                if (gridGrupo.GetFocusedRowIndex() == -1)
                    gridGeocerca.PerformCallback('geocercas');
                else
                    gridGeocerca.PerformCallback('1');
            } else {
                gridGeocerca.PerformCallback('geocercas');
            }
            
        }

        //GridGrupo
        function UpdateGridGeocerca(s, e) {
            gridGeocerca.PerformCallback('1');
            //gridSubusuario.PerformCallback('1');
        }

        function UpdateGrids(s, e) {
            gridGrupo.SetFocusedRowIndex(-1);
            gridGeocerca.PerformCallback('geocercas');
            //gridSubusuario.PerformCallback('subusuarios');

            var geocerca = hgeocercas.value;
            //var subuser = hsubuser.value;

            //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Geocercas</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + geocerca + '</span></div> </div>');
            //btnTodos2.SetText('<div class="row"> <div class="col-sm-6" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-2"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
            lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + geocerca + '</span></div>');
        }
        function UpdateGridsText(s, e) {
            var aux = txSearch.GetText();
            if (pgGrids.GetActiveTab().name == 'tbGeocercas') {
                if (aux.length >= 1) {
                    gridGeocerca.PerformCallback('cfgeocercas');
                } else {
                    gridGeocerca.PerformCallback('sfgeocercas');
                    gridGrupo.SetFocusedRowIndex(-1);
                }
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridGrupo.SetFocusedRowIndex(-1);
            //    //gridSubusuario.PerformCallback('subusuarios');
            //}
        }
        function InitButton(s, e) {
            if (pgGrids.GetActiveTab().name == 'tbGeocercas') {
                btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
                //btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
                //if (btnAsignarS.GetClientVisible()) {
                //    btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
                //    //gridSubusuario.UnselectRows();
                //}
                btnGeoN.SetClientVisible(!btnGeoN.GetClientVisible());
                //btnUpload.SetClientVisible(!btnUpload.GetClientVisible());
                changeNullText(txSearchGeo, "Buscar geocercas ...")
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
            //    //btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
            //    if (btnAsignar.GetClientVisible()) {
            //        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
            //        gridGeocerca.UnselectRows();
            //    }
            //    btnGeoN.SetClientVisible(!btnGeoN.GetClientVisible());
            //    //btnUpload.SetClientVisible(!btnUpload.GetClientVisible());
            //    changeNullText(txSearchGeo, "Buscar subusuarios ...")
            //}
        }
        function changeNullText(textBox, value) {
            var mainTbEl = textBox.GetMainElement();
            if (mainTbEl.getElementsByClassName('dxeFNTextSys')[0]) {
                mainTbEl.getElementsByClassName('dxeFNTextSys')[0].value = value;
            }
            textBox.GetInputElement().value = value;
        }

        function onInicioTab(s, e) {
            var opc = document.getElementById("hdregs").value;
            var tabActivo = pgGrids.GetTab(0);
            //var tabSbuser = pgGrids.GetTab(1);
            if (opc == "1") {
                tabActivo.SetVisible(true);
                //tabSbuser.SetVisible(true);
                document.getElementById("panelGrupo").style.display = '';
            } else {
                tabActivo.SetVisible(true);
                //tabSbuser.SetVisible(false);
                document.getElementById("panelGrupo").style.display = 'none';
            }
        }
        function OnDblClick(s, e){
            gridGrupo.SetFocusedRowIndex(-1);
            gridGeocerca.PerformCallback('geocercas');
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
        function OnMoreInfoClick(contentUrl) {
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
            pmGrupo.SetContentUrl(contentUrl);
            pmGrupo.Show();
        }
        function OnMoreInfoClick2(s, e) {
            var contentUrl = "AsignarGrupoGeo2.aspx?valores=" + geocercas;
            showPopup = true;
            pmGrupo2.SetContentUrl(contentUrl);
            pmGrupo2.Show();
        }

        //function OnMoreInfoClickS(contentUrl) {
        //    var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        //    pmGrupo3.SetContentUrl(contentUrl);
        //    pmGrupo3.Show();
        //}
        //function OnMoreInfoClickS2(s, e) {
        //    var contentUrl = "AsignarGrupoGeoS2.aspx?valores=" + subusuarios;
        //    pmGrupo4.SetContentUrl(contentUrl);
        //    pmGrupo4.Show();
        //}

        function OnMostrarGeo(s, e) {
            var contentUrl = "MapaGeocerca.aspx?data=nueva";
            pmMapa.SetContentUrl(contentUrl);
            pmMapa.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function OnUploadGeo(s, e) {
            var contentUrl = "UploadGeocerca.aspx";
            pmUpload.SetContentUrl(contentUrl);
            pmUpload.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        
        function HidePopup() {
            pmGrupo2.Hide();
            gridGeocerca.UnselectRows();
            if (gridGrupo.GetFocusedRowIndex() > -1) {
                gridGeocerca.PerformCallback('1');
            } else if (gridGrupo.GetFocusedRowIndex() == -1) {
                gridGeocerca.PerformCallback('geocercas');
            }
        }
        //function HidePopup2() {
        //    pmGrupo4.Hide();
        //    gridSubusuario.UnselectRows();
        //    if (gridGrupo.GetFocusedRowIndex() > -1) {
        //        gridSubusuario.PerformCallback('1');
        //    } else if (gridGrupo.GetFocusedRowIndex() == -1) {
        //        gridSubusuario.PerformCallback('subusuarios');
        //    }
        //}

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
        function ShowMessage2(contentUrl) {
            popupDelGeo.SetContentUrl(contentUrl);
            popupDelGeo.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function ShowMessage3(contentUrl) {
            pmMapa.SetContentUrl(contentUrl);
            pmMapa.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }

        function HidePopup3() {
            pcGrupo.Hide();
            gridGrupo.PerformCallback('1');
        }
        function HidePopup4() {
            popupDelete.Hide();
            gridGrupo.PerformCallback('1');
            gridGeocerca.PerformCallback('geocercas');
            //gridSubusuario.PerformCallback('subusuarios');
        }

        function HidePopup5() {
            popupDelGeo.Hide();
            //gridGrupo.PerformCallback('1');
            gridGeocerca.PerformCallback('geocercas');
            //gridSubusuario.PerformCallback('subusuarios');
        }
        function HidePopup6() {
            //debugger;
            var opc = document.getElementById("hdregs").value;
            pmMapa.Hide();
            if (opc == "1") {
                if (gridGrupo.GetFocusedRowIndex() == -1)
                    gridGeocerca.PerformCallback('geocercas');
                else
                    gridGeocerca.PerformCallback('1');
            } else {
                gridGeocerca.PerformCallback('geocercas');
            }
        }
        function OnBatchStartEdit(s, e) {
            var editor = s.GetEditor(e.focusedColumn.fieldName);
            editor.SetEnabled(e.visibleIndex > 0);
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
            //debugger;
            if (isPageSizeCallbackRequired === true) {
                UpdateGridHeight();
            }

            if (gridGrupo.GetFocusedRowIndex() > -1) {
                var lista = JSON.parse(gridGeocerca.cpNumbersJS);
                var geocerca = lista[0];
                //var subuser = lista[1];

                //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Geocercas</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + geocerca + '</span></div> </div>');
                //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
                lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + geocerca + '</span></div>');
            }
            else if (gridGrupo.GetFocusedRowIndex() == -1) {
                var lista = JSON.parse(gridGeocerca.cpNumbersJS);
                var geocerca = lista[0];
                //var subuser = lista[1];

                //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Geocercas</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + geocerca + '</span></div> </div>');
                //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
                lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + geocerca + '</span></div>');
            }
        }

        //function onEndCallback2(s, e) {
        //    if (isPageSizeCallbackRequired === true) {
        //        UpdateGridHeight();
        //    }

        //    if (gridGrupo.GetFocusedRowIndex() > -1) {
        //        var lista = JSON.parse(gridSubusuario.cpNumbers2JS);
        //        var subuser = lista[1];
        //        //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
        //    }
        //    else if (gridGrupo.GetFocusedRowIndex() == -1) {
        //        var lista = JSON.parse(gridSubusuario.cpNumbers2JS);
        //        var subuser = lista[1];
        //        //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
        //    }
        //}

        function hideAccordionPane(mobile) {
            if(mobile)
                $find('accPanel_AccordionExtender').set_selectedIndex(-1);
            else
                $find('accPanel_AccordionExtender').set_selectedIndex(0);
        }

        function OnControlsInitialized(s, e) {
            //UpdateGridHeight();
            //debugger;
            const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
            if (!e.isCallback) {
                if (isMobile) {
                    btnShow.SetClientVisible(false);
                    hideAccordionPane(isMobile);
                    UpdateGridHeight2();
                } else {
                    btnShow.SetClientVisible(true);
                    hideAccordionPane(isMobile);
                    UpdateGridHeight();
                }
            }
        }
        function OnBrowserWindowResized(s, e) {
            //debugger;
            //UpdateGridHeight();
            //if (pgGrids.GetActiveTab().name == 'tbGeocercas') {
                //gridGeocerca.PerformCallback();
                //UpdateGridHeight();
            //}
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridSubusuario.PerformCallback();
            //}
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
            gridGeocerca.PerformCallback();
        }

        //Button Show
        function shTable() {
            if (pgGrids.GetActiveTab().name == 'tbGeocercas') {
                btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
                //gridGrupo.SetVisible(!gridGrupo.GetVisible());
                AdjustSizeW(btnTodos.GetClientVisible());
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
            //    //gridGrupo.SetVisible(!gridGrupo.GetVisible());
            //    AdjustSizeW(btnTodos2.GetClientVisible());
            //}
        }

        function UpdateGridHeight() {
            gridGeocerca.SetHeight(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 190;
            gridGeocerca.SetHeight(height);

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
                height = document.body.scrollHeight - 220;
            gridGrupo.SetHeight(height);

            document.getElementById('hfHeight').value = height;
            isPageSizeCallbackRequired = false;
        }
        function UpdateGridHeight2() {
            //debugger;
            gridGrupo.SetHeight(0);
            var element = document.getElementById('lateral');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            height = height - 200;
            gridGrupo.SetHeight(height);

            document.getElementById('hfHeight').value = height;
        }
        function UpdateGridWidth() {
            gridGeocerca.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width;

            //gridSubusuario.SetWidth(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var width = positionInfo.width;

            var element2 = document.getElementById('lateral');
            var positionInfo2 = element2.getBoundingClientRect();
            var width2 = positionInfo2.width;

            //if (document.body.scrollWidth > width)
            //    width = document.body.scrollWidth - 500;
            gridGeocerca.SetWidth(width + width2 - 30);
            //gridSubusuario.SetWidth(width + width2 - 30);
        }
        function UpdateGridWidthReverse() {
            gridGeocerca.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width - 30;
            gridGeocerca.SetWidth(width);

            //gridSubusuario.SetWidth(0);
            //var element = document.getElementById('central');
            //var positionInfo = element.getBoundingClientRect();
            //var width = positionInfo.width - 30;
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

</head>
<body class="container-fluid">
    <form id="frmGeocerca" runat="server">
        <asp:ScriptManager ID="smMaster" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <div id="cabecera" class="row" style="margin: 5px 5px 5px 5px; background-color: #f2f1f0; padding: 10px; border-radius: 5px;">
            <div class="col-sm-3" style="display:flex; margin-right: 5px; margin-bottom: 5px">
                <dx:ASPxButton ID="btnShow" runat="server" AutoPostBack="False" CssClass="mybuttonmenu" ClientInstanceName="btnShow">
                    <ClientSideEvents Click="function(s, e) {shTable()}"/>
                </dx:ASPxButton>
                <image src="../../images/geocerca.png" style="width:40px; height:30px; margin: 5px 10px 0px 10px;"/>
                <h4 class="mt-1" style="font-weight: bold;">Geocercas</h4>
            </div>
            <div class="col w-100 flex">
                <dx:ASPxTextBox ID="txSearchGeo" runat="server" ClientInstanceName="txSearchGeo" NullText="Buscar geocercas ..." Paddings-PaddingLeft="40"
                                AutoPostBack="false" CssClass="mytext" Width="100%" Height="40px" Font-Size="Small">
                    <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
                </dx:ASPxTextBox>
            </div>
        </div>
        <div id="cuerpo" class="row" style="margin: 5px 5px 0px 5px; padding: 10px; background-color: #f2f1f0; border-radius: 5px;">
            <div id="lateral" class="col-sm-3" style="display:inline-block; height:100%">
                <div class="col-md w-100">
                    <dx:ASPxButton ID="btnTodos" runat="server" ClientInstanceName="btnTodos" Text="Geocercas" Width="100px" Font-Bold="true" Font-Size="10" 
                                   CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="true" ToolTip="Mostrar todas las geocercas"
                                   BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                        <ClientSideEvents Click="UpdateGrids"/>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="lbCount" runat="server" ClientInstanceName="lbCount" EncodeHtml="false" Text=" " Font-Bold="true" Font-Size="10" 
                                   CssClass="mybuttongrid" AutoPostBack="false" HoverStyle-BackColor="Transparent" HoverStyle-ForeColor="Transparent"/>
                    <%--<dx:ASPxButton ID="btnTodos2" runat="server" ClientInstanceName="btnTodos2" Text=" " Width="60%" Font-Bold="true" Font-Size="10" 
                                   CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="false" ClientVisible="false" ToolTip="Mostrar todos los subusuarios"
                                   BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                        <ClientSideEvents Click="UpdateGrids"/>
                    </dx:ASPxButton>--%>
                    <dx:ASPxButton ID="btnGeoN" runat="server" ClientInstanceName="btnGeoN" AutoPostBack="false" CssClass="mybuttongrid2" ToolTip="Nueva Geocerca"
                                   Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" Image-Url="../../images/plus.png" Image-Height="25" Image-Width="25">
                        <ClientSideEvents Click="OnMostrarGeo"/>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnUpload" runat="server" ClientInstanceName="btnUpload" AutoPostBack="false" CssClass="mybuttongrid2" ToolTip="Importar Geocerca"
                                   Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" Image-Url="../../images/upload.png" Image-Height="22" Image-Width="22" ClientVisible="false">
                        <ClientSideEvents Click="OnUploadGeo"/>
                    </dx:ASPxButton>
                </div>
                
                <hr class="solid"/>   
                <div id="panelGrupo">
                <asp:Accordion ID="accPanel" runat="server" SelectedIndex="0" AutoSize="None" FadeTransitions="true" TransitionDuration="150" FramesPerSecond="10"
                               RequireOpenedPane="false" SuppressHeaderPostbacks="false" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected">
                    <Panes>
                        <asp:AccordionPane ID="AccordionPane1" runat="server" >  
                            <Header>Grupos
                                <dx:ASPxButton ID="btnGrupoN" runat="server" ClientInstanceName="btnGrupoN" AutoPostBack="false" CssClass="mybuttongrid2f" ToolTip="Nuevo Grupo" 
                                               Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" OnInit="btnGrupoN_Init">
                                    <Image Url="../../images/plus.png" Width="20" Height="20"/>
                                </dx:ASPxButton>
                            </Header>
                            <Content>
                                <dx:ASPxGridView ID="gridGrupo" ClientInstanceName="gridGrupo" runat="server" KeyFieldName="id" OnHtmlDataCellPrepared="gridGrupo_HtmlDataCellPrepared"
                                                 Width="98%" AutoGenerateColumns="False" OnDataBinding="gridG_DataBinding" Border-BorderStyle="None" EnableCallBacks="true" CssClass="grid"
                                                 OnCustomColumnDisplayText="gridGrupo_CustomColumnDisplayText" OnPreRender="gridGrupo_PreRender" EnableCallbackAnimation="false" 
                                                 EnableCallbackCompression="false" EnablePagingCallbackAnimation="false" OnCustomCallback="gridGrupo_CustomCallback"
                                                 OnFocusedRowChanged="gridGrupo_FocusedRowChanged" OnBeforePerformDataSelect="gridGrupo_BeforePerformDataSelect">
                                    <Styles>  
                                        <Header HorizontalAlign="Center" Border-BorderStyle="None"></Header>  
                                        <Row BackColor="#f2f1f0"/> <FocusedRow BackColor="#ff4242" ForeColor="White"/>
                                    </Styles>
                                    <ClientSideEvents FocusedRowChanged="UpdateGridGeocerca" RowClick="UpdateGridGeocerca" RowDblClick="UpdateGrids"/>
                                    <Columns>
                                        <dx:GridViewDataColumn Name="btedit" VisibleIndex="3" Width="20">
                                            <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                            <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                            <DataItemTemplate>
                                                <dxe:ASPxButton ID="editar" runat="server" AutoPostBack="False" ClientInstanceName="editar" Visible="true" ToolTip="Editar Grupo"
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
                                    <SettingsBehavior AllowFocusedRow="true"/>
                                    <SettingsSearchPanel Visible="False" CustomEditorID="txSearch"/>
                                    <SettingsLoadingPanel Mode="Disabled"/>
                                    <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="200"/>
                                </dx:ASPxGridView>
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:Accordion>
                </div>
            </div>
            <div id="central" runat="server" class="col" style="display:inline-block">
                <dx:ASPxButton ID="btnAsignar" runat="server" Width="6%" ClientInstanceName="btnAsignar" ClientVisible="false" ToolTip="Asignar Grupos"
                               CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="18" Image-Height="22" Image-Url="../../images/pointasig.png" HoverStyle-BackColor="#dadad2"
                               AutoPostBack="False">
                    <ClientSideEvents Click="OnMoreInfoClick2"/>
                </dx:ASPxButton>
                <%--<dx:ASPxButton ID="btnAsignarS" runat="server" Width="6%" ClientInstanceName="btnAsignarS" ClientVisible="false" ToolTip="Asignar Grupos"
                               CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="18" Image-Height="22" Image-Url="../../images/pointasig2.png" HoverStyle-BackColor="#dadad2"
                               AutoPostBack="False">
                    <ClientSideEvents Click="OnMoreInfoClickS2"/>
                </dx:ASPxButton>--%>
                <hr class="solid2"/>
                <dx:ASPxPageControl ID="pgGrids" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids" EnableCallBacks="false"
                                    BackColor="#f2f1f0">
                    <ClientSideEvents ActiveTabChanged="InitButton" Init="onInicioTab"/>
                    <TabPages>
                        <dx:TabPage Name="tbGeocercas" Text="GEOCERCAS" ActiveTabStyle-Font-Bold="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ccUnidades" runat="server" BackColor="#f2f1f0">
                                    <dx:ASPxGridView ID="gridGeocerca" ClientInstanceName="gridGeocerca" runat="server" KeyFieldName="gid" CssClass="grid" Width="100%"
                                                     AutoGenerateColumns="False" OnDataBinding="gridGe_DataBinding" Border-BorderStyle="None" EnableCallBacks="true"
                                                     OnCustomCallback="gridGeocerca_CustomCallback" EnableCallbackAnimation="false" EnableCallbackCompression="false"
                                                     EnablePagingCallbackAnimation="false" OnRowUpdating="gridGeocerca_RowUpdating" OnBatchUpdate="gridGeocerca_BatchUpdate"
                                                     OnHtmlDataCellPrepared="gridGeocerca_HtmlDataCellPrepared" OnHtmlCommandCellPrepared="gridGeocerca_HtmlCommandCellPrepared"
                                                     OnCustomColumnDisplayText="gridGeocerca_CustomColumnDisplayText">
                                        <ClientSideEvents Init="onInit" EndCallback="onEndCallback" SelectionChanged="grid_SelectionChangedGeocerca" 
                                                          BatchEditEndEditing="onBatchEnd" BatchEditStartEditing="onBatchStar"/>
                                        <Styles>  
                                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                            <Cell BorderBottom-BorderStyle="Solid"/>
                                            <Row BackColor="#f2f1f0"/>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" AdaptivePriority="0" VisibleIndex="0" SelectAllCheckboxMode="Page" Name="chkall"
                                                                      CellStyle-HorizontalAlign="Center" Width="40" HeaderStyle-Border-BorderStyle="None" CellStyle-Border-BorderStyle="None"/>
                                            <%--<dx:GridViewDataColumn Caption=" " Width="50" Name="btasig" VisibleIndex="1" MaxWidth="80" AdaptivePriority="0">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="asignar" runat="server" AutoPostBack="False" ClientInstanceName="asignar" Visible="true" RenderMode="Button"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="12" Image-Url="../../images/tags.png" ToolTip="Asignar Grupo"
                                                                    OnInit="asignar_Init" HoverStyle-BackColor="#DB0632" CssClass="mybuttongrid">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>--%>
                                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btasig" VisibleIndex="6" MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <BatchEditModifiedCellStyle ><Border BorderStyle="None" BorderWidth="0"/></BatchEditModifiedCellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="btnMapas" runat="server" ClientInstanceName="btnMapas" ToolTip="Mostrar Mapa Geocerca" AutoPostBack="False"
                                                                   CssClass="mybuttongrid" RenderMode="Button" Image-Width="15" Image-Height="15" Image-Url="../../images/geogrupos.png" 
                                                                   HoverStyle-BackColor="#dadad2" OnInit="btnMapas_Init">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btdelete" VisibleIndex="7" MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" /> </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="eliminar" runat="server" AutoPostBack="False" ClientInstanceName="eliminar" RenderMode="Button" ToolTip="Eliminar Geocerca"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" CssClass="mybuttongrid"
                                                                    HoverStyle-BackColor="#dadad2" OnInit="eliminar_Init">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchGeo" ColumnNames="geocerca"/>
                                        <SettingsBehavior AllowSelectByRowClick="false"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <%--<SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />--%>
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                        <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" BatchEditSettings-StartEditAction="FocusedCellClick" 
                                                         BatchEditSettings-KeepChangesOnCallbacks="False"/>
                                        <Settings ShowStatusBar="Hidden" />
                                    </dx:ASPxGridView>
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
                                        <ClientSideEvents Init="onInit" EndCallback="onEndCallback2" SelectionChanged="grid_SelectionChangedSubusuario" />
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
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchGeo"/>
                                        <SettingsBehavior AllowSelectByRowClick="true"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>--%>
                    </TabPages>
                </dx:ASPxPageControl>
                
            </div>
            <div id="central2" runat="server" class="col" style="display:none">
                <div id="dvmapa" style="width: 380px; height: 500px; background-color: lightgray; top: -160px; left: 10px;"></div>
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

        <%--PopupMenuGrupoGeocercas--%>
        <dx:ASPxPopupControl ID="pmGrupo" runat="server" ClientInstanceName="pmGrupo" Width="380px" Height="420px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign ="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--PopupMenuGrupoGeocercas2--%>
        <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp"></dx:ASPxLoadingPanel>
        <dx:ASPxPopupControl ID="pmGrupo2" runat="server" ClientInstanceName="pmGrupo2" Width="390px" Height="450px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true" OnWindowCallback="pmGrupo2_WindowCallback">
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
        <dx:ASPxPopupControl ID="pmGrupo4" runat="server" ClientInstanceName="pmGrupo4" Width="380px" Height="440px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true" OnWindowCallback="pmGrupo4_WindowCallback">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>--%>

        <%--PopupModalMapa--%>
        <dx:ASPxPopupControl ID="pmMapa" runat="server" Width="600" Height="580" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pmMapa"
                             HeaderText="Mapa Geocerca" AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
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

        <%--PopupEliminarGeo--%>
        <dx:ASPxPopupControl ID="popupDelGeo" runat="server" ClientInstanceName="popupDelGeo" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
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

        <%--PopupModalUpload--%>
        <dx:ASPxPopupControl ID="pmUpload" runat="server" Width="600" Height="380" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pmUpload"
                             HeaderText="Importar Geocercas" AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
            <%--<ClientSideEvents CloseButtonClick="onCloseBtn"/>--%>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <asp:HiddenField ID="hfIndex" runat="server"/>
        <asp:HiddenField ID="hfIndex2" runat="server"/>
        <asp:HiddenField ID="hfHeight" runat="server"/>

        <asp:HiddenField ID="clHeight" runat="server"/>
        <asp:HiddenField ID="clWidth" runat="server"/>

        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />
        <asp:HiddenField runat="server" ID="hdregs"/>
        
        <asp:HiddenField runat="server" ID="husuario" />
        <asp:HiddenField runat="server" ID="hclave" />

        <asp:HiddenField ID="hgeocercas" runat="server"/>
        <asp:HiddenField ID="hsubuser" runat="server"/>

        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" BrowserWindowResized="OnBrowserWindowResized"/>
        </dx:ASPxGlobalEvents>
        
        <asp:HiddenField ID="hdurlgeocercas" runat="server" />
        <asp:HiddenField ID="hdurlgeocercasdetalle" runat="server" />
        <asp:HiddenField ID="hdlistamapas" runat="server" />
        <asp:HiddenField ID="hdservidorpe" runat="server" />
        <asp:HiddenField ID="hdservidorcapaspe" runat="server" />
        <asp:HiddenField ID="txLatitudInicial" runat="server" />
        <asp:HiddenField ID="txLongitudInicial" runat="server" />
        <asp:HiddenField ID="hdmotormapa" runat="server" />
        <asp:HiddenField ID="hdpais" runat="server" />
        <asp:HiddenField ID="txCapasAdicionales" runat="server" />

    </form>
</body>
</html>
