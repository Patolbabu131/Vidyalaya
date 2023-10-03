using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidyalaya.Models
{
    public class Teachers
    {
        [Key]
        public int TId { get; set; }
        [DisplayName("Teacher")]
        public string TName { get; set; }
        [DisplayName("Subject")]
        public string TSubject { get; set; }
        [DisplayName("Standard")]
        public string TStandard { get; set; }
        [ForeignKey("SId")]
        public int SId { get; set; }

       
    }
}
