
Imports System
Imports System.Data.Entity.Infrastructure.Interception
Imports System.Security.Claims
Imports System.Web
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Microsoft.VisualBasic.ApplicationServices
Imports Owin


Partial Class azure_auth
    Inherits System.Web.UI.Page
    Dim g As New globalFunc
    Dim auth As New auth_azure

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        auth.Get_Configuration()
        Get_Authentication()
    End Sub

    Public Sub Get_Authentication()
        Dim objCookie As HttpCookie = Nothing

        objCookie = New HttpCookie("UUU")

        If User.Identity.IsAuthenticated Then

            Dim identity As ClaimsIdentity =
                CType(User.Identity, ClaimsIdentity)

            Dim email As String = ""
            Dim fullname As String = ""

            ' Email
            Dim emailClaim As Claim =
                identity.FindFirst("preferred_username")

            If emailClaim IsNot Nothing Then
                email = emailClaim.Value
            End If

            ' Full Name
            Dim nameClaim As Claim =
                identity.FindFirst("name")

            If nameClaim IsNot Nothing Then
                fullname = nameClaim.Value
            End If

            Response.Write("Email: " & email & "<br/>")
            Response.Write("Full Name: " & fullname)


            objCookie.Value = fullname.Trim()
            Response.Cookies.Add(objCookie)
            Response.Redirect("/")

        End If
    End Sub

    Protected Sub btn_click_Click(sender As Object, e As EventArgs) Handles btn_click.Click
        Get_Authentication()
    End Sub

End Class


