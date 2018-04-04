Imports System.Runtime.InteropServices
'Imports System.Threading

Public Class Form1

#Region "vars"

    'Public Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    'Public Const MOUSEEVENTF_LEFTDOWN = &H2 ' left button down
    'Public Const MOUSEEVENTF_LEFTUP = &H4 ' left button up
    'Public Const MOUSEEVENTF_MIDDLEDOWN = &H20 ' middle button down
    'Public Const MOUSEEVENTF_MIDDLEUP = &H40 ' middle button up
    'Public Const MOUSEEVENTF_RIGHTDOWN = &H8 ' right button down
    'Public Const MOUSEEVENTF_RIGHTUP = &H10 ' right button up
    'Public Const VK_NUMPAD0 As Byte = &H60
    'Public Const VK_0 As Byte = &H30
    Public count As Integer
    Public toggle As Boolean = False
    Public toggle2 As Boolean = False
    Public toggle3 As Boolean = False
    Public ComboTog As Boolean = False
    '    Public mSet As Boolean = False
    Public kSet As Boolean = False
    Public MOUSE_UP As Integer
    Public MOUSE_DOWN As Integer
    Public Const KEYEVENTF_KEYUP = &H2
    Public Const KEYEVENTF_KEYDOWN = &H0
    Public iKey As Integer
    Public cKey As Char
    Public sKey As String
    Public px As Integer
    Public py As Integer
    Public dlG As String
    Dim CurrentColor As Color = GetPixelColor(px, py)
    Dim DruidColor As Color
    Dim comboColour As Color
    Dim comboPoint As POINT
    Dim myBmp As New Bitmap(1, 1)
    Dim kc As KeysConverter = New KeysConverter()

#End Region

#Region "DllImports"

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

#End Region

#Region "main"

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim f As Popup = New Popup()
        Me.Location = New System.Drawing.Point(10, 120)
        RadioButton2.Checked = True
        druid.Checked = True
        If GotWoW() = True Then
            If My.Settings.ComboKey = Nothing Then
                dlG = "Set Your Keys" & vbCrLf & "Select combo if needed"
            Else
                dlG = "Using saved Keys and Combo position if set"
            End If
        Else

            If My.Settings.ComboKey = Nothing Then
                dlG = "World of Warcraft not running!" & vbCrLf & "Start the game after setting your keys"
            Else
                dlG = "World of Warcraft not running!" & vbCrLf & "Start the game when ready." & vbCrLf & "Using saved Keys"
            End If
        End If
        ToolTip1.IsBalloon = True
        If My.Settings.ComboKey <> Nothing Then
            sKey = My.Settings.ComboKey
            cKey = My.Settings.MainKey
            iKey = My.Settings.TriggerKey
            Button6.Text = My.Settings.ComboKey
            Button4.Text = My.Settings.MainKey
            Button5.Text = My.Settings.TriggerKey
        End If
        If My.Settings.ComboPx > 0 Then
            px = My.Settings.ComboPx
            py = My.Settings.ComboPy
            Label2.Text = (px.ToString)
            Label7.Text = (py.ToString)
            dlG = dlG & " and last combo setttings"
        End If
        f.ShowDialog()
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If (e.KeyCode And Not Keys.Modifiers) = Keys.S AndAlso e.Modifiers = Keys.Control Then
            If CheckBox1.Checked = True Then
                CheckBox1.Checked = False
                MsgBox("Combo Disabled")
            Else
                CheckBox1.Checked = True
                MsgBox("Combo Enabled")
            End If
        End If
    End Sub

    Private Sub Form1_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Normal Then
            Me.NotifyIcon1.Visible = False
            Me.Text = "WoW Helper : Disabled"
            toggle3 = False
            Timer1.Enabled = False
            Timer3.Enabled = False
            CheckBox1.Checked = False
            counter = False
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If ComboTog = True Then
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            Dim g As Graphics = Graphics.FromImage(myBmp)
            g.CopyFromScreen(Cursor.Position, POINT.Empty, myBmp.Size)
            g.Dispose()
            'Clipboard.SetText(MousePosition.X.ToString & "," & MousePosition.Y.ToString & " " & Label3.BackColor.ToString) 'debugging
            Label2.Text = myBmp.GetPixel(0, 0).ToString
            Label3.BackColor = myBmp.GetPixel(0, 0)
        End If
        If e.KeyValue = 67 And ComboTog = True Then 'c key
            px = MousePosition.X
            py = MousePosition.Y
            My.Settings.ComboPx = px
            My.Settings.ComboPy = py
            My.Settings.Save()
            Label2.Text = (MousePosition.X.ToString)
            Label7.Text = (MousePosition.Y.ToString)
            Label4.Text = (Label3.BackColor.ToString)
            CurrentColor = myBmp.GetPixel(0, 0)
            'MsgBox(MousePosition.X.ToString & "," & MousePosition.Y.ToString & " " & CurrentColor.ToString)
            ComboTog = False
            Timer1.Enabled = False
        End If
    End Sub

#End Region

#Region "radiobuttons"

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

#End Region

#Region "traymenu"

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.NotifyIcon1.Visible = False 'Hides the tray icon. if we don't do this we can kill the app, but the icon will still be there
        End 'Kills the application
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.NotifyIcon1.Visible = False
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
        Me.Text = "WoW Helper : Disabled"
        toggle3 = False
        Timer1.Enabled = False
        Timer3.Enabled = False
        CheckBox1.Checked = False
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
        CheckBox1.Checked = False
        Timer1.Enabled = False
        Timer3.Enabled = False

    End Sub

#End Region

#Region "old mouse stuff"
    'Private Sub RadioButton5_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton5.CheckedChanged
    '    MOUSE_DOWN = &H2 ' left button down
    '    MOUSE_UP = &H4 ' left button up
    '    mSet = True
    'End Sub

    'Private Sub RadioButton6_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton6.CheckedChanged
    '    MOUSE_DOWN = &H8 ' right button down
    '    MOUSE_UP = &H10 ' right button up
    '    mSet = True
    'End Sub

    'Private Sub RadioButton7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton7.CheckedChanged
    '    MOUSE_DOWN = &H20 ' middle button down
    '    MOUSE_UP = &H40 ' middle button up
    '    mSet = True
    'End Sub
#End Region

#Region "buttons"

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If About.Visible = True Then
            Button2.Text = "About"
            About.Close()
            My.Computer.Audio.Stop()
        Else
            About.Show()
            About.Focus()
            Button2.Text = "Close"
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
        Me.Visible = True
        NotifyIcon1.Visible = True
        My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
        Me.Text = "WoW Helper : Enabled"
        toggle3 = True
        Timer1.Enabled = True
        Timer3.Enabled = True
        My.Settings.Save()
    End Sub

    Private Sub Button4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button4.KeyDown
        If Asc(e.KeyValue) > 0 And toggle = False Then
            Button4.Text = e.KeyValue
            cKey = Chr(e.KeyValue)
            My.Settings.MainKey = Chr(e.KeyValue)
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            toggle = True
        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        toggle = False
        Button5.Text = "Waiting"
    End Sub

    Private Sub Button5_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Button5.KeyDown
        If Asc(e.KeyValue) > 0 And toggle = False Then
            Button5.Text = e.KeyValue
            iKey = e.KeyValue
            My.Settings.TriggerKey = e.KeyValue
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            toggle = True
            '            mSet = False
        End If
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        toggle = False
        Button6.Text = "Waiting"
    End Sub

    Private Sub Button6_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Button6.KeyDown
        If Asc(e.KeyValue) > 0 And toggle = False Then
            Button6.Text = e.KeyValue
            sKey = Chr(e.KeyValue)
            My.Settings.ComboKey = Chr(e.KeyValue)
            'MsgBox("You have pressed " & e.KeyValue & " key") 'debugging
            e.Handled = True
            toggle = True
        End If
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        If Help.Visible = True Then
            Button7.Text = "Help"
            My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
            Help.Close()
        Else
            Help.Show()
            Help.Focus()
            My.Computer.Audio.Play(My.Resources.Minimize, AudioPlayMode.Background)
            Button7.Text = "Close"
        End If
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        ComboTog = True
        Timer1.Enabled = True
    End Sub

#End Region

#Region "timers"
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CurrentColor = GetPixelColor(px, py)
        If warlock.Checked = True Then
            If Not CurrentColor.B < 150 And CheckBox1.Checked = True Then
                Timer3.Enabled = True
                toggle2 = True
            Else
                Timer3.Enabled = False
                toggle2 = False
            End If
        End If
        If druid.Checked = True Then
            If Not CurrentColor.R < 150 And CheckBox1.Checked = True Then
                Timer3.Enabled = True
                toggle2 = True
            Else
                Timer3.Enabled = False
                toggle2 = False
            End If
        End If
        If rogue.Checked = True Then
            If Not CurrentColor.R < 150 And CheckBox1.Checked = True Then
                Timer3.Enabled = True
                toggle2 = True
            Else
                Timer3.Enabled = False
                toggle2 = False
            End If
        End If
        If GetAsyncKeyState(iKey) <> 0 And toggle2 = False Then
            Me.Text = "WoW Helper : Active"
            Timer2.Enabled = True
        Else
            Me.Text = "WoW Helper : Idle"
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If iKey <> 0 Then
            count = count + 1
            SendKeys.Send("{" + cKey + "}")
            Label1.Text = count
            NotifyIcon1.Text = count
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        Dim ActiveProcess As Process = GetActiveProcess() 'ProcessHelper.

        If ActiveProcess IsNot Nothing AndAlso _
            String.Equals(ActiveProcess.ProcessName, "Wow", StringComparison.OrdinalIgnoreCase) Or String.Equals(ActiveProcess.ProcessName, "Wow-64", StringComparison.OrdinalIgnoreCase) Then
            SendKeys.Send("{" + sKey + "}")
            '            MessageBox.Show("A 'javaw.exe' process has focus!")
        End If
    End Sub
#End Region
End Class
