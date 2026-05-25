
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
        GetLoginWithAuzure
    End Sub

    Public Sub GetLoginWithAuzure()
        'push to login with azure entra id
        Context.GetOwinContext().
               Authentication.Challenge(
                   New AuthenticationProperties With {
                       .RedirectUri = ConfigurationManager.AppSettings("RedirectUri")
                   },
                   OpenIdConnectAuthenticationDefaults.AuthenticationType)
        Response.End()
    End Sub
End Class


