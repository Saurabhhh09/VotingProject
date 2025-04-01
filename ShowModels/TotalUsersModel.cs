using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class TotalUsersModel
    {
        public List<UserModel> Users { get; set; }
        public UserModel User { get; set; }
    }
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string First_Name { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string Last_Name { get; set; }

        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        [Required]
        public EnumRole Role { get; set; }

        public string Date { get; set; }
    }
}
