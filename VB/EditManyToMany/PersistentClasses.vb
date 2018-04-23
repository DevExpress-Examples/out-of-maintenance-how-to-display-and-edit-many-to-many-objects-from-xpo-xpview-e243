Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo

Namespace DataObjects
	Public Class Department
		Inherits XPObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _Name, value)
			End Set
		End Property

		<Association("Locations-Departments")> _
		Public ReadOnly Property Locations() As XPCollection(Of Location)
			Get
				Return GetCollection(Of Location)("Locations")
			End Get
		End Property
	End Class

	Public Class Location
		Inherits XPObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private _Region As String
		Public Property Region() As String
			Get
				Return _Region
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Region", _Region, value)
			End Set
		End Property

		<Association("Locations-Departments")> _
		Public ReadOnly Property Departments() As XPCollection(Of Department)
			Get
				Return GetCollection(Of Department)("Departments")
			End Get
		End Property
	End Class
End Namespace
