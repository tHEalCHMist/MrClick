Imports System.IO
Public NotInheritable Class About
#Region "varibles"
    Dim FILE_NAME As String = My.Resources.about
    Dim TextLine As String
    Dim objReader As New System.IO.StringReader(FILE_NAME)

#End Region
#Region "Subs"
    Private Sub RichTextBox_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles RichTextBox1.MouseDown

        If e.Button = MouseButtons.Left Then
            Label1.Select()
        End If
    End Sub
    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Computer.Audio.Play(My.Resources.Loaderv4, AudioPlayMode.Background)
        CheckBox1.Checked = True
        RichTextBox1.ReadOnly = True
        Timer1.Enabled = True
        Timer2.Enabled = True
        Timer3.Enabled = True
        Timer1.Interval = 180
        Timer2.Interval = 60
        Timer3.Interval = 60
        RichTextBox1.Clear()
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Center
        omegaPassed = False
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        My.Computer.Audio.Stop()
        Form1.Show()
        Form1.Focus()
    End Sub
#End Region
#Region "Timers"
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        RichTextBox1.SelectedText = objReader.ReadLine() + Microsoft.VisualBasic.vbCrLf
        Dim p As New POINT
        SendMessage(RichTextBox1.Handle, EM_GETSCROLLPOS, 0, p)
        p.y += 1
        SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, p)
        If RichTextBox1.Text.Contains("Ω") Then
            omegaPassed = True
        End If
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        CustomTitlebar1.Text = MarqueeLeft(CustomTitlebar1.Text)
    End Sub
    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        bgChange()
        bgChangePlus()
        If omegaPassed = True Then
            'Me.BackColor = Color.FromArgb(CInt(BGred), CInt(BGblue), CInt(BGgreen))
            'CheckBox1.ForeColor = Color.FromArgb(CInt(newBGred), CInt(newBGblue), CInt(newBGgrn))
            'CheckBox1.BackColor = Color.Tan
            'Button1.ForeColor = Color.FromArgb(CInt(newBGred), CInt(newBGblue), CInt(newBGgrn))
            CustomTitlebar1.ForeColor = Color.FromArgb(CInt(newBGred), CInt(newBGblue), CInt(newBGgrn))
            'Panel2.BackColor = Color.FromArgb(CInt(newBGred), CInt(newBGblue), CInt(newBGgrn))
            'Button1.FlatAppearance.BorderColor = Color.FromArgb(CInt(newBGred), CInt(newBGblue), CInt(newBGgrn))
        End If
    End Sub
#End Region
    Private Sub CheckBox1_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckStateChanged
        If CheckBox1.Checked = True Then
            CheckBox1.Text = "Sound On"
            My.Computer.Audio.Play(My.Resources.Loaderv4, AudioPlayMode.Background)
        Else
            CheckBox1.Text = "Sound Off"
            My.Computer.Audio.Stop()
        End If
    End Sub
End Class