Imports VB = Microsoft.VisualBasic
Imports System
Imports System.Net.Mail
Imports System.Threading

Public Class Form1

#Region "Public Declarations"
    Dim DR As New DataCompTool
    Dim grow As Boolean = False
#End Region

#Region "Form1 Load"
    Private Sub Form1_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.FirstRun = True Then
            MsgBox("Welcome! Please Fill in the Following Form To Continue", MsgBoxStyle.Information, "First Run Setup")
            Settings.Show()
            Start.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Me.Opacity = 0
        End If


    End Sub
#End Region


#Region "Start Button"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Start.Click
        Button3.Enabled = False
        Start.Enabled = False
        'Width Change
        Me.Width = 1059
        Me.Height = 410
        Button5.Location = New Point(965, 0)
        Button4.Location = New Point(1010, 0)
        MoveGUI.Start()

        If My.Settings.Url.Length > 0 Then
            Dim url() = My.Settings.Url.Split(Environment.NewLine.ToCharArray())
            DR.Main(url)
            Timer1.Enabled = True
            Timer1.Start()
        Else
            MsgBox("Insert A Class To Monitor In Settings", MsgBoxStyle.Critical)
            Button2.PerformClick()
        End If

    End Sub
#End Region

#Region "Stop Button"
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button3.Enabled = True
        Start.Enabled = True

        Me.Height = 291
        Me.Width = 225
        Button5.Location = New Point(136, 0)
        Button4.Location = New Point(180, 0)
        StatusLabel.Text = ""
        Timer1.Stop()
        Timer1.Enabled = False
        DR.cleardataonstop()
    End Sub
#End Region

#Region "Settings Button"
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Button2.PerformClick()
        Settings.Show()
    End Sub
#End Region

#Region "Check Classes Timer"
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '   WaitPageLoad.Start()
        If My.Settings.Url.Length > 0 Then
            Dim url() = My.Settings.Url.Split(Environment.NewLine.ToCharArray())
            DR.Main(url)
            Timer1.Enabled = True
            Timer1.Start()
        Else
            MsgBox("Insert Webpage(s) To Monitor In Settings", MsgBoxStyle.Critical)
            Button2.PerformClick()
        End If

        '  System.Windows.Forms.Application.DoEvents()
        ' BackgroundWorker1.RunWorkerAsync()



    End Sub
#End Region

#Region "Manual Login Button"
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.Show()
        Form2.AddTab("https://ungssb.ung.edu/pls/ungprod/twbkwbis.P_wwwlogin", False)
    End Sub
#End Region

#Region "Show Form2 Button"
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form2.Show()
    End Sub
#End Region

#Region "GUI SUBS"

    Private Sub Start_MouseLeave(sender As Object, e As EventArgs) Handles Start.MouseLeave
        Start.BackgroundImage = My.Resources.Start_Active
    End Sub
    Private Sub Start_MouseEnter(sender As Object, e As EventArgs) Handles Start.MouseEnter
        Start.BackgroundImage = My.Resources.Start_Hover
    End Sub
    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Button2.BackgroundImage = My.Resources.Stop_hover
    End Sub
    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.BackgroundImage = My.Resources.Stop_Active
    End Sub
    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles Button3.MouseEnter
        Button3.BackgroundImage = My.Resources.login_hover
    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave
        Button3.BackgroundImage = My.Resources.login_active1
    End Sub
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
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
#End Region

#Region "Not Used Subs"
    Private Sub MoveGUI_Tick(sender As Object, e As EventArgs) Handles MoveGUI.Tick



    End Sub






    Private Sub Form1_Load(sender As Object, e As EventArgs)

    End Sub
    Private Sub Start_MouseHover(sender As Object, e As EventArgs) Handles Start.MouseHover

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs)

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork



    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub DataGridView3_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.RowEnter

    End Sub

    Public Sub WaitPageLoad_Tick(sender As Object, e As EventArgs) Handles WaitPageLoad.Tick



    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        'The Background worker will be a feature implemented at another time
        'ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub pan_header_Paint(sender As Object, e As PaintEventArgs) Handles pan_header.Paint

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()

    End Sub


#End Region




    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.BackgroundImage = My.Resources.Settings_hover
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.BackgroundImage = My.Resources.Settings
    End Sub
End Class
Public Class DataCompTool
#Region "DataCompTool Declarations"
    Dim Current As DataGridView
    Public ErrorOnLoad As Boolean
    Dim DataObjects(10, 2) As DataGridView
    Dim Previous As DataGridView
    Dim Delta As DataGridView
    Dim TimerInt As New Integer
    Dim LoadedTables As Integer
    Dim WithEvents CountDownTimer As New System.Windows.Forms.Timer
#End Region

#Region "Main"
    Public Sub Main(URL As Array)

        Form1.StatusLabel.Text = "Loading WebPages"
        'Loads Page If URL is not blank 
        LoadedTables = 0
        For Each _URL In URL
            If Not _URL = "" Then
                LoadedTables = LoadedTables + 1
                LoadTable(URL:=_URL)
            End If
        Next


        'Check to see if there are any errors with the page(s) loading
        ErrorOnLoad = Form2.CheckErrorOnLoad()


        'If no errors with the page loading then continue main 
        If Not ErrorOnLoad = True Then

            For i = 0 To Form2.TabControl1.TabCount - 1
                'Select our current tab 
                Form2.TabControl1.SelectTab(i)

                'Declare local current, previous, and delta 
                Current = DataObjects(i, 0)
                Previous = DataObjects(i, 1)
                Delta = DataObjects(i, 2)

                'Declare TimerInt for each round 

                TimerInt = TimerAlgorithm(LoadedTables)

                'Check to see if DataObjects "Current" is blank; Check to see if gatherdata returns blank
                If Not DataObjects(i, 0) Is Nothing Then
                    If Not GatherData(i) Is Nothing Then

                        'If DataObjects and GatherData are not Blank Then Copy The Current Data Into Previous
                        Previous = Current

                        'Gather New Current Data
                        Current = GatherData(i)

                        'Compare Data From Current And Previous. Put changes in Delta
                        Delta = CompareData(Current, Previous)

                        'Copy Data Data Into Array 
                        DataObjects(i, 0) = Current
                        DataObjects(i, 1) = Previous
                        DataObjects(i, 2) = Delta

                        'Show User The Results (Delta And Current)
                        CopyData(DataObjects(i, 0), Form1.DataGridView1, True, True)
                        CopyData(DataObjects(i, 2), Form1.DataGridView3, False, False)

                        Form1.DataGridView1.Sort(Form1.DataGridView1.Columns.Item(0), ComponentModel.ListSortDirection.Descending)
                        Form1.DataGridView3.Sort(Form1.DataGridView3.Columns.Item(0), ComponentModel.ListSortDirection.Descending)

                        CountDownTimerLogic(TimerInt)
                        Form1.Timer1.Interval = TimerInt
                    Else

                    End If

                Else
                    'If DataObjects "Current" is null, gather data into the aray
                    DataObjects(i, 0) = GatherData(i)

                    'Copy gathered data for the user 
                    CopyData(DataObjects(i, 0), Form1.DataGridView1, True, True)

                    'Set Timer Int
                    Form1.Timer1.Interval = TimerInt
                    CountDownTimerLogic(TimerInt)
                End If

            Next
            'Delete Tabs for prep of next run 
            Form2.DeleteTabs()




        ElseIf ErrorOnLoad = True Then
            'If ErrorOnLoad is true from the webbrowser then delete old data to prevent from comparison of null data
            'Array.Clear(DataObjects, 0, DataObjects.Length)
            Form2.DeleteTabs()
            CountDownTimerLogic(50000)
            Form1.Timer1.Interval = 55000
        End If
    End Sub
#End Region
  
#Region "Load Table"
    Public Sub LoadTable(URL As String)
        'Loads WebBrowser To The Defined URL
        Form2.AddTab(URL, True)
    End Sub
#End Region
   
#Region "Gather Data"
    Private Function GatherData(index As Integer)
        'Declare Booleans to detect if autologin is needed
        Dim UserNameFieldDetected As Boolean = False
        Dim PasswordFieldDetected As Boolean = False


        Try
            'Declare local datagridview
            Dim DataGrid As New DataGridView
            DataGrid.AllowUserToAddRows = False

            'Gather HTML collection from selected tabpage 
            Dim eleCollection = Form2.GetHTML(index)

            'Get header for local datagrid
            For Each htmltag As HtmlElement In eleCollection
                If htmltag.InnerText.Contains("CRN") Then
                    Dim tRows = htmltag.GetElementsByTagName("tr") ' get rows
                    Dim tHead As HtmlElementCollection = tRows(0).GetElementsByTagName("td") 'get headercolum
                    For Each th As HtmlElement In tHead
                        DataGrid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = th.InnerText, .HeaderText = th.InnerText})
                    Next

                    'Get Rows for local datagrid
                    For i = 1 To tRows.Count - 1 ' get data rows
                        Dim r As New DataGridViewRow
                        For Each td As HtmlElement In tRows(i).GetElementsByTagName("td")
                            r.Cells.Add(New DataGridViewTextBoxCell With {.Value = td.InnerText})
                        Next
                        DataGrid.Rows.Add(r)
                    Next
                    Exit For
                End If

            Next htmltag

            DataGrid.EndEdit()

            Return DataGrid

        Catch ex As Exception
            'If any problems occur, return GatherData as null value
            GatherData = Nothing
        End Try
    End Function
#End Region

#Region "Copy Data"
    Public Sub CopyData(Source As DataGridView, Destination As DataGridView, ClearOld As Boolean, CopyColumn As Boolean)
        'Clears destination before copying if ClearOld is true 
        If ClearOld = True Then
            Destination.Columns.Clear()
        End If

        'Copies Columns Over To Destination is CopyColumn is true
        If CopyColumn = True Then
            Try
                For i As Integer = 0 To (Source.Columns.Count - 1)
                    Destination.Columns.Add(New DataGridViewTextBoxColumn With {.Name = Source.Columns(i).Name, .HeaderText = Source.Columns(i).HeaderText})
                Next
            Catch ex As Exception
            End Try
        End If

        If CopyColumn = False Then
            If Destination.Columns.Count < 1 Then
                For i As Integer = 0 To (Source.Columns.Count - 1)
                    Try
                        Destination.Columns.Add(New DataGridViewTextBoxColumn With {.Name = Source.Columns(i).Name, .HeaderText = Source.Columns(i).HeaderText})
                    Catch ex As Exception
                    End Try
                Next
            End If
        End If

        'Copies Rows Over To Destination
        Try
            For rowIndex As Integer = 0 To (Source.Rows.Count - 1)
                Destination.Rows.Add(Source.Rows(rowIndex).Cells.Cast(Of DataGridViewCell).Select(Function(c) c.Value).ToArray)
            Next
            Destination.EndEdit()
            Source.EndEdit()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Compare Data"
    Private Function CompareData(Current As DataGridView, Previous As DataGridView)
        'Compares data from current to previous / previous to current and inserts the data into delta

        'Formats Delta With Columns
        Dim Delta As New DataGridView
        Delta.Columns.Clear()
        Delta.AllowUserToAddRows = False
        Delta.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Timestamp", .HeaderText = "Timestamp"})
        Delta.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status"})
        For i As Integer = 0 To (Current.Columns.Count - 1)
            Delta.Columns.Add(New DataGridViewTextBoxColumn With {.Name = Current.Columns(i).Name, .HeaderText = Current.Columns(i).HeaderText})
        Next


        'Compares CRN numbers from current to previous to determine added classes
        Dim isFound As Boolean = False
        For Each dgv1Row As DataGridViewRow In Current.Rows
            For Each dgv2Row As DataGridViewRow In Previous.Rows
                If dgv1Row.Cells("CRN").Value = dgv2Row.Cells("CRN").Value Then
                    isFound = True
                End If
            Next

            'Adds all items that do not have a match to the Delta Table                                             .
            If Not isFound Then
                Dim Status As String
                Dim TimeStamp As Date = Now()
                Status = "Added"
                Delta.Rows.Add(TimeStamp, Status, dgv1Row.Cells("CRN").Value, dgv1Row.Cells("Meeting Time").Value, dgv1Row.Cells("Session").Value, dgv1Row.Cells("Course").Value, dgv1Row.Cells("Hrs").Value, dgv1Row.Cells("Instructor").Value, dgv1Row.Cells("Max").Value, dgv1Row.Cells("Cur").Value, dgv1Row.Cells("Location").Value)
                InvokeAlert(Status, dgv1Row.Cells("CRN").Value, dgv1Row.Cells("Meeting Time").Value, dgv1Row.Cells("Session").Value, dgv1Row.Cells("Course").Value, dgv1Row.Cells("Hrs").Value, dgv1Row.Cells("Instructor").Value, dgv1Row.Cells("Max").Value, dgv1Row.Cells("Cur").Value, dgv1Row.Cells("Location").Value)
            End If
            isFound = False
        Next

        'Compares CRN numbers from previous to current to determine removed classes
        For Each dgv1Row As DataGridViewRow In Previous.Rows
            For Each dgv2Row As DataGridViewRow In Current.Rows
                If dgv1Row.Cells("CRN").Value = dgv2Row.Cells("CRN").Value Then
                    isFound = True
                End If
            Next

            'Adds all items that do not have a match to the Delta Table                                             .
            If Not isFound Then
                Dim Status As String
                Dim TimeStamp As Date = Now()
                Status = "Removed"
                Delta.Rows.Add(TimeStamp, Status, dgv1Row.Cells("CRN").Value, dgv1Row.Cells("Meeting Time").Value, dgv1Row.Cells("Session").Value, dgv1Row.Cells("Course").Value, dgv1Row.Cells("Hrs").Value, dgv1Row.Cells("Instructor").Value, dgv1Row.Cells("Max").Value, dgv1Row.Cells("Cur").Value, dgv1Row.Cells("Location").Value)
                InvokeAlert(Status, dgv1Row.Cells("CRN").Value, dgv1Row.Cells("Meeting Time").Value, dgv1Row.Cells("Session").Value, dgv1Row.Cells("Course").Value, dgv1Row.Cells("Hrs").Value, dgv1Row.Cells("Instructor").Value, dgv1Row.Cells("Max").Value, dgv1Row.Cells("Cur").Value, dgv1Row.Cells("Location").Value)
            End If
            isFound = False
        Next

        Return Delta
    End Function
#End Region

#Region "Invoke Alert"
    Public Sub InvokeAlert(Status, Crn, MeetingTime, Session, Course, Hrs, Instructor, Max, Cur, Location)
        'Decalarations
        Dim body As String = ""
        Dim SendText = False
        Dim SendMessageBox = False
        Dim SeatsLeft As Double

        My.Settings.EmailUserName = "collegetextnotifier@gmail.com"
        My.Settings.ServerAddress = "smtp.gmail.com"
        My.Settings.ServerPort = 587
        My.Settings.EmailPassword = "Jeremy1996@"
        My.Settings.Save()

        'Set The Alert Method
        If My.Settings.AlertSetting = "Text" Then
            SendText = True
        ElseIf My.Settings.AlertSetting = "MessageBox" Then
            SendMessageBox = True
        Else
            MsgBox("Error While Sending Alert - Check Alert Type")
            Application.Exit()
        End If

        'Calc Seats Left
        If Cur = "" Or Max = "" Then
            SeatsLeft = 0.00
        Else
            Try
                SeatsLeft = (Double.Parse(Max) - Double.Parse(Cur))
            Catch ex As Exception

            End Try

        End If

        'Set The Body Of The Message
        body =
            "Status: " & Status & Environment.NewLine _
          & "Course: " & Course & Environment.NewLine _
          & "Time: " & MeetingTime & Environment.NewLine _
          & "Instructor: " & Instructor & Environment.NewLine _
          & "CRN: " & Crn & Environment.NewLine _
          & "Max Seating: " & Max & Environment.NewLine _
          & "Currently Seating: " & Cur & Environment.NewLine _
          & "Seats Left: " & SeatsLeft
        '& "Location: " & Location
        '& "Session: " & Session & Environment.NewLine
        '& "Hrs: " & Hrs & Environment.NewLine 

        If SendText = True Then
            Try
                

                Dim SmtpServer As New SmtpClient(My.Settings.ServerAddress)

                SmtpServer.Credentials = New  _
                Net.NetworkCredential(My.Settings.EmailUserName, My.Settings.EmailPassword)
                SmtpServer.Port = My.Settings.ServerPort
                SmtpServer.EnableSsl = True

                Dim mail As New MailMessage()
                mail.From = New MailAddress(My.Settings.EmailUserName)
                mail.To.Add(My.Settings.PhoneAddress)
                mail.Body = body
                SmtpServer.Send(mail)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        ElseIf SendMessageBox = True Then
            MsgBox(body)
        End If
    End Sub
#End Region

#Region "Clear Data On Stop"
    Public Sub cleardataonstop()
        Array.Clear(DataObjects, 0, DataObjects.Length)
        Form1.DataGridView3.Columns.Clear()
        Form1.DataGridView1.Columns.Clear()
        CountDownTimer.Stop()
        CountDownTimer.Enabled = False
    End Sub
#End Region

#Region "Countdown Algorithm"
    Public Function TimerAlgorithm(NumberOfPages As Integer)
        If Not NumberOfPages = Nothing Then
            NumberOfPages = NumberOfPages * 2
            Dim RandomNumberGen As New Random
            'Random Number Between 20 and 50 
            Dim BaseTime As Integer = RandomNumberGen.Next(20, 50)
            Dim FinalTime As Integer = BaseTime + NumberOfPages
            FinalTime = FinalTime * 1000
            Return FinalTime
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Countdown Timer"
    Dim _countdownstartnumber
    Public Sub CountDownTimerLogic(CountDownStartNumber)
        If CountDownTimer.Enabled = False Then
            _countdownstartnumber = CountDownStartNumber / 1000
            CountDownTimer.Interval = 1000
            CountDownTimer.Enabled = True
            CountDownTimer.Start()
        Else
            CountDownTimer.Stop()
            _countdownstartnumber = CountDownStartNumber / 1000
            CountDownTimer.Interval = 1000
            CountDownTimer.Enabled = True
            CountDownTimer.Start()
        End If
        '  Form1.StatusLabel.Text = "Waiting For Next Check: " & CountDownStartNumber / 1000 & " Seconds"
    End Sub
    Private Sub CountDownTimer_Tick(sender As Object, e As EventArgs) Handles CountDownTimer.Tick
        If Not _countdownstartnumber = 0 Then
            If ErrorOnLoad = False Then
                _countdownstartnumber = _countdownstartnumber - 1
                Form1.StatusLabel.Text = "Waiting For Next Check: " & _countdownstartnumber & " Seconds"
            Else
                _countdownstartnumber = _countdownstartnumber - 1
                Form1.StatusLabel.Text = "Error On Previous Load - Next Check: " & _countdownstartnumber & " Seconds"
            End If

        Else
            CountDownTimer.Stop()
            CountDownTimer.Enabled = False
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
End Class







