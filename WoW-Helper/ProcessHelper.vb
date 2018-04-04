Imports System.Runtime.InteropServices

Public NotInheritable Class ProcessHelper
    'Private Sub New() 'Make no instances of this class.
    'End Sub

    '<DllImport("user32.dll", SetLastError:=True)> _
    'Private Shared Function GetForegroundWindow() As IntPtr
    'End Function

    '<DllImport("user32.dll", SetLastError:=True)> _
    'Private Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As UInteger) As Integer
    'End Function

    'Public Shared Function GetActiveProcess() As Process
    '    Dim FocusedWindow As IntPtr = GetForegroundWindow()
    '    If FocusedWindow = IntPtr.Zero Then Return Nothing

    '    Dim FocusedWindowProcessId As UInteger = 0
    '    GetWindowThreadProcessId(FocusedWindow, FocusedWindowProcessId)

    '    If FocusedWindowProcessId = 0 Then Return Nothing
    '    Return Process.GetProcessById(CType(FocusedWindowProcessId, Integer))
    'End Function
End Class
