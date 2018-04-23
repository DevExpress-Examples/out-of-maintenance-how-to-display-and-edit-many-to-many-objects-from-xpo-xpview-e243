using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

/// <summary>
/// Summary description for XpoHelper
/// </summary>
public static class XpoHelper {
    static XpoHelper() {
        UpdateSchema();
        CreateDefaultObjects();
    }

    public static Session GetNewSession() {
        return new Session(DataLayer);
    }

    public static UnitOfWork GetNewUnitOfWork() {
        return new UnitOfWork(DataLayer);
    }

    private readonly static object lockObject = new object();

    static IDataLayer fDataLayer;
    static IDataLayer DataLayer {
        get {
            if(fDataLayer == null) {
                lock(lockObject) {
                    fDataLayer = GetDataLayer();
                }
            }
            return fDataLayer;
        }
    }

    private static IDataLayer GetDataLayer() {
        XpoDefault.Session = null;

        InMemoryDataStore provider = new InMemoryDataStore();
        IDataLayer dl = new SimpleDataLayer(provider);

        return dl;
    }

    private static void UpdateSchema() {
        GetNewSession().UpdateSchema(typeof(DataObjects.Department).Assembly);
    }

    private static void CreateDefaultObjects() {
        using(UnitOfWork uow = GetNewUnitOfWork()) {
            DataObjects.Department dept = new DataObjects.Department(uow);
            dept.Name = "Dept A";
            DataObjects.Location loc = new DataObjects.Location(uow);
            loc.Region = "North";
            loc.Departments.Add(dept);
            uow.CommitChanges();
        }
    }
}
