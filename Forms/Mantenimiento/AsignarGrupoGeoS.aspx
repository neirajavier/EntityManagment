<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AsignarGrupoGeoS.aspx.vb" Inherits="ArtemisAdmin.AsignarGrupoGeoS" %>
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
            <dx:ASPxPanel ID="ASPxPanel1" runat="server">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem Caption="" Paddings-PaddingBottom="10">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxLabel ID="lbsubuser" runat="server" ClientInstanceName="lbsubuser" Font-Bold="true"/>
                                            <dx:ASPxLabel ID="lbidsubuser" runat="server" ClientInstanceName="lbidsubuser" ClientVisible="false"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxListBox ID="ListBoxGrupoSub" runat="server" SelectionMode="CheckColumn" ClientInstanceName="ListBoxGrupoSub"
                                                            EnableClientSideAPI="True" EnableSelectAll="false" EnableCallbackMode="false" Width="300" Height="300" 
                                                            OnSelectedIndexChanged="ListBoxGrupoSub_SelectedIndexChanged">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e) { document.getElementById('hfIndex2').value = e.index; 
                                                                                                        e.processOnServer=true; }" />
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
        <asp:HiddenField ID="hfIndex2" runat="server"/>
    </form>
</body>
</html>
