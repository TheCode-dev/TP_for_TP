using ExpressiveAnnotations.Attributes;
using System.Resources;
using System.Xml.Linq;

namespace TP_KP_Belyshev.Models
{
    public class UsersModel

    {
        //[RequiredIf("FirstName == true", ErrorMessage = "fsd")]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string CreateDate { get; set; }
        public string LastLogin { get; set; }
        public string Status { get; set; }
        public string DiscontID { get; set; }
        public string? Bonus { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IsManager { get; set; }
        public string DiscontPercent { get; set; }
    }
}
