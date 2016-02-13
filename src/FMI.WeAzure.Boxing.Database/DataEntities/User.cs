using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class User
    {
        public User()
        {
            Predictions = new HashSet<Prediction>();
            Logins = new HashSet<Login>();
        }

        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<Prediction> Predictions { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
