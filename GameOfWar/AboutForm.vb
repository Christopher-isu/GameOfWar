Public Class AboutForm

    Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "About War Game"
        Me.StartPosition = FormStartPosition.CenterParent

        ' Example: if you placed labels in designer:
        ' lblTitle.Text = "War Card Game"
        ' lblAuthor.Text = "Author: Your Name"
        ' lblVersion.Text = "Version 1.0"
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

   
End Class