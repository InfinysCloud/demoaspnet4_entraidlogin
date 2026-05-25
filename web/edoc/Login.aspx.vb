Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Web.UI
Imports System.Web.Services
Imports Owin

Partial Class Login
    Inherits System.Web.UI.Page
    Dim dtUser As DataTable
    Dim g As New globalFunc

    Protected Sub btnSubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.ServerClick
        If UserLogin(tbUser.Value, tbPass.Value) = 1 Then
            Dim objCookie As HttpCookie = Nothing
            objCookie = New HttpCookie("UUU")
            objCookie.Value = dtUser.Rows(0)("NamaLogin") 'User Name
            Response.Cookies.Add(objCookie)

            Response.Redirect("default.aspx") ' & strReq)

        Else
            lblError.Text = "Wrong username or password"
            lblError.Visible = True
            tbPass.Focus()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim signout As String = Request.QueryString("sign")
        Dim objCookienama As HttpCookie = Nothing

        If User.Identity.IsAuthenticated Then
            If (g.CookVal("UUU").Length > 0) Then
                objCookienama = New HttpCookie("UUU")
                objCookienama.Value = ""
                Response.Cookies.Add(objCookienama)
                Response.Redirect("/")
            End If
            Context.GetOwinContext().Authentication.SignOut()
        End If

        If Not Page.IsPostBack Then
            objCookienama = New HttpCookie("UUU")
            objCookienama.Value = ""
            Response.Cookies.Add(objCookienama)
        End If
    End Sub
    Public Function UserLogin(ByVal strUsername As String, ByVal strPassword As String) As Integer
        Dim iReturnValue As Integer = 0

        Dim strSQL As String

        strSQL = "SELECT ID, NamaLogin, NamaLengkap FROM tbl_user  "
        strSQL = strSQL + "WHERE     (NamaLogin= N'" & Replace(strUsername, "'", "") & "') AND (Password = N'" & strPassword & "') "

        dtUser = globalFunc.GetDataTable(strSQL)
        iReturnValue = dtUser.Rows.Count
        Return iReturnValue
    End Function


End Class
