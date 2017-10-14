Imports System.Runtime.InteropServices
'Imports System.Threading

Public Class Form1

    Public Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Public Const MOUSEEVENTF_LEFTDOWN = &H2 ' left button down
    Public Const MOUSEEVENTF_LEFTUP = &H4 ' left button up
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20 ' middle button down
    Public Const MOUSEEVENTF_MIDDLEUP = &H40 ' middle button up
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8 ' right button down
    Public Const MOUSEEVENTF_RIGHTUP = &H10 ' right button up
    Public Const VK_NUMPAD0 As Byte = &H60
    Public Const VK_0 As Byte = &H30
    Public count As Integer
    Public toggle As Boolean = False
    Public toggle2 As Boolean = False
    Public mSet As Boolean = False
    Public kSet As Boolean = False
    Public MOUSE_UP As Integer
    Public MOUSE_DOWN As Integer
    Public Const KEYEVENTF_KEYUP = &H2
    Public Const KEYEVENTF_KEYDOWN = &H0
    Public iKey As Integer
    Public cKey As Char
    Public sKey As String
    Dim CurrentColor As Color
    Dim kc As KeysConverter = New KeysConverter()
    <DllImport("user32.dll")> _
    Private Shared Function GetDC(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function ReleaseDC(hWnd As IntPtr, hdc As IntPtr) As Int32
    End Function

    <DllImport("gdi32.dll")> _
    Private Shared Function GetPixel(hdc As IntPtr, nXPos As Integer, nYPos As Integer) As UInteger
    End Function

    <DllImport("user32.dll")> _
    Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CurrentColor = GetPixelColor(170, 70)
        Me.Text = MarqueeLeft(Me.Text)
        If CurrentColor.R > 186 And CurrentColor.R < 212 And CheckBox1.Checked = True Then 'CurrentColor.GetHashCode
            Timer3.Enabled = True
        Else
            Timer3.Enabled = False
        End If
        If GetAsyncKeyState(iKey) <> 0 Then
            Me.Text = "MrClicky : Active"
            Timer2.Enabled = True

        Else
            Me.Text = "MrClicky : Idle"
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Location = New System.Drawing.Point(10, 10)
        RadioButton4.Checked = True
        Timer1.Enabled = True
        If GotWoW() = False Then
            MsgBox("World of Warcraft not running! please start wow", MsgBoxStyle.Critical)
            Application.Exit()
        Else
            MsgBox("Set Your Keys")
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If iKey <> 0 Then
            count = count + 1
            If mSet = True Then
                mouse_event(MOUSE_UP, 0, 0, 0, 0)
                mouse_event(MOUSE_DOWN, 0, 0, 0, 0)
                Label1.Text = count
                ToolTip1.IsBalloon = True
                NotifyIcon1.Text = count
            End If
            If mSet = False Then
                SendKeys.Send("{" + cKey + "}")
                Label1.Text = count
                ToolTip1.IsBalloon = True
                NotifyIcon1.Text = count
            End If
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Timer2.Interval = 96 '10 clicks a second
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Timer2.Interval = 80 '15 clicks a second
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Timer2.Interval = 48 '20 clicks a second
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        Timer2.Interval = 32 '31 clicks a second
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        toggle = False
        Button4.Text = "Waiting"
    End Sub

    Private Sub Button4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button4.KeyDown
        If Asc(e.KeyValue) > 0 And toggle = False Then
            Button4.Text = e.KeyValue
            cKey = Chr(e.KeyValue)
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            toggle = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        About.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
        Me.Visible = False
        NotifyIcon1.Visible = True
        Me.Hide()
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.NotifyIcon1.Visible = False 'Hides the tray icon. if we don't do this we can kill the app, but the icon will still be there
        End 'Kills the application
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.NotifyIcon1.Visible = False
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton5.CheckedChanged
        MOUSE_DOWN = &H2 ' left button down
        MOUSE_UP = &H4 ' left button up
        mSet = True
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton6.CheckedChanged
        MOUSE_DOWN = &H8 ' right button down
        MOUSE_UP = &H10 ' right button up
        mSet = True
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton7.CheckedChanged
        MOUSE_DOWN = &H20 ' middle button down
        MOUSE_UP = &H40 ' middle button up
        mSet = True
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        toggle = False
        Button5.Text = "Waiting"
    End Sub

    Private Sub Button5_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Button5.KeyDown
        If Asc(e.KeyValue) > 0 And toggle = False Then
            Button5.Text = e.KeyValue
            iKey = e.KeyValue
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            toggle = True
            mSet = False
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        SendKeys.Send("{" + sKey + "}")
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        toggle = False
        Button6.Text = "Waiting"
    End Sub

    Private Sub Button6_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Button6.KeyDown
        If Asc(e.KeyValue) > 0 And toggle = False Then
            Button6.Text = e.KeyValue
            sKey = Chr(e.KeyValue)
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            toggle = True
        End If
    End Sub
End Class
