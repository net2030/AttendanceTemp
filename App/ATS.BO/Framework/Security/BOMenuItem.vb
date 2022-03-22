#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Transactions
Imports System.Reflection
Imports ATS.BO.Framework
Imports ATS.DA.Framework
Imports System.ComponentModel
#End Region

Namespace Framework

#Region " BOMenuItem "
    Public Class BOMenuItem
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Return False
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Return Nothing
        End Function

        Public Overrides Function GetDataset(ByVal BOEntity As BOEntityBase, Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return Nothing
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Return Nothing
        End Function

        Protected Overrides Function MapPropertiesToEntity(ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Return Nothing
        End Function
#End Region

    End Class
#End Region

#Region " BOEMenuItem "
    <Serializable()> _
    Public Class BOEMenuItem
        Inherits BOEntityBase

#Region " Constructors "
        Public Sub New()
            colChildMenuItems = New BOMenuItems
        End Sub
#End Region

#Region " Local Variables  "
        Private intMenuItemId As Integer
        Private strMenuItemName As String
        Private strDescription As String
        Private strUrl As String
        Private intParentMenuItemId As Integer
        Private intDisplaySequence As Integer
        Private bolIsAlwaysEnabled As Boolean
        Private strMenuItemTag As String
        Private colChildMenuItems As BOMenuItems

#End Region
#Region " Properties "
        Public Property ChildMenuItems() As BOMenuItems
            Get
                Return colChildMenuItems
            End Get
            Set(ByVal value As BOMenuItems)
                colChildMenuItems = value
            End Set
        End Property
        Public Property MenuItemId() As Integer
            Get
                Return intMenuItemId
            End Get
            Set(ByVal value As Integer)
                intMenuItemId = value
            End Set
        End Property
        Public Property MenuItemName() As String
            Get
                Return strMenuItemName
            End Get
            Set(ByVal value As String)
                strMenuItemName = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return strDescription
            End Get
            Set(ByVal value As String)
                strDescription = value
            End Set
        End Property
        Public Property Url() As String
            Get
                Return strUrl
            End Get
            Set(ByVal value As String)
                strUrl = value
            End Set
        End Property
        Public Property ParentMenuItemId() As Integer
            Get
                Return intParentMenuItemId
            End Get
            Set(ByVal value As Integer)
                intParentMenuItemId = value
            End Set
        End Property
        Public Property DisplaySequence() As Integer
            Get
                Return intDisplaySequence
            End Get
            Set(ByVal value As Integer)
                intDisplaySequence = value
            End Set
        End Property
        Public Property IsAlwaysEnabled() As Boolean
            Get
                Return bolIsAlwaysEnabled
            End Get
            Set(ByVal value As Boolean)
                bolIsAlwaysEnabled = value
            End Set
        End Property
        Public Property MenuItemTag() As String
            Get
                Return strMenuItemTag
            End Get
            Set(ByVal value As String)
                strMenuItemTag = value
            End Set
        End Property
#End Region

#Region " Properties Mapping "
        Public Sub MapEntityToProperties(ByVal entity As ICommonAttributes)
            If Not IsNothing(entity) Then
                MyBase.AddedDate = entity.AddedDate
                MyBase.AddedUserAccountId = entity.AddedUserAccountId
                MyBase.UpdatedDate = entity.UpdatedDate
                MyBase.UpdatedUserAccountId = entity.UpdatedUserAccountId
                MyBase.VersionNo = entity.VersionNo
                MyBase.AddedUserName = entity.AddedUserName
                MyBase.UpdatedUserName = entity.UpdatedUserName
                Me.MapEntityToCustomProperties(entity)
            End If
        End Sub
        Private Sub MapEntityToCustomProperties(ByVal entity As ICommonAttributes)
            Dim eMenuItem As MenuItem = CType(entity, MenuItem)
            With eMenuItem
                intMenuItemId = .MenuItemId
                strMenuItemName = .MenuItemName
                strDescription = .Description
                strUrl = .Url
                intParentMenuItemId = .ParentMenuItemId
                intDisplaySequence = .DisplaySequence
                bolIsAlwaysEnabled = .IsAlwaysEnabled
                strMenuItemTag = .MenuItemTag
            End With
        End Sub
#End Region

#Region "Public Methods"
        Public Function HasAccessToMenu( _
        ByVal UserAccount As BOEUserAccount, _
        ByVal Roles As BORoles) As Boolean
            If IsAlwaysEnabled Then
                Return True
            Else
                'Loop through all the roles this user is in.  The first time the user has
                'access to the menu item then return true.  If you get through all the
                'roles then the user does not have access to this menu item.
                For Each bRole As BOERole In Roles
                    'Check if this user is in this role
                    If bRole.RoleUserAccounts.IsUserInRole(UserAccount.UserAccountId) Then
                        'Try to find the capability with the menu item Id.
                        Dim capabilities As IEnumerable(Of BOERoleCapability) = bRole.RoleCapabilities.GetByMenuItemId(intMenuItemId)

                        For Each Capability As BOERoleCapability In capabilities
                            If (Capability IsNot Nothing) AndAlso (Capability.AccessFlag <> BOERoleCapability.AccessFlagEnum.None) Then
                                'If the record is in the table and the user has access other then None then return true.
                                Return True
                            End If
                        Next
                    End If
                Next
            End If

            'If it gets here then the user didn't have access to this menu item.  BUT they may have access
            'to one of its children, now check the children and if they have access to any of  them  then 
            'return true.
            If ChildMenuItems.Count > 0 Then
                For Each child As BOEMenuItem In ChildMenuItems
                    If child.HasAccessToMenu(UserAccount, Roles) Then
                        Return True
                    End If
                Next
            End If

            'If it never found a role with any capability then return false.
            Return False
        End Function

#End Region

    End Class
#End Region

#Region " BOMenuItems "
    Public Class BOMenuItems
        Inherits BOListBase(Of BOEMenuItem)

        ''' <summary>
        ''' This will load up the object with the correct parent\child relationships 
        ''' within the menu structure. Any parent menu item will have its 
        ''' child menu items loaded in it's ChildMenuItems property.
        ''' </summary>
        Public Sub Load()
            'Load the list from the database.  This will then be traversed to create the 
            'parent child relationships in for each menu item.
            Dim MenuItems As List(Of MenuItem) = New DAMenuItem().[Load]()

            'Traverse through the list to create the parent child relationships                                                    
            For Each eMenuItem As MenuItem In MenuItems
                Dim beMenuItem As New BOEMenuItem
                beMenuItem.MapEntityToProperties(eMenuItem)

                ' Check if the menu already exists in this object.
                If MenuExists(beMenuItem.MenuItemId) = False Then
                    'Doesn't exist so now check if this is a top level item.
                    If (IsNothing(beMenuItem.ParentMenuItemId) Or (beMenuItem.ParentMenuItemId = 0)) Then
                        'Top level item so just add it.
                        Me.Add(beMenuItem)
                    Else
                        ' Get the parent menu item from this object if it exists.
                        Dim Parent As BOEMenuItem = GetByMenuItemId(Convert.ToInt32(beMenuItem.ParentMenuItemId))

                        If Parent Is Nothing Then
                            ' If it gets here then the parent isn't in the list yet.
                            ' Find the parent in the list.                            
                            Dim newParentMenuItem As BOEMenuItem = FindOrLoadParent(MenuItems, Convert.ToInt32(beMenuItem.ParentMenuItemId))

                            ' Add the current child menu item to the newly added parent
                            newParentMenuItem.ChildMenuItems.Add(beMenuItem)
                        Else
                            ' Parent already existed in this object.
                            ' Add this menu to the child of the parent
                            Parent.ChildMenuItems.Add(beMenuItem)
                        End If
                    End If
                End If
            Next
        End Sub

        ''' <summary>
        ''' Checks if the menu item exists.  This will look at the child object of the menu also.
        ''' </summary>
        Public Function MenuExists(ByVal menuItemId As Integer) As Boolean
            Return (GetByMenuItemId(menuItemId) IsNot Nothing)
        End Function

        ''' <summary>
        ''' Returns the menu item for the specified id.  
        ''' This will search through all child items in the list.
        ''' </summary>
        Public Function GetByMenuItemId(ByVal MenuItemId As Integer) As BOEMenuItem
            For Each beMenuItem As BOEMenuItem In Me
                ' Check if this is the item we are looking for
                If beMenuItem.MenuItemId = MenuItemId Then
                    Return beMenuItem
                Else
                    ' Check if this menu has children
                    If beMenuItem.ChildMenuItems.Count > 0 Then
                        ' Search the children for this item.
                        Dim childMenuItem As BOEMenuItem = beMenuItem.ChildMenuItems.GetByMenuItemId(MenuItemId)

                        ' If the menu is found in the children then it won't be null
                        If childMenuItem IsNot Nothing Then
                            Return childMenuItem
                        End If
                    End If
                End If
            Next

            'It wasn't found so return null.
            Return Nothing
        End Function


#Region "Private Methods"
        Private Function FindOrLoadParent( _
        ByVal MenuItems As List(Of MenuItem), _
        ByVal ParentMenuItemId As Integer) As BOEMenuItem
            ' Find the menu item in the entity list.
            Dim ParentMenuItem As MenuItem = MenuItems.[Single](Function(m) m.MenuItemId = ParentMenuItemId)

            ' Load this into the business object.
            Dim menuItemBO As New BOEMenuItem
            menuItemBO.MapEntityToProperties(ParentMenuItem)

            ' Check if it has a parent
            If IsNothing(ParentMenuItem.ParentMenuItemId) Then
                Me.Add(menuItemBO)
            Else
                ' Since this has a parent it should be added to its parent's children.
                ' Try to find the parent in the list already.
                Dim parent As BOEMenuItem = GetByMenuItemId(Convert.ToInt32(ParentMenuItem.ParentMenuItemId))

                If parent Is Nothing Then
                    ' This one's parent wasn't found.  So add it.
                    Dim newParent As BOEMenuItem = FindOrLoadParent(MenuItems, Convert.ToInt32(ParentMenuItem.ParentMenuItemId))
                    newParent.ChildMenuItems.Add(menuItemBO)
                Else
                    ' Add this menu to the child of the parent
                    parent.ChildMenuItems.Add(menuItemBO)
                End If
            End If

            Return menuItemBO
        End Function
#End Region

    End Class

#End Region


End Namespace

