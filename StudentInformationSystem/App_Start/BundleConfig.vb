﻿Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    ' Weitere Informationen zur Bündelung finden Sie unter https://go.microsoft.com/fwlink/?LinkID=303951
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        RegisterJQueryScriptManager()

        bundles.Add(New ScriptBundle("~/bundles/WebFormsJs").Include(
                        "~/Scripts/WebForms/WebForms.js",
                        "~/Scripts/WebForms/WebUIValidation.js",
                        "~/Scripts/WebForms/MenuStandards.js",
                        "~/Scripts/WebForms/Focus.js",
                        "~/Scripts/WebForms/GridView.js",
                        "~/Scripts/WebForms/DetailsView.js",
                        "~/Scripts/WebForms/TreeView.js",
                        "~/Scripts/WebForms/WebParts.js"))

        ' Die Reihenfolge ist sehr wichtig, damit diese Dateien funktionieren. Sie weisen ausdrückliche Abhängigkeiten auf.
        bundles.Add(New ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"))

        ' Verwenden Sie die Entwicklungsversion von Modernizr als Entwicklungs- und Lerngrundlage. Wenn Sie dann
        ' bereit für die Produktion, verwenden Sie das Buildtool unter https://modernizr.com, um nur die benötigten Tests auszuwählen.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"))
    End Sub

    Public Shared Sub RegisterJQueryScriptManager()
        Dim jQueryScriptResourceDefinition As New ScriptResourceDefinition
        With jQueryScriptResourceDefinition
            .Path = "~/scripts/jquery-3.7.0.min.js"
            .DebugPath = "~/scripts/jquery-3.7.0.js"
            .CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.7.0.min.js"
            .CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.7.0.js"
        End With

        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQueryScriptResourceDefinition)
    End Sub
End Class
