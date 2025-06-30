Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Partial Public Class Login
    Inherits Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        RegisterHyperLink.NavigateUrl = "Register"
        ' Diese Option aktivieren, nachdem Sie die Kontobestätigung für die Funktion zum Zurücksetzen des Kennworts aktiviert haben
        ' ForgotPasswordHyperLink.NavigateUrl = "Forgot"
        OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If Not [String].IsNullOrEmpty(returnUrl) Then
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then
            ' Benutzerkennwort überprüfen
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

            ' Anmeldefehler werden bezüglich einer Kontosperre nicht gezählt.
            ' Wenn Sie aktivieren möchten, dass Kennwortfehler eine Sperre auslösen, ändern Sie in "shouldLockout:= True".
            Dim result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout:=False)

            Select Case result
                Case SignInStatus.Success
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                    Exit Select
                Case SignInStatus.LockedOut
                    Response.Redirect("/Account/Lockout")
                    Exit Select
                Case SignInStatus.RequiresVerification
                    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                    Request.QueryString("ReturnUrl"),
                                                    RememberMe.Checked),
                                      True)
                    Exit Select
                Case Else
                    FailureText.Text = "Ungültiger Anmeldeversuch."
                    ErrorMessage.Visible = True
                    Exit Select
            End Select
        End If
    End Sub

    ' Password verification using PBKDF2 hash (Rfc2898DeriveBytes)
    Private Function VerifyPassword(inputPassword As String, storedHash As String) As Boolean
        ' Split the stored hash into salt and actual hash
        Dim parts = storedHash.Split(":"c)
        If parts.Length <> 2 Then Return False

        Dim salt = Convert.FromBase64String(parts(0))
        Dim stored = Convert.FromBase64String(parts(1))

        ' Derive hash from input password using the same salt
        Using pbkdf2 As New Rfc2898DeriveBytes(inputPassword, salt, 10000)
            Dim inputHash = pbkdf2.GetBytes(32) ' 32 bytes = 256-bit hash
            ' Compare byte sequences
            Return stored.SequenceEqual(inputHash)
        End Using
    End Function
End Class
