using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Package
    {
        [Key]
        public int PackageID { get; set; } // Primary key
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public float PackagePrice { get; set; }
    }
}
