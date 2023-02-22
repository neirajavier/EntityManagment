<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminMain.aspx.vb" Inherits="ArtemisAdmin.AdminMain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" />
    <link href="/Libs/leaflet/css/font-awesome.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://unpkg.com/leaflet@1.6.0/dist/leaflet.css" />

    <link href="/Libs/leaflet/css/distance.css" rel="stylesheet" />
    <link href="/Libs/leaflet/css/L.Control.Geonames.css" rel="stylesheet" />
    <link href="/Libs/leaflet/js/leaflet-measure.css" rel="stylesheet" />
    <link href="/Libs/leaflet/css/leaflet.contextmenu.min.css" rel="stylesheet" />
    <link href="/Libs/leaflet/css/MarkerCluster.css" rel="stylesheet" />
    <link href="/Libs/leaflet/css/MarkerCluster.Default.css" rel="stylesheet" />
    
    <link href='https://api.mapbox.com/mapbox.js/plugins/leaflet-fullscreen/v1.0.1/leaflet.fullscreen.css' rel='stylesheet' />

    <link href="https://cdn.datatables.net/1.10.21/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/buttons/1.6.3/css/buttons.bootstrap4.min.css" rel="stylesheet" />

    <link href="/Themes/smoothness/jquery-ui.css" rel="stylesheet" />
    <link href="/Themes/smoothness/theme.css" rel="stylesheet" />--%>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>AdminMain</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<dx:ASPxTextBox ID="txuid" runat="server"/>--%>
            <span class="btn-dark btn-sm small" style="font-size:11.8px" id="spusuario" title="Ultima Aztualizacion: " runat="server"></span>
            <asp:HiddenField runat="server" ID="hidusuario" />
            <asp:HiddenField runat="server" ID="hidsubusuario" />
            <asp:HiddenField runat="server" ID="hdregs" />
        </div>
    </form>
</body>
</html>
