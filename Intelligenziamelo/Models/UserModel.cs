using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intelligenziamelo.Models
{
    public class UserModel
    {
        public bool Login { get; set; }
        public User User { get; set; }
        public DescriptionUser DescriptionUser { get; set; }
        public Model Model { get; set; }

    }
}