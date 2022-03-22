Imports System.Web.UI
Namespace TimeLive.PageStateAdapter
''' <summary>
''' This class provide session based persistance of Viewstate 
''' </summary>
''' <remarks>Viewstate Session persistent adapter class</remarks>
	Public Class PageStateAdapter
		Inherits System.Web.UI.Adapters.PageAdapter
        Public Overrides Function GetStatePersister() As PageStatePersister
            'If Not Me.Page.Title.Contains("Show Report") Then
            '    Return New SessionPageStatePersister(Me.Page)
            'End If
        End Function
	End Class
End Namespace
