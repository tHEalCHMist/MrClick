Public Class Popup
    Private WithEvents tm As New Timer
    Private Sub Popup_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Label1.Text = Form1.dlG
        tm.Interval = 5000
        tm.Start()
    End Sub

    Private Sub tm_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tm.Tick
        Close()
    End Sub
End Class