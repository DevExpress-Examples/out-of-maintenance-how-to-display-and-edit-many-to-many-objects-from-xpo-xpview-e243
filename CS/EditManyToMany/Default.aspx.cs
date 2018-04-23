using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Web.ASPxEditors;

namespace EditManyToMany {
    public partial class _Default : System.Web.UI.Page {
        Session fSession;
        protected void Page_Init(object sender, EventArgs e) {
            fSession = XpoHelper.GetNewSession();
            BindGrid();
        }

        private void BindGrid() {
            // Create XPView
            XPClassInfo locationClass = fSession.GetClassInfo<DataObjects.Location>();
            XPClassInfo intermediateClass = locationClass.GetMember("Departments").IntermediateClass;
            XPView view = new XPView(fSession, intermediateClass);
            view.Properties.Add(new ViewProperty("Region", DevExpress.Xpo.SortDirection.Ascending, "Locations.Region", false, true));
            view.Properties.Add(new ViewProperty("Department", DevExpress.Xpo.SortDirection.Ascending, "Departments.Name", false, true));
            view.Properties.Add(new ViewProperty("LocID", DevExpress.Xpo.SortDirection.Ascending, "Locations.Oid", false, true));
            view.Properties.Add(new ViewProperty("DeptID", DevExpress.Xpo.SortDirection.Ascending, "Departments.Oid", false, true));
            view.Properties.Add(new ViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, intermediateClass.KeyProperty.Name, false, true));

            ASPxGridView1.KeyFieldName = "Oid";
            ASPxGridView1.DataSource = view;            
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
            object[] keys = (object[])ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "LocID", "DeptID"); 
            DataObjects.Location loc = fSession.GetObjectByKey<DataObjects.Location>(keys[0]);
            DataObjects.Department dept = fSession.GetObjectByKey<DataObjects.Department>(keys[1]);
            ASPxTextEdit edLoc = (ASPxTextEdit)ASPxGridView1.FindEditFormTemplateControl("editLocation");
            ASPxTextEdit edDept = (ASPxTextEdit)ASPxGridView1.FindEditFormTemplateControl("editDepartment");
            loc.Region = edLoc.Text;
            dept.Name = edDept.Text;
            fSession.Save(new object[] { loc, dept });

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            
            BindGrid(); // refresh the grid
        }
    }
}
