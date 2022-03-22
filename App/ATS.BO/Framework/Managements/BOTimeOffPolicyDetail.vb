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

#Region " BOTimeOffPolicyDetail "
    Public Class BOTimeOffPolicyDetail
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Dim oTimeOffPolicyDetail As New DATimeOffPolicyDetail
            If oTimeOffPolicyDetail.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            End If
            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oTimeOffPolicyDetail As New DATimeOffPolicyDetail
            Return MapEntityToProperties(oTimeOffPolicyDetail.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bTimeOffPolicyDetail As New BOETimeOffPolicyDetail

            If Not IsNothing(Entity) Then
                Dim eTimeOffPolicyDetail As TimeOffPolicyDetail = CType(Entity, TimeOffPolicyDetail)
                With bTimeOffPolicyDetail
                    .AddedDate = eTimeOffPolicyDetail.AddedDate
                    .AddedUserAccountId = eTimeOffPolicyDetail.AddedUserAccountId
                    .UpdatedDate = eTimeOffPolicyDetail.UpdatedDate
                    .UpdatedUserAccountId = eTimeOffPolicyDetail.UpdatedUserAccountId
                    .VersionNo = eTimeOffPolicyDetail.VersionNo

                    .TimeOffPolicyDetailId = eTimeOffPolicyDetail.TimeOffPolicyDetailId
                    .TimeOffPolicyId = eTimeOffPolicyDetail.TimeOffPolicyId
                    .GatepassTypeId = eTimeOffPolicyDetail.GatepassTypeId
                    .EarnPeriodId = eTimeOffPolicyDetail.EarnPeriodId
                    .ResetToZeroPeriodId = eTimeOffPolicyDetail.ResetToZeroPeriodId
                    .InitialSetToMinutes = eTimeOffPolicyDetail.InitialSetToMinutes
                    .ResetToMinutes = eTimeOffPolicyDetail.ResetToMinutes
                    .EarnMinutes = eTimeOffPolicyDetail.EarnMinutes
                    .EffectiveDate = eTimeOffPolicyDetail.EffectiveDate
                    .IsCarryForward = eTimeOffPolicyDetail.IsCarryForward
                    .MinValue = eTimeOffPolicyDetail.MinValue
                    .MaxValue = eTimeOffPolicyDetail.MaxValue
                    .AddedUserName = eTimeOffPolicyDetail.AddedUserName
                    .UpdatedUserName = eTimeOffPolicyDetail.UpdatedUserName
                End With
            End If

            Return bTimeOffPolicyDetail
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eTimeOffPolicyDetail As New TimeOffPolicyDetail

            If Not IsNothing(Entity) Then
                Dim bTimeOffPolicyDetail As BOETimeOffPolicyDetail = CType(Entity, BOETimeOffPolicyDetail)
                With eTimeOffPolicyDetail
                    .AddedDate = bTimeOffPolicyDetail.AddedDate
                    .AddedUserAccountId = bTimeOffPolicyDetail.AddedUserAccountId
                    .UpdatedDate = bTimeOffPolicyDetail.UpdatedDate
                    .UpdatedUserAccountId = bTimeOffPolicyDetail.UpdatedUserAccountId
                    .VersionNo = bTimeOffPolicyDetail.VersionNo

                    .TimeOffPolicyDetailId = bTimeOffPolicyDetail.TimeOffPolicyDetailId
                    .TimeOffPolicyId = bTimeOffPolicyDetail.TimeOffPolicyId
                    .GatepassTypeId = bTimeOffPolicyDetail.GatepassTypeId
                    .EarnPeriodId = bTimeOffPolicyDetail.EarnPeriodId
                    .ResetToZeroPeriodId = bTimeOffPolicyDetail.ResetToZeroPeriodId
                    .InitialSetToMinutes = bTimeOffPolicyDetail.InitialSetToMinutes
                    .ResetToMinutes = bTimeOffPolicyDetail.ResetToMinutes
                    .EarnMinutes = bTimeOffPolicyDetail.EarnMinutes
                    .EffectiveDate = bTimeOffPolicyDetail.EffectiveDate
                    .IsCarryForward = bTimeOffPolicyDetail.IsCarryForward
                    .MinValue = bTimeOffPolicyDetail.MinValue
                    .MaxValue = bTimeOffPolicyDetail.MaxValue
                End With
            End If

            Return eTimeOffPolicyDetail
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal TimeOffPolicyId As Integer,
                            ByVal GatepassTypeId As Integer,
                            ByVal EarnPeriodId As Integer,
                            ByVal ResetToZeroPeriodId As Integer,
                            ByVal InitialSetToMinutes As Decimal,
                            ByVal ResetToMinutes As Decimal,
                            ByVal EarnMinutes As Decimal,
                            ByVal EffectiveDate As Date,
                            ByVal IsCarryForward As Boolean,
                            ByVal MinValue As Decimal,
                            ByVal MaxValue As Decimal,
                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicyDetail As New DATimeOffPolicyDetail
            If oTimeOffPolicyDetail.Add(TimeOffPolicyId,
                             GatepassTypeId,
                             EarnPeriodId,
                             ResetToZeroPeriodId,
                             InitialSetToMinutes,
                             ResetToMinutes,
                             EarnMinutes,
                             EffectiveDate,
                             IsCarryForward,
                             MinValue,
                             MaxValue,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oTimeOffPolicyDetail.Identity
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(ByVal TimeOffPolicyDetailId As Integer,
                            ByVal TimeOffPolicyId As Integer,
                            ByVal GatepassTypeId As Integer,
                            ByVal EarnPeriodId As Integer,
                            ByVal ResetToZeroPeriodId As Integer,
                            ByVal InitialSetToMinutes As Decimal,
                            ByVal ResetToMinutes As Decimal,
                            ByVal EarnMinutes As Decimal,
                            ByVal EffectiveDate As Date,
                            ByVal IsCarryForward As Boolean,
                            ByVal MinValue As Decimal,
                            ByVal MaxValue As Decimal,
                            ByVal UserAccountId As Integer,
                            ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicyDetail As New DATimeOffPolicyDetail
            If oTimeOffPolicyDetail.Update(TimeOffPolicyDetailId,
                                 TimeOffPolicyId,
                                 GatepassTypeId,
                                 EarnPeriodId,
                                 ResetToZeroPeriodId,
                                 InitialSetToMinutes,
                                 ResetToMinutes,
                                 EarnMinutes,
                                 EffectiveDate,
                                 IsCarryForward,
                                 MinValue,
                                 MaxValue,
                                 UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function UpdateBalance(ByVal TimeOffBalanceId As Integer,
                                    ByVal CarryForward As Decimal,
                                    ByVal UserAccountId As Integer,
                                    ByVal VersionNo As Byte()) As Boolean

            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicyDetail As New DATimeOffPolicyDetail
            If oTimeOffPolicyDetail.UpdateBalance(TimeOffBalanceId,
                                 CarryForward,
                                 UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicyDetail.InfoMessage
            End If

            Return boolSeccessed

        End Function

        Public Function GetTimeOffPolicyDetailsByPolicyId(ByVal TimeOffPolicyId As Integer) As System.Data.DataSet
            Return New DATimeOffPolicyDetail().GetTimeOffPolicyDetailsByPolicyId(TimeOffPolicyId)
        End Function

        Public Function GetPeriodsDataSet() As System.Data.DataSet
            Return New DATimeOffPolicyDetail().GetPeriodsList()
        End Function

        Public Function GetTypesByLeaveType(ByVal LeaveType As Integer) As System.Data.DataSet
            Return New DATimeOffPolicyDetail().GetTypesByLeaveType(LeaveType)
        End Function
#End Region

    End Class
#End Region

#Region " BOETimeOffPolicyDetail "
    Public Class BOETimeOffPolicyDetail
        Inherits BOEntityBase

#Region " Local Variables  "

        Private intTimeOffPolicyDetailId As Integer
        Private intTimeOffPolicyId As Integer
        Private intGatepassTypeId As Integer
        Private intEarnPeriodId As Integer
        Private intResetToZeroPeriodId As Integer
        Private decInitialSetToMinutes As Decimal
        Private decResetToMinutes As Decimal
        Private decEarnMinutes As Decimal
        Private datEffectiveDate As Date
        Private bolIsCarryForward As Boolean
        Private decMinValue As Decimal
        Private decMaxValue As Decimal

#End Region

#Region " Public Properties "

        Public Property TimeOffPolicyDetailId() As Integer
            Get
                Return intTimeOffPolicyDetailId
            End Get
            Set(ByVal value As Integer)
                intTimeOffPolicyDetailId = value
            End Set
        End Property

        Public Property TimeOffPolicyId() As Integer
            Get
                Return intTimeOffPolicyId
            End Get
            Set(ByVal value As Integer)
                intTimeOffPolicyId = value
            End Set
        End Property

        Public Property GatepassTypeId() As Integer
            Get
                Return intGatepassTypeId
            End Get
            Set(ByVal value As Integer)
                intGatepassTypeId = value
            End Set
        End Property
        Public Property EarnPeriodId() As Integer
            Get
                Return intEarnPeriodId
            End Get
            Set(ByVal value As Integer)
                intEarnPeriodId = value
            End Set
        End Property


        Public Property ResetToZeroPeriodId() As Integer
            Get
                Return intResetToZeroPeriodId
            End Get
            Set(ByVal value As Integer)
                intResetToZeroPeriodId = value
            End Set
        End Property

        Public Property InitialSetToMinutes() As Decimal
            Get
                Return decInitialSetToMinutes
            End Get
            Set(ByVal value As Decimal)
                decInitialSetToMinutes = value
            End Set
        End Property

        Public Property ResetToMinutes() As Decimal
            Get
                Return decResetToMinutes
            End Get
            Set(ByVal value As Decimal)
                decResetToMinutes = value
            End Set
        End Property

        Public Property EarnMinutes() As Decimal
            Get
                Return decEarnMinutes
            End Get
            Set(ByVal value As Decimal)
                decEarnMinutes = value
            End Set
        End Property

        Public Property EffectiveDate() As Date
            Get
                Return datEffectiveDate
            End Get
            Set(ByVal value As Date)
                datEffectiveDate = value
            End Set
        End Property

        Public Property IsCarryForward() As Boolean
            Get
                Return bolIsCarryForward
            End Get
            Set(ByVal value As Boolean)
                bolIsCarryForward = value
            End Set
        End Property

        Public Property MinValue() As Decimal
            Get
                Return decMinValue
            End Get
            Set(ByVal value As Decimal)
                decMinValue = value
            End Set
        End Property

        Public Property MaxValue() As Decimal
            Get
                Return decMaxValue
            End Get
            Set(ByVal value As Decimal)
                decMaxValue = value
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
            Dim eTimeOffPolicyDetail As TimeOffPolicyDetail = CType(entity, TimeOffPolicyDetail)
            With eTimeOffPolicyDetail
                intTimeOffPolicyDetailId = .TimeOffPolicyDetailId
                intTimeOffPolicyId = .TimeOffPolicyId
                intGatepassTypeId = .GatepassTypeId
                intEarnPeriodId = .EarnPeriodId
                intResetToZeroPeriodId = .ResetToZeroPeriodId
                decInitialSetToMinutes = .InitialSetToMinutes
                decResetToMinutes = .ResetToMinutes
                decEarnMinutes = .EarnMinutes
                datEffectiveDate = .EffectiveDate
                bolIsCarryForward = .IsCarryForward
                decMinValue = .MinValue
                decMaxValue = .MinValue
            End With
        End Sub
#End Region

    End Class
#End Region



End Namespace

