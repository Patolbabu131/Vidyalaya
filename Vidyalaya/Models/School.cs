using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Net;

namespace Vidyalaya.Models
{
    public class School
    {
        [Key]
        public int SId { get; set; }
    
        [DisplayName("School")]
        public string SName { get; set; }
        [DisplayName("Loaction")]
        public string SAddress { get; set; }
        [DisplayName("City")]
        public string SCity { get; set; }
        [DisplayName("State")]
        public string SState { get; set; }
       
    }
}
