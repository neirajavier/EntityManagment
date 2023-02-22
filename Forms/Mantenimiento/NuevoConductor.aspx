<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NuevoConductor.aspx.vb" Inherits="ArtemisAdmin.NuevoConductor" %>
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
        function GetCheckBoxValue(s, e) {
            var value = s.GetChecked();
            if (value) {
                tbMotivoBaja.SetClientVisible(value);
            } else {
                tbMotivoBaja.SetClientVisible(value);
            }
        }

        function GetCheckBoxValueDriver(s, e) {
            var value = s.GetChecked();
            if (value) {
                tbDriverId.SetClientVisible(value);
            } else {
                tbDriverId.SetClientVisible(value);
            }
        }

        function OnBtnClientClick(s, e) {
            window.parent.HidePopup();
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
            <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="layoutForm" ClientInstanceName="layoutForm" Width="100%" Height="100%">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                            <Items>
                                <dx:LayoutGroup Caption="Datos Conductor" ColCount="2" GroupBoxStyle-Caption-ForeColor="Black" GroupBoxDecoration="HeadingLine">
                                    <Items>
                                        <dx:LayoutItem Caption="Identificacion" VerticalAlign="Middle" Paddings-PaddingTop="5" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbIdentificacion" runat="server" Width="50%" ClientInstanceName="tbIdentificacion" NullText="Ingrese Identificacion" MaxLength="10"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10">
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                        <%--<MaskSettings Mask="0000000000"/>
                                                        <ValidationSettings RegularExpression-ValidationExpression="[0-9]*" RegularExpression-ErrorText="Ingrese solo numeros"/>--%>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Driver ID" VerticalAlign="Middle" Paddings-PaddingTop="5" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbDriverId" runat="server" Width="50%" ClientInstanceName="tbDriverId" NullText="Ingrese Driver ID" MaxLength="20"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" ClientVisible="false"/>
                                                    <dx:ASPxCheckBox ID="chkDriver" ClientInstanceName="chkDriver" runat="server" Checked="false" Text="Driver ID" ToolTip="Solo para dispositivos llave DALLAS">
                                                            <ClientSideEvents ValueChanged="GetCheckBoxValueDriver"/>
                                                    </dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombres" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbNombres" runat="server" Width="80%" ClientInstanceName="tbNombres" NullText="Ingrese Nombres"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                         <dx:LayoutItem Caption="Apellidos" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbApellidos" runat="server" Width="80%" ClientInstanceName="tbApellidos" NullText="Ingrese Apellidos"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Altura" VerticalAlign="Middle" ColumnSpan="2" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbAltura" runat="server" Width="15%" ClientInstanceName="tbAltura" NullText="Ingrese Altura CM"
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
                                                    <dx:ASPxComboBox runat="server" ID="cbGenero" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbGenero" 
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
                                                    <dx:ASPxComboBox runat="server" ID="cbTipoSangre" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbTipoSangre" 
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
                                                    <dx:ASPxTextBox ID="tbConvencional" runat="server" Width="60%" ClientInstanceName="tbConvencional" NullText="Ingrese No. convencional"
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
                                                    <dx:ASPxTextBox ID="tbCelular" runat="server" Width="60%" ClientInstanceName="tbCelular" NullText="Ingrese No. celular"
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
                                                    <dx:ASPxTextBox ID="tbDireccion" runat="server" Width="80%" ClientInstanceName="tbDireccion" NullText="Ingrese direccion domicilio"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Email" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbCorreo" runat="server" Width="100%" ClientInstanceName="tbCorreo" NullText="Ingrese correo electronico"
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
                                                    <dx:ASPxDateEdit ID="fchExpiracionL" runat="server" Width="150px" ClientInstanceName="fchExpiracionL" UseMaskBehavior="true" 
                                                                                 DisplayFormatString="dd/MM/yyyy" Font-Size="10">
                                                        <ClientSideEvents Init="function(s, e) { fchExpiracionL.SetDate(new Date()); }" />
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
                                                    <dx:ASPxTextBox ID="tbRestriccion" runat="server" Width="50%" ClientInstanceName="tbRestriccion" NullText="Ingrese Restriccion"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Alerta Vencimiento" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbAlertar" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbAlertar" 
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
                                                    <dx:ASPxComboBox runat="server" ID="cbLicencia" DropDownStyle="DropDownList" IncrementalFilteringMode="None" ClientInstanceName="cbLicencia" 
                                                                     Width="100px" Font-Size="10">
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
                                                    <dx:ASPxTextBox ID="tbSeguroSocial" runat="server" Width="60%" ClientInstanceName="tbSeguroSocial" NullText="Ingrese No. Seguro Social"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Poliza de Seguro" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbPoliza" runat="server" Width="50%" ClientInstanceName="tbPoliza" NullText="Ingrese Poliza"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Compañia de Seguros" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="cbAseguradora" DropDownStyle="DropDownList" ClientInstanceName="cbAseguradora" Width="200px"
                                                                     IncrementalFilteringMode="None" Font-Size="10">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha Exp. Seguro" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit ID="fchExpiracionS" runat="server" Width="150px" ClientInstanceName="fchExpiracionS" UseMaskBehavior="true" 
                                                                     DisplayFormatString="dd/MM/yyyy" Font-Size="10">
                                                        <ClientSideEvents Init="function (s, e) { fchExpiracionS.SetDate(new Date()); }" />
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
                                                    <dx:ASPxTextBox ID="tbContactoE" runat="server" Width="100%" ClientInstanceName="tbContactoE" NullText="Ingrese contacto emergencia" 
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Numero Emergencia" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbNumeroE" runat="server" Width="60%" ClientInstanceName="tbNumeroE" NullText="Ingrese numero emergencia"
                                                                    BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                    FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10"/>
                                                        <ClientSideEvents KeyPress="OnKeyPress" KeyDown="OnKeyDown" KeyUp="OnKeyUp"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Dar Baja" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxCheckBox ID="chkBaja" ClientInstanceName="chkBaja" runat="server" Checked="false">
                                                            <ClientSideEvents ValueChanged="GetCheckBoxValue"/>
                                                    </dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Motivo Baja" VerticalAlign="Middle" CaptionStyle-Font-Size="10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="tbMotivoBaja" runat="server" Width="100%" ClientInstanceName="tbMotivoBaja" NullText="Ingrese motivo baja"
                                                                        BorderBottom-BorderStyle="Outset" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None"
                                                                        FocusedStyle-BorderBottom-BorderColor="Red" Font-Size="10" ClientVisible="False"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
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
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </div>
        
        <asp:HiddenField ID="tbCodigo" runat="server"/>
    </form>
</body>
</html>
