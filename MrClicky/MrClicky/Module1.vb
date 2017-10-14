﻿Imports System.IO
Module Module1
    Public cliCKed As Boolean
    Public BGred As Integer ' 0 to 255
    Public BGgreen As Integer  ' 0 to 255
    Public BGblue As Integer  ' 0 to 255
    Public randomGenerator As New Random
    Public cNum As Integer
    Public omegaPassed As Boolean
    Public newBGred As Integer
    Public newBGblue As Integer
    Public newBGgrn As Integer
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    Declare Function GetDC Lib "user32" (ByVal hWnd As IntPtr) As IntPtr
    Declare Function ReleaseDC Lib "user32" (ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As IntPtr
    Private Declare Function GetPixel Lib "gdi32" (ByVal hdc As IntPtr, ByVal X As Int32, ByVal Y As Int32) As Int32
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByRef lParam As POINT) As Integer
    Public Const EM_GETSCROLLPOS As Integer = 1245
    Public Const EM_SETSCROLLPOS As Integer = 1246
    Private myformdragging As Boolean
    Public cstate As Boolean
    Public moveSet As Boolean
    Public appName As String = My.Application.Info.AssemblyName
    Public sysDir As String
    Public osBit As Boolean
    Public winDir As String
    Public winVer As String = My.Computer.Info.OSFullName.ToString
    Public path As String = Directory.GetCurrentDirectory()
    Public OPsys As String = My.Computer.Info.OSPlatform
    Public x32 As Boolean = True
    Public marqueeTEXT As String
    Public verText As String
    Structure POINT
        Dim x As Integer
        Dim y As Integer
    End Structure

    Public Function MarqueeLeft(ByVal Text As String)
        Dim Str1 As String = Text.Remove(0, 1)
        Dim Str2 As String = Text(0)
        Return Str1 & Str2
    End Function
    Public Function GetRanNum(ByVal miN As Integer, ByVal maX As Integer)
        cNum = randomGenerator.Next(miN, maX)
        Return cNum
    End Function
    Public Function bgChange()
        ' generate random numbers for rbg
        BGred = GetRanNum(0, 255)
        BGgreen = GetRanNum(0, 255)
        BGblue = GetRanNum(0, 255)
        Return True
    End Function
    Public Function bgChangePlus()
        If BGblue + 127 < 255 Then
            newBGblue = BGblue + 127
        Else
            newBGblue = (BGblue + 127) - 255
        End If
        If BGgreen + 127 < 255 Then
            newBGgrn = BGgreen + 127
        Else
            newBGgrn = (BGgreen + 127) - 255
        End If
        If BGred + 127 < 255 Then
            newBGred = BGred + 127
        Else
            newBGred = (BGred + 127) - 255
        End If
        Return True
    End Function

    Public Function GetPixelColor(x As Integer, y As Integer) As System.Drawing.Color
        Dim hdc As IntPtr = GetDC(IntPtr.Zero)
        Dim pixel As UInteger = GetPixel(hdc, x, y)
        ReleaseDC(IntPtr.Zero, hdc)
        Dim color__1 As Color = Color.FromArgb(CInt(pixel And &HFF), CInt(pixel And &HFF00) >> 8, CInt(pixel And &HFF0000) >> 16)
        Return color__1
    End Function

    Public Function GotWoW()

        Dim hWnd As IntPtr
        Dim hDC As IntPtr
        Dim test As Boolean

        hWnd = FindWindow(vbNullString, "World of Warcraft")
        If (hWnd <> 0) Then
            test = True
        End If
        hDC = GetDC(hWnd)
        ReleaseDC(hWnd, hDC)
        Return test
    End Function

End Module



