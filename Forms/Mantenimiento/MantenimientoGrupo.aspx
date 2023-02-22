<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoGrupo.aspx.vb" Inherits="ArtemisAdmin.MantenimientoGrupo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
    <script src="https://raw.github.com/douglascrockford/JSON-js/master/json2.js"></script>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="../../Libs/js/x-notify.min.js"></script>
    <link rel="stylesheet" href="../../Libs/css/notification.css" />
    <script src="../../Libs/js/notification.js"></script>

    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title>VEHICULOS</title>

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
                         margin-right: 25px;
        }
        .mybuttongrid2f { border-radius: 10px 10px 10px 10px; 
                          border: none;
                          background: transparent;
                          /*margin-left: 185px;*/
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

    </style>

    <script type="text/javascript">
        var isPageSizeCallbackRequired = false;
        const notification = new Notification();
        var showPopup = true;
        var iframe;

        var unidades;
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
        function GrupoWindowHide3() {
            pmGrupo.Hide();
            if (gridGrupo.GetFocusedRowIndex >= 0)
                gridVehiculo.PerformCallback('1');
            else if (gridGrupo.GetFocusedRowIndex == -1)
                gridVehiculo.PerformCallback('unidades');
        }
        function GrupoWindowHide4() {
            pmGrupo.Hide();
            pmGrupo.PerformCallback();
        }
        function EliminaHide() {
            popupDelete.Hide();
        }

        //GridView SelectedChanged
        function grid_SelectionChangedVehiculo(s, e) {
            var contar = s.GetSelectedRowCount();
            var opc = document.getElementById("hdregs").value;

            if (opc == "1") {
                s.GetSelectedFieldValues("vid;idac", RecieveGridVehiculoValues);
                //s.GetSelectedFieldValues("vid", RecieveGridVehiculoValues);
                if (contar > 0) {
                    if (!btnAsignar.GetClientVisible()) {
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                    }
                } else {
                    if (btnAsignar.GetClientVisible())
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                }
            }
        }
        function RecieveGridVehiculoValues(values) {
            unidades = values;
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

        //GridGrupo
        function UpdateGridVehiculo(s, e) {
            gridVehiculo.PerformCallback('1');
            //gridSubusuario.PerformCallback('1');
        }
        function UpdateGrids(s, e) {
            gridGrupo.SetFocusedRowIndex(-1);
            gridVehiculo.PerformCallback('unidades');
            //gridSubusuario.PerformCallback('subusuarios');

            var activo = hactivos.value;
            //var subuser = hsubuser.value;

            btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Vehículos</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + activo + '</span></div> </div>');
            //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
        }
        function UpdateGridsText(s, e) {
            var aux = txSearch.GetText();
            if (pgGrids.GetActiveTab().name == 'tbUnidades') {
                if (aux.length >= 1) {
                    gridVehiculo.PerformCallback('cfunidades');
                } else {
                    gridVehiculo.PerformCallback('sfunidades');
                    gridGrupo.SetFocusedRowIndex(-1);
                }
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridGrupo.SetFocusedRowIndex(-1);
            //    gridSubusuario.PerformCallback('subusuarios');
            //}
        }
        function InitButton(s, e) {
            if (pgGrids.GetActiveTab().name == 'tbUnidades') {
                btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
                //btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
                //if (btnAsignarS.GetClientVisible()) {
                //    btnAsignarS.SetClientVisible(!btnAsignarS.GetClientVisible());
                //    gridSubusuario.UnselectRows();
                //}
                changeNullText(txSearchVeh, "Buscar vehículos ...")
            }
            //else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
            //    btnTodos2.SetClientVisible(!btnTodos2.GetClientVisible());
            //    if (btnAsignar.GetClientVisible()) {
            //        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
            //        gridVehiculo.UnselectRows();
            //    }
            //    changeNullText(txSearchVeh, "Buscar subusuarios ...")
            //}
        }
        function changeNullText(textBox, value) {
            var mainTbEl = textBox.GetMainElement();
            if (mainTbEl.getElementsByClassName('dxeFNTextSys')[0]) {
                mainTbEl.getElementsByClassName('dxeFNTextSys')[0].value = value;
            }
            textBox.GetInputElement().value = value;
        }

        function InitPageCtrl(s, e) {
            var opc = document.getElementById("hdregs").value;
            var tabActivo = pgGrids.GetTab(0);
            if (opc == "1") {
                tabActivo.SetVisible(true);
                document.getElementById("panelGrupo").style.display = '';
            } else {
                tabActivo.SetVisible(true);
                document.getElementById("panelGrupo").style.display = 'none';
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
        function OnMoreInfoClick(contentUrl) {
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
            pmGrupo.SetContentUrl(contentUrl);
            pmGrupo.Show();
        }
        //function OnMoreInfoClick2(contentUrl) {
        //    pmGrupo2.SetContentUrl(contentUrl);
        //    pmGrupo2.Show();
        //}
        function OnMoreInfoClick2(s, e) {
            var contentUrl = "AsignarGrupo2.aspx?valores=" + unidades;
            showPopup = true;
            pmGrupo2.SetContentUrl(contentUrl);
            pmGrupo2.Show();
        }

        //function OnMoreInfoClickS(contentUrl) {
        //    var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        //    pmGrupo3.SetContentUrl(contentUrl);
        //    pmGrupo3.Show();
        //}
        //function OnMoreInfoClickS2(contentUrl) {
        //    pmGrupo4.SetContentUrl(contentUrl);
        //    pmGrupo4.Show();
        //}
        //function OnMoreInfoClickS2(s, e) {
        //    var contentUrl = "AsignarGrupoS2.aspx?valores=" + subusuarios;
        //    pmGrupo4.SetContentUrl(contentUrl);
        //    pmGrupo4.Show();
        //}

        function HidePopup() {
            pmGrupo2.Hide();
            gridVehiculo.UnselectRows();
            gridVehiculo.PerformCallback('1');
        }
        //function HidePopup2() {
        //    pmGrupo4.Hide();
        //    gridSubusuario.UnselectRows();
        //    gridSubusuario.PerformCallback('1');
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
        function HidePopup3() {
            pcGrupo.Hide();
            gridGrupo.PerformCallback('1');
        }
        function HidePopup4() {
            popupDelete.Hide();
            gridGrupo.PerformCallback('1');
            gridVehiculo.PerformCallback('unidades');
            //gridSubusuario.PerformCallback('subusuarios');
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

            if (gridGrupo.GetFocusedRowIndex() > -1) {
                var lista = JSON.parse(gridVehiculo.cpNumbersJS);
                var activo = lista[0];
                //var subuser = lista[1];

                btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Vehículos</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + activo + '</span></div> </div>');
                //btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
            }
        }
        //function onEndCallback2(s, e) {
        //    if (isPageSizeCallbackRequired === true) {
        //        UpdateGridHeight();
        //    }

        //    if (gridGrupo.GetFocusedRowIndex() > -1) {
        //        var lista = JSON.parse(gridSubusuario.cpNumbers2JS);
        //        var subuser = lista[1];

        //        btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
        //    }
        //    else if (gridGrupo.GetFocusedRowIndex() == -1) {
        //        var lista = JSON.parse(gridSubusuario.cpNumbers2JS);
        //        var subuser = lista[1];

        //        btnTodos2.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Subusuarios</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + subuser + '</span></div> </div>');
        //    }
        //}

        function onBatchStar(s, e) {
            if (e.focusedColumn.fieldName == "id")
                e.cancel = true;
        }
        function onBatchEnd(s, e) {
            setTimeout(function () {
                if (s.batchEditApi.HasChanges()) {
                    s.UpdateEdit();
                }
            }, 1000);
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
                hideAccordionPane(isMobile);
                UpdateGridHeight2();
            } else {
                btnShow.SetClientVisible(true);
                hideAccordionPane(isMobile);
                UpdateGridHeight();
            }
        }
        function OnBrowserWindowResized(s, e) {
            //UpdateGridHeight();
            //if (pgGrids.GetActiveTab().name == 'tbUnidades') {
            //    gridVehiculo.PerformCallback();
            //} else if (pgGrids.GetActiveTab().name == 'tbSubusuarios') {
            //    gridSubusuario.PerformCallback();
            //}
            const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
            gridGeocerca.PerformCallback();
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

        //Button Show
        function shTable() {
            if (pgGrids.GetActiveTab().name == 'tbUnidades') {
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
            gridVehiculo.SetHeight(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 190;
            gridVehiculo.SetHeight(height);

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
                height = document.body.scrollHeight - 210;
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
            height = height - 300;
            gridGrupo.SetHeight(height);

            document.getElementById('hfHeight').value = height;
        }

        function UpdateGridWidth() {
            gridVehiculo.SetWidth(0);
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
            gridVehiculo.SetWidth(width + width2 - 30);
            //gridSubusuario.SetWidth(width + width2 - 30);
        }
        function UpdateGridWidthReverse() {
            gridVehiculo.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width - 30;
            gridVehiculo.SetWidth(width);

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
    <form id="frmVehiculos" runat="server">
        <asp:ScriptManager ID="smMaster" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <div id="cabecera" class="hstack gap-3 flex" style="margin: 5px 5px 5px 5px; background-color: #f2f1f0; padding: 10px; border-radius: 5px;">
            <div class="col-sm-3" style="display:flex; margin-right: 5px">
                <dx:ASPxButton ID="btnShow" runat="server" ClientInstanceName="btnShow" AutoPostBack="False" CssClass="mybuttonmenu">
                    <ClientSideEvents Click="function(s, e) {shTable()}"/>
                </dx:ASPxButton>
                <image src="../../images/car.png" style="width:40px; height:30px; margin: 5px 10px 0px 10px;"/>
                <h4 class="mt-1" style="font-weight: bold;">Vehículos</h4>
            </div>
            <div runat="server" class="w-100">
                <dx:ASPxTextBox ID="txSearchVeh" runat="server" ClientInstanceName="txSearchVeh" NullText="Buscar vehículos ..." Paddings-PaddingLeft="40"
                                AutoPostBack="false" CssClass="mytext" Width="100%" Height="40px" Font-Size="Small">
                    <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
                </dx:ASPxTextBox>
            </div>
        </div>
        <div id="cuerpo" class="row" style="margin: 5px 5px 0px 5px; padding: 10px; background-color: #f2f1f0; border-radius: 5px;">
            <div id="lateral" class="col-sm-3" style="display:inline-block">
                <dx:ASPxButton ID="btnTodos" runat="server" ClientInstanceName="btnTodos" Text=" " Width="70%" Font-Bold="true" Font-Size="10" 
                               CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="false" ToolTip="Mostrar todos los vehículos"
                               BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                    <ClientSideEvents Click="UpdateGrids"/>
                </dx:ASPxButton>
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
                                                 OnCustomColumnDisplayText="gridGrupo_CustomColumnDisplayText" OnPreRender="gridGrupo_PreRender" EnableCallbackAnimation="false" 
                                                 EnableCallbackCompression="false" EnablePagingCallbackAnimation="false" OnCustomCallback="gridGrupo_CustomCallback"
                                                 OnFocusedRowChanged="gridGrupo_FocusedRowChanged">
                                    <Styles>  
                                        <Header HorizontalAlign="Center" Border-BorderStyle="None"></Header>  
                                        <Row BackColor="#f2f1f0"/> <FocusedRow BackColor="#ff4242" ForeColor="White"/>
                                    </Styles>
                                    <ClientSideEvents FocusedRowChanged="UpdateGridVehiculo" RowClick="UpdateGridVehiculo" RowDblClick="UpdateGrids"/>
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
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="200"/>
                                </dx:ASPxGridView>
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:Accordion>
                </div>
            </div>
            <div id="central" runat="server" class="col" style="display:inline-block">
                <dx:ASPxButton ID="btnAsignar" runat="server" Width="6%" ClientInstanceName="btnAsignar" ClientVisible="false" ToolTip="Asignar Grupos"
                               CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="22" Image-Height="18" Image-Url="../../images/asignar.png" HoverStyle-BackColor="#dadad2"
                               AutoPostBack="False" >
                    <ClientSideEvents Click="OnMoreInfoClick2"/>
                </dx:ASPxButton>
                <%--<dx:ASPxButton ID="btnAsignarS" runat="server" Width="6%" ClientInstanceName="btnAsignarS" ClientVisible="false" ToolTip="Asignar Grupos" 
                               CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="22" Image-Height="18" Image-Url="../../images/asignar2.png" HoverStyle-BackColor="#dadad2"
                               AutoPostBack="False" >
                    <ClientSideEvents Click="OnMoreInfoClickS2"/>
                </dx:ASPxButton>--%>
                <hr class="solid2"/>
                <dx:ASPxPageControl ID="pgGrids" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids" EnableCallBacks="false"
                                    BackColor="#f2f1f0">
                    <ClientSideEvents ActiveTabChanged="InitButton" Init="InitPageCtrl"/>
                    <TabPages>
                        <dx:TabPage Name="tbUnidades" Text="VEHICULOS" ActiveTabStyle-Font-Bold="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ccUnidades" runat="server" BackColor="#f2f1f0">
                                    <dx:ASPxGridView ID="gridVehiculo" ClientInstanceName="gridVehiculo" runat="server" KeyFieldName="vid" CssClass="grid" Width="100%"
                                                     AutoGenerateColumns="False" OnDataBinding="gridU_DataBinding" Border-BorderStyle="None" EnableCallBacks="true" 
                                                     OnCustomCallback="gridVehiculo_CustomCallback" EnableCallbackAnimation="false" EnableCallbackCompression="false" 
                                                     EnablePagingCallbackAnimation="false" OnHtmlDataCellPrepared="gridVehiculo_HtmlDataCellPrepared" OnHtmlCommandCellPrepared="gridVehiculo_HtmlCommandCellPrepared"
                                                     OnRowUpdating="gridVehiculo_RowUpdating" OnBatchUpdate="gridVehiculo_BatchUpdate">
                                        <ClientSideEvents Init="onInit" EndCallback="onEndCallback" SelectionChanged="grid_SelectionChangedVehiculo" 
                                                          BatchEditEndEditing="onBatchEnd" BatchEditStartEditing="onBatchStar"/>
                                        <Styles>  
                                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                            <Cell BorderBottom-BorderStyle="Solid" Paddings-PaddingBottom="10px" Paddings-PaddingTop="10px"/>
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
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" ToolTip="Editar Etiqueta"
                                                                    OnInit="asignar_Init" HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>--%>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchVeh"/>
                                        <SettingsBehavior AllowSelectByRowClick="false"/>
                                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                        <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" BatchEditSettings-StartEditAction="FocusedCellClick" BatchEditSettings-KeepChangesOnCallbacks="False"/>
                                        <Settings ShowStatusBar="Hidden"/>
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
                                                                    OnInit="asignarS_Init" HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid">
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchVeh"/>
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
        </div>

        <%--PopupModalGrupo--%>
        <dx:ASPxPopupControl ID="pcGrupo" runat="server" Width="500" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
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

        <%--PopupMenuGrupoVehiculos--%>
        <dx:ASPxPopupControl ID="pmGrupo" runat="server" ClientInstanceName="pmGrupo" Width="500" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText="Editar Etiqueta" PopupHorizontalAlign ="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AllowDragging="True" AutoUpdatePosition="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        
        <%--PopupMenuGrupoVehiculos2--%>
        <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp"></dx:ASPxLoadingPanel>
        <dx:ASPxPopupControl ID="pmGrupo2" runat="server" ClientInstanceName="pmGrupo2" Width="380px" Height="440px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
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

        <asp:HiddenField ID="hfIndex" runat="server"/>
        <asp:HiddenField ID="hfIndex2" runat="server"/>
        <asp:HiddenField ID="hfHeight" runat="server"/>

        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />
        <asp:HiddenField runat="server" ID="hdregs"/>

        <asp:HiddenField ID="hactivos" runat="server"/>
        <asp:HiddenField ID="hsubuser" runat="server"/>

        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" BrowserWindowResized="OnBrowserWindowResized"/>
        </dx:ASPxGlobalEvents>

    </form>
</body>
</html>