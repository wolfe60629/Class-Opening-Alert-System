Public Class Settings
    Dim TextSettingsModified As Boolean
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        
        TPAddr.Text = My.Settings.PhoneNumb

        TUngUser.Text = My.Settings.UNGUserName
        TUngPass.Text = My.Settings.UNGPassword
        ComboTerm.Text = My.Settings.SettingsTerm
        STB1.Text = My.Settings.S1
        STB2.Text = My.Settings.S2
        STB3.Text = My.Settings.S3
        STB4.Text = My.Settings.S4
        STB5.Text = My.Settings.S5
        STB6.Text = My.Settings.S6
        STB7.Text = My.Settings.S7
        STB8.Text = My.Settings.S8
        NTB1.Text = My.Settings.N1
        NTB2.Text = My.Settings.N2
        NTB3.Text = My.Settings.N3
        NTB4.Text = My.Settings.N4
        NTB5.Text = My.Settings.N5
        NTB6.Text = My.Settings.N6
        NTB7.Text = My.Settings.N7
        NTB8.Text = My.Settings.N8
        Carrier.Text = My.Settings.Carrier
        CampusSelect.Text = My.Settings.Campus
        If My.Settings.AlertSetting = "Text" Then
            RadioButton1.Checked = True
        End If
        If My.Settings.AlertSetting = "MessageBox" Then
            RadioButton2.Checked = True
        End If
        If My.Settings.AutoLogin = True Then
            RadioButton3.Checked = True
        Else
            RadioButton4.Checked = True
        End If
        TextSettingsModified = False
    End Sub

#Region "GUI SUBS"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler pan_header.MouseDown, New MouseEventHandler(AddressOf ctl_MouseDown)
        AddHandler pan_header.MouseMove, New MouseEventHandler(AddressOf ctl_MouseMove)
        AddHandler pan_header.MouseUp, New MouseEventHandler(AddressOf ctl_MouseUp)
        AddHandler StatusLabel.MouseDown, New MouseEventHandler(AddressOf ctl_MouseDown)
        AddHandler StatusLabel.MouseMove, New MouseEventHandler(AddressOf ctl_MouseMove)
        AddHandler StatusLabel.MouseUp, New MouseEventHandler(AddressOf ctl_MouseUp)
    End Sub

    Private m_WindowState As FormWindowState = FormWindowState.Normal
    Private m_MousePressed As Boolean = False
    Private m_oldX As Integer, my_oldY As Integer

    Private Sub ctl_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim TS As Point = Me.PointToScreen(e.Location)
        m_oldX = TS.X
        my_oldY = TS.Y
        m_MousePressed = True
    End Sub

    Private Sub ctl_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        m_MousePressed = False
    End Sub

    Private Sub ctl_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If m_MousePressed = True AndAlso m_WindowState <> FormWindowState.Maximized Then
            Dim TS As Point = Me.PointToScreen(e.Location)

            Me.Location = New Point(Me.Location.X + (TS.X - m_oldX), Me.Location.Y + (TS.Y - my_oldY))
            m_oldX = TS.X
            my_oldY = TS.Y
        End If
    End Sub



#End Region
#Region "Close Button"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If My.Settings.FirstRun = True Then
            Application.Exit()
        Else
            Me.Close()
        End If

    End Sub
#End Region
#Region "Radio Buttons"
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            TPAddr.Visible = False
            TPAddr.Enabled = False
            Label17.Visible = False
            Label19.Visible = False
            Label20.Visible = False
            Label6.Visible = False
            Carrier.Enabled = False
            Carrier.Visible = False
            Label11.Location = New Point(19, 54)
            CampusSelect.Location = New Point(83, 83)
        End If
        If RadioButton1.Checked = True Then
            TPAddr.Visible = True
            TPAddr.Enabled = True
            Label17.Visible = True
            Label19.Visible = True
            Label20.Visible = True
            Label6.Visible = True
            Carrier.Enabled = True
            Carrier.Visible = True
            Label11.Location = New Point(19, 200)
            CampusSelect.Location = New Point(83, 229)
        End If
    End Sub
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            TUngUser.Enabled = True
            TUngUser.Visible = True
            TUngPass.Enabled = True
            TUngPass.Visible = True
            ComboTerm.Enabled = True
            ComboTerm.Visible = True
            Label3.Visible = True
            Label4.Visible = True
            Label5.Visible = True

        End If
        If RadioButton4.Checked = True Then
            TUngUser.Enabled = False
            TUngUser.Visible = False
            TUngPass.Enabled = False
            TUngPass.Visible = False
            ComboTerm.Enabled = False
            ComboTerm.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False
        End If
    End Sub
    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged

    End Sub
#End Region


    Private Sub GenerateURL()
        Dim Url As String
        Dim CampusCode As String

        Select Case CampusSelect.Text
            Case "All"
                CampusCode = "ALLCAMPUSES"
            Case "Blue Ridge Campus"
                CampusCode = "BLR"
            Case "Brenau University"
                CampusCode = "BU"
            Case "Cumming Campus"
                CampusCode = "9"
            Case "Dahlonega Campus"
                CampusCode = "A"
            Case "Gainesville Campus"
                CampusCode = "GA"
            Case "Georgia ONmyLINE"
                CampusCode = "GML"
            Case "Oconee Campus"
                CampusCode = "OC"
            Case "Off Campus"
                CampusCode = "F"
            Case "Summer Language Institute"
                CampusCode = "SLI"
            Case "UNG Online"
                CampusCode = "NGO"
            Case "eCore"
                CampusCode = "EC"
            Case Else
                CampusCode = "ALLCAMPUSES"
        End Select

        Url = "https://ungssb.ung.edu/pls/ungprod/html_schedule2.html_avail_sections?" & _
            "campus_in=" & CampusCode & _
            "&subj1_in=" & STB1.Text & _
            "&numb1_in=" & NTB1.Text & _
            "&subj2_in=" & STB2.Text & _
            "&numb2_in=" & NTB2.Text & _
            "&subj3_in=" & STB3.Text & _
            "&numb3_in=" & NTB3.Text & _
            "&subj4_in=" & STB4.Text & _
            "&numb4_in=" & NTB4.Text & _
            "&subj5_in=" & STB5.Text & _
            "&numb5_in=" & NTB5.Text & _
            "&subj6_in=" & STB6.Text & _
            "&numb6_in=" & NTB6.Text & _
            "&subj7_in=" & STB7.Text & _
            "&numb7_in=" & NTB7.Text & _
            "&subj8_in=" & STB8.Text & _
            "&numb8_in=" & NTB8.Text & _
            "&sess_in=" & _
            "&open_or_all_in=OPEN+ONLY" & _
            "&CALLING_PROC_NAME=HTML_SCHEDULE2.HTML_SELECT_SUBJECTSE"
        My.Settings.Url = Url
        My.Settings.Save()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Check to see if the autologin is enabled
        If RadioButton3.Checked = True Then 'If AutoLogin is enabled
            My.Settings.AutoLogin = True
            My.Settings.UNGUserName = TUngUser.Text
            My.Settings.UNGPassword = TUngPass.Text
            My.Settings.Save()
        End If
        If RadioButton4.Checked = True Then 'If AutoLogin is disabled
            My.Settings.AutoLogin = False
            My.Settings.Save()
        End If


        If Not STB1.Text = "" Then
            GenerateURL()
            My.Settings.S1 = STB1.Text
            My.Settings.S2 = STB2.Text
            My.Settings.S3 = STB3.Text
            My.Settings.S4 = STB4.Text
            My.Settings.S5 = STB5.Text
            My.Settings.S6 = STB6.Text
            My.Settings.S7 = STB7.Text
            My.Settings.S8 = STB8.Text
            My.Settings.N1 = NTB1.Text
            My.Settings.N2 = NTB2.Text
            My.Settings.N3 = NTB3.Text
            My.Settings.N4 = NTB4.Text
            My.Settings.N5 = NTB5.Text
            My.Settings.N6 = NTB6.Text
            My.Settings.N7 = NTB7.Text
            My.Settings.N8 = NTB8.Text
            My.Settings.Campus = CampusSelect.Text

            My.Settings.Save()
        End If



        'Check Required Fields For Text 
        If RadioButton1.Checked = True Then
            If CheckTextRequirements() = True Then 'All Required Fields Are Met
                My.Settings.Carrier = Carrier.Text
                My.Settings.PhoneNumb = TPAddr.Text
                My.Settings.AlertSetting = "Text"
                My.Settings.SettingsTerm = ComboTerm.Text
                My.Settings.Save()

                Select Case My.Settings.Carrier
                    Case "Alltel"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@sms.alltelwireless.com"
                        My.Settings.Save()
                    Case "AT&T"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@txt.att.net"
                        My.Settings.Save()
                    Case "Boost Mobile"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@sms.myboostmobile.com"
                        My.Settings.Save()
                    Case "Cricket Wireless"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@sms.mycricket.com"
                        My.Settings.Save()
                    Case "MetroPCS"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@mymetropcs.com"
                        My.Settings.Save()
                    Case "Project Fi"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@msg.fi.google.com"
                        My.Settings.Save()
                    Case "Republic Wireless"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@text.republicwireless.com"
                        My.Settings.Save()
                    Case "Sprint"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@messaging.sprintpcs.com"
                        My.Settings.Save()
                    Case "T-Mobile"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@tmomail.net"
                        My.Settings.Save()
                    Case "U.S. Cellular"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@email.uscc.net"
                        My.Settings.Save()
                    Case "Verizon Wireless"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@vtext.com"
                        My.Settings.Save()
                    Case "Virgin Mobile"
                        My.Settings.PhoneAddress = My.Settings.PhoneNumb & "@vmobl.com"
                        My.Settings.Save()
                    Case Else
                End Select

                If TextSettingsModified = True Then
                    Dim TestMessage As New DataCompTool
                    TestMessage.InvokeAlert("Test Message", "", "", "", "", "", "", "", "", "")
                    Dim result As Integer = MsgBox("In A Few Seconds A Test Message Will Be Sent To Your Phone. Did You Recieve One?", MsgBoxStyle.YesNo, "Testing The Settings")
                    If result = DialogResult.No Then

                    ElseIf result = DialogResult.Yes Then
                        Form1.Start.Enabled = True
                        Form1.Button2.Enabled = True
                        Form1.Button3.Enabled = True
                        My.Settings.FirstRun = False
                        My.Settings.Save()
                        Me.Close()
                        Form1.Opacity = 1
                    End If
                ElseIf TextSettingsModified = False Then
                    Form1.Start.Enabled = True
                    Form1.Button2.Enabled = True
                    Form1.Button3.Enabled = True
                    My.Settings.FirstRun = False
                    My.Settings.Save()
                    Me.Close()
                    Form1.Opacity = 1

                End If



            Else
                MsgBox("Please fill in all of the required fields", MsgBoxStyle.Critical, "Failed To Save")
            End If

        End If



        If RadioButton2.Checked = True Then
            My.Settings.PhoneNumb = ""
            My.Settings.AlertSetting = "MessageBox"
            My.Settings.FirstRun = False
            My.Settings.Save()
            Form1.Start.Enabled = True
            Form1.Button2.Enabled = True
            Form1.Button3.Enabled = True
            Me.Close()
            Form1.Opacity = 1
        End If

        'Convert Selected Term To Integer
        Select Case My.Settings.SettingsTerm
            Case "Spring 2020"
                My.Settings.UNGTerm = "202002"
                My.Settings.Save()

		End Select


    End Sub


    Private Function CheckTextRequirements()
        'Checks Each Column To See If The Requirements Fail. If they do then they add to the failure int
        Dim Failure As Integer = 0
        If RadioButton1.Checked = True Then
            If Carrier.Text.Count <= 0 Then
                Failure = Failure + 1
            End If
            If TPAddr.Text.Count <= 0 Then
                Failure = Failure + 1
            End If
        End If

        If RadioButton3.Checked = True Then
            If TUngUser.Text.Count <= 0 Then
                Failure = Failure + 1
            End If
            If TUngPass.Text.Count <= 0 Then
                Failure = Failure + 1
            End If
            If ComboTerm.Text.Length <= 0 Then
                Failure = Failure + 1
            End If
        End If


        'if the failure integer is greater than 0 then the function will return false for a failure to meet requirements
        If Failure > 0 Then
            CheckTextRequirements = False
        Else
            CheckTextRequirements = True
        End If



    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ResetSettings()

        Application.Exit()

    End Sub

    Private Sub ResetSettings()

        My.Settings.Carrier = ""
        My.Settings.PhoneNumb = ""
        My.Settings.AlertSetting = ""
        My.Settings.Url = ""
        My.Settings.FirstRun = True
        My.Settings.UNGPassword = ""
        My.Settings.UNGUserName = ""
        My.Settings.UNGTerm = ""
        My.Settings.N1 = ""
        My.Settings.N2 = ""
        My.Settings.N3 = ""
        My.Settings.N4 = ""
        My.Settings.N5 = ""
        My.Settings.N6 = ""
        My.Settings.N7 = ""
        My.Settings.N8 = ""
        My.Settings.N8 = ""
        My.Settings.S1 = ""
        My.Settings.S2 = ""
        My.Settings.S3 = ""
        My.Settings.S4 = ""
        My.Settings.S5 = ""
        My.Settings.S6 = ""
        My.Settings.S7 = ""
        My.Settings.S8 = ""
        My.Settings.Campus = ""
        My.Settings.Save()



    End Sub

    Private Sub TSAddr_TextChanged(sender As Object, e As EventArgs)
        TextSettingsModified = True
    End Sub

    Private Sub TEUser_TextChanged(sender As Object, e As EventArgs)
        TextSettingsModified = True
    End Sub

    Private Sub TEPass_TextChanged(sender As Object, e As EventArgs)
        TextSettingsModified = True
    End Sub

    Private Sub TSPort_TextChanged(sender As Object, e As EventArgs)
        TextSettingsModified = True
    End Sub

    Private Sub TPAddr_TextChanged(sender As Object, e As EventArgs) Handles TPAddr.TextChanged
        TextSettingsModified = True
    End Sub



    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub



    Private Sub Label16_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.Image = My.Resources.Save_Hover
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.Image = My.Resources.Save_Static
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Button2.Image = My.Resources.reset_hover
    End Sub

	Private Sub ComboTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboTerm.SelectedIndexChanged

	End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.Image = My.Resources.reset_static
    End Sub
End Class