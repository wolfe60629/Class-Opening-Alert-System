Public Class Form2

    Dim I As Integer = 1
    Dim ErrorOnLoad As Boolean = False
    Dim webbrowser0 As New WebBrowser

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
        AddHandler Label1.MouseDown, New MouseEventHandler(AddressOf ctl_MouseDown)
        AddHandler Label1.MouseMove, New MouseEventHandler(AddressOf ctl_MouseMove)
        AddHandler Label1.MouseUp, New MouseEventHandler(AddressOf ctl_MouseUp)
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


    Public Sub AddTab(url As String, CheckForLogin As Boolean)
        Dim tabPage As New TabPage(url)
        tabPage.Controls.Add(webbrowser0)
        webbrowser0.Dock = DockStyle.Fill
        TabControl1.TabPages.Add(tabPage)
        TabControl1.SelectedTab = tabPage
        webbrowser0.Navigate(urlString:=url)
        WaitForPageLoad()
        If CheckForLogin = True Then
            Dim UsernameFieldDetected As Boolean
            Dim PasswordFieldDetected As Boolean

            Try
                Dim eleCollection = webbrowser0.Document.GetElementsByTagName("TABLE")

                If eleCollection(2).InnerText.Contains("Username") And eleCollection(2).InnerText.Contains("Password") Then
                    UsernameFieldDetected = True
                    PasswordFieldDetected = True
                Else
                    UsernameFieldDetected = False
                    PasswordFieldDetected = False
                End If
            Catch ex As Exception

            End Try
            If UsernameFieldDetected = True And PasswordFieldDetected = True And My.Settings.AutoLogin = True Then
                Form1.StatusLabel.Text = "Logging In"

                AutoLogin(My.Settings.UNGUserName, My.Settings.UNGPassword, My.Settings.UNGTerm, url)




            ElseIf UsernameFieldDetected = True And PasswordFieldDetected = True And My.Settings.AutoLogin = False Then
                MsgBox("You Have Been Logged Out | Please Log In Manually", MsgBoxStyle.Critical, "Log In")
                Form1.Button2.PerformClick()

            End If
        ElseIf CheckForLogin = False Then

        End If


    End Sub
    Public Sub DeleteTabs()

        TabControl1.Controls.Clear()


    End Sub

    Public Function GetHTML(index As Integer)
        If Not index = Nothing Then
            Me.TabControl1.SelectTab(index)
        End If

        Try
            Return CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Document.GetElementsByTagName("TABLE")
        Catch
        End Try

    End Function



    Public Sub AutoLogin(username As String, password As String, term As Integer, NavigationFromURL As String)

        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Document.GetElementById("UserID").InnerText = username
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Document.GetElementsByTagName("Input").GetElementsByName("PIN")(0).InnerText = password
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Document.GetElementsByTagName("Input").GetElementsByName("")(0).InvokeMember("Click")
        WaitForPageLoad()
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Navigate("https://ungssb.ung.edu/pls/ungprod/bwskflib.P_SelDefTerm")
        WaitForPageLoad()
        Try

            CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Document.GetElementById("term_id").SetAttribute("value", term)
            CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Document.GetElementsByTagName("Input").GetElementsByName("")(1).InvokeMember("Click")
            WaitForPageLoad()

            'Navigate Tab back 
            CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Navigate(NavigationFromURL)
            WaitForPageLoad()


        Catch ex As Exception
            MsgBox("Application Failed While Inserting Term: Check UNG Log In Username And Password", MsgBoxStyle.Critical, "AutoLogin Failure")
        End Try

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs)
        AddTab("https://ungssb.ung.edu/pls/ungprod/html_schedule2.html_avail_sections?campus_in=GA&subj1_in=Soci&numb1_in=1160&subj2_in=&numb2_in=&subj3_in=&numb3_in=&subj4_in=&numb4_in=&subj5_in=&numb5_in=&subj6_in=&numb6_in=&subj7_in=&numb7_in=&subj8_in=&numb8_in=&sess_in=&open_or_all_in=OPEN+ONLY&CALLING_PROC_NAME=HTML_SCHEDULE2.HTML_SELECT_SUBJECTSE", False)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Hide()
    End Sub



    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Property pageready As Boolean = False

#Region "Page Loading Functions"
    Public Sub WaitForPageLoad()

        AddHandler CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not pageready
            TimeoutTimer.Enabled = True
            TimeoutTimer.Start()
            Application.DoEvents()
        End While
        If CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).DocumentText.Contains("Navigation to") = True Then
            ErrorOnLoad = True
        End If
        pageready = False
        TimeoutTimer.Stop()
        TimeoutTimer.Enabled = False
    End Sub

    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).ReadyState = WebBrowserReadyState.Complete Then
            pageready = True
            
            RemoveHandler CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        End If
    End Sub

#End Region


#Region "Dependent Subs"

    Private Sub wait(ByVal seconds As Integer)

        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub

#End Region

    Private Sub TimeoutTimer_Tick(sender As Object, e As EventArgs) Handles TimeoutTimer.Tick
        CType(TabControl1.SelectedTab.Controls.Item(0), WebBrowser).Stop()


        ErrorOnLoad = True

    End Sub

    Public Function CheckErrorOnLoad()
        If ErrorOnLoad = True Then
            Dim _error_on_load
            _error_on_load = ErrorOnLoad

            ErrorOnLoad = False
            Return _error_on_load

        Else
            Return ErrorOnLoad
        End If

    End Function
End Class

