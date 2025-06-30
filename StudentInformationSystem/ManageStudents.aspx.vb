Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports Npgsql

' Class to manage CRUD operations for student records
Public Class ManageStudents
    Inherits Page

    ' Connection string retrieved from Web.config
    Private ReadOnly connStr As String = ConfigurationManager.ConnectionStrings("SupabaseConnection").ConnectionString

    ' Executes on first page load, sets default enrollment date and loads student grid
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Nur Admins dürfen weiter
            If Session("role") Is Nothing _
           OrElse Session("role").ToString().Trim().ToLower() <> "admin" Then
                lblMessage.Text = "Access denied. Admins only."
                gvStudents.Visible = False
                btnAddStudent.Visible = False
                btnUpdateStudent.Visible = False
                btnDeleteStudent.Visible = False
                btnClear.Visible = False
                Return
            End If

            ' Erst wenn Admin:
            txtEnrollmentDate.Text = Date.Today.ToString("yyyy-MM-dd")
            LoadGrid()
        End If
    End Sub

    ' Loads students into the GridView from the database
    Private Sub LoadGrid()
        Using conn As New NpgsqlConnection(connStr)
            conn.Open()
            Dim cmd As New NpgsqlCommand("SELECT id, first_name, last_name, email, enrollment_date FROM students ORDER BY id", conn)
            Dim da As New NpgsqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvStudents.DataSource = dt
            gvStudents.DataBind()
        End Using
    End Sub

    ' Inserts a new student record into the database
    Protected Sub btnAddStudent_Click(sender As Object, e As EventArgs)
        If Not ValidateInput() Then Exit Sub

        Using conn As New NpgsqlConnection(connStr)
            conn.Open()
            Dim cmd As New NpgsqlCommand("INSERT INTO students (first_name, last_name, email, enrollment_date) VALUES (@first, @last, @mail, @date) RETURNING id", conn)
            cmd.Parameters.AddWithValue("@first", txtFirstName.Text)
            cmd.Parameters.AddWithValue("@last", txtLastName.Text)
            cmd.Parameters.AddWithValue("@mail", txtEmail.Text)
            cmd.Parameters.AddWithValue("@date", Date.Parse(txtEnrollmentDate.Text))
            cmd.ExecuteScalar()
            ' After student insertion in the database, send confirmation email
            If SendEnrollmentEmail(txtEmail.Text, txtFirstName.Text, Date.Parse(txtEnrollmentDate.Text)) Then
                lblMessage.Text = "Student successfully added and confirmation email sent."
                lblMessage.CssClass = "alert alert-success"
            Else
                lblMessage.Text = "Student added, but failed to send email."
                lblMessage.CssClass = "alert alert-warning"
            End If

            LoadGrid()
            ClearForm()
        End Using

    End Sub

    ' Updates an existing student record based on selected ID
    Protected Sub btnUpdateStudent_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(hfStudentId.Value) OrElse Not ValidateInput() Then Exit Sub

        Using conn As New NpgsqlConnection(connStr)
            conn.Open()
            Dim cmd As New NpgsqlCommand("UPDATE students SET first_name=@first, last_name=@last, email=@mail, enrollment_date=@date WHERE id=@id RETURNING id", conn)
            cmd.Parameters.AddWithValue("@first", txtFirstName.Text)
            cmd.Parameters.AddWithValue("@last", txtLastName.Text)
            cmd.Parameters.AddWithValue("@mail", txtEmail.Text)
            cmd.Parameters.AddWithValue("@date", Date.Parse(txtEnrollmentDate.Text))
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfStudentId.Value))
            cmd.ExecuteScalar()
            ' Inside btnAddStudent_Click after cmd.ExecuteScalar()
            lblMessage.Text = "Student updated successfully."
            lblMessage.CssClass = "alert alert-success"
            LoadGrid()
            ClearForm()
        End Using

        Response.Redirect(Request.RawUrl)
    End Sub

    ' Deletes a student record from the database based on selected ID
    Protected Sub btnDeleteStudent_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(hfStudentId.Value) Then Exit Sub

        Using conn As New NpgsqlConnection(connStr)
            conn.Open()
            Dim cmd As New NpgsqlCommand("DELETE FROM students WHERE id=@id RETURNING id", conn)
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfStudentId.Value))
            cmd.ExecuteScalar()
            ' Inside btnAddStudent_Click after cmd.ExecuteScalar()
            lblMessage.Text = "Student deleted successfully."
            lblMessage.CssClass = "alert alert-success"
            LoadGrid()
            ClearForm()
        End Using

        Response.Redirect(Request.RawUrl)
    End Sub

    ' Clears input fields and resets UI elements
    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        ClearForm()
    End Sub

    ' Helper method to reset the input form and UI state
    Private Sub ClearForm()
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtEmail.Text = ""
        txtEnrollmentDate.Text = Date.Today.ToString("yyyy-MM-dd")
        hfStudentId.Value = ""
        btnUpdateStudent.Enabled = False
        btnDeleteStudent.Enabled = False
        lblMessage.Text = ""
        lblMessage.CssClass = ""
    End Sub

    ' Populates form fields when a GridView row is selected
    Protected Sub gvStudents_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvStudents.SelectedRow
        hfStudentId.Value = row.Cells(0).Text
        txtFirstName.Text = row.Cells(1).Text
        txtLastName.Text = row.Cells(2).Text
        txtEmail.Text = row.Cells(3).Text
        txtEnrollmentDate.Text = DateTime.Parse(row.Cells(4).Text).ToString("yyyy-MM-dd")
        btnUpdateStudent.Enabled = True
        btnDeleteStudent.Enabled = True
    End Sub

    ' Validates user input before database operations
    Private Function ValidateInput() As Boolean
        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then Return False
        If String.IsNullOrWhiteSpace(txtLastName.Text) Then Return False
        If String.IsNullOrWhiteSpace(txtEmail.Text) OrElse Not Regex.IsMatch(txtEmail.Text, "^[^@\s]+@[^@\s]+\.[^@\s]+$") Then Return False
        If String.IsNullOrWhiteSpace(txtEnrollmentDate.Text) OrElse Not Date.TryParse(txtEnrollmentDate.Text, New Date()) Then Return False
        Return True
    End Function
    ' Sends enrollment confirmation email via SMTP after successful student enrollment
    Private Function SendEnrollmentEmail(recipientEmail As String, firstName As String, enrollmentDate As Date) As Boolean
        Try
            ' Initialize MailMessage object with recipient and message details
            Dim mail As New MailMessage()
            mail.To.Add(recipientEmail)
            mail.Subject = "Enrollment Confirmation"
            mail.Body = $"Dear {firstName}," & vbCrLf &
                        $"You have been successfully enrolled on {enrollmentDate:yyyy-MM-dd}." & vbCrLf &
                        "Welcome to our system!"

            ' SMTP client is automatically configured from Web.config settings
            Dim smtpClient As New SmtpClient()
            smtpClient.Send(mail)

            ' Return true if email sending succeeds
            Return True
        Catch ex As Exception
            ' Log exception (recommended in production environments)
            Return False ' Return false if email sending fails
        End Try
    End Function
End Class
