using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Database
{
    public class Login
    {
        [Key]
        public string Token { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
        
        public DateTime? LogoutAt { get; set; }

        public virtual User ForUser { get; set; }
    }
}
