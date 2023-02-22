<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoConductor.aspx.vb" Inherits="ArtemisAdmin.MantenimientoConductor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"/>
    <link href="/Libs/leaflet/css/font-awesome.min.css" rel="stylesheet" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="../../Libs/js/x-notify.min.js"></script>
    <link rel="stylesheet" href="../../Libs/css/notification.css" />
    <script src="../../Libs/js/notification.js"></script>

    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title>CONDUCTORES</title>

    <style type="text/css">
        body, html{
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
        }
        .formLayout {
            margin: 2px 5px 5px 2px;
            overflow: auto !important;
        }

        .mybuttonmenu { width: 40px; 
                         height:40px; 
                         border-radius: 25px 25px 25px 25px; 
                         border: none;
                         background: url('../../images/menucol.png') no-repeat 8% 8%;
        }
        .mybutton2 { border-radius: 25px 25px 25px 25px; }
        .mybutton2p { border-radius: 0px 25px 25px 0px; text-align:left; border: none; }
        .mybuttongrid { border-radius: 10px 10px 10px 10px; 
                        border: none;
                        background: transparent;
        }

        /*.buttonAlign {
            text-align: right;
            float: right;
        }
        @media(max-width:600px) {
            .buttonAlign {
                text-align: center;
                float: right;
            }
        }*/
        

        .mylist .dxlbd{ 
            border: none !important;
            height: auto !important;
        }
        .mytext {
            float: right;
            border-radius: 8px 8px 8px 8px;
        }
        hr.vertical {
            border: 0;
            margin: 0;
            border-left: 1px solid blue;
            display: flex;
            height: 200px;
            float: left;
        }

    </style>

    <script type="text/javascript">
        var isPageSizeCallbackRequired = false;
        const notification = new Notification();

        function ConductorWindow() {
            pcGrupo.Show();
            pcGrupo.SetHeaderText("Nuevo Conductor");
            tbID.SetText("0");
            tbGrupo.SetText("");
            tbDescripcion.SetText("");
            pcGrupo.PerformCallback();
        }

        //Mostrar Ventanas
        //function verVentana1() {
        //    var content1 = document.getElementById("central");
        //    //var content2 = document.getElementById("central2");
        //    content1.style.display = "block"
        //    content2.style.display = "none"

        //}
        //function verVentana2() {
        //    var content1 = document.getElementById("central");
        //    //var content2 = document.getElementById("central2");
        //    content1.style.display = "none"
        //    content2.style.display = "block"
        //}

        //function onInit(s, e) {
        //    fchExpiracionL.SetDate(new Date());
        //    fchExpiracionS.SetDate(new Date());
        //}

        function GetCheckBoxValue(s, e) {
            var value = s.GetChecked();
            if (value) {
                tbMotivoBaja.SetClientVisible(value);
            } else {
                tbMotivoBaja.SetClientVisible(value);
            }
        }
        function GetCheckBoxValue2(s, e) {
            var value = s.GetChecked();
            if (value) {
                tbMotivoBaja2.SetClientVisible(value);
            } else {
                tbMotivoBaja2.SetClientVisible(value);
            }
        }

        function UpdateGrids(s, e) {
            gridConductor.PerformCallback();
        }

        //Autosize GridView
        function AdjustSizeW(flag) {
            var element2 = document.getElementById('lateral');
            if (!flag) {
                element2.style.display = 'none';
                UpdateGridWidth();
                //UpdateGridWidth2();
            } else {
                element2.style.display = 'block';
                UpdateGridWidthReverse();
            }
        }
        function OnInit(s, e) {
            UpdateGridHeight();
            gridConductor.PerformCallback();
        }
        function OnEndCallback(s, e) {
            UpdateGridHeight();

            var lista = JSON.parse(gridConductor.cpNumbersJS);
            var conductor = lista[0];

            //btnTodos.SetText('<div class="row"> <div class="col-sm-8" style="padding-top: 3px">Conductores</div> <div class="col-sm-4"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + conductor + '</span></div> </div>');
            lbCount.SetText('<div style="padding-top: 2px"><span class="badge badge-dark" style="float: right; font-size: 12px;">' + conductor + '</span></div>');
        }

        function OnControlsInitialized(s, e) {
            //var body = document.getElementById('mybody');
            //var positionInfo = body.getBoundingClientRect();
            //var height = positionInfo.height;

            UpdateGridHeight();

            //var element = document.getElementById('central');
            //ASPxClientUtils.AttachEventToElement(element, "resize", function (evt) {
            //    UpdateGridHeight();
            //    UpdateGridWidth();
            //});
        }
        function OnBrowserWindowResized(s, e) {
            UpdateGridHeight();
            gridConductor.PerformCallback();
            //var element = document.getElementById('central');
            //ASPxClientUtils.AttachEventToElement(element, "resize", function (evt) {
            //    UpdateGridHeight();
            //    UpdateGridWidth();
            //});
        }

        //Popup Ventanas
        function OnMoreInfoClick(contentUrl) {
            pcNConductor.SetHeaderText("Nuevo Conductor");
            pcNConductor.SetContentUrl(contentUrl);
            pcNConductor.Show();
        }
        function OnMoreInfoClick2(contentUrl) {
            pcConductor.SetHeaderText("Editar Conductor");
            pcConductor.SetContentUrl(contentUrl);
            pcConductor.Show();
        }
        function OnMoreInfoClick3(contentUrl) {
            popupDelete.SetContentUrl(contentUrl);
            popupDelete.Show();
        }

        function HidePopup() {
            pcNConductor.Hide();
            gridConductor.PerformCallback();
        }
        function HidePopup2() {
            pcConductor.Hide();
            gridConductor.PerformCallback();
        }
        function EliminaHide() {
            popupDelete.Hide();
            gridConductor.PerformCallback();
        }

        //Button Show
        function shTable() {
            btnNuevo.SetClientVisible(!btnNuevo.GetClientVisible());
            btnTodos.SetClientVisible(!btnTodos.GetClientVisible());
            AdjustSizeW(btnNuevo.GetClientVisible());
        }

        function UpdateGridHeight() {
            debugger;
            gridConductor.SetHeight(0);
            var element = document.getElementById('mybody');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 110;
            gridConductor.SetHeight(height);

            //var element2 = document.getElementById('lateral');
            //var positionInfo2 = element2.getBoundingClientRect();
            //var height2 = positionInfo2.height;
            //if (document.body.scrollHeight > height2)
            //    height2 = document.body.scrollHeight - 110;
            //element2.style.height = height2 + 'px';

            document.getElementById('hfHeight').value = height;
        }
        function UpdateGridWidth() {
            gridConductor.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width;
            var element2 = document.getElementById('lateral');
            var positionInfo2 = element2.getBoundingClientRect();
            var width2 = positionInfo2.width;
            //if (document.body.scrollWidth > width)
            //    width = document.body.scrollWidth - 230;
            gridConductor.SetWidth(width + width2 - 60);
        }
        //function UpdateGridHeight2() {
        //    layoutForm.SetHeight(0);
        //    var element = document.getElementById('central2');
        //    var positionInfo = element.getBoundingClientRect();
        //    var positionInfo = element.getBoundingClientRect();
        //    var height = positionInfo.height;
        //    if (document.body.scrollHeight > height)
        //        height = document.body.scrollHeight - 80;
        //    layoutForm.SetHeight(height);
        //}
        //function UpdateGridWidth2() {
        //    layoutForm.SetWidth(0);
        //    var element = document.getElementById('central2');
        //    var positionInfo = element.getBoundingClientRect();
        //    var width = positionInfo.width;
        //    var element2 = document.getElementById('lateral');
        //    var positionInfo2 = element2.getBoundingClientRect();
        //    var width2 = positionInfo2.width;
        //    //if (document.body.scrollWidth > width)
        //    //    width = document.body.scrollWidth - 230;
        //    layoutForm.SetWidth(width+width2-20);
        //}
        function UpdateGridWidthReverse() {
            gridConductor.SetWidth(0);
            var element = document.getElementById('central');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width - 60;
            gridConductor.SetWidth(width);
        }
        document.getElementById("central").addEventListener('resize', function (evt) {
            if (ASPxClientUtils && !ASPxClientUtils.androidPlatform)
                return;
            var activeElement = document.activeElement;
            if (activeElement && (activeElement.tagName === "INPUT" || activeElement.tagName === "TEXTAREA") && activeElement.scrollIntoViewIfNeeded) {
                document.getElementById("central").setTimeout(function () { activeElement.scrollIntoViewIfNeeded(); }, 0);
            }
        });

        //document.getElementById("central2").addEventListener('resize', function (evt) {
        //    if (ASPxClientUtils && !ASPxClientUtils.androidPlatform)
        //        return;
        //    var activeElement = document.activeElement;
        //    if (activeElement && (activeElement.tagName === "INPUT" || activeElement.tagName === "TEXTAREA") && activeElement.scrollIntoViewIfNeeded)
        //        document.getElementById("central2").setTimeout(function () { activeElement.scrollIntoViewIfNeeded(); }, 0);
        //});
        window.addEventListener('resize', function (evt) {
            if (ASPxClientUtils && !ASPxClientUtils.androidPlatform)
                return;
            var activeElement = document.activeElement;
            if (activeElement && (activeElement.tagName === "INPUT" || activeElement.tagName === "TEXTAREA") && activeElement.scrollIntoViewIfNeeded) {
                window.setTimeout(function () { activeElement.scrollIntoViewIfNeeded(); }, 0);
            }
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

    </script>

</head>
<body id="mybody" class="container-fluid">
    <form id="frmGrupos" runat="server">
        <div id="cabecera" class="hstack gap-3 flex" style="margin: 5px 5px 5px 5px; background-color: #f2f1f0; padding: 10px; border-radius: 5px;">
            <div class="col-sm-3" style="display:flex; margin-right: 5px">
                <dx:ASPxButton ID="buttonMenu" runat="server" CssClass="mybuttonmenu" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e) {shTable()}"/>
                </dx:ASPxButton>
                <image src="../../images/conductor.png" style="width:25px; height:35px; margin: 3px 10px 0px 10px;"/>
                <h4 class="mt-1" style="font-weight: bold;">Conductores</h4>
            </div>
            <div runat="server" class="w-100">
                <dx:ASPxTextBox ID="txSearchCon" runat="server" ClientInstanceName="txSearchCon" NullText="Buscar conductor ..." Paddings-PaddingLeft="40"
                                AutoPostBack="false" CssClass="mytext" Width="100%" Height="40px" Font-Size="Small">
                    <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
                </dx:ASPxTextBox>
            </div>
        </div>

        <div id="cuerpo" class="row" style="margin: 5px 5px 0px 5px; padding: 5px; background-color: #f2f1f0; border-radius: 5px;">
            <div id="lateral" class="col-sm-3" style="display:inline-block">
                <div class="col-md w-100">
                    <dx:ASPxButton ID="btnTodos" runat="server" ClientInstanceName="btnTodos" Text="Conductores" Width="120px" Font-Bold="true" Font-Size="10" 
                                   CssClass="mybutton2p" AutoPostBack="false" EncodeHtml="false" ToolTip="Mostrar todos los conductores"
                                   BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632">
                        <ClientSideEvents Click="UpdateGrids"/>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="lbCount" runat="server" ClientInstanceName="lbCount" EncodeHtml="false" Text=" " Font-Bold="true" Font-Size="10" 
                                   CssClass="mybuttongrid" AutoPostBack="false" HoverStyle-BackColor="Transparent" HoverStyle-ForeColor="Transparent"/>
                    <dx:ASPxButton ID="btnNuevo" runat="server" ClientInstanceName="btnNuevo" AutoPostBack="false" CssClass="mybuttongrid" ToolTip="Nuevo Conductor" 
                                   Border-BorderStyle="None" HoverStyle-BackColor="#dadad2" OnInit="btnNuevo_Init">
                        <Image Url="../../images/personplus.png" Width="15" Height="15"/>
                    </dx:ASPxButton>
                </div>
                
            </div>
            <%--<div class="vr" style="width:none"></div>
            <hr class="vertical" />--%>
            <div id="central" class="col" style="display:inline-block">
                <dx:ASPxGridView ID="gridConductor" ClientInstanceName="gridConductor" runat="server" KeyFieldName="id" CssClass="grid" Width="100%" 
                                 AutoGenerateColumns="False" OnDataBinding="gridC_DataBinding" Border-BorderStyle="None" OnCustomCallback="gridConductor_CustomCallback">
                    <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback"/>
                        <Styles>  
                            <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                            <Cell BorderBottom-BorderStyle="Solid"/>
                            <Row BackColor="#f2f1f0"/>
                        </Styles>
                        <Columns>
                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btedit" VisibleIndex="0" AdaptivePriority="2" MaxWidth="40">
                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="editar" runat="server" ClientInstanceName="editar" Visible="true" RenderMode="Button" AutoPostBack="false" HoverStyle-BackColor="#dadad2"  
                                                   CssClass="mybuttongrid" Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" ToolTip="Editar datos"
                                                   OnInit="editar_Init">
                                    </dx:ASPxButton>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption=" " Width="50" Name="btelim" VisibleIndex="1" AdaptivePriority="3" MaxWidth="40">
                                <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="eliminar" runat="server" ClientInstanceName="eliminar" Visible="true" RenderMode="Button" AutoPostBack="false" HoverStyle-BackColor="#dadad2" 
                                                   CssClass="mybuttongrid" Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" ToolTip="Eliminar datos"
                                                   OnInit="eliminar_Init">
                                    </dx:ASPxButton>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="true" CustomEditorID="txSearchCon"/>
                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" 
                                       LastPageButton-Visible="true"/>  
                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                            AllowHideDataCellsByColumnMinWidth="true"/>
                        <SettingsLoadingPanel Mode="Disabled"/>
                        <SettingsBehavior AllowSelectByRowClick="true"/>
                        <SettingsPager EnableAdaptivity="true" Mode="ShowPager"/>
                </dx:ASPxGridView>
            </div>
        </div>

        <%--PopupNuevoConductor--%>
        <dx:ASPxPopupControl ID="pcNConductor" runat="server" Width="1020px" Height="580px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true" HeaderText=" "
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="pcNConductor" ShowCloseButton="true"
                AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
            <SettingsAdaptivity Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter" HorizontalAlign="WindowCenter" MinHeight="560px" MaxWidth="1020px"/>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEditarConductor--%>
        <dx:ASPxPopupControl ID="pcConductor" runat="server" Width="1020px" Height="560px" Modal="false" CloseAction="CloseButton" CloseOnEscape="true" HeaderText=" "
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="pcConductor" ShowCloseButton="true"
                AllowDragging="True" PopupAnimationType="Slide" AutoUpdatePosition="true">
            <SettingsAdaptivity Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter" HorizontalAlign="WindowCenter" MinHeight="560px" MaxWidth="1020px"/>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                        
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEliminarConductor--%>
        <dx:ASPxPopupControl ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Width="400" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                                 PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide" ShowCloseButton="true" HeaderText=" " >
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                        
                </dx:PopupControlContentControl>
            </ContentCollection>
            <SettingsAdaptivity MinWidth="250px" Mode="OnWindowInnerWidth" VerticalAlign="WindowCenter"/>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>    
        
        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" BrowserWindowResized="OnBrowserWindowResized"/>
        </dx:ASPxGlobalEvents>

        <asp:HiddenField ID="hfHeight" runat="server"/>
        
        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />
        <asp:HiddenField runat="server" ID="hdregs"/>
        <asp:HiddenField ID="tbCodigo" runat="server"/>
    </form>
</body>
</html>
