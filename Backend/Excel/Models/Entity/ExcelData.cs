using System;

namespace Models.Entity
{
    public class ExcelData
    {
        public Guid UID { get; set; }
        public string? FeatureID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int VersionNo { get; set; }
    }
}
