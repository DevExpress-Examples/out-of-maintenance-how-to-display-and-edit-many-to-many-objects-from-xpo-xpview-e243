Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Metadata

''' <summary>
''' Summary description for XpoHelper
''' </summary>
Public NotInheritable Class XpoHelper
	Private Sub New()
	End Sub
	Shared Sub New()
		UpdateSchema()
		CreateDefaultObjects()
	End Sub

	Public Shared Function GetNewSession() As Session
		Return New Session(DataLayer)
	End Function

	Public Shared Function GetNewUnitOfWork() As UnitOfWork
		Return New UnitOfWork(DataLayer)
	End Function

	Private ReadOnly Shared lockObject As Object = New Object()

	Private Shared fDataLayer As IDataLayer
	Private Shared ReadOnly Property DataLayer() As IDataLayer
		Get
			If fDataLayer Is Nothing Then
				SyncLock lockObject
					fDataLayer = GetDataLayer()
				End SyncLock
			End If
			Return fDataLayer
		End Get
	End Property

	Private Shared Function GetDataLayer() As IDataLayer
		XpoDefault.Session = Nothing

		Dim provider As New InMemoryDataStore()
		Dim dl As IDataLayer = New SimpleDataLayer(provider)

		Return dl
	End Function

	Private Shared Sub UpdateSchema()
		GetNewSession().UpdateSchema(GetType(DataObjects.Department).Assembly)
	End Sub

	Private Shared Sub CreateDefaultObjects()
		Using uow As UnitOfWork = GetNewUnitOfWork()
			Dim dept As New DataObjects.Department(uow)
			dept.Name = "Dept A"
			Dim loc As New DataObjects.Location(uow)
			loc.Region = "North"
			loc.Departments.Add(dept)
			uow.CommitChanges()
		End Using
	End Sub
End Class
