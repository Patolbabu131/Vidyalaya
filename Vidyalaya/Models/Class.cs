using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidyalaya.Models
{
    public class Class
    {
        [Key]
        public int CId { get; set; }
        [DisplayName("Class")]
        public string CName { get; set; }
        [DisplayName("Subject")]
        public string CSubject { get; set; }
        [DisplayName("Standard")]
        public string CStandard { get; set; }
        [DisplayName("Room No.")]
        public string CRoomNo { get; set; }
        [ForeignKey("SId")]
        public int SId { get; set; }

    }
}
