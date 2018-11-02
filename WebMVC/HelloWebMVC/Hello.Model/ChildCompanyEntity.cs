using System;

namespace Hello.Model
{
    [Serializable]
    public class ChildCompanyEntity
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string BelongToCompany { get; set; }
        public string Department { get; set; }
        public string ChildCompanyName { get; set; }
        public int ParentID { get; set; }
        public int Level { get; set; }
    }
}
