Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim g As New globalFunc

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Len(g.CookVal("UUU")) = 0 Then
                Response.Redirect("login.aspx")
            Else

                lblUser.Text = g.CookVal("UUU")
                lblUser2.Text = g.CookVal("UUU")
                lblUser3.Text = g.CookVal("UUU")

            End If

        End If

    End Sub

End Class

