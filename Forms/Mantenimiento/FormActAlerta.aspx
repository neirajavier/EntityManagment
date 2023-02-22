<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormActAlerta.aspx.vb" Inherits="ArtemisAdmin.FormActAlerta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxt" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

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

        .mybutton2 { border-radius: 10px 10px 10px 10px; }

        .mytext {
            float: right;
            border-radius: 8px 8px 8px 8px;
        }
    </style>

    <script type="text/javascript">
        var nodos;

        function onValueChanged(s, e) {
            var isVisible = s.GetValue();
            if (isVisible == 'I') {
                layoutForm.GetItemByName('Geo').SetVisible(true);
                layoutForm.GetItemByName('Grp').SetVisible(false);
            } else {
                layoutForm.GetItemByName('Geo').SetVisible(false);
                layoutForm.GetItemByName('Grp').SetVisible(true);
            }
        }
        function onChangedValue(s, e) {
            //debugger;
            var isVisible = s.GetValue();
            if (isVisible == '1') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(false);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(false);
                layoutForm.GetItemByName('Suceso').SetVisible(true);
            } else if (isVisible == '3' || isVisible == '4') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(true);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(false);
                layoutForm.GetItemByName('Suceso').SetVisible(false);
                layoutForm.GetItemByName('ChkGeo').SetVisible(false);
                //tbNombre.SetText('ALERTA ' + s.GetText() + ': ');
                window.parent.onResizeAlerta(isVisible);
            } else if (isVisible == '2') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(false);
                layoutForm.GetItemByName('LyGrp3').SetVisible(true);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(false);
                layoutForm.GetItemByName('Suceso').SetVisible(false);
                //tbNombre.SetText('ALERTA ' + s.GetText() + ': ');
                tbAlertar.SetText('5000');
                window.parent.onResizeAlerta(isVisible);
            } else if (isVisible == '6') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(true);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(true);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(true);
                layoutForm.GetItemByName('Suceso').SetVisible(true);
                layoutForm.GetItemByName('ChkGeo').SetVisible(true);
                //tbNombre.SetText('ALERTA ' + s.GetText() + ': ');
                window.parent.onResizeAlerta(isVisible);
            } else if (isVisible == '1000') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(false);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(true);
                layoutForm.GetItemByName('Suceso').SetVisible(false);
                //tbNombre.SetText('ALERTA ' + s.GetText() + ': ');
            } else if (isVisible == '2000') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(false);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(true);
                layoutForm.GetItemByName('Suceso').SetVisible(false);
                //tbNombre.SetText('ALERTA ' + s.GetText() + ': ');
            }
        }
        function onValueChanged2(s, e) {
            var isVisible = s.GetValue();
            cbForma.PerformCallback(isVisible);
        }
        function onChangedValue2(s, e) {
            var isVisible = s.GetValue();
            if (isVisible == '1' || isVisible == '2' || isVisible == '3') {
                tbLimiteSup.SetEnabled(true);
                tbLimiteInf.SetEnabled(true);
            } else if (isVisible == '11' || isVisible == '12' || isVisible == '13') {
                tbLimiteSup.SetEnabled(true);
                tbLimiteInf.SetEnabled(false);
            }
        }
        function onChkHoras(s, e) {
            //debugger;
            var isVisible = s.GetValue();
            if (isVisible) {
                cbHoraDesde.SetEnabled(false);
                cbMinutoDesde.SetEnabled(false);
                cbHoraHasta.SetEnabled(false);
                cbMinutoHasta.SetEnabled(false);
            } else {
                cbHoraDesde.SetEnabled(true);
                cbMinutoDesde.SetEnabled(true);
                cbHoraHasta.SetEnabled(true);
                cbMinutoHasta.SetEnabled(true);
            }
            
        }
        function onVisorSelect(dato) {
            //debugger;
            var isVisible = dato;
            if (isVisible == '1') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(false);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(false);
                layoutForm.GetItemByName('Suceso').SetVisible(true);
            } else if (isVisible == '3' || isVisible == '4') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(true);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(false);
                layoutForm.GetItemByName('Suceso').SetVisible(false);
                layoutForm.GetItemByName('ChkGeo').SetVisible(false);
                window.parent.onResizeAlerta(isVisible);
            } else if (isVisible == '2') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(false);
                layoutForm.GetItemByName('LyGrp3').SetVisible(true);
                layoutForm.GetItemByName('LyGrp4').SetVisible(false);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(false);
                layoutForm.GetItemByName('Suceso').SetVisible(false);
                window.parent.onResizeAlerta(isVisible);
            } else if (isVisible == '6') {
                layoutForm.GetItemByName('LyGrp2').SetVisible(true);
                layoutForm.GetItemByName('LyGrp3').SetVisible(false);
                layoutForm.GetItemByName('LyGrp4').SetVisible(true);
                //layoutForm.GetItemByName('LyGrp5').SetVisible(true);
                layoutForm.GetItemByName('Suceso').SetVisible(true);
                window.parent.onResizeAlerta(isVisible);
            } else if (isVisible == '1000') {

            }
        }

        function onInitTree(s, e) {
            s.SelectNode('Entidades', true);
        }
        function onSelectNode(s, e) {
            //debugger;
            s.GetSelectedNodeValues('ParentId', function (values) {
                nodos = values[0];
            });
        }
        function onEndCallBack(s, e) {
            debugger;
            if (nodos == '0') {
                s.GetSelectedNodeValues('Id', function (values2) {
                    if (values2 == 1) {
                        s.GetSelectedNodeValues('Ide', function (values3) {
                            cbRoot.PerformCallback("OKU:" + values3)
                        });
                    } else if (values2 == 2) {
                        s.GetSelectedNodeValues('Ide', function (values3) {
                            cbRoot.PerformCallback("OKG:" + values3)
                        });
                    } else if (values2 == 3) {
                        s.GetSelectedNodeValues('Ide', function (values3) {
                            cbRoot.PerformCallback("OKE:" + values3)
                        });
                    }
                });
            }
        }

        function onChangeSuceso(s, e) {
            var evento = cbAlerta.GetValue();
            if (evento == '1')
                tbNombre.SetText('ALERTA EVENTO: ' + s.GetText());
            //else if (evento == '3' || evento == '4')
            //    tbNombre.SetText('ALERTA GEOCERCA: ' + s.GetText());
            //else if (evento == '2') 
            //    tbNombre.SetText('ALERTA KILOMETRAJE: ' + s.GetText());
            //else if (evento == '6')
            //    tbNombre.SetText('ALERTA MULTICRITERIO: ' + s.GetText());
        }

        function onClick(s, e) {
            if (tbNombre.GetText() == '') {
                window.parent.showNotification('Ingrese un nombre de alerta', 'I')
            } else {
                layoutForm.SetVisible(false);
                layoutForm2.SetVisible(true);
                btnNext.SetClientVisible(!btnNext.GetClientVisible());
                btnSave.SetClientVisible(!btnBack.GetClientVisible());
                btnBack.SetClientVisible(!btnBack.GetClientVisible());
            }
        }
        function onClick2(s, e) {
            layoutForm.SetVisible(true);
            layoutForm2.SetVisible(false);
            
            btnNext.SetClientVisible(!btnNext.GetClientVisible());
            btnSave.SetClientVisible(!btnBack.GetClientVisible());
            btnBack.SetClientVisible(!btnBack.GetClientVisible());
        }

        function onInit(s, e) {
            UpdateGridHeight();
        }
        function UpdateGridHeight() {
            tlAsignacion.SetHeight(0);
            var element = document.getElementById('mybody');
            var positionInfo = element.getBoundingClientRect();
            var height = Math.round(positionInfo.height);
            //if (document.body.scrollHeight > height)
            //    height = document.body.scrollHeight - 50;
            tlAsignacion.SetHeight(100);
        }

        function OnKeyDown(s, e) {
            if ((e.htmlEvent.keyCode >= 48 && e.htmlEvent.keyCode <= 57) || (e.htmlEvent.keyCode >= 96 && e.htmlEvent.keyCode <= 105) || (e.htmlEvent.keyCode >= 8 && e.htmlEvent.keyCode <= 9)) {
                return true;
            } else {
                ASPxClientUtils.PreventEvent(e.htmlEvent);
            }
        }
        function OnKeyPress(s, e) {
            if ((e.htmlEvent.keyCode >= 48 && e.htmlEvent.keyCode <= 57) || (e.htmlEvent.keyCode >= 96 && e.htmlEvent.keyCode <= 105) || (e.htmlEvent.keyCode >= 8 && e.htmlEvent.keyCode <= 9)) {
                return true;
            } else {
                ASPxClientUtils.PreventEvent(e.htmlEvent);
            }
        }
        function OnKeyUp(s, e) {
            if ((e.htmlEvent.keyCode >= 48 && e.htmlEvent.keyCode <= 57) || (e.htmlEvent.keyCode >= 96 && e.htmlEvent.keyCode <= 105) || (e.htmlEvent.keyCode >= 8 && e.htmlEvent.keyCode <= 9)) {
                return true;
            } else {
                ASPxClientUtils.PreventEvent(e.htmlEvent);
            }
        }
    </script>
</head>
<body id="mybody">
    <form id="form1" runat="server">
        <div>
            <dx:ASPxButton ID="btnNext" runat="server" ClientInstanceName="btnNext" Text=" " Width="10%" Font-Bold="true" Font-Size="10" ClientVisible="true"
                           CssClass="mybutton2" AutoPostBack="false" EncodeHtml="false" ToolTip="Ir a las asignaciones" Image-Url="../../images/flecha.png"
                           Image-Height="25" Image-Width="25" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632"
                           Style="float: right; margin-left: 5px">
                <ClientSideEvents Click="onClick"/>
            </dx:ASPxButton>
            <dx:ASPxButton ID="btnSave" runat="server" ClientInstanceName="btnSave" Text=" " Width="10%" Font-Bold="true" Font-Size="10" ClientVisible="false"
                           CssClass="mybutton2" AutoPostBack="false" EncodeHtml="false" ToolTip="Guardar" Image-Url="../../images/save.png" UseSubmitBehavior="false"
                           Image-Height="25" Image-Width="25" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632"
                           Style="float: right; margin-left: 5px" OnClick="btnSave_Click">
                <%--<ClientSideEvents Click="onClick"/>--%>
            </dx:ASPxButton>
            <dx:ASPxButton ID="btnBack" runat="server" ClientInstanceName="btnBack" Text=" " Width="10%" Font-Bold="true" Font-Size="10" ClientVisible="false"
                           CssClass="mybutton2" AutoPostBack="false" EncodeHtml="false" ToolTip="Regresar" Image-Url="../../images/flecha2.png"
                           Image-Height="25" Image-Width="25" BackColor="Transparent" ForeColor="Black" HoverStyle-BackColor="#dadad2" HoverStyle-ForeColor="#DB0632"
                           Style="float: right; margin-left: 5px">
                <ClientSideEvents Click="onClick2"/>
            </dx:ASPxButton>
            <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        
                        <dx:ASPxFormLayout runat="server" ID="layoutForm" ClientInstanceName="layoutForm" Width="100%" Height="100%">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                            <Items>
                                <dx:LayoutGroup Caption=" " GroupBoxStyle-Caption-ForeColor="Black"
                                                GroupBoxDecoration="HeadingLine" GroupBoxStyle-Caption-Font-Size="Medium" Name="LyGrp">
                                    <Items>
                                        <dx:LayoutItem Caption="Nombre Alerta" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbNombre" runat="server" Width="100%" ClientInstanceName="tbNombre" NullText="Ingrese nombre alerta"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tipo Alerta" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbAlerta" DropDownStyle="DropDownList" ClientInstanceName="cbAlerta" Width="200px"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                        <ClientSideEvents ValueChanged="onChangedValue"/>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Sonido Alerta" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbSonido" DropDownStyle="DropDownList" ClientInstanceName="cbSonido" Width="200px"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Evento" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true" 
                                                       Name="Suceso" ClientVisible="false">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbSucesos" DropDownStyle="DropDownList" ClientInstanceName="cbSucesos" Width="100%"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                        <%--<ClientSideEvents ValueChanged="onChangeSuceso"/>--%>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Temporización de la Alerta (HH:mm)" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="Box" 
                                                GroupBoxStyle-Caption-Font-Size="Small" Name="LyGrp1" ColCount="7">
                                    <Items>
                                        <dx:LayoutItem Caption="Desde" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbHoraDesde" DropDownStyle="DropDownList" ClientInstanceName="cbHoraDesde" 
                                                                     IncrementalFilteringMode="None" Width="45px" Font-Size="10">
                                                        <SettingsAdaptivity Mode="Off" />
                                                        <Items>
                                                            <dx:ListEditItem Text="00" Value="00" Selected="true"/>
                                                            <dx:ListEditItem Text="01" Value="01" />
                                                            <dx:ListEditItem Text="02" Value="02" />
                                                            <dx:ListEditItem Text="03" Value="03" />
                                                            <dx:ListEditItem Text="04" Value="04" />
                                                            <dx:ListEditItem Text="05" Value="05" />
                                                            <dx:ListEditItem Text="06" Value="06" />
                                                            <dx:ListEditItem Text="07" Value="07" />
                                                            <dx:ListEditItem Text="08" Value="08" />
                                                            <dx:ListEditItem Text="09" Value="09" />
                                                            <dx:ListEditItem Text="10" Value="10" />
                                                            <dx:ListEditItem Text="11" Value="11" />
                                                            <dx:ListEditItem Text="12" Value="12" />
                                                            <dx:ListEditItem Text="13" Value="13" />
                                                            <dx:ListEditItem Text="14" Value="14" />
                                                            <dx:ListEditItem Text="15" Value="15" />
                                                            <dx:ListEditItem Text="16" Value="16" />
                                                            <dx:ListEditItem Text="17" Value="17" />
                                                            <dx:ListEditItem Text="18" Value="18" />
                                                            <dx:ListEditItem Text="19" Value="19" />
                                                            <dx:ListEditItem Text="20" Value="20" />
                                                            <dx:ListEditItem Text="21" Value="21" />
                                                            <dx:ListEditItem Text="22" Value="22" />
                                                            <dx:ListEditItem Text="23" Value="23" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbMinutoDesde" DropDownStyle="DropDownList" ClientInstanceName="cbMinutoDesde" 
                                                                     IncrementalFilteringMode="None" Width="45px" Font-Size="10">
                                                        <SettingsAdaptivity Mode="Off" />
                                                        <Items>
                                                            <dx:ListEditItem Text="00" Value="00" Selected="true"/>
                                                            <dx:ListEditItem Text="10" Value="10" />
                                                            <dx:ListEditItem Text="20" Value="20" />
                                                            <dx:ListEditItem Text="30" Value="30" />
                                                            <dx:ListEditItem Text="40" Value="40" />
                                                            <dx:ListEditItem Text="50" Value="50" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Hasta" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbHoraHasta" DropDownStyle="DropDownList" ClientInstanceName="cbHoraHasta" 
                                                                     IncrementalFilteringMode="None" Width="45px" Font-Size="10">
                                                        <SettingsAdaptivity Mode="Off" />
                                                        <Items>
                                                            <dx:ListEditItem Text="00" Value="00" />
                                                            <dx:ListEditItem Text="01" Value="01" />
                                                            <dx:ListEditItem Text="02" Value="02" />
                                                            <dx:ListEditItem Text="03" Value="03" />
                                                            <dx:ListEditItem Text="04" Value="04" />
                                                            <dx:ListEditItem Text="05" Value="05" />
                                                            <dx:ListEditItem Text="06" Value="06" />
                                                            <dx:ListEditItem Text="07" Value="07" />
                                                            <dx:ListEditItem Text="08" Value="08" />
                                                            <dx:ListEditItem Text="09" Value="09" />
                                                            <dx:ListEditItem Text="10" Value="10" />
                                                            <dx:ListEditItem Text="11" Value="11" />
                                                            <dx:ListEditItem Text="12" Value="12" />
                                                            <dx:ListEditItem Text="13" Value="13" />
                                                            <dx:ListEditItem Text="14" Value="14" />
                                                            <dx:ListEditItem Text="15" Value="15" />
                                                            <dx:ListEditItem Text="16" Value="16" />
                                                            <dx:ListEditItem Text="17" Value="17" />
                                                            <dx:ListEditItem Text="18" Value="18" />
                                                            <dx:ListEditItem Text="19" Value="19" />
                                                            <dx:ListEditItem Text="20" Value="20" />
                                                            <dx:ListEditItem Text="21" Value="21" />
                                                            <dx:ListEditItem Text="22" Value="22" />
                                                            <dx:ListEditItem Text="23" Value="23" Selected="true"/>
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbMinutoHasta" DropDownStyle="DropDownList" ClientInstanceName="cbMinutoHasta" 
                                                                     IncrementalFilteringMode="None" Width="45px" Font-Size="10">
                                                        <SettingsAdaptivity Mode="Off" />
                                                        <Items>
                                                            <dx:ListEditItem Text="00" Value="00" />
                                                            <dx:ListEditItem Text="10" Value="10" />
                                                            <dx:ListEditItem Text="20" Value="20" />
                                                            <dx:ListEditItem Text="30" Value="30" />
                                                            <dx:ListEditItem Text="40" Value="40" />
                                                            <dx:ListEditItem Text="50" Value="50" />
                                                            <dx:ListEditItem Text="59" Value="59" Selected="true"/>
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption=" " VerticalAlign="Middle" CaptionStyle-Font-Size="10" Name="ChkDias">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxCheckBox ID="chkDia" ClientInstanceName="chkDia" runat="server" 
                                                                     Checked="true" Text="Todo el día" Font-Bold="true">
                                                        <ClientSideEvents ValueChanged="onChkHoras" Init="onChkHoras"/>
                                                    </dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Alerta por Geocerca" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="Box" 
                                                GroupBoxStyle-Caption-Font-Size="Small" Name="LyGrp2" ClientVisible="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Agrupación" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbLicencia" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbLicencia" 
                                                                     Width="50%" Font-Size="10">
                                                        <SettingsAdaptivity Mode="Off" />
                                                        <Items>
                                                            <dx:ListEditItem Text="GEOCERCA" Value="I" Selected="true"/>
                                                            <dx:ListEditItem Text="GRUPO GEOCERCA" Value="G" />
                                                        </Items>
                                                        <ClientSideEvents ValueChanged="onValueChanged"/>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Geocercas" VerticalAlign="Middle" CaptionStyle-Font-Size="10" Name="Geo" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbGeocercas" DropDownStyle="DropDownList" ClientInstanceName="cbGeocercas" Width="90%"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Grupos" VerticalAlign="Middle" CaptionStyle-Font-Size="10" Name="Grp" ClientVisible="false" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbGrupos" DropDownStyle="DropDownList" ClientInstanceName="cbGrupos" Width="90%"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption=" " VerticalAlign="Middle" CaptionStyle-Font-Size="10" Name="ChkGeo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxCheckBox ID="chkDentro" ClientInstanceName="chkDentro" runat="server" 
                                                                     Checked="false" Text="Dentro del Grupo / Geocerca" Font-Bold="true">
                                                    </dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Alerta por Kilometraje" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="Box" 
                                                GroupBoxStyle-Caption-Font-Size="Small" Name="LyGrp3" ColCount="2" ClientVisible="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Alertar a los" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbAlertar" runat="server" Width="80%" ClientInstanceName="tbAlertar"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" MaxLength="6">
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" Text="KM(S)"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Pre-Alertar a los" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbPrealertar" runat="server" Width="80%" ClientInstanceName="tbPrealertar" 
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" MaxLength="6">
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" Text="KM(S)"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Criterios Adicionales" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="Box" 
                                                GroupBoxStyle-Caption-Font-Size="Small" Name="LyGrp4" ColCount="2" ClientVisible="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Límite Velocidad" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbLimite" runat="server" Width="100%" ClientInstanceName="tbLimite"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" MaxLength="3">
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" Text="KM/H"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Datos Alerta de Control" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="Box" 
                                                GroupBoxStyle-Caption-Font-Size="Small" Name="LyGrp5" ClientVisible="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Campo a evaluar" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbEvaluar" DropDownStyle="DropDownList" ClientInstanceName="cbEvaluar" Width="100%"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tipo de Control" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbTipo" DropDownStyle="DropDownList" ClientInstanceName="cbTipo" Width="100%"
                                                                     IncrementalFilteringMode="None" Font-Size="10" AutoPostBack="False">
                                                        <ClientSideEvents ValueChanged="onValueChanged2"/>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Forma de Control" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbForma" DropDownStyle="DropDownList" ClientInstanceName="cbForma" Width="100%"
                                                                     IncrementalFilteringMode="None" Font-Size="10" AutoPostBack="False" OnCallback="cbForma_Callback">
                                                        <ClientSideEvents ValueChanged="onChangedValue2"/>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Límite Superior" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbLimiteSup" runat="server" Width="25%" ClientInstanceName="tbLimiteSup" Text="0" ForeColor="DarkGreen"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Límite Inferior" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbLimiteInf" runat="server" Width="25%" ClientInstanceName="tbLimiteInf" Text="0" ForeColor="DarkRed"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>    
                        </dx:ASPxFormLayout>

                        <dx:ASPxCallback runat="server" ID="cbRoot" ClientInstanceName="cbRoot" OnCallback="cbRoot_Callback"/>
                        <dx:ASPxFormLayout runat="server" ID="layoutForm2" ClientInstanceName="layoutForm2" Width="100%" Height="100%" 
                                           ClientVisible="false">
                            <ClientSideEvents Init="onInit" />
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                            <Items>
                                <dx:LayoutGroup Caption="Asignaciones" GroupBoxStyle-Caption-ForeColor="Black" 
                                                GroupBoxDecoration="HeadingLine" GroupBoxStyle-Caption-Font-Size="Medium" Name="LyGrp6">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txSearchTree" runat="server" ClientInstanceName="txSearchTree" NullText="Buscar ..." Paddings-PaddingLeft="40"
                                                                    AutoPostBack="false" CssClass="mytext" Width="100%" Height="30px" Font-Size="Small">
                                                        <BackgroundImage Repeat="NoRepeat" HorizontalPosition="left" ImageUrl="../../images/search2.png" VerticalPosition="center"/>
                                                    </dx:ASPxTextBox>
                                                    <dxt:ASPxTreeList ID="tlAsignacion" runat="server" AutoGenerateColumns="False" EnableCallBacks="true" KeyFieldName="Id"
                                                                      ParentFieldName="ParentId" ClientInstanceName="tlAsignacion" OnDataBinding="tlAsignacion_DataBinding"
                                                                      Width="100%" CssClass="grid" EnableTheming="true" Theme="Moderno" OnPreRender="tlAsignacion_PreRender"
                                                                      OnSelectionChanged="tlAsignacion_SelectionChanged">
                                                        <Border BorderStyle="Solid"/>
                                                        <ClientSideEvents Init="onInitTree"/>
                                                        <%--<ClientSideEvents SelectionChanged="onSelectNode" EndCallback="onEndCallBack"/>--%>
                                                        <Columns>  
                                                            <dxt:TreeListDataColumn Caption=" " FieldName="Id" VisibleIndex="0" Visible="false"/>
                                                            <dxt:TreeListDataColumn Caption=" " FieldName="ParentId" VisibleIndex="1" Visible="false"/>
                                                            <dxt:TreeListDataColumn Caption=" " FieldName="Ide" VisibleIndex="2" Visible="false"/>
                                                            <dxt:TreeListDataColumn Caption=" " FieldName="Data" VisibleIndex="3" />
                                                        </Columns>      
                                                        <SettingsBehavior AutoExpandAllNodes="false" ExpandCollapseAction="NodeClick" 
                                                                          ExpandNodesOnFiltering="true" ProcessSelectionChangedOnServer="true"/>
                                                        <SettingsSearchPanel Visible="false" ColumnNames="Data" CustomEditorID="txSearchTree"/>
                                                        <SettingsSelection AllowSelectAll="true" Enabled="True" Recursive="true"/>
                                                        <Settings ShowColumnHeaders="false"/>
                                                    </dxt:ASPxTreeList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Notificar a" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="Box" 
                                                GroupBoxStyle-Caption-Font-Size="Small" Name="LyGrp7" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Celular(es)" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <%--<dx:ASPxTextBox ID="tbCelular" runat="server" Width="100%" ClientInstanceName="tbCelular"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>--%>
                                                    <dx:ASPxMemo ID="mnCelular" runat="server" Width="100%" ClientInstanceName="mnCelular"
                                                                 BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                 FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" Height="60"/>
                                                    <dx:ASPxLabel runat="server" Text="Escribir celulares separados por comas(,)" ForeColor="Red" Font-Bold="true" Font-Size="7.5"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Email(s)" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <%--<dx:ASPxTextBox ID="tbEmail" runat="server" Width="100%" ClientInstanceName="tbEmail" 
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>--%>
                                                    <dx:ASPxMemo ID="mnEmail" runat="server" Width="100%" ClientInstanceName="mnEmail"
                                                                 BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                 FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" Height="60"/>
                                                    <dx:ASPxLabel runat="server" Text="Escribir emails separados por comas(,)" ForeColor="Red" Font-Bold="true" Font-Size="7.5"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <%--<dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btGrabar" runat="server" Text="Guardar" Width="80px" AutoPostBack="False" ClientInstanceName="btGrabar"
                                                           Style="float: left; margin-right: 8px" CssClass="mybutton2">
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>
                            </Items>    
                        </dx:ASPxFormLayout>
                    </dx:PanelContent>

                </PanelCollection>
            </dx:ASPxPanel>

            

        </div>

        <asp:HiddenField runat="server" ID="hdregs"/>
    </form>
</body>
</html>
