Imports System.IO
Imports System.Threading

Public Class Form1

    Private psi As ProcessStartInfo
    Dim systemencoding As System.Text.Encoding
    Private cmd As Process
    Dim Startup As Integer = 0

    Private Delegate Sub InvokeWithString(ByVal text As String)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FindVirtualBox()
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim dialog = New FolderBrowserDialog()
        dialog.SelectedPath = Application.StartupPath
        If DialogResult.OK = dialog.ShowDialog() Then
            txtVBPath.Text = dialog.SelectedPath
        End If

    End Sub

    Private Sub FindVirtualBox()
        If Directory.Exists("C:\Program Files\Oracle\VirtualBox") Then
            txtVBPath.Text = "C:\Program Files\Oracle\VirtualBox"
        Else
            If Directory.Exists("C:\Program Files (x86)\Oracle\VirtualBox") Then
                txtVBPath.Text = "C:\Program Files (x86)\Oracle\VirtualBox"
            End If
        End If
        If txtVBPath.Text <> "" Then
            ExecuteCommand()
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        ExecuteCommand()
    End Sub

    Private Sub ExecuteCommand()
        Try
            cmd.Kill()
        Catch ex As Exception
        End Try

        txtOutput.Clear()
        If Startup = 0 Then
            psi = New ProcessStartInfo("cmd.exe", "/C VBoxManage.exe list vms")
            Startup = Startup + 1
        Else
            If ComboBox2.SelectedItem.ToString = "setextradata" Then
                psi = New ProcessStartInfo("cmd.exe", "/C VBoxManage.exe " & BuildCommand() & " " & """" & TextBox1.Text & """")
            Else
                psi = New ProcessStartInfo("cmd.exe", "/C VBoxManage.exe " & BuildCommand())
            End If
        End If
        System.Text.Encoding.GetEncoding(Globalization.CultureInfo.CurrentUICulture.TextInfo.OEMCodePage)
        With psi
            .WorkingDirectory = "C:\Program Files\Oracle\VirtualBox"
            .UseShellExecute = False
            .RedirectStandardError = True
            .RedirectStandardInput = True
            .RedirectStandardOutput = True
            .CreateNoWindow = True
            .StandardOutputEncoding = systemencoding
            .StandardOutputEncoding = systemencoding
        End With

        cmd = New Process With {.StartInfo = psi, .EnableRaisingEvents = True}
        AddHandler cmd.ErrorDataReceived, AddressOf Async_Data_Received
        AddHandler cmd.OutputDataReceived, AddressOf Async_Data_Received
        cmd.Start()
        cmd.BeginOutputReadLine()
        cmd.BeginErrorReadLine()
    End Sub

    Private Sub Async_Data_Received(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Me.Invoke(New InvokeWithString(AddressOf Sync_Output), e.Data)
    End Sub

    Private Sub Sync_Output(ByVal text As String)
        If text <> "" Then
            If text.Contains("{") Then
                text = text.Substring(1, text.IndexOf("{") - 3)
                ComboBox1.Items.Add(text)
                If ComboBox1.Items.Count > 0 Then
                    ComboBox1.SelectedIndex = 0
                End If
            Else
                txtOutput.AppendText(text & Environment.NewLine)
                txtOutput.ScrollToCaret()
            End If
        End If
    End Sub

    Function BuildCommand() As String
        Dim commandtorun As String = ""
        Dim value As String = ""
        Dim vmname As String = ""

        Select Case ComboBox3.SelectedItem.ToString
            Case "System Vendor"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemVendor"""
                Exit Select
            Case "Board Vendor"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardVendor"""
                Exit Select
            Case "BIOS Release Date"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBIOSReleaseDate"""
                Exit Select
            Case "BIOS Vendor"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBIOSVendor"""
                Exit Select
            Case "BIOS Version"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBIOSVersion"""
                Exit Select
            Case "Board Asset Tag"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardAssetTag"""
                Exit Select
            Case "Board Board Type"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardBoardType"""
                Exit Select
            Case "Board Loc In Chass"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardLocInChass"""
                Exit Select
            Case "Board Product"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardProduct"""
                Exit Select
            Case "Board Serial"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardSerial"""
                Exit Select
            Case "Board Version"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiBoardVersion"""
                Exit Select
            Case "OEM VBox Rev"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiOEMVBoxRev"""
                Exit Select
            Case "OEM VBox Ver"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiOEMVBoxVer"""
                Exit Select
            Case "System Family"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemFamily"""
                Exit Select
            Case "System Product"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemProduct"""
                Exit Select
            Case "System SKU"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemSKU"""
                Exit Select
            Case "System Serial"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemSerial"""
                Exit Select
            Case "System Uuid"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemUuid"""
                Exit Select
            Case "System Version"
                value = """VBoxInternal/Devices/pcbios/0/Config/DmiSystemVersion"""
                Exit Select
            Case "enumerate"
                value = "enumerate"
                Exit Select
        End Select

        commandtorun = ComboBox2.SelectedItem.ToString
        vmname = ComboBox1.SelectedItem.ToString

        Return commandtorun & " " & """" & vmname & """" & " " & value
    End Function

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Form2.Show()
    End Sub
End Class
