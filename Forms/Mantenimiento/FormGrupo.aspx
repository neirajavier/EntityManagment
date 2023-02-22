<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormGrupo.aspx.vb" Inherits="ArtemisAdmin.FormGrupo" %>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="flayoutGrupo" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem Caption="ID" ClientVisible="false">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbID" runat="server" Width="100%" ClientInstanceName="tbID"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Grupo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbGrupo" runat="server" Width="100%" ClientInstanceName="tbGrupo"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Descripcion">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="tbDescripcion" runat="server" Width="100%" ClientInstanceName="tbDescripcion" AutoResizeWithContainer="true"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                           
                                <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="15">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnOK" runat="server" Text="Guardar" Width="80px" AutoPostBack="False"
                                                           Style="float: left; margin-right: 8px" CssClass="mybutton2" OnClick="btnOK_Click">
                                            </dx:ASPxButton>
                                            <%--<dx:ASPxButton ID="btnCancel" runat="server" Text="Cancelar" Width="80px" AutoPostBack="False"
                                                           Style="float: left; margin-right: 8px" CssClass="mybutton2">
                                                <ClientSideEvents Click="function(s,e){ window.parent.GrupoWindowHide(); }" />
                                            </dx:ASPxButton>--%>
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
