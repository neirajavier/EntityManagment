<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AsignarGrupo2.aspx.vb" Inherits="ArtemisAdmin.AsignarGrupo2" %>
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

        .mylist .dxlbd{ 
            border: none !important;
            height: auto !important;
        }

    </style>

    <script type="text/javascript">
        function OnBtnClientClick(s, e) {
            window.parent.HidePopup();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPanel ID="ASPxPanel4" runat="server" Width="100" Height="100">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout4" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxListBox ID="ListBoxGrupoDet2" runat="server" SelectionMode="CheckColumn" ClientInstanceName="ListBoxGrupoDet2" AutoPostBack="false"
                                                            EnableClientSideAPI="True" EnableSelectAll="true" EnableCallbackMode="true" Width="300" Height="300"
                                                            OnSelectedIndexChanged="ListBoxGrupoDet2_SelectedIndexChanged">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e) { document.getElementById('hfIndex').value = e.index; 
                                                                                                        e.processOnServer = true; }" />
                                            </dx:ASPxListBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>   
                                <dx:LayoutItem Caption="" ShowCaption="False" Paddings-PaddingTop="20">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnAsignarG" runat="server" Text="Asignar" Width="80px" ClientInstanceName="btnAsignarG"
                                                           AutoPostBack="False" Style="float: left" CssClass="mybutton2" OnClick="btnAsignarG_Click">
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

        <asp:HiddenField ID="hfIndex" runat="server"/>
    </form>
</body>
</html>
