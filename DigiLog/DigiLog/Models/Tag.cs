using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Tag
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string TagNumber { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }


    }
}
