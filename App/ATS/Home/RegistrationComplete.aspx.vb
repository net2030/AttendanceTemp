
Partial Class Home_Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.lblEMailAddress.Text = Request.QueryString("EMailAddress")
        Me.Literal1.Text = ResourceHelper.GetFromResource("Thank you for your interest your TimeLive account has been activated.")
        Me.Literal4.Text = ResourceHelper.GetFromResource("You can log-in in TimeLive using your administrator email address and password which you entered in registration form.")
        Me.Literal5.Text = ResourceHelper.GetFromResource("Just click on [Go to login page] below to navigate to login page.")

        'EMailUtilities.DequeueEmail()


    End Sub
End Class
