Imports System.Globalization

Partial Class GeneralControls_MyDate
    Inherits System.Web.UI.UserControl

    Public DateType As String

    Public Sub New()

    End Sub

#Region "Properties & declaration"
   


    Public Property SelectedDate() As DateTime
        Get
            If Me.DefaultCalendarCulture = DefaultCultureOption.Grgorian Then
                Return Date.ParseExact(TextBox1.Text, "yyyy/MM/dd", New CultureInfo("en-US").DateTimeFormat)
            ElseIf Me.DefaultCalendarCulture = DefaultCultureOption.Hijri Then
                Return Date.ParseExact(TextBox1.Text, "yyyy/MM/dd", New CultureInfo("ar-SA").DateTimeFormat)
            Else
                Return Date.ParseExact(TextBox1.Text, "yyyy/MM/dd", New CultureInfo("en-US").DateTimeFormat)
            End If

        End Get
        Set(value As DateTime)
            TextBox1.Text = value.ToString("yyyy/MM/dd", New CultureInfo(Session("CultureInfo").ToString.Trim).DateTimeFormat)
        End Set
    End Property



    'I used Enum to show the little Intellisense box pop up with your options for culture
    Public Enum DefaultCultureOption
        Hijri
        Grgorian
    End Enum

    Public Property DefaultCalendarCulture() As DefaultCultureOption
        Get
            Return m_DefaultCalendarCulture
        End Get
        Set(value As DefaultCultureOption)
            m_DefaultCalendarCulture = value
        End Set
    End Property

    Private m_DefaultCalendarCulture As DefaultCultureOption


    Public Property TxtDate() As TextBox
        Get
            Return TextBox1
        End Get
        Set(value As TextBox)
            TextBox1 = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(value As String)
            TextBox1.Text = value
        End Set
    End Property

#End Region


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Attributes.Add("readonly", "readonly")
        If Not IsNothing(Session("CultureInfo")) AndAlso Session("CultureInfo").ToString = "ar-SA" Then
            DateType = "ummalqura"
            Me.DefaultCalendarCulture = DefaultCultureOption.Hijri
        Else
            DateType = "Gregorian"
            Me.DefaultCalendarCulture = DefaultCultureOption.Grgorian
        End If



    End Sub

    
    
End Class
