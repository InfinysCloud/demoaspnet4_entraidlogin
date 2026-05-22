Imports System.Data
Imports System.Web
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail
Imports System.Runtime.Remoting.Contexts
Imports System.Security.Cryptography
Imports System.Web.HttpContext
Imports System.Web.UI.HtmlControls
Imports System.Configuration.ConfigurationManager
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Microsoft.VisualBasic
Imports Owin



Public Class auth_azure

    Public ClientId As String
    Public TenantId As String
    Public ClientSecret As String
    Public RedirectUri As String

    Public Sub CheckLogin()

        If Not HttpContext.Current.Request.IsAuthenticated Then
            HttpContext.Current.Response.Redirect("~/default.aspx")

        End If

    End Sub

    Public Sub Get_Configuration()
        ClientId = ConfigurationManager.AppSettings("ClientId")
        TenantId = ConfigurationManager.AppSettings("TenantId")
        ClientSecret = ConfigurationManager.AppSettings("ClientSecret")
        RedirectUri = ConfigurationManager.AppSettings("RedirectUri")

    End Sub

    Public Sub Configuration(app As IAppBuilder)
        ' WAJIB: default sign-in type
        app.SetDefaultSignInAsAuthenticationType(
            CookieAuthenticationDefaults.AuthenticationType)

        ' Cookie auth
        app.UseCookieAuthentication(
            New CookieAuthenticationOptions With {
                .AuthenticationType =
                    CookieAuthenticationDefaults.AuthenticationType
            })

        Get_Configuration()

        app.UseOpenIdConnectAuthentication(
                New OpenIdConnectAuthenticationOptions With {
                .ClientId = ClientId,
                .ClientSecret = ClientSecret,
                .Authority = "https://login.microsoftonline.com/" & TenantId,
                .RedirectUri = RedirectUri,
                .ResponseType = "id_token",
                .SignInAsAuthenticationType = "Cookies",
                .TokenValidationParameters = New Microsoft.IdentityModel.Tokens.TokenValidationParameters With {
                     .ValidateIssuer = False}
                }
         )

    End Sub

End Class