Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls
Imports System.Web.HttpContext
Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.IO
Imports System.Net.Mail

Public Class globalFunc

    Public Function CookVal(ByVal str As String) As String
        Dim objCookie As HttpCookie = Nothing
        objCookie = New HttpCookie(str)
        If Current.Request.Cookies(str) IsNot Nothing Then
            'Return Replace(Current.Request.Cookies(str).Values.ToString, "%2c", ",")
            Return Current.Request.Cookies(str).Value
        Else
            Return ""
        End If

    End Function


    Public Shared Function GetDataTable(ByVal query As String) As DataTable
        Dim ConnString As String = System.Configuration.ConfigurationManager.AppSettings("ConnStr") 'ConfigurationManager.ConnectionStrings("ConnStrKop").ConnectionString
        Dim connection1 As New SqlConnection(ConnString)
        Dim adapter1 As New SqlDataAdapter
        adapter1.SelectCommand = New SqlCommand(query, connection1)
        Dim table1 As New DataTable
        connection1.Open()
        Try
            adapter1.Fill(table1)
        Finally
            connection1.Close()
        End Try
        Return table1
    End Function

    Public Shared Function Encrypt(ByVal clearText As String) As String
        Dim EncryptionKey As String = "rahasia"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Public Shared Function Decrypt(ByVal cipherText As String) As String
        Dim EncryptionKey As String = "rahasia"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

End Class
