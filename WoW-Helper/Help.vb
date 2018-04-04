Public Class Help

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
        Form1.Show()
        Form1.Focus()
        Form1.Button7.Text = "Help"
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class