using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Tag
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string TagNumber { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }


    }
}
