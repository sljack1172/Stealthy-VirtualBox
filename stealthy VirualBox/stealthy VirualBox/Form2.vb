Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = "The path to your VirtualBox folder should be set, if not click the" & vbCrLf & """browse""" & " button and select your VirtualBox folder."
        Label4.Text = "The ""Command to run"" combobox is just that, ""setextradata"" sets the new data that you type" & vbCrLf & "in to ""Data to set"" textbox. ""getextradata""" & "gets the data that is set."
        Label6.Text = "Use the ""VirtualBox VM to modify"" combobox to select the VM you want to edit."
        Label8.Text = "The ""Key to change"" combobox is used with getextradata and setextradata, HOWEVER," & vbCrLf & " the second option ""enumerate"" is for getextradata ONLY."
        Label10.Text = "The ""Data to set"" textbox is not used if you are using ""getextradata""," & vbCrLf & "otherwise simply add the value you want to show" & vbCrLf & "when the scammer runs msinfo32 or systeminfo."
        Label12.Text = "Click the ""Execute"" button."
    End Sub
End Class