Imports System.Configuration
Imports System.Net
Imports System.Runtime.Remoting.Contexts
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Owin

<Assembly: OwinStartup(GetType(MyWebApp.Startup))>

Namespace MyWebApp

    Public Class Startup
        Public ClientId As String
        Public TenantId As String
        Public ClientSecret As String
        Public RedirectUri As String

        Public Sub Get_Configuration()
            ClientId = ConfigurationManager.AppSettings("ClientId")
            TenantId = ConfigurationManager.AppSettings("TenantId")
            ClientSecret = ConfigurationManager.AppSettings("ClientSecret")
            RedirectUri = ConfigurationManager.AppSettings("RedirectUri")
        End Sub

        Public Sub Configuration(app As IAppBuilder)

            Try
                ' WAJIB untuk Entra ID / HTTPS modern
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                ' Set default sign in type
                app.SetDefaultSignInAsAuthenticationType(
            CookieAuthenticationDefaults.AuthenticationType)


                ' Cookie Authentication
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
                        .ResponseType = "code id_token",
                        .SignInAsAuthenticationType = "Cookies",
                        .TokenValidationParameters = New Microsoft.IdentityModel.Tokens.TokenValidationParameters With {
                             .ValidateIssuer = False}
                        }
                 )
            Catch


            End Try

        End Sub
    End Class
End Namespace