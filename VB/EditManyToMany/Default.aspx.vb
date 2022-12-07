Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Web

Namespace EditManyToMany
	Partial Public Class _Default
		Inherits System.Web.UI.Page
		Private fSession As Session
		Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
			fSession = XpoHelper.GetNewSession()
			BindGrid()
		End Sub

		Private Sub BindGrid()
			' Create XPView
			Dim locationClass As XPClassInfo = fSession.GetClassInfo(Of DataObjects.Location)()
			Dim intermediateClass As XPClassInfo = locationClass.GetMember("Departments").IntermediateClass
			Dim view As New XPView(fSession, intermediateClass)
			view.Properties.Add(New ViewProperty("Region", DevExpress.Xpo.SortDirection.Ascending, "Locations.Region", False, True))
			view.Properties.Add(New ViewProperty("Department", DevExpress.Xpo.SortDirection.Ascending, "Departments.Name", False, True))
			view.Properties.Add(New ViewProperty("LocID", DevExpress.Xpo.SortDirection.Ascending, "Locations.Oid", False, True))
			view.Properties.Add(New ViewProperty("DeptID", DevExpress.Xpo.SortDirection.Ascending, "Departments.Oid", False, True))
			view.Properties.Add(New ViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, intermediateClass.KeyProperty.Name, False, True))

			ASPxGridView1.KeyFieldName = "Oid"
			ASPxGridView1.DataSource = view
			ASPxGridView1.DataBind()
		End Sub

		Protected Sub ASPxGridView1_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
			Dim keys() As Object = CType(ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "LocID", "DeptID"), Object())
			Dim loc As DataObjects.Location = fSession.GetObjectByKey(Of DataObjects.Location)(keys(0))
			Dim dept As DataObjects.Department = fSession.GetObjectByKey(Of DataObjects.Department)(keys(1))
			Dim edLoc As ASPxTextEdit = CType(ASPxGridView1.FindEditFormTemplateControl("editLocation"), ASPxTextEdit)
			Dim edDept As ASPxTextEdit = CType(ASPxGridView1.FindEditFormTemplateControl("editDepartment"), ASPxTextEdit)
			loc.Region = edLoc.Text
			dept.Name = edDept.Text
			fSession.Save(New Object() { loc, dept })

			e.Cancel = True
			ASPxGridView1.CancelEdit()

			BindGrid() ' refresh the grid
		End Sub
	End Class
End Namespace
