<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditarPunto.aspx.vb" Inherits="ArtemisAdmin.EditarPunto" %>
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
        .mybutton3 { border-radius: 15px 15px 15px 15px; }

    </style>

    <script type="text/javascript">
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
            <dx:ASPxPanel ID="ASPxPanel2" runat="server">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem Caption="Latitud" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbLatitud" runat="server" Width="100%" ClientInstanceName="tbLatitud"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Longitud" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbLongitud" runat="server" Width="100%" ClientInstanceName="tbLongitud"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Categoría" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox runat="server" ID="cbCategoria" DropDownStyle="DropDownList" ClientInstanceName="cbCategoria" Width="200px"
                                                             IncrementalFilteringMode="None"  Font-Size="10">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Subcategoría" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox runat="server" ID="cbSubcategoria" DropDownStyle="DropDownList" ClientInstanceName="cbSubcategoria" Width="200px"
                                                             IncrementalFilteringMode="None"  Font-Size="10">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Color" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox runat="server" ID="cbColor" DropDownStyle="DropDownList" IncrementalFilteringMode="None"  
                                                             ClientInstanceName="cbColor" Width="100px" Font-Size="10">
                                                <SettingsAdaptivity Mode="Off" />
                                                <Items>
                                                    <dx:ListEditItem Text="AMARILLO" Value="#FFDA44" />
                                                    <dx:ListEditItem Text="AZUL" Value="#00457E" Selected="true"/>
                                                    <dx:ListEditItem Text="MORADO" Value="#933EC5" />
                                                    <dx:ListEditItem Text="NARANJA" Value="#FF9900" />
                                                    <dx:ListEditItem Text="NEGRO" Value="#000000" />
                                                    <dx:ListEditItem Text="ROJO" Value="#ff0000" />
                                                    <dx:ListEditItem Text="VERDE" Value="#77FF00" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Celular (CheckPoint)" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbCelular" runat="server" Width="100%" ClientInstanceName="tbCelular"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Email (CheckPoint)" VerticalAlign="Middle" CaptionStyle-Font-Size="10" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbEmail" runat="server" Width="100%" ClientInstanceName="tbEmail"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="15">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnOK" runat="server" Text="Guardar" Width="80px" AutoPostBack="False"
                                                           Style="float: left; margin-right: 8px" CssClass="mybutton2" OnClick="btnOK_Click">
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="250" />
                        </dx:ASPxFormLayout>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </div>
    </form>
</body>
</html>
