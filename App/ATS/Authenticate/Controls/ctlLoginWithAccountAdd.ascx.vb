
Partial Class Authenticate_Controls_ctlLoginWithAccountAdd
    Inherits System.Web.UI.UserControl

    Protected Sub btnlogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        LogIn(txtemail.Text, txtpassword.Text)        
    End Sub
    Public Sub LogIn(ByVal Username As String, ByVal Password As String)
        Dim BLL As New AuthenticateBLL
        Dim Authenticated As Boolean
        Authenticated = BLL.AuthenticateLogin(Username, Password)
        If Authenticated = True Then
            BLL.LoggedIn(Username, Password)
            If DBUtilities.IsTimeLiveMobileLogin Then
                Response.Redirect(UIUtilities.RedirectToMobileHomePage, False)
            Else
                Response.Redirect(UIUtilities.RedirectToHomePage, False)
            End If
        Else
            Try
                Throw New Exception()
            Catch ex As Exception
                lblErrorMessage.Visible = True
            End Try
        End If
    End Sub

    Protected Sub btnregister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregister.Click
        'Dim AccountBLL As New AccountBLL
        'If Validate() = True Then
        '    Try
        '        AccountBLL.AddAccount(1, txtorgname.Text, "", "", "", "", drpcountry.SelectedValue, txtemailr.Text, "", "", "", 1, 6, txtemailr.Text, txtpasswordr.Text, txtemailr.Text, txtfirstname.Text, txtlastname.Text, "Mr.", "", "", False, False, Now.Date, Now.Date, "en-US")
        '        LogIn(txtemailr.Text, txtpasswordr.Text)
        '    Catch ex As Exception
        '        lblSUEM.Visible = True
        '        lblSUEM.Text = ex.Message
        '    End Try
        'End If        
    End Sub
    Public Function Validate() As Boolean
        If txtfirstname.Text = "" Or txtlastname.Text = "" Or txtpasswordr.Text = "" Or txtverifypwd.Text = "" Or txtemailr.Text = "" Or txtorgname.Text = "" Then
            Try
                Throw New Exception()
            Catch ex As Exception
                lblSUEM.Visible = True
                lblSUEM.Text = "You must fill in all of the fields."
                Return False
            End Try
        End If
        Return True
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If UIUtilities.GetApplicationMode = "Installed" Then
            Me.imglogo.ImageUrl = "~/Images/TopHeader.jpg"
            lblFooter.Text = ResourceHelper.GetFromResource("Copyright 2007 - 2009 Livetecs LLC. All rights reserved.")
            If System.Configuration.ConfigurationManager.AppSettings("DEFAULT_PAGE_TITLE") Is Nothing Then
                Me.Page.Title = ResourceHelper.GetFromResource(Me.Page.Title)
            Else
                Me.Page.Title = ResourceHelper.GetFromResource(System.Configuration.ConfigurationManager.AppSettings("DEFAULT_PAGE_TITLE"))
            End If
        End If
            If Not Me.IsPostBack Then
            'drpcountry.SelectedValue = 233
            End If
    End Sub
End Class
