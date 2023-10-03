using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidyalaya.Models
{
    public class Activities
    {
        [Key]
        public int AId { get; set; }
        [DisplayName("Activity")]
        public string ATitle { get; set; }
        [DisplayName("Descriptoin")]
        public string ADescription { get; set; }
        [ForeignKey("SId")]
        public int SId { get; set; }

    }
}
