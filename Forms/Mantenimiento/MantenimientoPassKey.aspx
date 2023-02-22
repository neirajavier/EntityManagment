<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoPassKey.aspx.vb" Inherits="ArtemisAdmin.MantenimientoPassKey" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>LLAVE MAESTRA</title>

    <style type="text/css">
        .fullHeight {
            height: 100% !important;
        }
        .fullWidth {
            width: 100% !important;
        }
        .mybutton2 { border-radius: 25px 25px 25px 25px; }
    </style>

    <script type="text/javascript">
        function onSelectedIndexChanged(s, e) {
            var selectedItem = s.GetSelectedItem();
            var dato = selectedItem.text.split("::");
            tbAlias.SetText(dato[0].trim());

            cbComandos.PerformCallback();
        }
        function onSelectedIndexChanged2(s, e) {
            var selectedItem = s.GetSelectedItem();
            var dato = selectedItem.text.split("::");
            tbGeocerca.SetText(dato[0].trim());
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
<body>
    <form id="fPassKey" runat="server">
        <div>
            <dx:ASPxFormLayout ID="FormLayout" ClientInstanceName="FormLayout" runat="server" RequiredMarkDisplayMode="Auto" 
                               UseDefaultPaddings="false" AlignItemCaptionsInAllGroups="true" Width="70%">
                <Paddings PaddingBottom="15" PaddingTop="15" />
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit"></SettingsAdaptivity>
                <Styles>
                    <LayoutGroupBox CssClass="fullWidth fullHeight"></LayoutGroupBox>
                    <LayoutGroup Cell-CssClass="fullHeight"></LayoutGroup>
                </Styles>
                <Items>
                    <dx:LayoutGroup Caption="GENERACION PASSKEY" GroupBoxDecoration="Box" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxStyle-Caption-Font-Size="14" ColCount="2">
                        <Items>
                            <dx:LayoutItem Caption="Activo" VerticalAlign="Middle" Paddings-PaddingTop="15" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true" >
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cbActivos" runat="server" ClientInstanceName="cbActivos" Width="100%" DropDownStyle="DropDown">
                                            <ClientSideEvents SelectedIndexChanged="onSelectedIndexChanged" />
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" Paddings-PaddingTop="15">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="tbAlias" runat="server" Width="100%" ClientInstanceName="tbAlias" NullText="Activo seleccionado" ReadOnly="true"
                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Vigencia" VerticalAlign="Middle" Paddings-PaddingTop="10" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true" ColumnSpan="2">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit ID="fchVigencia" runat="server" Width="43%" ClientInstanceName="fchVigencia" UseMaskBehavior="true" 
                                                         DisplayFormatString="dd/MM/yyyy" Font-Size="10">
                                            <ClientSideEvents Init="function(s, e) { fchVigencia.SetDate(new Date()); }" />
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Comandos" VerticalAlign="Middle" Paddings-PaddingTop="15" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cbComandos" runat="server" ClientInstanceName="cbComandos" Width="100%" DropDownStyle="DropDown" OnCallback="cbComandos_Callback">
                                            <%--<ClientSideEvents SelectedIndexChanged="onSelectedIndexChanged" />--%>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" Paddings-PaddingTop="15">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="tbComandos" runat="server" Width="100%" ClientInstanceName="tbComandos" NullText="Comandos seleccionados" ReadOnly="true"
                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Horas Uso" VerticalAlign="Middle" Paddings-PaddingTop="10" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <span style="font-weight: bold">Desde<//span>
                                        <dx:ASPxDateEdit ID="hrsInicio" runat="server" Width="150px" ClientInstanceName="hrsInicio" UseMaskBehavior="true" Font-Size="10" 
                                                         EditFormat="Time" EditFormatString="hh:mm tt" DropDownButton-Visible="false">
                                            <ClientSideEvents Init="function(s, e) { hrsInicio.SetDate(new Date()); }" />
                                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormat="Time" TimeEditProperties-EditFormatString="hh:mm tt"/>
                                        </dx:ASPxDateEdit>
                                        <span style="font-weight: bold">Hasta</span>
                                        <dx:ASPxDateEdit ID="hrsFin" runat="server" Width="150px" ClientInstanceName="hrsFin" UseMaskBehavior="true" Font-Size="10" 
                                                         EditFormat="Time" EditFormatString="hh:mm tt" DropDownButton-Visible="false">
                                            <ClientSideEvents Init="function(s, e) { hrsFin.SetDate(new Date()); }" />
                                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormat="Time" TimeEditProperties-EditFormatString="hh:mm tt"/>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Cercanía" VerticalAlign="Middle" Paddings-PaddingTop="10" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true" ColumnSpan="2">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <%--<dx:ASPxTextBox ID="tbCercania" runat="server" Width="43%" ClientInstanceName="tbCercania" NullText="Ingrese Cercania en MTS"
                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                            <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                        </dx:ASPxTextBox>--%>
                                        <dx:ASPxSpinEdit ID="seCercania" runat="server" Number="5" NumberType="Integer" Width="10%" LargeIncrement="5" 
                                                         HorizontalAlign="Left" NullText="Cercania en MTS" NullTextDisplayMode="UnfocusedAndFocused">
                                            <SpinButtons ShowIncrementButtons="False" ShowLargeIncrementButtons="True" />
                                        </dx:ASPxSpinEdit><span style="font-weight: bold">en mts.</span>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Ubicación" VerticalAlign="Middle" Paddings-PaddingTop="15" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cbGeocercas" runat="server" ClientInstanceName="cbGeocercas" Width="100%" DropDownStyle="DropDown">
                                            <ClientSideEvents SelectedIndexChanged="onSelectedIndexChanged2" />
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle" Paddings-PaddingTop="15">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%" ClientInstanceName="tbGeocerca" NullText="Geocerca seleccionada" ReadOnly="true"
                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="5">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton ID="btGrabar" runat="server" Text="Guardar" Width="80px" AutoPostBack="False" ClientInstanceName="btGrabar"
                                               Style="float: left; margin-right: 8px" CssClass="mybutton2" OnClick="btGrabar_Click">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:ASPxFormLayout>
        </div>

        <asp:HiddenField runat="server" ID="hidusuario" />
        <asp:HiddenField runat="server" ID="hidsubusuario" />

    </form>
</body>
</html>
