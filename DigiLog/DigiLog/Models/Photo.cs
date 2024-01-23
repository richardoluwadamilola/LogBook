
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Photo
    {
        [Key]
        public long PhotoId { get; set; }
        [Display(Name = "Photo (Base64 Encoded)")]
        public byte[]? PhotoData { get; set; }

        [ForeignKey("Visitor")]
        public long VisitorId { get; set; }
        public Visitor? Visitor { get; set; }
    }
}
