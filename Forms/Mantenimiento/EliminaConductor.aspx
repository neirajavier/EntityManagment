<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EliminaConductor.aspx.vb" Inherits="ArtemisAdmin.EliminaConductor" %>
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
            <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout3" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem Caption=" ">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxLabel ID="lbCedula" runat="server" ClientInstanceName="lbCedula" ClientVisible="false"/>
                                            <dx:ASPxLabel ID="lbMessage" runat="server" ClientInstanceName="lbMessage" Font-Size="Medium"/>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="20">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnYes" runat="server" Text="SI" Width="80px" AutoPostBack="False" OnClick="btnYes_Click"
                                                           Style="float: left; margin-right: 8px" CssClass="mybutton2">
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btnNo" runat="server" Text="NO" Width="80px" AutoPostBack="False"
                                                           Style="float: left; margin-right: 8px" CssClass="mybutton2">
                                                <ClientSideEvents Click="function(s,e){ window.parent.EliminaHide(); }"/>
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
    </form>
</body>
</html>
