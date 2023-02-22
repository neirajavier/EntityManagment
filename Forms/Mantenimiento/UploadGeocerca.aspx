<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UploadGeocerca.aspx.vb" Inherits="ArtemisAdmin.UploadGeocerca" %>
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

        #dropZone {
            padding: 5px;
            margin: -5px;
        }
        .ResultFileName {
            text-overflow: ellipsis;
        }
    </style>

    <script type="text/javascript">
        var uploadInProgress = false,
            submitInitiated = false,
            uploadErrorOccurred = false;
            uploadedFiles = [];

        function onFileUploadComplete(s, e) {
            var callbackData = e.callbackData.split("|"),
                uploadedFileName = callbackData[0],
                isSubmissionExpired = callbackData[1] === "True";
            uploadedFiles.push(uploadedFileName);
            if (e.errorText.length > 0 || !e.isValid)
                uploadErrorOccurred = true;
            if (isSubmissionExpired && UploadedFilesTokenBox.GetText().length > 0) {
                var removedAfterTimeoutFiles = UploadedFilesTokenBox.GetTokenCollection().join("\n");
                alert("Los siguientes archivos se han eliminado del servidor debido al tiempo de espera definido de 5 minutos: \n\n" + removedAfterTimeoutFiles);
                UploadedFilesTokenBox.ClearTokenCollection();
            }
        }
        function onFileUploadStart(s, e) {
            uploadInProgress = true;
            uploadErrorOccurred = false;
            UploadedFilesTokenBox.SetIsValid(true);
        }
        function onFilesUploadComplete(s, e) {
            uploadInProgress = false;
            for (var i = 0; i < uploadedFiles.length; i++)
                UploadedFilesTokenBox.AddToken(uploadedFiles[i]);
            updateTokenBoxVisibility();
            uploadedFiles = [];
            if (submitInitiated) {
                SubmitButton.SetEnabled(true);
                SubmitButton.DoClick();
            }
        }
        function onSubmitButtonInit(s, e) {
            s.SetEnabled(true);
        }
        function onSubmitButtonClick(s, e) {
            ASPxClientEdit.ValidateGroup();
            if (!formIsValid())
                e.processOnServer = false;
            else if (uploadInProgress) {
                s.SetEnabled(false);
                submitInitiated = true;
                e.processOnServer = false;
            }
        }
        function onTokenBoxValidation(s, e) {
            var isValid = DocumentsUploadControl.GetText().length > 0 || UploadedFilesTokenBox.GetText().length > 0;
            e.isValid = isValid;
            if (!isValid) {
                e.errorText = "No se han subido archivos. Sube al menos un archivo.";
            }
        }
        function onTokenBoxValueChanged(s, e) {
            updateTokenBoxVisibility();
        }
        function updateTokenBoxVisibility() {
            var isTokenBoxVisible = UploadedFilesTokenBox.GetTokenCollection().length > 0;
            UploadedFilesTokenBox.SetVisible(isTokenBoxVisible);
        }
        function formIsValid() {
            return !ValidationSummary.IsVisible() && DescriptionTextBox.GetIsValid() && UploadedFilesTokenBox.GetIsValid() && !uploadErrorOccurred;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
            <dx:ASPxFormLayout ID="FormLayout" runat="server" Width="100%" ColCount="2" UseDefaultPaddings="false">
                <Items>
                    <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" Width="50%" UseDefaultPaddings="false" >
                        <Items>
                            <dx:LayoutGroup Caption="Archivo KML">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <div id="dropZone">
                                                    <dx:ASPxUploadControl runat="server" ID="DocumentsUploadControl" ClientInstanceName="DocumentsUploadControl" Width="100%"
                                                        AutoStartUpload="true" ShowProgressPanel="True" ShowTextBox="false" BrowseButton-Text="Seleccionar documento" FileUploadMode="OnPageLoad"
                                                        OnFileUploadComplete="DocumentsUploadControl_FileUploadComplete">
                                                        <AdvancedModeSettings EnableMultiSelect="true" EnableDragAndDrop="true" ExternalDropZoneID="dropZone" />

                                                        <ValidationSettings AllowedFileExtensions=".kml" MaxFileSize="4194304"> </ValidationSettings>
                                                        <ClientSideEvents
                                                            FileUploadComplete="onFileUploadComplete"
                                                            FilesUploadComplete="onFilesUploadComplete"
                                                            FilesUploadStart="onFileUploadStart" />
                                                    </dx:ASPxUploadControl>
                                                    <br />
                                                    <dx:ASPxTokenBox runat="server" Width="100%" ID="UploadedFilesTokenBox" ClientInstanceName="UploadedFilesTokenBox"
                                                        NullText="Selecciona los documentos" AllowCustomTokens="false" ClientVisible="false">
                                                        <ClientSideEvents Init="updateTokenBoxVisibility" ValueChanged="onTokenBoxValueChanged" Validation="onTokenBoxValidation" />
                                                        <ValidationSettings EnableCustomValidation="true" />
                                                    </dx:ASPxTokenBox>
                                                    <br />
                                                    <p class="Note">
                                                        <dx:ASPxLabel ID="MaxFileSizeLabel" runat="server" Text="Tamaño máximo de archivo: 10 MB." Font-Size="8pt" />
                                                    </p>
                                                    <dx:ASPxValidationSummary runat="server" ID="ValidationSummary" ClientInstanceName="ValidationSummary"
                                                        RenderMode="Table" Width="150px" ShowErrorAsLink="false" />
                                                </div>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton runat="server" ID="SubmitButton" ClientInstanceName="SubmitButton" Text="Importar" AutoPostBack="False" Width="20%"
                                            OnClick="SubmitButton_Click" ValidateInvisibleEditors="true" CssClass="mybutton2" Style="float: right; margin-right: 5px">
                                            <%--<ClientSideEvents Init="onSubmitButtonInit" Click="onSubmitButtonClick" />--%>
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Height="5" />
                        </Items>
                    </dx:LayoutGroup>
                </Items>
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600"/>
            </dx:ASPxFormLayout>
        </div>
    </form>
</body>
</html>
