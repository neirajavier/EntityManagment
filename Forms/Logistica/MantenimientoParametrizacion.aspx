<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoParametrizacion.aspx.vb" Inherits="ArtemisAdmin.MantenimientoParametrizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
	<link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>

    <link rel="stylesheet" href="../../Libs/css/notification.css" />
    <script src="../../Libs/js/notification.js"></script>

    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title>LOGISTICA PARAMETRIZACION</title>

    <style type="text/css">
        body, html {
            width: 100%;
            height: 100%;
            padding: 0;
            margin: 0;
            overflow: auto;
        }

        .grid {
            margin: 0 auto;
            border-radius: 10px;
            background-color: #f2f1f0;
        }

        .mybuttonmenu {
            width: 40px;
            height: 40px;
            border-radius: 25px 25px 25px 25px;
            border: none;
            background: url('../../images/menucol.png') no-repeat 8% 8%;
        }

        .mybutton2p {
            border-radius: 0px 25px 25px 0px;
            text-align: left;
            border: none;
        }

        .mybutton2 {
            border-radius: 25px 25px 25px 25px;
        }

        .mybutton3 {
            border-radius: 15px 15px 15px 15px;
        }

        .mybuttongrid {
            border-radius: 10px 10px 10px 10px;
            border: none;
            background: transparent;
        }

        .mybuttongrid2 {
            border-radius: 10px 10px 10px 10px;
            border: none;
            background: transparent;
            float: right;
            margin-right: 25px;
        }

        .mybuttongrid2f {
            border-radius: 10px 10px 10px 10px;
            border: none;
            background: transparent;
            /*margin-left: 185px;*/
        }

        .mylist .dxlbd {
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
            margin: 10px;
        }

        .mytext {
            border-radius: 8px 8px 8px 8px;
            margin-right: 10px;
        }

        .project-tab {
            width: 100%;
            padding: 10%;
            margin-top: -8%;
        }

            .project-tab #tabs {
                background: #007b5e;
                color: #eee;
                width: 100%;
            }

                .project-tab #tabs h6.section-title {
                    color: #eee;
                }

                .project-tab #tabs .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
                    color: #DB0632; /*#0062cc;*/
                    background-color: transparent;
                    border-color: transparent transparent #f3f3f3;
                    border-bottom: 3px solid !important;
                    font-size: 16px;
                    font-weight: bold;
                }

            .project-tab .nav-link {
                border: 1px solid transparent;
                border-top-left-radius: .25rem;
                border-top-right-radius: .25rem;
                color: #DB0632; /*#0062cc;*/
                font-size: 16px;
                font-weight: 600;
            }

                .project-tab .nav-link:hover {
                    border: none;
                }

            .project-tab thead {
                background: #f3f3f3;
                color: #333;
            }

            .project-tab a {
                text-decoration: none;
                color: #333;
                font-weight: 600;
            }
    </style>

    <script type="text/javascript">
        function OnControlsInitialized(s, e) {
            UpdateGridHeight();
        }
        function OnBrowserWindowResized(s, e) {
            UpdateGridHeight();
            if (pgGrids.GetActiveTab().name == 'tbMotivos') {
                gridMotivo.PerformCallback();
            } else if (pgGrids.GetActiveTab().name == 'tbSubmotivos') {
                gridSubmotivo.PerformCallback();
            }
            if (pgGrids2.GetActiveTab().name == 'tbCategorias') {
                gridCategoria.PerformCallback();
            } else if (pgGrids2.GetActiveTab().name == 'tbSubcategorias') {
                gridSubcategoria.PerformCallback();
            }
            if (pgGrids3.GetActiveTab().name == 'tbTipoUbicacion') {
                gridTipoUbicacion.PerformCallback();
            }
        }

        function ShowMessage(contentUrl) {
            popupDelMot.SetContentUrl(contentUrl);
            popupDelMot.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function ShowMessage2(contentUrl) {
            popupDelSubMot.SetContentUrl(contentUrl);
            popupDelSubMot.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function ShowMessage3(contentUrl) {
            popupDelCat.SetContentUrl(contentUrl);
            popupDelCat.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function ShowMessage4(contentUrl) {
            popupDelSubCat.SetContentUrl(contentUrl);
            popupDelSubCat.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }
        function ShowMessage5(contentUrl) {
            popupDelTipoUb.SetContentUrl(contentUrl);
            popupDelTipoUb.Show();
            var event = event || window.event; ASPxClientUtils.PreventEventAndBubble(event);
        }

        function InitButton(s, e) {
            //debugger;
            if (pgGrids.GetActiveTab().name == 'tbMotivos') {
                btnNuevo.SetClientVisible(!btnNuevo.GetClientVisible());
                btnNuevo2.SetClientVisible(!btnNuevo2.GetClientVisible());
                txMotivo.SetClientVisible(!txMotivo.GetClientVisible());
                txSubmotivo.SetClientVisible(!txSubmotivo.GetClientVisible());
                cbMotivo.SetClientVisible(!cbMotivo.GetClientVisible());
            } else if (pgGrids.GetActiveTab().name == 'tbSubmotivos') {
                btnNuevo.SetClientVisible(!btnNuevo.GetClientVisible());
                btnNuevo2.SetClientVisible(!btnNuevo2.GetClientVisible());
                txMotivo.SetClientVisible(!txMotivo.GetClientVisible());
                txSubmotivo.SetClientVisible(!txSubmotivo.GetClientVisible());
                cbMotivo.SetClientVisible(!cbMotivo.GetClientVisible());
            }
        }

        function InitButton2(s, e) {
            //debugger;
            if (pgGrids2.GetActiveTab().name == 'tbCategorias') {
                btnNuevo3.SetClientVisible(!btnNuevo3.GetClientVisible());
                btnNuevo4.SetClientVisible(!btnNuevo4.GetClientVisible());
                txCategoria.SetClientVisible(!txCategoria.GetClientVisible());
                txSubcategoria.SetClientVisible(!txSubcategoria.GetClientVisible());
                cbCategoria.SetClientVisible(!cbCategoria.GetClientVisible());
            } else if (pgGrids2.GetActiveTab().name == 'tbSubcategorias') {
                btnNuevo3.SetClientVisible(!btnNuevo3.GetClientVisible());
                btnNuevo4.SetClientVisible(!btnNuevo4.GetClientVisible());
                txCategoria.SetClientVisible(!txCategoria.GetClientVisible());
                txSubcategoria.SetClientVisible(!txSubcategoria.GetClientVisible());
                cbCategoria.SetClientVisible(!cbCategoria.GetClientVisible());
            }
        }

        function SaveMotivo(s, e) {
            //debugger;
            if (pgGrids.GetActiveTab().name == 'tbMotivos') {
                gridMotivo.PerformCallback('NUEVO');
                txMotivo.SetText("");
            } else if (pgGrids.GetActiveTab().name == 'tbSubmotivos') {
                gridSubmotivo.PerformCallback('NUEVO');
                txSubmotivo.SetText("");
            }

            cbMotivo.PerformCallback();
        }
        function SaveCategoria(s, e) {
            debugger;
            if (pgGrids2.GetActiveTab().name == 'tbCategorias') {
                gridCategoria.PerformCallback('NUEVO');
                txCategoria.SetText("");
            } else if (pgGrids2.GetActiveTab().name == 'tbSubcategorias') {
                gridSubcategoria.PerformCallback('NUEVO');
                txSubcategoria.SetText("");
            }

            cbCategoria.PerformCallback();
        }
        function SaveTipoUbicacion(s, e) {
            if (pgGrids3.GetActiveTab().name == 'tbTipoUbicacion') {
                gridTipoUbicacion.PerformCallback('NUEVO');
                txTipoUbicacion.SetText("");
            }

        }

        function HidePopup() {
            popupDelMot.Hide();
            gridMotivo.PerformCallback('1');
            cbMotivo.PerformCallback();
            txMotivo.SetText("");
        }
        function HidePopup2() {
            popupDelSubMot.Hide();
            gridSubmotivo.PerformCallback('1');
            txSubmotivo.SetText("");
        }
        function HidePopup3() {
            popupDelCat.Hide();
            gridCategoria.PerformCallback('1');
            cbCategoria.PerformCallback();
            txCategoria.SetText("");
        }
        function HidePopup4() {
            popupDelSubCat.Hide();
            gridSubcategoria.PerformCallback('1');
            txSubcategoria.SetText("");
        }
        function HidePopup5() {
            popupDelTipoUb.Hide();
            gridTipoUbicacion.PerformCallback('1');
            txTipoUbicacion.SetText("");
        }

        //Batch mode
        var currentEditingIndex = -1;
        function onBatchStar(s, e) {
            debugger;
            currentEditingIndex = e.visibleIndex;
        }
        function onBatchEnd(s, e) {
            setTimeout(function () {
                if (s.batchEditApi.HasChanges()) {
                    s.UpdateEdit();
                }
            }, 1000);
        }
        function onAddRow(s, e) {
            currentEditingIndex = e.visibleIndex;
            gridCategoria.AddNewRow();
        }

        //Button Show
        function UpdateGridHeight() {
            gridMotivo.SetHeight(0);
            gridCategoria.SetHeight(0);
            gridTipoUbicacion.SetHeight(0);
            var element = document.getElementById('cuerpo');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 190;
            gridMotivo.SetHeight(height);
            gridCategoria.SetHeight(height);
            gridTipoUbicacion.SetHeight(height);

            gridSubmotivo.SetHeight(0);
            gridSubcategoria.SetHeight(0);
            var element = document.getElementById('cuerpo');
            var positionInfo = element.getBoundingClientRect();
            var height = positionInfo.height;
            if (document.body.scrollHeight > height)
                height = document.body.scrollHeight - 100;
            gridSubmotivo.SetHeight(height);
            gridSubcategoria.SetHeight(height);

            document.getElementById('hfHeight').value = height;
            isPageSizeCallbackRequired = false;
        }
        function UpdateGridWidth() {
            gridMotivo.SetWidth(0);
            gridCategoria.SetWidth(0);
            gridTipoUbicacion.SetWidth(0);
            var element = document.getElementById('cuerpo');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width;

            gridSubmotivo.SetWidth(0);
            gridSubcategoria.SetWidth(0);
            var element = document.getElementById('cuerpo');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width;

            var element2 = document.getElementById('lateral');
            var positionInfo2 = element2.getBoundingClientRect();
            var width2 = positionInfo2.width;

            //if (document.body.scrollWidth > width)
            //    width = document.body.scrollWidth - 500;
            gridMotivo.SetWidth(width + width2 - 30);
            gridSubmotivo.SetWidth(width + width2 - 30);
            gridCategoria.SetWidth(width + width2 - 30);
            gridSubcategoria.SetWidth(width + width2 - 30);
            gridTipoUbicacion.SetWidth(width + width2 - 30);
        }
        function UpdateGridWidthReverse() {
            gridMotivo.SetWidth(0);
            gridCategoria.SetWidth(0);
            gridTipoUbicacion.SetWidth(0);
            var element = document.getElementById('cuerpo');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width - 30;
            gridMotivo.SetWidth(width);
            gridCategoria.SetWidth(width);
            gridTipoUbicacion.SetWidth(width);

            gridSubmotivo.SetWidth(0);
            gridSubcategoria.SetWidth(0);
            var element = document.getElementById('cuerpo');
            var positionInfo = element.getBoundingClientRect();
            var width = positionInfo.width - 30;
            gridSubmotivo.SetWidth(width);
            gridSubcategoria.SetWidth(width);
        }
        document.getElementById("cuerpo").addEventListener('resize', function (evt) {
            if (ASPxClientUtils && !ASPxClientUtils.androidPlatform)
                return;
            var activeElement = document.activeElement;
            if (activeElement && (activeElement.tagName === "INPUT" || activeElement.tagName === "TEXTAREA") && activeElement.scrollIntoViewIfNeeded)
                document.getElementById("cuerpo").setTimeout(function () { activeElement.scrollIntoViewIfNeeded(); }, 0);
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
                    autoClose: 3000,
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
                    autoClose: 3000,
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
                    autoClose: 3000,
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
                    autoClose: 3000,
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
<body>
    <form id="frmMotivo" runat="server">
        <asp:ScriptManager ID="smMaster" runat="server"></asp:ScriptManager>
        <div id="cabecera" class="hstack gap-3" style="margin: 5px 5px 5px 5px; background-color: #f2f1f0; padding: 10px; border-radius: 5px;">
            <image src="../../images/checkpoint.png" style="width:60px; height:50px"/>
            <h4 class="mt-1" style="font-weight: bold; padding-right:5%"">PARAMETRIZACIÓN</h4>
            <%--<dx:ASPxTextBox ID="txSearchVeh" runat="server" ClientInstanceName="txSearchVeh" NullText="Buscar Vehiculo / Subusuario ..." Paddings-PaddingLeft="40"
                            AutoPostBack="false" CssClass="mytext" Width="70%" Height="40px" Font-Size="Small">
                <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
            </dx:ASPxTextBox>--%>
        </div>
        <div id="cuerpo" class="row" style="margin: 5px 5px 0px 5px; padding: 10px; background-color: #f2f1f0; border-radius: 5px;">
            <div class="col-md-12">
                <nav>
                    <div class="nav nav-tabs nav-fill" id="nav-tab" role="tablist">
                        <a class="nav-item nav-link active" id="nav-categoria-tab" data-toggle="tab" href="#nav-categoria" role="tab" aria-controls="nav-categoria" aria-selected="true">CATEGORIAS</a>
                        <a class="nav-item nav-link" id="nav-motivo-tab" data-toggle="tab" href="#nav-motivo" role="tab" aria-controls="nav-motivo" aria-selected="false">MOTIVOS</a>
                        <a class="nav-item nav-link" id="nav-tipoubi-tab" data-toggle="tab" href="#nav-tipoubi" role="tab" aria-controls="nav-tipoubi" aria-selected="false">TIPO UBICACION</a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-categoria" role="tabpanel" aria-labelledby="nav-categoria-tab">
                        <hr class="solid2"/>
                        <div class="row col-10">
                            <dxe:ASPxTextBox ID="txCategoria" runat="server" ClientInstanceName="txCategoria" NullText="Ingrese categoría"
                                            AutoPostBack="false" CssClass="mytext" Width="40%" Height="35px" Font-Size="Small">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxButton ID="btnNuevo3" runat="server" Width="3%" ClientInstanceName="btnNuevo3" ToolTip="Guardar Categoría" AutoPostBack="False"
                                           CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="15" Image-Height="15" Image-Url="../../images/save.png" 
                                           HoverStyle-BackColor="#dadad2">
                                <ClientSideEvents Click="SaveCategoria"/>
                            </dxe:ASPxButton>
                                    
                            <dxe:ASPxTextBox ID="txSubcategoria" runat="server" ClientInstanceName="txSubcategoria" NullText="Ingrese subcategoría" ClientVisible="false"
                                            AutoPostBack="false" CssClass="mytext" Width="40%" Height="35px" Font-Size="Small">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxComboBox runat="server" ID="cbCategoria" DropDownStyle="DropDownList" ClientInstanceName="cbCategoria" Width="300px" ClientVisible="false"
                                             IncrementalFilteringMode="None" Font-Size="10" CssClass="mytext" OnCallback="cbCategoria_Callback" AutoPostBack="False">
                            </dxe:ASPxComboBox>
                            <dxe:ASPxButton ID="btnNuevo4" runat="server" Width="3%" ClientInstanceName="btnNuevo4" ToolTip="Guardar Subcategoría" ClientVisible="false" AutoPostBack="False" 
                                           CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="15" Image-Height="15" Image-Url="../../images/save.png" 
                                           HoverStyle-BackColor="#dadad2">
                                <ClientSideEvents Click="SaveCategoria"/>
                            </dxe:ASPxButton>
                        </div>
                        <hr class="solid2"/>
                        <dx:ASPxPageControl ID="pgGrids2" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids2" EnableCallBacks="false"
                                                    BackColor="#f2f1f0">
                            <ClientSideEvents ActiveTabChanged="InitButton2"/>
                            <TabPages>
                                <dx:TabPage Name="tbCategorias" Text="CATEGORIAS" ActiveTabStyle-Font-Bold="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server" BackColor="#f2f1f0">
                                            <dx:ASPxGridView ID="gridCategoria" ClientInstanceName="gridCategoria" runat="server" KeyFieldName="idC" CssClass="grid" Width="100%"
                                                             AutoGenerateColumns="False" Border-BorderStyle="None" EnableCallBacks="false" OnDataBinding="gridCategoria_DataBinding"
                                                             EnableCallbackAnimation="false" EnableCallbackCompression="false" OnCustomCallback="gridCategoria_CustomCallback"
                                                             EnablePagingCallbackAnimation="false" OnRowUpdating="gridCategoria_RowUpdating" OnRowInserting="gridCategoria_RowInserting"
                                                             OnBatchUpdate="gridCategoria_BatchUpdate"> 
                                                <ClientSideEvents BatchEditEndEditing="onBatchEnd" BatchEditStartEditing="onBatchStar"/>
                                                <Styles>  
                                                    <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                                    <Cell BorderBottom-BorderStyle="Solid" Paddings-PaddingBottom="10px" Paddings-PaddingTop="10px"/>
                                                    <Row BackColor="#f2f1f0"/>
                                                </Styles>
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowCancelButton="true" Width="15" VisibleIndex="0"
                                                                              ButtonRenderMode="Button">
                                                        <HeaderTemplate>  
                                                            <dx:ASPxButton runat="server" Text=" " RenderMode="Button" AutoPostBack="false" Image-Url="../../images/plus.png" 
                                                                           Image-Height="15" Image-Width="15" CssClass="mybuttongrid" HoverStyle-BackColor="#dadad2">  
                                                                <ClientSideEvents Click="onAddRow" />
                                                            </dx:ASPxButton>  
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="bteditC" Name="btedit3" VisibleIndex="2" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="editar3" runat="server" AutoPostBack="False" ClientInstanceName="editar3" RenderMode="Button" ToolTip="Ingresar Subcategoría"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="btdeleteC" Name="btdelete3" VisibleIndex="3" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="eliminar3" runat="server" AutoPostBack="False" ClientInstanceName="eliminar3" RenderMode="Button" ToolTip="Eliminar Categoría"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2" OnInit="eliminar3_Init">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <%--<dx:GridViewDataColumn Caption=" " Width="50" FieldName="btdetalleC" Name="btdetalle3" VisibleIndex="4" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="detalle3" runat="server" AutoPostBack="False" ClientInstanceName="detalle3" RenderMode="Button" ToolTip="Ver Subcategorias"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/detalle.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2" OnInit="eliminar3_Init">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>--%>
                                                </Columns>
                                                <SettingsSearchPanel Visible="false" CustomEditorID="txSearchVeh"/>
                                                <SettingsBehavior AllowSelectByRowClick="false" />
                                                <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                                <SettingsLoadingPanel Mode="Disabled"/>
                                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                                    AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" 
                                                                 BatchEditSettings-StartEditAction="FocusedCellClick" BatchEditSettings-KeepChangesOnCallbacks="False"/>
                                                <Settings ShowStatusBar="Hidden"/>
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="tbSubcategorias" Text="SUBCATEGORIAS" ActiveTabStyle-Font-Bold="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <dx:ASPxGridView ID="gridSubcategoria" ClientInstanceName="gridSubcategoria" runat="server" KeyFieldName="idSc" CssClass="grid" Width="100%"
                                                             AutoGenerateColumns="False" Border-BorderStyle="None" EnableCallBacks="false" OnDataBinding="gridSubcategoria_DataBinding"
                                                             EnableCallbackAnimation="false" EnableCallbackCompression="false" EnablePagingCallbackAnimation="false" 
                                                             OnCustomCallback="gridSubcategoria_CustomCallback" OnRowUpdating="gridSubcategoria_RowUpdating" OnBatchUpdate="gridSubcategoria_BatchUpdate">
                                                <Styles>  
                                                    <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                                    <Cell BorderBottom-BorderStyle="Solid"/>
                                                    <Row BackColor="#f2f1f0"/>
                                                </Styles>
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="btdeleteSc" Name="btdelete4" VisibleIndex="4" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="eliminar4" runat="server" AutoPostBack="False" ClientInstanceName="eliminar4" RenderMode="Button" ToolTip="Eliminar Subcategoría"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2" OnInit="eliminar4_Init">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsSearchPanel Visible="false"/>
                                                <SettingsBehavior AllowSelectByRowClick="true"/>
                                                <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                                <SettingsLoadingPanel Mode="Disabled"/>
                                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                                    AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                                <%--<SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" BatchEditSettings-StartEditAction="FocusedCellClick" BatchEditSettings-KeepChangesOnCallbacks="False"/>
                                                <Settings ShowStatusBar="Hidden"/>--%>
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                    <div class="tab-pane fade" id="nav-motivo" role="tabpanel" aria-labelledby="nav-motivo-tab">
                        <hr class="solid2"/>
                        <div class="row col-10">
                            <dxe:ASPxTextBox ID="txMotivo" runat="server" ClientInstanceName="txMotivo" NullText="Ingrese motivo"
                                            AutoPostBack="false" CssClass="mytext" Width="40%" Height="35px" Font-Size="Small">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxButton ID="btnNuevo" runat="server" Width="3%" ClientInstanceName="btnNuevo" ToolTip="Guardar Motivo" AutoPostBack="False"
                                           CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="15" Image-Height="15" Image-Url="../../images/save.png" 
                                           HoverStyle-BackColor="#dadad2">
                                <ClientSideEvents Click="SaveMotivo"/>
                            </dxe:ASPxButton>
                                    
                            <dxe:ASPxTextBox ID="txSubmotivo" runat="server" ClientInstanceName="txSubmotivo" NullText="Ingrese submotivo" ClientVisible="false"
                                            AutoPostBack="false" CssClass="mytext" Width="40%" Height="35px" Font-Size="Small">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxComboBox runat="server" ID="cbMotivo" DropDownStyle="DropDownList" ClientInstanceName="cbMotivo" Width="300px" ClientVisible="false"
                                             IncrementalFilteringMode="None" Font-Size="10" CssClass="mytext" OnCallback="cbMotivo_Callback" AutoPostBack="False">
                            </dxe:ASPxComboBox>
                            <dxe:ASPxButton ID="btnNuevo2" runat="server" Width="3%" ClientInstanceName="btnNuevo2" ToolTip="Guardar Submotivo" ClientVisible="false" AutoPostBack="False" 
                                           CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="15" Image-Height="15" Image-Url="../../images/save.png" 
                                           HoverStyle-BackColor="#dadad2">
                                <ClientSideEvents Click="SaveMotivo"/>
                            </dxe:ASPxButton>
                        </div>
                        <hr class="solid2"/>
                        <dx:ASPxPageControl ID="pgGrids" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids" EnableCallBacks="false"
                                                    BackColor="#f2f1f0">
                            <ClientSideEvents ActiveTabChanged="InitButton"/>
                            <TabPages>
                                <dx:TabPage Name="tbMotivos" Text="MOTIVOS" ActiveTabStyle-Font-Bold="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ccMotivos" runat="server" BackColor="#f2f1f0">
                                            <dx:ASPxGridView ID="gridMotivo" ClientInstanceName="gridMotivo" runat="server" KeyFieldName="idM" CssClass="grid" Width="100%"
                                                             AutoGenerateColumns="False" Border-BorderStyle="None" EnableCallBacks="false" OnDataBinding="gridMotivo_DataBinding"
                                                             EnableCallbackAnimation="false" EnableCallbackCompression="false" OnCustomCallback="gridMotivo_CustomCallback"
                                                             EnablePagingCallbackAnimation="false" OnRowUpdating="gridMotivo_RowUpdating" OnRowInserting="gridMotivo_RowInserting"
                                                             OnBatchUpdate="gridMotivo_BatchUpdate">
                                                <ClientSideEvents BatchEditEndEditing="onBatchEnd" BatchEditStartEditing="onBatchStar"/>
                                                <Styles>  
                                                    <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                                    <Cell BorderBottom-BorderStyle="Solid" Paddings-PaddingBottom="10px" Paddings-PaddingTop="10px"/>
                                                    <Row BackColor="#f2f1f0"/>
                                                </Styles>
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowCancelButton="true" Width="50" VisibleIndex="0"
                                                                              ButtonRenderMode="Button"/>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="bteditM" Name="btedit" VisibleIndex="2" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="editar" runat="server" AutoPostBack="False" ClientInstanceName="editar" RenderMode="Button" ToolTip="Ingresar Submotivos"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/pencil.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="btdeleteM" Name="btdelete" VisibleIndex="3" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="eliminar" runat="server" AutoPostBack="False" ClientInstanceName="eliminar" RenderMode="Button" ToolTip="Eliminar Motivo"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2" OnInit="eliminar_Init">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsSearchPanel Visible="false"/>
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
                                <dx:TabPage Name="tbSubmotivos" Text="SUBMOTIVOS" ActiveTabStyle-Font-Bold="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ccSubmotivos" runat="server">
                                            <dx:ASPxGridView ID="gridSubmotivo" ClientInstanceName="gridSubmotivo" runat="server" KeyFieldName="idSm" CssClass="grid" Width="100%"
                                                             AutoGenerateColumns="False" Border-BorderStyle="None" EnableCallBacks="false" OnDataBinding="gridSubmotivo_DataBinding"
                                                             EnableCallbackAnimation="false" EnableCallbackCompression="false" EnablePagingCallbackAnimation="false" 
                                                             OnCustomCallback="gridSubmotivo_CustomCallback" OnRowUpdating="gridSubmotivo_RowUpdating" OnBatchUpdate="gridSubmotivo_BatchUpdate">
                                                <Styles>  
                                                    <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                                    <Cell BorderBottom-BorderStyle="Solid"/>
                                                    <Row BackColor="#f2f1f0"/>
                                                </Styles>
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="btdeleteSm" Name="btdelete2" VisibleIndex="4" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="eliminar2" runat="server" AutoPostBack="False" ClientInstanceName="eliminar2" RenderMode="Button" ToolTip="Eliminar Submotivo"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2" OnInit="eliminar2_Init">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsSearchPanel Visible="false"/>
                                                <SettingsBehavior AllowSelectByRowClick="true"/>
                                                <SettingsPager EnableAdaptivity="true" Mode="ShowPager" NumericButtonCount="5" FirstPageButton-Visible="true" LastPageButton-Visible="true"/>
                                                <SettingsLoadingPanel Mode="Disabled"/>
                                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"
                                                                    AllowHideDataCellsByColumnMinWidth="true"></SettingsAdaptivity>
                                                <%--<SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" BatchEditSettings-StartEditAction="FocusedCellClick" BatchEditSettings-KeepChangesOnCallbacks="False"/>
                                                <Settings ShowStatusBar="Hidden"/>--%>
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                    <div class="tab-pane fade" id="nav-tipoubi" role="tabpanel" aria-labelledby="nav-tipoubi-tab">
                        <hr class="solid2"/>
                        <div class="row col-10">
                            <dxe:ASPxTextBox ID="txTipoUbicacion" runat="server" ClientInstanceName="txTipoUbicacion" NullText="Ingrese tipo ubicación"
                                            AutoPostBack="false" CssClass="mytext" Width="40%" Height="35px" Font-Size="Small">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxButton ID="btnNuevo5" runat="server" Width="3%" ClientInstanceName="btnNuevo5" ToolTip="Guardar Tipo Ubicacion" AutoPostBack="False"
                                           CssClass="mybuttongrid" Font-Size="8" Font-Bold="true" Image-Width="15" Image-Height="15" Image-Url="../../images/save.png" 
                                           HoverStyle-BackColor="#dadad2">
                                <ClientSideEvents Click="SaveTipoUbicacion"/>
                            </dxe:ASPxButton>
                        </div>
                        <hr class="solid2"/>
                        <dx:ASPxPageControl ID="pgGrids3" runat="server" ActiveTabIndex="0" Font-Size="Small" Width="100%" ClientInstanceName="pgGrids3" EnableCallBacks="false"
                                            BackColor="#f2f1f0">
                            <TabPages>
                                <dx:TabPage Name="tbTipoUbicacion" Text="TIPO UBICACION" ActiveTabStyle-Font-Bold="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server" BackColor="#f2f1f0">
                                            <dx:ASPxGridView ID="gridTipoUbicacion" ClientInstanceName="gridTipoUbicacion" runat="server" KeyFieldName="idT" CssClass="grid" Width="100%"
                                                             AutoGenerateColumns="False" Border-BorderStyle="None" EnableCallBacks="false" OnDataBinding="gridTipoUbicacion_DataBinding"
                                                             EnableCallbackAnimation="false" EnableCallbackCompression="false" OnCustomCallback="gridTipoUbicacion_CustomCallback"
                                                             EnablePagingCallbackAnimation="false" OnRowUpdating="gridTipoUbicacion_RowUpdating" OnRowInserting="gridTipoUbicacion_RowInserting"
                                                             OnBatchUpdate="gridTipoUbicacion_BatchUpdate">
                                                <ClientSideEvents BatchEditEndEditing="onBatchEnd" BatchEditStartEditing="onBatchStar"/>
                                                <Styles>  
                                                    <Header BackColor="#a9a8a8" Font-Bold="true" ForeColor="White"/>
                                                    <Cell BorderBottom-BorderStyle="Solid" Paddings-PaddingBottom="10px" Paddings-PaddingTop="10px"/>
                                                    <Row BackColor="#f2f1f0"/>
                                                </Styles>
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowCancelButton="true" Width="50" VisibleIndex="0"
                                                                              ButtonRenderMode="Button"/>
                                                    <dx:GridViewDataColumn Caption=" " Width="50" FieldName="btdeleteT" Name="btdelete5" VisibleIndex="2" 
                                                                           MaxWidth="80" AdaptivePriority="0" EditFormSettings-Visible="False">
                                                        <HeaderStyle>  <Border BorderStyle="None" />  </HeaderStyle>
                                                        <CellStyle>  <Border BorderStyle="None" />  </CellStyle>
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="eliminar5" runat="server" AutoPostBack="False" ClientInstanceName="eliminar5" RenderMode="Button" ToolTip="Eliminar Motivo"
                                                                           Border-BorderStyle="None" Image-Width="15" Image-Height="15" Image-Url="../../images/trash.png" CssClass="mybuttongrid"
                                                                           HoverStyle-BackColor="#dadad2" OnInit="eliminar5_Init">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsSearchPanel Visible="false"/>
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
        </div>

        <%--PopupEliminarMotivo--%>
        <dx:ASPxPopupControl ID="popupDelMot" runat="server" ClientInstanceName="popupDelMot" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEliminarSubmotivo--%>
        <dx:ASPxPopupControl ID="popupDelSubMot" runat="server" ClientInstanceName="popupDelSubMot" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEliminarCategoria--%>
        <dx:ASPxPopupControl ID="popupDelCat" runat="server" ClientInstanceName="popupDelCat" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEliminarSubcategoria--%>
        <dx:ASPxPopupControl ID="popupDelSubCat" runat="server" ClientInstanceName="popupDelSubCat" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <%--PopupEliminarTipoUbicacion--%>
        <dx:ASPxPopupControl ID="popupDelTipoUb" runat="server" ClientInstanceName="popupDelTipoUb" Width="420" Height="185" Modal="false" CloseAction="CloseButton" CloseOnEscape="true"
                             HeaderText=" " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Slide">
	        <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <asp:HiddenField runat="server" ID="hidusuario"/>
        <asp:HiddenField runat="server" ID="hidsubusuario"/>
        <asp:HiddenField runat="server" ID="hdregs"/>
        <asp:HiddenField runat="server" ID="hfHeight"/>

        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" BrowserWindowResized="OnBrowserWindowResized"/>
        </dx:ASPxGlobalEvents>

    </form>
</body>
</html>
