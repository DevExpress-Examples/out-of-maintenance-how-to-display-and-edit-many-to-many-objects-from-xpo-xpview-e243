using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;

namespace DataObjects {
    public class Department: XPObject {
        public Department(Session session)
            : base(session) { }

        private string _Name;
        public string Name {
            get {
                return _Name;
            }
            set {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        [Association("Locations-Departments")]
        public XPCollection<Location> Locations {
            get {
                return GetCollection<Location>("Locations");
            }
        }
    }

    public class Location: XPObject {
        public Location(Session session)
            : base(session) { }

        private string _Region;
        public string Region {
            get {
                return _Region;
            }
            set {
                SetPropertyValue("Region", ref _Region, value);
            }
        }

        [Association("Locations-Departments")]
        public XPCollection<Department> Departments {
            get {
                return GetCollection<Department>("Departments");
            }
        }
    }
}
