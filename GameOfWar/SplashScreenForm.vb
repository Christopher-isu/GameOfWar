'====================================================================
' SplashScreenForm.vb - COMPLETE with honor_diamond.jpg
' Shows for 1 second on startup with resource image
'====================================================================
Public Class SplashScreenForm

    Private Sub SplashScreenForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load honor_diamond.jpg from Resources into SplashPicBox
        SplashPicBox.Image = My.Resources.honor_diamond
        SplashPicBox.SizeMode = PictureBoxSizeMode.StretchImage

        ' Center form, no border, always on top
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.BackColor = Color.Black  ' Optional: black background

        ' Start 1-second timer to auto-close
        tmrClose.Interval = 1000
        tmrClose.Start()
    End Sub

    Private Sub tmrClose_Tick(sender As Object, e As EventArgs) Handles tmrClose.Tick
        tmrClose.Stop()
        Me.Close()
    End Sub
End Class
