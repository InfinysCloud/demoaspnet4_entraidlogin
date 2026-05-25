Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.OpenIdConnect

Namespace MyWebApp
    Public Class BasePage
        Inherits System.Web.UI.Page
        Dim g As New globalFunc


        Protected Overrides Sub OnLoad(e As EventArgs)
            If Len(g.CookVal("UUU")) = 0 Then

                If Not User.Identity.IsAuthenticated Then

                    Context.GetOwinContext().
                Authentication.Challenge(
                    New AuthenticationProperties With {
                        .RedirectUri = ConfigurationManager.AppSettings("RedirectUri")
                    },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType)

                    Response.End()
                Else
                    Response.Redirect("/azure/auth.aspx")
                End If
            End If
            MyBase.OnLoad(e)

        End Sub

    End Class

End Namespace
