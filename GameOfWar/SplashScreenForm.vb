'====================================================================
' Christopher Z
' Fall 2025
' RCET 3375
' https://github.com/Christopher-isu/GameOfWar.git
' The Game of War
'====================================================================
' SplashScreenForm.vb
' Simple splash screen shown when the application starts.
' Displays a picture (e.g., honor_diamond.jpg) in a PictureBox for 1 second.
'
' This form is shown once from WarGUIForm_Load, then closes automatically.
'====================================================================
Option Strict On
Option Explicit On

Public Class SplashScreenForm

    ' On load, configure appearance and start a timer to close automatically.
    Private Sub SplashScreenForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Assign the resource image to the PictureBox for visual effect.
        SplashPicBox.Image = My.Resources.honor_diamond
        SplashPicBox.SizeMode = PictureBoxSizeMode.Zoom

        ' Cosmetic form settings to look like a splash.
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True

        ' Timer (tmrClose) should be dropped on the form in the designer.
        ' It is responsible for closing the splash after 1 second.
        tmrClose.Interval = 1000
        tmrClose.Start()
    End Sub

    ' After the timer interval, close the splash so the main form can proceed.
    Private Sub tmrClose_Tick(sender As Object, e As EventArgs) Handles tmrClose.Tick
        tmrClose.Stop()
        Me.Close()
    End Sub

End Class
