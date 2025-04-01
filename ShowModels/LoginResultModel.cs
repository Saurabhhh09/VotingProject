using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class LoginResultModel
    {       
            public bool IsSuccessful { get; set; }
            public int UserId { get; set; }
            public string Role { get; set; }
       
    }
}
