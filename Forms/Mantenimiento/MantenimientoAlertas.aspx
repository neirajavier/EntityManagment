<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoAlertas.aspx.vb" Inherits="ArtemisAdmin.MantenimientoAlertas" %>
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
    <title>ALERTAS</title>

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

        .dxToggle[class*='Checked'] {
            background-color: green !important;
        }

    </style>

    <script type="text/javascript">
        var alertas, alertas2;
        const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);

        //function OnMostrarAlerta(s, e) {
        //    var contentUrl = "FormAlerta.aspx?data=nueva&id=0";
        //    pcNAlerta.SetContentUrl(contentUrl);
        //    pcNAlerta.Show();
        //}
        function OnMoreInfoClick(contentUrl) {
            pcNAlerta.SetContentUrl(contentUrl);
            pcNAlerta.Show();
        }

        function ShowInfo2(contentUrl) {
            pcNAlerta.SetContentUrl(contentUrl);
            pcNAlerta.Show();
        }
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
        function EliminaHide() {
            popupDelete.Hide();
        }
        //function OnEditarAlerta(s, e) {
        //    var contentUrl = "FormAlerta.aspx?data=edita";
        //    pcNAlerta.SetContentUrl(contentUrl);
        //    pcNAlerta.Show();
        //}
        function hideAlerta() {
            pcNAlerta.Hide();
            gridAlerta.PerformCallback('alertas');
        }
        function hideAlerta2() {
            popupDelAlt.Hide();
            gridAlerta.PerformCallback('alertas');
        }
        //Alerta SelectedChanged
        function grid_SelectionAlerta(s, e) {
            //debugger;
            var contar = s.GetSelectedRowCount();
            var opc = document.getElementById("hdregs").value;

            if (opc == "1") {
                s.GetSelectedFieldValues("ida", RecieveGridAlertaValues);
                if (contar > 0) {
                    if (!btnDelete.GetClientVisible()) {
                        btnDelete.SetClientVisible(!btnDelete.GetClientVisible());
                    }
                    if (!btnAsignar.GetClientVisible()) {
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                    }
                } else {
                    if (btnDelete.GetClientVisible()) {
                        btnDelete.SetClientVisible(!btnDelete.GetClientVisible());
                    }
                    if (btnAsignar.GetClientVisible()) {
                        btnAsignar.SetClientVisible(!btnAsignar.GetClientVisible());
                    }
                }
                s.GetSelectedFieldValues("id;ida", RecieveGridAlertaValues2);
            }
        }
        function RecieveGridAlertaValues(values) {
            alertas = values;
        }
        function RecieveGridAlertaValues2(values) {
            alertas2 = values;
        }

        function OnMoreInfoClick2(s, e) {
            debugger;
            var contentUrl = "AsignarGrupoAlt.aspx?valores=" + alertas2;
            //showPopup = true;
            pmGrupo2.SetContentUrl(contentUrl);
            pmGrupo2.Show();
        }
        function OnDeleteClick(s, e) {
            var contentUrl = "EliminarAlertas.aspx?valores=" + alertas;
            //showPopup = true;
            popupDelAlt.SetContentUrl(contentUrl);
            popupDelAlt.Show();
        }

        function onResizeAlerta(criterio) {
            if (criterio == '1')
                pcNAlerta.SetHeight(470);
            else if(criterio == '3' || criterio == '4')
                pcNAlerta.SetHeight(500);
            else if (criterio == '2')
                pcNAlerta.SetHeight(500);
            else if (criterio == '6')
                pcNAlerta.SetHeight(600);
            else if (criterio == '1000')
                pcNAlerta.SetHeight(470);
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
                var lista = JSON.parse(gridAlerta.cpNumbersJS);
                var alerta = lista[0];

                //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Alertas</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + alerta + '</span></div> </div>');
                lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + alerta + '</span></div>');
            }
            else if (gridGrupo.GetFocusedRowIndex() == -1) {
                var lista = JSON.parse(gridAlerta.cpNumbersJS);
                var alerta = lista[0];

                //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Alertas</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + alerta + '</span></div> </div>');
                lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + alerta + '</span></div>');
            }
        }

        function HidePopup() {
            pmGrupo2.Hide();
            gridAlerta.UnselectRows();
            if (gridGrupo.GetFocusedRowIndex() > -1) {
                gridAlerta.PerformCallback('1');
            } else if (gridGrupo.GetFocusedRowIndex() == -1) {
                gridAlerta.PerformCallback('alertas');
            }
        }
        function HidePopup3() {
            pcGrupo.Hide();
            gridGrupo.PerformCallback('1');
        }
        function HidePopup4() {
            popupDelete.Hide();
            gridGrupo.PerformCallback('1');
            gridAlerta.PerformCallback('alertas');
        }

        function UpdateGrids(s, e) {
            gridGrupo.SetFocusedRowIndex(-1);
            gridAlerta.PerformCallback('alertas');

            var alerta = halertas.value;

            //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Alertas</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + alerta + '</span></div> </div>');
            lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + alerta + '</span></div>');
        }
        function UpdateGridAlerta(s, e) {
            gridAlerta.PerformCallback('1');
        }

        function onCheckedStatus(status, id) {
            //debugger;
            if (status) {
                gridAlerta.PerformCallback('ON-' + id);
                showNotification('El estado de la alerta ha cambiado a ACTIVO.','S');
            } else {
                gridAlerta.PerformCallback('OFF-' + id);
                showNotification('El estado de la alerta ha cambiado a INACTIVO.', 'E');
            }
        }

        function onBatchStar(s, e) {
            if (e.focusedColumn.fieldName == "data" || e.focusedColumn.fieldName == "asg" || e.focusedColumn.fieldName == "act")
                e.cancel = true;
            var keyIndex = s.GetColumnByField("data").index;
            var key = e.rowValues[keyIndex].value;
            //if (e.focusedColumn.fieldName == "gpa" && key == "Poligonal") {
            //    e.cancel = true;
            //}
        }
        function onBatchEnd(s, e) {
            setTimeout(function () {
                if (s.batchEditApi.HasChanges()) {
                    s.UpdateEdit();
                }
            }, 500);
        }

        function onInicioTab(s, e) {
            var opc = document.getElementById("hdregs").value;
            if (opc == "1") {
                document.getElementById("panelGrupo").style.display = '';
            } else {
                document.getElementById("panelGrupo").style.display = 'none';
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
            //UpdateGridHeight();
            //if (pgGrids.GetActiveTab().name == 'tbAlertas') {
            //    gridVehiculo.PerformCallback();
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
            gridAlerta.PerformCallback();
        }

        //Button Show
        function shTable() {
            //debugger;
            btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
            gridGrupo.SetVisible(!gridGrupo.GetVisible());
            AdjustSizeW(btnTodos.GetClientVisible());
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

        function UpdateGridHeight() {
            gridAlerta.SetHeight(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 190;
            gridAlerta.SetHeight(height);

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
        function UpdateGridWidth() {
            gridAlerta.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width;

            var element2 = document.getElementById('lateral');
            var positionInfo2 = element2.getBoundingClientRect();
            var width2 = positionInfo2.width;

            gridAlerta.SetWidth(width + width2 - 30);
        }
        function UpdateGridHeight2() {
            gridGrupo.SetHeight(0);
            var element = document.getElementById('lateral');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            height = height - 200;
            gridGrupo.SetHeight(height);

            document.getElementById('hfHeight').value = height;
        }
        function UpdateGridWidthReverse() {
            gridAlerta.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width - 30;
            gridAlerta.SetWidth(width);
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
    <form id="frmAlertas" runat="server">
        <div>
            <asp:ScriptManager ID="smMaster" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
            <div id="cabecera" class="hstack gap-3 flex" style="margin: 5px 5px 5px 5px; background-color: #f2f1f0; padding: 10px; border-radius: 5px;">
                <div class="col-sm-3" style="display:flex; margin-right: 5px">
                    <dx:ASPxButton ID="btnShow" runat="server" ClientInstanceName="btnShow" AutoPostBack="False" CssClass="mybuttonmenu">
                        <ClientSideEvents Click="function(s, e) {shTable()}"/>
                    </dx:ASPxButton>
                    <image src="../../images/alerta.png" style="width:40px; height:40px; margin: 0px 10px 0px 10px;"/>
                    <h4 class="mt-1" style="font-weight: bold;">Alertas</h4>
                </div>
                <div runat="server" class="w-100">
                    <dx:ASPxTextBox ID="txSearchAle" runat="server" ClientInstanceName="txSearchAle" NullText="Buscar alertas ..." Paddings-PaddingLeft="40"
                                    AutoPostBack="false" CssClass="mytext" Width="100%" Height="40px" Font-Size="Small">
                        <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div id="cuerpo" class="row" style="margin: 5px 5px 0px 5px; padding: 10px; background-color: #f2f1f0; border-radius: 5px;">
                <div id="lateral" class="col-sm-3" style="display:inline-block">
                    <div class="col-md w-100">
                    <dx:ASPxButton ID="btnTodos" runat="server" ClientInstanceName="btnTodos" Text="Alertas" Width="100px" Font-Bold="true" Font-Size="10" 
                               CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="true" ToolTip="Mostrar todos las alertas"
                               BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                        <ClientSideEvents Click="UpdateGrids"/>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="lbCount" runat="server" ClientInstanceName="lbCount" EncodeHtml="false" Text=" " Font-Bold="true" Font-Size="10" 
                                   CssClass="mybuttongrid" AutoPostBack="false" HoverStyle-BackColor="Transparent" HoverStyle-ForeColor="Transparent"/>
                    <dx:ASPxButton ID="btnAlert" runat="server" ClientInstanceName="btnAlert" AutoPostBack="false" CssClass="mybuttongrid2" ToolTip="Nueva Alerta"
                                   Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" Image-Url="../../images/plus.png" Image-Height="25" Image-Width="25" OnInit="btnAlert_Init">
                        <%--<ClientSideEvents Click="OnMostrarAlerta"/>--%>
                    </dx:ASPxButton>
                    </div>
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
                                    <dx:ASPxGridView ID="gridGrupo" ClientInstanceName="gridGrupo" runat="server" KeyFieldName="id" Width="98%" OnDataBinding="gridG_DataBinding" 
                                                     Border-BorderStyle="None" EnableCallBacks="true" CssClass="grid" EnableCallbackAnimation="false" OnCustomCallback="gridGrupo_CustomCallback"
                                                     EnableCallbackCompression="false" EnablePagingCallbackAnimation="false" OnHtmlDataCellPrepared="gridGrupo_HtmlDataCellPrepared"
                                                     OnPreRender="gridGrupo_PreRender" OnCustomColumnDisplayText="gridGrupo_CustomColumnDisplayText"
                                                     OnFocusedRowChanged="gridGrupo_FocusedRowChanged">
                                        <Styles>  
                                            <Header HorizontalAlign="Center" Border-BorderStyle="None"></Header>  
                                            <Row BackColor="#f2f1f0"/> <FocusedRow BackColor="#ff4242" ForeColor="White"/>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="UpdateGridAlerta" RowClick="UpdateGridAlerta" RowDblClick="UpdateGrids"/>
                                        <Columns>
                                            <dx:GridViewDataColumn Name="btedit" VisibleIndex="3" Width="20">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="editar" runat="server" AutoPostBack="False" ClientInstanceName="editar" Visible="true" ToolTip="Editar Grupo"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" 
                                                                    HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid" OnInit="editar_Init"> 
                                                    </dx:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Name="btdel" VisibleIndex="4" Width="20">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton ID="borrar" runat="server" AutoPostBack="False" ClientInstanceName="borrar" Visible="true" ToolTip="Eliminar Grupo"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" 
                                                                    HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid" OnInit="borrar_Init">
                                                    </dx:ASPxButton>
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
                                   CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="22" Image-Height="18" Image-Url="../../images/asigalert.png" HoverStyle-BackColor="#dadad2"
                                   AutoPostBack="False" >
                        <ClientSideEvents Click="OnMoreInfoClick2"/>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnDelete" runat="server" Width="6%" ClientInstanceName="btnDelete" ClientVisible="false" ToolTip="Eliminar alertas seleccionadas"
                                   CssClass="mybuttongrid" Image-Width="18" Image-Height="20" Image-Url="../../images/delete.png" HoverStyle-BackColor="#dadad2"
                                   AutoPostBack="False">
                        <ClientSideEvents Click="OnDeleteClick"/>
                    </dx:ASPxButton>
                    <hr class="solid2"/>
                    <dx:ASPxPageControl ID="pgGrids" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids" EnableCallBacks="false"
                                        BackColor="#f2f1f0">
                        <ClientSideEvents Init="onInicioTab"/>
                    <TabPages>
                        <dx:TabPage Name="tbAlertas" Text="ALERTAS" ActiveTabStyle-Font-Bold="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ccAlertas" runat="server" BackColor="#f2f1f0">
                                    <dx:ASPxGridView ID="gridAlerta" ClientInstanceName="gridAlerta" runat="server" KeyFieldName="ida" CssClass="grid" Width="100%"
                                                     AutoGenerateColumns="False" OnDataBinding="gridA_DataBinding" Border-BorderStyle="None" EnableCallBacks="true" 
                                                     EnableCallbackAnimation="false" EnableCallbackCompression="false" EnablePagingCallbackAnimation="false"
                                                     OnCustomCallback="gridAlerta_CustomCallback" OnCustomColumnDisplayText="gridAlerta_CustomColumnDisplayText"
                                                     OnRowUpdating="gridAlerta_RowUpdating" OnBatchUpdate="gridAlerta_BatchUpdate">
                                        <ClientSideEvents Init="onInit" EndCallback="onEndCallback" SelectionChanged="grid_SelectionAlerta"
                                                          BatchEditEndEditing="onBatchEnd" BatchEditStartEditing="onBatchStar"/>
                                        <Styles>  
                                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                            <Cell BorderBottom-BorderStyle="Solid"/>
                                            <Row BackColor="#f2f1f0"/>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" AdaptivePriority="0" VisibleIndex="0" SelectAllCheckboxMode="Page" Name="chkall" 
                                                                      CellStyle-HorizontalAlign="Center" Width="40" HeaderStyle-Border-BorderStyle="None" CellStyle-Border-BorderStyle="None"/>
                                            <dx:GridViewDataColumn Name="btedit" VisibleIndex="5" Width="20" EditFormSettings-Visible="False">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dxe:ASPxButton ID="editar2" runat="server" AutoPostBack="False" ClientInstanceName="editar2" Visible="true" ToolTip="Editar Grupo"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" 
                                                                    HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid" OnInit="editar2_Init"> 
                                                    </dxe:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <%--<dx:GridViewDataColumn Name="btdel" VisibleIndex="6" Width="20" EditFormSettings-Visible="False">
                                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                <DataItemTemplate>
                                                    <dxe:ASPxButton ID="borrar2" runat="server" AutoPostBack="False" ClientInstanceName="borrar2" Visible="true" ToolTip="Eliminar Grupo"
                                                                    Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" 
                                                                    HoverStyle-BackColor="#dadad2" CssClass="mybuttongrid" OnInit="borrar2_Init">
                                                    </dxe:ASPxButton>
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>--%>
                                            <dx:GridViewDataColumn FieldName="act" Caption=" " VisibleIndex="6" Width="30" EditFormSettings-Visible="False">  
                                                <DataItemTemplate>  
                                                    <dx:ASPxCheckBox ID="chkEstado" ClientInstanceName="chkEstado" runat="server" AutoPostBack="False"
                                                                     ToggleSwitchDisplayMode="Always" OnInit="chkEstado_Init">  
                                                    </dx:ASPxCheckBox>  
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsSearchPanel Visible="false" CustomEditorID="txSearchAle" ColumnNames="alias"/>
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
                    </TabPages>
                </dx:ASPxPageControl>
                </div>
            </div>
        </div>

        <%--PopupNuevaAlerta--%>
        <dx:ASPxPopupControl ID="pcNAlerta" runat="server" Width="620px" Height="470px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true" HeaderText=" "
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="pcNAlerta" ShowCloseButton="true"
                AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
            <ClientSideEvents CloseButtonClick="function(s, e) {onResizeAlerta('1')}"/>    
            <SettingsAdaptivity Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter" HorizontalAlign="WindowCenter" MinWidth="250px" MinHeight="470px"/>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <%--PopupEliminarAlerta--%>
        <dx:ASPxPopupControl ID="popupDelAlt" runat="server" ClientInstanceName="popupDelAlt" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
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
        <%--PopupMenuGrupoAlertas--%>
        <dx:ASPxPopupControl ID="pmGrupo2" runat="server" ClientInstanceName="pmGrupo2" Width="390px" Height="450px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide"
                             AutoUpdatePosition="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
        </dx:ASPxPopupControl>

        <asp:HiddenField ID="hfIndex" runat="server"/>
        <asp:HiddenField ID="hfIndex2" runat="server"/>
        <asp:HiddenField ID="hfHeight" runat="server"/>

        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />
        <asp:HiddenField runat="server" ID="hdregs"/>

        <asp:HiddenField ID="halertas" runat="server"/>
        <asp:HiddenField ID="hsubuser" runat="server"/>

        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" BrowserWindowResized="OnBrowserWindowResized"/>
        </dx:ASPxGlobalEvents>

    </form>
</body>
</html>
