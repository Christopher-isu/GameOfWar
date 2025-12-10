'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' https://github.com/Christopher-isu/GameOfWar.git
' The Game of War
'====================================================================
' AboutForm.vb
' Modal dialog that displays information about the War game:
' - Title, version, author, and brief description.
'
' Shown from WarGUIForm when btnAbout is clicked.
'====================================================================
Option Strict On
Option Explicit On

Public Class AboutForm

    ' On load, configure form appearance and label text.
    Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "About War Card Game"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Example label content (assuming lblTitle, lblVersion, lblAuthor exist in designer).
        ' These can be set in the designer or here in code.
        ' lblTitle.Text = "War Card Game"
        ' lblVersion.Text = "Version 1.0"
        ' lblAuthor.Text = "Author: Your Name"
        ' lblDescription.Text = "Two-player War implemented as CS assignment."
    End Sub

    ' OK button closes the dialog and returns control to the main form.
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

End Class
