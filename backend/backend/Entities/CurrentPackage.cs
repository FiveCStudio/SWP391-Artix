using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class CurrentPackage
    {
        [Key]
        public int CurrentPackageID { get; set; } // Primary key
        public int CreatorID { get; set; } // Foreign key
        public int PackageID { get; set; } // Foreign key
        public DateTime Date { get; set; }

        [ForeignKey("CreatorID")]
        public Creator Creator { get; set; } // Navigation property

        [ForeignKey("PackageID")]
        public Package Package { get; set; } // Navigation property
    }
}
