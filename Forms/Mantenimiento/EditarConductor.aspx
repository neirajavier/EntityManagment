<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditarConductor.aspx.vb" Inherits="ArtemisAdmin.EditarConductor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style type="text/css">
        .mybutton2 { border-radius: 25px 25px 25px 25px; }
    </style>

    <script type="text/javascript">
        function GetCheckBoxValue2(s, e) {
            var value = s.GetChecked();
            if (value) {
                tbMotivoBaja2.SetClientVisible(value);
            } else {
                tbMotivoBaja2.SetClientVisible(value);
            }
        }

        function onInit(s, e) {
            var value = s.GetText();
            if (value.length > 0) {
                tbDriverId2.SetClientVisible(true);
            } else {
                tbDriverId2.SetClientVisible(false);
            }
        }

        function OnBtnClientClick(s, e) {
            window.parent.HidePopup2();
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
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="layoutForm" ClientInstanceName="layoutForm">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500"/>
                            <Items>
                                <dx:LayoutGroup Caption="Datos Conductor" ColCount="2" GroupBoxDecoration="HeadingLine" GroupBoxStyle-Caption-ForeColor="Black">
                                    <GroupBoxStyle>
                                        <Caption Font-Size="12" />
                                    </GroupBoxStyle>
                                    <Items>
                                        <dx:LayoutItem Caption="Identificacion" VerticalAlign="Middle" Paddings-PaddingTop="5" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbIdentificacion2" runat="server" Width="50%" ClientInstanceName="tbIdentificacion2" NullText="Ingrese Identificacion" MaxLength="10"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Driver ID" VerticalAlign="Middle" Paddings-PaddingTop="5" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbDriverId2" runat="server" Width="50%" ClientInstanceName="tbDriverId2" NullText="Ingrese Driver ID" MaxLength="20"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ClientSideEvents Init="onInit"/>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombres" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbNombres2" runat="server" Width="80%" ClientInstanceName="tbNombres2" NullText="Ingrese Nombres"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Apellidos" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbApellidos2" runat="server" Width="80%" ClientInstanceName="tbApellidos2" NullText="Ingrese Apellidos"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Altura" VerticalAlign="Middle" ColumnSpan="2" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbAltura2" runat="server" Width="10%" ClientInstanceName="tbAltura2" NullText="Ingrese Altura CM"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Genero" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox runat="server" ID="cbGenero2" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbGenero2" 
                                                                         Width="150px" Font-Size="10">
                                                                        <SettingsAdaptivity Mode="Off" />
                                                                        <Items>
                                                                            <dx:ListEditItem Text="Masculino" Value="M" Selected="true" />
                                                                            <dx:ListEditItem Text="Femenino" Value="F" />
                                                                        </Items>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tipo Sangre" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox runat="server" ID="cbTipoSangre2" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbTipoSangre2" 
                                                                         Width="100px" Font-Size="10">
                                                            <SettingsAdaptivity Mode="Off" />
                                                            <Items>
                                                                <dx:ListEditItem Text="O+" Value="O+" Selected="true" />
                                                                <dx:ListEditItem Text="O-" Value="O-" />
                                                                <dx:ListEditItem Text="A+" Value="A+" />
                                                                <dx:ListEditItem Text="A-" Value="A-" />
                                                                <dx:ListEditItem Text="B+" Value="B+" />
                                                                <dx:ListEditItem Text="B-" Value="B-" />
                                                                <dx:ListEditItem Text="AB+" Value="AB+" />
                                                                <dx:ListEditItem Text="AB-" Value="AB-" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                         <dx:LayoutItem Caption="Telf. Convencional" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbConvencional2" runat="server" Width="50%" ClientInstanceName="tbConvencional2" NullText="Ingrese No. convencional"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Telf. Celular" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbCelular2" runat="server" Width="50%" ClientInstanceName="tbCelular2" NullText="Ingrese No. celular"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Direccion" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbDireccion2" runat="server" Width="80%" ClientInstanceName="tbDireccion2" NullText="Ingrese direccion domicilio"
                                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Email" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbCorreo2" runat="server" Width="100%" ClientInstanceName="tbCorreo2" NullText="Ingrese correo electronico"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ValidationSettings RegularExpression-ValidationExpression="\w+([-+.']\w+)*\W*@\w+([-.]\w+)*.\w+([-.]\w+)*"
                                                                                RegularExpression-ErrorText="Formato de correo invalido"/>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Fecha Exp. Licencia" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="fchExpiracionL2" runat="server" Width="150px" ClientInstanceName="fchExpiracionL2" UseMaskBehavior="true" 
                                                                         DisplayFormatString="dd/MM/yyyy" Font-Size="10">
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Restriccion Licencia" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbRestriccion2" runat="server" Width="150px" ClientInstanceName="tbRestriccion2" NullText="Ingrese Restriccion"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Alerta Vencimiento" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox runat="server" ID="cbAlertar2" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbAlertar2" 
                                                                         Width="150px" Font-Size="10">
                                                            <SettingsAdaptivity Mode="Off" />
                                                            <Items>
                                                                <dx:ListEditItem Text="SI" Value="1" Selected="true" />
                                                                <dx:ListEditItem Text="NO" Value="0" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Tipo Licencia" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox runat="server" ID="cbLicencia2" DropDownStyle="DropDownList" IncrementalFilteringMode="None"  
                                                                         ClientInstanceName="cbLicencia2" Width="100px" Font-Size="10">
                                                            <SettingsAdaptivity Mode="Off" />
                                                            <Items>
                                                                <dx:ListEditItem Text="A" Value="A" />
                                                                <dx:ListEditItem Text="B" Value="B" Selected="true"/>
                                                                <dx:ListEditItem Text="C" Value="C" />
                                                                <dx:ListEditItem Text="D" Value="D" />
                                                                <dx:ListEditItem Text="E" Value="E" />
                                                                <dx:ListEditItem Text="F" Value="F" />
                                                                <dx:ListEditItem Text="G" Value="G" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="No. Seguro Social" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbSeguroSocial2" runat="server" Width="60%" ClientInstanceName="tbSeguroSocial2" NullText="Ingrese No. Seguro Social"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Poliza de Seguro" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="tbPoliza2" runat="server" Width="50%" ClientInstanceName="tbPoliza2" NullText="Ingrese Poliza"
                                                                            BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                            FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Compañia de Seguros" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox runat="server" ID="cbAseguradora2" DropDownStyle="DropDownList" ClientInstanceName="cbAseguradora2" Width="200px"
                                                                         IncrementalFilteringMode="None"  Font-Size="10">
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Fecha Exp. Seguro" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="fchExpiracionS2" runat="server" Width="150px" ClientInstanceName="fchExpiracionS2" UseMaskBehavior="true" 
                                                                         DisplayFormatString="dd/MM/yyyy" Font-Size="10">
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Contacto Emergencia" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbContactoE2" runat="server" Width="100%" ClientInstanceName="tbContactoE2" NullText="Ingrese contacto emergencia" 
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Numero Emergencia" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbNumeroE2" runat="server" Width="50%" ClientInstanceName="tbNumeroE2" NullText="Ingrese numero emergencia"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                            <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Dar Baja" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCheckBox ID="chkBaja2" ClientInstanceName="chkBaja2" runat="server">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {  GetCheckBoxValue2(s, e); }" />
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Motivo Baja" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="tbMotivoBaja2" runat="server" Width="100%" ClientInstanceName="tbMotivoBaja2" NullText="Ingrese motivo baja"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" ClientVisible="false">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                             </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btGrabar2" runat="server" Text="Guardar" Width="80px" AutoPostBack="False" ClientInstanceName="btGrabar2"
                                                                       Style="float: left; margin-right: 8px" CssClass="mybutton2" OnClick="btGrabar2_Click">
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                         </Items>
                                        </dx:LayoutGroup>
                                    </Items>    
                                </dx:ASPxFormLayout>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
        </div>
    </form>
</body>
</html>
