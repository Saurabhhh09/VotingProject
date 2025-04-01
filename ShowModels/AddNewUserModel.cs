using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class AddNewUserModel : RegistrationModel
    {
        [Required]
        public EnumRole Role { get; set; }
    }
}
