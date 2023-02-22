<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AsignarGrupoPto.aspx.vb" Inherits="ArtemisAdmin.AsignarGrupoPto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPanel ID="ASPxPanel2" runat="server">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout2" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem Caption="" Paddings-PaddingBottom="10">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxLabel ID="lbalias" runat="server" ClientInstanceName="lbalias" Font-Bold="true"/>
                                            <dx:ASPxLabel ID="lbactivo" runat="server" ClientInstanceName="lbactivo" ClientVisible="false"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxListBox ID="ListBoxGrupoDet" runat="server" SelectionMode="CheckColumn" ClientInstanceName="ListBoxGrupoDet"
                                                            EnableClientSideAPI="True" EnableSelectAll="true" EnableCallbackMode="true" Width="300" Height="300"
                                                            OnSelectedIndexChanged="ListBoxGrupoDet_SelectedIndexChanged">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e) { document.getElementById('hfIndex').value = e.index; 
                                                                                                        e.processOnServer = true; }" />
                                            </dx:ASPxListBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </div>
        <asp:HiddenField ID="hfIndex" runat="server"/>
    </form>
</body>
</html>
