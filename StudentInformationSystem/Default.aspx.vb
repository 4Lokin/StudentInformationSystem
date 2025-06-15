Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Optional: logic that runs on every page load
    End Sub
    ''' <summary>
    ''' Handles the greeting button click.
    ''' Reads the user's input from <c>t_entername</c>,
    ''' sanitizes it, and displays either a welcome or warning
    ''' message inside <c>msg_box</c>.
    ''' </summary>
    ''' <param name="sender">The button that was clicked.</param>
    ''' <param name="e">Event data for the click event.</param>
    Protected Sub mybutton_Click(sender As Object, e As EventArgs) Handles mybutton.Click
        Dim name As String = t_entername.Text.Trim()

        If Not String.IsNullOrEmpty(name) Then
            lbl_message.Text = $"Welcome, {Server.HtmlEncode(name)}!"
            msg_box.CssClass = "alert alert-success mt-4 fade show"
        Else
            lbl_message.Text = "Please enter a name."
            msg_box.CssClass = "alert alert-warning mt-4 fade show"
        End If

        msg_box.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "fadeIn", "fadeInMsgBox();", True)
    End Sub
    ''' <summary>
    ''' Handles the calculator button click.
    ''' Validates that <c>txtNumber1</c> and <c>txtNumber2</c> are valid decimals,
    ''' then performs the arithmetic operation selected in <c>ddlOperator</c>.
    ''' Displays the result in <c>lblResult</c> or an error message in <c>ResultPanel</c>.
    ''' </summary>
    ''' <param name="sender">The button that was clicked.</param>
    ''' <param name="e">Event data for the click event.</param>
    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim num1 As Decimal
        Dim num2 As Decimal
        Dim result As Decimal
        Dim errorMessage As String = Nothing

        If Not Decimal.TryParse(txtNumber1.Text.Trim(), num1) Then
            errorMessage = "Please enter a valid number for Number 1."
        ElseIf Not Decimal.TryParse(txtNumber2.Text.Trim(), num2) Then
            errorMessage = "Please enter a valid number for Number 2."
        End If

        If errorMessage IsNot Nothing Then
            lblResult.Text = errorMessage
            ResultPanel.CssClass = "alert alert-danger mt-4 fade show"
            ResultPanel.Visible = True
            Exit Sub
        End If

        Select Case ddlOperator.SelectedValue
            Case "Add"
                result = num1 + num2
            Case "Subtract"
                result = num1 - num2
            Case "Multiply"
                result = num1 * num2
            Case "Divide"
                If num2 = 0 Then
                    lblResult.Text = "Division by zero is not allowed."
                    ResultPanel.CssClass = "alert alert-danger mt-4 fade show"
                    ResultPanel.Visible = True
                    Exit Sub
                End If
                result = num1 / num2
        End Select

        lblResult.Text = $"Result: {result}"
        ResultPanel.CssClass = "alert alert-success mt-4 fade show"
        ResultPanel.Visible = True
    End Sub
End Class