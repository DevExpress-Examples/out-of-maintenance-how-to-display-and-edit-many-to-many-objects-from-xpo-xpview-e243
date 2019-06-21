<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/EditManyToMany/Default.aspx) (VB: [Default.aspx](./VB/EditManyToMany/Default.aspx))
* [Default.aspx.cs](./CS/EditManyToMany/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/EditManyToMany/Default.aspx.vb))
* [PersistentClasses.cs](./CS/EditManyToMany/PersistentClasses.cs) (VB: [PersistentClasses.vb](./VB/EditManyToMany/PersistentClasses.vb))
* [XpoHelper.cs](./CS/EditManyToMany/XpoHelper.cs) (VB: [XpoHelper.vb](./VB/EditManyToMany/XpoHelper.vb))
<!-- default file list end -->
# How to display and edit many-to-many objects from XPO XPView
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e243/)**
<!-- run online end -->


<p><strong>Foreword</strong> It's not a good idea to edit many-to-many objects in one grid from the usability point of view. Usually, objects are modified individually. To link objects,  display a list of associated objects and Add / Remove buttons in a similar manner as a User Permissions dialog is implemented in Windows.</p><p>This example simply demonstrates how to load properties of a many-to-many association into the XPView component and display it in the ASPxGridView. In addition, the ASPxGridView.RowUpdating event is handled to allow a user to edit values displayed in the grid.</p>

<br/>


